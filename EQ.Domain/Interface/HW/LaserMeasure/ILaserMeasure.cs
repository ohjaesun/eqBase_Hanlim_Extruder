using EQ.Domain.Entities.LaserMeasure;

namespace EQ.Domain.Interface.LaserMeasure
{
    /// <summary>
    /// 레이저 거리 계측기 인터페이스
    /// HL_G1, ZW7000 등 다양한 레이저 센서를 추상화합니다.
    /// </summary>
    public interface ILaserMeasure : IDisposable
    {
        #region 초기화/종료
        /// <summary>
        /// 계측기 초기화
        /// </summary>
        /// <param name="config">설정</param>
        /// <returns>성공 여부</returns>
        bool Init(LaserMeasureConfig config);

        /// <summary>
        /// 연결 종료
        /// </summary>
        void Close();
        #endregion

        #region 상태
        /// <summary>
        /// 연결 상태
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 연속 측정 지원 여부
        /// </summary>
        bool SupportsContinuous { get; }
        #endregion

        #region 단발 측정
        /// <summary>
        /// 비동기 측정
        /// </summary>
        /// <param name="channelId">채널 ID</param>
        /// <returns>측정 값 (mm)</returns>
        Task<double> MeasureAsync(int channelId = 0);

        /// <summary>
        /// 마지막 측정 값 조회
        /// </summary>
        /// <param name="channelId">채널 ID</param>
        /// <returns>마지막 측정 값 (mm)</returns>
        double GetLastValue(int channelId = 0);
        #endregion

        #region 연속 측정
        /// <summary>
        /// 연속 측정 시작
        /// </summary>
        /// <param name="channelId">채널 ID</param>
        /// <param name="intervalMs">측정 간격 (ms)</param>
        /// <exception cref="NotSupportedException">연속 측정 미지원 시</exception>
        void StartContinuous(int channelId = 0, int intervalMs = 100);

        /// <summary>
        /// 연속 측정 정지
        /// </summary>
        /// <param name="channelId">채널 ID</param>
        void StopContinuous(int channelId = 0);

        /// <summary>
        /// 연속 측정 실행 중 여부
        /// </summary>
        /// <param name="channelId">채널 ID</param>
        /// <returns>실행 중 여부</returns>
        bool IsContinuousRunning(int channelId = 0);
        #endregion

        #region 이벤트
        /// <summary>
        /// 측정 완료 이벤트
        /// </summary>
        event EventHandler<LaserMeasureEventArgs>? OnMeasured;
        #endregion
    }
}
