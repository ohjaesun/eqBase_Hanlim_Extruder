using EQ.Domain.Entities.SecsGem;
using EQ.Domain.Enums.SecsGem;

namespace EQ.Domain.Interface.SecsGem
{
    /// <summary>
    /// SECS/GEM 드라이버 인터페이스
    /// EZGemPlus 또는 다른 SECS/GEM 라이브러리를 추상화합니다.
    /// </summary>
    public interface ISecsGemDriver : IDisposable
    {
        #region 상태 속성
        /// <summary>
        /// 호스트 연결 상태
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Communicating 상태 (S1F13/F14 완료)
        /// </summary>
        bool IsCommunicating { get; }

        /// <summary>
        /// 현재 Control State
        /// </summary>
        ControlState ControlState { get; }

        /// <summary>
        /// 라이선스 확인 여부
        /// </summary>
        bool IsLicensed { get; }
        #endregion

        #region 이벤트
        /// <summary>
        /// EZGem 이벤트 발생
        /// </summary>
        event EventHandler<SecsGemEventArgs> GemEvent;

        /// <summary>
        /// 메시지 수신
        /// </summary>
        event EventHandler<SecsMessageEventArgs> MessageReceived;

        /// <summary>
        /// 메시지 송신
        /// </summary>
        event EventHandler<SecsMessageEventArgs> MessageSent;

        /// <summary>
        /// 연결 상태 변경
        /// </summary>
        event EventHandler<ConnectionChangedEventArgs> ConnectionChanged;

        /// <summary>
        /// Control State 변경
        /// </summary>
        event EventHandler<ControlStateChangedEventArgs> ControlStateChanged;

        /// <summary>
        /// Remote Command 수신
        /// </summary>
        event EventHandler<RemoteCommandEventArgs> RemoteCommandReceived;

        /// <summary>
        /// Terminal Message 수신
        /// </summary>
        event EventHandler<TerminalMessageEventArgs> TerminalMessageReceived;
        #endregion

        #region 라이프사이클
        /// <summary>
        /// 드라이버 시작
        /// </summary>
        /// <returns>0: 성공, 음수: 에러 코드</returns>
        int Start();

        /// <summary>
        /// 드라이버 정지
        /// </summary>
        /// <returns>0: 성공, 음수: 에러 코드</returns>
        int Stop();

        /// <summary>
        /// 런타임 상태 확인 (라이선스)
        /// </summary>
        /// <returns>0: 미인식, 1: 인식</returns>
        short GetRuntimeState();
        #endregion

        #region 설정
        /// <summary>
        /// SecsGemConfig를 사용하여 드라이버 초기화 설정을 적용합니다.
        /// Start() 호출 전에 반드시 호출해야 합니다.
        /// </summary>
        /// <param name="config">SECS/GEM 설정</param>
        void ApplyConfiguration(SecsGemConfig config);
        #endregion

        #region 로그 설정
        /// <summary>
        /// 로그 활성화
        /// </summary>
        void EnableLog();

        /// <summary>
        /// 로그 비활성화
        /// </summary>
        void DisableLog();

        /// <summary>
        /// 특정 메시지 로그 스킵
        /// </summary>
        /// <param name="stream">Stream 번호</param>
        /// <param name="function">Function 번호</param>
        void SkipLog(short stream, short function);
        #endregion

        #region SVID 관리
        /// <summary>
        /// SVID 등록
        /// </summary>
        /// <param name="svid">SVID</param>
        /// <param name="name">이름</param>
        /// <param name="format">데이터 포맷</param>
        /// <param name="unit">단위</param>
        /// <returns>결과 코드</returns>
        int AddSVID(int svid, string name, string format, string unit);

        /// <summary>
        /// SVID 값 설정
        /// </summary>
        /// <param name="svid">SVID</param>
        /// <param name="value">값</param>
        /// <returns>결과 코드</returns>
        int SetSVIDValue(int svid, string value);

        /// <summary>
        /// SVID 값 조회
        /// </summary>
        /// <param name="svid">SVID</param>
        /// <returns>값</returns>
        string GetSVIDValue(int svid);
        #endregion

        #region ECID 관리
        /// <summary>
        /// ECID 등록
        /// </summary>
        /// <param name="ecid">ECID</param>
        /// <param name="name">이름</param>
        /// <param name="unit">단위</param>
        /// <param name="format">데이터 포맷</param>
        /// <returns>결과 코드</returns>
        int AddECID(int ecid, string name, string unit, string format);

        /// <summary>
        /// ECID 값 설정
        /// </summary>
        /// <param name="ecid">ECID</param>
        /// <param name="value">값</param>
        /// <returns>결과 코드</returns>
        int SetECValue(int ecid, string value);

        /// <summary>
        /// ECID 값 조회
        /// </summary>
        /// <param name="ecid">ECID</param>
        /// <returns>값</returns>
        string GetECValue(int ecid);

        /// <summary>
        /// ECID 범위 설정
        /// </summary>
        /// <param name="ecid">ECID</param>
        /// <param name="minValue">최소값</param>
        /// <param name="maxValue">최대값</param>
        /// <returns>결과 코드</returns>
        int SetECRange(int ecid, string minValue, string maxValue);
        #endregion

        #region CEID 관리
        /// <summary>
        /// CEID 등록
        /// </summary>
        /// <param name="ceid">CEID</param>
        /// <param name="name">이름</param>
        /// <param name="comment">설명</param>
        /// <returns>결과 코드</returns>
        int AddCEID(int ceid, string name, string comment);

        /// <summary>
        /// 이벤트 보고 (S6F11)
        /// </summary>
        /// <param name="ceid">CEID</param>
        /// <returns>결과 코드</returns>
        int SendEventReport(int ceid);

        /// <summary>
        /// 이벤트 보고 (Transaction ID 포함)
        /// </summary>
        /// <param name="ceid">CEID</param>
        /// <param name="transactionId">Transaction ID</param>
        /// <returns>결과 코드</returns>
        int SendEventReportEx(int ceid, int transactionId);
        #endregion

        #region ALID 관리
        /// <summary>
        /// ALID 등록
        /// </summary>
        /// <param name="alid">ALID</param>
        /// <param name="alarmText">알람 텍스트</param>
        /// <param name="alarmCode">알람 코드</param>
        /// <returns>결과 코드</returns>
        int AddALID(int alid, string alarmText, string alarmCode);

        /// <summary>
        /// 알람 보고 (S5F1)
        /// </summary>
        /// <param name="alid">ALID</param>
        /// <param name="alarmCode">알람 코드 (0: Clear, 1: Set)</param>
        /// <returns>결과 코드</returns>
        int SendAlarmReport(int alid, short alarmCode);
        #endregion

        #region Control State
        /// <summary>
        /// Online Remote로 전환
        /// </summary>
        void GoOnlineRemote();

        /// <summary>
        /// Online Local로 전환
        /// </summary>
        void GoOnlineLocal();

        /// <summary>
        /// Offline으로 전환
        /// </summary>
        void GoOffline();
        #endregion

        #region Remote Command
        /// <summary>
        /// Remote Command 등록
        /// </summary>
        /// <param name="rcmd">명령 이름</param>
        /// <returns>결과 코드</returns>
        int AddRemoteCommand(string rcmd);

        /// <summary>
        /// Remote Command 조회
        /// </summary>
        /// <param name="msgId">메시지 ID</param>
        /// <param name="rcmd">명령 이름 (출력)</param>
        /// <returns>파라미터 개수</returns>
        short GetRemoteCommand(int msgId, ref string rcmd);

        /// <summary>
        /// Remote Command 파라미터 조회
        /// </summary>
        /// <param name="msgId">메시지 ID</param>
        /// <param name="index">파라미터 인덱스</param>
        /// <param name="cpName">파라미터 이름 (출력)</param>
        /// <param name="cpValue">파라미터 값 (출력)</param>
        void GetRemoteCommandParam(int msgId, int index, ref string cpName, ref string cpValue);

        /// <summary>
        /// Remote Command 응답 (S2F42)
        /// </summary>
        /// <param name="msgId">메시지 ID</param>
        /// <param name="rcmd">명령 이름</param>
        /// <param name="hcAck">응답 코드</param>
        /// <returns>결과 코드</returns>
        short ReplyRemoteCommand(int msgId, string rcmd, short hcAck);
        #endregion

        #region Spool
        /// <summary>
        /// Spooling 활성화
        /// </summary>
        void EnableSpooling();

        /// <summary>
        /// Spooling 비활성화
        /// </summary>
        void DisableSpooling();

        /// <summary>
        /// 최대 Spool 개수 설정
        /// </summary>
        /// <param name="maxCount">최대 개수</param>
        void SetMaxSpoolCount(int maxCount);

        /// <summary>
        /// Spool 전송 개수 설정
        /// </summary>
        /// <param name="maxTransmit">전송 개수</param>
        void SetMaxSpoolTransmit(short maxTransmit);

        /// <summary>
        /// Spool 덮어쓰기 활성화
        /// </summary>
        void EnableSpoolOverwrite();

        /// <summary>
        /// Spool 덮어쓰기 비활성화
        /// </summary>
        void DisableSpoolOverwrite();
        #endregion

        #region Terminal Message
        /// <summary>
        /// Terminal Message 조회
        /// </summary>
        /// <param name="msgId">메시지 ID</param>
        /// <param name="tid">Terminal ID (출력)</param>
        /// <param name="message">메시지 내용 (출력)</param>
        /// <returns>남은 메시지 개수 (-1: 없음)</returns>
        int GetTerminalMsg(int msgId, ref int tid, ref string message);
        #endregion

        #region 사용자 정의 메시지
        /// <summary>
        /// 자동 응답 비활성화 (사용자 처리)
        /// </summary>
        /// <param name="stream">Stream 번호</param>
        /// <param name="function">Function 번호</param>
        /// <returns>결과 코드</returns>
        int DisableAutoReply(short stream, short function);

        /// <summary>
        /// 메시지 생성
        /// </summary>
        /// <param name="stream">Stream 번호</param>
        /// <param name="function">Function 번호</param>
        /// <param name="wbit">W-bit (1: Reply 필요)</param>
        /// <returns>메시지 ID</returns>
        int CreateMsg(short stream, short function, short wbit);

        /// <summary>
        /// 메시지 전송
        /// </summary>
        /// <param name="msgId">메시지 ID</param>
        /// <returns>결과 코드</returns>
        int SendMsg(int msgId);

        /// <summary>
        /// 메시지 정보 조회
        /// </summary>
        /// <param name="msgId">메시지 ID</param>
        /// <param name="stream">Stream 번호 (출력)</param>
        /// <param name="function">Function 번호 (출력)</param>
        /// <param name="wbit">W-bit (출력)</param>
        /// <param name="length">데이터 길이 (출력)</param>
        void GetMsgInfo(int msgId, ref short stream, ref short function, ref short wbit, ref int length);

        /// <summary>
        /// 메시지 Abort
        /// </summary>
        /// <param name="msgId">메시지 ID</param>
        void AbortMsg(int msgId);

        /// <summary>
        /// 메시지 종료 (데이터 클리어)
        /// </summary>
        /// <param name="msgId">메시지 ID</param>
        void CloseMsg(int msgId);
        #endregion

        #region 메시지 아이템 구성
        /// <summary>
        /// List 아이템 열기
        /// </summary>
        /// <param name="msgId">메시지 ID</param>
        void OpenListItem(int msgId);

        /// <summary>
        /// List 아이템 닫기
        /// </summary>
        /// <param name="msgId">메시지 ID</param>
        void CloseListItem(int msgId);

        /// <summary>
        /// ASCII 아이템 추가
        /// </summary>
        /// <param name="msgId">메시지 ID</param>
        /// <param name="value">값</param>
        /// <param name="length">길이</param>
        void AddAsciiItem(int msgId, string value, int length);

        /// <summary>
        /// U1 아이템 추가
        /// </summary>
        /// <param name="msgId">메시지 ID</param>
        /// <param name="value">값</param>
        /// <param name="count">개수</param>
        void AddU1Item(int msgId, byte value, int count);

        /// <summary>
        /// U2 아이템 추가
        /// </summary>
        /// <param name="msgId">메시지 ID</param>
        /// <param name="value">값</param>
        /// <param name="count">개수</param>
        void AddU2Item(int msgId, ushort value, int count);

        /// <summary>
        /// U4 아이템 추가
        /// </summary>
        /// <param name="msgId">메시지 ID</param>
        /// <param name="value">값</param>
        /// <param name="count">개수</param>
        void AddU4Item(int msgId, uint value, int count);

        /// <summary>
        /// I4 아이템 추가
        /// </summary>
        /// <param name="msgId">메시지 ID</param>
        /// <param name="value">값</param>
        /// <param name="count">개수</param>
        void AddI4Item(int msgId, int value, int count);

        /// <summary>
        /// F4 아이템 추가
        /// </summary>
        /// <param name="msgId">메시지 ID</param>
        /// <param name="value">값</param>
        /// <param name="count">개수</param>
        void AddF4Item(int msgId, float value, int count);
        #endregion

        #region 포맷 코드 설정
        /// <summary>
        /// 포맷 코드 설정
        /// </summary>
        /// <param name="itemType">아이템 타입 (SVID, ECID, ALID, CEID, TRID, RPTID, DATAID)</param>
        /// <param name="formatCode">포맷 코드 (U1, U2, U4, I1, I2, I4, B, BOOL)</param>
        void SetFormatCode(string itemType, string formatCode);
        #endregion
    }
}
