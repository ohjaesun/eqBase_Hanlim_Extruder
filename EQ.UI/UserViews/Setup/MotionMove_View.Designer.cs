

namespace EQ.UI.UserViews
{
    partial class MotionMove_View
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
            _LabelTitle = new EQ.UI.Controls._Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            _Label2 = new EQ.UI.Controls._Label();
            _Label3 = new EQ.UI.Controls._Label();
            _Label4 = new EQ.UI.Controls._Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            _RadioButton1 = new EQ.UI.Controls._RadioButton();
            _RadioButton2 = new EQ.UI.Controls._RadioButton();
            flowLayoutPanel2 = new FlowLayoutPanel();
            _RadioButton3 = new EQ.UI.Controls._RadioButton();
            _RadioButton4 = new EQ.UI.Controls._RadioButton();
            _RadioButton5 = new EQ.UI.Controls._RadioButton();
            _RadioButton6 = new EQ.UI.Controls._RadioButton();
            _RadioButton7 = new EQ.UI.Controls._RadioButton();
            _RadioButton8 = new EQ.UI.Controls._RadioButton();
            _TextBox4 = new EQ.UI.Controls._TextBox();
            flowLayoutPanel3 = new FlowLayoutPanel();
            _RadioButton9 = new EQ.UI.Controls._RadioButton();
            _TextBox1 = new EQ.UI.Controls._TextBox();
            _RadioButton10 = new EQ.UI.Controls._RadioButton();
            _TextBox2 = new EQ.UI.Controls._TextBox();
            _RadioButton11 = new EQ.UI.Controls._RadioButton();
            _TextBox3 = new EQ.UI.Controls._TextBox();
            _Button1 = new EQ.UI.Controls._Button();
            _Button2 = new EQ.UI.Controls._Button();
            _Button3 = new EQ.UI.Controls._Button();
            _Button4 = new EQ.UI.Controls._Button();
            _Button5 = new EQ.UI.Controls._Button();
            _Button6 = new EQ.UI.Controls._Button();
            flowLayoutPanel4 = new FlowLayoutPanel();
            _LabelInfo1 = new EQ.UI.Controls._Label();
            _LabelInfo2 = new EQ.UI.Controls._Label();
            _LabelInfo3 = new EQ.UI.Controls._Label();
            _LabelInfo4 = new EQ.UI.Controls._Label();
            timer1 = new System.Windows.Forms.Timer(components);
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // _LabelTitle
            // 
            _LabelTitle.BackColor = Color.FromArgb(46, 204, 113);
            _LabelTitle.Dock = DockStyle.Top;
            _LabelTitle.Font = new Font("D2Coding", 12F);
            _LabelTitle.ForeColor = Color.Black;
            _LabelTitle.Location = new Point(0, 0);
            _LabelTitle.Name = "_LabelTitle";
            _LabelTitle.Size = new Size(840, 31);
            _LabelTitle.TabIndex = 0;
            _LabelTitle.Text = "_Label1";
            _LabelTitle.TextAlign = ContentAlignment.MiddleCenter;
            _LabelTitle.ThemeStyle = UI.Controls.ThemeStyle.Success_Green;
            _LabelTitle.TooltipText = null;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 102F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 93F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 91F));
            tableLayoutPanel1.Controls.Add(_Label2, 0, 0);
            tableLayoutPanel1.Controls.Add(_Label3, 0, 1);
            tableLayoutPanel1.Controls.Add(_Label4, 0, 2);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 1, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel2, 1, 1);
            tableLayoutPanel1.Controls.Add(flowLayoutPanel3, 1, 2);
            tableLayoutPanel1.Controls.Add(_Button1, 2, 0);
            tableLayoutPanel1.Controls.Add(_Button2, 3, 0);
            tableLayoutPanel1.Controls.Add(_Button3, 2, 1);
            tableLayoutPanel1.Controls.Add(_Button4, 3, 1);
            tableLayoutPanel1.Controls.Add(_Button5, 2, 2);
            tableLayoutPanel1.Controls.Add(_Button6, 3, 2);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 31);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            tableLayoutPanel1.Size = new Size(840, 141);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // _Label2
            // 
            _Label2.BackColor = SystemColors.Control;
            _Label2.Dock = DockStyle.Fill;
            _Label2.Font = new Font("D2Coding", 12F);
            _Label2.ForeColor = SystemColors.ControlText;
            _Label2.Location = new Point(4, 1);
            _Label2.Name = "_Label2";
            _Label2.Size = new Size(96, 45);
            _Label2.TabIndex = 0;
            _Label2.Text = "Jog";
            _Label2.TextAlign = ContentAlignment.MiddleCenter;
            _Label2.TooltipText = null;
            // 
            // _Label3
            // 
            _Label3.BackColor = SystemColors.Control;
            _Label3.Dock = DockStyle.Fill;
            _Label3.Font = new Font("D2Coding", 12F);
            _Label3.ForeColor = SystemColors.ControlText;
            _Label3.Location = new Point(4, 47);
            _Label3.Name = "_Label3";
            _Label3.Size = new Size(96, 45);
            _Label3.TabIndex = 0;
            _Label3.Text = "Rel Move";
            _Label3.TextAlign = ContentAlignment.MiddleCenter;
            _Label3.TooltipText = null;
            // 
            // _Label4
            // 
            _Label4.BackColor = SystemColors.Control;
            _Label4.Dock = DockStyle.Fill;
            _Label4.Font = new Font("D2Coding", 12F);
            _Label4.ForeColor = SystemColors.ControlText;
            _Label4.Location = new Point(4, 93);
            _Label4.Name = "_Label4";
            _Label4.Size = new Size(96, 47);
            _Label4.TabIndex = 0;
            _Label4.Text = "Mode";
            _Label4.TextAlign = ContentAlignment.MiddleCenter;
            _Label4.TooltipText = null;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(_RadioButton1);
            flowLayoutPanel1.Controls.Add(_RadioButton2);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(107, 4);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(543, 39);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // _RadioButton1
            // 
            _RadioButton1.AutoSize = true;
            _RadioButton1.BackColor = Color.FromArgb(52, 152, 219);
            _RadioButton1.Checked = true;
            _RadioButton1.Font = new Font("D2Coding", 14.2499981F);
            _RadioButton1.ForeColor = Color.Black;
            _RadioButton1.Location = new Point(3, 3);
            _RadioButton1.Name = "_RadioButton1";
            _RadioButton1.Size = new Size(64, 26);
            _RadioButton1.TabIndex = 0;
            _RadioButton1.TabStop = true;
            _RadioButton1.Text = "Slow";
            _RadioButton1.UseVisualStyleBackColor = false;
            // 
            // _RadioButton2
            // 
            _RadioButton2.AutoSize = true;
            _RadioButton2.BackColor = Color.FromArgb(52, 152, 219);
            _RadioButton2.Font = new Font("D2Coding", 14.2499981F);
            _RadioButton2.ForeColor = Color.Black;
            _RadioButton2.Location = new Point(73, 3);
            _RadioButton2.Name = "_RadioButton2";
            _RadioButton2.Size = new Size(64, 26);
            _RadioButton2.TabIndex = 0;
            _RadioButton2.Text = "Fast";
            _RadioButton2.UseVisualStyleBackColor = false;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(_RadioButton3);
            flowLayoutPanel2.Controls.Add(_RadioButton4);
            flowLayoutPanel2.Controls.Add(_RadioButton5);
            flowLayoutPanel2.Controls.Add(_RadioButton6);
            flowLayoutPanel2.Controls.Add(_RadioButton7);
            flowLayoutPanel2.Controls.Add(_RadioButton8);
            flowLayoutPanel2.Controls.Add(_TextBox4);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.Location = new Point(107, 50);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(543, 39);
            flowLayoutPanel2.TabIndex = 1;
            // 
            // _RadioButton3
            // 
            _RadioButton3.AutoSize = true;
            _RadioButton3.BackColor = Color.FromArgb(52, 152, 219);
            _RadioButton3.Checked = true;
            _RadioButton3.Font = new Font("D2Coding", 14.2499981F);
            _RadioButton3.ForeColor = Color.Black;
            _RadioButton3.Location = new Point(3, 3);
            _RadioButton3.Name = "_RadioButton3";
            _RadioButton3.Size = new Size(73, 26);
            _RadioButton3.TabIndex = 0;
            _RadioButton3.TabStop = true;
            _RadioButton3.Text = "0.1mm";
            _RadioButton3.UseVisualStyleBackColor = false;
            // 
            // _RadioButton4
            // 
            _RadioButton4.AutoSize = true;
            _RadioButton4.BackColor = Color.FromArgb(52, 152, 219);
            _RadioButton4.Font = new Font("D2Coding", 14.2499981F);
            _RadioButton4.ForeColor = Color.Black;
            _RadioButton4.Location = new Point(82, 3);
            _RadioButton4.Name = "_RadioButton4";
            _RadioButton4.Size = new Size(73, 26);
            _RadioButton4.TabIndex = 0;
            _RadioButton4.Text = "0.5mm";
            _RadioButton4.UseVisualStyleBackColor = false;
            // 
            // _RadioButton5
            // 
            _RadioButton5.AutoSize = true;
            _RadioButton5.BackColor = Color.FromArgb(52, 152, 219);
            _RadioButton5.Font = new Font("D2Coding", 14.2499981F);
            _RadioButton5.ForeColor = Color.Black;
            _RadioButton5.Location = new Point(161, 3);
            _RadioButton5.Name = "_RadioButton5";
            _RadioButton5.Size = new Size(55, 26);
            _RadioButton5.TabIndex = 0;
            _RadioButton5.Text = "1mm";
            _RadioButton5.UseVisualStyleBackColor = false;
            // 
            // _RadioButton6
            // 
            _RadioButton6.AutoSize = true;
            _RadioButton6.BackColor = Color.FromArgb(52, 152, 219);
            _RadioButton6.Font = new Font("D2Coding", 14.2499981F);
            _RadioButton6.ForeColor = Color.Black;
            _RadioButton6.Location = new Point(222, 3);
            _RadioButton6.Name = "_RadioButton6";
            _RadioButton6.Size = new Size(55, 26);
            _RadioButton6.TabIndex = 0;
            _RadioButton6.Text = "5mm";
            _RadioButton6.UseVisualStyleBackColor = false;
            // 
            // _RadioButton7
            // 
            _RadioButton7.AutoSize = true;
            _RadioButton7.BackColor = Color.FromArgb(52, 152, 219);
            _RadioButton7.Font = new Font("D2Coding", 14.2499981F);
            _RadioButton7.ForeColor = Color.Black;
            _RadioButton7.Location = new Point(283, 3);
            _RadioButton7.Name = "_RadioButton7";
            _RadioButton7.Size = new Size(64, 26);
            _RadioButton7.TabIndex = 0;
            _RadioButton7.Text = "10mm";
            _RadioButton7.UseVisualStyleBackColor = false;
            // 
            // _RadioButton8
            // 
            _RadioButton8.AutoSize = true;
            _RadioButton8.BackColor = Color.FromArgb(52, 152, 219);
            _RadioButton8.Font = new Font("D2Coding", 14.2499981F);
            _RadioButton8.ForeColor = Color.Black;
            _RadioButton8.Location = new Point(353, 3);
            _RadioButton8.Name = "_RadioButton8";
            _RadioButton8.Size = new Size(46, 26);
            _RadioButton8.TabIndex = 0;
            _RadioButton8.Text = "um";
            _RadioButton8.UseVisualStyleBackColor = false;
            // 
            // _TextBox4
            // 
            _TextBox4.BackColor = Color.FromArgb(149, 165, 166);
            _TextBox4.Font = new Font("D2Coding", 12F);
            _TextBox4.ForeColor = Color.White;
            _TextBox4.Location = new Point(405, 3);
            _TextBox4.Name = "_TextBox4";
            _TextBox4.Size = new Size(100, 26);
            _TextBox4.TabIndex = 1;
            _TextBox4.Text = "0";
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.Controls.Add(_RadioButton9);
            flowLayoutPanel3.Controls.Add(_TextBox1);
            flowLayoutPanel3.Controls.Add(_RadioButton10);
            flowLayoutPanel3.Controls.Add(_TextBox2);
            flowLayoutPanel3.Controls.Add(_RadioButton11);
            flowLayoutPanel3.Controls.Add(_TextBox3);
            flowLayoutPanel3.Dock = DockStyle.Fill;
            flowLayoutPanel3.Location = new Point(107, 96);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(543, 41);
            flowLayoutPanel3.TabIndex = 1;
            // 
            // _RadioButton9
            // 
            _RadioButton9.AutoSize = true;
            _RadioButton9.BackColor = Color.FromArgb(52, 152, 219);
            _RadioButton9.Checked = true;
            _RadioButton9.Font = new Font("D2Coding", 14.2499981F);
            _RadioButton9.ForeColor = Color.Black;
            _RadioButton9.Location = new Point(3, 3);
            _RadioButton9.Name = "_RadioButton9";
            _RadioButton9.Size = new Size(55, 26);
            _RadioButton9.TabIndex = 0;
            _RadioButton9.TabStop = true;
            _RadioButton9.Text = "ABS";
            _RadioButton9.UseVisualStyleBackColor = false;
            // 
            // _TextBox1
            // 
            _TextBox1.BackColor = Color.FromArgb(149, 165, 166);
            _TextBox1.Font = new Font("D2Coding", 12F);
            _TextBox1.ForeColor = Color.White;
            _TextBox1.Location = new Point(64, 3);
            _TextBox1.Name = "_TextBox1";
            _TextBox1.Size = new Size(100, 26);
            _TextBox1.TabIndex = 1;
            _TextBox1.Text = "0";
            // 
            // _RadioButton10
            // 
            _RadioButton10.AutoSize = true;
            _RadioButton10.BackColor = Color.FromArgb(52, 152, 219);
            _RadioButton10.Font = new Font("D2Coding", 14.2499981F);
            _RadioButton10.ForeColor = Color.Black;
            _RadioButton10.Location = new Point(170, 3);
            _RadioButton10.Name = "_RadioButton10";
            _RadioButton10.Size = new Size(82, 26);
            _RadioButton10.TabIndex = 0;
            _RadioButton10.Text = "Torque";
            _RadioButton10.UseVisualStyleBackColor = false;
            // 
            // _TextBox2
            // 
            _TextBox2.BackColor = Color.FromArgb(149, 165, 166);
            _TextBox2.Font = new Font("D2Coding", 12F);
            _TextBox2.ForeColor = Color.White;
            _TextBox2.Location = new Point(258, 3);
            _TextBox2.Name = "_TextBox2";
            _TextBox2.Size = new Size(100, 26);
            _TextBox2.TabIndex = 1;
            _TextBox2.Text = "0";
            // 
            // _RadioButton11
            // 
            _RadioButton11.AutoSize = true;
            _RadioButton11.BackColor = Color.FromArgb(52, 152, 219);
            _RadioButton11.Font = new Font("D2Coding", 14.2499981F);
            _RadioButton11.ForeColor = Color.Black;
            _RadioButton11.Location = new Point(364, 3);
            _RadioButton11.Name = "_RadioButton11";
            _RadioButton11.Size = new Size(55, 26);
            _RadioButton11.TabIndex = 0;
            _RadioButton11.Text = "Vel";
            _RadioButton11.UseVisualStyleBackColor = false;
            // 
            // _TextBox3
            // 
            _TextBox3.BackColor = Color.FromArgb(149, 165, 166);
            _TextBox3.Font = new Font("D2Coding", 12F);
            _TextBox3.ForeColor = Color.White;
            _TextBox3.Location = new Point(425, 3);
            _TextBox3.Name = "_TextBox3";
            _TextBox3.Size = new Size(100, 26);
            _TextBox3.TabIndex = 1;
            _TextBox3.Text = "0";
            // 
            // _Button1
            // 
            _Button1.BackColor = Color.FromArgb(52, 152, 219);
            _Button1.Dock = DockStyle.Fill;
            _Button1.Font = new Font("D2Coding", 12F);
            _Button1.ForeColor = Color.Black;
            _Button1.Location = new Point(657, 4);
            _Button1.Name = "_Button1";
            _Button1.Size = new Size(87, 39);
            _Button1.TabIndex = 2;
            _Button1.Text = "-";
            _Button1.ThemeStyle = UI.Controls.ThemeStyle.Info_Sky;
            _Button1.TooltipText = null;
            _Button1.UseVisualStyleBackColor = false;
            _Button1.MouseDown += _Button1_MouseDown;
            _Button1.MouseUp += _Button1_MouseUp;
            // 
            // _Button2
            // 
            _Button2.BackColor = Color.FromArgb(52, 152, 219);
            _Button2.Dock = DockStyle.Fill;
            _Button2.Font = new Font("D2Coding", 12F);
            _Button2.ForeColor = Color.Black;
            _Button2.Location = new Point(751, 4);
            _Button2.Name = "_Button2";
            _Button2.Size = new Size(85, 39);
            _Button2.TabIndex = 2;
            _Button2.Text = "+";
            _Button2.ThemeStyle = UI.Controls.ThemeStyle.Info_Sky;
            _Button2.TooltipText = null;
            _Button2.UseVisualStyleBackColor = false;
            _Button2.MouseDown += _Button1_MouseDown;
            _Button2.MouseUp += _Button1_MouseUp;
            // 
            // _Button3
            // 
            _Button3.BackColor = Color.FromArgb(52, 152, 219);
            _Button3.Dock = DockStyle.Fill;
            _Button3.Font = new Font("D2Coding", 12F);
            _Button3.ForeColor = Color.Black;
            _Button3.Location = new Point(657, 50);
            _Button3.Name = "_Button3";
            _Button3.Size = new Size(87, 39);
            _Button3.TabIndex = 2;
            _Button3.Text = "-";
            _Button3.ThemeStyle = UI.Controls.ThemeStyle.Info_Sky;
            _Button3.TooltipText = null;
            _Button3.UseVisualStyleBackColor = false;
            _Button3.Click += btnClick;
            // 
            // _Button4
            // 
            _Button4.BackColor = Color.FromArgb(52, 152, 219);
            _Button4.Dock = DockStyle.Fill;
            _Button4.Font = new Font("D2Coding", 12F);
            _Button4.ForeColor = Color.Black;
            _Button4.Location = new Point(751, 50);
            _Button4.Name = "_Button4";
            _Button4.Size = new Size(85, 39);
            _Button4.TabIndex = 2;
            _Button4.Text = "+";
            _Button4.ThemeStyle = UI.Controls.ThemeStyle.Info_Sky;
            _Button4.TooltipText = null;
            _Button4.UseVisualStyleBackColor = false;
            _Button4.Click += btnClick;
            // 
            // _Button5
            // 
            _Button5.BackColor = Color.FromArgb(48, 63, 159);
            _Button5.Dock = DockStyle.Fill;
            _Button5.Font = new Font("D2Coding", 12F);
            _Button5.ForeColor = Color.White;
            _Button5.Location = new Point(657, 96);
            _Button5.Name = "_Button5";
            _Button5.Size = new Size(87, 41);
            _Button5.TabIndex = 2;
            _Button5.Text = "Go";
            _Button5.TooltipText = null;
            _Button5.UseVisualStyleBackColor = false;
            _Button5.Click += btnClick;
            // 
            // _Button6
            // 
            _Button6.BackColor = Color.FromArgb(231, 76, 60);
            _Button6.Dock = DockStyle.Fill;
            _Button6.Font = new Font("D2Coding", 12F);
            _Button6.ForeColor = Color.Black;
            _Button6.Location = new Point(751, 96);
            _Button6.Name = "_Button6";
            _Button6.Size = new Size(85, 41);
            _Button6.TabIndex = 2;
            _Button6.Text = "Stop";
            _Button6.ThemeStyle = UI.Controls.ThemeStyle.Danger_Red;
            _Button6.TooltipText = null;
            _Button6.UseVisualStyleBackColor = false;
            _Button6.Click += btnClick;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.Controls.Add(_LabelInfo1);
            flowLayoutPanel4.Controls.Add(_LabelInfo2);
            flowLayoutPanel4.Controls.Add(_LabelInfo3);
            flowLayoutPanel4.Controls.Add(_LabelInfo4);
            flowLayoutPanel4.Dock = DockStyle.Fill;
            flowLayoutPanel4.Location = new Point(0, 172);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(840, 105);
            flowLayoutPanel4.TabIndex = 2;
            // 
            // _LabelInfo1
            // 
            _LabelInfo1.BackColor = Color.FromArgb(149, 165, 166);
            _LabelInfo1.Font = new Font("D2Coding", 12F);
            _LabelInfo1.ForeColor = Color.White;
            _LabelInfo1.Location = new Point(3, 0);
            _LabelInfo1.Name = "_LabelInfo1";
            _LabelInfo1.Size = new Size(71, 24);
            _LabelInfo1.TabIndex = 0;
            _LabelInfo1.Text = "Servo";
            _LabelInfo1.TextAlign = ContentAlignment.MiddleCenter;
            _LabelInfo1.TooltipText = null;
            // 
            // _LabelInfo2
            // 
            _LabelInfo2.BackColor = Color.FromArgb(149, 165, 166);
            _LabelInfo2.Font = new Font("D2Coding", 12F);
            _LabelInfo2.ForeColor = Color.White;
            _LabelInfo2.Location = new Point(80, 0);
            _LabelInfo2.Name = "_LabelInfo2";
            _LabelInfo2.Size = new Size(71, 24);
            _LabelInfo2.TabIndex = 1;
            _LabelInfo2.Text = "Alarm";
            _LabelInfo2.TextAlign = ContentAlignment.MiddleCenter;
            _LabelInfo2.TooltipText = null;
            // 
            // _LabelInfo3
            // 
            _LabelInfo3.BackColor = Color.FromArgb(149, 165, 166);
            _LabelInfo3.Font = new Font("D2Coding", 12F);
            _LabelInfo3.ForeColor = Color.White;
            _LabelInfo3.Location = new Point(157, 0);
            _LabelInfo3.Name = "_LabelInfo3";
            _LabelInfo3.Size = new Size(71, 24);
            _LabelInfo3.TabIndex = 1;
            _LabelInfo3.Text = "Inpos";
            _LabelInfo3.TextAlign = ContentAlignment.MiddleCenter;
            _LabelInfo3.TooltipText = null;
            // 
            // _LabelInfo4
            // 
            _LabelInfo4.BackColor = Color.FromArgb(149, 165, 166);
            _LabelInfo4.Font = new Font("D2Coding", 12F);
            _LabelInfo4.ForeColor = Color.White;
            _LabelInfo4.Location = new Point(234, 0);
            _LabelInfo4.Name = "_LabelInfo4";
            _LabelInfo4.Size = new Size(71, 24);
            _LabelInfo4.TabIndex = 1;
            _LabelInfo4.Text = "Pos";
            _LabelInfo4.TextAlign = ContentAlignment.MiddleCenter;
            _LabelInfo4.TooltipText = null;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // MotionMove_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(flowLayoutPanel4);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(_LabelTitle);
            Margin = new Padding(3, 5, 3, 5);
            Name = "MotionMove_View";
            Size = new Size(840, 277);
            tableLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            flowLayoutPanel2.ResumeLayout(false);
            flowLayoutPanel2.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel3.PerformLayout();
            flowLayoutPanel4.ResumeLayout(false);
            ResumeLayout(false);
        }





        #endregion

        private Controls._Label _LabelTitle;
        private TableLayoutPanel tableLayoutPanel1;
        private Controls._Label _Label2;
        private Controls._Label _Label3;
        private Controls._Label _Label4;
        private FlowLayoutPanel flowLayoutPanel1;
        private Controls._RadioButton _RadioButton1;
        private Controls._RadioButton _RadioButton2;
        private FlowLayoutPanel flowLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel3;
        private Controls._Button _Button1;
        private Controls._Button _Button2;
        private Controls._Button _Button3;
        private Controls._Button _Button4;
        private Controls._Button _Button5;
        private Controls._Button _Button6;
        private Controls._RadioButton _RadioButton3;
        private Controls._RadioButton _RadioButton4;
        private Controls._RadioButton _RadioButton5;
        private Controls._RadioButton _RadioButton6;
        private Controls._RadioButton _RadioButton7;
        private Controls._RadioButton _RadioButton8;
        private Controls._TextBox _TextBox1;
        private Controls._RadioButton _RadioButton9;
        private Controls._RadioButton _RadioButton10;
        private Controls._RadioButton _RadioButton11;
        private Controls._TextBox _TextBox4;
        private Controls._TextBox _TextBox2;
        private Controls._TextBox _TextBox3;
        private FlowLayoutPanel flowLayoutPanel4;
        private Controls._Label _LabelInfo1;
        private Controls._Label _LabelInfo2;
        private Controls._Label _LabelInfo3;
        private Controls._Label _LabelInfo4;
        private System.Windows.Forms.Timer timer1;
    }
}
