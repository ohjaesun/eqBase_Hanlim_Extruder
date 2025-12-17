using System;
using System.Collections.Generic;

namespace EQ.Core.Act.Composition.Extrusion.Utils
{
    /// <summary>
    /// 순수 PID 제어 알고리즘 구현 (데드타임 보상 포함)
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

        // 데드타임 보상용 지연 버퍼
        private Queue<double> _delayBuffer;
        private int _delayBufferSize;
        private bool _useDelayCompensation;
        private readonly object _bufferLock = new object(); // 스레드 안전성을 위한 lock

        public PidController()
        {
            _kp = 1.0;
            _ki = 0.0;
            _kd = 0.0;
            _minOutput = double.MinValue;
            _maxOutput = double.MaxValue;
            _integral = 0.0;
            _previousError = 0.0;
            _delayBuffer = new Queue<double>();
            _delayBufferSize = 0;
            _useDelayCompensation = false;
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
        /// 데드타임 보상용 지연 버퍼 설정
        /// </summary>
        /// <param name="bufferSize">지연 버퍼 크기 (6~15, 0이면 보상 비활성화)</param>
        public void SetDelayCompensation(int bufferSize)
        {
            lock (_bufferLock)
            {
                _delayBufferSize = bufferSize;
                _useDelayCompensation = bufferSize > 0;
                _delayBuffer.Clear();
            }
        }

        /// <summary>
        /// PID 제어 계산 (데드타임 보상 포함)
        /// </summary>
        /// <param name="setpoint">목표값 (SV)</param>
        /// <param name="processValue">현재값 (PV)</param>
        /// <returns>제어 출력값 (CV)</returns>
        public double Compute(double setpoint, double processValue)
        {
            // 데드타임 보상: 측정값을 버퍼에 저장 (스레드 안전)
            double delayedValue = processValue;
            if (_useDelayCompensation)
            {
                lock (_bufferLock)
                {
                    _delayBuffer.Enqueue(processValue);
                    
                    // 버퍼가 가득 차면 가장 오래된 값을 사용
                    if (_delayBuffer.Count > _delayBufferSize)
                    {
                        delayedValue = _delayBuffer.Dequeue();
                    }
                    else
                    {
                        // 버퍼가 아직 채워지지 않으면 현재값 사용
                        delayedValue = processValue;
                    }
                }
            }

            // 1. 오차 계산 (지연된 측정값 사용)
            double error = setpoint - delayedValue;

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
            lock (_bufferLock)
            {
                _delayBuffer.Clear();
            }
        }
    }
}
