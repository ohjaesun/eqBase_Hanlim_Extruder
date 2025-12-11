using EQ.Domain.Enums;
using System;

namespace EQ.Domain.Entities
{
    public class AlarmSolutionData
    {
        public ErrorList ErrorCode { get; set; }
        public string Cause { get; set; }
        public string Solution { get; set; }
    }
    public class AlarmSolutionStorage
    {
        public List<AlarmSolutionData> Items { get; set; } = new List<AlarmSolutionData>();
    }

    /// <summary>
    /// 알람 이력 저장을 위한 데이터 엔티티
    /// </summary>
    public class AlarmData
    {
        // 참고: SqliteStorage가 Timestamp를 자동으로 관리하므로 
        // 기존의 Date, Time은 Id, CallName, Info만 사용합니다.

        /// <summary>
        /// 알람 ID 또는 타이틀
        /// </summary>
        public string IDs { get; set; }
        /// <summary>
        /// 알람을 호출한 위치 (현재 미사용)
        /// </summary>
        public string CallName { get; set; }
        public string CallPath { get; set; }
        /// <summary>
        /// 알람 상세 메시지
        /// </summary>
        public string Info { get; set; }

        public AlarmData() { }

        public AlarmData(string id,  string info, string callName, string CallPath)
        {
            this.IDs = id;
            this.CallName = callName;
            this.Info = info;
            this.CallPath = CallPath;
        }
    }
}