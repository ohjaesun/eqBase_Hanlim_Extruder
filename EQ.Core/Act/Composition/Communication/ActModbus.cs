// EQ.Core/Action/Composition/ActModbus.cs
using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Domain.Interface;
using System.Collections.Concurrent;

namespace EQ.Core.Act.Composition
{
    /// <summary>
    /// ActVision과 동일한 패턴으로,
    /// 등록된 여러 IModbusClient 인스턴스를 관리하는 모듈
    /// </summary>
    public class ActModbus : ActComponent
    {
        private readonly ConcurrentDictionary<string, IModbusClient> _clients = new();

        public ActModbus(ACT act) : base(act) { }

        /// <summary>
        /// (FormSplash에서 호출)
        /// 생성된 Modbus 클라이언트 인스턴스를 사전에 등록합니다.
        /// </summary>
        public void RegisterClient(string name, IModbusClient client)
        {
            if (_clients.TryAdd(name, client))
            {
                Log.Instance.Info($"[ActModbus] 클라이언트 등록됨: {name}");
            }
            else
            {
                Log.Instance.Warning($"[ActModbus] 이미 등록된 클라이언트 이름입니다: {name}");
            }
        }

        /// <summary>
        /// (시퀀스 등에서 호출)
        /// 이름으로 등록된 클라이언트의 인스턴스를 반환합니다.
        /// </summary>
        public IModbusClient GetClient(string name)
        {
            if (_clients.TryGetValue(name, out var client))
            {
                return client;
            }

            Log.Instance.Error($"[ActModbus] 등록되지 않은 클라이언트를 요청했습니다: {name}");
            return null; // 또는 예외 발생
        }

        /// <summary>
        /// (프로그램 종료 시 호출)
        /// 등록된 모든 Modbus 클라이언트의 연결을 종료합니다.
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