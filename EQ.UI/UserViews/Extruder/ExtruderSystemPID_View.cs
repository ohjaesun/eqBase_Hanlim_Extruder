using EQ.UI.UserViews;

namespace EQ.UI.UserViews.Extruder
{
    public partial class ExtruderSystemPID_View : UserControlBaseplain
    {
        public ExtruderSystemPID_View()
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

         
        }

        #endregion
    }
}
