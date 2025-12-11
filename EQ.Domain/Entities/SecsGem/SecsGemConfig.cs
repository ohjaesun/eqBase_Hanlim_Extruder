using EQ.Domain.Enums.SecsGem;

namespace EQ.Domain.Entities.SecsGem
{
    /// <summary>
    /// SECS/GEM 연결 설정
    /// EZGemPlus 드라이버 초기화에 필요한 설정값을 정의합니다.
    /// </summary>
    public class SecsGemConfig
    {
        #region 장비 정보
        /// <summary>
        /// 장비 모델명 (MDLN)
        /// S1F13 메시지에 사용됨
        /// </summary>
        public string ModelName { get; set; } = "EqBase";

        /// <summary>
        /// 소프트웨어 버전 (SOFTREV)
        /// S1F13 메시지에 사용됨
        /// </summary>
        public string SoftwareRevision { get; set; } = "1.0.0";

        /// <summary>
        /// 장비 ID (Device ID)
        /// </summary>
        public int DeviceId { get; set; } = 0;
        #endregion

        #region 연결 설정
        /// <summary>
        /// 호스트 IP 주소
        /// </summary>
        public string HostIp { get; set; } = "127.0.0.1";

        /// <summary>
        /// 호스트 포트
        /// </summary>
        public int HostPort { get; set; } = 5000;

        /// <summary>
        /// 패시브 모드 여부 (true: 장비가 서버, false: 장비가 클라이언트)
        /// </summary>
        public bool IsPassive { get; set; } = true;
        #endregion

        #region 타이머 설정 (초)
        /// <summary>
        /// T3 타이머 (Reply Timeout) - 기본 45초
        /// </summary>
        public int T3Timeout { get; set; } = 45;

        /// <summary>
        /// T5 타이머 (Connection Separation Timeout) - 기본 10초
        /// </summary>
        public int T5Timeout { get; set; } = 10;

        /// <summary>
        /// T6 타이머 (Control Transaction Timeout) - 기본 5초
        /// </summary>
        public int T6Timeout { get; set; } = 5;

        /// <summary>
        /// T7 타이머 (Not Selected Timeout) - 기본 10초
        /// </summary>
        public int T7Timeout { get; set; } = 10;

        /// <summary>
        /// T8 타이머 (Network Intercharacter Timeout) - 기본 5초
        /// </summary>
        public int T8Timeout { get; set; } = 5;

        /// <summary>
        /// CommRequest 타이머 - Connection 후 S1F13 대기 시간 (초)
        /// </summary>
        public int CommRequestTimeout { get; set; } = 10;
        #endregion

        #region 로그 설정
        /// <summary>
        /// 로그 사용 여부
        /// </summary>
        public bool EnableLog { get; set; } = true;

        /// <summary>
        /// 로그 파일 경로
        /// </summary>
        public string LogFilePath { get; set; } = "LOG\\SECSGEM.log";

        /// <summary>
        /// 로그 보관 기간 (일)
        /// </summary>
        public int LogRetentionDays { get; set; } = 30;
        #endregion

        #region Spool 설정
        /// <summary>
        /// Spooling 사용 여부
        /// </summary>
        public bool EnableSpooling { get; set; } = true;

        /// <summary>
        /// 최대 Spool 개수
        /// </summary>
        public int MaxSpoolCount { get; set; } = 1000;

        /// <summary>
        /// 한번에 전송할 Spool 개수 (0: 전체)
        /// </summary>
        public int MaxSpoolTransmit { get; set; } = 0;

        /// <summary>
        /// Spool 덮어쓰기 허용 여부
        /// </summary>
        public bool EnableSpoolOverwrite { get; set; } = false;
        #endregion

        #region 초기 상태
        /// <summary>
        /// 시작 시 Control State
        /// </summary>
        public ControlState InitialControlState { get; set; } = ControlState.Offline;

        /// <summary>
        /// 자동 Online 전환 여부
        /// </summary>
        public bool AutoGoOnline { get; set; } = false;
        #endregion
    }
}
