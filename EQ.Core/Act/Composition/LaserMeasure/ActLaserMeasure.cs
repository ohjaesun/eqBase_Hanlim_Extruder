using EQ.Common.Logs;
using EQ.Domain.Entities.LaserMeasure;
using EQ.Domain.Enums.LaserMeasure;
using EQ.Domain.Interface.LaserMeasure;
using System.Collections.Concurrent;

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

        #region Dispose
        public void Dispose()
        {
            foreach (var driver in _drivers.Values)
            {
                driver.Dispose();
            }
            _drivers.Clear();
            _configs.Clear();
        }
        #endregion
    }
}
