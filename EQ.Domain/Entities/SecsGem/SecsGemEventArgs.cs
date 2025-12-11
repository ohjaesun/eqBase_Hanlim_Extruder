using EQ.Domain.Enums.SecsGem;

namespace EQ.Domain.Entities.SecsGem
{
    /// <summary>
    /// SECS/GEM 이벤트 인수 기본 클래스
    /// </summary>
    public class SecsGemEventArgs : EventArgs
    {
        /// <summary>
        /// 이벤트 ID
        /// </summary>
        public EZGemEventId EventId { get; }

        /// <summary>
        /// 추가 파라미터
        /// </summary>
        public int Parameter { get; }

        /// <summary>
        /// 이벤트 발생 시간
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="eventId">이벤트 ID</param>
        /// <param name="parameter">추가 파라미터</param>
        public SecsGemEventArgs(EZGemEventId eventId, int parameter = 0)
        {
            EventId = eventId;
            Parameter = parameter;
            Timestamp = DateTime.Now;
        }
    }

    /// <summary>
    /// SECS 메시지 이벤트 인수
    /// </summary>
    public class SecsMessageEventArgs : EventArgs
    {
        /// <summary>
        /// Stream 번호
        /// </summary>
        public int Stream { get; }

        /// <summary>
        /// Function 번호
        /// </summary>
        public int Function { get; }

        /// <summary>
        /// 수신 메시지 여부 (false면 송신)
        /// </summary>
        public bool IsIncoming { get; }

        /// <summary>
        /// 메시지 ID (사용자 정의 메시지 처리용)
        /// </summary>
        public int MessageId { get; }

        /// <summary>
        /// 이벤트 발생 시간
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="stream">Stream 번호</param>
        /// <param name="function">Function 번호</param>
        /// <param name="isIncoming">수신 여부</param>
        /// <param name="messageId">메시지 ID</param>
        public SecsMessageEventArgs(int stream, int function, bool isIncoming, int messageId = 0)
        {
            Stream = stream;
            Function = function;
            IsIncoming = isIncoming;
            MessageId = messageId;
            Timestamp = DateTime.Now;
        }

        /// <summary>
        /// SxFy 형식 문자열
        /// </summary>
        public string SxFy => string.Format("S{0}F{1}", Stream, Function);

        /// <summary>
        /// 방향 표시 문자열
        /// </summary>
        public string Direction => IsIncoming ? "RX" : "TX";

        /// <summary>
        /// 문자열 표현
        /// </summary>
        public override string ToString()
        {
            return string.Format("[{0}] {1} {2}", Timestamp.ToString("HH:mm:ss.fff"), Direction, SxFy);
        }
    }

    /// <summary>
    /// 연결 상태 변경 이벤트 인수
    /// </summary>
    public class ConnectionChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 연결 여부
        /// </summary>
        public bool IsConnected { get; }

        /// <summary>
        /// 메시지
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 이벤트 발생 시간
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="isConnected">연결 여부</param>
        /// <param name="message">메시지</param>
        public ConnectionChangedEventArgs(bool isConnected, string message = "")
        {
            IsConnected = isConnected;
            Message = message;
            Timestamp = DateTime.Now;
        }
    }

    /// <summary>
    /// Control State 변경 이벤트 인수
    /// </summary>
    public class ControlStateChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 이전 상태
        /// </summary>
        public ControlState PreviousState { get; }

        /// <summary>
        /// 현재 상태
        /// </summary>
        public ControlState CurrentState { get; }

        /// <summary>
        /// 이벤트 발생 시간
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="previousState">이전 상태</param>
        /// <param name="currentState">현재 상태</param>
        public ControlStateChangedEventArgs(ControlState previousState, ControlState currentState)
        {
            PreviousState = previousState;
            CurrentState = currentState;
            Timestamp = DateTime.Now;
        }
    }

    /// <summary>
    /// Remote Command 수신 이벤트 인수
    /// </summary>
    public class RemoteCommandEventArgs : EventArgs
    {
        /// <summary>
        /// 명령 데이터
        /// </summary>
        public RemoteCommandData CommandData { get; }

        /// <summary>
        /// 이벤트 발생 시간
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="commandData">명령 데이터</param>
        public RemoteCommandEventArgs(RemoteCommandData commandData)
        {
            CommandData = commandData;
            Timestamp = DateTime.Now;
        }
    }

    /// <summary>
    /// Terminal Message 수신 이벤트 인수
    /// </summary>
    public class TerminalMessageEventArgs : EventArgs
    {
        /// <summary>
        /// Terminal ID
        /// </summary>
        public int TerminalId { get; }

        /// <summary>
        /// 메시지 내용
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// 이벤트 발생 시간
        /// </summary>
        public DateTime Timestamp { get; }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="terminalId">Terminal ID</param>
        /// <param name="message">메시지 내용</param>
        public TerminalMessageEventArgs(int terminalId, string message)
        {
            TerminalId = terminalId;
            Message = message;
            Timestamp = DateTime.Now;
        }
    }
}
