using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Domain.Enums;
using System.Threading.Tasks;
using static EQ.Core.Globals;

namespace EQ.Core.Act.Composition.Extruder
{
    /// <summary>
    /// 압출기 및 피더 제어
    /// </summary>
    public class ActExtruder : ActComponent
    {
        public ActExtruder(ACT act) : base(act) { }

        /// <summary>
        /// 압출 공정 시작
        /// </summary>
        public async Task<ActionStatus> StartExtrusionAsync(double extruderSpeed, double feederSpeed)
        {
            var stepString = new System.Collections.Generic.List<string> { "Start", "StartExtruder", "StartFeeder", "WaitStable", "End" };

            return await _act.ExecuteAction(
                title: "StartExtrusion",
                stepNames: stepString,
                stepLogic: async (context, stepName) =>
                {
                    int nextStep = context.StepIndex;

                    switch (stepName)
                    {
                        case "Start":
                            Log.Instance.Info(L("압출 공정 시작 - Extruder: {0} rpm, Feeder: {1} rpm", extruderSpeed, feederSpeed));
                            nextStep++;
                            break;

                        case "StartExtruder":
                            // 압출기 모터 기동
                            // await _act.Motion.JogAsync(MotionID.SCREW_T, extruderSpeed);
                            nextStep++;
                            break;

                        case "StartFeeder":
                            // 피더 모터 기동
                            // await _act.Motion.JogAsync(MotionID.FEEDER_T, feederSpeed);
                            nextStep++;
                            break;

                        case "WaitStable":
                            // 안정화 대기
                            await Task.Delay(1000);
                            nextStep++;
                            break;

                        case "End":
                            context.Status = ActionStatus.Finished;
                            Log.Instance.Info(L("압출 공정 시작 완료"));
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
        /// 압출 공정 정지
        /// </summary>
        public async Task<ActionStatus> StopExtrusionAsync()
        {
            var stepString = new System.Collections.Generic.List<string> { "Start", "StopExtruder", "StopFeeder", "End" };

            return await _act.ExecuteAction(
                title: "StopExtrusion",
                stepNames: stepString,
                stepLogic: async (context, stepName) =>
                {
                    int nextStep = context.StepIndex;

                    switch (stepName)
                    {
                        case "Start":
                            Log.Instance.Info(L("압출 공정 정지 시작"));
                            nextStep++;
                            break;

                        case "StopExtruder":
                            // 압출기 모터 정지
                            // _act.Motion.MoveStop(MotionID.SCREW_T);
                            await Task.Delay(100);
                            nextStep++;
                            break;

                        case "StopFeeder":
                            // 피더 모터 정지
                            // _act.Motion.MoveStop(MotionID.FEEDER_T);
                            await Task.Delay(100);
                            nextStep++;
                            break;

                        case "End":
                            context.Status = ActionStatus.Finished;
                            Log.Instance.Info(L("압출 공정 정지 완료"));
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
        /// 토크 모니터링 (정기적으로 호출)
        /// </summary>
        public bool CheckTorqueLimits(double minTorque, double maxTorque)
        {
            // var status = _act.Motion.GetStatus(MotionID.SCREW_T);
            // double currentTorque = status.ActualTorque;
            double currentTorque = 0.0; // Placeholder

            if (currentTorque < minTorque || currentTorque > maxTorque)
            {
                Log.Instance.Warning(L("토크 범위 벗어남 - 현재: {0}, 범위: {1}~{2}", currentTorque, minTorque, maxTorque));
                return false;
            }

            return true;
        }
    }
}
