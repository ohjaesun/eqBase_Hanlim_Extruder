using EQ.Common.Logs;
using EQ.Core.Act;
using System.Threading.Tasks;
using static EQ.Core.Globals;

namespace EQ.Core.Act.Composition.Extruder
{
    /// <summary>
    /// 후공정 제어 (선별, 커팅, 카운팅)
    /// </summary>
    public class ActFinishing : ActComponent
    {
        private int _goodCount = 0;
        private int _rejectCount = 0;
        private double _currentLength = 0.0;

        public int GoodCount => _goodCount;
        public int RejectCount => _rejectCount;
        public double CurrentLength => _currentLength;

        public ActFinishing(ACT act) : base(act) { }

        /// <summary>
        ///Good 카운트 증가
        /// </summary>
        public void IncrementGoodCount()
        {
            _goodCount++;
            Log.Instance.Info(L("양품 카운트: {0}", _goodCount));
        }

        /// <summary>
        /// Reject 카운트 증가
        /// </summary>
        public void IncrementRejectCount()
        {
            _rejectCount++;
            Log.Instance.Warning(L("불량품 카운트: {0}", _rejectCount));
        }

        /// <summary>
        /// 카운트 초기화
        /// </summary>
        public void ResetCounts()
        {
            _goodCount = 0;
            _rejectCount = 0;
            Log.Instance.Info(L("카운트 초기화"));
        }

        /// <summary>
        /// 생산 길이 업데이트
        /// </summary>
        public void UpdateLength(double speed, double deltaTime)
        {
            // deltaTime(초) * speed(mm/s) = 길이(mm)
            _currentLength += speed * deltaTime;
        }

        /// <summary>
        /// 길이 초기화
        /// </summary>
        public void ResetLength()
        {
            _currentLength = 0.0;
        }

        /// <summary>
        /// 커팅 및 선별 실행
        /// </summary>
        public async Task<ActionStatus> CutAndSortAsync(bool isGood)
        {
            var stepString = new System.Collections.Generic.List<string> { "Start", "Cut", "Sort", "UpdateCount", "End" };

            return await _act.ExecuteAction(
                title: "CutAndSort",
                stepNames: stepString,
                stepLogic: async (context, stepName) =>
                {
                    int nextStep = context.StepIndex;

                    switch (stepName)
                    {
                        case "Start":
                            Log.Instance.Info(L("커팅 및 선별 시작 - 양품: {0}", isGood));
                            nextStep++;
                            break;

                        case "Cut":
                            // 미구현: 커터 동작
                            // await _act.IO.CutterAsync();
                            await Task.Delay(500);
                            nextStep++;
                            break;

                        case "Sort":
                            if (!isGood)
                            {
                                // 불량품 선별
                                // await _act.IO.SorterRejectAsync();
                                await Task.Delay(500);
                            }
                            nextStep++;
                            break;

                        case "UpdateCount":
                            if (isGood)
                                IncrementGoodCount();
                            else
                                IncrementRejectCount();
                            nextStep++;
                            break;

                        case "End":
                            context.Status = ActionStatus.Finished;
                            Log.Instance.Info(L("커팅 및 선별 완료"));
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
        /// 양품 Bin 가득 참 체크
        /// </summary>
        public bool CheckGoodBinFull(int maxCount)
        {
            if (_goodCount >= maxCount)
            {
                _act.PopupNoti(L("양품 Bin 가득 참"), L("양품 카운트: {0}", _goodCount), Domain.Enums.NotifyType.Warning);
                return true;
            }
            return false;
        }
    }
}
