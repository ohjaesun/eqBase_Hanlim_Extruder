using EQ.Core.Service;
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

namespace EQ.UI.UserViews.EQ_HanLim_Extuder
{
    public partial class Temp_View : UserControlBaseplain
    {
        public Temp_View()
        {
            InitializeComponent();
        }

        private void Temp_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            timer1.Interval = 1000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var _temp1 = ActManager.Instance.Act.Temp.Get(TempID.Zone1).ReadPV();
            var _temp2 = ActManager.Instance.Act.Temp.Get(TempID.Zone2).ReadPV();

            var _run1 = ActManager.Instance.Act.Temp.Get(TempID.Zone1).IsRunning();
            var _run2 = ActManager.Instance.Act.Temp.Get(TempID.Zone2).IsRunning();                      

            this.SuspendLayout();
            if (_run1 && _LabeRun1.ThemeStyle != UI.Controls.ThemeStyle.Success_Green)
            {
                _LabeRun1.ThemeStyle = UI.Controls.ThemeStyle.Success_Green;
                _LabeRun1.Text = "RUN";
            }                
            else if (!_run1 && _LabeRun1.ThemeStyle != UI.Controls.ThemeStyle.Neutral_Gray)
            {
                _LabeRun1.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
                _LabeRun1.Text = "STOP";
            }

            if (_run2 && _LabeRun2.ThemeStyle != UI.Controls.ThemeStyle.Success_Green)
            {
                _LabeRun2.ThemeStyle = UI.Controls.ThemeStyle.Success_Green;
                _LabeRun2.Text = "RUN";
            }
            else if (!_run2 && _LabeRun2.ThemeStyle != UI.Controls.ThemeStyle.Neutral_Gray)
            {
                _LabeRun2.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
                _LabeRun2.Text = "STOP";
            }

            //var tempUnit = ActManager.Instance.Act.Option.GetUIValueByName<int>("_ComboBoxSelectRCP");
            var tempUnit = ActManager.Instance.Act.Option.GetUIValueByName<bool>("_RadioButtonCelsius");

            if (tempUnit)
            {
                _LabelTemp1.Text = $"{_temp1:0.0} °C";
                _LabelTemp2.Text = $"{_temp2:0.0} °C";
            }
            else
            {                
                _temp1 = (_temp1 * 9 / 5) + 32;
                _temp2 = (_temp2 * 9 / 5) + 32;

                _LabelTemp1.Text = $"{_temp1:0.0} °F";
                _LabelTemp2.Text = $"{_temp2:0.0} °F";
            }

            
            this.ResumeLayout();


            

        }
    }
}
