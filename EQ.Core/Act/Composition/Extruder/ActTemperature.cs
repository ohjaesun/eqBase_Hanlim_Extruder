using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Enums;
using EQ.Domain.Interface;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EQ.Core.Act.Composition
{
    /// <summary>
    /// 온도 폴링 데이터 구조체
    /// </summary>
    public struct TemperatureData
    {
        /// <summary>온도 컨트롤러 이름</summary>
        public string Name { get; init; }
        
        /// <summary>동작 상태 (Run/Stop)</summary>
        public bool IsRunning { get; init; }
        
        /// <summary>현재 온도 (PV)</summary>
        public double CurrentTemperature { get; init; }
        
        /// <summary>설정 온도 (SV)</summary>
        public double TargetTemperature { get; init; }
        
        /// <summary>연결 상태</summary>
        public bool IsConnected { get; init; }
    }

    /// <summary>
    /// 여러 온도 컨트롤러(ITemperatureController)를 통합 관리하는 모듈
    /// </summary>
    public class ActTemperature : ActComponent
    {
        // 이름으로 컨트롤러를 찾기 위한 딕셔너리
        private readonly ConcurrentDictionary<string, ITemperatureController> _controllers = new();

        // 캐시된 온도 데이터 저장소
        private readonly ConcurrentDictionary<string, TemperatureData> _cachedData = new();

        // 온도 폴링용
        private CancellationTokenSource _pollingCts;
        private Task _pollingTask;

        /// <summary>
        /// 온도 값이 업데이트될 때 발생하는 이벤트
        /// </summary>
        public event System.Action<TemperatureData> OnTemperatureUpdated;

        public ActTemperature(ACT act) : base(act) { }

        /// <summary>
        /// (FormSplash 등에서 호출) 컨트롤러 등록
        /// </summary>
        public void Register(string name, ITemperatureController controller)
        {
            if (_controllers.TryAdd(name, controller))
            {
                Log.Instance.Info($"[ActTemp] 온도 컨트롤러 등록됨: {name}");
            }
            else
            {
                Log.Instance.Warning($"[ActTemp] 이미 등록된 이름입니다: {name}");
            }
        }
        public void Register(System.Enum zone, ITemperatureController controller)
        {
            Register(zone.ToString(), controller);
        }

        /// <summary>
        /// 이름으로 특정 컨트롤러 가져오기
        /// </summary>
        public ITemperatureController Get(string name)
        {
            if (_controllers.TryGetValue(name, out var ctrl))
            {
                return ctrl;
            }
            Log.Instance.Error($"[ActTemp] 존재하지 않는 컨트롤러 요청: {name}");
            return null;
        }

        public ITemperatureController Get(System.Enum zone)
        {
            return Get(zone.ToString());
        }

        /// <summary>
        /// 등록된 모든 컨트롤러 목록 반환 (일괄 제어용)
        /// </summary>
        public List<ITemperatureController> GetAll()
        {
            return _controllers.Values.ToList();
        }

        /// <summary>
        /// 캐시된 온도 데이터 전체를 조회합니다.
        /// </summary>
        /// <param name="zone">온도 Zone ID</param>
        /// <returns>캐시된 TemperatureData, 없으면 기본값 반환</returns>
        public TemperatureData GetCachedData(TempID zone)
        {
            string name = zone.ToString();
            if (_cachedData.TryGetValue(name, out var data))
            {
                return data;
            }
            Log.Instance.Warning($"[ActTemp] 캐시된 데이터 없음: {name}");
            return new TemperatureData { Name = name, IsConnected = false };
        }

        /// <summary>
        /// 캐시된 현재 온도(PV)를 조회합니다.
        /// </summary>
        /// <param name="zone">온도 Zone ID</param>
        /// <returns>현재 온도 값</returns>
        public double GetCachedPV(TempID zone)
        {
            return GetCachedData(zone).CurrentTemperature;
        }

        /// <summary>
        /// 캐시된 설정 온도(SV)를 조회합니다.
        /// </summary>
        /// <param name="zone">온도 Zone ID</param>
        /// <returns>설정 온도 값</returns>
        public double GetCachedSV(TempID zone)
        {
            return GetCachedData(zone).TargetTemperature;
        }

        /// <summary>
        /// 캐시된 동작 상태를 조회합니다.
        /// </summary>
        /// <param name="zone">온도 Zone ID</param>
        /// <returns>동작 중이면 true, 아니면 false</returns>
        public bool GetCachedIsRunning(TempID zone)
        {
            return GetCachedData(zone).IsRunning;
        }

        /// <summary>
        /// 모든 캐시된 온도 데이터를 조회합니다.
        /// </summary>
        /// <returns>캐시된 모든 TemperatureData 리스트</returns>
        public List<TemperatureData> GetAllCachedData()
        {
            return _cachedData.Values.ToList();
        }

        /// <summary>
        /// 모든 등록된 온도 컨트롤러를 주기적으로 폴링하여 온도 값을 읽습니다.
        /// </summary>
        /// <param name="samplingTimeMs">샘플링 주기 (밀리초)</param>
        public void StartPolling(int samplingTimeMs = 1000)
        {
            StopPolling(); // 기존 폴링이 있다면 중지

            _pollingCts = new CancellationTokenSource();
            _pollingTask = Task.Run(async () =>
            {
                var token = _pollingCts.Token;
                Log.Instance.Info($"[ActTemp] 온도 폴링 시작 (샘플링 주기: {samplingTimeMs}ms)");

                while (!token.IsCancellationRequested)
                {
                    try
                    {
                        // 모든 등록된 컨트롤러의 온도를 읽음
                        foreach (var kvp in _controllers)
                        {
                            string name = kvp.Key;
                            ITemperatureController controller = kvp.Value;

                            try
                            {
                                var data = new TemperatureData
                                {
                                    Name = name,
                                    IsRunning = controller.IsRunning(),
                                    CurrentTemperature = controller.ReadPV(),
                                    TargetTemperature = controller.ReadSV(),
                                    IsConnected = true // 읽기 성공 시 연결됨
                                };

                                // 캐시에 저장
                                _cachedData[name] = data;

                                OnTemperatureUpdated?.Invoke(data);

                                
                            }
                            catch (System.Exception ex)
                            {
                                Log.Instance.Error($"[ActTemp] {name} 온도 읽기 실패: {ex.Message}");
                                
                                // 에러 발생 시에도 이벤트 발생 (연결 끊김 상태로)
                                var errorData = new TemperatureData
                                {
                                    Name = name,
                                    IsRunning = false,
                                    CurrentTemperature = 0,
                                    TargetTemperature = 0,
                                    IsConnected = false
                                };

                                // 캐시에 저장
                                _cachedData[name] = errorData;

                                OnTemperatureUpdated?.Invoke(errorData);
                            }
                        }                       

                        await Task.Delay(samplingTimeMs, token);
                    }
                    catch (TaskCanceledException)
                    {
                        break;
                    }
                }

                Log.Instance.Info("[ActTemp] 온도 폴링 종료");
            }, _pollingCts.Token);
        }

        /// <summary>
        /// 온도 폴링을 중지합니다.
        /// </summary>
        public void StopPolling()
        {
            if (_pollingCts != null)
            {
                _pollingCts.Cancel();
                _pollingTask?.Wait(2000); // 최대 2초 대기
                _pollingCts?.Dispose();
                _pollingCts = null;
                _pollingTask = null;
            }
        }
    }
}
