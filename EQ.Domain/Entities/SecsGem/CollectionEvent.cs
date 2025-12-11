namespace EQ.Domain.Entities.SecsGem
{
    /// <summary>
    /// Collection Event (CEID) 정의
    /// SEMI E5/E30 표준의 수집 이벤트를 정의합니다.
    /// S6F11/S6F12로 호스트에 보고되는 장비 이벤트입니다.
    /// </summary>
    public class CollectionEvent
    {
        /// <summary>
        /// Collection Event ID
        /// </summary>
        public int CEID { get; set; }

        /// <summary>
        /// 이벤트 이름
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 이벤트 설명
        /// </summary>
        public string Comment { get; set; } = string.Empty;

        /// <summary>
        /// 이벤트 활성화 여부 (호스트에서 S2F37로 설정)
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 연결된 Report ID 목록
        /// </summary>
        public List<int> LinkedReportIds { get; set; } = new List<int>();

        /// <summary>
        /// 기본 생성자
        /// </summary>
        public CollectionEvent()
        {
        }

        /// <summary>
        /// 파라미터 생성자
        /// </summary>
        /// <param name="ceid">Collection Event ID</param>
        /// <param name="name">이벤트 이름</param>
        /// <param name="comment">이벤트 설명</param>
        public CollectionEvent(int ceid, string name, string comment = "")
        {
            CEID = ceid;
            Name = name;
            Comment = comment;
        }

        /// <summary>
        /// Report 연결
        /// </summary>
        /// <param name="rptId">Report ID</param>
        public void LinkReport(int rptId)
        {
            if (!LinkedReportIds.Contains(rptId))
            {
                LinkedReportIds.Add(rptId);
            }
        }

        /// <summary>
        /// Report 연결 해제
        /// </summary>
        /// <param name="rptId">Report ID</param>
        public void UnlinkReport(int rptId)
        {
            LinkedReportIds.Remove(rptId);
        }

        /// <summary>
        /// 문자열 표현
        /// </summary>
        public override string ToString()
        {
            string status = IsEnabled ? "Enabled" : "Disabled";
            return string.Format("CEID[{0}] {1} ({2})", CEID, Name, status);
        }
    }
}
