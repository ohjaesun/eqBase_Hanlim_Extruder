using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScottPlot;

namespace EQ.UI.UserViews.Extruder
{
    public partial class ExtruderChart_View : UserControlBaseplain
    {
        private Plot plot1;
        private Plot plot2;

        public ExtruderChart_View()
        {
            InitializeComponent();
            InitializeCharts();
        }

        private void InitializeCharts()
        {
            // ScottPlot 5.x FormsPlot의 Multiplot 속성을 사용하여 2개의 서브플롯 구성
            
            // 2개의 서브플롯 추가
            formsPlot1.Multiplot.AddPlots(2);
            
            plot1 = formsPlot1.Multiplot.GetPlot(0);
            plot2 = formsPlot1.Multiplot.GetPlot(1);

            // 첫 번째 서브플롯: Temperature
            plot1.Title("Temperature");
            plot1.XLabel("Time (s)");
            plot1.YLabel("Temperature (°C)");
            double[] time1 = Generate.Consecutive(100);
            double[] temp = Generate.Sin(100, mult: 10, offset: 50);
            plot1.Add.Scatter(time1, temp);

            // 두 번째 서브플롯: Speed
            plot2.Title("Speed");
            plot2.XLabel("Time (s)");
            plot2.YLabel("Speed (RPM)");
            double[] time2 = Generate.Consecutive(100);
            double[] speed = Generate.Sin(100, mult: 20, offset: 100);
            plot2.Add.Scatter(time2, speed);

            // 차트 갱신
            formsPlot1.Refresh();
        }

        private void ChkSyncXAxis_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSyncXAxis.Checked)
            {
                // ⭐ X축 동기화 활성화
                formsPlot1.Multiplot.SharedAxes.ShareX(new[] { plot1, plot2 });
            }
            else
            {
                // X축 동기화 비활성화 (빈 배열 전달)
                formsPlot1.Multiplot.SharedAxes.ShareX(Array.Empty<Plot>());               
            }
            
            formsPlot1.Refresh();
        }
    }
}

