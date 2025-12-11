namespace EQ.Domain.Entities.LaserMeasure
{
    /// <summary>
    /// 레이저 계측 이벤트 인자
    /// </summary>
    public class LaserMeasureEventArgs : EventArgs
    {
        /// <summary>
        /// 채널 ID
        /// </summary>
        public int ChannelId { get; }

        /// <summary>
        /// 측정 값 (mm)
        /// </summary>
        public double Value { get; }

        /// <summary>
        /// 측정 시간
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// 에러 여부
        /// </summary>
        public bool IsError { get; }

        /// <summary>
        /// 에러 메시지 (에러 시)
        /// </summary>
        public string ErrorMessage { get; }

        public LaserMeasureEventArgs(int channelId, double value, bool isError = false, string errorMessage = "")
        {
            ChannelId = channelId;
            Value = value;
            Timestamp = DateTime.Now;
            IsError = isError;
            ErrorMessage = errorMessage;
        }
    }
}
