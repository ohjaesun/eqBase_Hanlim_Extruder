using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EQ.Core.Act.Composition
{
    /// <summary>
    /// 웨이퍼 전용 비즈니스 로직을 담당합니다.
    /// </summary>
    public class ActWafer : ActProduct<WaferCell>
    {
        // 생성자에서 저장 키("WaferMapData")를 고정
        public ActWafer(ACT act) : base(act, "WaferMapData")
        {
        }

        // --- [Wafer 전용 특화 기능 예시] ---

        /// <summary>
        /// 현재 웨이퍼의 수율(Yield)을 계산합니다.
        /// </summary>
        public double CalculateYield()
        {
            if (CurrentMap == null || CurrentMap.Rows == 0) return 0.0;

            int total = 0;
            int good = 0;

            for (int y = 0; y < CurrentMap.Rows; y++)
            {
                for (int x = 0; x < CurrentMap.Cols; x++)
                {
                    ref WaferCell cell = ref CurrentMap[x, y];

                    // None(빈 공간)과 Skip(검사 제외)은 모수에서 제외
                    if (cell.Grade != ProductUnitChipGrade.None &&
                        cell.Grade != ProductUnitChipGrade.Skip)
                    {
                        total++;
                        if (cell.Grade == ProductUnitChipGrade.Good)
                        {
                            good++;
                        }
                    }
                }
            }

            if (total == 0) return 0.0;
            return Math.Round((double)good / total * 100.0, 2);
        }

        /// <summary>
        /// 특정 Bin Code의 개수를 반환합니다.
        /// </summary>
        public int CountBinCode(short binCode)
        {
            if (CurrentMap == null) return 0;
            int count = 0;

            for (int y = 0; y < CurrentMap.Rows; y++)
            {
                for (int x = 0; x < CurrentMap.Cols; x++)
                {
                    if (CurrentMap[x, y].BinCode == binCode)
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}