using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Core.Service; // SignalManager 사용
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Core.Sequence
{
    public enum SeqStatus { STOP, RUN, SEQ_STOPPING, ERROR, TIMEOUT, }

    public interface ISeqInterface
    {
        // SEQ 매니저가 사용할 속성
        SeqStatus _Status { get; set; }
        string _StepString { get; }
        int _Step { get; set; }
        int _StepMax { get; }

        // [New] UI 표시용 신호 상태 (대기중 / 켜짐)
        string _WaitSignalName { get; }
        string _SetSignalName { get; }

        // SEQ 매니저가 호출할 메서드
        Task doSequence();
        Task OnErrorRecovery();
        Task OnStopping();

        // UI 또는 데이터 표시용
        DataTable GetDataTable();
        ConcurrentDictionary<string, Stopwatch> _StepTime { get; }

        // 상태 제어용
        IEnumerable<(string StepName, long Time)> GetSortedStepTimes();
        void _StepTimeAllStop();
        void _StepTimeClear();
    }

    public abstract class AbstractSeqBase<T> : ISeqInterface where T : Enum
    {
        // --- A. 의존성 주입 (공통) ---
        protected readonly SEQ _seq; // SEQ 매니저 접근용
        protected readonly ACT _act; // ACT 기능(Motion/IO) 접근용

        // [New] 신호 관리를 위한 변수들
        private readonly Dictionary<Enum, SignalTriggerToken> _myTokens = new Dictionary<Enum, SignalTriggerToken>();
        private readonly HashSet<string> _activeSetSignals = new HashSet<string>();

        public AbstractSeqBase(SEQ seqManager, ACT actManager)
        {
            _seq = seqManager;
            _act = actManager;
            _Status = SeqStatus.STOP;
            _StepTimes = new ConcurrentDictionary<string, Stopwatch>();
        }

        public abstract Task doSequence();

        public SeqStatus _Status { get; set; }

        protected int _StepIndex;
        public int _Step
        {
            get => _StepIndex;
            set
            {
                _StepIndex = value;
                Step = (T)(object)value; // Enum 타입도 함께 업데이트
            }
        }

        public string _StepString => ((T)(object)_StepIndex).ToString();

        public int _StepMax => Enum.GetNames(typeof(T)).Length;

        // --- B. 인터페이스 구현 (신호 상태 표시) ---

        // 1. 현재 대기 중인 신호 이름
        public string _WaitSignalName { get; private set; } = string.Empty;

        // 2. 현재 내가 켜놓은 신호 목록 (Comma로 구분)
        public string _SetSignalName
        {
            get
            {
                lock (_activeSetSignals)
                    return string.Join(", ", _activeSetSignals);
            }
        }

        // --- C. 공통 스텝(Enum) 관리 기능 ---

        public T Step
        {
            get => (T)(object)_StepIndex;
            set
            {
                _StepIndex = (int)(object)value; // 숫자 인덱스도 함께 업데이트

                // 스텝 변경 시 시간 측정기 재시작
                foreach (var p in _StepTimes)
                    p.Value.Stop();

                var name = ((T)(object)_StepIndex).ToString();
                if (_StepTimes.ContainsKey(name) == false)
                    _StepTimes.TryAdd(name, new Stopwatch());
            }
        }

        // --- D. 공통 시간 관리 기능 ---

        protected ConcurrentDictionary<string, Stopwatch> _StepTimes;
        public ConcurrentDictionary<string, Stopwatch> _StepTime => _StepTimes;

        public IEnumerable<(string StepName, long Time)> GetSortedStepTimes()
        {
            // Enum에 정의된 순서대로 순회
            foreach (T step in Enum.GetValues(typeof(T)))
            {
                string key = step.ToString();

                // 딕셔너리에 해당 스텝의 시간이 기록되어 있다면 반환
                if (_StepTimes.TryGetValue(key, out var stopwatch))
                {
                    yield return (key, stopwatch.ElapsedMilliseconds);
                }
                else
                {
                    // (옵션) 아직 실행 안 된 스텝도 0ms로 찍고 싶다면 아래 주석 해제
                    // yield return (key, 0);
                }
            }
        }
        public void _StepTimeAllStop()
        {
            foreach (var p in _StepTimes) p.Value.Stop();
        }

        public void _StepTimeClear()
        {
            foreach (var p in _StepTimes) p.Value.Reset();
        }

        // --- E. 공통 UI 데이터 기능 ---
        public DataTable GetDataTable()
        {
            var columnName = typeof(T).Name;
            DataTable dt = new DataTable();
            dt.Columns.Add(columnName);
            dt.Columns.Add("Elsp", typeof(string));
            foreach (var p in Enum.GetValues(typeof(T)))
            {
                dt.Rows.Add(p.ToString());
            }
            return dt;
        }

        public virtual async Task OnErrorRecovery()
        {
            Log.Instance.Error($"[{this.GetType().Name}] 시퀀스 에러 발생. 기본 복구 루틴 실행.");
            await Task.CompletedTask;
        }
        public virtual async Task OnStopping()
        {
            Log.Instance.Error($"[{this.GetType().Name}] 시퀀스 에러 발생. 기본 복구 루틴 실행.");
            await Task.CompletedTask;
        }

        // ---------------------------------------------------------
        // F. [New] 신호 동기화 헬퍼 메서드 (Signal Helper)
        // ---------------------------------------------------------

        /// <summary>
        /// [초기화] 시퀀스 생성자에서 호출하여 신호 토큰을 자동 발급받습니다.
        /// </summary>
        protected void InitSignals<TSignal>() where TSignal : Enum
        {
            string seqName = this.GetType().Name; // 예: "Seq01"

            foreach (TSignal sig in Enum.GetValues(typeof(TSignal)))
            {
                // 이름 규칙: "시퀀스명_신호명" (예: Seq01_TrayReady)
                string uniqueName = $"{seqName}_{sig}";

                var token = SequenceSignalManager.Instance.Register(uniqueName);
                if (token != null)
                {
                    _myTokens[sig] = token;
                }
            }
        }

        /// <summary>
        /// [Set] 내 신호를 켭니다. (UI 표시 업데이트 포함)
        /// </summary>
        protected void SetSignal(Enum signal)
        {
            if (_myTokens.TryGetValue(signal, out var token))
            {
                if (SequenceSignalManager.Instance.Set(token))
                {
                    lock (_activeSetSignals) _activeSetSignals.Add(signal.ToString());
                }
            }
            else
            {
                Log.Instance.Error($"[{this.GetType().Name}] SetSignal 실패: 등록되지 않은 신호 {signal}");
            }
        }

        /// <summary>
        /// [Reset] 내 신호를 끕니다. (UI 표시 업데이트 포함)
        /// </summary>
        protected void ResetSignal(Enum signal)
        {
            if (_myTokens.TryGetValue(signal, out var token))
            {
                SequenceSignalManager.Instance.Reset(token);
                lock (_activeSetSignals)
                    _activeSetSignals.Remove(signal.ToString());
            }
        }

        /// <summary>
        /// [Wait] 다른 시퀀스의 신호를 기다립니다. (자동 이름 추론: XXX_Sig -> XXX)
        /// </summary>
        protected async Task WaitSignalAsync(Enum signal)
        {
            // 1. Enum 이름에서 대상 시퀀스 이름 추론
            string enumTypeName = signal.GetType().Name; // 예: "Loader_Sig"
            if (!enumTypeName.EndsWith("_Sig"))
            {
                throw new ArgumentException($"Enum 이름 규칙 위반: {enumTypeName} (XXX_Sig 형식이어야 합니다)");
            }

            string targetSeqName = enumTypeName.Substring(0, enumTypeName.Length - 4); // "_Sig" 제거 -> "Loader"
            string signalName = signal.ToString(); // "TrayReady"
            string uniqueName = $"{targetSeqName}_{signalName}"; // "Loader_TrayReady"

            // 2. UI 업데이트 (대기 시작)
            _WaitSignalName = $"{targetSeqName}.{signalName}";

            try
            {
                // 3. 실제 대기
         //       await SequenceSignalManager.Instance.WaitAsync(uniqueName);
            }
            finally
            {
                // 4. UI 업데이트 (대기 종료)
         //       _WaitSignalName = string.Empty;
            }

            try
            {
                // 3. 신호 대기 Task 생성 (아직 await 하지 않음)
                var signalTask = SequenceSignalManager.Instance.WaitAsync(uniqueName);

                // 4. [핵심] 루프를 돌며 신호와 내 상태를 동시에 체크
                while (!signalTask.IsCompleted)
                {                   
                    if (_Status != SeqStatus.RUN)
                    {
                        // 로그 남기고 예외를 던져서 doSequence를 즉시 탈출
                        Log.Instance.Warning($"[{this.GetType().Name}] 대기 중단 (Status: {_Status})");
                        throw new OperationCanceledException("Sequence Stopped during wait.");
                    }
                   
                    var delayTask = Task.Delay(100);
                    var completedTask = await Task.WhenAny(signalTask, delayTask);
                  
                    if (completedTask == signalTask)
                    {
                        break;
                    }
                }

                // (여기까지 오면 신호를 정상적으로 받은 것)
            }
            finally
            {
                _WaitSignalName = string.Empty;
            }
        }
    }
}