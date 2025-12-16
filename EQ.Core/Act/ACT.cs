
using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Core.Act.Composition;
using EQ.Core.Act.Composition.LaserMeasure;
using EQ.Core.Act.Composition.Extruder;
using EQ.Core.Act.Composition.SecsGem;
using EQ.Core.Act.EQ_Hanlim_Extuder;
using EQ.Core.Sequence;
using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.Domain.Interface;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Core.Act
{
    /// <summary>
    /// (신규) 단방향 알림 이벤트용 데이터 클래스
    /// </summary>
    public class NotifyEventArgs : EventArgs
    {
        public string Title { get; }
        public string Message { get; }
        public NotifyType Type { get; }

        public NotifyEventArgs(string title, string message, NotifyType type)
        {
            Title = title;
            Message = message;
            Type = type;
        }
    }
    public class AlarmEventArgs : EventArgs
    {
        public ErrorList Error { get; }
        public string Message { get; }

        public AlarmEventArgs(ErrorList error, string message)
        {
            Error = error;
            Message = message;
        }
    }

    /// <summary>
    /// 모든 기능별 클래스(Motion, IO 등)가 상속받을 기본 클래스
    /// ACT 메인 인스턴스에 접근할 수 있게 해줍니다.
    /// </summary>
    public abstract class ActComponent
    {
        protected readonly ACT _act;

        public ActComponent(ACT act)
        {
            _act = act;
        }
    }

    public partial class ACT
    {

        // --- 컴포지션: 기능별 모듈 선언 ---

        /// <summary>
        /// 모터, 로봇 등 모션(Motion) 관련 기능을 제어합니다.
        /// </summary>
        public ActMotion Motion { get; private set; }
        /// <summary>
        /// 실린더, 센서 등 입출력(IO) 관련 기능을 제어합니다.
        /// </summary>
        public ActIO IO { get; private set; }

        public ActUserOption Option { get; private set; }
        public ActRecipe Recipe { get; private set; }
      
        public ActTowerLamp TowerLamp { get; private set; }

        public ActUser User { get; private set; }
        public ActAlarmDB AlarmDB { get; private set; } // [추가] ActAlarm 모듈
        public ActVision Vision { get; private set; }

        public ActModbus Modbus { get; private set; }
        public ActSerialPort SerialPort { get; private set; }
          
        public ActSample action_Sample { get; private set; } // 샘플 액션 추가

        public ActLanguage Language { get; private set; }

        public ActTemperature Temp { get; private set; } // 온도계

        public ActTray Tray { get; private set; }
        public ActWafer Wafer { get; private set; }

        public ActMagazine<TrayCell> TrayMagazine { get; private set; }
        public ActMagazine<WaferCell> WaferMagazine { get; private set; }

        /// <summary>
        /// PIO (E84) 통신 기능을 제어합니다.
        /// </summary>
        public ActPIO PIO { get; private set; }

        /// <summary>
        /// SECS/GEM 통신 기능을 제어합니다.
        /// </summary>
        public ActSecsGem SecsGem { get; private set; }

        /// <summary>
        /// 레이저 거리 계측기를 제어합니다.
        /// </summary>
        public ActLaserMeasure LaserMeasure { get; private set; }

        // === 한림 압출기 전용 컴포넌트 ===
        
        /// <summary>
        /// (Legacy) 압출기 통합 제어
        /// </summary>
        public ActExtuder Extuder { get; private set; }

        /// <summary>
        /// 압출기 레시피 관리
        /// </summary>
        public ActExtruderRecipe ExtruderRecipe { get; private set; }

        /// <summary>
        /// 압출 및 피더 제어
        /// </summary>
        public Act.Composition.Extruder.ActExtruder Extruder { get; private set; }

        /// <summary>
        /// 인취 및 PID 직경 제어
        /// </summary>
        public Act.Composition.Extruder.ActPuller Puller { get; private set; }

        /// <summary>
        /// 후공정 제어 (커팅, 선별, 카운팅)
        /// </summary>
        public Act.Composition.Extruder.ActFinishing Finishing { get; private set; }

        /// <summary>
        /// 의존성 주입 부
        /// </summary>
        public ACT()
        {
            // 'this'를 넘겨주어 모듈들이 ACT의 기능(ExecuteStepBasedAction)을 쓰게 함
            this.Motion = new ActMotion(this);
            this.IO = new ActIO(this);
            this.action_Sample = new ActSample(this); // 샘플 액션 초기화
            this.Option = new ActUserOption(this);
            this.Recipe = new ActRecipe(this);
            this.TowerLamp = new ActTowerLamp(this);// FSM이 TowerLamp를 사용하므로 TowerLamp를 먼저 생성
            this.User = new ActUser(this);

            this.AlarmDB = new ActAlarmDB(this);
            this.Vision = new ActVision(this);
            this.Modbus = new ActModbus(this);
            this.SerialPort = new ActSerialPort(this);
          
            this.Temp = new ActTemperature(this);

            this.Tray = new ActTray(this);
            this.Wafer = new ActWafer(this);

            this.TrayMagazine = new ActMagazine<TrayCell>(this, "TrayMag");
            this.WaferMagazine = new ActMagazine<WaferCell>(this, "WaferMag");

            this.Language = new ActLanguage(this);

            this.PIO = new ActPIO(this);

            this.SecsGem = new ActSecsGem(this);

            this.LaserMeasure = new ActLaserMeasure(this);

            // 한림 익스투더 관련 초기화 (Legacy)
            this.Extuder = new ActExtuder(this);
            this.ExtruderRecipe = new ActExtruderRecipe(this);

            // 한림 압출기 모듈형 컴포넌트
            this.Extruder = new Act.Composition.Extruder.ActExtruder(this);
            this.Puller = new Act.Composition.Extruder.ActPuller(this);
            this.Finishing = new Act.Composition.Extruder.ActFinishing(this);
        }


        public event EventHandler<AlarmEventArgs> OnAlarmRequest;
        public void PopupAlarm(ErrorList err, string message,
                               [CallerMemberName] string callerName = "",
                               [CallerFilePath] string sourceFilePath = "")
        {
            
            AlarmDB.WriteLog(err, message, callerName, sourceFilePath);
            Log.Instance.Error($"{err} , {message}");          

            
            TowerLamp.SetState(EqState.Error);            
            OnAlarmRequest?.Invoke(this, new AlarmEventArgs(err, message));
        }

        public event EventHandler<NotifyEventArgs> OnNotificationRequest;
        public IConfirmationService PopupYesNo { get; private set; }
        public void RegisterConfirmationService(IConfirmationService service)
        {
            this.PopupYesNo = service;
        }
        /// <summary>
        /// UI 레이어에 알림 팝업을 요청합니다. (Fire and Forget)
        /// (Core는 UI를 모르므로 이벤트를 발생시킴)
        /// </summary>
        public void PopupNoti(string title, string message, NotifyType type)
        {
            if (type == NotifyType.Error)
            {
                // title을 ID로, message를 Info로 사용 (기존 Alarm_DB 로직과 동일하게)
               
            }
            // UI(FormSplash)에 구독자가 있으면 이벤트 발생
            OnNotificationRequest?.Invoke(this, new NotifyEventArgs(title, message, type));
        }
        public void PopupNoti(string message, NotifyType type)
        {
            string title = type.ToString();
            if (type == NotifyType.Error)
            {
                // title을 ID로, message를 Info로 사용 (기존 Alarm_DB 로직과 동일하게)
              
            }
            // UI(FormSplash)에 구독자가 있으면 이벤트 발생
            OnNotificationRequest?.Invoke(this, new NotifyEventArgs(title, message, type));
        }

        // 'static' 제거 (인스턴스 멤버로 변경)
        public ConcurrentDictionary<string, int> ACT_TimeOut = new ConcurrentDictionary<string, int>();
        public ConcurrentDictionary<string, ActionState> ACT_STATUS = new ConcurrentDictionary<string, ActionState>();

        public delegate void Msg(string msg);
        public event Msg OnMsg; // 'static' 제거

        #region Action 상태 관리 (구 static 메서드들)

        public async Task SystemResetAsync()
        {
            try
            {
                // 1. 타워램프/부저 끄기 (Idle 상태: 녹색 점등)
                // 에러음이 시끄러우므로 가장 먼저 끕니다.
                TowerLamp.SetState(EqState.Idle);

                // 2. 시퀀스 상태 초기화 (Error -> Stop)               
                EQ.Core.Service.SeqManager.Instance.Seq.ResetAllSequences();

                // 3. 실행 중이던 액션(Action) 에러 상태 초기화
                ActErrorReset(); // (기존에 있던 메서드 활용)

                // 4. 모든 모터 알람 리셋
                foreach (MotionID id in Enum.GetValues(typeof(MotionID)))
                {
                    // 알람이 있는 축만 리셋 명령 전송 (통신 부하 절감)
                    var status = Motion.GetStatus(id);
                    if (status.AmpAlarm || status.AmpAlarmCode != 0)
                    {
                        Motion.AlarmReset(id);
                        await Task.Delay(10); // 약간의 딜레이 (선택)
                    }
                }              
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[System] Reset Failed: {ex.Message}");
            }
        }

        public ActionStatus GetActionStatus()
        {
            bool isRunning = false;

            foreach (var item in this.ACT_STATUS)
            {
                // 1. 에러/타임아웃은 발견 즉시 리턴 (최고 우선순위)
                if (item.Value.Status == ActionStatus.Timeout || item.Value.Status == ActionStatus.Error)
                {
                    return ActionStatus.Error;
                }

                // 2. 실행 중 상태가 있으면 플래그만 세우고 계속 검사 (혹시 뒤에 에러가 있을 수 있으므로)
                if (item.Value.Status == ActionStatus.Running)
                {
                    isRunning = true;
                }
            }

            // 3. 모든 검사가 끝난 후 판단
            if (isRunning)
                return ActionStatus.Running;

            return ActionStatus.Finished;
        }

       

        public void E_STOP()
        {
            Log.Instance.Warning("E_STOP 실행 - 모든 실행 중인 액션, 시퀀스 즉시 중지");

            // 1. 실행 중인 모든 Action 강제 종료 (Error 처리)
            foreach (var s in this.ACT_STATUS.Values)
            {
                if (s.Status == ActionStatus.Running)
                {
                    s.Status = ActionStatus.Error;
                    // 비동기 Task 즉시 취소를 위해 토큰 Cancel 호출 (ActionState 정의 참조)
                    s.cancellatinSource?.Cancel();
                }
            }

            // 2. 실행 중인 모든 Sequence 정지 (Error 상태로 변경하여 루프 탈출 유도)
            var seqManager = EQ.Core.Service.SeqManager.Instance.Seq;
            foreach (EQ.Core.Sequence.SEQ.SeqName name in Enum.GetValues(typeof(EQ.Core.Sequence.SEQ.SeqName)))
            {
                var seq = seqManager.GetSequence(name);
                // 실행 중(RUN)이거나 정지 중(STOPPING)인 경우 강제 에러 처리
                if (seq != null && (seq._Status == SeqStatus.RUN || seq._Status == SeqStatus.SEQ_STOPPING))
                {
                    seq._Status = SeqStatus.ERROR;
                    Log.Instance.Error($"[E_STOP] Sequence {name} Forced to ERROR.");
                }
            }

            // 3. 모든 모터 정지 명령 (Enum 정의된 모든 축)
            foreach (EQ.Domain.Enums.MotionID id in Enum.GetValues(typeof(EQ.Domain.Enums.MotionID)))
            {
                // ActMotion의 MoveStop 호출 (하드웨어 QuickStop 연결됨)
                this.Motion.MoveStop(id);
            }

            // 4. 타워 램프 및 부저 상태를 Error로 변경 (시각/청각 알림)
            this.TowerLamp.SetState(EQ.Domain.Enums.EqState.Error);

            PopupNoti("E-STOP Click!!!" , NotifyType.Error);
        }

        public void ActErrorReset()
        {
            foreach (var s in this.ACT_STATUS.Values)
            {
                if (s.Status == ActionStatus.Error || s.Status == ActionStatus.Timeout)
                {
                    s.Status = ActionStatus.Finished;
                }
            }
        }

        #endregion

        #region Action 생성 및 실행

        /// <summary>
        /// Action 상태 객체를 생성하고 등록합니다. (구 SetTitle)
        /// </summary>
        private ActionState CreateState(string title, string subTitle = "")
        {
            bool err = GetActionStatus() == ActionStatus.Error;
            if (err)
            {
               
            }

            ActionState state = new ActionState(title);
           

            // --- 타임아웃 로드 로직 (구 SetTitle) ---
            string iniSection = "Timeout";
            if (ACT_TimeOut.Count == 0)
            {
                
                // DEPENDENCY: CIni ini = new CIni("ActionTimeout"); ...
            }
            if (ACT_TimeOut.ContainsKey(state.Title) == false)
            {
                // DEPENDENCY: CIni ini = new CIni("ActionTimeout"); ...
                ACT_TimeOut.TryAdd(state.Title, 10); // 기본값 10
            }
            state.Timeout = ACT_TimeOut[state.Title];
            // --- ---

            if (!this.ACT_STATUS.TryAdd(state.Uid, state))
            {
                // DEPENDENCY: Log.Instance.Error($"Action 실행 실패: {state.Title}");
            }

            // DEPENDENCY: TimeCheck.start(state.Title.ToString());

            return state;
        }

        /// <summary>
        /// 비동기 작업을 타임아웃과 함께 실행합니다. (구 DoWork)
        /// </summary>
        private async Task DoWork(Func<Task> action, System.Action timeoutAction, TimeSpan timeout, CancellationTokenSource cancel)
        {
            if (cancel.IsCancellationRequested) return;

            var task = Task.Run(action, cancel.Token);
            var delayTask = Task.Delay(timeout, cancel.Token);

            var completedTask = await Task.WhenAny(task, delayTask);

            if (completedTask == delayTask)
            {
                // Timeout
                cancel.Cancel(); // 진행 중인 action 취소
                timeoutAction();
            }
            // 'task'가 먼저 완료되면 (정상 종료) 아무것도 하지 않음
        }

        /// <summary>
        /// (신규) Step 기반 Action 실행을 위한 템플릿 메서드
        /// </summary>
        /// <param name="title">Action 제목</param>
        /// <param name="stepNames">Step 이름 목록</param>
        /// <param name="stepLogic">
        /// [입력] (Context, 현재 Step이름), 
        /// [출력] (다음 Step의 인덱스)를 반환하는 로직
        /// </param>
        /// <returns></returns>
        public async Task<ActionStatus> ExecuteAction(string title, List<string> stepNames, Func<ActionState, string, Task<int>> stepLogic)
        {
            // 1. Context 생성 (구 SetTitle)
            var context = CreateState(title);
            if (context.Status != ActionStatus.Running) return context.Status;

            int currentStepIndex = 0;

            // 2. DoWork를 사용한 실행 (타임아웃 래퍼)
            await DoWork(
                async () => // 실행할 Action (State Machine)
                {
                    while (context.Status == ActionStatus.Running)
                    {
                        if (context.cancellatinSource.IsCancellationRequested) return;

                        if (currentStepIndex < 0 || currentStepIndex >= stepNames.Count)
                        {                          
                            context.Status = ActionStatus.Error;
                            return;
                        }

                        // 현재 Step 정보 업데이트
                        string currentStepName = stepNames[currentStepIndex];
                        context.StepName = currentStepName;
                        context.StepIndex = currentStepIndex;

                        // Step 로직 실행
                        int nextStepIndex = await stepLogic(context, currentStepName);

                        if (context.Status != ActionStatus.Running) return; 

                        // 4. 다음 Step으로 이동
                        currentStepIndex = nextStepIndex;
                    }
                },
                () => // 타임아웃 시 실행할 Action
                {
                    context.Status = ActionStatus.Timeout;
             
                             
                },
                TimeSpan.FromSeconds(context.Timeout), // 타임아웃 시간
                context.cancellatinSource // 취소 토큰
            );

            //실행 결과
            if (context.Status == ActionStatus.Error || context.Status == ActionStatus.Timeout)
            {
             
                if (context.Status == ActionStatus.Timeout)                   
                    PopupAlarm(ErrorList.ACT_TIMEOUT, L("Action {0} step:{1}", context.Title, context.StepName));

               
                //Timeout이 아닌 내부 알람은 action 안에서 처리 및 알람 띄워야 함                               
                //    PopupAlarm(ErrorList.ACT_ERROR, $"Action {context.Title} step:{context.StepName}"); // 여기서 timeout 아닌 알람 띄우지 말것

                // 1. 시퀀스 매니저 접근
                var seqManager = EQ.Core.Service.SeqManager.Instance.Seq;

                // 2. 현재 액션을 호출한 시퀀스 이름 파싱 (Manual 호출일 수 있으므로 TryParse 사용)
                SEQ.SeqName callerSeqName;
                bool isSequenceCall = Enum.TryParse(context.CallSequenceName, out callerSeqName);

                // 3. 전체 시퀀스를 순회하며 상태 변경
                foreach (SEQ.SeqName seqName in Enum.GetValues(typeof(SEQ.SeqName)))
                {
                    var seq = seqManager.GetSequence(seqName);
                    if (seq == null) continue;

                    // Case A: 에러를 발생시킨 당사자 시퀀스 -> ERROR 상태로 변경
                    if (isSequenceCall && seqName == callerSeqName)
                    {
                        if (seq._Status != SeqStatus.ERROR)
                        {
                            seq._Status = SeqStatus.ERROR;
                            Log.Instance.Error($"[System] Sequence '{seqName}' changed to ERROR due to Action Fail.");
                        }
                    }
                    // Case B: 에러와 무관하지만 현재 돌고 있는 다른 시퀀스 -> STOPPING (안전 정지) 요청
                    else
                    {
                        if (seq._Status == SeqStatus.RUN)
                        {
                            seq._Status = SeqStatus.SEQ_STOPPING;
                            Log.Instance.Warning($"[System] Sequence '{seqName}' requested to STOP due to System Error.");
                        }
                    }
                }

               
            }

            return context.Status;
        }

        #endregion
    }
}
