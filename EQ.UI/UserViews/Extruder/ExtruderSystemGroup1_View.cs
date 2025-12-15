using EQ.UI.UserViews;

namespace EQ.UI.UserViews.Extruder
{
    public partial class ExtruderSystemGroup1_View : UserControlBaseplain
    {
        public ExtruderSystemGroup1_View()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            InitializeButtonEvents();
        }

        private void InitializeButtonEvents()
        {
            // Feeder 섹션 이벤트
            _btnFeederStart.Click += OnFeederStartClick;
        }

        #region Feeder 섹션 이벤트

        private void OnFeederStartClick(object? sender, EventArgs e)
        {
            // TODO: Feeder Start 로직 구현
        }

        #endregion

        #region 공용 업데이트 메서드

        public void UpdateFeederStatus(bool isDisabled, double speed, double actualSpeed)
        {
            if (InvokeRequired)
            {
                Invoke(() => UpdateFeederStatus(isDisabled, speed, actualSpeed));
                return;
            }

            //_lblFeederDisabled.Visible = isDisabled;
            _txtFeederSpeed.Text = speed.ToString("F1");
            _txtFeederActualSpeed.Text = actualSpeed.ToString("F1");
        }

        public void UpdateExtruderStatus(
            bool hasError,
            double speed,
            double tempZone1,
            double tempZone2,
            double actualSpeed,
            double barrelTemp1,
            double barrelTemp2,
            string barrel,
            double torque,
            string errorId,
            string warningId,
            string fuse)
        {
            if (InvokeRequired)
            {
                Invoke(() => UpdateExtruderStatus(hasError, speed, tempZone1, tempZone2, 
                    actualSpeed, barrelTemp1, barrelTemp2, barrel, torque, errorId, warningId, fuse));
                return;
            }

            _lblExtruderError1.Visible = hasError;
            _lblExtruderError2.Visible = hasError;
            _txtSpeed.Text = speed.ToString("F1");
            _txtTempZone1.Text = tempZone1.ToString("F1");
            _txtTempZone2.Text = tempZone2.ToString("F1");
            _lblActualSpeedValue.Text = actualSpeed.ToString("F1");
            _lblBarrelTemp1Value.Text = barrelTemp1.ToString("F1");
            _lblBarrelTemp2Value.Text = barrelTemp2.ToString("F1");
            _lblBarrelValue.Text = barrel;
            _lblTorqueValue.Text = torque.ToString("F1");
            _lblErrorIdValue.Text = errorId;
            _lblWarningIdValue.Text = warningId;
            _lblFuseValue.Text = fuse;
        }

        #endregion
    }
}
