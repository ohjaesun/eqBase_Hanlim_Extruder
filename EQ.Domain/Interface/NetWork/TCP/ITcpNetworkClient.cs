// EQ.Domain/Interface/ITcpNetworkClient.cs
using Tcp; // PacketData가 정의된 네임스페이스

namespace EQ.Domain.Interface
{
    public interface ITcpNetworkClient
    {
        // 이벤트
        event Action<PacketData> OnRead;
        event Action OnConnected;
        event Action OnDisconnected;

        // 속성
        bool IsConnected { get; }

        // 메서드
        void Init(string name, string ip, int port, int bufSize = 8192, bool autoReconnect = true, EndType endType = EndType.None);
        void Close();
        Task SendData(string data);
        Task SendData(byte[] data);
    }
}