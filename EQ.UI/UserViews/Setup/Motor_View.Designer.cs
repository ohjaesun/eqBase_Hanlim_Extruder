namespace EQ.UI.UserViews
{
    partial class Motor_View
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            richTextBox1 = new RichTextBox();
            dataGridView1 = new EQ.UI.Controls._DataGridView();
            motionMove_View1 = new MotionMove_View();
            _Panel2 = new EQ.UI.Controls._Panel();
            _Panel3 = new EQ.UI.Controls._Panel();
            _Panel4 = new EQ.UI.Controls._Panel();
            _PanelMain.SuspendLayout();
            _Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            _Panel2.SuspendLayout();
            _Panel3.SuspendLayout();
            _Panel4.SuspendLayout();
            SuspendLayout();
            // 
            // _LabelTitle
            // 
            _LabelTitle.Size = new Size(893, 59);
            // 
            // _ButtonSave
            // 
            _ButtonSave.Location = new Point(893, 0);
            // 
            // _PanelMain
            // 
            _PanelMain.Controls.Add(_Panel4);
            _PanelMain.Controls.Add(_Panel3);
            _PanelMain.Controls.Add(_Panel2);
            _PanelMain.Size = new Size(993, 713);
            // 
            // _Panel1
            // 
            _Panel1.Size = new Size(993, 59);
            // 
            // richTextBox1
            // 
            richTextBox1.BackColor = Color.FromArgb(40, 40, 40);
            richTextBox1.BorderStyle = BorderStyle.None;
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Font = new Font("D2Coding", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 129);
            richTextBox1.ForeColor = Color.White;
            richTextBox1.Location = new Point(0, 0);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.Size = new Size(993, 118);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.FromArgb(255, 255, 225);
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewCellStyle1.Font = new Font("D2Coding", 12F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeight = 35;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(255, 255, 225);
            dataGridViewCellStyle2.Font = new Font("D2Coding", 12F);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(155, 89, 182);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.Font = new Font("D2Coding", 12F);
            dataGridView1.GridColor = Color.FromArgb(189, 195, 199);
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(993, 389);
            dataGridView1.TabIndex = 2;
            dataGridView1.ThemeStyle = UI.Controls.ThemeStyle.Display_LightYellow;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellPainting += dataGridView1_CellPainting;
            // 
            // motionMove_View1
            // 
            motionMove_View1.Dock = DockStyle.Fill;
            motionMove_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            motionMove_View1.Location = new Point(0, 0);
            motionMove_View1.Margin = new Padding(3, 5, 3, 5);
            motionMove_View1.Name = "motionMove_View1";
            motionMove_View1.Size = new Size(993, 206);
            motionMove_View1.TabIndex = 3;
            // 
            // _Panel2
            // 
            _Panel2.BackColor = SystemColors.Control;
            _Panel2.Controls.Add(richTextBox1);
            _Panel2.Dock = DockStyle.Bottom;
            _Panel2.ForeColor = SystemColors.ControlText;
            _Panel2.Location = new Point(0, 595);
            _Panel2.Name = "_Panel2";
            _Panel2.Size = new Size(993, 118);
            _Panel2.TabIndex = 4;
            // 
            // _Panel3
            // 
            _Panel3.BackColor = SystemColors.Control;
            _Panel3.Controls.Add(motionMove_View1);
            _Panel3.Dock = DockStyle.Bottom;
            _Panel3.ForeColor = SystemColors.ControlText;
            _Panel3.Location = new Point(0, 389);
            _Panel3.Name = "_Panel3";
            _Panel3.Size = new Size(993, 206);
            _Panel3.TabIndex = 4;
            // 
            // _Panel4
            // 
            _Panel4.BackColor = SystemColors.Control;
            _Panel4.Controls.Add(dataGridView1);
            _Panel4.Dock = DockStyle.Fill;
            _Panel4.ForeColor = SystemColors.ControlText;
            _Panel4.Location = new Point(0, 0);
            _Panel4.Name = "_Panel4";
            _Panel4.Size = new Size(993, 389);
            _Panel4.TabIndex = 4;
            // 
            // Motor_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "Motor_View";
            Size = new Size(993, 772);
            Load += Motor_View_Load;
            _PanelMain.ResumeLayout(false);
            _Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            _Panel2.ResumeLayout(false);
            _Panel3.ResumeLayout(false);
            _Panel4.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox richTextBox1;
        private EQ.UI.Controls._DataGridView dataGridView1;
        private MotionMove_View motionMove_View1;
        private Controls._Panel _Panel4;
        private Controls._Panel _Panel3;
        private Controls._Panel _Panel2;
    }
}