using EQ.Common.Logs;
using EQ.Domain.Entities.SecsGem;
using EQ.Domain.Enums.SecsGem;
using EQ.Domain.Interface.SecsGem;
using System.Collections.Concurrent;
using static EQ.Core.Globals;

namespace EQ.Core.Act.Composition.SecsGem
{
    /// <summary>
    /// SECS/GEM 통신 비즈니스 로직을 담당하는 모듈
    /// EZGemPlus 라이브러리를 ISecsGemDriver를 통해 추상화하여 사용합니다.
    /// </summary>
    public class ActSecsGem : ActComponent
    {
        #region Fields
        private ISecsGemDriver? _driver;
        private SecsGemConfig? _config;
        private bool _initialized = false;

        // 데이터 저장소
        private readonly ConcurrentDictionary<int, StatusVariable> _svidMap = new();
        private readonly ConcurrentDictionary<int, EquipmentConstant> _ecidMap = new();
        private readonly ConcurrentDictionary<int, CollectionEvent> _ceidMap = new();
        private readonly ConcurrentDictionary<int, AlarmDefinition> _alidMap = new();
        private readonly ConcurrentDictionary<string, RemoteCommandDef> _rcmdMap = new();

        // 값 업데이트 콜백
        private readonly ConcurrentDictionary<int, Func<object>> _svidValueProviders = new();
        private readonly ConcurrentDictionary<int, Func<object>> _ecidValueProviders = new();
        #endregion

        #region Properties
        /// <summary>
        /// 연결 상태
        /// </summary>
        public bool IsConnected => _driver?.IsConnected ?? false;

        /// <summary>
        /// 통신 상태
        /// </summary>
        public bool IsCommunicating => _driver?.IsCommunicating ?? false;

        /// <summary>
        /// Control State
        /// </summary>
        public ControlState ControlState => _driver?.ControlState ?? ControlState.Offline;

        /// <summary>
        /// 라이선스 상태
        /// </summary>
        public bool IsLicensed => _driver?.IsLicensed ?? false;

        /// <summary>
        /// 초기화 여부
        /// </summary>
        public bool IsInitialized => _initialized;
        #endregion

        #region Events
        /// <summary>
        /// 연결 상태 변경 이벤트
        /// </summary>
        public event EventHandler<ConnectionChangedEventArgs>? OnConnectionChanged;

        /// <summary>
        /// Control State 변경 이벤트
        /// </summary>
        public event EventHandler<ControlStateChangedEventArgs>? OnControlStateChanged;

        /// <summary>
        /// Remote Command 수신 이벤트
        /// </summary>
        public event EventHandler<RemoteCommandEventArgs>? OnRemoteCommand;

        /// <summary>
        /// 메시지 수신 이벤트
        /// </summary>
        public event EventHandler<SecsMessageEventArgs>? OnMessageReceived;
        #endregion

        #region Constructor
        /// <summary>
        /// ActSecsGem 생성자
        /// </summary>
        public ActSecsGem(ACT act) : base(act) { }
        #endregion

        #region Definition File Loading
        /// <summary>
        /// 정의 파일 기본 경로
        /// </summary>
        private const string DEFINITIONS_FOLDER = "CommonData\\SecsGem";
        private const string DEFINITIONS_FILE = "SecsGemDefinitions";

        /// <summary>
        /// 파일에서 SVID/ECID/CEID/ALID/RCMD 정의를 로드하고 드라이버에 등록합니다.
        /// 파일이 없으면 기본값으로 생성합니다.
        /// </summary>
        /// <param name="basePath">기본 경로 (Application.StartupPath)</param>
        public void LoadDefinitionsFromFile(string basePath)
        {
            if (_driver == null)
            {
                Log.Instance.Warning(L("ActSecsGem: 드라이버가 초기화되지 않아 정의를 로드할 수 없습니다."));
                return;
            }

            string folderPath = Path.Combine(basePath, DEFINITIONS_FOLDER);
            string filePath = Path.Combine(folderPath, $"{DEFINITIONS_FILE}.json");

            SecsGemDefinitions definitions;

            // 파일이 없으면 기본값으로 생성
            if (!File.Exists(filePath))
            {
                Log.Instance.SecsGem(L("정의 파일이 없습니다. 기본값으로 생성합니다: {0}", filePath));
                definitions = SecsGemDefinitions.CreateDefault();
                SaveDefinitionsToFile(basePath, definitions);
            }
            else
            {
                // 파일에서 로드
                definitions = LoadDefinitionsFromJson(filePath);
                if (definitions == null)
                {
                    Log.Instance.Warning(L("정의 파일 로드 실패. 기본값 사용: {0}", filePath));
                    definitions = SecsGemDefinitions.CreateDefault();
                }
            }

            // 드라이버에 등록
            RegisterAllDefinitions(definitions);

            Log.Instance.SecsGem(L("정의 로드 완료 - SVID:{0}, ECID:{1}, CEID:{2}, ALID:{3}, RCMD:{4}",
                definitions.SVIDs.Count, definitions.ECIDs.Count, definitions.CEIDs.Count,
                definitions.ALIDs.Count, definitions.RCMDs.Count));
        }

        /// <summary>
        /// 정의를 파일로 저장
        /// </summary>
        public void SaveDefinitionsToFile(string basePath, SecsGemDefinitions? definitions = null)
        {
            string folderPath = Path.Combine(basePath, DEFINITIONS_FOLDER);
            string filePath = Path.Combine(folderPath, $"{DEFINITIONS_FILE}.json");

            try
            {
                Directory.CreateDirectory(folderPath);

                // 현재 등록된 정의 수집 (definitions가 null인 경우)
                if (definitions == null)
                {
                    definitions = new SecsGemDefinitions
                    {
                        SVIDs = _svidMap.Values.ToList(),
                        ECIDs = _ecidMap.Values.ToList(),
                        CEIDs = _ceidMap.Values.ToList(),
                        ALIDs = _alidMap.Values.ToList(),
                        RCMDs = _rcmdMap.Values.ToList()
                    };
                }

                string json = System.Text.Json.JsonSerializer.Serialize(definitions,
                    new System.Text.Json.JsonSerializerOptions { WriteIndented = true });

                File.WriteAllText(filePath, json);
                Log.Instance.SecsGem(L("정의 파일 저장 완료: {0}", filePath));
            }
            catch (Exception ex)
            {
                Log.Instance.Error(L("정의 파일 저장 실패: {0}", ex.Message));
            }
        }

        /// <summary>
        /// JSON 파일에서 정의 로드
        /// </summary>
        private SecsGemDefinitions? LoadDefinitionsFromJson(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                return System.Text.Json.JsonSerializer.Deserialize<SecsGemDefinitions>(json);
            }
            catch (Exception ex)
            {
                Log.Instance.Error(L("정의 파일 파싱 실패: {0}", ex.Message));
                return null;
            }
        }

        /// <summary>
        /// 모든 정의를 드라이버에 등록
        /// </summary>
        private void RegisterAllDefinitions(SecsGemDefinitions definitions)
        {
            // SVID 등록
            foreach (var sv in definitions.SVIDs)
            {
                var idx = sv.SVID;

                Func<object>? valueProvider = null;

                //TODO 장비의 변수와 연결 시킴
                switch (idx)
                {
                    default:
                        {
                            Log.Instance.Error($"SVID {idx} is not defined");
                        }
                        break;
                }

                RegisterSVID(sv, valueProvider);
            }

            // ECID 등록
            foreach (var ec in definitions.ECIDs)
            {
                var idx = ec.ECID;

                Func<object>? valueProvider = null;

                //TODO 장비의 변수와 연결 시킴
                switch (idx)
                {
                    default:
                        {
                            Log.Instance.Error($"ECID {idx} is not defined");
                        }
                        break;
                }

                RegisterECID(ec, valueProvider);
            }

            // CEID 등록
            foreach (var ce in definitions.CEIDs)
            {
                var idx = ce.CEID;

                //TODO 장비의 이벤트와 연결 시킴
                switch (idx)
                {
                    default:
                        {
                            Log.Instance.Error($"CEID {idx} is not defined");
                        }
                        break;
                }

                RegisterCEID(ce);
            }

            // ALID 등록
            foreach (var alarm in definitions.ALIDs)
            {
                var idx = alarm.ALID;

                //TODO 장비의 알람과 연결 시킴
                switch (idx)
                {
                    default:
                        {
                            Log.Instance.Error($"ALID {idx} is not defined");
                        }
                        break;
                }

                RegisterALID(alarm);
            }

            // RCMD 등록
            foreach (var rcmd in definitions.RCMDs)
            {
                var cmdName = rcmd.CommandName;

                //TODO 장비의 원격 명령과 연결 시킴
                switch (cmdName)
                {
                    default:
                        {
                            Log.Instance.Error($"RCMD {cmdName} is not defined");
                        }
                        break;
                }

                RegisterRemoteCommand(rcmd);
            }
        }

        /// <summary>
        /// 현재 등록된 정의 조회
        /// </summary>
        public SecsGemDefinitions GetCurrentDefinitions()
        {
            return new SecsGemDefinitions
            {
                SVIDs = _svidMap.Values.ToList(),
                ECIDs = _ecidMap.Values.ToList(),
                CEIDs = _ceidMap.Values.ToList(),
                ALIDs = _alidMap.Values.ToList(),
                RCMDs = _rcmdMap.Values.ToList()
            };
        }
        #endregion

        #region Initialization
        /// <summary>
        /// SECS/GEM 드라이버 초기화
        /// </summary>
        /// <param name="driver">ISecsGemDriver 구현체</param>
        /// <param name="config">설정</param>
        public void Initialize(ISecsGemDriver driver, SecsGemConfig config)
        {
            if (_initialized)
            {
                Log.Instance.Warning(L("ActSecsGem: 이미 초기화되어 있습니다."));
                return;
            }

            _driver = driver;
            _config = config;

            // 드라이버 이벤트 등록
            RegisterDriverEvents();

            // 설정 적용
            ApplyConfiguration();

            _initialized = true;
            Log.Instance.SecsGem(L("ActSecsGem: 초기화 완료"));
        }

        /// <summary>
        /// 드라이버 이벤트 핸들러 등록
        /// </summary>
        private void RegisterDriverEvents()
        {
            if (_driver == null) return;

            _driver.ConnectionChanged += OnDriverConnectionChanged;
            _driver.ControlStateChanged += OnDriverControlStateChanged;
            _driver.RemoteCommandReceived += OnDriverRemoteCommand;
            _driver.MessageReceived += OnDriverMessageReceived;
            _driver.GemEvent += OnDriverGemEvent;
        }

        /// <summary>
        /// 설정 적용
        /// </summary>
        private void ApplyConfiguration()
        {
            if (_driver == null || _config == null) return;

            // 드라이버에 설정 전달
            _driver.ApplyConfiguration(_config);

            Log.Instance.SecsGem(L("ActSecsGem: 설정 적용 완료"));
        }
        #endregion

        #region Lifecycle
        /// <summary>
        /// SECS/GEM 드라이버 시작
        /// </summary>
        public int Start()
        {
            if (!_initialized || _driver == null)
            {
                Log.Instance.Error(L("ActSecsGem: 초기화되지 않음"));
                return -1;
            }

            int result = _driver.Start();
            if (result == 0)
            {
                Log.Instance.SecsGem(L("ActSecsGem: 드라이버 시작됨"));

                // 초기 Control State 설정
                if (_config?.InitialControlState == ControlState.OnlineRemote)
                    _driver.GoOnlineRemote();
                else if (_config?.InitialControlState == ControlState.OnlineLocal)
                    _driver.GoOnlineLocal();

                _driver.GoOnlineRemote();
            }
            else
            {
                Log.Instance.Error(L("ActSecsGem: 드라이버 시작 실패 (Code: {0})", result));
            }

            return result;
        }

        /// <summary>
        /// SECS/GEM 드라이버 정지
        /// </summary>
        public int Stop()
        {
            if (_driver == null) return 0;

            int result = _driver.Stop();
            Log.Instance.SecsGem(L("ActSecsGem: 드라이버 정지됨"));
            return result;
        }

        /// <summary>
        /// Online Remote 상태로 전환
        /// </summary>
        public void GoOnlineRemote()
        {
            _driver?.GoOnlineRemote();
        }

        /// <summary>
        /// Online Local 상태로 전환
        /// </summary>
        public void GoOnlineLocal()
        {
            _driver?.GoOnlineLocal();
        }

        /// <summary>
        /// Offline 상태로 전환
        /// </summary>
        public void GoOffline()
        {
            _driver?.GoOffline();
        }
        #endregion

        #region SVID Management
        /// <summary>
        /// SVID 등록
        /// </summary>
        /// <param name="sv">Status Variable 정의</param>
        /// <param name="valueProvider">값 제공 함수 (자동 업데이트용)</param>
        public void RegisterSVID(StatusVariable sv, Func<object>? valueProvider = null)
        {
            if (_driver == null) return;

            _svidMap[sv.SVID] = sv;
            if (valueProvider != null)
            {
                _svidValueProviders[sv.SVID] = valueProvider;
            }

            // Format은 이미 string 타입 (A, U1, U2 등)
            _driver.AddSVID(sv.SVID, sv.Name, sv.Format, sv.Unit);

            Log.Instance.SecsGem(L("SVID 등록: {0} ({1})", sv.SVID, sv.Name));
        }

        /// <summary>
        /// SVID 값 업데이트
        /// </summary>
        public void UpdateSVIDValue(int svid, object value)
        {
            if (_driver == null) return;

            if (_svidMap.TryGetValue(svid, out var sv))
            {
                sv.Value = value?.ToString() ?? "";
                _driver.SetSVIDValue(svid, sv.Value);
            }
        }

        /// <summary>
        /// 모든 SVID 값 자동 업데이트 (Host 요청 시 호출)
        /// </summary>
        public void UpdateAllSVIDValues()
        {
            foreach (var kvp in _svidValueProviders)
            {
                try
                {
                    var value = kvp.Value();
                    UpdateSVIDValue(kvp.Key, value);
                }
                catch (Exception ex)
                {
                    Log.Instance.Error(L("SVID {0} 업데이트 실패: {1}", kvp.Key, ex.Message));
                }
            }
        }
        #endregion

        #region ECID Management
        /// <summary>
        /// ECID 등록
        /// </summary>
        /// <param name="ec">Equipment Constant 정의</param>
        /// <param name="valueProvider">값 제공 함수 (자동 업데이트용)</param>
        public void RegisterECID(EquipmentConstant ec, Func<object>? valueProvider = null)
        {
            if (_driver == null) return;

            _ecidMap[ec.ECID] = ec;
            if (valueProvider != null)
            {
                _ecidValueProviders[ec.ECID] = valueProvider;
            }

            // Format은 이미 string 타입
            _driver.AddECID(ec.ECID, ec.Name, ec.Unit, ec.Format);

            Log.Instance.SecsGem(L("ECID 등록: {0} ({1})", ec.ECID, ec.Name));
        }

        /// <summary>
        /// ECID 값 업데이트
        /// </summary>
        public void UpdateECValue(int ecid, object value)
        {
            if (_driver == null) return;

            if (_ecidMap.TryGetValue(ecid, out var ec))
            {
                ec.Value = value?.ToString() ?? "";
                _driver.SetECValue(ecid, ec.Value);
            }
        }

        /// <summary>
        /// 모든 ECID 값 자동 업데이트 (Host 요청 시 호출)
        /// </summary>
        public void UpdateAllECValues()
        {
            foreach (var kvp in _ecidValueProviders)
            {
                try
                {
                    var value = kvp.Value();
                    UpdateECValue(kvp.Key, value);
                }
                catch (Exception ex)
                {
                    Log.Instance.Error(L("ECID {0} 업데이트 실패: {1}", kvp.Key, ex.Message));
                }
            }
        }
        #endregion

        #region CEID Management
        /// <summary>
        /// CEID 등록
        /// </summary>
        public void RegisterCEID(CollectionEvent ce)
        {
            if (_driver == null) return;

            _ceidMap[ce.CEID] = ce;
            _driver.AddCEID(ce.CEID, ce.Name, ce.Comment);

            Log.Instance.SecsGem(L("CEID 등록: {0} ({1})", ce.CEID, ce.Name));
        }

        /// <summary>
        /// Collection Event 전송 (S6F11)
        /// </summary>
        public int SendEvent(int ceid)
        {
            if (_driver == null) return -1;

            int result = _driver.SendEventReport(ceid);
            if (result != 0)
            {
                Log.Instance.Error(L("SendEvent 실패 (CEID: {0}, Code: {1})", ceid, result));
            }
            else
            {
                Log.Instance.SecsGem(L("SendEvent 성공 (CEID: {0})", ceid));
            }

            return result;
        }
        #endregion

        #region ALID Management
        /// <summary>
        /// ALID 등록
        /// </summary>
        public void RegisterALID(AlarmDefinition alarm)
        {
            if (_driver == null) return;

            _alidMap[alarm.ALID] = alarm;
            _driver.AddALID(alarm.ALID, alarm.AlarmText, alarm.AlarmCode);

            Log.Instance.SecsGem(L("ALID 등록: {0} ({1})", alarm.ALID, alarm.AlarmText));
        }

        /// <summary>
        /// Alarm 전송 (S5F1)
        /// </summary>
        /// <param name="alid">Alarm ID</param>
        /// <param name="isSet">true: Set, false: Clear</param>
        public int SendAlarm(int alid, bool isSet)
        {
            if (_driver == null) return -1;

            short alarmCode = isSet ? (short)1 : (short)0; // 1: Set, 0: Clear
            int result = _driver.SendAlarmReport(alid, alarmCode);

            if (result != 0)
            {
                Log.Instance.Error(L("SendAlarm 실패 (ALID: {0}, Code: {1})", alid, result));
            }
            else
            {
                Log.Instance.SecsGem(L("SendAlarm 성공 (ALID: {0}, Set: {1})", alid, isSet));
                if (_alidMap.TryGetValue(alid, out var alarm))
                {
                    if (isSet) alarm.SetAlarm();
                    else alarm.ClearAlarm();
                }
            }

            return result;
        }
        #endregion

        #region Remote Command Management
        /// <summary>
        /// Remote Command 정의 등록
        /// </summary>
        public void RegisterRemoteCommand(RemoteCommandDef rcmd)
        {
            if (_driver == null) return;

            _rcmdMap[rcmd.CommandName] = rcmd;
            _driver.AddRemoteCommand(rcmd.CommandName);

            Log.Instance.SecsGem(L("RCMD 등록: {0}", rcmd.CommandName));
        }

        /// <summary>
        /// Remote Command 응답
        /// </summary>
        private void ReplyRemoteCommand(int msgId, string rcmd, HcAck hcAck)
        {
            _driver?.ReplyRemoteCommand(msgId, rcmd, (short)hcAck);
        }

        /// <summary>
        /// Remote Command 처리 (내부)
        /// </summary>
        private void HandleRemoteCommand(RemoteCommandData commandData)
        {
            string cmdName = commandData.CommandName;
            HcAck result = HcAck.Acknowledge; // Ok -> Acknowledge

            // 등록된 명령인지 확인
            if (!_rcmdMap.TryGetValue(cmdName, out var rcmdDef))
            {
                Log.Instance.Warning(L("미등록 Remote Command: {0}", cmdName));
                result = HcAck.CommandNotExist;
            }
            else
            {
                // 파라미터 검증
                foreach (var param in rcmdDef.Parameters.Where(p => p.IsRequired))
                {
                    if (!commandData.Parameters.ContainsKey(param.Name))
                    {
                        Log.Instance.Warning(L("필수 파라미터 누락: {0}", param.Name));
                        result = HcAck.InvalidParameter; // ParameterInvalid -> InvalidParameter
                        break;
                    }
                }
            }

            // 성공했으면 이벤트로 처리 위임
            if (result == HcAck.Acknowledge)
            {
                commandData.ResponseCode = result;
                OnRemoteCommand?.Invoke(this, new RemoteCommandEventArgs(commandData));

                // 이벤트 핸들러에서 ResponseCode가 변경되었을 수 있음
                result = commandData.ResponseCode;
            }

            // 응답 전송
            ReplyRemoteCommand(commandData.MessageId, cmdName, result);
            Log.Instance.SecsGem(L("RCMD 응답: {0} -> {1}", cmdName, result));
        }
        #endregion

        #region Driver Event Handlers
        private void OnDriverConnectionChanged(object? sender, ConnectionChangedEventArgs e)
        {
            Log.Instance.SecsGem(L("연결 상태 변경: {0}", e.IsConnected ? "Connected" : "Disconnected"));
            OnConnectionChanged?.Invoke(this, e);
        }

        private void OnDriverControlStateChanged(object? sender, ControlStateChangedEventArgs e)
        {
            Log.Instance.SecsGem(L("Control State 변경: {0} -> {1}", e.PreviousState, e.CurrentState));
            OnControlStateChanged?.Invoke(this, e);
        }

        private void OnDriverRemoteCommand(object? sender, RemoteCommandEventArgs e)
        {
            HandleRemoteCommand(e.CommandData);
        }

        private void OnDriverMessageReceived(object? sender, SecsMessageEventArgs e)
        {
            OnMessageReceived?.Invoke(this, e);
        }

        private void OnDriverGemEvent(object? sender, SecsGemEventArgs e)
        {
            // Host로부터 SVID/ECID 요청 시 자동 업데이트
            int stream = e.Parameter / 1000;
            int function = e.Parameter % 1000;

            // S1F3 (Selected Equipment Status Request) -> SVID 업데이트
            // S1F11 (Status Variable Namelist Request) -> SVID 업데이트
            if (stream == 1 && (function == 3 || function == 11))
            {
                UpdateAllSVIDValues();
            }

            // S2F13 (Equipment Constant Request) -> ECID 업데이트
            if (stream == 2 && function == 13)
            {
                UpdateAllECValues();
            }
        }
        #endregion

        #region IDisposable
        /// <summary>
        /// 리소스 해제
        /// </summary>
        public void Dispose()
        {
            if (_driver != null)
            {
                _driver.ConnectionChanged -= OnDriverConnectionChanged;
                _driver.ControlStateChanged -= OnDriverControlStateChanged;
                _driver.RemoteCommandReceived -= OnDriverRemoteCommand;
                _driver.MessageReceived -= OnDriverMessageReceived;
                _driver.GemEvent -= OnDriverGemEvent;

                _driver.Dispose();
            }

            _svidMap.Clear();
            _ecidMap.Clear();
            _ceidMap.Clear();
            _alidMap.Clear();
            _rcmdMap.Clear();

            _initialized = false;
        }
        #endregion
    }
}
