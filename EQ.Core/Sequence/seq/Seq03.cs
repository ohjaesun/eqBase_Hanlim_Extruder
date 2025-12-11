using EQ.Core.Act;
using EQ.Domain.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

using static EQ.Core.Sequence.seq03Step;
namespace EQ.Core.Sequence
{
    public enum seq03_Sig // 이름 포맷 꼭 지켜야 함 (이름 + _Sig)  => XXX_Sig
    {
        TrayReady,      // 트레이 준비 완료 신호
        WorkComplete    // 작업 완료 신호
    }
    public enum seq03Step
    {
        Start,
        Step2,
        Step3,
        Step4,
        Step5,
        End
    }

    public class Seq03 : AbstractSeqBase<seq03Step>
    {

        public Seq03(SEQ seqManager, ACT actManager) : base(seqManager, actManager)
        {
            InitSignals<seq03_Sig>();
        }

        public override async Task doSequence()
        {
           
            switch (Step)
            {
                case Start:
                    {
                        Step++;
                    }
                    break;

                case Step2:
                    {
                        await _act.IO.doubleTypeOnAsync(IO_OUT.TRAY_FEEDER_Front_Clamp_ForWard_ON);
                        Step++;
                    }
                    break;

                case Step3:
                    {
                        await Task.Delay(1000);
                        Step++;
                    }
                    break;

                case Step4:
                    {
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