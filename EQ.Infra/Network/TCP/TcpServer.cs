using EQ.Common.Logs; 
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;

//서버로 동작시 연결 관리 클래스
namespace Tcp
{
    public class TcpServer
    {
        private Socket _listener;
        private CancellationTokenSource _cts;
        private EndType _endType;
        private readonly ConcurrentDictionary<string, ClientConnection> _clients = new();

        public event Action<PacketData> OnDataReceived;
        public event Action<string> OnClientConnected;
        public event Action<string> OnClientDisconnected;

        public void Start(int port, EndType endType = EndType.None)
        {
            _endType = endType;
            _cts = new CancellationTokenSource();

            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _listener.Bind(new IPEndPoint(IPAddress.Any, port));
            _listener.Listen(10); // 동시 접속 대기열 10개

            Log.Instance.Info($"[TCP Server] 서버 시작. Port: {port}");
            Task.Run(() => AcceptLoop(_cts.Token), _cts.Token);
        }

        public void Stop()
        {
            _cts?.Cancel();
            _listener?.Close();

            foreach (var client in _clients.Values)
            {
                client.Close();
            }
            _clients.Clear();
            Log.Instance.Info("[TCP Server] 서버 중지.");
        }

        private async Task AcceptLoop(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                  
                    Socket clientSocket = await _listener.AcceptAsync(token);

                    ClientConnection connection = new ClientConnection(clientSocket, _endType);

                    if (_clients.TryAdd(connection.Id, connection))
                    {
                        Log.Instance.Info($"[TCP Server] 클라이언트 연결: {connection.Id}");
                        OnClientConnected?.Invoke(connection.Id);

                        // 이벤트 핸들러 연결
                        connection.OnRead += (packet) => OnDataReceived?.Invoke(packet);
                        connection.OnDisconnected += (id) =>
                        {
                            if (_clients.TryRemove(id, out var removedConn))
                            {
                                Log.Instance.Warning($"[TCP Server] 클라이언트 연결 종료: {id}");
                                OnClientDisconnected?.Invoke(id);
                            }
                        };

                       
                        _ = connection.StartReceiving();
                    }
                    else
                    {
                     
                        connection.Close();
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Log.Instance.Debug("[TCP Server] AcceptLoop 중지됨.");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[TCP Server] AcceptLoop 오류: {ex.Message}");
            }
        }

        public async Task Send(string clientId, string data)
        {
            if (_clients.TryGetValue(clientId, out var client))
            {
                await client.SendData(data);
            }
        }

        public async Task Send(string clientId, byte[] data)
        {
            if (_clients.TryGetValue(clientId, out var client))
            {
                await client.SendData(data);
            }
        }
    }
}