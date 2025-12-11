// EQ.Domain/Enums/UserLevel.cs
namespace EQ.Domain.Enums
{
    /// <summary>
    /// 사용자 권한 등급
    /// (숫자가 높을수록 높은 권한)
    /// </summary>
    public enum UserLevel
    {
        /// <summary>
        /// 공정 운용자 (기본 권한)
        /// </summary>
        Operator = 1,

        /// <summary>
        /// 장비 엔지니어 (설정 수정 가능)
        /// </summary>
        Engineer = 5,

        /// <summary>
        /// 장비 관리자 (모든 권한)
        /// </summary>
        Admin = 10
    }
}