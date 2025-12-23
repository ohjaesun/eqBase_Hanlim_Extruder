namespace EQ.Domain.Enums
{
    /// <summary>
    /// Audit Trail 이벤트 유형
    /// (사양서 9.9.3.7 - 이벤트 유형 분류)
    /// </summary>
    public enum AuditEventType
    {
        // 사용자 인증 (8.12, 9.9.3.7)
        LoginSuccess,           // 로그인 성공
        LoginFailed,            // 로그인 실패
        Logout,                 // 로그아웃
        
        // 사용자 관리 (9.9.4)
        UserCreated,            // 사용자 생성
        UserDeleted,            // 사용자 삭제
        UserLocked,             // 사용자 잠금
        UserUnlocked,           // 사용자 잠금 해제
        PasswordChanged,        // 비밀번호 변경
        PasswordReset,          // 비밀번호 재설정 (관리자)
        
        // Recipe 관리 (8.12, 9.9.3.7)
        RecipeCreated,          // Recipe 생성
        RecipeModified,         // Recipe 수정
        RecipeDeleted,          // Recipe 삭제
        RecipeLoaded,           // Recipe 로드
        
        // 공정 제어 (9.9.3.7)
        ParameterChanged,       // 파라미터 변경 (온도, 속도 등)
        SequenceStarted,        // 시퀀스 시작 (8.12 - Run)
        SequenceStopped,        // 시퀀스 정지 (8.12 - Stop)
        
        // 장비 상태 (9.5.4)
        EmergencyStop,          // 비상정지
        AlarmOccurred,          // 알람 발생
        AlarmCleared,           // 알람 해제
        
        // 시스템
        SystemStartup,          // 시스템 시작
        SystemShutdown,         // 시스템 종료
        SystemCrash,           // 시스템 충돌
        ConfigurationChanged,   // 설정 변경
        
        // 데이터 관리
        DataExported,           // 데이터 내보내기
        DataImported,           // 데이터 가져오기
    }
}
