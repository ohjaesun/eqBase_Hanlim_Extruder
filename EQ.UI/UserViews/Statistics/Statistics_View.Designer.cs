using EQ.UI.Controls;

namespace EQ.UI.UserViews
{
    partial class Statistics_View
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            _LabelTitle = new _Label();
            _PanelControl = new Panel();
            _LblSummary = new _Label();
            _BtnLoad = new _Button();
            _DatePicker = new DateTimePicker();
            _LabelDate = new _Label();
            _TabControl = new TabControl();
            _TabPageSummary = new TabPage();
            _GridStats = new _DataGridView();
            _TabPageChart = new TabPage();
            _SplitContainer = new SplitContainer();
            _ChartTrend = new System.Windows.Forms.DataVisualization.Charting.Chart();
            _ChartStep = new System.Windows.Forms.DataVisualization.Charting.Chart();
            _PanelChartTop = new Panel();
            _ComboSeq = new _ComboBox();
            _LabelSeqSelect = new Label();
            _TabPageUtil = new TabPage();
            _ChartPie = new System.Windows.Forms.DataVisualization.Charting.Chart();
            _PanelUtilInfo = new Panel();
            _LabelFailCount = new _Label();
            _LabelMTTR = new _Label();
            _LabelMTBF = new _Label();
            _LabelAvailability = new _Label();
            _PanelControl.SuspendLayout();
            _TabControl.SuspendLayout();
            _TabPageSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_GridStats).BeginInit();
            _TabPageChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_SplitContainer).BeginInit();
            _SplitContainer.Panel1.SuspendLayout();
            _SplitContainer.Panel2.SuspendLayout();
            _SplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_ChartTrend).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_ChartStep).BeginInit();
            _PanelChartTop.SuspendLayout();
            _TabPageUtil.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_ChartPie).BeginInit();
            _PanelUtilInfo.SuspendLayout();
            SuspendLayout();
            // 
            // _LabelTitle
            // 
            _LabelTitle.BackColor = Color.FromArgb(149, 165, 166);
            _LabelTitle.Dock = DockStyle.Top;
            _LabelTitle.Font = new Font("D2Coding", 12F);
            _LabelTitle.ForeColor = Color.White;
            _LabelTitle.Location = new Point(0, 0);
            _LabelTitle.Name = "_LabelTitle";
            _LabelTitle.Size = new Size(900, 40);
            _LabelTitle.TabIndex = 0;
            _LabelTitle.Text = "Total Analysis View";
            _LabelTitle.TextAlign = ContentAlignment.MiddleCenter;
            _LabelTitle.ThemeStyle = ThemeStyle.Neutral_Gray;
            _LabelTitle.TooltipText = null;
            // 
            // _PanelControl
            // 
            _PanelControl.BackColor = Color.White;
            _PanelControl.Controls.Add(_LblSummary);
            _PanelControl.Controls.Add(_BtnLoad);
            _PanelControl.Controls.Add(_DatePicker);
            _PanelControl.Controls.Add(_LabelDate);
            _PanelControl.Dock = DockStyle.Top;
            _PanelControl.Location = new Point(0, 40);
            _PanelControl.Name = "_PanelControl";
            _PanelControl.Size = new Size(900, 60);
            _PanelControl.TabIndex = 1;
            // 
            // _LblSummary
            // 
            _LblSummary.AutoSize = true;
            _LblSummary.BackColor = SystemColors.Control;
            _LblSummary.Font = new Font("D2Coding", 12F, FontStyle.Bold);
            _LblSummary.ForeColor = SystemColors.ControlText;
            _LblSummary.Location = new Point(390, 22);
            _LblSummary.Name = "_LblSummary";
            _LblSummary.Size = new Size(53, 18);
            _LblSummary.TabIndex = 3;
            _LblSummary.Text = "Ready";
            _LblSummary.ThemeStyle = ThemeStyle.Neutral_Gray;
            _LblSummary.TooltipText = null;
            // 
            // _BtnLoad
            // 
            _BtnLoad.BackColor = Color.FromArgb(52, 73, 94);
            _BtnLoad.Font = new Font("D2Coding", 12F);
            _BtnLoad.ForeColor = Color.White;
            _BtnLoad.Location = new Point(250, 10);
            _BtnLoad.Name = "_BtnLoad";
            _BtnLoad.Size = new Size(120, 40);
            _BtnLoad.TabIndex = 2;
            _BtnLoad.Text = "Analyze";
            _BtnLoad.ThemeStyle = ThemeStyle.Primary_Indigo;
            _BtnLoad.TooltipText = null;
            _BtnLoad.UseVisualStyleBackColor = false;
            _BtnLoad.Click += _BtnLoad_Click;
            // 
            // _DatePicker
            // 
            _DatePicker.Font = new Font("D2Coding", 12F);
            _DatePicker.Format = DateTimePickerFormat.Short;
            _DatePicker.Location = new Point(100, 18);
            _DatePicker.Name = "_DatePicker";
            _DatePicker.Size = new Size(130, 26);
            _DatePicker.TabIndex = 1;
            // 
            // _LabelDate
            // 
            _LabelDate.AutoSize = true;
            _LabelDate.BackColor = SystemColors.Control;
            _LabelDate.Font = new Font("D2Coding", 12F);
            _LabelDate.ForeColor = SystemColors.ControlText;
            _LabelDate.Location = new Point(15, 22);
            _LabelDate.Name = "_LabelDate";
            _LabelDate.Size = new Size(80, 18);
            _LabelDate.TabIndex = 4;
            _LabelDate.Text = "Log Date:";
            _LabelDate.ThemeStyle = ThemeStyle.Neutral_Gray;
            _LabelDate.TooltipText = null;
            // 
            // _TabControl
            // 
            _TabControl.Appearance = TabAppearance.Buttons;
            _TabControl.Controls.Add(_TabPageSummary);
            _TabControl.Controls.Add(_TabPageChart);
            _TabControl.Controls.Add(_TabPageUtil);
            _TabControl.Dock = DockStyle.Fill;
            _TabControl.Font = new Font("D2Coding", 12F);
            _TabControl.Location = new Point(0, 100);
            _TabControl.Name = "_TabControl";
            _TabControl.SelectedIndex = 0;
            _TabControl.Size = new Size(900, 500);
            _TabControl.TabIndex = 2;
            // 
            // _TabPageSummary
            // 
            _TabPageSummary.Controls.Add(_GridStats);
            _TabPageSummary.Location = new Point(4, 30);
            _TabPageSummary.Name = "_TabPageSummary";
            _TabPageSummary.Padding = new Padding(3);
            _TabPageSummary.Size = new Size(892, 466);
            _TabPageSummary.TabIndex = 0;
            _TabPageSummary.Text = "Summary";
            _TabPageSummary.UseVisualStyleBackColor = true;
            // 
            // _GridStats
            // 
            _GridStats.AllowUserToAddRows = false;
            _GridStats.AllowUserToDeleteRows = false;
            _GridStats.AllowUserToResizeRows = false;
            _GridStats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _GridStats.BackgroundColor = Color.FromArgb(255, 255, 225);
            _GridStats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(255, 255, 225);
            dataGridViewCellStyle1.Font = new Font("D2Coding", 12F);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            _GridStats.DefaultCellStyle = dataGridViewCellStyle1;
            _GridStats.Dock = DockStyle.Fill;
            _GridStats.Font = new Font("D2Coding", 12F);
            _GridStats.Location = new Point(3, 3);
            _GridStats.Name = "_GridStats";
            _GridStats.ReadOnly = true;
            _GridStats.RowHeadersVisible = false;
            _GridStats.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _GridStats.Size = new Size(886, 460);
            _GridStats.TabIndex = 0;
            _GridStats.ThemeStyle = ThemeStyle.Display_LightYellow;
            // 
            // _TabPageChart
            // 
            _TabPageChart.Controls.Add(_SplitContainer);
            _TabPageChart.Controls.Add(_PanelChartTop);
            _TabPageChart.Location = new Point(4, 30);
            _TabPageChart.Name = "_TabPageChart";
            _TabPageChart.Padding = new Padding(3);
            _TabPageChart.Size = new Size(892, 466);
            _TabPageChart.TabIndex = 1;
            _TabPageChart.Text = "Tact Time";
            _TabPageChart.UseVisualStyleBackColor = true;
            // 
            // _SplitContainer
            // 
            _SplitContainer.Dock = DockStyle.Fill;
            _SplitContainer.Location = new Point(3, 43);
            _SplitContainer.Name = "_SplitContainer";
            _SplitContainer.Orientation = Orientation.Horizontal;
            // 
            // _SplitContainer.Panel1
            // 
            _SplitContainer.Panel1.Controls.Add(_ChartTrend);
            // 
            // _SplitContainer.Panel2
            // 
            _SplitContainer.Panel2.Controls.Add(_ChartStep);
            _SplitContainer.Size = new Size(886, 420);
            _SplitContainer.SplitterDistance = 208;
            _SplitContainer.TabIndex = 1;
            // 
            // _ChartTrend
            // 
            chartArea1.AxisX.Title = "Cycle Count";
            chartArea1.AxisY.Title = "Total Time (ms)";
            chartArea1.Name = "AreaTrend";
            _ChartTrend.ChartAreas.Add(chartArea1);
            _ChartTrend.Dock = DockStyle.Fill;
            legend1.Name = "LegendTrend";
            _ChartTrend.Legends.Add(legend1);
            _ChartTrend.Location = new Point(0, 0);
            _ChartTrend.Name = "_ChartTrend";
            series1.BorderWidth = 2;
            series1.ChartArea = "AreaTrend";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "LegendTrend";
            series1.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
            series1.Name = "TactTime";
            _ChartTrend.Series.Add(series1);
            _ChartTrend.Size = new Size(886, 208);
            _ChartTrend.TabIndex = 0;
            _ChartTrend.Text = "Trend Chart";
            title1.Name = "Title1";
            title1.Text = "Tact Time Trend";
            _ChartTrend.Titles.Add(title1);
            _ChartTrend.MouseClick += _ChartTrend_MouseClick;
            // 
            // _ChartStep
            // 
            chartArea2.AxisX.Title = "Step Name";
            chartArea2.AxisY.Title = "Duration (ms)";
            chartArea2.Name = "AreaStep";
            _ChartStep.ChartAreas.Add(chartArea2);
            _ChartStep.Dock = DockStyle.Fill;
            legend2.Name = "LegendStep";
            _ChartStep.Legends.Add(legend2);
            _ChartStep.Location = new Point(0, 0);
            _ChartStep.Name = "_ChartStep";
            series2.ChartArea = "AreaStep";
            series2.IsValueShownAsLabel = true;
            series2.Legend = "LegendStep";
            series2.Name = "StepTime";
            _ChartStep.Series.Add(series2);
            _ChartStep.Size = new Size(886, 208);
            _ChartStep.TabIndex = 0;
            _ChartStep.Text = "Step Chart";
            title2.Name = "Title1";
            title2.Text = "Step Detail (Click Trend Point)";
            _ChartStep.Titles.Add(title2);
            // 
            // _PanelChartTop
            // 
            _PanelChartTop.Controls.Add(_ComboSeq);
            _PanelChartTop.Controls.Add(_LabelSeqSelect);
            _PanelChartTop.Dock = DockStyle.Top;
            _PanelChartTop.Location = new Point(3, 3);
            _PanelChartTop.Name = "_PanelChartTop";
            _PanelChartTop.Size = new Size(886, 40);
            _PanelChartTop.TabIndex = 0;
            // 
            // _ComboSeq
            // 
            _ComboSeq.BackColor = Color.FromArgb(155, 89, 182);
            _ComboSeq.DrawMode = DrawMode.OwnerDrawFixed;
            _ComboSeq.DropDownStyle = ComboBoxStyle.DropDownList;
            _ComboSeq.Font = new Font("D2Coding", 12F);
            _ComboSeq.ForeColor = Color.Black;
            _ComboSeq.Location = new Point(160, 8);
            _ComboSeq.Name = "_ComboSeq";
            _ComboSeq.Size = new Size(250, 27);
            _ComboSeq.TabIndex = 0;
            _ComboSeq.ThemeStyle = ThemeStyle.Highlight_DeepYellow;
            _ComboSeq.TooltipText = null;
            _ComboSeq.SelectedIndexChanged += _ComboSeq_SelectedIndexChanged;
            // 
            // _LabelSeqSelect
            // 
            _LabelSeqSelect.AutoSize = true;
            _LabelSeqSelect.Location = new Point(10, 11);
            _LabelSeqSelect.Name = "_LabelSeqSelect";
            _LabelSeqSelect.Size = new Size(136, 18);
            _LabelSeqSelect.TabIndex = 1;
            _LabelSeqSelect.Text = "Target Sequence:";
            // 
            // _TabPageUtil
            // 
            _TabPageUtil.Controls.Add(_ChartPie);
            _TabPageUtil.Controls.Add(_PanelUtilInfo);
            _TabPageUtil.Location = new Point(4, 30);
            _TabPageUtil.Name = "_TabPageUtil";
            _TabPageUtil.Padding = new Padding(3);
            _TabPageUtil.Size = new Size(892, 466);
            _TabPageUtil.TabIndex = 2;
            _TabPageUtil.Text = "MTBF";
            _TabPageUtil.UseVisualStyleBackColor = true;
            // 
            // _ChartPie
            // 
            chartArea3.Name = "AreaPie";
            _ChartPie.ChartAreas.Add(chartArea3);
            _ChartPie.Dock = DockStyle.Fill;
            legend3.Name = "LegendPie";
            _ChartPie.Legends.Add(legend3);
            _ChartPie.Location = new Point(3, 3);
            _ChartPie.Name = "_ChartPie";
            series3.ChartArea = "AreaPie";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series3.IsValueShownAsLabel = true;
            series3.LabelFormat = "#PERCENT";
            series3.Legend = "LegendPie";
            series3.Name = "StateSeries";
            _ChartPie.Series.Add(series3);
            _ChartPie.Size = new Size(886, 289);
            _ChartPie.TabIndex = 0;
            title3.Name = "Title1";
            title3.Text = "Equipment State Distribution";
            _ChartPie.Titles.Add(title3);
            // 
            // _PanelUtilInfo
            // 
            _PanelUtilInfo.BackColor = Color.WhiteSmoke;
            _PanelUtilInfo.Controls.Add(_LabelFailCount);
            _PanelUtilInfo.Controls.Add(_LabelMTTR);
            _PanelUtilInfo.Controls.Add(_LabelMTBF);
            _PanelUtilInfo.Controls.Add(_LabelAvailability);
            _PanelUtilInfo.Dock = DockStyle.Bottom;
            _PanelUtilInfo.Location = new Point(3, 292);
            _PanelUtilInfo.Name = "_PanelUtilInfo";
            _PanelUtilInfo.Size = new Size(886, 171);
            _PanelUtilInfo.TabIndex = 1;
            // 
            // _LabelFailCount
            // 
            _LabelFailCount.AutoSize = true;
            _LabelFailCount.BackColor = SystemColors.Control;
            _LabelFailCount.Font = new Font("D2Coding", 12F);
            _LabelFailCount.ForeColor = SystemColors.ControlText;
            _LabelFailCount.Location = new Point(30, 60);
            _LabelFailCount.Name = "_LabelFailCount";
            _LabelFailCount.Size = new Size(112, 18);
            _LabelFailCount.TabIndex = 1;
            _LabelFailCount.Text = "Fail Count: 0";
            _LabelFailCount.ThemeStyle = ThemeStyle.Neutral_Gray;
            _LabelFailCount.TooltipText = null;
            // 
            // _LabelMTTR
            // 
            _LabelMTTR.AutoSize = true;
            _LabelMTTR.BackColor = SystemColors.Control;
            _LabelMTTR.Font = new Font("D2Coding", 12F);
            _LabelMTTR.ForeColor = SystemColors.ControlText;
            _LabelMTTR.Location = new Point(400, 60);
            _LabelMTTR.Name = "_LabelMTTR";
            _LabelMTTR.Size = new Size(64, 18);
            _LabelMTTR.TabIndex = 3;
            _LabelMTTR.Text = "MTTR: -";
            _LabelMTTR.ThemeStyle = ThemeStyle.Neutral_Gray;
            _LabelMTTR.TooltipText = null;
            // 
            // _LabelMTBF
            // 
            _LabelMTBF.AutoSize = true;
            _LabelMTBF.BackColor = SystemColors.Control;
            _LabelMTBF.Font = new Font("D2Coding", 12F);
            _LabelMTBF.ForeColor = SystemColors.ControlText;
            _LabelMTBF.Location = new Point(400, 24);
            _LabelMTBF.Name = "_LabelMTBF";
            _LabelMTBF.Size = new Size(64, 18);
            _LabelMTBF.TabIndex = 2;
            _LabelMTBF.Text = "MTBF: -";
            _LabelMTBF.ThemeStyle = ThemeStyle.Neutral_Gray;
            _LabelMTBF.TooltipText = null;
            // 
            // _LabelAvailability
            // 
            _LabelAvailability.AutoSize = true;
            _LabelAvailability.BackColor = SystemColors.Control;
            _LabelAvailability.Font = new Font("D2Coding", 14F, FontStyle.Bold);
            _LabelAvailability.ForeColor = SystemColors.ControlText;
            _LabelAvailability.Location = new Point(30, 20);
            _LabelAvailability.Name = "_LabelAvailability";
            _LabelAvailability.Size = new Size(180, 22);
            _LabelAvailability.TabIndex = 0;
            _LabelAvailability.Text = "Availability: - %";
            _LabelAvailability.ThemeStyle = ThemeStyle.Neutral_Gray;
            _LabelAvailability.TooltipText = null;
            // 
            // Statistics_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(_TabControl);
            Controls.Add(_PanelControl);
            Controls.Add(_LabelTitle);
            Name = "Statistics_View";
            Size = new Size(900, 600);
            Load += Statistics_View_Load;
            _PanelControl.ResumeLayout(false);
            _PanelControl.PerformLayout();
            _TabControl.ResumeLayout(false);
            _TabPageSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_GridStats).EndInit();
            _TabPageChart.ResumeLayout(false);
            _SplitContainer.Panel1.ResumeLayout(false);
            _SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_SplitContainer).EndInit();
            _SplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_ChartTrend).EndInit();
            ((System.ComponentModel.ISupportInitialize)_ChartStep).EndInit();
            _PanelChartTop.ResumeLayout(false);
            _PanelChartTop.PerformLayout();
            _TabPageUtil.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_ChartPie).EndInit();
            _PanelUtilInfo.ResumeLayout(false);
            _PanelUtilInfo.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private EQ.UI.Controls._Label _LabelTitle;
        private System.Windows.Forms.Panel _PanelControl;
        private EQ.UI.Controls._Button _BtnLoad;
        private System.Windows.Forms.DateTimePicker _DatePicker;
        private EQ.UI.Controls._Label _LabelDate;
        private EQ.UI.Controls._Label _LblSummary;

        private System.Windows.Forms.TabControl _TabControl;

        // Tab 1
        private System.Windows.Forms.TabPage _TabPageSummary;
        private EQ.UI.Controls._DataGridView _GridStats;

        // Tab 2
        private System.Windows.Forms.TabPage _TabPageChart;
        private System.Windows.Forms.SplitContainer _SplitContainer;
        private System.Windows.Forms.Panel _PanelChartTop;
        private System.Windows.Forms.Label _LabelSeqSelect;
        private EQ.UI.Controls._ComboBox _ComboSeq;
        private System.Windows.Forms.DataVisualization.Charting.Chart _ChartTrend;
        private System.Windows.Forms.DataVisualization.Charting.Chart _ChartStep;

        // Tab 3
        private System.Windows.Forms.TabPage _TabPageUtil;
        private System.Windows.Forms.DataVisualization.Charting.Chart _ChartPie;
        private System.Windows.Forms.Panel _PanelUtilInfo;
        private EQ.UI.Controls._Label _LabelMTTR;
        private EQ.UI.Controls._Label _LabelMTBF;
        private EQ.UI.Controls._Label _LabelFailCount;
        private EQ.UI.Controls._Label _LabelAvailability;
    }
}