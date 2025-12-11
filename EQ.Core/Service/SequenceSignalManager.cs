using EQ.Common.Logs;
using EQ.Core.Act;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace EQ.Core.Service
{
    /// <summary>
    /// 신호 제어 권한을 증명하는 토큰
    /// </summary>
    public class SignalTriggerToken
    {
        internal string Name { get; }
        internal SignalTriggerToken(string name) => Name = name;
    }

    public class SequenceSignalManager
    {
        private static readonly Lazy<SequenceSignalManager> _instance = new Lazy<SequenceSignalManager>(() => new SequenceSignalManager());
        public static SequenceSignalManager Instance => _instance.Value;

        private class SignalData
        {
            public TaskCompletionSource<bool> Tcs { get; set; }
            public SignalTriggerToken Token { get; set; }
        }

        private readonly ConcurrentDictionary<string, SignalData> _signals = new();

        private SequenceSignalManager() { }

        // --- [1] 신호 등록 (토큰 발급) ---
        public SignalTriggerToken Register(string signalName)
        {
            var token = new SignalTriggerToken(signalName);
            var data = new SignalData
            {
                Tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously),
                Token = token
            };

            // 이미 있으면 기존 토큰 반환 (또는 에러 처리)
            if (!_signals.TryAdd(signalName, data))
            {
                Log.Instance.Warning($"[SignalManager] 이미 등록된 신호: {signalName}");
                return _signals[signalName].Token;
            }

            return token;
        }

        // --- [2] 대기 (Wait) ---
        public Task WaitAsync(string signalName)
        {
            if (_signals.TryGetValue(signalName, out var data))
            {
                return data.Tcs.Task;
            }
            // 신호가 없을 경우에 대한 정책 (무한대기 or 에러)
                      
            Log.Instance.Error($"[SignalManager] 미등록 신호 대기 요청: {signalName}");
            return Task.FromException(new Exception($"Signal '{signalName}' not found."));
        }

        // --- [3] 신호 켜기 (Set) - 토큰 필요 ---
        public bool Set(SignalTriggerToken token)
        {
            if (token == null) return false;

            if (_signals.TryGetValue(token.Name, out var data))
            {
                if (data.Token == token) // 보안 검사
                {
                    bool changed = data.Tcs.TrySetResult(true);
                    if (changed) Log.Instance.Info($"[Signal] ON -> {token.Name}");
                    return changed;
                }
                else
                {
                    Log.Instance.Error($"[Signal] 권한 없음 (Token 불일치): {token.Name}");
                }
            }
            return false;
        }

        // --- [4] 리셋 (Reset) - 토큰 필요 ---
        public void Reset(SignalTriggerToken token)
        {
            if (token == null) return;

            if (_signals.TryGetValue(token.Name, out var data) && data.Token == token)
            {
                if (data.Tcs.Task.IsCompleted)
                {
                    data.Tcs = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
                    Log.Instance.Info($"[Signal] Reset -> {token.Name}");
                }
            }
        }
    }
}