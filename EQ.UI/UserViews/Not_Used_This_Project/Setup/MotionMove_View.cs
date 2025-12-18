using EQ.Common.Helper;
using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using ScottPlot.Colormaps;

namespace EQ.UI.UserViews
{
    public partial class MotionMove_View : UserControlBaseplain
    {
        public MotionMove_View()
        {
            InitializeComponent();

            _LabelTitle.Text = string.Empty;

            timer1.Interval = 500;
            timer1.Start();
        }

        MotionID ID { get; set; }


        public void setMotion(MotionID id)
        {
            ID = id;
            _LabelTitle.Text = ID.ToString();
        }
        public void setDefinePos(double Pos)
        {
            _TextBox1.Invoke(() =>
            {
                _TextBox1.Text = Pos.ToString();
            });
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.SuspendLayout();

            try
            {
                if (string.IsNullOrEmpty(_TextBox1.Text)) return;

                var _act = ActManager.Instance.Act.Motion;
                var status = _act.GetStatus(ID);

                _LabelInfo1.ThemeStyle = status.ServoOn ? UI.Controls.ThemeStyle.Success_Green : UI.Controls.ThemeStyle.Neutral_Gray;
                _LabelInfo2.ThemeStyle = status.AmpAlarm ? UI.Controls.ThemeStyle.Danger_Red : UI.Controls.ThemeStyle.Neutral_Gray;
                _LabelInfo3.ThemeStyle = status.InPos ? UI.Controls.ThemeStyle.Success_Green : UI.Controls.ThemeStyle.Neutral_Gray;
                _LabelInfo4.Text = status.ActualPos.ToString();
            }
            finally
            {
                // 모든 업데이트가 완료된 후, 일시 중단된 그리기 로직을 재개하고 강제 갱신
                this.ResumeLayout(false);
            }
        }

        private void btnClick(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            var idx = Utils.GetButtonIdx(btn.Name);

            if (string.IsNullOrEmpty(_LabelTitle.Text)) return;

            var _act = ActManager.Instance.Act.Motion;

            switch (idx)
            {              
                case 3: // Rel -
                case 4: // Rel +
                    {
                        double tar = 0;

                        if (_RadioButton3.Checked) tar = 100;
                        if (_RadioButton4.Checked) tar = 500;
                        if (_RadioButton5.Checked) tar = 1000;
                        if (_RadioButton6.Checked) tar = 5000;
                        if (_RadioButton7.Checked) tar = 10000;
                        if (_RadioButton8.Checked)
                        {
                            if (double.TryParse(_TextBox4.Text, out tar) == false) return;
                        }

                        if (idx == 3) tar = tar * -1;

                        var speed = ActManager.Instance.Act.Option.MotionSpeed.SpeedList[(int)ID];

                        posCommand posCommand = new posCommand();
                        posCommand.idx = ID;
                        posCommand.targetPostition = tar;
                        posCommand.velocity = speed.ManualSpeed;

                        _act.MoveRelAsync(posCommand);
                        break;
                    }

                    {
                        break;
                    }
                case 5: // Mode Go
                    {
                        double tar = 0;

                        if (_RadioButton9.Checked)  // ABS
                        {
                            if (double.TryParse(_TextBox1.Text, out tar) == false) return;

                            var speed = ActManager.Instance.Act.Option.MotionSpeed.SpeedList[(int)ID];

                            posCommand posCommand = new posCommand();
                            posCommand.idx = ID;
                            posCommand.targetPostition = tar;
                            posCommand.velocity = speed.ManualSpeed;

                            _act.MoveAbsAsync(posCommand);
                        }
                        else if (_RadioButton10.Checked) // Torque
                        {
                            if (double.TryParse(_TextBox2.Text, out tar) == false) return;

                            if (tar > 300 || tar < -300)
                            {
                                ActManager.Instance.Act.PopupNoti("-300~300", NotifyType.Info);
                                return;
                            }
                            _act.MoveTrqAsync(ID, tar);
                        }
                        else if (_RadioButton11.Checked) // Vel
                        {
                            if (double.TryParse(_TextBox3.Text, out tar) == false) return;

                            _act.MoveVelAsync(ID, tar);
                        }
                        break;
                    }
                case 6: // Stop
                    {
                        _act.MoveStop(ID);
                        break;
                    }
            }
        }

        private void _Button1_MouseDown(object sender, MouseEventArgs e)
        {
            var btn = (Button)sender;
            var idx = Utils.GetButtonIdx(btn.Name);

            if (string.IsNullOrEmpty(_LabelTitle.Text)) return;

            var _act = ActManager.Instance.Act.Motion;

            bool _fast = _RadioButton2.Checked;

            switch (idx)
            {
                case 1: //Jog -
                    {
                        _act.MoveJogStartAsync(ID, false,_fast);
                    }
                    break;
                    case 2: //Jog +
                    {
                        _act.MoveJogStartAsync(ID, true,_fast);
                    }
                    break;
            }
        }

        private void _Button1_MouseUp(object sender, MouseEventArgs e)
        {
            var btn = (Button)sender;
            var idx = Utils.GetButtonIdx(btn.Name);

            if (string.IsNullOrEmpty(_LabelTitle.Text)) return;

            var _act = ActManager.Instance.Act.Motion;

            _act.MoveJogStopAsync(ID);

         
        }
    }
}
