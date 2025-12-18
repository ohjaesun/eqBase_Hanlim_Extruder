namespace EQ.UI.UserViews.SecsGem
{
    partial class SecsGem_View
    {
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code
        private void InitializeComponent()
        {
            _TabControl = new TabControl();
            _TabPageStatus = new TabPage();
            _LabelCommunicatingValue = new Label();
            _LabelCommunicating = new Label();
            _LabelControlStateValue = new Label();
            _LabelControlState = new Label();
            _LabelConnectionValue = new Label();
            _LabelConnectionStatus = new Label();
            _PanelStatusLed = new Panel();
            _TabPageControl = new TabPage();
            _ButtonStop = new EQ.UI.Controls._Button();
            _ButtonStart = new EQ.UI.Controls._Button();
            _ButtonGoOffline = new EQ.UI.Controls._Button();
            _ButtonGoOnlineLocal = new EQ.UI.Controls._Button();
            _ButtonGoOnlineRemote = new EQ.UI.Controls._Button();
            _TabPageEvent = new TabPage();
            _GroupBoxAlarm = new GroupBox();
            _ButtonAlarmClear = new EQ.UI.Controls._Button();
            _ButtonAlarmSet = new EQ.UI.Controls._Button();
            _ComboBoxALID = new ComboBox();
            _GroupBoxCEID = new GroupBox();
            _ButtonSendCEID = new EQ.UI.Controls._Button();
            _ComboBoxCEID = new ComboBox();
            _TabPageData = new TabPage();
            _TabControlData = new TabControl();
            _TabPageSVID = new TabPage();
            _DataGridViewSVID = new DataGridView();
            _TabPageECID = new TabPage();
            _DataGridViewECID = new DataGridView();
            _ButtonRefreshData = new EQ.UI.Controls._Button();
            _ButtonSaveData = new EQ.UI.Controls._Button();
            _PanelMain.SuspendLayout();
            _Panel1.SuspendLayout();
            _TabControl.SuspendLayout();
            _TabPageStatus.SuspendLayout();
            _TabPageControl.SuspendLayout();
            _TabPageEvent.SuspendLayout();
            _GroupBoxAlarm.SuspendLayout();
            _GroupBoxCEID.SuspendLayout();
            _TabPageData.SuspendLayout();
            _TabControlData.SuspendLayout();
            _TabPageSVID.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_DataGridViewSVID).BeginInit();
            _TabPageECID.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_DataGridViewECID).BeginInit();
            SuspendLayout();
            // 
            // _LabelTitle
            // 
            _LabelTitle.Size = new Size(600, 59);
            _LabelTitle.Text = "SECS/GEM";
            // 
            // _PanelMain
            // 
            _PanelMain.Controls.Add(_TabControl);
            _PanelMain.Size = new Size(600, 391);
            // 
            // _Panel1
            // 
            _Panel1.Size = new Size(600, 59);
            // 
            // _TabControl
            // 
            _TabControl.Controls.Add(_TabPageStatus);
            _TabControl.Controls.Add(_TabPageControl);
            _TabControl.Controls.Add(_TabPageEvent);
            _TabControl.Controls.Add(_TabPageData);
            _TabControl.Dock = DockStyle.Fill;
            _TabControl.Font = new Font("D2Coding", 10F);
            _TabControl.Location = new Point(0, 0);
            _TabControl.Name = "_TabControl";
            _TabControl.SelectedIndex = 0;
            _TabControl.Size = new Size(600, 391);
            _TabControl.TabIndex = 0;
            // 
            // _TabPageStatus
            // 
            _TabPageStatus.Controls.Add(_LabelCommunicatingValue);
            _TabPageStatus.Controls.Add(_LabelCommunicating);
            _TabPageStatus.Controls.Add(_LabelControlStateValue);
            _TabPageStatus.Controls.Add(_LabelControlState);
            _TabPageStatus.Controls.Add(_LabelConnectionValue);
            _TabPageStatus.Controls.Add(_LabelConnectionStatus);
            _TabPageStatus.Controls.Add(_PanelStatusLed);
            _TabPageStatus.Location = new Point(4, 24);
            _TabPageStatus.Name = "_TabPageStatus";
            _TabPageStatus.Padding = new Padding(10);
            _TabPageStatus.Size = new Size(592, 363);
            _TabPageStatus.TabIndex = 0;
            _TabPageStatus.Text = "Status";
            _TabPageStatus.UseVisualStyleBackColor = true;
            // 
            // _LabelCommunicatingValue
            // 
            _LabelCommunicatingValue.AutoSize = true;
            _LabelCommunicatingValue.Font = new Font("D2Coding", 12F, FontStyle.Bold);
            _LabelCommunicatingValue.ForeColor = Color.Red;
            _LabelCommunicatingValue.Location = new Point(190, 95);
            _LabelCommunicatingValue.Name = "_LabelCommunicatingValue";
            _LabelCommunicatingValue.Size = new Size(26, 18);
            _LabelCommunicatingValue.TabIndex = 6;
            _LabelCommunicatingValue.Text = "No";
            // 
            // _LabelCommunicating
            // 
            _LabelCommunicating.AutoSize = true;
            _LabelCommunicating.Font = new Font("D2Coding", 12F);
            _LabelCommunicating.Location = new Point(60, 95);
            _LabelCommunicating.Name = "_LabelCommunicating";
            _LabelCommunicating.Size = new Size(120, 18);
            _LabelCommunicating.TabIndex = 5;
            _LabelCommunicating.Text = "Communicating:";
            // 
            // _LabelControlStateValue
            // 
            _LabelControlStateValue.AutoSize = true;
            _LabelControlStateValue.Font = new Font("D2Coding", 12F, FontStyle.Bold);
            _LabelControlStateValue.Location = new Point(170, 60);
            _LabelControlStateValue.Name = "_LabelControlStateValue";
            _LabelControlStateValue.Size = new Size(71, 18);
            _LabelControlStateValue.TabIndex = 4;
            _LabelControlStateValue.Text = "Offline";
            // 
            // _LabelControlState
            // 
            _LabelControlState.AutoSize = true;
            _LabelControlState.Font = new Font("D2Coding", 12F);
            _LabelControlState.Location = new Point(60, 60);
            _LabelControlState.Name = "_LabelControlState";
            _LabelControlState.Size = new Size(120, 18);
            _LabelControlState.TabIndex = 3;
            _LabelControlState.Text = "Control State:";
            // 
            // _LabelConnectionValue
            // 
            _LabelConnectionValue.AutoSize = true;
            _LabelConnectionValue.Font = new Font("D2Coding", 12F, FontStyle.Bold);
            _LabelConnectionValue.ForeColor = Color.Red;
            _LabelConnectionValue.Location = new Point(170, 25);
            _LabelConnectionValue.Name = "_LabelConnectionValue";
            _LabelConnectionValue.Size = new Size(116, 18);
            _LabelConnectionValue.TabIndex = 2;
            _LabelConnectionValue.Text = "Disconnected";
            // 
            // _LabelConnectionStatus
            // 
            _LabelConnectionStatus.AutoSize = true;
            _LabelConnectionStatus.Font = new Font("D2Coding", 12F);
            _LabelConnectionStatus.Location = new Point(60, 25);
            _LabelConnectionStatus.Name = "_LabelConnectionStatus";
            _LabelConnectionStatus.Size = new Size(96, 18);
            _LabelConnectionStatus.TabIndex = 1;
            _LabelConnectionStatus.Text = "Connection:";
            // 
            // _PanelStatusLed
            // 
            _PanelStatusLed.BackColor = Color.Red;
            _PanelStatusLed.Location = new Point(20, 20);
            _PanelStatusLed.Name = "_PanelStatusLed";
            _PanelStatusLed.Size = new Size(30, 30);
            _PanelStatusLed.TabIndex = 0;
            // 
            // _TabPageControl
            // 
            _TabPageControl.Controls.Add(_ButtonStop);
            _TabPageControl.Controls.Add(_ButtonStart);
            _TabPageControl.Controls.Add(_ButtonGoOffline);
            _TabPageControl.Controls.Add(_ButtonGoOnlineLocal);
            _TabPageControl.Controls.Add(_ButtonGoOnlineRemote);
            _TabPageControl.Location = new Point(4, 24);
            _TabPageControl.Name = "_TabPageControl";
            _TabPageControl.Padding = new Padding(10);
            _TabPageControl.Size = new Size(592, 363);
            _TabPageControl.TabIndex = 1;
            _TabPageControl.Text = "Control";
            _TabPageControl.UseVisualStyleBackColor = true;
            // 
            // _ButtonStop
            // 
            _ButtonStop.BackColor = Color.FromArgb(231, 76, 60);
            _ButtonStop.Font = new Font("D2Coding", 12F);
            _ButtonStop.ForeColor = Color.Black;
            _ButtonStop.Location = new Point(180, 80);
            _ButtonStop.Name = "_ButtonStop";
            _ButtonStop.Size = new Size(150, 40);
            _ButtonStop.TabIndex = 4;
            _ButtonStop.Text = "Stop";
            _ButtonStop.ThemeStyle = UI.Controls.ThemeStyle.Danger_Red;
            _ButtonStop.TooltipText = null;
            _ButtonStop.UseVisualStyleBackColor = false;
            _ButtonStop.Click += _ButtonStop_Click;
            // 
            // _ButtonStart
            // 
            _ButtonStart.BackColor = Color.FromArgb(46, 204, 113);
            _ButtonStart.Font = new Font("D2Coding", 12F);
            _ButtonStart.ForeColor = Color.Black;
            _ButtonStart.Location = new Point(20, 80);
            _ButtonStart.Name = "_ButtonStart";
            _ButtonStart.Size = new Size(150, 40);
            _ButtonStart.TabIndex = 3;
            _ButtonStart.Text = "Start";
            _ButtonStart.ThemeStyle = UI.Controls.ThemeStyle.Success_Green;
            _ButtonStart.TooltipText = null;
            _ButtonStart.UseVisualStyleBackColor = false;
            _ButtonStart.Click += _ButtonStart_Click;
            // 
            // _ButtonGoOffline
            // 
            _ButtonGoOffline.BackColor = SystemColors.Control;
            _ButtonGoOffline.Font = new Font("D2Coding", 12F);
            _ButtonGoOffline.ForeColor = SystemColors.ControlText;
            _ButtonGoOffline.Location = new Point(340, 20);
            _ButtonGoOffline.Name = "_ButtonGoOffline";
            _ButtonGoOffline.Size = new Size(150, 40);
            _ButtonGoOffline.TabIndex = 2;
            _ButtonGoOffline.Text = "Go Offline";
            _ButtonGoOffline.ThemeStyle = UI.Controls.ThemeStyle.Default;
            _ButtonGoOffline.TooltipText = null;
            _ButtonGoOffline.UseVisualStyleBackColor = false;
            _ButtonGoOffline.Click += _ButtonGoOffline_Click;
            // 
            // _ButtonGoOnlineLocal
            // 
            _ButtonGoOnlineLocal.BackColor = Color.FromArgb(52, 152, 219);
            _ButtonGoOnlineLocal.Font = new Font("D2Coding", 12F);
            _ButtonGoOnlineLocal.ForeColor = Color.Black;
            _ButtonGoOnlineLocal.Location = new Point(180, 20);
            _ButtonGoOnlineLocal.Name = "_ButtonGoOnlineLocal";
            _ButtonGoOnlineLocal.Size = new Size(150, 40);
            _ButtonGoOnlineLocal.TabIndex = 1;
            _ButtonGoOnlineLocal.Text = "Go Online Local";
            _ButtonGoOnlineLocal.ThemeStyle = UI.Controls.ThemeStyle.Info_Sky;
            _ButtonGoOnlineLocal.TooltipText = null;
            _ButtonGoOnlineLocal.UseVisualStyleBackColor = false;
            _ButtonGoOnlineLocal.Click += _ButtonGoOnlineLocal_Click;
            // 
            // _ButtonGoOnlineRemote
            // 
            _ButtonGoOnlineRemote.BackColor = Color.FromArgb(46, 204, 113);
            _ButtonGoOnlineRemote.Font = new Font("D2Coding", 12F);
            _ButtonGoOnlineRemote.ForeColor = Color.Black;
            _ButtonGoOnlineRemote.Location = new Point(20, 20);
            _ButtonGoOnlineRemote.Name = "_ButtonGoOnlineRemote";
            _ButtonGoOnlineRemote.Size = new Size(150, 40);
            _ButtonGoOnlineRemote.TabIndex = 0;
            _ButtonGoOnlineRemote.Text = "Go Online Remote";
            _ButtonGoOnlineRemote.ThemeStyle = UI.Controls.ThemeStyle.Success_Green;
            _ButtonGoOnlineRemote.TooltipText = null;
            _ButtonGoOnlineRemote.UseVisualStyleBackColor = false;
            _ButtonGoOnlineRemote.Click += _ButtonGoOnlineRemote_Click;
            // 
            // _TabPageEvent
            // 
            _TabPageEvent.Controls.Add(_GroupBoxAlarm);
            _TabPageEvent.Controls.Add(_GroupBoxCEID);
            _TabPageEvent.Location = new Point(4, 24);
            _TabPageEvent.Name = "_TabPageEvent";
            _TabPageEvent.Padding = new Padding(10);
            _TabPageEvent.Size = new Size(592, 372);
            _TabPageEvent.TabIndex = 2;
            _TabPageEvent.Text = "Event/Alarm";
            _TabPageEvent.UseVisualStyleBackColor = true;
            // 
            // _GroupBoxAlarm
            // 
            _GroupBoxAlarm.Controls.Add(_ButtonAlarmClear);
            _GroupBoxAlarm.Controls.Add(_ButtonAlarmSet);
            _GroupBoxAlarm.Controls.Add(_ComboBoxALID);
            _GroupBoxAlarm.Font = new Font("D2Coding", 10F);
            _GroupBoxAlarm.Location = new Point(20, 120);
            _GroupBoxAlarm.Name = "_GroupBoxAlarm";
            _GroupBoxAlarm.Size = new Size(300, 80);
            _GroupBoxAlarm.TabIndex = 1;
            _GroupBoxAlarm.TabStop = false;
            _GroupBoxAlarm.Text = "Alarm (ALID)";
            // 
            // _ButtonAlarmClear
            // 
            _ButtonAlarmClear.BackColor = Color.FromArgb(46, 204, 113);
            _ButtonAlarmClear.Font = new Font("D2Coding", 10F);
            _ButtonAlarmClear.ForeColor = Color.Black;
            _ButtonAlarmClear.Location = new Point(220, 28);
            _ButtonAlarmClear.Name = "_ButtonAlarmClear";
            _ButtonAlarmClear.Size = new Size(70, 28);
            _ButtonAlarmClear.TabIndex = 2;
            _ButtonAlarmClear.Text = "Clear";
            _ButtonAlarmClear.ThemeStyle = UI.Controls.ThemeStyle.Success_Green;
            _ButtonAlarmClear.TooltipText = null;
            _ButtonAlarmClear.UseVisualStyleBackColor = false;
            _ButtonAlarmClear.Click += _ButtonAlarmClear_Click;
            // 
            // _ButtonAlarmSet
            // 
            _ButtonAlarmSet.BackColor = Color.FromArgb(231, 76, 60);
            _ButtonAlarmSet.Font = new Font("D2Coding", 10F);
            _ButtonAlarmSet.ForeColor = Color.Black;
            _ButtonAlarmSet.Location = new Point(140, 28);
            _ButtonAlarmSet.Name = "_ButtonAlarmSet";
            _ButtonAlarmSet.Size = new Size(70, 28);
            _ButtonAlarmSet.TabIndex = 1;
            _ButtonAlarmSet.Text = "Set";
            _ButtonAlarmSet.ThemeStyle = UI.Controls.ThemeStyle.Danger_Red;
            _ButtonAlarmSet.TooltipText = null;
            _ButtonAlarmSet.UseVisualStyleBackColor = false;
            _ButtonAlarmSet.Click += _ButtonAlarmSet_Click;
            // 
            // _ComboBoxALID
            // 
            _ComboBoxALID.DropDownStyle = ComboBoxStyle.DropDownList;
            _ComboBoxALID.Font = new Font("D2Coding", 10F);
            _ComboBoxALID.FormattingEnabled = true;
            _ComboBoxALID.Location = new Point(10, 30);
            _ComboBoxALID.Name = "_ComboBoxALID";
            _ComboBoxALID.Size = new Size(120, 23);
            _ComboBoxALID.TabIndex = 0;
            // 
            // _GroupBoxCEID
            // 
            _GroupBoxCEID.Controls.Add(_ButtonSendCEID);
            _GroupBoxCEID.Controls.Add(_ComboBoxCEID);
            _GroupBoxCEID.Font = new Font("D2Coding", 10F);
            _GroupBoxCEID.Location = new Point(20, 20);
            _GroupBoxCEID.Name = "_GroupBoxCEID";
            _GroupBoxCEID.Size = new Size(300, 80);
            _GroupBoxCEID.TabIndex = 0;
            _GroupBoxCEID.TabStop = false;
            _GroupBoxCEID.Text = "Collection Event (CEID)";
            // 
            // _ButtonSendCEID
            // 
            _ButtonSendCEID.BackColor = Color.FromArgb(52, 152, 219);
            _ButtonSendCEID.Font = new Font("D2Coding", 10F);
            _ButtonSendCEID.ForeColor = Color.Black;
            _ButtonSendCEID.Location = new Point(200, 28);
            _ButtonSendCEID.Name = "_ButtonSendCEID";
            _ButtonSendCEID.Size = new Size(80, 28);
            _ButtonSendCEID.TabIndex = 1;
            _ButtonSendCEID.Text = "Send";
            _ButtonSendCEID.ThemeStyle = UI.Controls.ThemeStyle.Info_Sky;
            _ButtonSendCEID.TooltipText = null;
            _ButtonSendCEID.UseVisualStyleBackColor = false;
            _ButtonSendCEID.Click += _ButtonSendCEID_Click;
            // 
            // _ComboBoxCEID
            // 
            _ComboBoxCEID.DropDownStyle = ComboBoxStyle.DropDownList;
            _ComboBoxCEID.Font = new Font("D2Coding", 10F);
            _ComboBoxCEID.FormattingEnabled = true;
            _ComboBoxCEID.Location = new Point(10, 30);
            _ComboBoxCEID.Name = "_ComboBoxCEID";
            _ComboBoxCEID.Size = new Size(180, 23);
            _ComboBoxCEID.TabIndex = 0;
            // 
            // _TabPageData
            // 
            _TabPageData.Controls.Add(_TabControlData);
            _TabPageData.Controls.Add(_ButtonRefreshData);
            _TabPageData.Controls.Add(_ButtonSaveData);
            _TabPageData.Location = new Point(4, 24);
            _TabPageData.Name = "_TabPageData";
            _TabPageData.Padding = new Padding(10);
            _TabPageData.Size = new Size(592, 363);
            _TabPageData.TabIndex = 3;
            _TabPageData.Text = "Data";
            _TabPageData.UseVisualStyleBackColor = true;
            // 
            // _TabControlData
            // 
            _TabControlData.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _TabControlData.Controls.Add(_TabPageSVID);
            _TabControlData.Controls.Add(_TabPageECID);
            _TabControlData.Font = new Font("D2Coding", 9F);
            _TabControlData.Location = new Point(10, 10);
            _TabControlData.Name = "_TabControlData";
            _TabControlData.SelectedIndex = 0;
            _TabControlData.Size = new Size(572, 299);
            _TabControlData.TabIndex = 0;
            // 
            // _TabPageSVID
            // 
            _TabPageSVID.Controls.Add(_DataGridViewSVID);
            _TabPageSVID.Location = new Point(4, 23);
            _TabPageSVID.Name = "_TabPageSVID";
            _TabPageSVID.Padding = new Padding(5);
            _TabPageSVID.Size = new Size(564, 272);
            _TabPageSVID.TabIndex = 0;
            _TabPageSVID.Text = "SVID (Status Variable)";
            _TabPageSVID.UseVisualStyleBackColor = true;
            // 
            // _DataGridViewSVID
            // 
            _DataGridViewSVID.AllowUserToAddRows = false;
            _DataGridViewSVID.AllowUserToDeleteRows = false;
            _DataGridViewSVID.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _DataGridViewSVID.BackgroundColor = Color.White;
            _DataGridViewSVID.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _DataGridViewSVID.Dock = DockStyle.Fill;
            _DataGridViewSVID.Font = new Font("D2Coding", 9F);
            _DataGridViewSVID.Location = new Point(5, 5);
            _DataGridViewSVID.Name = "_DataGridViewSVID";
            _DataGridViewSVID.RowHeadersWidth = 30;
            _DataGridViewSVID.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _DataGridViewSVID.Size = new Size(554, 262);
            _DataGridViewSVID.TabIndex = 0;
            // 
            // _TabPageECID
            // 
            _TabPageECID.Controls.Add(_DataGridViewECID);
            _TabPageECID.Location = new Point(4, 23);
            _TabPageECID.Name = "_TabPageECID";
            _TabPageECID.Padding = new Padding(5);
            _TabPageECID.Size = new Size(564, 272);
            _TabPageECID.TabIndex = 1;
            _TabPageECID.Text = "ECID (Equipment Constant)";
            _TabPageECID.UseVisualStyleBackColor = true;
            // 
            // _DataGridViewECID
            // 
            _DataGridViewECID.AllowUserToAddRows = false;
            _DataGridViewECID.AllowUserToDeleteRows = false;
            _DataGridViewECID.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _DataGridViewECID.BackgroundColor = Color.White;
            _DataGridViewECID.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _DataGridViewECID.Dock = DockStyle.Fill;
            _DataGridViewECID.Font = new Font("D2Coding", 9F);
            _DataGridViewECID.Location = new Point(5, 5);
            _DataGridViewECID.Name = "_DataGridViewECID";
            _DataGridViewECID.RowHeadersWidth = 30;
            _DataGridViewECID.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _DataGridViewECID.Size = new Size(554, 262);
            _DataGridViewECID.TabIndex = 0;
            // 
            // _ButtonRefreshData
            // 
            _ButtonRefreshData.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _ButtonRefreshData.BackColor = Color.FromArgb(52, 152, 219);
            _ButtonRefreshData.Font = new Font("D2Coding", 10F);
            _ButtonRefreshData.ForeColor = Color.Black;
            _ButtonRefreshData.Location = new Point(10, 319);
            _ButtonRefreshData.Name = "_ButtonRefreshData";
            _ButtonRefreshData.Size = new Size(100, 32);
            _ButtonRefreshData.TabIndex = 1;
            _ButtonRefreshData.Text = "Refresh";
            _ButtonRefreshData.ThemeStyle = UI.Controls.ThemeStyle.Info_Sky;
            _ButtonRefreshData.TooltipText = null;
            _ButtonRefreshData.UseVisualStyleBackColor = false;
            _ButtonRefreshData.Click += _ButtonRefreshData_Click;
            // 
            // _ButtonSaveData
            // 
            _ButtonSaveData.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            _ButtonSaveData.BackColor = Color.FromArgb(46, 204, 113);
            _ButtonSaveData.Font = new Font("D2Coding", 10F);
            _ButtonSaveData.ForeColor = Color.Black;
            _ButtonSaveData.Location = new Point(120, 319);
            _ButtonSaveData.Name = "_ButtonSaveData";
            _ButtonSaveData.Size = new Size(100, 32);
            _ButtonSaveData.TabIndex = 2;
            _ButtonSaveData.Text = "Save";
            _ButtonSaveData.ThemeStyle = UI.Controls.ThemeStyle.Success_Green;
            _ButtonSaveData.TooltipText = null;
            _ButtonSaveData.UseVisualStyleBackColor = false;
            _ButtonSaveData.Click += _ButtonSaveData_Click;
            // 
            // SecsGem_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "SecsGem_View";
            Size = new Size(600, 450);
            Load += SecsGem_View_Load;
            _PanelMain.ResumeLayout(false);
            _Panel1.ResumeLayout(false);
            _TabControl.ResumeLayout(false);
            _TabPageStatus.ResumeLayout(false);
            _TabPageStatus.PerformLayout();
            _TabPageControl.ResumeLayout(false);
            _TabPageEvent.ResumeLayout(false);
            _GroupBoxAlarm.ResumeLayout(false);
            _GroupBoxCEID.ResumeLayout(false);
            _TabPageData.ResumeLayout(false);
            _TabControlData.ResumeLayout(false);
            _TabPageSVID.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_DataGridViewSVID).EndInit();
            _TabPageECID.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_DataGridViewECID).EndInit();
            ResumeLayout(false);
        }
        #endregion

        private TabControl _TabControl;
        private TabPage _TabPageStatus;
        private TabPage _TabPageControl;
        private TabPage _TabPageEvent;
        private TabPage _TabPageData;

        // Status Tab
        private Panel _PanelStatusLed;
        private Label _LabelConnectionStatus;
        private Label _LabelConnectionValue;
        private Label _LabelControlState;
        private Label _LabelControlStateValue;
        private Label _LabelCommunicating;
        private Label _LabelCommunicatingValue;

        // Control Tab
        private Controls._Button _ButtonGoOnlineRemote;
        private Controls._Button _ButtonGoOnlineLocal;
        private Controls._Button _ButtonGoOffline;
        private Controls._Button _ButtonStart;
        private Controls._Button _ButtonStop;

        // Event Tab
        private GroupBox _GroupBoxCEID;
        private ComboBox _ComboBoxCEID;
        private Controls._Button _ButtonSendCEID;
        private GroupBox _GroupBoxAlarm;
        private ComboBox _ComboBoxALID;
        private Controls._Button _ButtonAlarmSet;
        private Controls._Button _ButtonAlarmClear;

        // Data Tab
        private TabControl _TabControlData;
        private TabPage _TabPageSVID;
        private TabPage _TabPageECID;
        private DataGridView _DataGridViewSVID;
        private DataGridView _DataGridViewECID;
        private Controls._Button _ButtonRefreshData;
        private Controls._Button _ButtonSaveData;
    }
}
