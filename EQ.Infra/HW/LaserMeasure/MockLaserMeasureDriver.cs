using EQ.Common.Logs;
using EQ.Domain.Entities.LaserMeasure;
using EQ.Domain.Interface.LaserMeasure;
using System.Collections.Concurrent;

namespace EQ.Infra.LaserMeasure
{
    /// <summary>
    /// 레이저 계측기 Mock 드라이버 (시뮬레이션용)
    /// 랜덤 값을 생성하여 테스트 환경에서 사용
    /// </summary>
    public class MockLaserMeasureDriver : ILaserMeasure
    {
        #region Fields
        private LaserMeasureConfig? _config;
        private bool _isConnected = false;
        private bool _disposed = false;
        private readonly Random _random = new();

        private readonly ConcurrentDictionary<int, double> _lastValues = new();
        private readonly ConcurrentDictionary<int, System.Timers.Timer> _continuousTimers = new();

        // Mock 설정
        private double _baseValue = 10.0;       // 기본 측정값 (mm)
        private double _noiseRange = 0.5;       // 노이즈 범위 (±mm)
        private int _measureDelayMs = 50;       // 측정 지연 시간 (ms)
        #endregion

        #region Properties
        public bool IsConnected => _isConnected;
        public bool SupportsContinuous => true;

        /// <summary>
        /// 기본 측정값 설정 (mm)
        /// </summary>
        public double BaseValue
        {
            get => _baseValue;
            set => _baseValue = value;
        }

        /// <summary>
        /// 노이즈 범위 설정 (±mm)
        /// </summary>
        public double NoiseRange
        {
            get => _noiseRange;
            set => _noiseRange = value;
        }
        #endregion

        #region Events
        public event EventHandler<LaserMeasureEventArgs>? OnMeasured;
        #endregion

        #region Initialization
        public bool Init(LaserMeasureConfig config)
        {
            try
            {
                _config = config;
                _isConnected = true;

                Log.Instance.Info(string.Format("MockLaserMeasureDriver: {0} 초기화 완료 (시뮬레이션 모드)", config.Name));
                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Error(string.Format("MockLaserMeasureDriver: 초기화 실패 - {0}", ex.Message));
                return false;
            }
        }

        public void Close()
        {
            StopAllContinuous();
            _isConnected = false;
            Log.Instance.Info("MockLaserMeasureDriver: 종료");
        }
        #endregion

        #region Measurement
        public async Task<double> MeasureAsync(int channelId = 0)
        {
            if (!_isConnected)
            {
                throw new InvalidOperationException("드라이버가 초기화되지 않았습니다.");
            }

            // 측정 지연 시뮬레이션
            await Task.Delay(_measureDelayMs);

            // 랜덤 노이즈 추가
            double noise = (_random.NextDouble() - 0.5) * 2 * _noiseRange;
            double value = _baseValue + noise;

            _lastValues[channelId] = value;
            OnMeasured?.Invoke(this, new LaserMeasureEventArgs(channelId, value));

            return value;
        }

        public double GetLastValue(int channelId = 0)
        {
            return _lastValues.GetValueOrDefault(channelId, _baseValue);
        }

        /// <summary>
        /// 특정 값을 강제로 설정 (테스트용)
        /// </summary>
        public void SetMockValue(int channelId, double value)
        {
            _lastValues[channelId] = value;
            OnMeasured?.Invoke(this, new LaserMeasureEventArgs(channelId, value));
        }

        /// <summary>
        /// 에러 상태 시뮬레이션 (테스트용)
        /// </summary>
        public void SimulateError(int channelId, string errorMessage)
        {
            OnMeasured?.Invoke(this, new LaserMeasureEventArgs(channelId, 0, true, errorMessage));
        }
        #endregion

        #region Continuous Measurement
        public void StartContinuous(int channelId = 0, int intervalMs = 100)
        {
            if (_continuousTimers.ContainsKey(channelId))
            {
                return;
            }

            var timer = new System.Timers.Timer(intervalMs);
            timer.Elapsed += async (s, e) =>
            {
                try
                {
                    await MeasureAsync(channelId);
                }
                catch (Exception ex)
                {
                    OnMeasured?.Invoke(this, new LaserMeasureEventArgs(channelId, 0, true, ex.Message));
                }
            };

            timer.AutoReset = true;
            timer.Start();
            _continuousTimers[channelId] = timer;

            Log.Instance.Info(string.Format("MockLaserMeasureDriver: 연속 측정 시작 (Channel: {0}, Interval: {1}ms)", channelId, intervalMs));
        }

        public void StopContinuous(int channelId = 0)
        {
            if (_continuousTimers.TryRemove(channelId, out var timer))
            {
                timer.Stop();
                timer.Dispose();
                Log.Instance.Info(string.Format("MockLaserMeasureDriver: 연속 측정 정지 (Channel: {0})", channelId));
            }
        }

        public bool IsContinuousRunning(int channelId = 0)
        {
            return _continuousTimers.ContainsKey(channelId);
        }

        private void StopAllContinuous()
        {
            foreach (var kvp in _continuousTimers)
            {
                kvp.Value.Stop();
                kvp.Value.Dispose();
            }
            _continuousTimers.Clear();
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            if (!_disposed)
            {
                Close();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
