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

        public Chart_View()
        {
            InitializeComponent();
        }

        private void Chart_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            InitializeChart();
            InitializeCheckBoxes(); // 체크박스 초기화 추가
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
            _formsPlot1.Refresh();
        }

        private void InitializeCheckBoxes()
        {
            return;
            // 체크박스와 로거 매칭 헬퍼 함수
            SetupCheckBox(_CheckBox1, _logZone1);
            SetupCheckBox(_CheckBox2, _logZone2);
            SetupCheckBox(_CheckBox3, _logRpm);
            SetupCheckBox(_CheckBox4, _logTorque);
        }

        private void SetupCheckBox(CheckBox chk, DataLogger logger)
        {
            if (chk == null) return;

            // 텍스트와 색상을 로거와 일치시킴
            chk.Text = logger.LegendText;
            chk.ForeColor = System.Drawing.ColorTranslator.FromHtml(logger.Color.ToString());
            chk.Checked = true;
            //    chk.Font = new Font("Segoe UI", 9, FontStyle.Bold); // 폰트 스타일 (선택사항)

            // 이벤트 연결: 체크 여부에 따라 그래프 보임/숨김
            chk.CheckedChanged += (s, e) =>
            {
                logger.IsVisible = chk.Checked;
                _formsPlot1.Refresh();
            };
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
                        
            switch(idx)
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
    }
}