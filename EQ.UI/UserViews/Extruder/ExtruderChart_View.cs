using EQ.Common.Helper;
using EQ.Core.Service;
using EQ.Domain.Enums;
using ScottPlot;
using ScottPlot.Plottables;

namespace EQ.UI.UserViews.Extruder
{


    public partial class ExtruderChart_View : UserControlBaseplain
    {
        private Plot plot1;
        private Plot plot2;
        private Plot plot3;


        int lastIndex;
        private DataLogger[] loggers;

        public ExtruderChart_View()
        {
            InitializeComponent();
        }

        private void InitializeCharts(int idx)
        {

            timer1.Stop();

            var plots = new List<Plot>();

            if (_CheckBox1.Checked) plots.Add(null);
            if (_CheckBox2.Checked) plots.Add(null);
            if (_CheckBox3.Checked) plots.Add(null);

            formsPlot1.Reset();
            formsPlot1.Multiplot.Reset();

            formsPlot1.Multiplot.AddPlots(plots.Count);

            int i = 0;
            if (_CheckBox1.Checked) plot1 = formsPlot1.Multiplot.GetPlot(i++);
            if (_CheckBox2.Checked) plot2 = formsPlot1.Multiplot.GetPlot(i++);
            if (_CheckBox3.Checked) plot3 = formsPlot1.Multiplot.GetPlot(i++);           


            int cnt = Enum.GetValues<ChartTypes>().Length;
            loggers = new DataLogger[cnt];
            lastIndex = 0;

            foreach (var logger in loggers)
            {
                if (logger != null)
                    logger.ManageAxisLimits = false;
            }

            // ======================
            // 1번 차트 (RPM / Speed / Torque)
            // ======================
            {
                var plot = plot1;

                plot.Axes.Left.Label.Text = "RPM";

                var rightAxis = plot.Axes.AddRightAxis();
                rightAxis.LabelText = "Speed";

                var rightAxis2 = plot.Axes.AddRightAxis();
                rightAxis2.LabelText = "Torque";

                loggers[(int)ChartTypes.FEEDER_RPM] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.FEEDER_RPM].ManageAxisLimits = false;
                loggers[(int)ChartTypes.FEEDER_RPM].Axes.YAxis = plot.Axes.Left;
                loggers[(int)ChartTypes.FEEDER_RPM].LinePattern = LinePattern.Dashed;

                loggers[(int)ChartTypes.EXTRUDER_RPM] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.EXTRUDER_RPM].ManageAxisLimits = false;
                loggers[(int)ChartTypes.EXTRUDER_RPM].Axes.YAxis = plot.Axes.Left;
                loggers[(int)ChartTypes.EXTRUDER_RPM].LinePattern = LinePattern.Dashed;

                loggers[(int)ChartTypes.PULLER_RPM] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.PULLER_RPM].ManageAxisLimits = false;
                loggers[(int)ChartTypes.PULLER_RPM].Axes.YAxis = plot.Axes.Left;
                loggers[(int)ChartTypes.PULLER_RPM].LinePattern = LinePattern.Dashed;

                loggers[(int)ChartTypes.FEEDER_SPEED] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.FEEDER_SPEED].ManageAxisLimits = false;
                loggers[(int)ChartTypes.FEEDER_SPEED].Axes.YAxis = plot.Axes.Right;
                loggers[(int)ChartTypes.FEEDER_SPEED].LinePattern = LinePattern.Solid;

                loggers[(int)ChartTypes.EXTRUDER_SPEED] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.EXTRUDER_SPEED].ManageAxisLimits = false;
                loggers[(int)ChartTypes.EXTRUDER_SPEED].Axes.YAxis = plot.Axes.Right;
                loggers[(int)ChartTypes.EXTRUDER_SPEED].LinePattern = LinePattern.Solid;

                loggers[(int)ChartTypes.PULLER_SPEED] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.PULLER_SPEED].ManageAxisLimits = false;
                loggers[(int)ChartTypes.PULLER_SPEED].Axes.YAxis = plot.Axes.Right;
                loggers[(int)ChartTypes.PULLER_SPEED].LinePattern = LinePattern.Solid;

                loggers[(int)ChartTypes.FEEDER_TRQ] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.FEEDER_TRQ].ManageAxisLimits = false;
                loggers[(int)ChartTypes.FEEDER_TRQ].Axes.YAxis = rightAxis2;
                loggers[(int)ChartTypes.FEEDER_TRQ].LinePattern = LinePattern.DenselyDashed;

                loggers[(int)ChartTypes.EXTRUDER_TRQ] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.EXTRUDER_TRQ].ManageAxisLimits = false;
                loggers[(int)ChartTypes.EXTRUDER_TRQ].Axes.YAxis = rightAxis2;
                loggers[(int)ChartTypes.EXTRUDER_TRQ].LinePattern = LinePattern.DenselyDashed;

                loggers[(int)ChartTypes.PULLER_TRQ] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.PULLER_TRQ].ManageAxisLimits = false;
                loggers[(int)ChartTypes.PULLER_TRQ].Axes.YAxis = rightAxis2;
                loggers[(int)ChartTypes.PULLER_TRQ].LinePattern = LinePattern.DenselyDashed;

                plot.Axes.SetLimitsY(0, 200, plot.Axes.Left);
                plot.Axes.SetLimitsY(0, 50, plot.Axes.Right);
                plot.Axes.SetLimitsY(0, 50, rightAxis2);
            }

            // ======================
            // 2번 차트 (Temperature)
            // ======================
            {
                var plot = plot2;
                plot.Axes.Left.Label.Text = "Temp";

                var rightAxis = plot.Axes.AddRightAxis();
                rightAxis.LabelText = "Temp";

                //   var rightAxis2 = plot.Axes.AddRightAxis();
                //   rightAxis2.LabelText = "Temp";

                loggers[(int)ChartTypes.ZONE1_TEMP_PV] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.ZONE1_TEMP_PV].ManageAxisLimits = false;
                loggers[(int)ChartTypes.ZONE1_TEMP_PV].Axes.YAxis = plot.Axes.Left;
                loggers[(int)ChartTypes.ZONE1_TEMP_PV].LinePattern = LinePattern.Dashed;

                loggers[(int)ChartTypes.ZONE1_TEMP_SV] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.ZONE1_TEMP_SV].ManageAxisLimits = false;
                loggers[(int)ChartTypes.ZONE1_TEMP_SV].Axes.YAxis = plot.Axes.Left;
                loggers[(int)ChartTypes.ZONE1_TEMP_SV].LinePattern = LinePattern.Solid;

                loggers[(int)ChartTypes.ZONE2_TEMP_PV] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.ZONE2_TEMP_PV].ManageAxisLimits = false;
                loggers[(int)ChartTypes.ZONE2_TEMP_PV].Axes.YAxis = plot.Axes.Left;
                loggers[(int)ChartTypes.ZONE2_TEMP_PV].LinePattern = LinePattern.Dashed;

                loggers[(int)ChartTypes.ZONE2_TEMP_SV] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.ZONE2_TEMP_SV].ManageAxisLimits = false;
                loggers[(int)ChartTypes.ZONE2_TEMP_SV].Axes.YAxis = plot.Axes.Left;
                loggers[(int)ChartTypes.ZONE2_TEMP_SV].LinePattern = LinePattern.Solid;

                loggers[(int)ChartTypes.ZONE3_TEMP_PV] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.ZONE3_TEMP_PV].ManageAxisLimits = false;
                loggers[(int)ChartTypes.ZONE3_TEMP_PV].Axes.YAxis = plot.Axes.Right;
                loggers[(int)ChartTypes.ZONE3_TEMP_PV].LinePattern = LinePattern.Dashed;

                loggers[(int)ChartTypes.ZONE3_TEMP_SV] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.ZONE3_TEMP_SV].ManageAxisLimits = false;
                loggers[(int)ChartTypes.ZONE3_TEMP_SV].Axes.YAxis = plot.Axes.Right;
                loggers[(int)ChartTypes.ZONE3_TEMP_SV].LinePattern = LinePattern.Solid;

                loggers[(int)ChartTypes.ZONE4_TEMP_PV] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.ZONE4_TEMP_PV].ManageAxisLimits = false;
                loggers[(int)ChartTypes.ZONE4_TEMP_PV].Axes.YAxis = plot.Axes.Right;
                loggers[(int)ChartTypes.ZONE4_TEMP_PV].LinePattern = LinePattern.Dashed;

                loggers[(int)ChartTypes.ZONE4_TEMP_SV] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.ZONE4_TEMP_SV].ManageAxisLimits = false;
                loggers[(int)ChartTypes.ZONE4_TEMP_SV].Axes.YAxis = plot.Axes.Right;
                loggers[(int)ChartTypes.ZONE4_TEMP_SV].LinePattern = LinePattern.Solid;


                plot.Axes.SetLimitsY(0, 200, plot.Axes.Left);
                plot.Axes.SetLimitsY(0, 50, plot.Axes.Right);


            }

            // ======================
            // 3번 차트 (Diameter)
            // ======================
            {
                var plot = plot3;

                plot.Axes.Left.Label.Text = "Diameter";

                var rightAxis = plot.Axes.AddRightAxis();
                rightAxis.LabelText = "Ovality";

                var rightAxis2 = plot.Axes.AddRightAxis();
                rightAxis2.LabelText = "Count";

                loggers[(int)ChartTypes.DIAMETER] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.DIAMETER].ManageAxisLimits = false;
                loggers[(int)ChartTypes.DIAMETER].Axes.YAxis = plot.Axes.Left;
                loggers[(int)ChartTypes.DIAMETER].LinePattern = LinePattern.Solid;

                loggers[(int)ChartTypes.OVALITY] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.OVALITY].ManageAxisLimits = false;
                loggers[(int)ChartTypes.OVALITY].Axes.YAxis = plot.Axes.Right;
                loggers[(int)ChartTypes.OVALITY].LinePattern = LinePattern.Dotted;

                loggers[(int)ChartTypes.GOOD_COUNT] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.GOOD_COUNT].ManageAxisLimits = false;
                loggers[(int)ChartTypes.GOOD_COUNT].Axes.YAxis = rightAxis2;
                loggers[(int)ChartTypes.GOOD_COUNT].LinePattern = LinePattern.Dashed;

                loggers[(int)ChartTypes.BAD_COUNT] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.BAD_COUNT].ManageAxisLimits = false;
                loggers[(int)ChartTypes.BAD_COUNT].Axes.YAxis = rightAxis2;
                loggers[(int)ChartTypes.BAD_COUNT].LinePattern = LinePattern.DenselyDashed;

                plot.Axes.SetLimitsY(0, 200, plot.Axes.Left);
                plot.Axes.SetLimitsY(0, 50, plot.Axes.Right);
                plot.Axes.SetLimitsY(0, 50, rightAxis2);
            }

            // X축을 시간으로 표시
            // 모든 플롯에 동일한 패딩을 적용하여 Y축 정렬
            var padding = new ScottPlot.PixelPadding(left: 80, right: 120, bottom: 50, top: 10);

            if (_CheckBox1.Checked && plot1 != null)
            {
                plot1.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericAutomatic();
                plot1.Layout.Fixed(padding);
            }

            if (_CheckBox2.Checked && plot2 != null)
            {
                plot2.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericAutomatic();//new ScottPlot.TickGenerators.DateTimeAutomatic();
                plot2.Layout.Fixed(padding);
            }

            if (_CheckBox3.Checked && plot3 != null)
            {
                plot3.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericAutomatic();
                plot3.Layout.Fixed(padding);
            }

            formsPlot1.Refresh();

            timer1.Interval = 1000;
            timer1.Start();

           
        }

        private void ChkSyncXAxis_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSyncXAxis.Checked)
            {
                // ⭐ X축 동기화 활성화
                formsPlot1.Multiplot.SharedAxes.ShareX(new[] { plot1, plot2, plot3 });
            }
            else
            {
                // X축 동기화 비활성화 (빈 배열 전달)
                formsPlot1.Multiplot.SharedAxes.ShareX(Array.Empty<Plot>());
            }

            formsPlot1.Refresh();
        }

        private void _ButtonVisable_Click(object sender, EventArgs e)
        {
            var idx = Utils.GetButtonIdx((sender as Button).Name);

            InitializeCharts(idx);

        }

       

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool hasNewData = false;
            int cnt = Enum.GetValues<ChartTypes>().Length;

            double x = DateTime.Now.ToOADate(); // 시간 X축


            var getData = ActManager.Instance.Act.ChartData.GetDataSinceAll(lastIndex);

            int getDataCount = getData[0].Count();

            if (getDataCount == 0)
                return;

            int chartIdx = 0;

            foreach (var d in getData)
            {
                var _index = lastIndex;
                foreach (var p in d)
                {
                    double x2 = p.Timestamp.ToOADate();
                    var d2 = p.Value;
                    loggers[chartIdx].Add(_index++, d2);
                }
                chartIdx++;
            }

            lastIndex += getData.Max(d => d.Count());

            // ==========================================
            // X축 스크롤 로직 추가
            // ==========================================
            double windowSize = 100; // 화면에 보여줄 데이터 포인트 개수
            double padding = 10;     // 우측 여백 

            // 현재 들어온 데이터의 최대 index가 windowSize를 넘었을 때만 스크롤
            if (lastIndex > windowSize)
            {
                double minX = lastIndex - windowSize;
                double maxX = lastIndex + padding;

                // 모든 플롯의 X축 범위를 동일하게 업데이트
                if (plot1 != null) plot1.Axes.SetLimitsX(minX, maxX);
                if (plot2 != null) plot2.Axes.SetLimitsX(minX, maxX);
                if (plot3 != null) plot3.Axes.SetLimitsX(minX, maxX);
            }
            else
            {
                // 데이터가 아직 차기 전에는 0부터 windowSize까지 고정
                if (plot1 != null) plot1.Axes.SetLimitsX(0, windowSize + padding);
                if (plot2 != null) plot2.Axes.SetLimitsX(0, windowSize + padding);
                if (plot3 != null) plot3.Axes.SetLimitsX(0, windowSize + padding);
            }

            formsPlot1.Refresh();

            if (_CheckBox4.Checked)
                timer1.Stop();
        }

        private void ExtruderChart_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            // ChartTypes enum만큼 동적으로 체크박스 생성
            var chartTypes = Enum.GetValues<ChartTypes>();

            flowLayoutPanel1.Controls.Clear();

            foreach (ChartTypes chartType in chartTypes)
            {
                // 체크박스 생성
                var checkbox = new CheckBox
                {
                    Name = chartType.ToString(),
                    Text = chartType.ToString().Replace("_", " "),
                    AutoSize = true,
                    Checked = true, // 기본 체크 상태
                    Margin = new Padding(5)
                };

                // 체크 이벤트 핸들러 연결
                checkbox.CheckedChanged += ChartCheckBox_CheckedChanged;

                // flowLayoutPanel1에 추가
                flowLayoutPanel1.Controls.Add(checkbox);
            }

            InitializeCharts(0);

            //Test
            ActManager.Instance.Act.ChartData.start();
        }

        private void ChartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (loggers == null) return;

            var checkbox = sender as CheckBox;
            if (checkbox == null) return;

            timer1.Stop();

            // 체크박스 이름으로 ChartTypes enum 값을 가져옴
            if (Enum.TryParse<ChartTypes>(checkbox.Name, out ChartTypes chartType))
            {
                int index = (int)chartType;

                // 해당 DataLogger의 표시/숨김 설정
                if (loggers[index] != null)
                {
                    loggers[index].IsVisible = checkbox.Checked;
                    formsPlot1.Refresh();
                }
            }

            timer1.Start();
        }

        private void _CheckBox1_CheckStateChanged(object sender, EventArgs e)
        {
            //_CheckBox1 ~ 3의 체크 상태에 따라 plot1~3의 보이기/숨기기 설정
            InitializeCharts(0);
        }

        private void _CheckBox4_CheckStateChanged(object sender, EventArgs e)
        {
            if(_CheckBox4.Checked)
                timer1.Stop();
            else
                timer1.Start();
        }
    }
}

