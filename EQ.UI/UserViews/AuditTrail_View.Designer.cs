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
            _btnExport = new EQ.UI.Controls._Button();
            _btnApplyFilter = new EQ.UI.Controls._Button();
            _dateTo = new DateTimePicker();
            _lblTo = new EQ.UI.Controls._Label();
            _dateFrom = new DateTimePicker();
            _lblFrom = new EQ.UI.Controls._Label();
            panelMain = new Panel();
            _gridAuditTrail = new EQ.UI.Controls._DataGridView();
            _Button1 = new EQ.UI.Controls._Button();
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
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(112, 800);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // _chkAll
            // 
            _chkAll.Appearance = Appearance.Button;
            _chkAll.BackColor = Color.FromArgb(52, 152, 219);
            _chkAll.Font = new Font("D2Coding", 12F);
            _chkAll.ForeColor = Color.Black;
            _chkAll.Location = new Point(3, 3);
            _chkAll.Name = "_chkAll";
            _chkAll.Size = new Size(103, 42);
            _chkAll.TabIndex = 7;
            _chkAll.Text = "All";
            _chkAll.TextAlign = ContentAlignment.MiddleCenter;
            _chkAll.UseVisualStyleBackColor = false;
            _chkAll.Visible = false;
            _chkAll.CheckedChanged += _chkAll_CheckedChanged;
            // 
            // _chkLogin
            // 
            _chkLogin.Appearance = Appearance.Button;
            _chkLogin.BackColor = Color.FromArgb(52, 152, 219);
            _chkLogin.Font = new Font("D2Coding", 12F);
            _chkLogin.ForeColor = Color.Black;
            _chkLogin.Location = new Point(3, 51);
            _chkLogin.Name = "_chkLogin";
            _chkLogin.Size = new Size(103, 42);
            _chkLogin.TabIndex = 8;
            _chkLogin.Text = "Login";
            _chkLogin.TextAlign = ContentAlignment.MiddleCenter;
            _chkLogin.UseVisualStyleBackColor = false;
            _chkLogin.CheckStateChanged += _chkLogin_CheckStateChanged;
            // 
            // _chkUser
            // 
            _chkUser.Appearance = Appearance.Button;
            _chkUser.BackColor = Color.FromArgb(52, 152, 219);
            _chkUser.Font = new Font("D2Coding", 12F);
            _chkUser.ForeColor = Color.Black;
            _chkUser.Location = new Point(3, 99);
            _chkUser.Name = "_chkUser";
            _chkUser.Size = new Size(103, 42);
            _chkUser.TabIndex = 9;
            _chkUser.Text = "User";
            _chkUser.TextAlign = ContentAlignment.MiddleCenter;
            _chkUser.UseVisualStyleBackColor = false;
            _chkUser.CheckStateChanged += _chkLogin_CheckStateChanged;
            // 
            // _chkRecipe
            // 
            _chkRecipe.Appearance = Appearance.Button;
            _chkRecipe.BackColor = Color.FromArgb(52, 152, 219);
            _chkRecipe.Font = new Font("D2Coding", 12F);
            _chkRecipe.ForeColor = Color.Black;
            _chkRecipe.Location = new Point(3, 147);
            _chkRecipe.Name = "_chkRecipe";
            _chkRecipe.Size = new Size(103, 42);
            _chkRecipe.TabIndex = 10;
            _chkRecipe.Text = "Recipe";
            _chkRecipe.TextAlign = ContentAlignment.MiddleCenter;
            _chkRecipe.UseVisualStyleBackColor = false;
            _chkRecipe.CheckStateChanged += _chkLogin_CheckStateChanged;
            // 
            // _chkParameter
            // 
            _chkParameter.Appearance = Appearance.Button;
            _chkParameter.BackColor = Color.FromArgb(52, 152, 219);
            _chkParameter.Font = new Font("D2Coding", 12F);
            _chkParameter.ForeColor = Color.Black;
            _chkParameter.Location = new Point(3, 195);
            _chkParameter.Name = "_chkParameter";
            _chkParameter.Size = new Size(103, 42);
            _chkParameter.TabIndex = 11;
            _chkParameter.Text = "Parameter";
            _chkParameter.TextAlign = ContentAlignment.MiddleCenter;
            _chkParameter.UseVisualStyleBackColor = false;
            _chkParameter.CheckStateChanged += _chkLogin_CheckStateChanged;
            // 
            // _chkSystem
            // 
            _chkSystem.Appearance = Appearance.Button;
            _chkSystem.BackColor = Color.FromArgb(52, 152, 219);
            _chkSystem.Font = new Font("D2Coding", 12F);
            _chkSystem.ForeColor = Color.Black;
            _chkSystem.Location = new Point(3, 243);
            _chkSystem.Name = "_chkSystem";
            _chkSystem.Size = new Size(103, 42);
            _chkSystem.TabIndex = 12;
            _chkSystem.Text = "System";
            _chkSystem.TextAlign = ContentAlignment.MiddleCenter;
            _chkSystem.UseVisualStyleBackColor = false;
            _chkSystem.CheckStateChanged += _chkLogin_CheckStateChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(_Button1);
            panel1.Controls.Add(_btnExport);
            panel1.Controls.Add(_btnApplyFilter);
            panel1.Controls.Add(_dateTo);
            panel1.Controls.Add(_lblTo);
            panel1.Controls.Add(_dateFrom);
            panel1.Controls.Add(_lblFrom);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(112, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1788, 69);
            panel1.TabIndex = 1;
            // 
            // _btnExport
            // 
            _btnExport.BackColor = Color.FromArgb(48, 63, 159);
            _btnExport.Font = new Font("D2Coding", 12F);
            _btnExport.ForeColor = Color.White;
            _btnExport.Location = new Point(580, 15);
            _btnExport.Name = "_btnExport";
            _btnExport.Size = new Size(120, 32);
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
            _btnApplyFilter.Location = new Point(450, 15);
            _btnApplyFilter.Name = "_btnApplyFilter";
            _btnApplyFilter.Size = new Size(120, 32);
            _btnApplyFilter.TabIndex = 4;
            _btnApplyFilter.Text = "Apply Filter";
            _btnApplyFilter.TooltipText = null;
            _btnApplyFilter.UseVisualStyleBackColor = false;
            _btnApplyFilter.Click += _btnApplyFilter_Click;
            // 
            // _dateTo
            // 
            _dateTo.CustomFormat = "yyyy-MM-dd";
            _dateTo.Format = DateTimePickerFormat.Custom;
            _dateTo.Location = new Point(290, 15);
            _dateTo.Name = "_dateTo";
            _dateTo.Size = new Size(150, 32);
            _dateTo.TabIndex = 3;
            // 
            // _lblTo
            // 
            _lblTo.BackColor = Color.FromArgb(149, 165, 166);
            _lblTo.Font = new Font("D2Coding", 12F);
            _lblTo.ForeColor = Color.White;
            _lblTo.Location = new Point(245, 15);
            _lblTo.Name = "_lblTo";
            _lblTo.Size = new Size(40, 30);
            _lblTo.TabIndex = 2;
            _lblTo.Text = "To:";
            _lblTo.TextAlign = ContentAlignment.MiddleRight;
            _lblTo.TooltipText = null;
            // 
            // _dateFrom
            // 
            _dateFrom.CustomFormat = "yyyy-MM-dd";
            _dateFrom.Format = DateTimePickerFormat.Custom;
            _dateFrom.Location = new Point(85, 15);
            _dateFrom.Name = "_dateFrom";
            _dateFrom.Size = new Size(150, 32);
            _dateFrom.TabIndex = 1;
            // 
            // _lblFrom
            // 
            _lblFrom.BackColor = Color.FromArgb(149, 165, 166);
            _lblFrom.Font = new Font("D2Coding", 12F);
            _lblFrom.ForeColor = Color.White;
            _lblFrom.Location = new Point(20, 15);
            _lblFrom.Name = "_lblFrom";
            _lblFrom.Size = new Size(60, 30);
            _lblFrom.TabIndex = 0;
            _lblFrom.Text = "From:";
            _lblFrom.TextAlign = ContentAlignment.MiddleRight;
            _lblFrom.TooltipText = null;
            // 
            // panelMain
            // 
            panelMain.Controls.Add(_gridAuditTrail);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(112, 69);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1788, 731);
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
            _gridAuditTrail.MultiSelect = false;
            _gridAuditTrail.Name = "_gridAuditTrail";
            _gridAuditTrail.ReadOnly = true;
            _gridAuditTrail.RowHeadersVisible = false;
            _gridAuditTrail.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _gridAuditTrail.Size = new Size(1788, 731);
            _gridAuditTrail.TabIndex = 0;
            _gridAuditTrail.CellDoubleClick += _gridAuditTrail_CellDoubleClick;
            // 
            // _Button1
            // 
            _Button1.BackColor = Color.FromArgb(48, 63, 159);
            _Button1.Font = new Font("D2Coding", 12F);
            _Button1.ForeColor = Color.White;
            _Button1.Location = new Point(706, 15);
            _Button1.Name = "_Button1";
            _Button1.Size = new Size(120, 32);
            _Button1.TabIndex = 5;
            _Button1.Text = "Export PDF";
            _Button1.TooltipText = null;
            _Button1.UseVisualStyleBackColor = false;
            _Button1.Click += Export_PDF;
            // 
            // AuditTrail_View
            // 
            AutoScaleDimensions = new SizeF(10F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelMain);
            Controls.Add(panel1);
            Controls.Add(flowLayoutPanel1);
            Name = "AuditTrail_View";
            Size = new Size(1900, 800);
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
    }
}
