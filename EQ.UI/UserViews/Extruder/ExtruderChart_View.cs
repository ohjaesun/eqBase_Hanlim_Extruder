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


        int[] lastIndex;
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
            lastIndex = new int[cnt];

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
                loggers[(int)ChartTypes.FEEDER_RPM].Axes.YAxis = plot.Axes.Left;

                loggers[(int)ChartTypes.EXTRUDER_RPM] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.EXTRUDER_RPM].Axes.YAxis = plot.Axes.Left;

                loggers[(int)ChartTypes.PULLER_RPM] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.PULLER_RPM].Axes.YAxis = plot.Axes.Left;

                loggers[(int)ChartTypes.FEEDER_SPEED] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.FEEDER_SPEED].Axes.YAxis = plot.Axes.Right;

                loggers[(int)ChartTypes.EXTRUDER_SPEED] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.EXTRUDER_SPEED].Axes.YAxis = plot.Axes.Right;

                loggers[(int)ChartTypes.PULLER_SPEED] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.PULLER_SPEED].Axes.YAxis = plot.Axes.Right;

                loggers[(int)ChartTypes.FEEDER_TRQ] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.FEEDER_TRQ].Axes.YAxis = rightAxis2;

                loggers[(int)ChartTypes.EXTRUDER_TRQ] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.EXTRUDER_TRQ].Axes.YAxis = rightAxis2;

                loggers[(int)ChartTypes.PULLER_TRQ] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.PULLER_TRQ].Axes.YAxis = rightAxis2;


            }

            // ======================
            // 2번 차트 (Temperature)
            // ======================
            {
                var plot = plot2;
                plot.Axes.Left.Label.Text = "Temp";

                var rightAxis = plot.Axes.AddRightAxis();
                rightAxis.LabelText = "Temp";

                var rightAxis2 = plot.Axes.AddRightAxis();
                rightAxis2.LabelText = "Temp";

                loggers[(int)ChartTypes.ZONE1_TEMP_PV] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.ZONE1_TEMP_PV].Axes.YAxis = plot.Axes.Left;


                loggers[(int)ChartTypes.ZONE1_TEMP_SV] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.ZONE1_TEMP_SV].Axes.YAxis = plot.Axes.Left;

                loggers[(int)ChartTypes.ZONE2_TEMP_PV] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.ZONE2_TEMP_PV].Axes.YAxis = plot.Axes.Left;

                loggers[(int)ChartTypes.ZONE2_TEMP_SV] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.ZONE2_TEMP_SV].Axes.YAxis = plot.Axes.Left;

                loggers[(int)ChartTypes.ZONE3_TEMP_PV] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.ZONE3_TEMP_PV].Axes.YAxis = plot.Axes.Right;

                loggers[(int)ChartTypes.ZONE3_TEMP_SV] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.ZONE3_TEMP_SV].Axes.YAxis = plot.Axes.Right;

                loggers[(int)ChartTypes.ZONE4_TEMP_PV] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.ZONE4_TEMP_PV].Axes.YAxis = rightAxis2;

                loggers[(int)ChartTypes.ZONE4_TEMP_SV] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.ZONE4_TEMP_SV].Axes.YAxis = rightAxis2;


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
                loggers[(int)ChartTypes.DIAMETER].Axes.YAxis = plot.Axes.Left;

                loggers[(int)ChartTypes.OVALITY] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.OVALITY].Axes.YAxis = plot.Axes.Right;

                loggers[(int)ChartTypes.GOOD_COUNT] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.GOOD_COUNT].Axes.YAxis = rightAxis2;

                loggers[(int)ChartTypes.BAD_COUNT] = plot.Add.DataLogger();
                loggers[(int)ChartTypes.BAD_COUNT].Axes.YAxis = rightAxis2;


            }

            // X축을 시간으로 표시
            plot1.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.DateTimeAutomatic();
            plot2.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.DateTimeAutomatic();
            plot3.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.DateTimeAutomatic();



            formsPlot1.Refresh();

            timer1.Interval = 1000;
            timer1.Start();

            _axisInitialized = false;
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

        private bool _axisInitialized = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            bool hasNewData = false;
            int cnt = Enum.GetValues<ChartTypes>().Length;

            double x = DateTime.Now.ToOADate(); // 시간 X축

            for (int i = 0; i < cnt; i++)
            {
                var newData = ActManager.Instance.Act.ChartData
                    .GetDataSince((ChartTypes)i, lastIndex[i]);

                if (newData.Count == 0)
                    continue;

                foreach (var d in newData)
                {
                    // X축: Timestamp를 OADate로 변환 (DateTime 좌표)
                    double x2 = d.Timestamp.ToOADate();
                    var d2 = d.Value;
                    loggers[i].Add(x2, d2);
                }

                lastIndex[i] += newData.Count;
                hasNewData = true;
            }

            formsPlot1.Refresh();


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
        }

        private void _CheckBox1_CheckStateChanged(object sender, EventArgs e)
        {
            //_CheckBox1 ~ 3의 체크 상태에 따라 plot1~3의 보이기/숨기기 설정
            InitializeCharts(0);
        }
    }
}

