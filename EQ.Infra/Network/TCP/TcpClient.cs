using EQ.Common.Logs;
using EQ.Domain.Interface;
using System.Net.Sockets;
using System.Text;

namespace Tcp
{
    /// <summary>
    /// 자동 재연결 및 STX/ETX/CRLF 프로토콜을 지원하는 TCP 클라이언트
    /// </summary>
    public class TcpClient : ITcpNetworkClient
    {
        public event Action<PacketData> OnRead;
        public event Action OnConnected;
        public event Action OnDisconnected;

        private Socket _socket;
        private CancellationTokenSource _cts;

        // 설정값
        private string _ip;
        private int _port;
        private string _name;
        private bool _autoReconnect;
        private EndType _endType;

        // 파싱 버퍼
        private byte[] _messageBuffer;
        private int _bufferOffset = 0;
        private bool _waitingForLF = false; // [신규] CRLF 처리를 위한 상태

        public bool IsConnected => _socket?.Connected ?? false;

        public TcpClient()
        {
        }

        public void Init(string name, string ip, int port, int bufSize = 8192, bool autoReconnect = true, EndType endType = EndType.None)
        {
            _name = name;
            _ip = ip;
            _port = port;
            _autoReconnect = autoReconnect;
            _endType = endType;
            _messageBuffer = new byte[bufSize];

            _cts = new CancellationTokenSource();
            Task.Run(() => ConnectLoop(_cts.Token), _cts.Token);
        }

        public void Close()
        {
            _autoReconnect = false; // 재연결 루프 중단
            _cts?.Cancel();
            _socket?.Close();
            Log.Instance.Info($"[TCP Client {_name}] 연결이 수동으로 종료되었습니다.");
        }

        private async Task ConnectLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (!IsConnected)
                {
                    try
                    {
                        // 소켓 재성성
                        _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        _bufferOffset = 0; // [수정] 버퍼 오프셋 초기화
                        _waitingForLF = false; // [수정] 상태 초기화
                        await _socket.ConnectAsync(_ip, _port, token);

                        Log.Instance.Info($"[TCP Client {_name}] 서버 연결 성공: {_ip}:{_port}");
                        OnConnected?.Invoke();

                        // (Fire-and-Forget) 수신 루프 시작
                        _ = StartReceiving(token);
                    }
                    catch (SocketException ex)
                    {
                        Log.Instance.Warning($"[TCP Client {_name}] 연결 실패: {ex.SocketErrorCode}.");
                        OnDisconnected?.Invoke(); // 연결 시도 실패도 Disconnected 이벤트
                    }
                    catch (OperationCanceledException)
                    {
                        break; // Close()가 호출됨
                    }
                    catch (Exception ex)
                    {
                        Log.Instance.Error($"[TCP Client {_name}] 알 수 없는 연결 오류: {ex.Message}");
                        OnDisconnected?.Invoke();
                    }
                }

                await Task.Delay(5000, token); // 5초 대기
            }
        }

        private async Task StartReceiving(CancellationToken token)
        {
            var receiveBuffer = new Memory<byte>(new byte[4096]);
            try
            {
                while (_socket.Connected && !token.IsCancellationRequested)
                {
                    int received = await _socket.ReceiveAsync(receiveBuffer, SocketFlags.None);
                    if (received == 0)
                    {
                        break; // 서버가 연결을 정상 종료
                    }
                    ProcessReceivedData(receiveBuffer.Span.Slice(0, received));
                }
            }
            catch (SocketException ex)
            {
                Log.Instance.Error($"[TCP Client {_name}] 수신 오류: {ex.SocketErrorCode}");
            }
            catch (OperationCanceledException)
            {
                // 정상 종료
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[TCP Client {_name}] 수신 루프 오류: {ex.Message}");
            }
            finally
            {
                _socket.Close();
                OnDisconnected?.Invoke();
                Log.Instance.Warning($"[TCP Client {_name}] 서버와 연결이 끊어졌습니다.");
            }
        }

        /// <summary>
        /// [수정] EndType.None, ETX, CR, LF, CRLF 모두 처리
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
                    Name = _name,
                    Ip = _ip,
                    Port = _port.ToString(),
                    Str = text,
                    Bytes = packetBytes
                });
                return;
            }

            // 2. 그 외 타입은 바이트 단위로 파싱
            foreach (byte b in data)
            {
                // 2a. CRLF 상태 처리
                if (_endType == EndType.CRLF && _waitingForLF)
                {
                    _waitingForLF = false; // 상태 리셋
                    if (b == 0x0A) // LF
                    {
                        continue; // LF 무시 (이미 CR에서 처리)
                    }
                    // CR (0x0D) 다음에 LF (0x0A)가 아닌 다른 바이트가 오면
                    // 해당 바이트(b)는 다음 메시지의 시작으로 간주하고 아래 로직에서 처리
                }

                bool isTerminator = false;
                bool resetBufferOnStart = false; // STX용

                // 2b. 종료자 확인
                switch (_endType)
                {
                    case EndType.ETX:
                        if (b == 0x02) // STX
                        {
                            resetBufferOnStart = true;
                        }
                        else if (b == 0x03) // ETX
                        {
                            isTerminator = true;
                        }
                        break;
                    case EndType.CR:
                        if (b == 0x0D) isTerminator = true;
                        break;
                    case EndType.LF:
                        if (b == 0x0A) isTerminator = true;
                        break;
                    case EndType.CRLF:
                        if (b == 0x0D) // CR
                        {
                            isTerminator = true;
                            _waitingForLF = true; // [상태 변경] 다음 바이트가 LF인지 확인
                        }
                        break;
                }

                // 2c. 종료자 처리
                if (isTerminator)
                {
                    if (_bufferOffset > 0)
                    {
                        byte[] packetBytes = new byte[_bufferOffset];
                        Buffer.BlockCopy(_messageBuffer, 0, packetBytes, 0, _bufferOffset);
                        string text = Encoding.UTF8.GetString(packetBytes);

                        OnRead?.Invoke(new PacketData
                        {
                            Name = _name,
                            Ip = _ip,
                            Port = _port.ToString(),
                            Str = text,
                            Bytes = packetBytes
                        });
                    }
                    _bufferOffset = 0; // 버퍼 리셋
                }
                // 2d. 시작 신호 처리 (ETX 모드)
                else if (resetBufferOnStart)
                {
                    _bufferOffset = 0;
                }
                // 2e. 버퍼에 추가
                else
                {
                    if (_bufferOffset < _messageBuffer.Length)
                    {
                        _messageBuffer[_bufferOffset++] = b;
                    }
                    else
                    {
                        Log.Instance.Error($"[TCP Client {_name}] 파싱 버퍼 오버플로우. 버퍼를 리셋합니다.");
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
        /// [수정] Send 시 EndType에 맞는 종료자 추가
        /// </summary>
        public async Task SendData(byte[] data)
        {
            if (!IsConnected)
            {
                Log.Instance.Warning($"[TCP Client {_name}] 연결되지 않은 상태에서 송신 시도.");
                return;
            }

            try
            {
                byte[] sendData;

                switch (_endType)
                {
                    case EndType.ETX:
                        byte[] stx = { 0x02 };
                        byte[] etx = { 0x03 };
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

                await _socket.SendAsync(new ArraySegment<byte>(sendData), SocketFlags.None);
            }
            catch (SocketException ex)
            {
                Log.Instance.Error($"[TCP Client {_name}] 송신 오류: {ex.SocketErrorCode}");
                _socket.Close(); // 송신 오류 시 연결을 닫고 재연결 루프가 처리하도록 함
                OnDisconnected?.Invoke();
            }
        }
    }
}