using EQ.Core.Act;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Core.Act
{
    /*
     * Action은 단위 동작을 정의하는 클래스입니다.
     * 이는 시퀀스처럼 step 기반으로 동작하나 기능을 좀더 세분화하고 재사용성을 높이기 위해 설계되었습니다.
     * 아래는 Action 클래스의 샘플 구현입니다.
     * 
     * 내용 구현 후 ACT.cs 파일의 ACT 클래스 내에 해당 Action 클래스를 멤버로 추가하여 사용합니다.
     */

    public class ActSample : ActComponent  //1. 'ActComponent' 상속 하고 클래스 이름 수정
    {
        public ActSample(ACT act) : base(act) { } 

        public async Task<ActionStatus> SampleAsync()
        {
            var stepString = new List<string> { "Start", "Step1", "Step2", "Step3", "End" };

            return await _act.ExecuteAction(
                title: $"SampleAsync",       // 2. Action Title 지정 - 함수명 그대로 넣어주세요 - 로그등에서 사용됩니다.
                stepNames: stepString,
                stepLogic: async (context, stepName) =>
                {
                    int nextStep = context.StepIndex;

                    switch (stepName)
                    {
                        case "Start":
                            
                            nextStep++; // 다음 스텝으로 이동
                            break;

                        case "Step1":
                            nextStep++;                           
                            break;

                        case "Step2":                          
                            await Task.Delay(10);   // Delay 등 비동기 처리 예시

                            // 특정 조건에 따라 다음 스텝을 변경하는 예시
                            nextStep = stepString.ToList().FindIndex(x => x.Equals("Step3"));                          
                            break;

                        case "Step3":
                            nextStep++;
                            break;

                        case "End":
                            context.Status = ActionStatus.Finished; // 정상 종료
                            break;

                        default: //정의 되지 않은 step
                            context.Status = ActionStatus.Error;
                            break;
                    }
                    return nextStep;
                }
            );
        }
    }
}
