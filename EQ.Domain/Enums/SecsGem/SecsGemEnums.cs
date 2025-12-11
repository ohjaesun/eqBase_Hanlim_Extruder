namespace EQ.Domain.Enums.SecsGem
{
    /// <summary>
    /// GEM Control State (제어 상태)
    /// SEMI E30 표준에 정의된 장비 제어 상태
    /// </summary>
    public enum ControlState
    {
        /// <summary>
        /// 오프라인 상태 - 호스트와 통신 불가
        /// </summary>
        Offline = 1,

        /// <summary>
        /// 온라인 로컬 - 호스트 연결되었으나 로컬 제어
        /// </summary>
        OnlineLocal = 2,

        /// <summary>
        /// 온라인 리모트 - 호스트가 장비 제어
        /// </summary>
        OnlineRemote = 3
    }

    /// <summary>
    /// GEM Communication State (통신 상태)
    /// </summary>
    public enum CommunicationState
    {
        /// <summary>
        /// 통신 비활성화
        /// </summary>
        Disabled = 0,

        /// <summary>
        /// Establish Communication 대기 중
        /// </summary>
        WaitCommunicatingRequest = 1,

        /// <summary>
        /// 통신 중 (S1F13/F14 완료)
        /// </summary>
        Communicating = 2
    }

    /// <summary>
    /// EZGemPlus Event ID
    /// 엔비아소프트 EZGemPlus 라이브러리 이벤트 코드
    /// </summary>
    public enum EZGemEventId
    {
        /// <summary>
        /// 호스트 연결됨
        /// </summary>
        Connect = 1,

        /// <summary>
        /// 호스트 연결 해제
        /// </summary>
        Disconnect = 2,

        /// <summary>
        /// 평가판 모드 (라이선스 없음)
        /// </summary>
        EvaluationMode = 10,

        /// <summary>
        /// 정식 런타임 모드 (라이선스 있음)
        /// </summary>
        RuntimeMode = 11,

        /// <summary>
        /// Linktest 요청 수신
        /// </summary>
        LinktestRequestIn = 101,

        /// <summary>
        /// Linktest 요청 송신
        /// </summary>
        LinktestRequestOut = 102,

        /// <summary>
        /// Linktest 응답 수신
        /// </summary>
        LinktestResponseIn = 103,

        /// <summary>
        /// Linktest 응답 송신
        /// </summary>
        LinktestResponseOut = 104,

        /// <summary>
        /// Transaction Timeout (S9F9)
        /// </summary>
        TransactionTimeout = 202,

        /// <summary>
        /// Unrecognized Device ID (S9F1)
        /// </summary>
        UnrecognizedDeviceId = 203,

        /// <summary>
        /// Unrecognized Stream (S9F3)
        /// </summary>
        UnrecognizedStream = 204,

        /// <summary>
        /// Unrecognized Function (S9F5)
        /// </summary>
        UnrecognizedFunction = 205,

        /// <summary>
        /// Invalid Data (S9F7)
        /// </summary>
        InvalidData = 206,

        /// <summary>
        /// Discard Message (S9F9)
        /// </summary>
        DiscardMsg = 207,

        /// <summary>
        /// Abort Message 수신
        /// </summary>
        AbortMsgIn = 208,

        /// <summary>
        /// T1 Timeout
        /// </summary>
        TimeoutT1 = 301,

        /// <summary>
        /// T2 Timeout
        /// </summary>
        TimeoutT2 = 302,

        /// <summary>
        /// T3 Timeout
        /// </summary>
        TimeoutT3 = 303,

        /// <summary>
        /// T4 Timeout
        /// </summary>
        TimeoutT4 = 304,

        /// <summary>
        /// T5 Timeout
        /// </summary>
        TimeoutT5 = 305,

        /// <summary>
        /// T6 Timeout
        /// </summary>
        TimeoutT6 = 306,

        /// <summary>
        /// T7 Timeout
        /// </summary>
        TimeoutT7 = 307,

        /// <summary>
        /// T8 Timeout
        /// </summary>
        TimeoutT8 = 308,

        /// <summary>
        /// Retry Limit 도달
        /// </summary>
        RetryLimit = 309,

        /// <summary>
        /// 메시지 수신 (lParam = Stream * 1000 + Function)
        /// </summary>
        MsgIn = 401,

        /// <summary>
        /// 메시지 송신 (lParam = Stream * 1000 + Function)
        /// </summary>
        MsgOut = 402,

        /// <summary>
        /// S2F37 응답 완료
        /// </summary>
        S2F37Response = 501,

        /// <summary>
        /// Control State → Offline
        /// </summary>
        Offline = 1001,

        /// <summary>
        /// Control State → Online Local
        /// </summary>
        OnlineLocal = 1002,

        /// <summary>
        /// Control State → Online Remote
        /// </summary>
        OnlineRemote = 1003,

        /// <summary>
        /// Communication State → Communicating
        /// </summary>
        Communicating = 1010,

        /// <summary>
        /// Host ECID 변경 요청 (S2F15)
        /// </summary>
        HostECID = 1015,

        /// <summary>
        /// Trace 설정됨
        /// </summary>
        TraceTimeSet = 1020,

        /// <summary>
        /// Remote Command 수신 (S2F41)
        /// </summary>
        RemoteCommand = 1030,

        /// <summary>
        /// Spool 업데이트됨
        /// </summary>
        SpoolUpdated = 1040,

        /// <summary>
        /// Spool 가득 참
        /// </summary>
        SpoolFull = 1041,

        /// <summary>
        /// Terminal Message 수신 (S10F3/S10F5)
        /// </summary>
        TerminalMessage = 1050
    }

    /// <summary>
    /// SECS-II 데이터 포맷 코드
    /// </summary>
    public enum SecsFormat
    {
        /// <summary>
        /// List
        /// </summary>
        List,

        /// <summary>
        /// Binary
        /// </summary>
        Binary,

        /// <summary>
        /// Boolean
        /// </summary>
        Boolean,

        /// <summary>
        /// ASCII String
        /// </summary>
        Ascii,

        /// <summary>
        /// Signed 1-byte Integer
        /// </summary>
        Int1,

        /// <summary>
        /// Signed 2-byte Integer
        /// </summary>
        Int2,

        /// <summary>
        /// Signed 4-byte Integer
        /// </summary>
        Int4,

        /// <summary>
        /// Signed 8-byte Integer
        /// </summary>
        Int8,

        /// <summary>
        /// Unsigned 1-byte Integer
        /// </summary>
        UInt1,

        /// <summary>
        /// Unsigned 2-byte Integer
        /// </summary>
        UInt2,

        /// <summary>
        /// Unsigned 4-byte Integer
        /// </summary>
        UInt4,

        /// <summary>
        /// Unsigned 8-byte Integer
        /// </summary>
        UInt8,

        /// <summary>
        /// 4-byte Floating Point
        /// </summary>
        Float4,

        /// <summary>
        /// 8-byte Floating Point
        /// </summary>
        Float8
    }

    /// <summary>
    /// Remote Command 응답 코드 (HCACK)
    /// </summary>
    public enum HcAck
    {
        /// <summary>
        /// 명령 수행 완료
        /// </summary>
        Acknowledge = 0,

        /// <summary>
        /// 명령이 존재하지 않음
        /// </summary>
        CommandNotExist = 1,

        /// <summary>
        /// 현재 수행할 수 없음
        /// </summary>
        CannotPerformNow = 2,

        /// <summary>
        /// 파라미터가 유효하지 않음
        /// </summary>
        InvalidParameter = 3,

        /// <summary>
        /// 명령 수락, 완료 이벤트로 알림 예정
        /// </summary>
        AcknowledgeWithEvent = 4,

        /// <summary>
        /// 이미 원하는 상태임
        /// </summary>
        AlreadyInDesiredCondition = 5
    }

    /// <summary>
    /// Alarm Code (ALCD)
    /// </summary>
    public enum AlarmCode
    {
        /// <summary>
        /// 알람 클리어됨
        /// </summary>
        Cleared = 0,

        /// <summary>
        /// 알람 발생
        /// </summary>
        Set = 1
    }
}
