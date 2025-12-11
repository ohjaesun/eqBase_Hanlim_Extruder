using EQ.Common.Logs;
using EQ.Domain.Interface;
using System;
using System.IO.Ports;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tcp; // PacketData

namespace EQ.Infra.Serial
{
    /// <summary>
    /// .NET의 System.IO.Ports.SerialPort를 사용한 ISerialPortClient 구현체
    /// (TCPClient.cs의 EndType 파싱 로직 재사용)
    /// </summary>
    public class SystemSerialPortClient : ISerialPortClient
    {
        public event Action<PacketData> OnRead;
        public event Action OnConnected;
        public event Action OnDisconnected;

        private SerialPort _serialPort;
        private CancellationTokenSource _cts;
        private Task _receiveLoopTask;

        // 설정값
        private string _name;
        private string _portName;
        private EndType _endType;

        // 파싱 버퍼 (TcpClient.cs와 동일)
        private byte[] _messageBuffer;
        private int _bufferOffset = 0;
        private bool _waitingForLF = false;

        public bool IsConnected => _serialPort?.IsOpen ?? false;

        public SystemSerialPortClient()
        {
        }

        public void Init(string name, string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits, EndType endType = EndType.None)
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                Close();
            }

            _name = name;
            _portName = portName;
            _endType = endType;
            _messageBuffer = new byte[8192]; // 8KB 버퍼
            _bufferOffset = 0;
            _waitingForLF = false;

            try
            {
                _serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
                _serialPort.ReadTimeout = 500;
                _serialPort.WriteTimeout = 500;
                _serialPort.Open();

                _cts = new CancellationTokenSource();
                _receiveLoopTask = Task.Run(() => ReceiveLoop(_cts.Token), _cts.Token);

                Log.Instance.Info($"[SerialClient {_name}] 포트 열기 성공: {portName} ({baudRate})");
                OnConnected?.Invoke();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[SerialClient {_name}] 포트 열기 실패: {portName}. {ex.Message}");
                OnDisconnected?.Invoke();
            }
        }

        public void Close()
        {
            _cts?.Cancel();
            _receiveLoopTask?.Wait(1000); // 수신 루프 종료 대기

            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
                _serialPort.Dispose();
            }
            _serialPort = null;
            _cts = null;

            Log.Instance.Info($"[SerialClient {_name}] 포트 닫힘: {_portName}");
            OnDisconnected?.Invoke();
        }

        private async Task ReceiveLoop(CancellationToken token)
        {
            var receiveBuffer = new Memory<byte>(new byte[4096]);
            try
            {
                while (!token.IsCancellationRequested && _serialPort.IsOpen)
                {
                    // SerialPort.BaseStream은 ReadAsync를 지원
                    int received = await _serialPort.BaseStream.ReadAsync(receiveBuffer, token);
                    if (received == 0)
                    {
                        continue; // (시리얼에서는 0이 올 수 있음)
                    }
                    ProcessReceivedData(receiveBuffer.Span.Slice(0, received));
                }
            }
            catch (OperationCanceledException)
            {
                // 정상 종료
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[SerialClient {_name}] 수신 루프 오류: {ex.Message}");
                // (오류 발생 시 포트 닫고 종료 알림)
                Close();
            }
        }

        /// <summary>
        /// (로직을 EQ.Infra/Network/TCP/TcpClient.cs 에서 그대로 가져옴)
        /// EndType.None, ETX, CR, LF, CRLF 모두 처리
        /// </summary>
        private void ProcessReceivedData(Span<byte> data)
        {
            // 1. None 타입은 즉시 처리
            if (_endType == EndType.None)
            {
                byte[] packetBytes = data.ToArray();
                string text = Encoding.UTF8.GetString(packetBytes);
                OnRead?.Invoke(new PacketData
                {
                    Name = _name, // (수정) IP/Port 대신 이름 사용
                    Str = text,
                    Bytes = packetBytes
                });
                return;
            }

            // 2. 그 외 타입은 바이트 단위로 파싱 (TcpClient.cs와 100% 동일)
            foreach (byte b in data)
            {
                if (_endType == EndType.CRLF && _waitingForLF)
                {
                    _waitingForLF = false;
                    if (b == 0x0A) continue;
                }

                bool isTerminator = false;
                bool resetBufferOnStart = false;

                switch (_endType)
                {
                    case EndType.ETX:
                        if (b == 0x02) resetBufferOnStart = true;
                        else if (b == 0x03) isTerminator = true;
                        break;
                    case EndType.CR:
                        if (b == 0x0D) isTerminator = true;
                        break;
                    case EndType.LF:
                        if (b == 0x0A) isTerminator = true;
                        break;
                    case EndType.CRLF:
                        if (b == 0x0D)
                        {
                            isTerminator = true;
                            _waitingForLF = true;
                        }
                        break;
                }

                if (isTerminator)
                {
                    if (_bufferOffset > 0)
                    {
                        byte[] packetBytes = new byte[_bufferOffset];
                        Buffer.BlockCopy(_messageBuffer, 0, packetBytes, 0, _bufferOffset);
                        string text = Encoding.UTF8.GetString(packetBytes);

                        OnRead?.Invoke(new PacketData
                        {
                            Name = _name, // (수정) IP/Port 대신 이름 사용
                            Str = text,
                            Bytes = packetBytes
                        });
                    }
                    _bufferOffset = 0; // 버퍼 리셋
                }
                else if (resetBufferOnStart)
                {
                    _bufferOffset = 0;
                }
                else
                {
                    if (_bufferOffset < _messageBuffer.Length)
                    {
                        _messageBuffer[_bufferOffset++] = b;
                    }
                    else
                    {
                        Log.Instance.Error($"[SerialClient {_name}] 파싱 버퍼 오버플로우. 버퍼를 리셋합니다.");
                        _bufferOffset = 0;
                    }
                }
            }
        }

        public async Task SendData(string data)
        {
            if (string.IsNullOrEmpty(data)) return;
            byte[] bDts = Encoding.UTF8.GetBytes(data.Trim());
            await SendData(bDts);
        }

        /// <summary>
        /// (로직을 EQ.Infra/Network/TCP/TcpClient.cs 에서 그대로 가져옴)
        /// Send 시 EndType에 맞는 종료자 추가
        /// </summary>
        public async Task SendData(byte[] data)
        {
            if (!IsConnected)
            {
                Log.Instance.Warning($"[SerialClient {_name}] 연결되지 않은 상태에서 송신 시도.");
                return;
            }

            try
            {
                byte[] sendData;
                switch (_endType)
                {
                    case EndType.ETX:
                        byte[] stx = { 0x02 }; byte[] etx = { 0x03 };
                        sendData = new byte[data.Length + 2];
                        Buffer.BlockCopy(stx, 0, sendData, 0, 1);
                        Buffer.BlockCopy(data, 0, sendData, 1, data.Length);
                        Buffer.BlockCopy(etx, 0, sendData, data.Length + 1, 1);
                        break;
                    case EndType.CR:
                        sendData = new byte[data.Length + 1];
                        Buffer.BlockCopy(data, 0, sendData, 0, data.Length);
                        sendData[data.Length] = 0x0D; // CR
                        break;
                    case EndType.LF:
                        sendData = new byte[data.Length + 1];
                        Buffer.BlockCopy(data, 0, sendData, 0, data.Length);
                        sendData[data.Length] = 0x0A; // LF
                        break;
                    case EndType.CRLF:
                        sendData = new byte[data.Length + 2];
                        Buffer.BlockCopy(data, 0, sendData, 0, data.Length);
                        sendData[data.Length] = 0x0D; // CR
                        sendData[data.Length + 1] = 0x0A; // LF
                        break;
                    case EndType.None:
                    default:
                        sendData = data;
                        break;
                }

                await _serialPort.BaseStream.WriteAsync(sendData, 0, sendData.Length);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[SerialClient {_name}] 송신 오류: {ex.Message}");
                Close(); // 송신 오류 시 포트 닫기
            }
        }
    }
}