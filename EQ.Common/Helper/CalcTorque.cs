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
        /// 토크 % → N·cm 변환
        /// </summary>
        /// <param name="model">SGMXJ 모터 모델</param>
        /// <param name="torquePercent">토크 지령 (%)</param>
        /// <returns>토크 (N·cm)</returns>
        public static double ToNcm(SGMXJModel model, double torquePercent)
        {
            // 1️⃣ 정격 토크 (Nm)
            double ratedTorqueNm = model switch
            {
                SGMXJModel.A5A => 0.159,
                SGMXJModel._01A => 0.318,
                SGMXJModel.C2A => 0.477,
                SGMXJModel._02A => 0.637,
                SGMXJModel._04A => 1.27,
                _ => throw new ArgumentOutOfRangeException(nameof(model))
            };

            // 2️⃣ % → Nm
            double torqueNm = ratedTorqueNm * torquePercent / 100.0;

            // 3️⃣ Nm → N·cm (×100)
            return torqueNm * 100.0;
        }
    }
}
