using EQ.Common.Logs;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Tcp
{
    /// <summary>
    /// 서버 측에서 개별 클라이언트와의 통신을 담당하는 세션 클래스
    /// </summary>
    internal class ClientConnection
    {
        private readonly Socket _socket;
        private readonly EndType _endType;
        private readonly byte[] _messageBuffer; // STX/ETX 파싱을 위한 메시지 버퍼
        private int _bufferOffset = 0;

        public string Id { get; }
        public event Action<PacketData> OnRead;
        public event Action<string> OnDisconnected;

        public ClientConnection(Socket socket, EndType endType)
        {
            _socket = socket;
            _endType = endType;
            Id = socket.RemoteEndPoint.ToString();
            _messageBuffer = new byte[1024 * 8]; // 8KB 버퍼
        }

        public async Task StartReceiving()
        {
            var receiveBuffer = new Memory<byte>(new byte[4096]); // 4KB 수신 버퍼
            try
            {
                while (_socket.Connected)
                {
                    int received = await _socket.ReceiveAsync(receiveBuffer, SocketFlags.None);
                    if (received == 0)
                    {
                        // 0바이트 수신 = 정상 종료
                        break;
                    }

                    // 수신된 데이터 처리
                    ProcessReceivedData(receiveBuffer.Span.Slice(0, received));
                }
            }
            catch (SocketException ex)
            {
                Log.Instance.Error($"[ClientConnection {Id}] 수신 오류: {ex.SocketErrorCode}");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[ClientConnection {Id}] 알 수 없는 오류: {ex.Message}");
            }
            finally
            {
                Close();
                OnDisconnected?.Invoke(Id);
            }
        }

        private void ProcessReceivedData(Span<byte> data)
        {
            if (_endType == EndType.ETX)
            {
                foreach (byte b in data)
                {
                    if (b == 0x02) // STX
                    {
                        _bufferOffset = 0; // 버퍼 리셋
                    }
                    else if (b == 0x03) // ETX
                    {
                        if (_bufferOffset > 0)
                        {
                            byte[] packetBytes = new byte[_bufferOffset];
                            Buffer.BlockCopy(_messageBuffer, 0, packetBytes, 0, _bufferOffset);
                            string text = Encoding.UTF8.GetString(packetBytes);

                            OnRead?.Invoke(new PacketData
                            {
                                ClientId = this.Id,
                                Ip = ((IPEndPoint)_socket.RemoteEndPoint).Address.ToString(),
                                Port = ((IPEndPoint)_socket.RemoteEndPoint).Port.ToString(),
                                Str = text,
                                Bytes = packetBytes
                            });
                        }
                        _bufferOffset = 0; // 버퍼 리셋
                    }
                    else
                    {
                        if (_bufferOffset < _messageBuffer.Length)
                        {
                            _messageBuffer[_bufferOffset++] = b;
                        }
                        else
                        {
                            Log.Instance.Error($"[ClientConnection {Id}] STX/ETX 버퍼 오버플로우. 버퍼를 리셋합니다.");
                            _bufferOffset = 0;
                        }
                    }
                }
            }
            else // EndType.None
            {
                byte[] packetBytes = data.ToArray();
                string text = Encoding.UTF8.GetString(packetBytes);
                OnRead?.Invoke(new PacketData
                {
                    ClientId = this.Id,
                    Ip = ((IPEndPoint)_socket.RemoteEndPoint).Address.ToString(),
                    Port = ((IPEndPoint)_socket.RemoteEndPoint).Port.ToString(),
                    Str = text,
                    Bytes = packetBytes
                });
            }
        }

        public async Task SendData(string data)
        {
            if (string.IsNullOrEmpty(data)) return;

            byte[] bDts = Encoding.UTF8.GetBytes(data);
            await SendData(bDts);
        }

        public async Task SendData(byte[] data)
        {
            if (!_socket.Connected) return;

            try
            {
                if (_endType == EndType.ETX)
                {
                    byte[] stx = { 0x02 };
                    byte[] etx = { 0x03 };
                    byte[] sendData = new byte[data.Length + 2];

                    Buffer.BlockCopy(stx, 0, sendData, 0, 1);
                    Buffer.BlockCopy(data, 0, sendData, 1, data.Length);
                    Buffer.BlockCopy(etx, 0, sendData, data.Length + 1, 1);

                    await _socket.SendAsync(new ArraySegment<byte>(sendData), SocketFlags.None);
                }
                else
                {
                    await _socket.SendAsync(new ArraySegment<byte>(data), SocketFlags.None);
                }
            }
            catch (SocketException ex)
            {
                Log.Instance.Error($"[ClientConnection {Id}] 송신 오류: {ex.SocketErrorCode}");
                Close();
            }
        }

        public void Close()
        {
            _socket?.Close();
        }
    }
}