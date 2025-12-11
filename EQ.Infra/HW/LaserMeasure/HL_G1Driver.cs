using EQ.Common.Logs;
using EQ.Domain.Entities.LaserMeasure;
using EQ.Domain.Interface.LaserMeasure;
using Modbus.Device;
using System.Collections.Concurrent;
using System.IO.Ports;

namespace EQ.Infra.LaserMeasure
{
    /// <summary>
    /// Panasonic HL-G1 시리즈 레이저 계측기 드라이버
    /// RS485 통신 기반
    /// </summary>
    public class HL_G1Driver : ILaserMeasure
    {
        #region Fields
        private SerialPort? _port;
        private ModbusSerialMaster? _master;
        private LaserMeasureConfig? _config;
        private bool _disposed = false;

        private readonly ConcurrentDictionary<int, double> _lastValues = new();
        private readonly ConcurrentDictionary<int, System.Timers.Timer> _continuousTimers = new();
        private readonly ConcurrentDictionary<int, TaskCompletionSource<double>> _pendingMeasures = new();

        private string _receiveBuffer = string.Empty;
        private readonly byte[] _receiveDataBuffer = new byte[20];
        private int _offset = 0;
        #endregion

        #region Properties
        public bool IsConnected => _port?.IsOpen ?? false;
        public bool SupportsContinuous => true;
        #endregion

        #region Events
        public event EventHandler<LaserMeasureEventArgs>? OnMeasured;
        #endregion

        #region Initialization
        public bool Init(LaserMeasureConfig config)
        {
            try
            {
                _config = config;

                _port = new SerialPort(config.PortName)
                {
                    BaudRate = config.BaudRate,
                    DataBits = 8,
                    Parity = Parity.None,
                    StopBits = StopBits.One
                };

                _port.DataReceived += SerialDataReceived;
                _port.Open();

                _master = ModbusSerialMaster.CreateRtu(_port);

                Log.Instance.Info(string.Format("HL_G1Driver: {0} 초기화 완료 (Port: {1})", config.Name, config.PortName));
                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Error(string.Format("HL_G1Driver: 초기화 실패 - {0}", ex.Message));
                return false;
            }
        }

        public void Close()
        {
            StopAllContinuous();

            if (_port?.IsOpen == true)
            {
                _port.Close();
            }
            _port?.Dispose();
            _port = null;

            Log.Instance.Info("HL_G1Driver: 종료");
        }
        #endregion

        #region Measurement
        public async Task<double> MeasureAsync(int channelId = 0)
        {
            if (!IsConnected || _config == null)
            {
                throw new InvalidOperationException("드라이버가 초기화되지 않았습니다.");
            }

            var tcs = new TaskCompletionSource<double>();
            _pendingMeasures[channelId] = tcs;

            // 측정 명령 전송
            SendMeasureCommand(_config.SlaveId);

            // 타임아웃 처리
            using var cts = new CancellationTokenSource(_config.Timeout);
            try
            {
                await using (cts.Token.Register(() => tcs.TrySetCanceled()))
                {
                    return await tcs.Task;
                }
            }
            catch (OperationCanceledException)
            {
                _pendingMeasures.TryRemove(channelId, out _);
                throw new TimeoutException("측정 타임아웃");
            }
        }

        public double GetLastValue(int channelId = 0)
        {
            return _lastValues.GetValueOrDefault(channelId, 0.0);
        }

        private void SendMeasureCommand(byte slaveId)
        {
            if (_port == null || !_port.IsOpen) return;

            char[] bytes = new char[10];
            bytes[0] = '%';
            bytes[1] = (char)0x0;
            bytes[2] = (char)(slaveId + 0x30);
            bytes[3] = '#';
            bytes[4] = 'R';
            bytes[5] = 'M';
            bytes[6] = 'D';
            bytes[7] = '*';
            bytes[8] = '*';
            bytes[9] = (char)0x0d;

            _port.Write(bytes, 0, bytes.Length);
        }
        #endregion

        #region Continuous Measurement
        public void StartContinuous(int channelId = 0, int intervalMs = 100)
        {
            if (_continuousTimers.ContainsKey(channelId))
            {
                return; // 이미 실행 중
            }

            var timer = new System.Timers.Timer(intervalMs);
            timer.Elapsed += async (s, e) =>
            {
                try
                {
                    double value = await MeasureAsync(channelId);
                    _lastValues[channelId] = value;
                    OnMeasured?.Invoke(this, new LaserMeasureEventArgs(channelId, value));
                }
                catch (Exception ex)
                {
                    OnMeasured?.Invoke(this, new LaserMeasureEventArgs(channelId, 0, true, ex.Message));
                }
            };

            timer.AutoReset = true;
            timer.Start();
            _continuousTimers[channelId] = timer;

            Log.Instance.Info(string.Format("HL_G1Driver: 연속 측정 시작 (Channel: {0}, Interval: {1}ms)", channelId, intervalMs));
        }

        public void StopContinuous(int channelId = 0)
        {
            if (_continuousTimers.TryRemove(channelId, out var timer))
            {
                timer.Stop();
                timer.Dispose();
                Log.Instance.Info(string.Format("HL_G1Driver: 연속 측정 정지 (Channel: {0})", channelId));
            }
        }

        public bool IsContinuousRunning(int channelId = 0)
        {
            return _continuousTimers.ContainsKey(channelId);
        }

        private void StopAllContinuous()
        {
            foreach (var kvp in _continuousTimers)
            {
                kvp.Value.Stop();
                kvp.Value.Dispose();
            }
            _continuousTimers.Clear();
        }
        #endregion

        #region Serial Data Handling
        private void SerialDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (_port == null) return;

                int bytesToRead = _port.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                _port.Read(buffer, 0, bytesToRead);

                for (int i = 0; i < buffer.Length; i++)
                {
                    if (buffer[i] == 0x0D)
                    {
                        ProcessReceivedData();
                        _receiveBuffer = string.Empty;
                        _offset = 0;
                    }
                    else
                    {
                        if (_offset < _receiveDataBuffer.Length)
                        {
                            _receiveDataBuffer[_offset++] = buffer[i];
                            _receiveBuffer += (char)buffer[i];
                        }
                        else
                        {
                            _receiveBuffer = string.Empty;
                            _offset = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(string.Format("HL_G1Driver: 수신 처리 오류 - {0}", ex.Message));
            }
        }

        private void ProcessReceivedData()
        {
            if (string.IsNullOrEmpty(_receiveBuffer)) return;

            try
            {
                if (_receiveBuffer.Contains("RMD"))
                {
                    int startIndex = _receiveBuffer.IndexOf('%');
                    string id = _receiveBuffer.Substring(startIndex + 1, 2);

                    startIndex = _receiveBuffer.IndexOf('D');
                    string data = _receiveBuffer.Substring(startIndex + 1, 7);

                    int slaveId = id[1] - 0x30;
                    double value = double.Parse(data);

                    _lastValues[0] = value;

                    // 대기 중인 측정 요청 완료
                    if (_pendingMeasures.TryRemove(0, out var tcs))
                    {
                        tcs.TrySetResult(value);
                    }

                    OnMeasured?.Invoke(this, new LaserMeasureEventArgs(0, value));
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error(string.Format("HL_G1Driver: 데이터 파싱 오류 - {0}", ex.Message));
            }
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            if (!_disposed)
            {
                Close();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
