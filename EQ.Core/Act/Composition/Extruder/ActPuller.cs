using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Core.Act.Composition.Extrusion.Utils;
using EQ.Domain.Enums;
using EQ.Domain.Entities.Extruder;
using System;
using System.Threading;
using System.Threading.Tasks;
using static EQ.Core.Globals;

namespace EQ.Core.Act.Composition.Extruder
{
    /// <summary>
    /// 인취기 제어 및 PID 기반 직경 제어
    /// </summary>
    public class ActPuller : ActComponent
    {
        private readonly PidController _pid;
        private bool _isControlling;
        private CancellationTokenSource _controlCts;

        public ActPuller(ACT act) : base(act)
        {          
            _pid = new PidController();
        }

        /// <summary>
        /// 인취기 가동 및 PID 직경 제어 시작
        /// </summary>
        public async Task<ActionStatus> StartPullingWithControlAsync(ExtruderRecipe recipe)
        {
            var stepString = new System.Collections.Generic.List<string> { "Start", "InitPID", "StartMotor", "StartControl", "End" };

            return await _act.ExecuteAction(
                title: "StartPullingWithControl",
                stepNames: stepString,
                stepLogic: async (context, stepName) =>
                {
                    int nextStep = context.StepIndex;

                    switch (stepName)
                    {
                        case "Start":
                            Log.Instance.Info(L("인취 제어 시작 - 목표 직경: {0}mm", recipe.Diameter));
                            nextStep++;
                            break;

                        case "InitPID":
                            // PID 파라미터 설정
                            _pid.SetGains(recipe.Kp, recipe.Ki, recipe.Kd);
                            _pid.SetLimits(recipe.MINCV, recipe.MAXCV);
                            _pid.Reset();
                            
                            Log.Instance.Info(L("PID 초기화 완료 - Kp:{0}, Ki:{1}, Kd:{2}", recipe.Kp, recipe.Ki, recipe.Kd));
                            nextStep++;
                            break;

                        case "StartMotor":
                            // 기본 속도로 모터 기동
                            // await _act.Motion.JogAsync(MotionID.PULLER_T, recipe.PullerMotorSpeed);
                            await Task.Delay(500);
                            nextStep++;
                            break;

                        case "StartControl":
                            // PID 제어 루프 시작
                            StartControlLoop(recipe);
                            nextStep++;
                            break;

                        case "End":
                            context.Status = ActionStatus.Finished;
                            Log.Instance.Info(L("인취 제어 시작 완료"));
                            break;

                        default:
                            context.Status = ActionStatus.Error;
                            break;
                    }

                    return nextStep;
                }
            );
        }

        /// <summary>
        /// PID 제어 루프 (백그라운드 Task)
        /// </summary>
        private void StartControlLoop(ExtruderRecipe recipe)
        {
            _isControlling = true;
            _controlCts = new CancellationTokenSource();

            _ = Task.Run(async () =>
            {
                var token = _controlCts.Token;

                Log.Instance.Info(L("직경 제어 루프 시작"));

                // PID 제어 주기 (레시피에서 읽어옴, ms 단위)
                int controlIntervalMs = (int)(recipe.PidSamplingTime * 1000);

                while (_isControlling && !token.IsCancellationRequested)
                {
                    try
                    {
                        // 캐시된 레이저 센서 값 사용 (이동평균 적용됨, 즉시 반환)
                        // double currentDia = _act.LaserMeasure.GetCachedValue(LaserMeasureId.Diameter);
                        double currentDia = 0.0; // Placeholder (실제 구현 시 주석 해제)

                        // PID 계산
                        double correction = _pid.Compute(recipe.Diameter, currentDia);

                        // 데드밴드 체크
                        if (Math.Abs(recipe.Diameter - currentDia) > recipe.DB)
                        {
                            double newSpeed = recipe.PullerMotorSpeed + correction;
                            // 미구현: 속도 변경
                            // await _act.Motion.ChangeSpeedAsync(MotionID.PULLER_T, newSpeed);
                        }
                    }
                    catch (Exception ex)
                    {
                      
                        Log.Instance.Error(L("직경 제어 오류: {0}", ex.Message));
                    }

                    // 레시피의 PID 샘플링 주기만큼 대기
                    await Task.Delay(controlIntervalMs, token);
                }

                Log.Instance.Info(L("직경 제어 루프 종료"));
            });
        }

        /// <summary>
        /// 인취기 정지 및 PID 제어 중단
        /// </summary>
        public async Task<ActionStatus> StopPullingAsync()
        {
            var stepString = new System.Collections.Generic.List<string> { "Start", "StopControl", "StopMotor", "End" };

            return await _act.ExecuteAction(
                title: "StopPulling",
                stepNames: stepString,
                stepLogic: async (context, stepName) =>
                {
                    int nextStep = context.StepIndex;

                    switch (stepName)
                    {
                        case "Start":
                            Log.Instance.Info(L("인취 정지 시작"));
                            nextStep++;
                            break;

                        case "StopControl":
                            // PID 제어 루프 중지
                            _isControlling = false;
                            _controlCts?.Cancel();
                            await Task.Delay(200);
                            nextStep++;
                            break;

                        case "StopMotor":
                            // 모터 정지
                            // _act.Motion.MoveStop(MotionID.PULLER_T);
                            await Task.Delay(100);
                            nextStep++;
                            break;

                        case "End":
                            context.Status = ActionStatus.Finished;
                            Log.Instance.Info(L("인취 정지 완료"));
                            break;

                        default:
                            context.Status = ActionStatus.Error;
                            break;
                    }

                    return nextStep;
                }
            );
        }
    }
}
