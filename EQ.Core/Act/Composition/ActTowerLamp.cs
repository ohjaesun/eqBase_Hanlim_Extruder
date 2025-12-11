using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Domain.Enums;
using System.Collections.Generic; // Dictionary 사용
using System.Threading.Tasks;

namespace EQ.Core.Act
{
    /// <summary>
    /// 타워 램프 및 부저를 FSM 상태에 맞게 제어하는 모듈
    /// </summary>
    public class ActTowerLamp : ActComponent
    {
        // [수정 1] 상태별 램프 매핑을 저장할 딕셔너리
        private Dictionary<EqState, IO_OUT> _lampMap;
        private EqState _currentState = EqState.Init; // 현재 상태 추적용 변수

        public ActTowerLamp(ACT act) : base(act)
        {
            InitializeMap();
        }

        // [수정 2] 기본 매핑 초기화 (생성자에서 호출)
        private void InitializeMap()
        {
            _lampMap = new Dictionary<EqState, IO_OUT>
            {
                { EqState.Init,    IO_OUT.Tower_Lamp_Yellow },
                { EqState.Idle,    IO_OUT.Tower_Lamp_Green },
                { EqState.Running, IO_OUT.Tower_Lamp_Yellow }, // Init과 동일
                { EqState.Error,   IO_OUT.Tower_Lamp_Red }
            };
        }

        // [수정 3] 외부(옵션 등)에서 색상 변경이 필요할 때 호출
        public void RemapState(EqState state, IO_OUT newLampColor)
        {
            if (_lampMap.ContainsKey(state))
            {
                _lampMap[state] = newLampColor;
            }
            else
            {
                _lampMap.Add(state, newLampColor);
            }
        }

        // FSM 상태에 따른 램프/부저 설정
        public void SetState(EqState state)
        {
            if (_currentState != state)
            {
                _currentState = state;
                // ★ 통계 분석을 위한 핵심 로그 (파싱 키워드: "[State]")
                Log.Instance.Time($"[State] {state}");
            }

            // 모든 램프/부저를 끈다
            AllOff();

            // [수정 4] 딕셔너리를 사용하여 램프 켜기
            if (_lampMap.TryGetValue(state, out IO_OUT targetLamp))
            {
                SetLamp(targetLamp, true);
            }

            // 에러 상태일 때는 부저 추가 (부저는 별도 매핑 없이 고정 로직 유지)
            if (state == EqState.Error)
            {
                SetBuzzer(true);
            }
        }

        /// <summary>
        /// 현재 램프 IO 상태를 읽어 논리적인 장비 상태(EqState)를 반환합니다.
        /// </summary>
        public EqState GetState()
        {
            // [수정 5] 매핑된 정보를 기반으로 상태 판단 (우선순위 유지: Error > Running > Idle)

            if (IsLampOn(EqState.Error)) return EqState.Error;

            if (IsLampOn(EqState.Running)) return EqState.Running;

            if (IsLampOn(EqState.Idle)) return EqState.Idle;

            // 아무것도 켜져 있지 않거나 Init 상태
            return EqState.Init;
        }

        // 상태에 매핑된 램프가 켜져 있는지 확인하는 헬퍼
        private bool IsLampOn(EqState state)
        {
            if (_lampMap.TryGetValue(state, out IO_OUT lamp))
            {
                return _act.IO.ReadOutput(lamp);
            }
            return false;
        }

        private void SetLamp(IO_OUT lamp, bool on)
        {
            _act.IO.WriteOutput(lamp, on);
        }

        private void SetBuzzer(bool on)
        {
            // IO.cs에 정의된 부저 사용
            _act.IO.WriteOutput(IO_OUT.BUZZER_1, on);
        }

        private void AllOff()
        {
            SetLamp(IO_OUT.Tower_Lamp_Red, false);
            SetLamp(IO_OUT.Tower_Lamp_Yellow, false);
            SetLamp(IO_OUT.Tower_Lamp_Green, false);
            SetBuzzer(false);
        }

        public void SilenceBuzzer()
        {
            SetBuzzer(false);
        }
    }
}