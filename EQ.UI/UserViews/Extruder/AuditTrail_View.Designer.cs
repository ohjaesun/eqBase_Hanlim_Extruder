namespace EQ.UI.UserViews
{
    partial class AuditTrail_View
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            flowLayoutPanel1 = new FlowLayoutPanel();
            _chkAll = new EQ.UI.Controls._CheckBox();
            _chkLogin = new EQ.UI.Controls._CheckBox();
            _chkUser = new EQ.UI.Controls._CheckBox();
            _chkRecipe = new EQ.UI.Controls._CheckBox();
            _chkParameter = new EQ.UI.Controls._CheckBox();
            _chkSystem = new EQ.UI.Controls._CheckBox();
            panel1 = new Panel();
            _Button5 = new EQ.UI.Controls._Button();
            _Button4 = new EQ.UI.Controls._Button();
            _Button3 = new EQ.UI.Controls._Button();
            _Button2 = new EQ.UI.Controls._Button();
            _Button1 = new EQ.UI.Controls._Button();
            _btnExport = new EQ.UI.Controls._Button();
            _btnApplyFilter = new EQ.UI.Controls._Button();
            _dateTo = new DateTimePicker();
            _lblTo = new EQ.UI.Controls._Label();
            _dateFrom = new DateTimePicker();
            _Label2 = new EQ.UI.Controls._Label();
            _Label1 = new EQ.UI.Controls._Label();
            _lblFrom = new EQ.UI.Controls._Label();
            panelMain = new Panel();
            _gridAuditTrail = new EQ.UI.Controls._DataGridView();
            flowLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_gridAuditTrail).BeginInit();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(_chkAll);
            flowLayoutPanel1.Controls.Add(_chkLogin);
            flowLayoutPanel1.Controls.Add(_chkUser);
            flowLayoutPanel1.Controls.Add(_chkRecipe);
            flowLayoutPanel1.Controls.Add(_chkParameter);
            flowLayoutPanel1.Controls.Add(_chkSystem);
            flowLayoutPanel1.Dock = DockStyle.Left;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Margin = new Padding(2);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(102, 600);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // _chkAll
            // 
            _chkAll.Appearance = Appearance.Button;
            _chkAll.BackColor = SystemColors.Control;
            _chkAll.Font = new Font("D2Coding", 12F);
            _chkAll.ForeColor = SystemColors.ControlText;
            _chkAll.Location = new Point(2, 2);
            _chkAll.Margin = new Padding(2);
            _chkAll.Name = "_chkAll";
            _chkAll.Size = new Size(95, 45);
            _chkAll.TabIndex = 7;
            _chkAll.Text = "All";
            _chkAll.TextAlign = ContentAlignment.MiddleCenter;
            _chkAll.ThemeStyle = UI.Controls.ThemeStyle.Default;
            _chkAll.UseVisualStyleBackColor = false;
            _chkAll.Visible = false;
            _chkAll.CheckedChanged += _chkAll_CheckedChanged;
            // 
            // _chkLogin
            // 
            _chkLogin.Appearance = Appearance.Button;
            _chkLogin.BackColor = SystemColors.Control;
            _chkLogin.Font = new Font("D2Coding", 12F);
            _chkLogin.ForeColor = SystemColors.ControlText;
            _chkLogin.Location = new Point(2, 51);
            _chkLogin.Margin = new Padding(2);
            _chkLogin.Name = "_chkLogin";
            _chkLogin.Size = new Size(95, 45);
            _chkLogin.TabIndex = 8;
            _chkLogin.Text = "Login";
            _chkLogin.TextAlign = ContentAlignment.MiddleCenter;
            _chkLogin.ThemeStyle = UI.Controls.ThemeStyle.Default;
            _chkLogin.UseVisualStyleBackColor = false;
            _chkLogin.CheckStateChanged += _chkLogin_CheckStateChanged;
            // 
            // _chkUser
            // 
            _chkUser.Appearance = Appearance.Button;
            _chkUser.BackColor = SystemColors.Control;
            _chkUser.Font = new Font("D2Coding", 12F);
            _chkUser.ForeColor = SystemColors.ControlText;
            _chkUser.Location = new Point(2, 100);
            _chkUser.Margin = new Padding(2);
            _chkUser.Name = "_chkUser";
            _chkUser.Size = new Size(95, 45);
            _chkUser.TabIndex = 9;
            _chkUser.Text = "User";
            _chkUser.TextAlign = ContentAlignment.MiddleCenter;
            _chkUser.ThemeStyle = UI.Controls.ThemeStyle.Default;
            _chkUser.UseVisualStyleBackColor = false;
            _chkUser.CheckStateChanged += _chkLogin_CheckStateChanged;
            // 
            // _chkRecipe
            // 
            _chkRecipe.Appearance = Appearance.Button;
            _chkRecipe.BackColor = SystemColors.Control;
            _chkRecipe.Font = new Font("D2Coding", 12F);
            _chkRecipe.ForeColor = SystemColors.ControlText;
            _chkRecipe.Location = new Point(2, 149);
            _chkRecipe.Margin = new Padding(2);
            _chkRecipe.Name = "_chkRecipe";
            _chkRecipe.Size = new Size(95, 45);
            _chkRecipe.TabIndex = 10;
            _chkRecipe.Text = "Recipe";
            _chkRecipe.TextAlign = ContentAlignment.MiddleCenter;
            _chkRecipe.ThemeStyle = UI.Controls.ThemeStyle.Default;
            _chkRecipe.UseVisualStyleBackColor = false;
            _chkRecipe.CheckStateChanged += _chkLogin_CheckStateChanged;
            // 
            // _chkParameter
            // 
            _chkParameter.Appearance = Appearance.Button;
            _chkParameter.BackColor = SystemColors.Control;
            _chkParameter.Font = new Font("D2Coding", 12F);
            _chkParameter.ForeColor = SystemColors.ControlText;
            _chkParameter.Location = new Point(2, 198);
            _chkParameter.Margin = new Padding(2);
            _chkParameter.Name = "_chkParameter";
            _chkParameter.Size = new Size(95, 45);
            _chkParameter.TabIndex = 11;
            _chkParameter.Text = "Parameter";
            _chkParameter.TextAlign = ContentAlignment.MiddleCenter;
            _chkParameter.ThemeStyle = UI.Controls.ThemeStyle.Default;
            _chkParameter.UseVisualStyleBackColor = false;
            _chkParameter.CheckStateChanged += _chkLogin_CheckStateChanged;
            // 
            // _chkSystem
            // 
            _chkSystem.Appearance = Appearance.Button;
            _chkSystem.BackColor = SystemColors.Control;
            _chkSystem.Font = new Font("D2Coding", 12F);
            _chkSystem.ForeColor = SystemColors.ControlText;
            _chkSystem.Location = new Point(2, 247);
            _chkSystem.Margin = new Padding(2);
            _chkSystem.Name = "_chkSystem";
            _chkSystem.Size = new Size(95, 45);
            _chkSystem.TabIndex = 12;
            _chkSystem.Text = "System";
            _chkSystem.TextAlign = ContentAlignment.MiddleCenter;
            _chkSystem.ThemeStyle = UI.Controls.ThemeStyle.Default;
            _chkSystem.UseVisualStyleBackColor = false;
            _chkSystem.CheckStateChanged += _chkLogin_CheckStateChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(_Button5);
            panel1.Controls.Add(_Button4);
            panel1.Controls.Add(_Button3);
            panel1.Controls.Add(_Button2);
            panel1.Controls.Add(_Button1);
            panel1.Controls.Add(_btnExport);
            panel1.Controls.Add(_btnApplyFilter);
            panel1.Controls.Add(_dateTo);
            panel1.Controls.Add(_lblTo);
            panel1.Controls.Add(_dateFrom);
            panel1.Controls.Add(_Label2);
            panel1.Controls.Add(_Label1);
            panel1.Controls.Add(_lblFrom);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(102, 0);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(1418, 52);
            panel1.TabIndex = 1;
            // 
            // _Button5
            // 
            _Button5.BackColor = Color.FromArgb(48, 63, 159);
            _Button5.Font = new Font("D2Coding", 12F);
            _Button5.ForeColor = Color.White;
            _Button5.Location = new Point(493, 12);
            _Button5.Name = "_Button5";
            _Button5.Size = new Size(34, 28);
            _Button5.TabIndex = 6;
            _Button5.Text = "->";
            _Button5.TooltipText = null;
            _Button5.UseVisualStyleBackColor = false;
            _Button5.Click += _Button5_Click;
            // 
            // _Button4
            // 
            _Button4.BackColor = Color.FromArgb(48, 63, 159);
            _Button4.Font = new Font("D2Coding", 12F);
            _Button4.ForeColor = Color.White;
            _Button4.Location = new Point(453, 12);
            _Button4.Name = "_Button4";
            _Button4.Size = new Size(34, 28);
            _Button4.TabIndex = 6;
            _Button4.Text = "<-";
            _Button4.TooltipText = null;
            _Button4.UseVisualStyleBackColor = false;
            _Button4.Click += _Button4_Click;
            // 
            // _Button3
            // 
            _Button3.BackColor = Color.FromArgb(48, 63, 159);
            _Button3.Font = new Font("D2Coding", 12F);
            _Button3.ForeColor = Color.White;
            _Button3.Location = new Point(236, 11);
            _Button3.Name = "_Button3";
            _Button3.Size = new Size(34, 28);
            _Button3.TabIndex = 6;
            _Button3.Text = "->";
            _Button3.TooltipText = null;
            _Button3.UseVisualStyleBackColor = false;
            _Button3.Click += _Button3_Click;
            // 
            // _Button2
            // 
            _Button2.BackColor = Color.FromArgb(48, 63, 159);
            _Button2.Font = new Font("D2Coding", 12F);
            _Button2.ForeColor = Color.White;
            _Button2.Location = new Point(196, 11);
            _Button2.Name = "_Button2";
            _Button2.Size = new Size(34, 28);
            _Button2.TabIndex = 6;
            _Button2.Text = "<-";
            _Button2.TooltipText = null;
            _Button2.UseVisualStyleBackColor = false;
            _Button2.Click += _Button2_Click;
            // 
            // _Button1
            // 
            _Button1.BackColor = Color.FromArgb(48, 63, 159);
            _Button1.Font = new Font("D2Coding", 12F);
            _Button1.ForeColor = Color.White;
            _Button1.Location = new Point(795, 5);
            _Button1.Margin = new Padding(2);
            _Button1.Name = "_Button1";
            _Button1.Size = new Size(96, 37);
            _Button1.TabIndex = 5;
            _Button1.Text = "Export PDF";
            _Button1.TooltipText = null;
            _Button1.UseVisualStyleBackColor = false;
            _Button1.Click += Export_PDF;
            // 
            // _btnExport
            // 
            _btnExport.BackColor = Color.FromArgb(48, 63, 159);
            _btnExport.Font = new Font("D2Coding", 12F);
            _btnExport.ForeColor = Color.White;
            _btnExport.Location = new Point(694, 5);
            _btnExport.Margin = new Padding(2);
            _btnExport.Name = "_btnExport";
            _btnExport.Size = new Size(96, 37);
            _btnExport.TabIndex = 5;
            _btnExport.Text = "Export CSV";
            _btnExport.TooltipText = null;
            _btnExport.UseVisualStyleBackColor = false;
            _btnExport.Click += _btnExport_Click;
            // 
            // _btnApplyFilter
            // 
            _btnApplyFilter.BackColor = Color.FromArgb(48, 63, 159);
            _btnApplyFilter.Font = new Font("D2Coding", 12F);
            _btnApplyFilter.ForeColor = Color.White;
            _btnApplyFilter.Location = new Point(590, 5);
            _btnApplyFilter.Margin = new Padding(2);
            _btnApplyFilter.Name = "_btnApplyFilter";
            _btnApplyFilter.Size = new Size(96, 37);
            _btnApplyFilter.TabIndex = 4;
            _btnApplyFilter.Text = "Apply";
            _btnApplyFilter.TooltipText = null;
            _btnApplyFilter.UseVisualStyleBackColor = false;
            _btnApplyFilter.Click += _btnApplyFilter_Click;
            // 
            // _dateTo
            // 
            _dateTo.CustomFormat = "yyyy-MM-dd";
            _dateTo.Format = DateTimePickerFormat.Custom;
            _dateTo.Location = new Point(322, 11);
            _dateTo.Margin = new Padding(2);
            _dateTo.Name = "_dateTo";
            _dateTo.Size = new Size(121, 26);
            _dateTo.TabIndex = 3;
            // 
            // _lblTo
            // 
            _lblTo.BackColor = Color.FromArgb(149, 165, 166);
            _lblTo.Font = new Font("D2Coding", 12F);
            _lblTo.ForeColor = Color.White;
            _lblTo.Location = new Point(286, 11);
            _lblTo.Margin = new Padding(2, 0, 2, 0);
            _lblTo.Name = "_lblTo";
            _lblTo.Size = new Size(32, 22);
            _lblTo.TabIndex = 2;
            _lblTo.Text = "To:";
            _lblTo.TextAlign = ContentAlignment.MiddleRight;
            _lblTo.TooltipText = null;
            // 
            // _dateFrom
            // 
            _dateFrom.CustomFormat = "yyyy-MM-dd";
            _dateFrom.Format = DateTimePickerFormat.Custom;
            _dateFrom.Location = new Point(68, 11);
            _dateFrom.Margin = new Padding(2);
            _dateFrom.Name = "_dateFrom";
            _dateFrom.Size = new Size(121, 26);
            _dateFrom.TabIndex = 1;
            // 
            // _Label2
            // 
            _Label2.BackColor = Color.FromArgb(46, 204, 113);
            _Label2.Font = new Font("D2Coding", 12F);
            _Label2.ForeColor = Color.Black;
            _Label2.Location = new Point(1038, 7);
            _Label2.Margin = new Padding(2, 0, 2, 0);
            _Label2.Name = "_Label2";
            _Label2.Size = new Size(114, 22);
            _Label2.TabIndex = 0;
            _Label2.Text = "용량";
            _Label2.TextAlign = ContentAlignment.MiddleLeft;
            _Label2.ThemeStyle = UI.Controls.ThemeStyle.Success_Green;
            _Label2.TooltipText = null;
            // 
            // _Label1
            // 
            _Label1.BackColor = Color.FromArgb(149, 165, 166);
            _Label1.Font = new Font("D2Coding", 12F);
            _Label1.ForeColor = Color.White;
            _Label1.Location = new Point(908, 7);
            _Label1.Margin = new Padding(2, 0, 2, 0);
            _Label1.Name = "_Label1";
            _Label1.Size = new Size(113, 22);
            _Label1.TabIndex = 0;
            _Label1.Text = "Usage Disk :";
            _Label1.TextAlign = ContentAlignment.MiddleRight;
            _Label1.TooltipText = null;
            // 
            // _lblFrom
            // 
            _lblFrom.BackColor = Color.FromArgb(149, 165, 166);
            _lblFrom.Font = new Font("D2Coding", 12F);
            _lblFrom.ForeColor = Color.White;
            _lblFrom.Location = new Point(16, 11);
            _lblFrom.Margin = new Padding(2, 0, 2, 0);
            _lblFrom.Name = "_lblFrom";
            _lblFrom.Size = new Size(48, 22);
            _lblFrom.TabIndex = 0;
            _lblFrom.Text = "From:";
            _lblFrom.TextAlign = ContentAlignment.MiddleRight;
            _lblFrom.TooltipText = null;
            // 
            // panelMain
            // 
            panelMain.Controls.Add(_gridAuditTrail);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(102, 52);
            panelMain.Margin = new Padding(2);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1418, 548);
            panelMain.TabIndex = 1;
            // 
            // _gridAuditTrail
            // 
            _gridAuditTrail.AllowUserToAddRows = false;
            _gridAuditTrail.AllowUserToDeleteRows = false;
            _gridAuditTrail.AllowUserToResizeRows = false;
            _gridAuditTrail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _gridAuditTrail.BackgroundColor = Color.FromArgb(255, 255, 225);
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(255, 255, 225);
            dataGridViewCellStyle1.Font = new Font("D2Coding", 12F);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            _gridAuditTrail.DefaultCellStyle = dataGridViewCellStyle1;
            _gridAuditTrail.Dock = DockStyle.Fill;
            _gridAuditTrail.Font = new Font("D2Coding", 12F);
            _gridAuditTrail.Location = new Point(0, 0);
            _gridAuditTrail.Margin = new Padding(2);
            _gridAuditTrail.MultiSelect = false;
            _gridAuditTrail.Name = "_gridAuditTrail";
            _gridAuditTrail.ReadOnly = true;
            _gridAuditTrail.RowHeadersVisible = false;
            _gridAuditTrail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _gridAuditTrail.Size = new Size(1418, 548);
            _gridAuditTrail.TabIndex = 0;
            _gridAuditTrail.CellDoubleClick += _gridAuditTrail_CellDoubleClick;
            // 
            // AuditTrail_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelMain);
            Controls.Add(panel1);
            Controls.Add(flowLayoutPanel1);
            Margin = new Padding(2, 3, 2, 3);
            Name = "AuditTrail_View";
            Size = new Size(1520, 600);
            Load += AuditTrail_View_Load;
            flowLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_gridAuditTrail).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Panel panel1;
        private Panel panelMain;
        private DateTimePicker _dateFrom;
        private DateTimePicker _dateTo;
        private Controls._Button _btnApplyFilter;
        private Controls._Button _btnExport;
        private Controls._DataGridView _gridAuditTrail;
        private Controls._CheckBox _chkAll;
        private Controls._CheckBox _chkLogin;
        private Controls._CheckBox _chkUser;
        private Controls._CheckBox _chkRecipe;
        private Controls._CheckBox _chkParameter;
        private Controls._CheckBox _chkSystem;
        private Controls._Label _lblFrom;
        private Controls._Label _lblTo;
        private Controls._Button _Button1;
        private Controls._Label _Label1;
        private Controls._Label _Label2;
        private Controls._Button _Button5;
        private Controls._Button _Button4;
        private Controls._Button _Button3;
        private Controls._Button _Button2;
    }
}
