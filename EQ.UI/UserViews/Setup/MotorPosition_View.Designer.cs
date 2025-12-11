namespace EQ.UI.UserViews
{
    partial class MotorPosition_View
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            panelTop = new Panel();
            _LabelCurrentPos = new EQ.UI.Controls._Label();
            label2 = new Label();
            label1 = new Label();
            _ComboGroup = new EQ.UI.Controls._ComboBox();
            _ComboAxis = new EQ.UI.Controls._ComboBox();
            dataGridView1 = new EQ.UI.Controls._DataGridView();
            _LabelInfo = new EQ.UI.Controls._Label();
            timer1 = new System.Windows.Forms.Timer(components);
            _Panel2 = new EQ.UI.Controls._Panel();
            _Panel3 = new EQ.UI.Controls._Panel();
            _Panel4 = new EQ.UI.Controls._Panel();
            motionMove_View1 = new MotionMove_View();
            _PanelMain.SuspendLayout();
            _Panel1.SuspendLayout();
            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            _Panel2.SuspendLayout();
            _Panel3.SuspendLayout();
            _Panel4.SuspendLayout();
            SuspendLayout();
            // 
            // _LabelTitle
            // 
            _LabelTitle.Size = new Size(800, 59);
            _LabelTitle.Text = "Motor Position Teaching";
            // 
            // _ButtonSave
            // 
            _ButtonSave.Location = new Point(800, 0);
            // 
            // _PanelMain
            // 
            _PanelMain.Controls.Add(_Panel4);
            _PanelMain.Controls.Add(_Panel3);
            _PanelMain.Controls.Add(_Panel2);
            _PanelMain.Controls.Add(panelTop);
            _PanelMain.Size = new Size(900, 541);
            // 
            // _Panel1
            // 
            _Panel1.Size = new Size(900, 59);
            // 
            // panelTop
            // 
            panelTop.Controls.Add(_LabelCurrentPos);
            panelTop.Controls.Add(label2);
            panelTop.Controls.Add(label1);
            panelTop.Controls.Add(_ComboGroup);
            panelTop.Controls.Add(_ComboAxis);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(900, 45);
            panelTop.TabIndex = 0;
            // 
            // _LabelCurrentPos
            // 
            _LabelCurrentPos.AutoSize = true;
            _LabelCurrentPos.BackColor = Color.FromArgb(241, 196, 15);
            _LabelCurrentPos.Font = new Font("D2Coding", 15.7499981F, FontStyle.Regular, GraphicsUnit.Point, 129);
            _LabelCurrentPos.ForeColor = Color.Black;
            _LabelCurrentPos.Location = new Point(473, 12);
            _LabelCurrentPos.Name = "_LabelCurrentPos";
            _LabelCurrentPos.Size = new Size(80, 24);
            _LabelCurrentPos.TabIndex = 3;
            _LabelCurrentPos.Text = "_Label1";
            _LabelCurrentPos.ThemeStyle = UI.Controls.ThemeStyle.Warning_Yellow;
            _LabelCurrentPos.TooltipText = null;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(244, 13);
            label2.Name = "label2";
            label2.Size = new Size(56, 18);
            label2.TabIndex = 2;
            label2.Text = "Group:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(13, 13);
            label1.Name = "label1";
            label1.Size = new Size(48, 18);
            label1.TabIndex = 2;
            label1.Text = "Axis:";
            // 
            // _ComboGroup
            // 
            _ComboGroup.BackColor = Color.FromArgb(155, 89, 182);
            _ComboGroup.DrawMode = DrawMode.OwnerDrawFixed;
            _ComboGroup.DropDownStyle = ComboBoxStyle.DropDownList;
            _ComboGroup.Font = new Font("D2Coding", 12F);
            _ComboGroup.ForeColor = Color.Black;
            _ComboGroup.FormattingEnabled = true;
            _ComboGroup.Location = new Point(306, 9);
            _ComboGroup.Name = "_ComboGroup";
            _ComboGroup.Size = new Size(150, 27);
            _ComboGroup.TabIndex = 1;
            _ComboGroup.ThemeStyle = UI.Controls.ThemeStyle.Highlight_DeepYellow;
            _ComboGroup.TooltipText = null;
            _ComboGroup.SelectedIndexChanged += _ComboFilter_SelectedIndexChanged;
            // 
            // _ComboAxis
            // 
            _ComboAxis.BackColor = Color.FromArgb(155, 89, 182);
            _ComboAxis.DrawMode = DrawMode.OwnerDrawFixed;
            _ComboAxis.DropDownStyle = ComboBoxStyle.DropDownList;
            _ComboAxis.Font = new Font("D2Coding", 12F);
            _ComboAxis.ForeColor = Color.Black;
            _ComboAxis.FormattingEnabled = true;
            _ComboAxis.Location = new Point(67, 9);
            _ComboAxis.Name = "_ComboAxis";
            _ComboAxis.Size = new Size(150, 27);
            _ComboAxis.TabIndex = 0;
            _ComboAxis.ThemeStyle = UI.Controls.ThemeStyle.Highlight_DeepYellow;
            _ComboAxis.TooltipText = null;
            _ComboAxis.SelectedIndexChanged += _ComboFilter_SelectedIndexChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.FromArgb(149, 165, 166);
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(52, 73, 94);
            dataGridViewCellStyle1.Font = new Font("D2Coding", 12F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(149, 165, 166);
            dataGridViewCellStyle2.Font = new Font("D2Coding", 12F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Font = new Font("D2Coding", 12F);
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowTemplate.Height = 30;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(900, 259);
            dataGridView1.TabIndex = 1;
            dataGridView1.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellFormatting += dataGridView1_CellFormatting;
            dataGridView1.CellMouseDown += dataGridView1_CellMouseDown;
            // 
            // _LabelInfo
            // 
            _LabelInfo.BackColor = Color.FromArgb(241, 196, 15);
            _LabelInfo.Dock = DockStyle.Fill;
            _LabelInfo.Font = new Font("D2Coding", 12F);
            _LabelInfo.ForeColor = Color.Black;
            _LabelInfo.Location = new Point(0, 0);
            _LabelInfo.Name = "_LabelInfo";
            _LabelInfo.Size = new Size(900, 31);
            _LabelInfo.TabIndex = 2;
            _LabelInfo.Text = "Right Click on Position Cell: Teach Current Position";
            _LabelInfo.TextAlign = ContentAlignment.MiddleCenter;
            _LabelInfo.ThemeStyle = UI.Controls.ThemeStyle.Warning_Yellow;
            _LabelInfo.TooltipText = null;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // _Panel2
            // 
            _Panel2.BackColor = SystemColors.Control;
            _Panel2.Controls.Add(_LabelInfo);
            _Panel2.Dock = DockStyle.Bottom;
            _Panel2.ForeColor = SystemColors.ControlText;
            _Panel2.Location = new Point(0, 510);
            _Panel2.Name = "_Panel2";
            _Panel2.Size = new Size(900, 31);
            _Panel2.TabIndex = 3;
            // 
            // _Panel3
            // 
            _Panel3.BackColor = SystemColors.Control;
            _Panel3.Controls.Add(motionMove_View1);
            _Panel3.Dock = DockStyle.Bottom;
            _Panel3.ForeColor = SystemColors.ControlText;
            _Panel3.Location = new Point(0, 304);
            _Panel3.Name = "_Panel3";
            _Panel3.Size = new Size(900, 206);
            _Panel3.TabIndex = 3;
            // 
            // _Panel4
            // 
            _Panel4.BackColor = SystemColors.Control;
            _Panel4.Controls.Add(dataGridView1);
            _Panel4.Dock = DockStyle.Fill;
            _Panel4.ForeColor = SystemColors.ControlText;
            _Panel4.Location = new Point(0, 45);
            _Panel4.Name = "_Panel4";
            _Panel4.Size = new Size(900, 259);
            _Panel4.TabIndex = 3;
            // 
            // motionMove_View1
            // 
            motionMove_View1.Dock = DockStyle.Fill;
            motionMove_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            motionMove_View1.Location = new Point(0, 0);
            motionMove_View1.Margin = new Padding(3, 5, 3, 5);
            motionMove_View1.Name = "motionMove_View1";
            motionMove_View1.Size = new Size(900, 206);
            motionMove_View1.TabIndex = 0;
            // 
            // MotorPosition_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "MotorPosition_View";
            Size = new Size(900, 600);
            Load += MotorPosition_View_Load;
            _PanelMain.ResumeLayout(false);
            _Panel1.ResumeLayout(false);
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            _Panel2.ResumeLayout(false);
            _Panel3.ResumeLayout(false);
            _Panel4.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private EQ.UI.Controls._ComboBox _ComboAxis;
        private EQ.UI.Controls._ComboBox _ComboGroup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private EQ.UI.Controls._DataGridView dataGridView1;
        private EQ.UI.Controls._Label _LabelInfo;
        private Controls._Label _LabelCurrentPos;
        private System.Windows.Forms.Timer timer1;
        private Controls._Panel _Panel4;
        private Controls._Panel _Panel3;
        private Controls._Panel _Panel2;
        private MotionMove_View motionMove_View1;
    }
}