using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EQ.Core.Sequence.SEQ;
using static EQ.Core.Globals;

namespace EQ.UI
{
    public partial class FormTest : FormBase
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private async void _Button1_Click(object sender, EventArgs e)
        {
            var act = ActManager.Instance.Act;

            act.PopupNoti("시퀀스 시작", "Seq01이 시작되었습니다.", NotifyType.Error);

            var r = await act.PopupYesNo.ConfirmAsync(
                        "시퀀스 확인",
                        "A 실린더가 ON 되었습니다. 다음 스텝으로 진행할까요?",
                        NotifyType.Error
                    );
            if (r == YesNoResult.Yes)
            {

            }
        }

        private void _Button2_Click(object sender, EventArgs e)
        {
            var act = ActManager.Instance.Act;
            SeqManager.Instance.Seq.RunSequence(SeqName.Seq1_시나리오명);

            var seq = SeqManager.Instance.Seq;
            //시퀀스의 현재 상태 가져오기
            var status = seq.GetSequence(SeqName.Seq1_시나리오명)._Status;
        }

        private void _Button3_Click(object sender, EventArgs e)
        {
            var act = ActManager.Instance.Act;
            act.IO.GetIoStatus();
        }

        private void _Button4_Click(object sender, EventArgs e)
        {
            var act = ActManager.Instance.Act;
            act.TowerLamp.SetState(EqState.Running);
        }

        private void _Button5_Click(object sender, EventArgs e)
        {
            FormLogin loginForm = new FormLogin();
            loginForm.ShowDialog();
        }

        private void _Button6_Click(object sender, EventArgs e)
        {
            var act = ActManager.Instance.Act;
            var r = act.User.CheckAccess(UserLevel.Engineer);
            if (r)
            {
                act.PopupNoti("로그인레벨", "접근 허용.", NotifyType.Info);
            }
            else
            {
                act.PopupNoti("로그인레벨", "접근 불가.", NotifyType.Error);
            }
        }

        private async void _Button7_Click(object sender, EventArgs e)
        {
            var act = ActManager.Instance.Act;
            await act.Motion.MoveAbsAsync(STAGE_X.Target);
        }

        private void _Button8_Click(object sender, EventArgs e)
        {
            var act = ActManager.Instance.Act;
            act.PopupAlarm(ErrorList.MOTOR_SERVO_OFF, L("Servo Error detected!"));

            //    act.AlarmDB.Set(ErrorList.ServoOff, "test");
        }

        private void _Button10_Click(object sender, EventArgs e)
        {
            //데이터 값 변경

            var sourceMag = ActManager.Instance.Act.WaferMagazine.GetMagazine(MagazineName.LoadPort1);
            var wafer = sourceMag.GetSlot(0);

            //2차원 인덱서 접근
            wafer[0, 0].Grade = Domain.Entities.ProductUnitChipGrade.Good;


            //1차원 직접 접근 ( 속도면에서 이게 빠름 )
            /*
             * index = (y * Cols) + x
             * 가로(Cols)가 10개일 때, (3, 2) 좌표의 인덱스는 (2 * 10) + 3 = 23번
             */
            wafer.Units[0].Grade = ProductUnitChipGrade.Good;

            // [방법 B] 전체 순회 (가장 빠름)
            // 좌표 계산(y * Cols + x) 오버헤드가 없어 성능이 가장 좋습니다.
            for (int i = 0; i < wafer.Units.Length; i++)
            {
                // ref를 사용하여 구조체 데이터 직접 수정
                ref var cell = ref wafer.Units[i];
                cell.Grade = ProductUnitChipGrade.Good;
            }
        }

        private void _Button9_Click(object sender, EventArgs e)
        {
            //매거진 + 데이터 
            //   ActManager.Instance.Act.TrayMagazine.SaveAll(); //트레이 매거진
            ActManager.Instance.Act.WaferMagazine.SaveAll(); //웨이퍼 매거진

            //매거진의 개별 slot
            // 로드 매거진(ID:1)의 0번 슬롯 저장
            //ActManager.Instance.Act.WaferMagazine.SaveSlot(1, 0);

            // 로드 매거진(ID:1)의 0번 슬롯을 파일에서 다시 불러오기
            //ActManager.Instance.Act.WaferMagazine.LoadSlot(1, 0);



            //단독 데이터
            //     ActManager.Instance.Act.Tray.SaveMap();
            //     ActManager.Instance.Act.Wafer.SaveMap();
        }

        private void _Button11_Click(object sender, EventArgs e)
        {
            var keypad = new EQ.UI.Forms.FormKeypad("Input",10,0,100);
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                double newValue = keypad.ResultValue;
                // 값 적용 로직
            }
            //ActManager.Instance.Act.SecsGem.Start();
        }
    }
}
