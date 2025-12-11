using EQ.Common.Logs;
using EQ.Domain.Interface;
using System;
using System.Timers;
using Timer = System.Timers.Timer; // 타이머 사용

namespace EQ.Infra.Mock
{
    public class MockTempController : ITemperatureController
    {
        private readonly string _name;

        // 상태 변수
        private double _currentTemp; // PV
        private double _targetTemp;  // SV
        private bool _isRunning;     // Run/Stop

        // 시뮬레이션용 타이머
        private readonly Timer _simTimer;
        private readonly Random _rnd = new Random();

        // 물리 상수 (시뮬레이션 속도 조절)
        private const double AMBIENT_TEMP = 25.0; // 상온
        private const double HEATING_RATE = 0.8;  // 틱당 상승 온도
        private const double COOLING_RATE = 0.3;  // 틱당 하강 온도 (자연 냉각)
        private const double NOISE_RANGE = 0.1;   // 센서 노이즈 범위

        public MockTempController(string name, double initialTemp = 25.0)
        {
            _name = name;
            _currentTemp = initialTemp;
            _targetTemp = initialTemp;
            _isRunning = false;

            // 0.5초마다 온도 변화 시뮬레이션
            _simTimer = new Timer(500);
            _simTimer.Elapsed += SimulatePhysics;
            _simTimer.Start();
        }

        // --- ITemperatureController 구현 ---

        public double ReadPV()
        {
            // 실제 센서처럼 약간의 노이즈를 섞어서 반환
            double noise = (_rnd.NextDouble() * NOISE_RANGE * 2) - NOISE_RANGE;
            return Math.Round(_currentTemp + noise, 1);
        }

        public double ReadSV()
        {
            return Math.Round(_targetTemp, 1);
        }

        public bool IsRunning()
        {
            return _isRunning;
        }

        public void WriteSV(double value)
        {
            _targetTemp = value;
            Log.Instance.Info($"[MockTemp:{_name}] SV 변경 -> {value:F1}");
        }

        public void SetRun(bool run)
        {
            _isRunning = run;
            Log.Instance.Info($"[MockTemp:{_name}] Control -> {(run ? "RUN" : "STOP")}");
        }

        // --- 물리 엔진 시뮬레이션 ---

        private void SimulatePhysics(object sender, ElapsedEventArgs e)
        {
            double target = _isRunning ? _targetTemp : AMBIENT_TEMP;
            double diff = target - _currentTemp;

            // 목표 온도에 거의 도달했으면 유지 (미세 진동)
            if (Math.Abs(diff) < 0.5)
            {
                _currentTemp = target + ((_rnd.NextDouble() * 0.2) - 0.1);
            }
            else if (diff > 0)
            {
                // 가열 (Heating)
                _currentTemp += HEATING_RATE;
                if (_currentTemp > target) _currentTemp = target; // 오버슈트 방지
            }
            else
            {
                // 냉각 (Cooling) - Run 상태에서 목표를 낮췄거나, Stop 상태일 때
                _currentTemp -= COOLING_RATE;
                if (_currentTemp < target) _currentTemp = target;
            }
        }
    }
}