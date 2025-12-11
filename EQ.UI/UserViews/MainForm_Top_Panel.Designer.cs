namespace EQ.UI.UserViews
{
    partial class MainForm_Top_Panel
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
            components = new System.ComponentModel.Container();
            tableLayoutPanel1 = new TableLayoutPanel();
            _Label_Title = new EQ.UI.Controls._Label();
            _Label_rcpName = new EQ.UI.Controls._Label();
            _Label_SeqStatus = new EQ.UI.Controls._Label();
            _Label4 = new EQ.UI.Controls._Label();
            _LabelLamp = new EQ.UI.Controls._Label();
            _LabelVer = new EQ.UI.Controls._Label();
            _LabelTime = new EQ.UI.Controls._Label();
            _LabelUser = new EQ.UI.Controls._Label();
            timerFlicker = new System.Windows.Forms.Timer(components);
            timer1000 = new System.Windows.Forms.Timer(components);
            timer100 = new System.Windows.Forms.Timer(components);
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.ColumnCount = 8;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 131F));
            tableLayoutPanel1.Controls.Add(_Label_Title, 0, 0);
            tableLayoutPanel1.Controls.Add(_Label_rcpName, 1, 0);
            tableLayoutPanel1.Controls.Add(_Label_SeqStatus, 2, 0);
            tableLayoutPanel1.Controls.Add(_Label4, 3, 0);
            tableLayoutPanel1.Controls.Add(_LabelLamp, 4, 0);
            tableLayoutPanel1.Controls.Add(_LabelVer, 5, 0);
            tableLayoutPanel1.Controls.Add(_LabelTime, 6, 0);
            tableLayoutPanel1.Controls.Add(_LabelUser, 7, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1083, 65);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // _Label_Title
            // 
            _Label_Title.BackColor = Color.FromArgb(149, 165, 166);
            _Label_Title.Dock = DockStyle.Fill;
            _Label_Title.Font = new Font("D2Coding", 12F);
            _Label_Title.ForeColor = Color.White;
            _Label_Title.Location = new Point(4, 1);
            _Label_Title.Name = "_Label_Title";
            _Label_Title.Size = new Size(114, 63);
            _Label_Title.TabIndex = 0;
            _Label_Title.Text = "INNOBIZ";
            _Label_Title.TextAlign = ContentAlignment.MiddleCenter;
            _Label_Title.TooltipText = null;
            // 
            // _Label_rcpName
            // 
            _Label_rcpName.BackColor = Color.Black;
            _Label_rcpName.Dock = DockStyle.Fill;
            _Label_rcpName.Font = new Font("D2Coding", 12F);
            _Label_rcpName.ForeColor = Color.White;
            _Label_rcpName.Location = new Point(125, 1);
            _Label_rcpName.Name = "_Label_rcpName";
            _Label_rcpName.Size = new Size(157, 63);
            _Label_rcpName.TabIndex = 0;
            _Label_rcpName.Text = "RCP_Title";
            _Label_rcpName.TextAlign = ContentAlignment.MiddleCenter;
            _Label_rcpName.ThemeStyle = UI.Controls.ThemeStyle.Black_White;
            _Label_rcpName.TooltipText = null;
            // 
            // _Label_SeqStatus
            // 
            _Label_SeqStatus.BackColor = Color.FromArgb(149, 165, 166);
            _Label_SeqStatus.Dock = DockStyle.Fill;
            _Label_SeqStatus.Font = new Font("D2Coding", 12F);
            _Label_SeqStatus.ForeColor = Color.White;
            _Label_SeqStatus.Location = new Point(289, 1);
            _Label_SeqStatus.Name = "_Label_SeqStatus";
            _Label_SeqStatus.Size = new Size(114, 63);
            _Label_SeqStatus.TabIndex = 0;
            _Label_SeqStatus.Text = "SEQ";
            _Label_SeqStatus.TextAlign = ContentAlignment.MiddleCenter;
            _Label_SeqStatus.TooltipText = null;
            // 
            // _Label4
            // 
            _Label4.BackColor = Color.FromArgb(149, 165, 166);
            _Label4.Dock = DockStyle.Fill;
            _Label4.Font = new Font("D2Coding", 12F);
            _Label4.ForeColor = Color.White;
            _Label4.Location = new Point(410, 1);
            _Label4.Name = "_Label4";
            _Label4.Size = new Size(114, 63);
            _Label4.TabIndex = 0;
            _Label4.Text = "ACT";
            _Label4.TextAlign = ContentAlignment.MiddleCenter;
            _Label4.TooltipText = null;
            // 
            // _LabelLamp
            // 
            _LabelLamp.BackColor = Color.FromArgb(149, 165, 166);
            _LabelLamp.Dock = DockStyle.Fill;
            _LabelLamp.Font = new Font("D2Coding", 12F);
            _LabelLamp.ForeColor = Color.White;
            _LabelLamp.Location = new Point(531, 1);
            _LabelLamp.Name = "_LabelLamp";
            _LabelLamp.Size = new Size(114, 63);
            _LabelLamp.TabIndex = 0;
            _LabelLamp.Text = "LAMP";
            _LabelLamp.TextAlign = ContentAlignment.MiddleCenter;
            _LabelLamp.TooltipText = null;
            // 
            // _LabelVer
            // 
            _LabelVer.BackColor = Color.FromArgb(149, 165, 166);
            _LabelVer.Dock = DockStyle.Fill;
            _LabelVer.Font = new Font("D2Coding", 12F);
            _LabelVer.ForeColor = Color.White;
            _LabelVer.Location = new Point(652, 1);
            _LabelVer.Name = "_LabelVer";
            _LabelVer.Size = new Size(94, 63);
            _LabelVer.TabIndex = 0;
            _LabelVer.Text = "_Label1";
            _LabelVer.TextAlign = ContentAlignment.MiddleCenter;
            _LabelVer.TooltipText = null;
            // 
            // _LabelTime
            // 
            _LabelTime.BackColor = Color.FromArgb(149, 165, 166);
            _LabelTime.Dock = DockStyle.Fill;
            _LabelTime.Font = new Font("D2Coding", 12F);
            _LabelTime.ForeColor = Color.White;
            _LabelTime.Location = new Point(753, 1);
            _LabelTime.Name = "_LabelTime";
            _LabelTime.Size = new Size(194, 63);
            _LabelTime.TabIndex = 0;
            _LabelTime.Text = "Time";
            _LabelTime.TextAlign = ContentAlignment.MiddleCenter;
            _LabelTime.TooltipText = null;
            // 
            // _LabelUser
            // 
            _LabelUser.BackColor = Color.FromArgb(149, 165, 166);
            _LabelUser.Dock = DockStyle.Fill;
            _LabelUser.Font = new Font("D2Coding", 12F);
            _LabelUser.ForeColor = Color.White;
            _LabelUser.Location = new Point(954, 1);
            _LabelUser.Name = "_LabelUser";
            _LabelUser.Size = new Size(125, 63);
            _LabelUser.TabIndex = 0;
            _LabelUser.Text = "Login";
            _LabelUser.TextAlign = ContentAlignment.MiddleCenter;
            _LabelUser.TooltipText = null;
            _LabelUser.Click += _Label8_Click;
            // 
            // timer1000
            // 
            timer1000.Tick += timer1000_Tick_1;
            // 
            // MainForm_Top_Panel
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            Margin = new Padding(3, 4, 3, 4);
            Name = "MainForm_Top_Panel";
            Size = new Size(1083, 65);
            Load += MainForm_Top_Panel_Load;
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Controls._Label _Label_Title;
        private Controls._Label _Label_rcpName;
        private Controls._Label _Label_SeqStatus;
        private Controls._Label _Label4;
        private Controls._Label _LabelLamp;
        private Controls._Label _LabelVer;
        private Controls._Label _LabelTime;
        private Controls._Label _LabelUser;
        private System.Windows.Forms.Timer timerFlicker;
        private System.Windows.Forms.Timer timer1000;
        private System.Windows.Forms.Timer timer100;
    }
}
