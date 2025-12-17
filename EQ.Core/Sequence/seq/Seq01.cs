using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Domain.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

using static EQ.Core.Sequence.seq01Step;
namespace EQ.Core.Sequence
{
    public enum Seq01_Sig // 이름 포맷 꼭 지켜야 함 (시퀀스 ClassName + _Sig)  => XXX_Sig ==> Seq01_Sig
    {
        MachineReady,      
        TempReady,         
        ExtrusionWorking,  
        WorkComplete       
    }

    public enum seq01Step
    {
        Start,                  // 시작
        CheckSafety,           // 안전 확인
        SetTemperature,        // 온도 설정
        WaitSoaking,           // 온도 안정화 대기
        StartExtruder,         // 압출 스크류 기동
        WaitExtruderStable,    // 압출 안정화 대기
        StartFeeder,           // 피더 기동
        WaitMaterialOut,       // 압출물 배출 대기
        StartPuller,           // 인취기 기동
        StartPIDControl,       // PID 제어 시작
        Production,            // 정상 생산
        CheckLength,           // 목표 길이 확인
        FinishProduction,      // 종료 준비
        StopFeeder,            // 피더 정지
        StopPuller,            // 인취기 정지
        StopExtruder,          // 압출 스크류 정지
        End                    // 종료
    }

    public class Seq01 : AbstractSeqBase<seq01Step>
    {     
  
        public Seq01(SEQ seqManager, ACT actManager) : base(seqManager, actManager)
        {
            InitSignals<Seq01_Sig>();
        }

       

        public override async Task doSequence()
        {           
            switch (Step)
            {
                case Start:
                    {
                        Log.Instance.Info("압출 시퀀스 시작");
                        ResetSignal(Seq01_Sig.MachineReady);
                        ResetSignal(Seq01_Sig.TempReady);
                        ResetSignal(Seq01_Sig.ExtrusionWorking);
                        ResetSignal(Seq01_Sig.WorkComplete);
                        Step++;
                    }
                    break;

                case CheckSafety:
                    {
                        // 안전 조건 확인
                        // - 비상정지 해제 확인
                        // - 도어 닫힘 확인
                        // - 모터 정지 상태 확인
                        Log.Instance.Info("안전 확인 중...");
                        await Task.Delay(500);
                        SetSignal(Seq01_Sig.MachineReady);
                        Step++;
                    }
                    break;

                case SetTemperature:
                    {
                        // 온도 설정 (레시피에서 읽어옴)
                        // - 배럴 1, 2 온도 설정
                        // - 히팅 플레이트 온도 설정
                        // - 냉각수 온도 설정
                        Log.Instance.Info("온도 설정 시작");
                        // var recipe = _act.ExtruderRecipe.CurrentRecipe;
                        // await _act.Temperature.SetAsync(TempID.Barrel1, recipe.ExtruderTemperature1);
                        // await _act.Temperature.SetAsync(TempID.Barrel2, recipe.ExtruderTemperature2);
                        await Task.Delay(1000);
                        Step++;
                    }
                    break;

                case WaitSoaking:
                    {
                        // 온도 안정화 대기 (Soaking Time)
                        // - 모든 온도가 목표값 도달 확인
                        // - HeatUpTime 만큼 대기
                        Log.Instance.Info("온도 안정화 대기 중...");
                        // bool tempReady = await _act.Temperature.IsAllTempReadyAsync();
                        // if (tempReady)
                        // {
                        //     await Task.Delay((int)(recipe.HeatUpTime * 1000));
                        //     SetSignal(Seq01_Sig.TempReady);
                        //     Step++;
                        // }
                        await Task.Delay(2000);
                        SetSignal(Seq01_Sig.TempReady);
                        Step++;
                    }
                    break;

                case StartExtruder:
                    {
                        // 압출 스크류 모터 기동
                        // - 저속 → 목표 속도로 가속
                        Log.Instance.Info("압출 스크류 기동");
                        // var recipe = _act.ExtruderRecipe.CurrentRecipe;
                        // await _act.Motion.JogAsync(MotionID.EXTRUDER_R, recipe.ExtruderSpeed);
                        await Task.Delay(1000);
                        Step++;
                    }
                    break;

                case WaitExtruderStable:
                    {
                        // 압출 스크류 안정화 대기
                        // - 토크 안정 확인
                        // - 압출 시작 확인
                        Log.Instance.Info("압출 안정화 대기 중...");
                        await Task.Delay(3000);
                        Step++;
                    }
                    break;

                case StartFeeder:
                    {
                        // 피더 모터 기동
                        // - 재료 공급 시작
                        Log.Instance.Info("피더 기동");
                        // var recipe = _act.ExtruderRecipe.CurrentRecipe;
                        // await _act.Motion.JogAsync(MotionID.FEEDER_R, recipe.FeederSetPoint);
                        await Task.Delay(1000);
                        Step++;
                    }
                    break;

                case WaitMaterialOut:
                    {
                        // 압출물 배출 대기
                        // - 압출 노즐에서 재료가 나오기 시작할 때까지 대기
                        Log.Instance.Info("압출물 배출 대기 중...");
                        await Task.Delay(5000);
                        Step++;
                    }
                    break;

                case StartPuller:
                    {
                        // 인취기 모터 기동
                        // - 초기 저속으로 시작
                        Log.Instance.Info("인취기 기동");
                        // var recipe = _act.ExtruderRecipe.CurrentRecipe;
                        // await _act.Motion.JogAsync(MotionID.PULLER_T, recipe.PullerStartSpeed);
                        await Task.Delay(1000);
                        Step++;
                    }
                    break;

                case StartPIDControl:
                    {
                        // PID 직경 제어 시작
                        Log.Instance.Info("PID 제어 시작");
                        // var recipe = _act.ExtruderRecipe.CurrentRecipe;
                        // await _act.Puller.StartPullingWithControlAsync(recipe);
                        await Task.Delay(1000);
                        SetSignal(Seq01_Sig.ExtrusionWorking);
                        Step++;
                    }
                    break;

                case Production:
                    {
                        // 정상 생산 상태
                        // - 직경 측정 및 PID 제어 동작 중
                        // - 품질 모니터링
                        Log.Instance.Info("정상 생산 중...");
                        await Task.Delay(1000);
                        // 자동으로 다음 스텝으로 진행하려면 조건 추가
                        // Step++; // 테스트용
                    }
                    break;

                case CheckLength:
                    {
                        // 목표 길이 도달 확인
                        // - 엔코더 값으로 길이 측정
                        Log.Instance.Info("목표 길이 확인");
                        // var recipe = _act.ExtruderRecipe.CurrentRecipe;
                        // double currentLength = _act.Motion.GetPosition(MotionID.PULLER_T);
                        // if (currentLength >= recipe.FilamentLength)
                        // {
                        //     Step++;
                        // }
                        await Task.Delay(500);
                        Step++;
                    }
                    break;

                case FinishProduction:
                    {
                        // 종료 준비
                        Log.Instance.Info("생산 종료 준비");
                        await Task.Delay(500);
                        Step++;
                    }
                    break;

                case StopFeeder:
                    {
                        // 피더 정지 (재료 공급 중단)
                        Log.Instance.Info("피더 정지");
                        // _act.Motion.MoveStop(MotionID.FEEDER_R);
                        await Task.Delay(1000);
                        Step++;
                    }
                    break;

                case StopPuller:
                    {
                        // 인취기 정지 및 PID 제어 중단
                        Log.Instance.Info("인취기 정지");
                        // await _act.Puller.StopPullingAsync();
                        await Task.Delay(1000);
                        Step++;
                    }
                    break;

                case StopExtruder:
                    {
                        // 압출 스크류 정지
                        Log.Instance.Info("압출 스크류 정지");
                        // _act.Motion.MoveStop(MotionID.EXTRUDER_R);
                        await Task.Delay(1000);
                        ResetSignal(Seq01_Sig.ExtrusionWorking);
                        SetSignal(Seq01_Sig.WorkComplete);
                        Step++;
                    }
                    break;

                case End:
                    _Step++; //End보다 크게 만들어서 종료 처리
                    Log.Instance.Info("압출 시퀀스 완료");
                    break;

                default:                   
                    _Status = SeqStatus.ERROR;
                    break;
            }
        }

        // 에러가 발생해서 시퀀스가 멈출 때 자동으로 호출됨
        public override async Task OnErrorRecovery()
        {
            var ErrorStep = Step;

            // 에러 복구 처리

        }

        public override async Task OnStopping()
        {
            var ErrorStep = Step;

            // 스탑 동작시 처리
        }
    }
}
