// EQ.Domain/Enums/EqState.cs
namespace EQ.Domain.Enums
{
    /// <summary>
    /// 장비의 주요 상태 (FSM)
    /// </summary>
    public enum EqState
    {
        /// <summary>
        /// 초기화/설정 중 (부팅 직후)
        /// </summary>
        Init,
        /// <summary>
        /// 대기 중 (공정 시작 가능)
        /// </summary>
        Idle,
        /// <summary>
        /// 공정/시퀀스 실행 중
        /// </summary>
        Running,
        /// <summary>
        /// 알람/오류 발생 (리셋 필요)
        /// </summary>
        Error
    }
}