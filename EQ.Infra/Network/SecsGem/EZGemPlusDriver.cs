using EQ.Common.Logs;
using EQ.Domain.Entities.SecsGem;
using EQ.Domain.Enums.SecsGem;
using EQ.Domain.Interface.SecsGem;
using EZGemPlusCS;
using static System.Net.Mime.MediaTypeNames;

namespace EQ.Infra.SecsGem
{
    /// <summary>
    /// EZGemPlus DLL Wrapper
    /// 엔비아소프트 EZGemPlus 라이브러리를 래핑합니다.
    /// ISecsGemDriver 인터페이스를 구현하여 Core 레이어와 분리합니다.
    /// </summary>
    public class EZGemPlusDriver : ISecsGemDriver
    {
        #region Fields
        private readonly CEZGemPlusLib _gem;
        private bool _isConnected;
        private bool _isCommunicating;
        private ControlState _controlState = ControlState.Offline;
        private bool _disposed = false;
        private bool _initialized = false;
        #endregion

        #region Properties
        /// <inheritdoc/>
        public bool IsConnected => _isConnected;

        /// <inheritdoc/>
        public bool IsCommunicating => _isCommunicating;

        /// <inheritdoc/>
        public ControlState ControlState => _controlState;

        /// <inheritdoc/>
        public bool IsLicensed => _gem.GetRuntimeState() == 1;
        #endregion

        #region Events
        /// <inheritdoc/>
        public event EventHandler<SecsGemEventArgs>? GemEvent;

        /// <inheritdoc/>
        public event EventHandler<SecsMessageEventArgs>? MessageReceived;

        /// <inheritdoc/>
        public event EventHandler<SecsMessageEventArgs>? MessageSent;

        /// <inheritdoc/>
        public event EventHandler<ConnectionChangedEventArgs>? ConnectionChanged;

        /// <inheritdoc/>
        public event EventHandler<ControlStateChangedEventArgs>? ControlStateChanged;

        /// <inheritdoc/>
        public event EventHandler<RemoteCommandEventArgs>? RemoteCommandReceived;

        /// <inheritdoc/>
        public event EventHandler<TerminalMessageEventArgs>? TerminalMessageReceived;
        #endregion

        #region Constructor
        /// <summary>
        /// EZGemPlusDriver 생성자
        /// </summary>
        public EZGemPlusDriver()
        {
            _gem = new CEZGemPlusLib();
        }

        /// <summary>
        /// 이벤트 핸들러 등록 (Start 전에 호출)
        /// </summary>
        private void RegisterEventHandlers()
        {
            if (_initialized) return;

            _gem.OnEZGemEvent += new ON_EZGEM_EVENT(OnEZGemEventHandler);
            _gem.OnEZGemMsg += new ON_EZGEM_MSG(OnEZGemMsgHandler);

            _initialized = true;
            Log.Instance.SecsGem("EZGemPlusDriver: 이벤트 핸들러 등록됨");
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// EZGemPlus 이벤트 핸들러
        /// </summary>
        private void OnEZGemEventHandler(IntPtr lpParam, short nEventId, int lParam)
        {
            var eventType = (EZGemEventId)nEventId;
            var previousControlState = _controlState;

            // 연결 상태 업데이트
            switch (nEventId)
            {
                case 1: // Connect
                    _isConnected = true;
                    Log.Instance.SecsGem("호스트 연결됨");
                    ConnectionChanged?.Invoke(this, new ConnectionChangedEventArgs(true, "호스트 연결됨"));
                    break;

                case 2: // Disconnect
                    _isConnected = false;
                    _isCommunicating = false;
                    Log.Instance.SecsGem("호스트 연결 해제");
                    ConnectionChanged?.Invoke(this, new ConnectionChangedEventArgs(false, "호스트 연결 해제"));
                    break;

                case 10: // EvaluationMode
                    Log.Instance.SecsGem("평가판 모드 (라이선스 없음)");
                    break;

                case 11: // RuntimeMode
                    Log.Instance.SecsGem("정식 런타임 모드 (라이선스 확인)");
                    break;

                case 1010: // Communicating
                    _isCommunicating = true;
                    Log.Instance.SecsGem("Communicating 상태");
                    break;

                case 1001: // Offline
                    _controlState = ControlState.Offline;
                    Log.Instance.SecsGem("Control State → Offline");
                    ControlStateChanged?.Invoke(this,
                        new ControlStateChangedEventArgs(previousControlState, ControlState.Offline));
                    break;

                case 1002: // OnlineLocal
                    _controlState = ControlState.OnlineLocal;
                    Log.Instance.SecsGem("Control State → Online Local");
                    ControlStateChanged?.Invoke(this,
                        new ControlStateChangedEventArgs(previousControlState, ControlState.OnlineLocal));
                    break;

                case 1003: // OnlineRemote
                    _controlState = ControlState.OnlineRemote;
                    Log.Instance.SecsGem("Control State → Online Remote");
                    ControlStateChanged?.Invoke(this,
                        new ControlStateChangedEventArgs(previousControlState, ControlState.OnlineRemote));
                    break;

                case 401: // MsgIn
                    int streamIn = lParam / 1000;
                    int functionIn = lParam % 1000;
                    Log.Instance.SecsGem(string.Format("RX: S{0}F{1}", streamIn, functionIn));
                    MessageReceived?.Invoke(this, new SecsMessageEventArgs(streamIn, functionIn, true));
                    break;

                case 402: // MsgOut
                    int streamOut = lParam / 1000;
                    int functionOut = lParam % 1000;
                    Log.Instance.SecsGem(string.Format("TX: S{0}F{1}", streamOut, functionOut));
                    MessageSent?.Invoke(this, new SecsMessageEventArgs(streamOut, functionOut, false));
                    break;

                case 1030: // RemoteCommand
                    HandleRemoteCommand(lParam);
                    break;

                case 1015: // HostECID
                    Log.Instance.SecsGem("Host ECID 변경 요청 (S2F15)");
                    break;

                case 1050: // TerminalMessage
                    HandleTerminalMessage(lParam);
                    break;

                case 1040: // SpoolUpdated
                    Log.Instance.SecsGem("Spool 업데이트");
                    break;

                case 1041: // SpoolFull
                    Log.Instance.SecsGem("Spool 가득 참");
                    break;

                default:
                    Log.Instance.SecsGem(string.Format("이벤트 수신 - ID:{0}, Param:{1}", nEventId, lParam));
                    break;
            }

            // 모든 이벤트를 외부로 전파
            GemEvent?.Invoke(this, new SecsGemEventArgs(eventType, lParam));
        }

        /// <summary>
        /// 사용자 정의 메시지 핸들러
        /// </summary>
        private void OnEZGemMsgHandler(IntPtr lpParam, int lMsgId)
        {
            short nStream = 0, nFunction = 0, nWbit = 0;
            int nLength = 0;

            _gem.GetMsgInfo(lMsgId, ref nStream, ref nFunction, ref nWbit, ref nLength);

            Log.Instance.SecsGem(string.Format("사용자 정의 메시지 수신 - S{0}F{1} (W={2})", nStream, nFunction, nWbit));

            // Offline 상태면 Abort 처리
            if (_controlState == ControlState.Offline)
            {
                _gem.AbortMsg(lMsgId);
                Log.Instance.SecsGem("Offline 상태로 메시지 Abort");
                return;
            }

            // 사용자 정의 메시지 처리 이벤트 발생
            MessageReceived?.Invoke(this, new SecsMessageEventArgs(nStream, nFunction, true, lMsgId));

            // W-bit가 없으면 메시지 정리
            if (nWbit == 0)
            {
                _gem.CloseMsg(lMsgId);
            }
        }

        private void HandleRemoteCommand(int lMsgId)
        {
            string strCommand = "";
            short nParamCount = (short)_gem.GetRemoteCommand(lMsgId, ref strCommand);

            Log.Instance.SecsGem(string.Format("Remote Command 수신 - {0} (파라미터: {1}개)", strCommand, nParamCount));

            var commandData = new RemoteCommandData
            {
                MessageId = lMsgId,
                CommandName = strCommand
            };

            // 파라미터 수집
            for (int i = 0; i < nParamCount; i++)
            {
                string cpName = "";
                string cpValue = "";
                int nFormat = 0;
                _gem.GetRemoteCommandParam(lMsgId, i, ref cpName, ref cpValue, ref nFormat);
                commandData.Parameters[cpName] = cpValue;
            }

            RemoteCommandReceived?.Invoke(this, new RemoteCommandEventArgs(commandData));
        }

        private void HandleTerminalMessage(int lMsgId)
        {
            int nTid = 0;
            string strMessage = "";
            int nCount = _gem.GetTerminalMsg(lMsgId, ref nTid, ref strMessage);

            while (nCount >= 0)
            {
                Log.Instance.SecsGem(string.Format("Terminal Message - TID:{0}, MSG:{1}", nTid, strMessage));
                TerminalMessageReceived?.Invoke(this, new TerminalMessageEventArgs(nTid, strMessage));

                nCount = _gem.GetTerminalMsg(lMsgId, ref nTid, ref strMessage);
            }
        }
        #endregion

        #region Lifecycle
        /// <inheritdoc/>
        public int Start()
        {
            // 이벤트 핸들러 등록
            RegisterEventHandlers();

            int result = _gem.Start();
            if (result < 0)
            {
                Log.Instance.Error(string.Format("SECS/GEM: 드라이버 시작 실패 (Code: {0})", result));
            }
            else
            {
                Log.Instance.SecsGem("드라이버 시작됨");

               
            }
            return result;
        }

        /// <inheritdoc/>
        public int Stop()
        {
            int result = _gem.Stop();
            _isConnected = false;
            _isCommunicating = false;
            Log.Instance.SecsGem("드라이버 정지됨");
            return result;
        }

        /// <inheritdoc/>
        public short GetRuntimeState()
        {
            return (short)_gem.GetRuntimeState();
        }
        #endregion

        #region Configuration
        /// <inheritdoc/>
        public void ApplyConfiguration(SecsGemConfig config)
        {
            // 장비 정보 설정
            _gem.SetModelName(config.ModelName);
            _gem.SetSoftRev(config.SoftwareRevision);

            // Device ID 및 연결 설정
            _gem.DeviceID = (short)config.DeviceId;
            _gem.PassiveMode = config.IsPassive ? (short)1 : (short)0;
            _gem.SetIP(config.HostIp);
            _gem.Port = (short)config.HostPort;

            // 타이머 설정
            _gem.T3 = (short)config.T3Timeout;
            _gem.T5 = (short)config.T5Timeout;
            _gem.T6 = (short)config.T6Timeout;
            _gem.T7 = (short)config.T7Timeout;
            _gem.T8 = (short)config.T8Timeout;
            _gem.CommRequest = (short)config.CommRequestTimeout;

            // 로그 설정
            if (!string.IsNullOrEmpty(config.LogFilePath))
            {
                
                _gem.SetLogFile(AppContext.BaseDirectory + $"\\log\\SecsGem\\SecsGem.txt");
                //     _gem.SetLogFile(config.LogFilePath);
                _gem.SetLogRetention((short)config.LogRetentionDays);

                if (config.EnableLog)
                    _gem.EnableLog();
                else
                    _gem.DisableLog();
            }

            // Spool 설정
            if (config.EnableSpooling)
            {
                _gem.EnableSpooling();
                _gem.SetMaxSpoolCount((uint)config.MaxSpoolCount);

                if (config.EnableSpoolOverwrite)
                    _gem.EnableSpoolOverwrite();
                else
                    _gem.DisableSpoolOverwrite();
            }
            else
            {
                _gem.DisableSpooling();
            }

            Log.Instance.SecsGem("EZGemPlusDriver: 설정 적용 완료");
        }
        #endregion

        #region Log Settings
        /// <inheritdoc/>
        public void EnableLog()
        {
            _gem.EnableLog();
        }

        /// <inheritdoc/>
        public void DisableLog()
        {
            _gem.DisableLog();
        }

        /// <inheritdoc/>
        public void SkipLog(short stream, short function)
        {
            _gem.SkipLog(stream, function);
        }
        #endregion

        #region SVID Management
        /// <inheritdoc/>
        public int AddSVID(int svid, string name, string format, string unit)
        {
            return _gem.AddSVID(svid, name, format, unit);
        }

        /// <inheritdoc/>
        public int SetSVIDValue(int svid, string value)
        {
            return _gem.SetSVIDValue(svid, value);
        }

        /// <inheritdoc/>
        public string GetSVIDValue(int svid)
        {
            string value = "";
            _gem.GetSVIDValue(svid, ref value);
            return value;
        }
        #endregion

        #region ECID Management
        /// <inheritdoc/>
        public int AddECID(int ecid, string name, string unit, string format)
        {
            return _gem.AddECID(ecid, name, unit, format);
        }

        /// <inheritdoc/>
        public int SetECValue(int ecid, string value)
        {
            return _gem.SetECValue(ecid, value);
        }

        /// <inheritdoc/>
        public string GetECValue(int ecid)
        {
            string value = "";
            _gem.GetECValue(ecid, ref value);
            return value;
        }

        /// <inheritdoc/>
        public int SetECRange(int ecid, string minValue, string maxValue)
        {
            return _gem.SetECRange(ecid, minValue, maxValue);
        }
        #endregion

        #region CEID Management
        /// <inheritdoc/>
        public int AddCEID(int ceid, string name, string comment)
        {
            return _gem.AddCEID(ceid, name, comment);
        }

        /// <inheritdoc/>
        public int SendEventReport(int ceid)
        {
            int result = _gem.SendEventReport(ceid);
            if (result != 0)
            {
                Log.Instance.Error(string.Format("SendEventReport 실패 (CEID:{0}, Code:{1})", ceid, result));
            }
            return result;
        }

        /// <inheritdoc/>
        public int SendEventReportEx(int ceid, int transactionId)
        {
            return _gem.SendEventReportEx(ceid, transactionId);
        }
        #endregion

        #region ALID Management
        /// <inheritdoc/>
        public int AddALID(int alid, string alarmText, string alarmCode)
        {
            return _gem.AddALID(alid, alarmText, alarmCode);
        }

        /// <inheritdoc/>
        public int SendAlarmReport(int alid, short alarmCode)
        {
            int result = _gem.SendAlarmReport(alid, 1);
            if (result != 0)
            {
             
                Log.Instance.Error(string.Format("SendAlarmReport 실패 (ALID:{0}, Code:{1})", alid, result));
            }
            return result;
        }
        #endregion

        #region Control State
        /// <inheritdoc/>
        public void GoOnlineRemote()
        {
            _gem.GoOnlineRemote();
        }

        /// <inheritdoc/>
        public void GoOnlineLocal()
        {
            _gem.GoOnlineLocal();
        }

        /// <inheritdoc/>
        public void GoOffline()
        {
            _gem.GoOffline();
        }
        #endregion

        #region Remote Command
        /// <inheritdoc/>
        public int AddRemoteCommand(string rcmd)
        {
            return _gem.AddRemoteCommand(rcmd);
        }

        /// <inheritdoc/>
        public short GetRemoteCommand(int msgId, ref string rcmd)
        {
            return (short)_gem.GetRemoteCommand(msgId, ref rcmd);
        }

        /// <inheritdoc/>
        public void GetRemoteCommandParam(int msgId, int index, ref string cpName, ref string cpValue)
        {
            int nFormat = 0;
            _gem.GetRemoteCommandParam(msgId, index, ref cpName, ref cpValue, ref nFormat);
        }

        /// <inheritdoc/>
        public short ReplyRemoteCommand(int msgId, string rcmd, short hcAck)
        {
            // DLL API에 맞게 호출 (실제 시그니처 확인 후 수정 필요)
            return (short)_gem.ReplyRemoteCommand(msgId, hcAck);
        }
        #endregion

        #region Spool
        /// <inheritdoc/>
        public void EnableSpooling()
        {
            _gem.EnableSpooling();
        }

        /// <inheritdoc/>
        public void DisableSpooling()
        {
            _gem.DisableSpooling();
        }

        /// <inheritdoc/>
        public void SetMaxSpoolCount(int maxCount)
        {
            _gem.SetMaxSpoolCount((uint)maxCount);
        }

        /// <inheritdoc/>
        public void SetMaxSpoolTransmit(short maxTransmit)
        {
            // DLL에 해당 메서드가 없을 수 있음
            // _gem.SetMaxSpoolTransmit(maxTransmit);
        }

        /// <inheritdoc/>
        public void EnableSpoolOverwrite()
        {
            _gem.EnableSpoolOverwrite();
        }

        /// <inheritdoc/>
        public void DisableSpoolOverwrite()
        {
            _gem.DisableSpoolOverwrite();
        }
        #endregion

        #region Terminal Message
        /// <inheritdoc/>
        public int GetTerminalMsg(int msgId, ref int tid, ref string message)
        {
            return _gem.GetTerminalMsg(msgId, ref tid, ref message);
        }
        #endregion

        #region User Defined Message
        /// <inheritdoc/>
        public int DisableAutoReply(short stream, short function)
        {
            return _gem.DisableAutoReply(stream, function);
        }

        /// <inheritdoc/>
        public int CreateMsg(short stream, short function, short wbit)
        {
            return _gem.CreateMsg(stream, function, wbit);
        }

        /// <summary>
        /// Reply 메시지 생성
        /// </summary>
        public int CreateReplyMsg(int msgId)
        {
            return _gem.CreateReplyMsg(msgId);
        }

        /// <inheritdoc/>
        public int SendMsg(int msgId)
        {
            return _gem.SendMsg(msgId);
        }

        /// <inheritdoc/>
        public void GetMsgInfo(int msgId, ref short stream, ref short function, ref short wbit, ref int length)
        {
            _gem.GetMsgInfo(msgId, ref stream, ref function, ref wbit, ref length);
        }

        /// <inheritdoc/>
        public void AbortMsg(int msgId)
        {
            _gem.AbortMsg(msgId);
        }

        /// <inheritdoc/>
        public void CloseMsg(int msgId)
        {
            _gem.CloseMsg(msgId);
        }

        /// <summary>
        /// S9F3 에러 메시지 전송
        /// </summary>
        public void SendS9F3(int msgId)
        {
            _gem.SendS9F3(msgId);
        }

        /// <summary>
        /// S9F5 에러 메시지 전송
        /// </summary>
        public void SendS9F5(int msgId)
        {
            _gem.SendS9F5(msgId);
        }

        /// <summary>
        /// S9F7 에러 메시지 전송
        /// </summary>
        public void SendS9F7(int msgId)
        {
            _gem.SendS9F7(msgId);
        }
        #endregion

        #region Message Item Construction
        /// <inheritdoc/>
        public void OpenListItem(int msgId)
        {
            _gem.OpenListItem(msgId);
        }

        /// <inheritdoc/>
        public void CloseListItem(int msgId)
        {
            _gem.CloseListItem(msgId);
        }

        /// <summary>
        /// List Item 개수 조회
        /// </summary>
        public int GetListItem(int msgId)
        {
            return _gem.GetListItem(msgId);
        }

        /// <summary>
        /// List Item Open (개수 반환)
        /// </summary>
        public int GetListItemOpen(int msgId)
        {
            return _gem.GetListItemOpen(msgId);
        }

        /// <summary>
        /// List Item Close
        /// </summary>
        public void GetListItemClose(int msgId)
        {
            _gem.GetListItemClose(msgId);
        }

        /// <inheritdoc/>
        public void AddAsciiItem(int msgId, string value, int length)
        {
            _gem.AddAsciiItem(msgId, value, length);
        }

        /// <summary>
        /// ASCII Item 조회
        /// </summary>
        public void GetAsciiItem(int msgId, ref string value)
        {
            _gem.GetAsciiItem(msgId, ref value);
        }

        /// <inheritdoc/>
        public void AddU1Item(int msgId, byte value, int count)
        {
            _gem.AddU1Item(msgId, value);
        }

        /// <summary>
        /// U1 Item 조회 (단일)
        /// </summary>
        public int GetU1Item(int msgId, ref byte value)
        {
            return _gem.GetU1Item(msgId, ref value);
        }

        /// <summary>
        /// U1 Item 조회 (배열)
        /// </summary>
        public int GetU1Item(int msgId, byte[] values)
        {
            return _gem.GetU1Item(msgId, values);
        }

        /// <inheritdoc/>
        public void AddU2Item(int msgId, ushort value, int count)
        {
            _gem.AddU2Item(msgId, value);
        }

        /// <inheritdoc/>
        public void AddU4Item(int msgId, uint value, int count)
        {
            _gem.AddU4Item(msgId, value);
        }

        /// <inheritdoc/>
        public void AddI4Item(int msgId, int value, int count)
        {
            _gem.AddI4Item(msgId, value);
        }

        /// <inheritdoc/>
        public void AddF4Item(int msgId, float value, int count)
        {
            _gem.AddF4Item(msgId, value);
        }

        /// <summary>
        /// Array Item 조회
        /// </summary>
        public int GetArrayItem(int msgId, ref string value)
        {
            return _gem.GetArrayItem(msgId, ref value);
        }
        #endregion

        #region Format Code
        /// <inheritdoc/>
        public void SetFormatCode(string itemType, string formatCode)
        {
            _gem.SetFormatCode(itemType, formatCode);
        }
        #endregion

        #region IDisposable
        /// <summary>
        /// 리소스 해제
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 리소스 해제 구현
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Stop();

                    // 이벤트 핸들러 해제
                    if (_initialized)
                    {
                        _gem.OnEZGemEvent -= OnEZGemEventHandler;
                        _gem.OnEZGemMsg -= OnEZGemMsgHandler;
                    }
                }

                _disposed = true;
            }
        }

        /// <summary>
        /// 소멸자
        /// </summary>
        ~EZGemPlusDriver()
        {
            Dispose(false);
        }
        #endregion
    }
}
