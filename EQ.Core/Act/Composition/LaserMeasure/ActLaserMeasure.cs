using EQ.Common.Logs;
using EQ.Domain.Entities.LaserMeasure;
using EQ.Domain.Enums.LaserMeasure;
using EQ.Domain.Interface.LaserMeasure;
using System.Collections.Concurrent;
using System;

namespace EQ.Core.Act.Composition.LaserMeasure
{
    /// <summary>
    /// 레이저 계측기 비즈니스 로직 모듈
    /// 멀티 채널 관리 및 드라이버 추상화
    /// </summary>
    public class ActLaserMeasure : ActComponent
    {
        #region Fields
        private readonly ConcurrentDictionary<LaserMeasureId, ILaserMeasure> _drivers = new();
        private readonly ConcurrentDictionary<LaserMeasureId, LaserMeasureConfig> _configs = new();
        
        // 백그라운드 폴링 및 캐싱
        private CancellationTokenSource? _pollCts;
        private bool _isPolling;
        private readonly object _cacheLock = new object();
        
        // 캐시된 센서 값 (LaserMeasureId별)
        private readonly Dictionary<LaserMeasureId, double> _cachedValues = new();
        
        // 이동평균 버퍼
        private readonly Dictionary<LaserMeasureId, Queue<double>> _movingAverageBuffers = new();
        private int _movingAverageCount = 20; // 기본값
        private int _samplingIntervalMs = 50; // 기본값
        #endregion

        #region Events
        /// <summary>
        /// 측정 완료 이벤트
        /// </summary>
        public event EventHandler<(LaserMeasureId Id, LaserMeasureEventArgs Args)>? OnMeasured;
        #endregion

        #region Constructor
        public ActLaserMeasure(ACT act) : base(act) { }
        #endregion

        #region Driver Registration
        /// <summary>
        /// 드라이버 등록
        /// </summary>
        /// <param name="id">계측기 ID</param>
        /// <param name="driver">ILaserMeasure 구현체</param>
        /// <param name="config">설정</param>
        public bool RegisterDriver(LaserMeasureId id, ILaserMeasure driver, LaserMeasureConfig config)
        {
            if (_drivers.ContainsKey(id))
            {
                Log.Instance.Warning(string.Format("ActLaserMeasure: ID {0}는 이미 등록되어 있습니다.", id));
                return false;
            }

            if (!driver.Init(config))
            {
                Log.Instance.Error(string.Format("ActLaserMeasure: ID {0} 드라이버 초기화 실패", id));
                return false;
            }

            driver.OnMeasured += (sender, args) =>
            {
                OnMeasured?.Invoke(this, (id, args));
            };

            _drivers[id] = driver;
            _configs[id] = config;

            Log.Instance.Info(string.Format("ActLaserMeasure: ID {0} ({1}) 등록 완료", id, config.Name));
            return true;
        }

        /// <summary>
        /// 드라이버 해제
        /// </summary>
        /// <param name="id">계측기 ID</param>
        public void UnregisterDriver(LaserMeasureId id)
        {
            if (_drivers.TryRemove(id, out var driver))
            {
                driver.Dispose();
                _configs.TryRemove(id, out _);
                Log.Instance.Info(string.Format("ActLaserMeasure: ID {0} 해제", id));
            }
        }

        /// <summary>
        /// 드라이버 조회
        /// </summary>
        public ILaserMeasure? GetDriver(LaserMeasureId id)
        {
            return _drivers.GetValueOrDefault(id);
        }

        /// <summary>
        /// 등록된 모든 ID 조회
        /// </summary>
        public IEnumerable<LaserMeasureId> GetRegisteredIds()
        {
            return _drivers.Keys;
        }
        #endregion

        #region Measurement
        /// <summary>
        /// 단발 측정
        /// </summary>
        public async Task<double> MeasureAsync(LaserMeasureId id, int channelId = 0)
        {
            if (!_drivers.TryGetValue(id, out var driver))
            {
                throw new InvalidOperationException(string.Format("ID {0}가 등록되지 않았습니다.", id));
            }

            return await driver.MeasureAsync(channelId);
        }

        /// <summary>
        /// 마지막 측정 값 조회
        /// </summary>
        public double GetLastValue(LaserMeasureId id, int channelId = 0)
        {
            if (_drivers.TryGetValue(id, out var driver))
            {
                return driver.GetLastValue(channelId);
            }
            return 0.0;
        }
        #endregion

        #region Continuous Measurement
        /// <summary>
        /// 연속 측정 시작
        /// </summary>
        public void StartContinuous(LaserMeasureId id, int channelId = 0, int intervalMs = 100)
        {
            if (!_drivers.TryGetValue(id, out var driver))
            {
                throw new InvalidOperationException(string.Format("ID {0}가 등록되지 않았습니다.", id));
            }

            if (!driver.SupportsContinuous)
            {
                throw new NotSupportedException(string.Format("ID {0}는 연속 측정을 지원하지 않습니다.", id));
            }

            driver.StartContinuous(channelId, intervalMs);
        }

        /// <summary>
        /// 연속 측정 정지
        /// </summary>
        public void StopContinuous(LaserMeasureId id, int channelId = 0)
        {
            if (_drivers.TryGetValue(id, out var driver))
            {
                driver.StopContinuous(channelId);
            }
        }

        /// <summary>
        /// 연속 측정 실행 여부
        /// </summary>
        public bool IsContinuousRunning(LaserMeasureId id, int channelId = 0)
        {
            if (_drivers.TryGetValue(id, out var driver))
            {
                return driver.IsContinuousRunning(channelId);
            }
            return false;
        }

        /// <summary>
        /// 연속 측정 지원 여부
        /// </summary>
        public bool SupportsContinuous(LaserMeasureId id)
        {
            if (_drivers.TryGetValue(id, out var driver))
            {
                return driver.SupportsContinuous;
            }
            return false;
        }
        #endregion

        #region Status
        /// <summary>
        /// 연결 상태 조회
        /// </summary>
        public bool IsConnected(LaserMeasureId id)
        {
            if (_drivers.TryGetValue(id, out var driver))
            {
                return driver.IsConnected;
            }
            return false;
        }

        /// <summary>
        /// 설정 조회
        /// </summary>
        public LaserMeasureConfig? GetConfig(LaserMeasureId id)
        {
            return _configs.GetValueOrDefault(id);
        }
        #endregion

        #region Background Polling
        /// <summary>
        /// 백그라운드 폴링 시작 (모든 등록된 센서)
        /// </summary>
        /// <param name="samplingMs">샘플링 주기 (ms)</param>
        /// <param name="movingAverageCount">이동평균 개수</param>
        public void StartPolling(int samplingMs = 50, int movingAverageCount = 20)
        {
            if (_isPolling)
            {
                Log.Instance.Warning("ActLaserMeasure: 이미 폴링 중입니다.");
                return;
            }

            _samplingIntervalMs = samplingMs;
            _movingAverageCount = movingAverageCount;
            _isPolling = true;
            _pollCts = new CancellationTokenSource();

            // 등록된 모든 센서에 대해 버퍼 초기화
            lock (_cacheLock)
            {
                foreach (var id in _drivers.Keys)
                {
                    if (!_movingAverageBuffers.ContainsKey(id))
                    {
                        _movingAverageBuffers[id] = new Queue<double>();
                    }
                    if (!_cachedValues.ContainsKey(id))
                    {
                        _cachedValues[id] = 0.0;
                    }
                }
            }

            _ = Task.Run(async () => await PollingLoopAsync(_pollCts.Token));
            Log.Instance.Info($"ActLaserMeasure: 폴링 시작 - {samplingMs}ms 주기, {movingAverageCount}개 이동평균");
        }

        /// <summary>
        /// 백그라운드 폴링 정지
        /// </summary>
        public void StopPolling()
        {
            if (!_isPolling) return;

            _isPolling = false;
            _pollCts?.Cancel();
            _pollCts?.Dispose();
            _pollCts = null;

            Log.Instance.Info("ActLaserMeasure: 폴링 정지");
        }

        /// <summary>
        /// 폴링 루프 (백그라운드 Task)
        /// </summary>
        private async Task PollingLoopAsync(CancellationToken token)
        {
            while (_isPolling && !token.IsCancellationRequested)
            {
                try
                {
                    // 등록된 모든 센서에 대해 측정
                    foreach (var kvp in _drivers)
                    {
                        var id = kvp.Key;
                        var driver = kvp.Value;

                        try
                        {
                            // 센서 값 읽기 (channelId 0 기본)
                            double rawValue = await driver.MeasureAsync(0);

                            // 이동평균 적용 및 캐싱
                            lock (_cacheLock)
                            {
                                if (!_movingAverageBuffers.ContainsKey(id))
                                    _movingAverageBuffers[id] = new Queue<double>();

                                var buffer = _movingAverageBuffers[id];
                                buffer.Enqueue(rawValue);

                                // 버퍼 크기 유지
                                while (buffer.Count > _movingAverageCount)
                                    buffer.Dequeue();

                                // 평균 계산 및 캐시 업데이트
                                if (buffer.Count > 0)
                                {
                                    _cachedValues[id] = buffer.Average();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Instance.Error($"ActLaserMeasure: ID {id} 폴링 오류 - {ex.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"ActLaserMeasure: 폴링 루프 오류 - {ex.Message}");
                }

                // 다음 샘플링까지 대기
                try
                {
                    await Task.Delay(_samplingIntervalMs, token);
                }
                catch (TaskCanceledException)
                {
                    // 정상 종료
                    break;
                }
            }
        }

        /// <summary>
        /// 캐시된 값 조회 (즉시 반환, 통신 지연 없음)
        /// </summary>
        /// <param name="id">센서 ID</param>
        /// <returns>캐시된 측정값 (이동평균 적용됨)</returns>
        public double GetCachedValue(LaserMeasureId id)
        {
            lock (_cacheLock)
            {
                return _cachedValues.GetValueOrDefault(id, 0.0);
            }
        }

        /// <summary>
        /// 폴링 실행 여부
        /// </summary>
        public bool IsPolling => _isPolling;
        #endregion

        #region Dispose
        public void Dispose()
        {
            // 폴링 정지
            StopPolling();

            foreach (var driver in _drivers.Values)
            {
                driver.Dispose();
            }
            _drivers.Clear();
            _configs.Clear();
            _cachedValues.Clear();
            _movingAverageBuffers.Clear();
        }
        #endregion
    }
}
