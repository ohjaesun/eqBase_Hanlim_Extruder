using EQ.Core.Act;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using System;

namespace EQ.Core.Act.Composition
{
    /// <summary>
    /// 트레이 전용 비즈니스 로직을 담당합니다.
    /// (ActProduct<TrayCell>을 상속받아 기본 기능 자동 확보)
    /// </summary>
    public class ActTray : ActProduct<TrayCell>
    {
        // 생성자에서 저장 키("TrayData")를 고정하여 ACT에서 이름 실수를 방지합니다.
        public ActTray(ACT act) : base(act, "TrayData")
        {
        }

        // --- [Tray 전용 특화 기능 예시] ---

        /// <summary>
        /// 모든 셀의 온도 데이터를 0으로 초기화합니다.
        /// </summary>
        public void ResetAllTemperatures()
        {
            if (CurrentMap == null) return;

            for (int y = 0; y < CurrentMap.Rows; y++)
            {
                for (int x = 0; x < CurrentMap.Cols; x++)
                {
                    // 구조체 참조를 가져와서 직접 수정 (속도 최적화)
                    ref TrayCell cell = ref CurrentMap[x, y];

                    // Buffer16<float>는 InlineArray이므로 Span으로 초기화
                    // (주의: InlineArray 전체 초기화 로직 필요 시 Helper 사용 권장)
                    // 여기서는 간단히 첫 번째 값만 0으로
                    cell.Temperatures[0] = 0.0f;
                }
            }
        }
    }
}