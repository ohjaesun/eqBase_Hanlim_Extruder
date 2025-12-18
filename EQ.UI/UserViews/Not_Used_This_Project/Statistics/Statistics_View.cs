using EQ.Core.Service;
using EQ.UI.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace EQ.UI.UserViews
{
    public partial class Statistics_View : UserControlBaseplain
    {
        private readonly LogStatisticsService _service = new LogStatisticsService();

        // 차트 데이터 캐시
        private List<SequenceCycleData> _allCycles;
        private List<SequenceCycleData> _filteredCycles;

        public Statistics_View()
        {
            InitializeComponent();
        }

        private void Statistics_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            _DatePicker.Value = DateTime.Now;
            InitGridColumns();

            _BtnLoad_Click(null, null); //오늘 날짜 자동 실행
        }

        private void InitGridColumns()
        {
            _GridStats.Columns.Clear();
            _GridStats.Columns.Add("Type", "Type");
            _GridStats.Columns.Add("Name", "Name");
            _GridStats.Columns.Add("Count", "Count");
            _GridStats.Columns.Add("Avg", "Avg(ms)");
            _GridStats.Columns.Add("Min", "Min(ms)");
            _GridStats.Columns.Add("Max", "Max(ms)");
            _GridStats.Columns.Add("Total", "Total(ms)");

            _GridStats.Columns["Type"].Width = 80;
            _GridStats.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            for (int i = 2; i < 7; i++)
                _GridStats.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        // [로드 버튼 클릭] 통합 분석 실행
        private async void _BtnLoad_Click(object sender, EventArgs e)
        {
            _BtnLoad.Enabled = false;
            _LblSummary.Text = "Analyzing...";
            _GridStats.Rows.Clear();
            _ChartTrend.Series[0].Points.Clear();
            _ChartStep.Series[0].Points.Clear();
            _ChartPie.Series[0].Points.Clear();
            _ComboSeq.Items.Clear();

            try
            {
                DateTime date = _DatePicker.Value;
                string logPath = Path.Combine(
                    Environment.CurrentDirectory, "Log",
                    date.ToString("yyyy"), date.ToString("MM"), date.ToString("dd"), "Time.txt");

                if (!File.Exists(logPath))
                {
                    _LblSummary.Text = "Log File Not Found.";
                    _LblSummary.ForeColor = Color.Red;
                    return;
                }

                // 1. Summary Analysis (Grid)
                var summary = await Task.Run(() => _service.AnalyzeSummary(logPath));
                foreach (var item in summary)
                {
                    int idx = _GridStats.Rows.Add(item.Type, item.Name, item.Count, item.AvgTime, item.MinTime, item.MaxTime, item.TotalTime);
                    if (item.Type == "SEQUENCE")
                        _GridStats.Rows[idx].DefaultCellStyle.BackColor = Color.AliceBlue;
                }

                // 2. Cycle Analysis (Tact Time Chart)
                _allCycles = await Task.Run(() => _service.AnalyzeCycles(logPath));
                if (_allCycles != null && _allCycles.Count > 0)
                {
                    var seqNames = _allCycles.Select(c => c.SequenceName).Distinct().OrderBy(n => n).ToList();
                    foreach (var name in seqNames) _ComboSeq.Items.Add(name);

                    if (_ComboSeq.Items.Count > 0) _ComboSeq.SelectedIndex = 0;
                }

                // 3. Utilization & Reliability (Pie Chart)
                var utilResult = await Task.Run(() => _service.AnalyzeUtilization(logPath));
                UpdateUtilizationTab(utilResult);

                _LblSummary.Text = $"Complete. (Summary: {summary.Count})";
                _LblSummary.ForeColor = Color.Blue;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                _BtnLoad.Enabled = true;
            }
        }

        // [Tab 2] 시퀀스 선택 시 차트 업데이트
        private void _ComboSeq_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_ComboSeq.SelectedItem == null || _allCycles == null) return;

            string targetSeq = _ComboSeq.SelectedItem.ToString();
            _filteredCycles = _allCycles.Where(c => c.SequenceName == targetSeq).ToList();

            UpdateTrendChart();
        }

        private void UpdateTrendChart()
        {
            var series = _ChartTrend.Series[0];
            series.Points.Clear();
            _ChartStep.Series[0].Points.Clear();

            if (_filteredCycles.Count == 0) return;

            double avg = _filteredCycles.Average(c => c.TotalTime);

            for (int i = 0; i < _filteredCycles.Count; i++)
            {
                var cycle = _filteredCycles[i];
                int ptIdx = series.Points.AddXY(i + 1, cycle.TotalTime);

                series.Points[ptIdx].Tag = cycle;
                series.Points[ptIdx].ToolTip = $"Cycle {i + 1}\n{cycle.TotalTime} ms\n{cycle.Timestamp:HH:mm:ss}";

                if (cycle.TotalTime > avg * 1.5)
                {
                    series.Points[ptIdx].Color = Color.Red;
                    series.Points[ptIdx].MarkerSize = 8;
                }
            }
            _ChartTrend.ChartAreas[0].RecalculateAxesScale();
        }

        // [Tab 2] 드릴다운
        private void _ChartTrend_MouseClick(object sender, MouseEventArgs e)
        {
            var hit = _ChartTrend.HitTest(e.X, e.Y);
            if (hit.ChartElementType == ChartElementType.DataPoint)
            {
                var point = _ChartTrend.Series[0].Points[hit.PointIndex];
                if (point.Tag is SequenceCycleData cycleData)
                {
                    UpdateStepChart(cycleData);
                }
            }
        }

        private void UpdateStepChart(SequenceCycleData data)
        {
            var chartArea = _ChartStep.ChartAreas[0];
            var series = _ChartStep.Series[0];

            series.Points.Clear();
            _ChartStep.Titles[0].Text = $"Step Detail - {data.Timestamp:HH:mm:ss} (Total: {data.TotalTime}ms)";

            // Y축 로그 스케일 처리
            long maxVal = data.Steps.Values.Count > 0 ? data.Steps.Values.Max() : 0;
            long minVal = data.Steps.Values.Count > 0 ? data.Steps.Values.Where(v => v > 0).DefaultIfEmpty(0).Min() : 0;

            if (maxVal > 1000 && minVal > 0 && maxVal / minVal > 10)
            {
                chartArea.AxisY.IsLogarithmic = true;
                chartArea.AxisY.Title = "Duration (ms) [Log Scale]";
            }
            else
            {
                chartArea.AxisY.IsLogarithmic = false;
                chartArea.AxisY.Title = "Duration (ms)";
            }

            // X축 설정
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.LabelStyle.Angle = -45;
            chartArea.AxisX.LabelStyle.Font = new Font("D2Coding", 8F);

            int _step = 1;
            foreach (var step in data.Steps)
            {
                double val = (chartArea.AxisY.IsLogarithmic && step.Value <= 0) ? 0.1 : step.Value;
                // int ptIdx = series.Points.AddXY(step.Key, val);
                int ptIdx = series.Points.AddXY(_step, val);
                _step++;
                series.Points[ptIdx].ToolTip = $"{step.Key}: {step.Value} ms";
                if (step.Value > 1000) series.Points[ptIdx].Color = Color.OrangeRed;
            }
            chartArea.RecalculateAxesScale();
        }

        // [Tab 3] 가동률 업데이트
        private void UpdateUtilizationTab(UtilizationResult data)
        {
            var series = _ChartPie.Series[0];
            series.Points.Clear();

            foreach (var kvp in data.StateDurations)
            {
                if (kvp.Value.TotalSeconds <= 0) continue;

                int ptIdx = series.Points.AddXY(kvp.Key, kvp.Value.TotalMinutes);

                switch (kvp.Key)
                {
                    case "Running": series.Points[ptIdx].Color = Color.LimeGreen; break;
                    case "Idle": series.Points[ptIdx].Color = Color.Gold; break;
                    case "Error": series.Points[ptIdx].Color = Color.Red; break;
                    case "Init": series.Points[ptIdx].Color = Color.Gray; break;
                }

                series.Points[ptIdx].LegendText = $"{kvp.Key} ({kvp.Value:hh\\:mm\\:ss})";
                series.Points[ptIdx].ToolTip = $"{kvp.Key}\nTime: {kvp.Value:hh\\:mm\\:ss}\nRatio: #PERCENT";
            }

            _LabelAvailability.Text = $"Availability: {data.Availability:F2} %";
            _LabelFailCount.Text = $"Fail Count: {data.FailureCount} 회";
            _LabelMTBF.Text = $"MTBF: {data.MTBF}";
            _LabelMTTR.Text = $"MTTR: {data.MTTR}";
        }
    }
}