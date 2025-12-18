// EQ.UI/UserViews/Interlock_IO_View.cs
using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Enums;
using EQ.UI.Controls;
using System;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace EQ.UI.UserViews
{
    public partial class Interlock_IO_View : UserControlBase
    {
        private readonly ACT _act;
        private Timer _uiUpdateTimer;

        public Interlock_IO_View()
        {
            InitializeComponent();
            _act = ActManager.Instance.Act;
        }

        private void Interlock_IO_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            // _ButtonSave는 이 화면에서 사용하지 않음
            _ButtonSave.Enabled = false;
            _ButtonSave.Visible = false;
            _LabelTitle.Text = "Interlock Status (I/O)";

            // 디자이너에서 추가한 Timer가 아니므로 여기서 초기화
            _uiUpdateTimer = new Timer();
            _uiUpdateTimer.Interval = 200; // 0.2초마다 상태 업데이트
            _uiUpdateTimer.Tick += UiUpdateTimer_Tick;
            _uiUpdateTimer.Start();

            // UserControl이 닫힐 때 Timer를 정리하도록 이벤트 연결
            this.Disposed += Interlock_View_Disposed;
        }

        private void Interlock_View_Disposed(object sender, EventArgs e)
        {
            // Timer가 실행 중이면 정지하고 리소스 해제
            if (_uiUpdateTimer != null)
            {
                _uiUpdateTimer.Stop();
                _uiUpdateTimer.Tick -= UiUpdateTimer_Tick;
                _uiUpdateTimer.Dispose();
                _uiUpdateTimer = null;
            }
        }

        /// <summary>
        /// Timer가 주기적으로 호출하는 상태 업데이트 메서드
        /// </summary>
        private void UiUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (_act == null) return;

            // --- ▼ [수정된 부분] ▼ ---
            // 컨트롤 변수 이름이 _labelStatus1, 2, 3으로 변경됨

            // 1. EMO 상태 업데이트 (_labelStatus1)
            // (가정: EMO는 A접점(False=Pressed)이므로 !를 붙여 OK/NG 반전)
            bool isEmoOk = !_act.IO.ReadInput(IO_IN.Emergency_Button_Front) &&
                           !_act.IO.ReadInput(IO_IN.Emergency_Button_Left) &&
                           !_act.IO.ReadInput(IO_IN.Emergency_Button_Rear) &&
                           !_act.IO.ReadInput(IO_IN.Emergency_Button_Right);
            UpdateLabelStatus(_labelStatus1, isEmoOk);

            // 2. Main Air 상태 업데이트 (_labelStatus2)
            // (가정: B접점(True=OK))
            bool isAirOk = _act.IO.ReadInput(IO_IN.Main_Air_Check);
            UpdateLabelStatus(_labelStatus2, isAirOk);

            // 3. Door 상태 업데이트 (_labelStatus3)
            // (가정: B접점(True=Closed))
            bool isDoorOk = _act.IO.ReadInput(IO_IN.DOOR_SW_LEFT_1) &&
                            _act.IO.ReadInput(IO_IN.DOOR_SW_LEFT_2) &&
                            _act.IO.ReadInput(IO_IN.DOOR_SW_FRONT_L) &&
                            _act.IO.ReadInput(IO_IN.DOOR_SW_FRONT_R);
            UpdateLabelStatus(_labelStatus3, isDoorOk);

            // _labelStatus4 부터 10까지는 "---" 상태로 유지 (로직 없음)
            // --- ▲ [수정된 부분] ▲ ---
        }

        /// <summary>
        /// 라벨의 텍스트와 테마를 상태(OK/NG)에 따라 변경하는 헬퍼
        /// </summary>
        private void UpdateLabelStatus(_Label label, bool isOk)
        {
            if (isOk)
            {
                if (label.Text != "OK") // 상태가 변경될 때만 업데이트 (깜박임 방지)
                {
                    label.Text = "OK";
                    label.ThemeStyle = ThemeStyle.Success_Green; // Controls.cs에 정의된 테마
                }
            }
            else
            {
                if (label.Text != "NG") // 상태가 변경될 때만 업데이트
                {
                    label.Text = "NG";
                    label.ThemeStyle = ThemeStyle.Danger_Red; // Controls.cs에 정의된 테마
                }
            }
        }
    }
}