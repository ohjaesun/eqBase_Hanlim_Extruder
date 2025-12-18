namespace EQ.UI.UserViews
{
    partial class Alarm_View
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            _PanelTop = new Panel();
            _ButtonExport = new EQ.UI.Controls._Button();
            _ButtonLoad = new EQ.UI.Controls._Button();
            label2 = new Label();
            _DateTimePickerEnd = new DateTimePicker();
            label1 = new Label();
            _DateTimePickerStart = new DateTimePicker();
            _TableLayoutMain = new TableLayoutPanel();
            _TableLayoutStats = new TableLayoutPanel();
            _GroupBoxStats = new GroupBox();
            _ListViewStats = new EQ.UI.Controls._ListView();
            chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            _GroupBoxHistory = new GroupBox();
            _DataGridViewHistory = new EQ.UI.Controls._DataGridView();
            _GroupBoxPivot = new GroupBox();
            _DataGridViewPivot = new EQ.UI.Controls._DataGridView();
            _PanelMain.SuspendLayout();
            _Panel1.SuspendLayout();
            _PanelTop.SuspendLayout();
            _TableLayoutMain.SuspendLayout();
            _TableLayoutStats.SuspendLayout();
            _GroupBoxStats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)chart1).BeginInit();
            _GroupBoxHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_DataGridViewHistory).BeginInit();
            _GroupBoxPivot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_DataGridViewPivot).BeginInit();
            SuspendLayout();
            // 
            // _LabelTitle
            // 
            _LabelTitle.Size = new Size(700, 59);
            // 
            // _ButtonSave
            // 
            _ButtonSave.Location = new Point(700, 0);
            // 
            // _PanelMain
            // 
            _PanelMain.Controls.Add(_TableLayoutMain);
            _PanelMain.Controls.Add(_PanelTop);
            _PanelMain.Size = new Size(800, 513);
            // 
            // _Panel1
            // 
            _Panel1.Size = new Size(800, 59);
            // 
            // _PanelTop
            // 
            _PanelTop.Controls.Add(_ButtonExport);
            _PanelTop.Controls.Add(_ButtonLoad);
            _PanelTop.Controls.Add(label2);
            _PanelTop.Controls.Add(_DateTimePickerEnd);
            _PanelTop.Controls.Add(label1);
            _PanelTop.Controls.Add(_DateTimePickerStart);
            _PanelTop.Dock = DockStyle.Top;
            _PanelTop.Location = new Point(0, 0);
            _PanelTop.Name = "_PanelTop";
            _PanelTop.Size = new Size(800, 45);
            _PanelTop.TabIndex = 0;
            // 
            // _ButtonExport
            // 
            _ButtonExport.BackColor = Color.FromArgb(52, 152, 219);
            _ButtonExport.Font = new Font("D2Coding", 12F);
            _ButtonExport.ForeColor = Color.Black;
            _ButtonExport.Location = new Point(677, 6);
            _ButtonExport.Name = "_ButtonExport";
            _ButtonExport.Size = new Size(117, 33);
            _ButtonExport.TabIndex = 3;
            _ButtonExport.Text = "Export CSV";
            _ButtonExport.ThemeStyle = UI.Controls.ThemeStyle.Info_Sky;
            _ButtonExport.TooltipText = null;
            _ButtonExport.UseVisualStyleBackColor = false;
            _ButtonExport.Click += _ButtonExport_Click;
            // 
            // _ButtonLoad
            // 
            _ButtonLoad.BackColor = Color.FromArgb(48, 63, 159);
            _ButtonLoad.Font = new Font("D2Coding", 12F);
            _ButtonLoad.ForeColor = Color.White;
            _ButtonLoad.Location = new Point(554, 6);
            _ButtonLoad.Name = "_ButtonLoad";
            _ButtonLoad.Size = new Size(117, 33);
            _ButtonLoad.TabIndex = 2;
            _ButtonLoad.Text = "Load";
            _ButtonLoad.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _ButtonLoad.TooltipText = null;
            _ButtonLoad.UseVisualStyleBackColor = false;
            _ButtonLoad.Click += _ButtonLoad_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(288, 13);
            label2.Name = "label2";
            label2.Size = new Size(40, 18);
            label2.TabIndex = 0;
            label2.Text = "End:";
            // 
            // _DateTimePickerEnd
            // 
            _DateTimePickerEnd.Location = new Point(334, 7);
            _DateTimePickerEnd.Name = "_DateTimePickerEnd";
            _DateTimePickerEnd.Size = new Size(200, 26);
            _DateTimePickerEnd.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 13);
            label1.Name = "label1";
            label1.Size = new Size(56, 18);
            label1.TabIndex = 0;
            label1.Text = "Start:";
            // 
            // _DateTimePickerStart
            // 
            _DateTimePickerStart.Location = new Point(68, 7);
            _DateTimePickerStart.Name = "_DateTimePickerStart";
            _DateTimePickerStart.Size = new Size(200, 26);
            _DateTimePickerStart.TabIndex = 0;
            // 
            // _TableLayoutMain
            // 
            _TableLayoutMain.ColumnCount = 1;
            _TableLayoutMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            _TableLayoutMain.Controls.Add(_TableLayoutStats, 0, 0);
            _TableLayoutMain.Controls.Add(_GroupBoxHistory, 0, 1);
            _TableLayoutMain.Controls.Add(_GroupBoxPivot, 0, 2);
            _TableLayoutMain.Dock = DockStyle.Fill;
            _TableLayoutMain.Location = new Point(0, 45);
            _TableLayoutMain.Name = "_TableLayoutMain";
            _TableLayoutMain.RowCount = 3;
            _TableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            _TableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            _TableLayoutMain.RowStyles.Add(new RowStyle(SizeType.Percent, 30F));
            _TableLayoutMain.Size = new Size(800, 468);
            _TableLayoutMain.TabIndex = 1;
            // 
            // _TableLayoutStats
            // 
            _TableLayoutStats.ColumnCount = 2;
            _TableLayoutStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 51.0723877F));
            _TableLayoutStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 48.9276123F));
            _TableLayoutStats.Controls.Add(_GroupBoxStats, 0, 0);
            _TableLayoutStats.Controls.Add(chart1, 1, 0);
            _TableLayoutStats.Dock = DockStyle.Fill;
            _TableLayoutStats.Location = new Point(3, 3);
            _TableLayoutStats.Name = "_TableLayoutStats";
            _TableLayoutStats.RowCount = 1;
            _TableLayoutStats.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            _TableLayoutStats.Size = new Size(794, 157);
            _TableLayoutStats.TabIndex = 0;
            // 
            // _GroupBoxStats
            // 
            _GroupBoxStats.Controls.Add(_ListViewStats);
            _GroupBoxStats.Dock = DockStyle.Fill;
            _GroupBoxStats.Location = new Point(3, 3);
            _GroupBoxStats.Name = "_GroupBoxStats";
            _GroupBoxStats.Size = new Size(399, 151);
            _GroupBoxStats.TabIndex = 0;
            _GroupBoxStats.TabStop = false;
            _GroupBoxStats.Text = "Statistics (by ID)";
            // 
            // _ListViewStats
            // 
            _ListViewStats.BackColor = Color.FromArgb(149, 165, 166);
            _ListViewStats.Dock = DockStyle.Fill;
            _ListViewStats.Font = new Font("D2Coding", 12F);
            _ListViewStats.ForeColor = Color.White;
            _ListViewStats.Location = new Point(3, 22);
            _ListViewStats.Name = "_ListViewStats";
            _ListViewStats.OwnerDraw = true;
            _ListViewStats.Size = new Size(393, 126);
            _ListViewStats.TabIndex = 0;
            _ListViewStats.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _ListViewStats.UseCompatibleStateImageBehavior = false;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            chart1.ChartAreas.Add(chartArea1);
            chart1.Dock = DockStyle.Fill;
            legend1.Name = "Legend1";
            chart1.Legends.Add(legend1);
            chart1.Location = new Point(408, 3);
            chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            chart1.Series.Add(series1);
            chart1.Size = new Size(383, 151);
            chart1.TabIndex = 1;
            chart1.Text = "chart1";
            // 
            // _GroupBoxHistory
            // 
            _GroupBoxHistory.Controls.Add(_DataGridViewHistory);
            _GroupBoxHistory.Dock = DockStyle.Fill;
            _GroupBoxHistory.Location = new Point(3, 166);
            _GroupBoxHistory.Name = "_GroupBoxHistory";
            _GroupBoxHistory.Size = new Size(794, 157);
            _GroupBoxHistory.TabIndex = 1;
            _GroupBoxHistory.TabStop = false;
            _GroupBoxHistory.Text = "History";
            // 
            // _DataGridViewHistory
            // 
            _DataGridViewHistory.AllowUserToAddRows = false;
            _DataGridViewHistory.AllowUserToDeleteRows = false;
            _DataGridViewHistory.AllowUserToResizeRows = false;
            _DataGridViewHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _DataGridViewHistory.BackgroundColor = Color.FromArgb(149, 165, 166);
            _DataGridViewHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(149, 165, 166);
            dataGridViewCellStyle1.Font = new Font("D2Coding", 12F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            _DataGridViewHistory.DefaultCellStyle = dataGridViewCellStyle1;
            _DataGridViewHistory.Dock = DockStyle.Fill;
            _DataGridViewHistory.Font = new Font("D2Coding", 12F);
            _DataGridViewHistory.Location = new Point(3, 22);
            _DataGridViewHistory.Name = "_DataGridViewHistory";
            _DataGridViewHistory.ReadOnly = true;
            _DataGridViewHistory.RowHeadersVisible = false;
            _DataGridViewHistory.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _DataGridViewHistory.Size = new Size(788, 132);
            _DataGridViewHistory.TabIndex = 0;
            _DataGridViewHistory.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _DataGridViewHistory.CellDoubleClick += _DataGridViewHistory_CellDoubleClick;
            // 
            // _GroupBoxPivot
            // 
            _GroupBoxPivot.Controls.Add(_DataGridViewPivot);
            _GroupBoxPivot.Dock = DockStyle.Fill;
            _GroupBoxPivot.Location = new Point(3, 329);
            _GroupBoxPivot.Name = "_GroupBoxPivot";
            _GroupBoxPivot.Size = new Size(794, 136);
            _GroupBoxPivot.TabIndex = 2;
            _GroupBoxPivot.TabStop = false;
            _GroupBoxPivot.Text = "Pivot by Date/ID";
            // 
            // _DataGridViewPivot
            // 
            _DataGridViewPivot.AllowUserToAddRows = false;
            _DataGridViewPivot.AllowUserToDeleteRows = false;
            _DataGridViewPivot.AllowUserToResizeRows = false;
            _DataGridViewPivot.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _DataGridViewPivot.BackgroundColor = Color.FromArgb(149, 165, 166);
            _DataGridViewPivot.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(149, 165, 166);
            dataGridViewCellStyle2.Font = new Font("D2Coding", 12F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            _DataGridViewPivot.DefaultCellStyle = dataGridViewCellStyle2;
            _DataGridViewPivot.Dock = DockStyle.Fill;
            _DataGridViewPivot.Font = new Font("D2Coding", 12F);
            _DataGridViewPivot.Location = new Point(3, 22);
            _DataGridViewPivot.Name = "_DataGridViewPivot";
            _DataGridViewPivot.ReadOnly = true;
            _DataGridViewPivot.RowHeadersVisible = false;
            _DataGridViewPivot.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _DataGridViewPivot.Size = new Size(788, 111);
            _DataGridViewPivot.TabIndex = 0;
            _DataGridViewPivot.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // Alarm_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "Alarm_View";
            Size = new Size(800, 572);
            Load += Alarm_View_Load;
            _PanelMain.ResumeLayout(false);
            _Panel1.ResumeLayout(false);
            _PanelTop.ResumeLayout(false);
            _PanelTop.PerformLayout();
            _TableLayoutMain.ResumeLayout(false);
            _TableLayoutStats.ResumeLayout(false);
            _GroupBoxStats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)chart1).EndInit();
            _GroupBoxHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_DataGridViewHistory).EndInit();
            _GroupBoxPivot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_DataGridViewPivot).EndInit();
            ResumeLayout(false);
        }
        #endregion

        private Controls._Button _ButtonLoad;
        private System.Windows.Forms.DateTimePicker _DateTimePickerStart;
        private System.Windows.Forms.DateTimePicker _DateTimePickerEnd;
        private Controls._Button _ButtonExport;
        private Controls._DataGridView _DataGridViewHistory;
        private Controls._DataGridView _DataGridViewPivot;
        private Controls._ListView _ListViewStats;
        private TableLayoutPanel _TableLayoutMain;
        private TableLayoutPanel _TableLayoutStats;
        private GroupBox _GroupBoxStats;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private GroupBox _GroupBoxHistory;
        private GroupBox _GroupBoxPivot;
        private Panel _PanelTop;
        private Label label2;
        private Label label1;
    }
}