using EQ.Common.Logs;
using EQ.Domain.Entities.LaserMeasure;
using EQ.Domain.Interface.LaserMeasure;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Text;

namespace EQ.Infra.LaserMeasure
{
    /// <summary>
    /// Omron ZW-7000 시리즈 레이저 계측기 드라이버
    /// TCP/IP 통신 기반
    /// 연속 측정 미지원
    /// </summary>
    public class ZW7000Driver : ILaserMeasure
    {
        #region Fields
        private TcpClient? _client;
        private NetworkStream? _stream;
        private LaserMeasureConfig? _config;
        private bool _disposed = false;

        private readonly ConcurrentDictionary<int, double> _lastValues = new();
        private readonly byte[] _measureCommand = { (byte)'m', 0x0d };
        #endregion

        #region Properties
        public bool IsConnected => _client?.Connected ?? false;
        public bool SupportsContinuous => false;
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

                _client = new TcpClient();
                _client.Connect(config.IpAddress, config.Port);
                _stream = _client.GetStream();

                Log.Instance.Info(string.Format("ZW7000Driver: {0} 초기화 완료 (IP: {1}:{2})", config.Name, config.IpAddress, config.Port));
                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Error(string.Format("ZW7000Driver: 초기화 실패 - {0}", ex.Message));
                return false;
            }
        }

        public void Close()
        {
            _stream?.Close();
            _client?.Close();
            _stream = null;
            _client = null;

            Log.Instance.Info("ZW7000Driver: 종료");
        }
        #endregion

        #region Measurement
        public async Task<double> MeasureAsync(int channelId = 0)
        {
            if (!IsConnected || _stream == null || _config == null)
            {
                throw new InvalidOperationException("드라이버가 초기화되지 않았습니다.");
            }

            try
            {
                // 측정 명령 전송
                await _stream.WriteAsync(_measureCommand, 0, _measureCommand.Length);

                // 응답 대기
                byte[] buffer = new byte[256];
                using var cts = new CancellationTokenSource(_config.Timeout);

                int bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length, cts.Token);

                if (bytesRead > 0)
                {
                    string response = Encoding.ASCII.GetString(buffer, 0, bytesRead).Trim();

                    if (double.TryParse(response, out double value))
                    {
                        _lastValues[channelId] = value;
                        OnMeasured?.Invoke(this, new LaserMeasureEventArgs(channelId, value));
                        return value;
                    }
                    else
                    {
                        throw new FormatException(string.Format("응답 파싱 실패: {0}", response));
                    }
                }

                throw new TimeoutException("응답 없음");
            }
            catch (OperationCanceledException)
            {
                throw new TimeoutException("측정 타임아웃");
            }
        }

        public double GetLastValue(int channelId = 0)
        {
            return _lastValues.GetValueOrDefault(channelId, 0.0);
        }
        #endregion

        #region Continuous Measurement (Not Supported)
        public void StartContinuous(int channelId = 0, int intervalMs = 100)
        {
            throw new NotSupportedException("ZW7000은 연속 측정을 지원하지 않습니다.");
        }

        public void StopContinuous(int channelId = 0)
        {
            // 연속 측정을 지원하지 않으므로 아무 작업 없음
        }

        public bool IsContinuousRunning(int channelId = 0)
        {
            return false;
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
