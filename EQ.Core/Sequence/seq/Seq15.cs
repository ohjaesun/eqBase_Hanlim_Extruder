using EQ.Core.Act;
using EQ.Domain.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

using static EQ.Core.Sequence.seq15Step;
namespace EQ.Core.Sequence
{
    public enum seq15_Sig // 이름 포맷 꼭 지켜야 함 (이름 + _Sig)  => XXX_Sig
    {
        TrayReady,      // 트레이 준비 완료 신호
        WorkComplete    // 작업 완료 신호
    }
    public enum seq15Step
    {
        Start,
        LOAD_START,
        LOAD_MOVE,
        LOAD_COMPLETE,
        Step5,
        PROCESS_START,
        UNLOAD_START,
        UNLOAD_MOVE,
        UNLOAD_COMPLETE,
        End
    }

    public class Seq15 : AbstractSeqBase<seq15Step>
    {

        public Seq15(SEQ seqManager, ACT actManager) : base(seqManager, actManager)
        {
            InitSignals<seq15_Sig>();
        }

        public override async Task doSequence()
        {
            // 'Step' 대신 'StepEnum'을 사용하면 switch가 더 명확해집니다.
            switch (Step)
            {
                case Start:
                    {
                        Step++;
                    }
                    break;

                case LOAD_START:
                    {
                        // 1. [PIO] 반입 요청 및 대기 (Handshake Start)
                        // -> 상대 장비가 준비될 때까지 기다립니다.
                        var status = await _act.PIO.LoadReqAsync(PIOId.LoadPort1);
                       
                        Step++;
                    }
                    break;

                case LOAD_MOVE:
                    {
                        // 2. [Motion] 실제 모터 이동 (물리적 동작)                     
                      
                        await Task.Delay(1000);
                        Step++;
                    }
                    break;

                case LOAD_COMPLETE:
                    {
                        // 3. [PIO] 반입 완료 처리 (Handshake End)
                        // -> 신호를 끄고 통신을 종료합니다.
                        await _act.PIO.LoadCompAsync(PIOId.LoadPort1);
                        await Task.Delay(500);
                        Step++;
                    }
                    break;

                case Step5:
                    {
                        await Task.Delay(500);
                        Step++;
                    }
                    break;

                case PROCESS_START:
                    {
                        Step++;
                    }
                    break;

                    case UNLOAD_START:
                    {
                        // 1. [PIO] 반출 요청 (Handshake Start)
                        // -> 나 자재 줄 준비 됐어! 받을 준비 해!
                        var status = await _act.PIO.UnloadReqAsync(PIOId.UnLoadPort1);
                        if (status != ActionStatus.Finished)
                        {
                        
                        }
                        Step++;
                    } break;

                    case UNLOAD_MOVE:
                    {
                        // 2. [Motion] 자재 내보내기
                        //모터 동작
                        Step++;
                    }
                    break;

                    case UNLOAD_COMPLETE:
                    {
                        // 3. [PIO] 반출 완료 (Handshake End)
                        await _act.PIO.UnloadCompAsync(PIOId.UnLoadPort1);
                        Step++;
                    } break;

                case End:
                    _Step++; //End보다 크게 만들어서 종료 처리
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