using EQ.Common.Helper;
using EQ.Core.Service;
using ScottPlot;
using ScottPlot.Plottables;
using ScottPlot.WinForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EQ.UI.UserViews.EQ_HanLim_Extuder
{
    public partial class Chart_View : UserControlBaseWithTitle
    {
        private DataLogger _logZone1;
        private DataLogger _logZone2;
        private DataLogger _logRpm;
        private DataLogger _logTorque;
        private int _lastIndex = 0;
        private ScottPlot.Plottables.Crosshair _crosshair;
        private ScottPlot.Plottables.Annotation _tooltip;

        public Chart_View()
        {
            InitializeComponent();
        }

        private void Chart_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            InitializeChart();
            Init();
        }

        private void InitializeChart()
        {
            _formsPlot1.Plot.Clear();

            // --- [그룹 1] 왼쪽 Y축: 온도 (10 ~ 100) ---
            _logZone1 = _formsPlot1.Plot.Add.DataLogger();
            _logZone1.LegendText = "Zone 1";
            _logZone1.Color = Colors.Red;

            _logZone2 = _formsPlot1.Plot.Add.DataLogger();
            _logZone2.LegendText = "Zone 2";
            _logZone2.Color = Colors.Orange;

            // 왼쪽 Y축 라벨 및 범위 고정
            var leftAxis = _formsPlot1.Plot.Axes.Left;
            leftAxis.Label.Text = "Temperature (°C)";
            leftAxis.Label.ForeColor = Colors.Red;

            // [핵심] Y축 범위 고정 (10 ~ 100)
            //   _formsPlot1.Plot.Axes.SetLimitsY(10, 100, leftAxis);
            //   var leftLock = new ScottPlot.AxisRules.LockedVertical(leftAxis, 10, 100);
            //   _formsPlot1.Plot.Axes.Rules.Add(leftLock);
            _formsPlot1.Plot.Axes.SetLimitsY(10, 100, leftAxis);



            // --- [그룹 2] 오른쪽 Y축: 모터 (0 ~ 350) ---
            var rightAxis = _formsPlot1.Plot.Axes.AddRightAxis();

            _logRpm = _formsPlot1.Plot.Add.DataLogger();
            _logRpm.LegendText = "RPM";
            _logRpm.Color = Colors.Blue;
            _logRpm.Axes.YAxis = rightAxis; // 오른쪽 축 할당

            _logTorque = _formsPlot1.Plot.Add.DataLogger();
            _logTorque.LegendText = "Torque";
            _logTorque.Color = Colors.SkyBlue;
            _logTorque.Axes.YAxis = rightAxis; // 오른쪽 축 할당

            // 오른쪽 Y축 라벨 및 범위 고정
            rightAxis.Label.Text = "Motor (RPM / Torque)";
            rightAxis.Label.ForeColor = Colors.Blue;

            // [핵심] Y축 범위 고정 (0 ~ 350)
            //   _formsPlot1.Plot.Axes.SetLimitsY(0, 350, rightAxis);
            //   var rightLock = new ScottPlot.AxisRules.LockedVertical(rightAxis, 0, 350);
            //   _formsPlot1.Plot.Axes.Rules.Add(rightLock);
            _formsPlot1.Plot.Axes.SetLimitsY(0, 350, rightAxis);


            // 범례는 외부 체크박스를 쓰므로 숨김
            _formsPlot1.Plot.HideLegend();

            // Crosshair 추가 (마우스 위치에 십자선 표시)
            _crosshair = _formsPlot1.Plot.Add.Crosshair(0, 0);
            _crosshair.IsVisible = false;
            _crosshair.LineColor = Colors.Gray;
            _crosshair.LineWidth = 1;

            // 툴팁 Annotation 추가 (마우스 우상단에 값 표시)
            _tooltip = _formsPlot1.Plot.Add.Annotation(string.Empty);
            _tooltip.IsVisible = false;
            _tooltip.LabelBackgroundColor = Colors.White.WithAlpha(0.9);
            _tooltip.LabelBorderColor = Colors.Gray;
            _tooltip.LabelBorderWidth = 1;
            _tooltip.LabelFontColor = Colors.Black;
            _tooltip.LabelFontSize = 12;
            _tooltip.LabelPadding = 5;

            // 마우스 이벤트 연결
            _formsPlot1.MouseMove += FormsPlot1_MouseMove;
            _formsPlot1.MouseLeave += FormsPlot1_MouseLeave;

            _formsPlot1.Refresh();
        }





        public void Init()
        {
            timer1.Interval = 500;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var datas = ActManager.Instance.Act.Extuder.RunDatas;
            if (datas == null) return;

            lock (datas)
            {
                int currentCount = datas.Count;

                // [Reset Logic]
                if (currentCount < _lastIndex)
                {
                    _lastIndex = 0;
                    _logZone1.Clear();
                    _logZone2.Clear();
                    _logRpm.Clear();
                    _logTorque.Clear();
                }

                // [Update Logic]
                if (currentCount > _lastIndex)
                {
                    for (int i = _lastIndex; i < currentCount; i++)
                    {
                        var data = datas[i];
                        _logZone1.Add(data.Zone1);
                        _logZone2.Add(data.Zone2);
                        _logRpm.Add(data.Rpm);
                        _logTorque.Add(data.Torque);
                    }
                    _lastIndex = currentCount;

                    // 데이터가 추가되면 최근 데이터 보기 (선택사항: 필요 시 주석 해제)
                    // _logZone1.ViewSlide(100); 

                    _formsPlot1.Refresh();
                }
            }
        }

        private void _CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            var chk = sender as CheckBox;
            var idx = Utils.GetButtonIdx(chk.Name);

            switch (idx)
            {
                case 1:
                    _logZone1.IsVisible = chk.Checked;
                    break;
                case 2:
                    _logZone2.IsVisible = chk.Checked;
                    break;
                case 3:
                    _logRpm.IsVisible = chk.Checked;
                    break;
                case 4:
                    _logTorque.IsVisible = chk.Checked;
                    break;
            }
        }

        private void FormsPlot1_MouseMove(object sender, MouseEventArgs e)
        {
            // 마우스 픽셀 좌표를 데이터 좌표로 변환
            var pixel = new ScottPlot.Pixel(e.X, e.Y);
            var coords = _formsPlot1.Plot.GetCoordinates(pixel);

            // Crosshair 위치 업데이트
            _crosshair.IsVisible = true;
            _crosshair.Position = coords;

            // X 인덱스 계산 (DataLogger는 0부터 시작하는 인덱스 사용)
            int index = (int)Math.Round(coords.X);

            if(_CheckBox5.Checked == false)
            {
                _tooltip.IsVisible = false;
            }
            // 데이터 범위 확인
            else if (index >= 0 && index < _lastIndex)
            {
                var datas = ActManager.Instance.Act.Extuder.RunDatas;
                if (datas != null && index < datas.Count)
                {
                    var data = datas[index];

                    // 툴팁 텍스트 설정 (마우스 위치 우상단에 표시)
                    _tooltip.IsVisible = true;
                    _tooltip.Text = $"Index: {index}\nZone1: {data.Zone1:F1}°C\nZone2: {data.Zone2:F1}°C\nRPM: {data.Rpm:F1}\nTorque: {data.Torque:F1}";

                    // 마우스 위치의 우상단에 배치 (오프셋 적용)
                    _tooltip.OffsetX = e.X + 10;  // 마우스 우측으로 15픽셀
                    _tooltip.OffsetY = e.Y - 10;  // 마우스 상단으로 10픽셀
                }
                else
                {
                    _tooltip.IsVisible = false;
                }
            }
            else
            {
                _tooltip.IsVisible = false;
            }

            _formsPlot1.Refresh();
        }

        private void FormsPlot1_MouseLeave(object sender, EventArgs e)
        {
            // 마우스가 차트 영역을 벗어나면 Crosshair 및 툴팁 숨김
            _crosshair.IsVisible = false;
            _tooltip.IsVisible = false;
            _formsPlot1.Refresh();
        }

        private void _CheckBox5_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}