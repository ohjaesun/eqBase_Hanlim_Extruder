using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Domain.Interface; // ISerialPortClient
using System.Collections.Concurrent;
using Tcp;

namespace EQ.Core.Act.Composition
{
    /// <summary>
    /// 등록된 여러 ISerialPortClient 인스턴스를 관리하는 모듈
    /// </summary>
    public class ActSerialPort : ActComponent
    {
        private readonly ConcurrentDictionary<string, ISerialPortClient> _clients = new();

        public event Action<PacketData> OnDataReceived;

        public ActSerialPort(ACT act) : base(act) { }

        /// <summary>
        /// (FormSplash에서 호출)
        /// 생성된 Serial 클라이언트 인스턴스를 사전에 등록합니다.
        /// </summary>
        public void RegisterClient(string name, ISerialPortClient client)
        {
            if (_clients.TryAdd(name, client))
            {
                client.OnRead += (packet) => HandleSerialData(name, packet);
                Log.Instance.Info($"[ActSerialPort] 클라이언트 등록됨: {name}");
            }
            else
            {
                Log.Instance.Warning($"[ActSerialPort] 이미 등록된 클라이언트 이름입니다: {name}");
            }
        }

        private void HandleSerialData(string clientName, PacketData packet)
        {
            // 1. Core 레벨에서 공통 로깅 수행
            Log.Instance.Info($"[{clientName} RECV] {packet.Str}");

            // 2. 필요시, Core 레벨의 이벤트를 다시 발행하여 UI 등 외부에 알림
            OnDataReceived?.Invoke(packet);

            // 3. (추가 확장) clientName에 따라 특정 로직 수행
            // switch (clientName)
            // {
            //    case "Scanner":
            //        _act.Recipe.CurrentBarcode = packet.Str;
            //        break;
            // }
        }
        /// <summary>
        /// (시퀀스 등에서 호출)
        /// 이름으로 등록된 클라이언트의 인스턴스를 반환합니다.
        /// </summary>
        public ISerialPortClient GetClient(string name)
        {
            if (_clients.TryGetValue(name, out var client))
            {
                return client;
            }

            Log.Instance.Error($"[ActSerialPort] 등록되지 않은 클라이언트를 요청했습니다: {name}");
            return null; // 또는 예외 발생
        }

        /// <summary>
        /// (프로그램 종료 시 호출)
        /// 등록된 모든 Serial 클라이언트의 연결을 종료합니다.
        /// </summary>
        public void CloseAll()
        {
            foreach (var client in _clients.Values)
            {
                client.Close();
            }
            _clients.Clear();
        }
    }
}