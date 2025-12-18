using EQ.Domain.Enums;
using System;

namespace EQ.Domain.Entities
{
    /// <summary>
    /// 다중 사용자 계정 관리를 위한 엔티티
    /// (사양서 9.9.4.1~9.9.4.4 구현)
    /// </summary>
    public class UserAccount
    {
        /// <summary>
        /// 사용자 ID (고유 키, Primary Key)
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// 사용자 이름 (표시용)
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 권한 레벨 (Operator/Engineer/Admin)
        /// </summary>
        public UserLevel Level { get; set; }

        /// <summary>
        /// SHA-256 해시 비밀번호
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// 계정 잠금 여부 (9.9.4.2 - 로그인 실패 시 자동 잠금)
        /// </summary>
        public bool IsLocked { get; set; }

        /// <summary>
        /// 연속 로그인 실패 횟수 (9.9.4.2)
        /// </summary>
        public int FailedAttempts { get; set; }

        /// <summary>
        /// 마지막 로그인 시간
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 계정 생성 시간
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 기본 생성자 (SQLite 역직렬화용)
        /// </summary>
        public UserAccount()
        {
            CreatedTime = DateTime.Now;
        }

        /// <summary>
        /// 새 계정 생성용 생성자
        /// </summary>
        public UserAccount(string userId, string userName, UserLevel level, string passwordHash)
        {
            UserId = userId;
            UserName = userName;
            Level = level;
            PasswordHash = passwordHash;
            IsLocked = false;
            FailedAttempts = 0;
            LastLoginTime = null;
            CreatedTime = DateTime.Now;
        }
    }
}
