using EQ.Domain.Enums.LaserMeasure;

namespace EQ.Domain.Entities.LaserMeasure
{
    /// <summary>
    /// 레이저 계측기 설정
    /// </summary>
    public class LaserMeasureConfig
    {
        /// <summary>
        /// 계측기 ID
        /// </summary>
        public LaserMeasureId Id { get; set; } = LaserMeasureId.Laser1;

        /// <summary>
        /// 계측기 타입
        /// </summary>
        public LaserMeasureType Type { get; set; } = LaserMeasureType.HL_G1;

        /// <summary>
        /// 계측기 이름
        /// </summary>
        public string Name { get; set; } = string.Empty;

        #region TCP 설정 (ZW7000)
        /// <summary>
        /// IP 주소
        /// </summary>
        public string IpAddress { get; set; } = "192.168.0.1";

        /// <summary>
        /// 포트
        /// </summary>
        public int Port { get; set; } = 5000;
        #endregion

        #region Serial 설정 (HL_G1)
        /// <summary>
        /// 시리얼 포트명
        /// </summary>
        public string PortName { get; set; } = "COM1";

        /// <summary>
        /// 통신 속도
        /// </summary>
        public int BaudRate { get; set; } = 9600;

        /// <summary>
        /// Slave ID (RS485)
        /// </summary>
        public byte SlaveId { get; set; } = 1;
        #endregion

        /// <summary>
        /// 통신 타임아웃 (ms)
        /// </summary>
        public int Timeout { get; set; } = 3000;

        /// <summary>
        /// 채널 수
        /// </summary>
        public int ChannelCount { get; set; } = 1;
    }
}
