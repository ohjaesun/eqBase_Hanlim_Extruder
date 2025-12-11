namespace EQ.Domain.Entities.SecsGem
{
    /// <summary>
    /// Alarm Definition (ALID) 정의
    /// SEMI E5 표준의 알람을 정의합니다.
    /// S5F1/S5F2로 호스트에 보고되는 장비 알람입니다.
    /// </summary>
    public class AlarmDefinition
    {
        /// <summary>
        /// Alarm ID
        /// </summary>
        public int ALID { get; set; }

        /// <summary>
        /// 알람 텍스트 (ALTX)
        /// </summary>
        public string AlarmText { get; set; } = string.Empty;

        /// <summary>
        /// 알람 코드 (ALCD) - 심각도 등급
        /// </summary>
        public string AlarmCode { get; set; } = string.Empty;

        /// <summary>
        /// 알람 활성화 여부 (호스트에서 S5F3으로 설정)
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 현재 알람 발생 상태
        /// </summary>
        public bool IsSet { get; set; } = false;

        /// <summary>
        /// 알람 설명
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 기본 생성자
        /// </summary>
        public AlarmDefinition()
        {
        }

        /// <summary>
        /// 파라미터 생성자
        /// </summary>
        /// <param name="alid">Alarm ID</param>
        /// <param name="alarmText">알람 텍스트</param>
        /// <param name="alarmCode">알람 코드</param>
        public AlarmDefinition(int alid, string alarmText, string alarmCode = "")
        {
            ALID = alid;
            AlarmText = alarmText;
            AlarmCode = alarmCode;
        }

        /// <summary>
        /// 알람 발생
        /// </summary>
        public void SetAlarm()
        {
            IsSet = true;
        }

        /// <summary>
        /// 알람 해제
        /// </summary>
        public void ClearAlarm()
        {
            IsSet = false;
        }

        /// <summary>
        /// 문자열 표현
        /// </summary>
        public override string ToString()
        {
            string status = IsSet ? "SET" : "CLEAR";
            return string.Format("ALID[{0}] {1} ({2})", ALID, AlarmText, status);
        }
    }
}
