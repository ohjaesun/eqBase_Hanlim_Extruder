using EQ.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace EQ.Domain.Entities
{
   

    // --- Entity ---
    public class MotionInterlockItem
    {
        [ReadOnly(true)]
        public MotionID TargetAxis { get; set; } // 내가 움직이려는 축

        public InterLockType Type { get; set; }

        // 1. Motor Position 조건일 때
        public MotionID SourceAxis { get; set; }
        public double CompareValue { get; set; }     // 기준 값
        public double Range { get; set; } = 10.0;    // 범위 (Equal/NotEqual 시 사용)
        public CompareCondition Condition { get; set; }

        // 2. IO 조건일 때
        public int IoIndex { get; set; }
        public bool IoSignal { get; set; } = true;   // ON일때 걸리냐 OFF일때 걸리냐
        public bool IsInput { get; set; } = true;    // Input/Output 구분

        // 공통
        public StopDirection StopDir { get; set; }
        public string Description { get; set; }

        public MotionInterlockItem() { }

        // 편의상 설명 생성 메서드
        public void MakeDescription()
        {
            if (Type == InterLockType.Position || Type == InterLockType.DefinedPos)
            {
                string condStr = Condition switch
                {
                    CompareCondition.Less => $"< {CompareValue}",
                    CompareCondition.Greater => $"> {CompareValue}",
                    CompareCondition.Equal => $"{CompareValue}±{Range}",
                    CompareCondition.NotEqual => $"Not {CompareValue}±{Range}",
                    _ => ""
                };
                Description = $"IF [Axis {SourceAxis}] Pos {condStr} THEN Stop {TargetAxis} ({StopDir})";
            }
            else
            {
                string ioType = IsInput ? "IN" : "OUT";
                string sig = IoSignal ? "ON" : "OFF";
                Description = $"IF [{ioType}-{IoIndex:D4}] is {sig} THEN Stop {TargetAxis} ({StopDir})";
            }
        }
    }

    // --- UserOption Class ---
    public class UserOptionMotionInterlock
    {
        public List<MotionInterlockItem> Items { get; set; } = new List<MotionInterlockItem>();

        public UserOptionMotionInterlock() { }

        public void Synchronize()
        {
            // 삭제된 모터 ID에 대한 정리는 필요할 수 있으나, 
            // 인터락은 사용자가 직접 추가/삭제하는 데이터이므로 
            // 자동으로 생성하거나 지우는 로직은 최소화합니다.
            // 다만, 유효하지 않은 데이터 정리는 가능합니다.
        }

        // 특정 모터에 걸린 인터락 목록 가져오기
        public List<MotionInterlockItem> GetList(MotionID target)
        {
            return Items.Where(x => x.TargetAxis == target).ToList();
        }
    }
}