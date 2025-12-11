using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Domain.Interface;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace EQ.Core.Act.Composition
{
    /// <summary>
    /// 여러 온도 컨트롤러(ITemperatureController)를 통합 관리하는 모듈
    /// </summary>
    public class ActTemperature : ActComponent
    {
        // 이름으로 컨트롤러를 찾기 위한 딕셔너리
        private readonly ConcurrentDictionary<string, ITemperatureController> _controllers = new();

        public ActTemperature(ACT act) : base(act) { }

        /// <summary>
        /// (FormSplash 등에서 호출) 컨트롤러 등록
        /// </summary>
        public void Register(string name, ITemperatureController controller)
        {
            if (_controllers.TryAdd(name, controller))
            {
                Log.Instance.Info($"[ActTemp] 온도 컨트롤러 등록됨: {name}");
            }
            else
            {
                Log.Instance.Warning($"[ActTemp] 이미 등록된 이름입니다: {name}");
            }
        }
        public void Register(Enum zone, ITemperatureController controller)
        {
            Register(zone.ToString(), controller);
        }

        /// <summary>
        /// 이름으로 특정 컨트롤러 가져오기
        /// </summary>
        public ITemperatureController Get(string name)
        {
            if (_controllers.TryGetValue(name, out var ctrl))
            {
                return ctrl;
            }
            Log.Instance.Error($"[ActTemp] 존재하지 않는 컨트롤러 요청: {name}");
            return null;
        }

        public ITemperatureController Get(Enum zone)
        {
            return Get(zone.ToString());
        }

        /// <summary>
        /// 등록된 모든 컨트롤러 목록 반환 (일괄 제어용)
        /// </summary>
        public List<ITemperatureController> GetAll()
        {
            return _controllers.Values.ToList();
        }
    }
}