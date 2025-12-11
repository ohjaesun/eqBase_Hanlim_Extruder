using EQ.UI.Controls;

namespace EQ.UI.UserViews
{
    partial class Statistics_ScottPlot_View
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this._LabelTitle = new EQ.UI.Controls._Label();
            this._PanelControl = new System.Windows.Forms.Panel();
            this._LblSummary = new EQ.UI.Controls._Label();
            this._BtnLoad = new EQ.UI.Controls._Button();
            this._DatePicker = new System.Windows.Forms.DateTimePicker();
            this._LabelDate = new EQ.UI.Controls._Label();
            this._TabControl = new System.Windows.Forms.TabControl();
            this._TabPageSummary = new System.Windows.Forms.TabPage();
            this._GridStats = new EQ.UI.Controls._DataGridView();
            this._TabPageChart = new System.Windows.Forms.TabPage();
            this._SplitContainer = new System.Windows.Forms.SplitContainer();
            this._formsPlotTrend = new ScottPlot.WinForms.FormsPlot();
            this._formsPlotStep = new ScottPlot.WinForms.FormsPlot();
            this._PanelChartTop = new System.Windows.Forms.Panel();
            this._ComboSeq = new EQ.UI.Controls._ComboBox();
            this._LabelSeqSelect = new System.Windows.Forms.Label();
            this._TabPageUtil = new System.Windows.Forms.TabPage();
            this._formsPlotPie = new ScottPlot.WinForms.FormsPlot();
            this._PanelUtilInfo = new System.Windows.Forms.Panel();
            this._LabelFailCount = new EQ.UI.Controls._Label();
            this._LabelMTTR = new EQ.UI.Controls._Label();
            this._LabelMTBF = new EQ.UI.Controls._Label();
            this._LabelAvailability = new EQ.UI.Controls._Label();
            this._TabPageTiming = new System.Windows.Forms.TabPage(); // [추가]
            this._formsPlotTiming = new ScottPlot.WinForms.FormsPlot(); // [추가]

            this._PanelControl.SuspendLayout();
            this._TabControl.SuspendLayout();
            this._TabPageSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._GridStats)).BeginInit();
            this._TabPageChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).BeginInit();
            this._SplitContainer.Panel1.SuspendLayout();
            this._SplitContainer.Panel2.SuspendLayout();
            this._SplitContainer.SuspendLayout();
            this._PanelChartTop.SuspendLayout();
            this._TabPageUtil.SuspendLayout();
            this._PanelUtilInfo.SuspendLayout();
            this._TabPageTiming.SuspendLayout(); // [추가]
            this.SuspendLayout();
            // 
            // _LabelTitle
            // 
            this._LabelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._LabelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._LabelTitle.Font = new System.Drawing.Font("D2Coding", 12F);
            this._LabelTitle.ForeColor = System.Drawing.Color.White;
            this._LabelTitle.Location = new System.Drawing.Point(0, 0);
            this._LabelTitle.Name = "_LabelTitle";
            this._LabelTitle.Size = new System.Drawing.Size(900, 40);
            this._LabelTitle.TabIndex = 0;
            this._LabelTitle.Text = "Total Analysis View (ScottPlot)";
            this._LabelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._LabelTitle.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // _PanelControl
            // 
            this._PanelControl.BackColor = System.Drawing.Color.White;
            this._PanelControl.Controls.Add(this._LblSummary);
            this._PanelControl.Controls.Add(this._BtnLoad);
            this._PanelControl.Controls.Add(this._DatePicker);
            this._PanelControl.Controls.Add(this._LabelDate);
            this._PanelControl.Dock = System.Windows.Forms.DockStyle.Top;
            this._PanelControl.Location = new System.Drawing.Point(0, 40);
            this._PanelControl.Name = "_PanelControl";
            this._PanelControl.Size = new System.Drawing.Size(900, 60);
            this._PanelControl.TabIndex = 1;
            // 
            // _LblSummary
            // 
            this._LblSummary.AutoSize = true;
            this._LblSummary.BackColor = System.Drawing.SystemColors.Control;
            this._LblSummary.Font = new System.Drawing.Font("D2Coding", 12F, System.Drawing.FontStyle.Bold);
            this._LblSummary.ForeColor = System.Drawing.SystemColors.ControlText;
            this._LblSummary.Location = new System.Drawing.Point(390, 22);
            this._LblSummary.Name = "_LblSummary";
            this._LblSummary.Size = new System.Drawing.Size(53, 18);
            this._LblSummary.TabIndex = 3;
            this._LblSummary.Text = "Ready";
            this._LblSummary.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // _BtnLoad
            // 
            this._BtnLoad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this._BtnLoad.Font = new System.Drawing.Font("D2Coding", 12F);
            this._BtnLoad.ForeColor = System.Drawing.Color.White;
            this._BtnLoad.Location = new System.Drawing.Point(250, 10);
            this._BtnLoad.Name = "_BtnLoad";
            this._BtnLoad.Size = new System.Drawing.Size(120, 40);
            this._BtnLoad.TabIndex = 2;
            this._BtnLoad.Text = "Analyze";
            this._BtnLoad.ThemeStyle = EQ.UI.Controls.ThemeStyle.Primary_Indigo;
            this._BtnLoad.UseVisualStyleBackColor = false;
            this._BtnLoad.Click += new System.EventHandler(this._BtnLoad_Click);
            // 
            // _DatePicker
            // 
            this._DatePicker.Font = new System.Drawing.Font("D2Coding", 12F);
            this._DatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this._DatePicker.Location = new System.Drawing.Point(100, 18);
            this._DatePicker.Name = "_DatePicker";
            this._DatePicker.Size = new System.Drawing.Size(130, 26);
            this._DatePicker.TabIndex = 1;
            // 
            // _LabelDate
            // 
            this._LabelDate.AutoSize = true;
            this._LabelDate.BackColor = System.Drawing.SystemColors.Control;
            this._LabelDate.Font = new System.Drawing.Font("D2Coding", 12F);
            this._LabelDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this._LabelDate.Location = new System.Drawing.Point(15, 22);
            this._LabelDate.Name = "_LabelDate";
            this._LabelDate.Size = new System.Drawing.Size(80, 18);
            this._LabelDate.TabIndex = 4;
            this._LabelDate.Text = "Log Date:";
            this._LabelDate.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // _TabControl
            // 
            this._TabControl.Appearance = System.Windows.Forms.TabAppearance.Buttons;
            this._TabControl.Controls.Add(this._TabPageSummary);
            this._TabControl.Controls.Add(this._TabPageChart);
            this._TabControl.Controls.Add(this._TabPageTiming); // [추가] 순서 변경 가능
            this._TabControl.Controls.Add(this._TabPageUtil);
            this._TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._TabControl.Font = new System.Drawing.Font("D2Coding", 12F);
            this._TabControl.Location = new System.Drawing.Point(0, 100);
            this._TabControl.Name = "_TabControl";
            this._TabControl.SelectedIndex = 0;
            this._TabControl.Size = new System.Drawing.Size(900, 500);
            this._TabControl.TabIndex = 2;
            // 
            // _TabPageSummary
            // 
            this._TabPageSummary.Controls.Add(this._GridStats);
            this._TabPageSummary.Location = new System.Drawing.Point(4, 30);
            this._TabPageSummary.Name = "_TabPageSummary";
            this._TabPageSummary.Padding = new System.Windows.Forms.Padding(3);
            this._TabPageSummary.Size = new System.Drawing.Size(892, 466);
            this._TabPageSummary.TabIndex = 0;
            this._TabPageSummary.Text = "Summary";
            this._TabPageSummary.UseVisualStyleBackColor = true;
            // 
            // _GridStats
            // 
            this._GridStats.AllowUserToAddRows = false;
            this._GridStats.AllowUserToDeleteRows = false;
            this._GridStats.AllowUserToResizeRows = false;
            this._GridStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._GridStats.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(225)))));
            this._GridStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(225)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("D2Coding", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._GridStats.DefaultCellStyle = dataGridViewCellStyle1;
            this._GridStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this._GridStats.Font = new System.Drawing.Font("D2Coding", 12F);
            this._GridStats.Location = new System.Drawing.Point(3, 3);
            this._GridStats.Name = "_GridStats";
            this._GridStats.ReadOnly = true;
            this._GridStats.RowHeadersVisible = false;
            this._GridStats.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._GridStats.Size = new System.Drawing.Size(886, 460);
            this._GridStats.TabIndex = 0;
            this._GridStats.ThemeStyle = EQ.UI.Controls.ThemeStyle.Display_LightYellow;
            // 
            // _TabPageChart
            // 
            this._TabPageChart.Controls.Add(this._SplitContainer);
            this._TabPageChart.Controls.Add(this._PanelChartTop);
            this._TabPageChart.Location = new System.Drawing.Point(4, 30);
            this._TabPageChart.Name = "_TabPageChart";
            this._TabPageChart.Padding = new System.Windows.Forms.Padding(3);
            this._TabPageChart.Size = new System.Drawing.Size(892, 466);
            this._TabPageChart.TabIndex = 1;
            this._TabPageChart.Text = "Tact Time";
            this._TabPageChart.UseVisualStyleBackColor = true;
            // 
            // _SplitContainer
            // 
            this._SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._SplitContainer.Location = new System.Drawing.Point(3, 43);
            this._SplitContainer.Name = "_SplitContainer";
            this._SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _SplitContainer.Panel1
            // 
            this._SplitContainer.Panel1.Controls.Add(this._formsPlotTrend);
            // 
            // _SplitContainer.Panel2
            // 
            this._SplitContainer.Panel2.Controls.Add(this._formsPlotStep);
            this._SplitContainer.Size = new System.Drawing.Size(886, 420);
            this._SplitContainer.SplitterDistance = 208;
            this._SplitContainer.TabIndex = 1;
            // 
            // _formsPlotTrend
            // 
            this._formsPlotTrend.DisplayScale = 1F;
            this._formsPlotTrend.Dock = System.Windows.Forms.DockStyle.Fill;
            this._formsPlotTrend.Location = new System.Drawing.Point(0, 0);
            this._formsPlotTrend.Name = "_formsPlotTrend";
            this._formsPlotTrend.Size = new System.Drawing.Size(886, 208);
            this._formsPlotTrend.TabIndex = 0;
            this._formsPlotTrend.MouseDown += new System.Windows.Forms.MouseEventHandler(this._formsPlotTrend_MouseDown);
            // 
            // _formsPlotStep
            // 
            this._formsPlotStep.DisplayScale = 1F;
            this._formsPlotStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this._formsPlotStep.Location = new System.Drawing.Point(0, 0);
            this._formsPlotStep.Name = "_formsPlotStep";
            this._formsPlotStep.Size = new System.Drawing.Size(886, 208);
            this._formsPlotStep.TabIndex = 0;
            // 
            // _PanelChartTop
            // 
            this._PanelChartTop.Controls.Add(this._ComboSeq);
            this._PanelChartTop.Controls.Add(this._LabelSeqSelect);
            this._PanelChartTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._PanelChartTop.Location = new System.Drawing.Point(3, 3);
            this._PanelChartTop.Name = "_PanelChartTop";
            this._PanelChartTop.Size = new System.Drawing.Size(886, 40);
            this._PanelChartTop.TabIndex = 0;
            // 
            // _ComboSeq
            // 
            this._ComboSeq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this._ComboSeq.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._ComboSeq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._ComboSeq.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ComboSeq.ForeColor = System.Drawing.Color.Black;
            this._ComboSeq.FormattingEnabled = true;
            this._ComboSeq.Location = new System.Drawing.Point(160, 8);
            this._ComboSeq.Name = "_ComboSeq";
            this._ComboSeq.Size = new System.Drawing.Size(250, 27);
            this._ComboSeq.TabIndex = 0;
            this._ComboSeq.ThemeStyle = EQ.UI.Controls.ThemeStyle.Highlight_DeepYellow;
            this._ComboSeq.SelectedIndexChanged += new System.EventHandler(this._ComboSeq_SelectedIndexChanged);
            // 
            // _LabelSeqSelect
            // 
            this._LabelSeqSelect.AutoSize = true;
            this._LabelSeqSelect.Location = new System.Drawing.Point(10, 11);
            this._LabelSeqSelect.Name = "_LabelSeqSelect";
            this._LabelSeqSelect.Size = new System.Drawing.Size(136, 18);
            this._LabelSeqSelect.TabIndex = 1;
            this._LabelSeqSelect.Text = "Target Sequence:";
            // 
            // _TabPageUtil
            // 
            this._TabPageUtil.Controls.Add(this._formsPlotPie);
            this._TabPageUtil.Controls.Add(this._PanelUtilInfo);
            this._TabPageUtil.Location = new System.Drawing.Point(4, 30);
            this._TabPageUtil.Name = "_TabPageUtil";
            this._TabPageUtil.Padding = new System.Windows.Forms.Padding(3);
            this._TabPageUtil.Size = new System.Drawing.Size(892, 466);
            this._TabPageUtil.TabIndex = 2;
            this._TabPageUtil.Text = "MTBF";
            this._TabPageUtil.UseVisualStyleBackColor = true;
            // 
            // _formsPlotPie
            // 
            this._formsPlotPie.DisplayScale = 1F;
            this._formsPlotPie.Dock = System.Windows.Forms.DockStyle.Fill;
            this._formsPlotPie.Location = new System.Drawing.Point(3, 3);
            this._formsPlotPie.Name = "_formsPlotPie";
            this._formsPlotPie.Size = new System.Drawing.Size(886, 289);
            this._formsPlotPie.TabIndex = 0;
            // 
            // _PanelUtilInfo
            // 
            this._PanelUtilInfo.BackColor = System.Drawing.Color.WhiteSmoke;
            this._PanelUtilInfo.Controls.Add(this._LabelFailCount);
            this._PanelUtilInfo.Controls.Add(this._LabelMTTR);
            this._PanelUtilInfo.Controls.Add(this._LabelMTBF);
            this._PanelUtilInfo.Controls.Add(this._LabelAvailability);
            this._PanelUtilInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._PanelUtilInfo.Location = new System.Drawing.Point(3, 292);
            this._PanelUtilInfo.Name = "_PanelUtilInfo";
            this._PanelUtilInfo.Size = new System.Drawing.Size(886, 171);
            this._PanelUtilInfo.TabIndex = 1;
            // 
            // _LabelFailCount
            // 
            this._LabelFailCount.AutoSize = true;
            this._LabelFailCount.BackColor = System.Drawing.SystemColors.Control;
            this._LabelFailCount.Font = new System.Drawing.Font("D2Coding", 12F);
            this._LabelFailCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this._LabelFailCount.Location = new System.Drawing.Point(30, 60);
            this._LabelFailCount.Name = "_LabelFailCount";
            this._LabelFailCount.Size = new System.Drawing.Size(112, 18);
            this._LabelFailCount.TabIndex = 1;
            this._LabelFailCount.Text = "Fail Count: 0";
            this._LabelFailCount.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // _LabelMTTR
            // 
            this._LabelMTTR.AutoSize = true;
            this._LabelMTTR.BackColor = System.Drawing.SystemColors.Control;
            this._LabelMTTR.Font = new System.Drawing.Font("D2Coding", 12F);
            this._LabelMTTR.ForeColor = System.Drawing.SystemColors.ControlText;
            this._LabelMTTR.Location = new System.Drawing.Point(400, 60);
            this._LabelMTTR.Name = "_LabelMTTR";
            this._LabelMTTR.Size = new System.Drawing.Size(64, 18);
            this._LabelMTTR.TabIndex = 3;
            this._LabelMTTR.Text = "MTTR: -";
            this._LabelMTTR.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // _LabelMTBF
            // 
            this._LabelMTBF.AutoSize = true;
            this._LabelMTBF.BackColor = System.Drawing.SystemColors.Control;
            this._LabelMTBF.Font = new System.Drawing.Font("D2Coding", 12F);
            this._LabelMTBF.ForeColor = System.Drawing.SystemColors.ControlText;
            this._LabelMTBF.Location = new System.Drawing.Point(400, 24);
            this._LabelMTBF.Name = "_LabelMTBF";
            this._LabelMTBF.Size = new System.Drawing.Size(64, 18);
            this._LabelMTBF.TabIndex = 2;
            this._LabelMTBF.Text = "MTBF: -";
            this._LabelMTBF.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // _LabelAvailability
            // 
            this._LabelAvailability.AutoSize = true;
            this._LabelAvailability.BackColor = System.Drawing.SystemColors.Control;
            this._LabelAvailability.Font = new System.Drawing.Font("D2Coding", 14F, System.Drawing.FontStyle.Bold);
            this._LabelAvailability.ForeColor = System.Drawing.SystemColors.ControlText;
            this._LabelAvailability.Location = new System.Drawing.Point(30, 20);
            this._LabelAvailability.Name = "_LabelAvailability";
            this._LabelAvailability.Size = new System.Drawing.Size(180, 22);
            this._LabelAvailability.TabIndex = 0;
            this._LabelAvailability.Text = "Availability: - %";
            this._LabelAvailability.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // _TabPageTiming
            // 
            this._TabPageTiming.Controls.Add(this._formsPlotTiming);
            this._TabPageTiming.Location = new System.Drawing.Point(4, 30);
            this._TabPageTiming.Name = "_TabPageTiming";
            this._TabPageTiming.Padding = new System.Windows.Forms.Padding(3);
            this._TabPageTiming.Size = new System.Drawing.Size(892, 466);
            this._TabPageTiming.TabIndex = 3;
            this._TabPageTiming.Text = "Timing Chart";
            this._TabPageTiming.UseVisualStyleBackColor = true;
            // 
            // _formsPlotTiming
            // 
            this._formsPlotTiming.DisplayScale = 1F;
            this._formsPlotTiming.Dock = System.Windows.Forms.DockStyle.Fill;
            this._formsPlotTiming.Location = new System.Drawing.Point(3, 3);
            this._formsPlotTiming.Name = "_formsPlotTiming";
            this._formsPlotTiming.Size = new System.Drawing.Size(886, 460);
            this._formsPlotTiming.TabIndex = 0;
            // 
            // Statistics_ScottPlot_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._TabControl);
            this.Controls.Add(this._PanelControl);
            this.Controls.Add(this._LabelTitle);
            this.Name = "Statistics_ScottPlot_View";
            this.Size = new System.Drawing.Size(900, 600);
            this.Load += new System.EventHandler(this.Statistics_ScottPlot_View_Load);
            this._PanelControl.ResumeLayout(false);
            this._PanelControl.PerformLayout();
            this._TabControl.ResumeLayout(false);
            this._TabPageSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._GridStats)).EndInit();
            this._TabPageChart.ResumeLayout(false);
            this._SplitContainer.Panel1.ResumeLayout(false);
            this._SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).EndInit();
            this._SplitContainer.ResumeLayout(false);
            this._PanelChartTop.ResumeLayout(false);
            this._PanelChartTop.PerformLayout();
            this._TabPageUtil.ResumeLayout(false);
            this._PanelUtilInfo.ResumeLayout(false);
            this._PanelUtilInfo.PerformLayout();
            this._TabPageTiming.ResumeLayout(false);
            this.ResumeLayout(false);
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
        // ScottPlot Controls
        private ScottPlot.WinForms.FormsPlot _formsPlotTrend;
        private ScottPlot.WinForms.FormsPlot _formsPlotStep;

        // Tab 3
        private System.Windows.Forms.TabPage _TabPageUtil;
        // ScottPlot Control
        private ScottPlot.WinForms.FormsPlot _formsPlotPie;
        private System.Windows.Forms.Panel _PanelUtilInfo;
        private EQ.UI.Controls._Label _LabelMTTR;
        private EQ.UI.Controls._Label _LabelMTBF;
        private EQ.UI.Controls._Label _LabelFailCount;
        private EQ.UI.Controls._Label _LabelAvailability;

        // Tab 4 (New)
        private System.Windows.Forms.TabPage _TabPageTiming;
        private ScottPlot.WinForms.FormsPlot _formsPlotTiming;
    }
}