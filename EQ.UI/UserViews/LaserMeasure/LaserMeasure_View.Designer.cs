using EQ.UI.Controls;

namespace EQ.UI.UserViews.LaserMeasure
{
    partial class LaserMeasure_View
    {
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code
        private void InitializeComponent()
        {
            _DataGridViewLasers = new Controls._DataGridView();
            _ButtonRefresh = new Controls._Button();
            _ButtonStartContinuous = new Controls._Button();
            _ButtonStopContinuous = new Controls._Button();
            _LabelStatus = new Controls._Label();
            _PanelStatusLed = new Panel();
            _PanelToolbar = new Panel();

            _PanelMain.SuspendLayout();
            _PanelToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_DataGridViewLasers).BeginInit();
            SuspendLayout();

            // 
            // _LabelTitle
            // 
            _LabelTitle.Text = "Laser Measure";

            // 
            // _PanelMain
            // 
            _PanelMain.Controls.Add(_DataGridViewLasers);
            _PanelMain.Controls.Add(_PanelToolbar);

            // 
            // _PanelToolbar (상단 도구 패널)
            // 
            _PanelToolbar.Controls.Add(_PanelStatusLed);
            _PanelToolbar.Controls.Add(_LabelStatus);
            _PanelToolbar.Controls.Add(_ButtonRefresh);
            _PanelToolbar.Controls.Add(_ButtonStartContinuous);
            _PanelToolbar.Controls.Add(_ButtonStopContinuous);
            _PanelToolbar.Dock = DockStyle.Top;
            _PanelToolbar.Location = new Point(0, 0);
            _PanelToolbar.Name = "_PanelToolbar";
            _PanelToolbar.Size = new Size(580, 40);
            _PanelToolbar.TabIndex = 0;

            // 
            // _PanelStatusLed
            // 
            _PanelStatusLed.BackColor = Color.Gray;
            _PanelStatusLed.Location = new Point(10, 10);
            _PanelStatusLed.Name = "_PanelStatusLed";
            _PanelStatusLed.Size = new Size(20, 20);
            _PanelStatusLed.TabIndex = 0;

            // 
            // _LabelStatus
            // 
            _LabelStatus.AutoSize = true;
            _LabelStatus.Font = new Font("D2Coding", 10F);
            _LabelStatus.Location = new Point(40, 12);
            _LabelStatus.Name = "_LabelStatus";
            _LabelStatus.Size = new Size(100, 17);
            _LabelStatus.TabIndex = 1;
            _LabelStatus.Text = "Disconnected";

            // 
            // _ButtonRefresh
            // 
            _ButtonRefresh.BackColor = Color.FromArgb(52, 152, 219);
            _ButtonRefresh.Font = new Font("D2Coding", 10F);
            _ButtonRefresh.ForeColor = Color.White;
            _ButtonRefresh.Location = new Point(160, 6);
            _ButtonRefresh.Name = "_ButtonRefresh";
            _ButtonRefresh.Size = new Size(80, 28);
            _ButtonRefresh.TabIndex = 2;
            _ButtonRefresh.Text = "Refresh";
            _ButtonRefresh.ThemeStyle = ThemeStyle.Info_Sky;
            _ButtonRefresh.Click += _ButtonRefresh_Click;

            // 
            // _ButtonStartContinuous
            // 
            _ButtonStartContinuous.BackColor = Color.FromArgb(46, 204, 113);
            _ButtonStartContinuous.Font = new Font("D2Coding", 10F);
            _ButtonStartContinuous.ForeColor = Color.White;
            _ButtonStartContinuous.Location = new Point(250, 6);
            _ButtonStartContinuous.Name = "_ButtonStartContinuous";
            _ButtonStartContinuous.Size = new Size(80, 28);
            _ButtonStartContinuous.TabIndex = 3;
            _ButtonStartContinuous.Text = "Start";
            _ButtonStartContinuous.ThemeStyle = ThemeStyle.Success_Green;
            _ButtonStartContinuous.Click += _ButtonStartContinuous_Click;

            // 
            // _ButtonStopContinuous
            // 
            _ButtonStopContinuous.BackColor = Color.FromArgb(231, 76, 60);
            _ButtonStopContinuous.Font = new Font("D2Coding", 10F);
            _ButtonStopContinuous.ForeColor = Color.White;
            _ButtonStopContinuous.Location = new Point(340, 6);
            _ButtonStopContinuous.Name = "_ButtonStopContinuous";
            _ButtonStopContinuous.Size = new Size(80, 28);
            _ButtonStopContinuous.TabIndex = 4;
            _ButtonStopContinuous.Text = "Stop";
            _ButtonStopContinuous.ThemeStyle = ThemeStyle.Danger_Red;
            _ButtonStopContinuous.Click += _ButtonStopContinuous_Click;

            // 
            // _DataGridViewLasers
            // 
            _DataGridViewLasers.Dock = DockStyle.Fill;
            _DataGridViewLasers.AllowUserToAddRows = false;
            _DataGridViewLasers.AllowUserToDeleteRows = false;
            _DataGridViewLasers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _DataGridViewLasers.BackgroundColor = Color.White;
            _DataGridViewLasers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _DataGridViewLasers.Font = new Font("D2Coding", 10F);
            _DataGridViewLasers.Location = new Point(0, 40);
            _DataGridViewLasers.Name = "_DataGridViewLasers";
            _DataGridViewLasers.ReadOnly = true;
            _DataGridViewLasers.RowHeadersWidth = 30;
            _DataGridViewLasers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _DataGridViewLasers.Size = new Size(580, 310);
            _DataGridViewLasers.TabIndex = 1;

            // 
            // LaserMeasure_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "LaserMeasure_View";
            Size = new Size(600, 400);
            Load += LaserMeasure_View_Load;

            _PanelToolbar.ResumeLayout(false);
            _PanelToolbar.PerformLayout();
            _PanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_DataGridViewLasers).EndInit();
            ResumeLayout(false);
        }
        #endregion

        private Panel _PanelToolbar;
        private Controls._DataGridView _DataGridViewLasers;
        private Controls._Button _ButtonRefresh;
        private Controls._Button _ButtonStartContinuous;
        private Controls._Button _ButtonStopContinuous;
        private Controls._Label _LabelStatus;
        private Panel _PanelStatusLed;
    }
}

