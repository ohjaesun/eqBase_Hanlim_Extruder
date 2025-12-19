using System;

namespace EQ.Domain.Entities
{
    /// <summary>
    /// 차트 데이터 포인트
    /// </summary>
    public class DataPoint
    {
        /// <summary>
        /// 타임스탬프
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 값
        /// </summary>
        public double Value { get; set; }
    }
}
