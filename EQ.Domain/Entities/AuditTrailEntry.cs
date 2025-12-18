using EQ.Domain.Enums;
using System;

namespace EQ.Domain.Entities
{
    /// <summary>
    /// Audit Trail 이력 기록 엔티티
    /// (사양서 9.9.3 - Audit Trail 요구사항 구현)
    /// </summary>
    public class AuditTrailEntry
    {
        /// <summary>
        /// 자동 증가 Primary Key
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 이벤트 발생 시간 (KST)
        /// (사양서 9.9.3.7 - 일시 기록)
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 이벤트 유형
        /// (사양서 9.9.3.7 - 유형 분류)
        /// </summary>
        public AuditEventType EventType { get; set; }

        /// <summary>
        /// 사용자 ID
        /// (사양서 9.9.3.7 - 사용자 정보)
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// 사용자 이름 (표시용)
        /// (사양서 9.9.3.7 - 사용자 정보)
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 이벤트 설명 (사람이 읽을 수 있는 형식)
        /// (사양서 9.9.3.7 - 설명)
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 추가 세부정보 (JSON 직렬화)
        /// - 파라미터 변경: 변경 전/후 값
        /// - Recipe 변경: Recipe 이름, 버전
        /// - 알람: 알람 코드, 메시지
        /// </summary>
        public string DetailJson { get; set; } = string.Empty;

        /// <summary>
        /// 기본 생성자
        /// </summary>
        public AuditTrailEntry()
        {
            Timestamp = DateTime.Now;
        }

        /// <summary>
        /// 새 이력 기록 생성용 생성자
        /// </summary>
        public AuditTrailEntry(
            AuditEventType eventType,
            string userId,
            string userName,
            string description,
            string detailJson = "")
        {
            Timestamp = DateTime.Now;
            EventType = eventType;
            UserId = userId;
            UserName = userName;
            Description = description;
            DetailJson = detailJson;
        }
    }
}
