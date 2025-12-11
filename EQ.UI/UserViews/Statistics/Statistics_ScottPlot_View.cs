using EQ.Core.Service;
using EQ.UI.Controls;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Color = System.Drawing.Color;

namespace EQ.UI.UserViews
{
    public partial class Statistics_ScottPlot_View : UserControlBaseplain
    {
        private readonly LogStatisticsService _service = new LogStatisticsService();

        // 차트 데이터 캐시
        private List<SequenceCycleData> _allCycles;
        private List<SequenceCycleData> _filteredCycles;
        private List<StateTimelineItem> _stateTimeline;

        public Statistics_ScottPlot_View()
        {
            InitializeComponent();
        }

        private void Statistics_ScottPlot_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            _DatePicker.Value = DateTime.Now;
            InitGridColumns();

            // 오늘 날짜 자동 실행
            _BtnLoad_Click(null, null);
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

            // UI 초기화
            _GridStats.Rows.Clear();
            _formsPlotTrend.Plot.Clear();
            _formsPlotStep.Plot.Clear();
            _formsPlotPie.Plot.Clear();
            _formsPlotTiming.Plot.Clear(); // [추가] 타이밍 차트 초기화
            _ComboSeq.Items.Clear();

            // ScottPlot UI 갱신
            _formsPlotTrend.Refresh();
            _formsPlotStep.Refresh();
            _formsPlotPie.Refresh();
            _formsPlotTiming.Refresh();

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

                // 2. Cycle Analysis (Tact Time Chart & Timing Chart)
                _stateTimeline = await Task.Run(() => _service.GetStateTimeline(logPath));

                _allCycles = await Task.Run(() => _service.AnalyzeCycles(logPath));
                if (_allCycles != null && _allCycles.Count > 0)
                {
                    var seqNames = _allCycles.Select(c => c.SequenceName).Distinct().OrderBy(n => n).ToList();
                    foreach (var name in seqNames) _ComboSeq.Items.Add(name);

                    if (_ComboSeq.Items.Count > 0) _ComboSeq.SelectedIndex = 0;

                    // [추가] Timing Chart 업데이트 호출
                    UpdateTimingChart();
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
            _formsPlotTrend.Plot.Clear();
            _formsPlotStep.Plot.Clear();

            if (_filteredCycles == null || _filteredCycles.Count == 0)
            {
                _formsPlotTrend.Refresh();
                return;
            }

            // 데이터 준비
            double[] xs = Enumerable.Range(1, _filteredCycles.Count).Select(i => (double)i).ToArray();
            double[] ys = _filteredCycles.Select(c => (double)c.TotalTime).ToArray();
            double avg = ys.Average();

            // 1. Scatter Plot 추가
            var sp = _formsPlotTrend.Plot.Add.Scatter(xs, ys);
            sp.LineWidth = 2;
            sp.Color = ScottPlot.Colors.Blue;
            sp.MarkerSize = 15;

            // 2. 기준선(평균) 추가
            var hLine = _formsPlotTrend.Plot.Add.HorizontalLine(avg);
            hLine.LinePattern = LinePattern.Dashed;
            hLine.Color = ScottPlot.Colors.Green;
            hLine.Label.Text = $"Avg: {avg:F1} ms";
            hLine.Label.FontSize = 14;

            // 3. 이상치(평균의 1.5배) 강조
            List<double> highX = new List<double>();
            List<double> highY = new List<double>();
            for (int i = 0; i < ys.Length; i++)
            {
                if (ys[i] > avg * 1.5)
                {
                    highX.Add(xs[i]);
                    highY.Add(ys[i]);
                }
            }

            if (highX.Count > 0)
            {
                var spHigh = _formsPlotTrend.Plot.Add.Scatter(highX.ToArray(), highY.ToArray());
                spHigh.LineWidth = 0;
                spHigh.MarkerShape = MarkerShape.FilledCircle;
                spHigh.Color = ScottPlot.Colors.Red;
                spHigh.MarkerSize = 8;
            }

            // 4. 스타일링
            _formsPlotTrend.Plot.Title($"Tact Time Trend (Count: {_filteredCycles.Count})");
            _formsPlotTrend.Plot.Axes.Bottom.Label.Text = "Cycle Count";
            _formsPlotTrend.Plot.Axes.Left.Label.Text = "Total Time (ms)";

            // [수정] X축 정수 틱 강제 (5.1.57 호환)
            ScottPlot.TickGenerators.NumericAutomatic tickGen = new ScottPlot.TickGenerators.NumericAutomatic();
            tickGen.IntegerTicksOnly = true;
            _formsPlotTrend.Plot.Axes.Bottom.TickGenerator = tickGen;

            _formsPlotTrend.Plot.Axes.AutoScale();
            _formsPlotTrend.Refresh();
        }

        // [Tab 2] 마우스 클릭 이벤트 (Drill Down) - 5.1.57 호환
        private void _formsPlotTrend_MouseDown(object sender, MouseEventArgs e)
        {
            if (_filteredCycles == null || _filteredCycles.Count == 0) return;

            // 1. 마우스 좌표 변환
            Pixel mousePixel = new Pixel(e.X, e.Y);
            Coordinates mouseLocation = _formsPlotTrend.Plot.GetCoordinates(mousePixel);

            // 2. Scatter 플롯들 중에서 가장 가까운 점 찾기
            var scatters = _formsPlotTrend.Plot.GetPlottables<ScottPlot.Plottables.Scatter>();

            foreach (var scatter in scatters)
            {
                // GetNearest는 데이터 좌표 상에서 가장 가까운 점(DataPoint)을 반환
                var nearest = scatter.Data.GetNearest(mouseLocation, _formsPlotTrend.Plot.LastRender);

                // [수정] DistanceTo 없음 -> 픽셀 거리 직접 계산
                if (nearest.IsReal)
                {
                    Pixel nearestPixel = _formsPlotTrend.Plot.GetPixel(nearest.Coordinates);
                    double distance = Math.Sqrt(Math.Pow(mousePixel.X - nearestPixel.X, 2) + Math.Pow(mousePixel.Y - nearestPixel.Y, 2));

                    if (distance < 10)
                    {
                        // X축은 1부터 시작하므로 index = X - 1
                        int index = (int)Math.Round(nearest.X) - 1;

                        if (index >= 0 && index < _filteredCycles.Count)
                        {
                            UpdateStepChart(_filteredCycles[index]);
                            return;
                        }
                    }
                }
            }
        }

        private void UpdateStepChart(SequenceCycleData data)
        {
            _formsPlotStep.Plot.Clear();

            var stepNames = data.Steps.Keys.ToArray();
            var durations = data.Steps.Values.Select(v => (double)v).ToArray();

            if (stepNames.Length == 0) return;

            // Bar Chart
            var bars = _formsPlotStep.Plot.Add.Bars(durations);

            foreach (var bar in bars.Bars)
            {
                bar.FillColor = (bar.Value > 1000) ? ScottPlot.Colors.OrangeRed : ScottPlot.Colors.CornflowerBlue;

                // [수정] Text.Label.xxx -> Text.LabelXxx 속성 직접 사용 (5.1.57 호환)
                var txt = _formsPlotStep.Plot.Add.Text(bar.Value.ToString("0"), bar.Position, bar.Value);
                txt.LabelAlignment = Alignment.LowerCenter;
                txt.LabelFontColor = ScottPlot.Colors.Black;
                txt.LabelFontSize = 11;
            }

            // X축 커스텀 레이블
            ScottPlot.TickGenerators.NumericManual tickGen = new ScottPlot.TickGenerators.NumericManual();
            for (int i = 0; i < stepNames.Length; i++)
            {
                tickGen.AddMajor(i, stepNames[i]);
            }
            _formsPlotStep.Plot.Axes.Bottom.TickGenerator = tickGen;

            // 스타일링
            _formsPlotStep.Plot.Title($"Step Detail - {data.Timestamp:HH:mm:ss} (Total: {data.TotalTime}ms)");
            _formsPlotStep.Plot.Axes.Left.Label.Text = "Duration (ms)";
            _formsPlotStep.Plot.Axes.Bottom.TickLabelStyle.Rotation = -45;
            _formsPlotStep.Plot.Axes.Bottom.TickLabelStyle.Alignment = Alignment.MiddleRight;

            // Y축 여유 공간 (텍스트 짤림 방지)
            if (durations.Length > 0)
            {
                _formsPlotStep.Plot.Axes.SetLimitsY(0, durations.Max() * 1.15);
            }

            _formsPlotStep.Plot.Axes.AutoScaleX();
            _formsPlotStep.Refresh();
        }

        // [Tab 3] 가동률 Pie Chart 업데이트 - 5.1.57 호환
        private void UpdateUtilizationTab(UtilizationResult data)
        {
            _formsPlotPie.Plot.Clear();

            List<PieSlice> slices = new List<PieSlice>();
            double totalSeconds = data.StateDurations.Values.Sum(t => t.TotalSeconds);

            if (totalSeconds <= 0) return;

            foreach (var kvp in data.StateDurations)
            {
                if (kvp.Value.TotalSeconds <= 0) continue;

                ScottPlot.Color sliceColor = kvp.Key switch
                {
                    "Running" => ScottPlot.Colors.LimeGreen,
                    "Idle" => ScottPlot.Colors.Gold,
                    "Error" => ScottPlot.Colors.Red,
                    "Init" => ScottPlot.Colors.Gray,
                    _ => ScottPlot.Colors.LightGray
                };

                var slice = new PieSlice()
                {
                    Value = kvp.Value.TotalMinutes,
                    FillColor = sliceColor,
                    Label = $"{kvp.Key}\n{(kvp.Value.TotalSeconds / totalSeconds):P1}"
                };
                slices.Add(slice);
            }

            var pie = _formsPlotPie.Plot.Add.Pie(slices);
            // [수정] ShowSliceLabels 제거 (자동 표시)
            pie.SliceLabelDistance = 1.2;

            _formsPlotPie.Plot.Title("Equipment State Distribution");

            // [수정] HideAxesAndGrid 대체 (5.x 호환)
            _formsPlotPie.Plot.HideGrid();
            _formsPlotPie.Plot.Axes.Bottom.IsVisible = false;
            _formsPlotPie.Plot.Axes.Left.IsVisible = false;
            _formsPlotPie.Plot.Axes.Right.IsVisible = false;
            _formsPlotPie.Plot.Axes.Top.IsVisible = false;

            _formsPlotPie.Refresh();

            _LabelAvailability.Text = $"Availability: {data.Availability:F2} %";
            _LabelFailCount.Text = $"Fail Count: {data.FailureCount} 회";
            _LabelMTBF.Text = $"MTBF: {data.MTBF}";
            _LabelMTTR.Text = $"MTTR: {data.MTTR}";
        }

        // [Tab 4] Timing Chart (Gantt) 업데이트 - [신규 추가]
        private void UpdateTimingChart()
        {
            _formsPlotTiming.Plot.Clear();
            // 기존 규칙 초기화 (중복 추가 방지)
            _formsPlotTiming.Plot.Axes.Rules.Clear();

            if (_allCycles == null || _allCycles.Count == 0)
            {
                _formsPlotTiming.Refresh();
                return;
            }

            // 1. Y축 라벨 준비 (시퀀스 이름 목록)
            var distinctSeqNames = _allCycles
                .Select(c => c.SequenceName)
                .Distinct()
                .OrderBy(n => n)
                .ToList();

            if (distinctSeqNames.Count == 0) return;

            int statusRowIndex = distinctSeqNames.Count;

            // 2. 데이터 플로팅 (Gantt Bar)
            foreach (var cycle in _allCycles)
            {
                int yIndex = distinctSeqNames.IndexOf(cycle.SequenceName);
                if (yIndex < 0) continue;

                DateTime endTime = cycle.Timestamp;
                DateTime startTime = cycle.Timestamp.AddMilliseconds(-cycle.TotalTime);

                double xStart = startTime.ToOADate();
                double xEnd = endTime.ToOADate();

                // 사각형 그리기 (Y축 중심으로 높이 0.8)
                var bar = _formsPlotTiming.Plot.Add.Rectangle(xStart, xEnd, yIndex - 0.4, yIndex + 0.4);

                bar.FillStyle.Color = ScottPlot.Colors.CornflowerBlue.WithAlpha(150);
                bar.LineStyle.Color = ScottPlot.Colors.Blue;
                bar.LineStyle.Width = 1;

                // [추가됨] 바 안에 시간 텍스트 표시
                // X좌표: 시작과 끝의 중간, Y좌표: 바의 중심(yIndex)
                double xCenter = (xStart + xEnd) / 2;
                var txt = _formsPlotTiming.Plot.Add.Text($"{cycle.TotalTime}ms", xCenter, yIndex);

                // 텍스트 스타일 (5.1.57 호환)
                txt.LabelAlignment = ScottPlot.Alignment.MiddleCenter; // 정중앙 정렬
                txt.LabelFontColor = ScottPlot.Colors.White;           // 파란 배경 위 흰색 글씨
                txt.LabelFontSize = 10;                                // 글자 크기
                txt.LabelBold = true;
            }

            if (_stateTimeline != null && _stateTimeline.Count > 0)
            {
                // 시간순 정렬
                var sortedStates = _stateTimeline.OrderBy(s => s.Timestamp).ToList();

                for (int i = 0; i < sortedStates.Count; i++)
                {
                    var current = sortedStates[i];

                    // 다음 상태 시작 시간까지를 현재 상태의 유지 시간으로 간주
                    // (마지막 상태는 임의로 5초 정도 보여주거나 로그 끝까지로 설정)
                    DateTime start = current.Timestamp;
                    DateTime end = (i < sortedStates.Count - 1)
                        ? sortedStates[i + 1].Timestamp
                        : start.AddSeconds(5);

                    double xStart = start.ToOADate();
                    double xEnd = end.ToOADate();

                    // 상태별 색상 지정
                    ScottPlot.Color stateColor = current.State switch
                    {
                        "Running" => ScottPlot.Colors.LimeGreen,
                        "Idle" => ScottPlot.Colors.Gold,
                        "Error" => ScottPlot.Colors.Red,
                        "Init" => ScottPlot.Colors.Gray,
                        _ => ScottPlot.Colors.LightGray
                    };

                    // Status 행(statusRowIndex)에 그리기
                    var bar = _formsPlotTiming.Plot.Add.Rectangle(xStart, xEnd, statusRowIndex - 0.4, statusRowIndex + 0.4);
                    bar.FillStyle.Color = stateColor;
                    bar.LineStyle.Width = 0; // 테두리 없음

                    // 상태 텍스트 표시 (바 가운데)
                    double durationSec = (end - start).TotalSeconds;
                    // 너무 짧은 구간은 텍스트 생략 (예: 1초 미만)
                    if (durationSec > 0.5 && false)
                    {
                        double xCenter = (xStart + xEnd) / 2;
                        var txt = _formsPlotTiming.Plot.Add.Text(current.State, xCenter, statusRowIndex);
                        txt.LabelAlignment = Alignment.MiddleCenter;
                        txt.LabelFontColor = ScottPlot.Colors.Black;
                        txt.LabelFontSize = 10;
                    }
                }
            }

            // 3. Y축 커스텀 설정 (시퀀스 이름 표시)
            ScottPlot.TickGenerators.NumericManual yTicker = new ScottPlot.TickGenerators.NumericManual();
            for (int i = 0; i < distinctSeqNames.Count; i++)
            {
                yTicker.AddMajor(i, distinctSeqNames[i]);
            }
            _formsPlotTiming.Plot.Axes.Left.TickGenerator = yTicker;


            // 4. X축 설정 (DateTime)
            _formsPlotTiming.Plot.Axes.DateTimeTicksBottom();

            // 스타일링
            _formsPlotTiming.Plot.Title("Sequence Timing Chart (Parallel Execution)");
            _formsPlotTiming.Plot.Axes.Bottom.Label.Text = "Time";
            _formsPlotTiming.Plot.Axes.Left.Label.Text = "Sequence Name";

            // 그리드 설정
            _formsPlotTiming.Plot.Grid.MajorLineColor = ScottPlot.Colors.LightGray.WithAlpha(100);
            _formsPlotTiming.Plot.Grid.MinorLineColor = ScottPlot.Colors.LightGray.WithAlpha(50);

            // 5. 축 범위 및 규칙 설정
            // X축은 데이터에 맞게 자동 조정
            _formsPlotTiming.Plot.Axes.AutoScaleX();

            // Y축 잠금 규칙 추가 (생성자에 범위 전달: min, max)
            var yAxis = _formsPlotTiming.Plot.Axes.Left;
            var lockedRule = new ScottPlot.AxisRules.LockedVertical(yAxis, -1, distinctSeqNames.Count);
            _formsPlotTiming.Plot.Axes.Rules.Add(lockedRule);

            _formsPlotTiming.Refresh();
        }
    }
}