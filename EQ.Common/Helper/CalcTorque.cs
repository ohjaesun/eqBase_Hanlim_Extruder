using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Common.Helper
{
    /// <summary>
    /// SGMXJ 시리즈 모터 모델
    /// </summary>
    public enum SGMXJModel
    {
        A5A,   // 50W
        _01A,  // 100W
        C2A,   // 150W
        _02A,  // 200W
        _04A   // 400W
    }
    public static class CalcTorque
    {
        /// <summary>
        /// SGMXJ 모델별 정격 토크 (Nm)
        /// </summary>
        private static double GetRatedTorqueNm(SGMXJModel model)
        {
            return model switch
            {
                SGMXJModel.A5A => 0.159,
                SGMXJModel._01A => 0.318,
                SGMXJModel.C2A => 0.477,
                SGMXJModel._02A => 0.637,
                SGMXJModel._04A => 1.27,
                _ => throw new ArgumentOutOfRangeException(nameof(model), model, "Unknown SGMXJ model")
            };
        }

        /// <summary>
        /// 토크 % → N·cm 변환
        /// </summary>
        public static double ToNcm(SGMXJModel model, double torquePercent)
        {
            double ratedTorqueNm = GetRatedTorqueNm(model);

            double torqueNm = ratedTorqueNm * torquePercent / 100.0;
            return torqueNm * 100.0;
        }

        /// <summary>
        /// N·cm → 토크 % 변환
        /// </summary>
        public static double ToPercent(SGMXJModel model, double torqueNcm)
        {
            double ratedTorqueNm = GetRatedTorqueNm(model);
            double ratedTorqueNcm = ratedTorqueNm * 100.0;

            return torqueNcm / ratedTorqueNcm * 100.0;
        }
    }
}
