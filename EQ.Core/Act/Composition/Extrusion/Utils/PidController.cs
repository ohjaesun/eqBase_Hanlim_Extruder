using System;

namespace EQ.Core.Act.Composition.Extrusion.Utils
{
    /// <summary>
    /// 순수 PID 제어 알고리즘 구현
    /// </summary>
    public class PidController
    {
        // PID 게인
        private double _kp; // 비례 게인
        private double _ki; // 적분 게인
        private double _kd; // 미분 게인

        // 제어변수(CV) 제한
        private double _minOutput;
        private double _maxOutput;

        // 적분 누적값 및 이전 오차
        private double _integral;
        private double _previousError;

        public PidController()
        {
            _kp = 1.0;
            _ki = 0.0;
            _kd = 0.0;
            _minOutput = double.MinValue;
            _maxOutput = double.MaxValue;
            _integral = 0.0;
            _previousError = 0.0;
        }

        /// <summary>
        /// PID 게인 설정
        /// </summary>
        public void SetGains(double kp, double ki, double kd)
        {
            _kp = kp;
            _ki = ki;
            _kd = kd;
        }

        /// <summary>
        /// 출력 제한값 설정
        /// </summary>
        public void SetLimits(double min, double max)
        {
            _minOutput = min;
            _maxOutput = max;
        }

        /// <summary>
        /// PID 제어 계산
        /// </summary>
        /// <param name="setpoint">목표값 (SV)</param>
        /// <param name="processValue">현재값 (PV)</param>
        /// <returns>제어 출력값 (CV)</returns>
        public double Compute(double setpoint, double processValue)
        {
            // 1. 오차 계산
            double error = setpoint - processValue;

            // 2. 비례항 (P)
            double proportional = _kp * error;

            // 3. 적분항 (I)
            _integral += error;
            double integral = _ki * _integral;

            // 4. 미분항 (D)
            double derivative = _kd * (error - _previousError);
            _previousError = error;

            // 5. PID 출력 계산
            double output = proportional + integral + derivative;

            // 6. 출력 제한 (Anti-windup)
            if (output > _maxOutput)
            {
                output = _maxOutput;
                // 적분 항 제한 (Anti-windup)
                _integral -= error;
            }
            else if (output < _minOutput)
            {
                output = _minOutput;
                // 적분 항 제한 (Anti-windup)
                _integral -= error;
            }

            return output;
        }

        /// <summary>
        /// PID 상태 초기화
        /// </summary>
        public void Reset()
        {
            _integral = 0.0;
            _previousError = 0.0;
        }
    }
}
