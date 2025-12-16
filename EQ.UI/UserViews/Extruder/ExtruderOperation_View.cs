using EQ.Common.Helper;
using EQ.Core.Act;
using EQ.Core.Act.Composition;
using EQ.Core.Service;
using EQ.Domain.Enums;
using System;
using System.Windows.Forms;

using static EQ.Core.Globals;

namespace EQ.UI.UserViews.Extruder
{
    /// <summary>
    /// Extruder 설정 화면 - HMI 스타일 UI
    /// Recipe/Batch, Parameter, Safety, Part bins 섹션으로 구성
    /// </summary>
    public partial class ExtruderOperation_View : UserControlBaseplain
    {
        readonly ACT act = ActManager.Instance.Act;

        public ExtruderOperation_View()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

        }

        private void ExtruderOperation_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            SafeSubscribe(
       () => act.Temp.OnTemperatureUpdated += OnTempUpdate,
       () => act.Temp.OnTemperatureUpdated -= OnTempUpdate);

            timer1.Interval = 1000;
            timer1.Start();
        }

        TemperatureData[] _temp = new TemperatureData[2];

        private void OnTempUpdate(TemperatureData data)
        {
            TemperatureData temp = new TemperatureData()
            {
                Name = data.Name,
                IsRunning = data.IsRunning,
                CurrentTemperature = data.CurrentTemperature,
                TargetTemperature = data.TargetTemperature,
                IsConnected = data.IsConnected
            };
            if (data.Name.Equals(TempID.Zone1.ToString()))
                _temp[0] = temp;
            else
                _temp[1] = temp;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SuspendLayout();

           

            //왼쪽 패널
            {
                //스크류 모터 
                {
                    var screwMotor = act.Motion.GetStatus(MotionID.SCREW_T);
                    _lblTorqueActual.Text = CalcTorque.ToNcm(SGMXJModel._04A, screwMotor.ActualTorque).ToString("F1");//토크 % to Ncm
                    _lblScrewSpeedActual.Text = screwMotor.ActualVelocity.ToString("F1");     //RPM       
                    _Label6.Text = screwMotor.InPos ? "Stop" : "Run";
                }

                //Puller 모터 
                {
                    var pullerMotor = act.Motion.GetStatus(MotionID.PULLER_T);
                    _lblPullerSpeedActual.Text = pullerMotor.ActualVelocity.ToString("F1");   //RPM
                    _Label8.Text = pullerMotor.InPos ? "Stop" : "Run";
                }

                //Feeder 모터 
                {
                    var feederMotor = act.Motion.GetStatus(MotionID.FEEDER_T);
                    _lblFeederSpeedActual.Text = feederMotor.ActualVelocity.ToString("F1");   //RPM
                    _Label7.Text = feederMotor.InPos ? "Stop" : "Run";
                }
            }


            //오른쪽 패널
            {
                //배럴 온도
                {

                }

                //히터 온도
                {
                    _lblHeatPlateRightActual.Text = _temp[0].CurrentTemperature.ToString("F1") ?? "0.0";
                    _lblHeatPlateLeftActual.Text = _temp[1].CurrentTemperature.ToString("F1") ?? "0.0";

                    _txtHeatPlateRightTarget.Text = _temp[0].TargetTemperature.ToString("F1") ?? "0.0";
                    _txtHeatPlateLeftTarget.Text = _temp[1].TargetTemperature.ToString("F1") ?? "0.0";

                    _Label46.Text = _temp[0].IsRunning ? "RUN" : "STOP";
                    _Label46.ThemeStyle = _temp[0].IsRunning ? UI.Controls.ThemeStyle.Success_Green : UI.Controls.ThemeStyle.Neutral_Gray;
                    _Label47.Text = _temp[1].IsRunning ? "RUN" : "STOP";
                    _Label47.ThemeStyle = _temp[1].IsRunning ? UI.Controls.ThemeStyle.Success_Green : UI.Controls.ThemeStyle.Neutral_Gray;
                }
            }

            //중간 패널
            {
                // 길이
                { 
                }

                // 원형도
                {

                }

            }



            ResumeLayout();
        }
    }
}
