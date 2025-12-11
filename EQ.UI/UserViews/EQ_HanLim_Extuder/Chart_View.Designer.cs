namespace EQ.UI.UserViews.EQ_HanLim_Extuder
{
    partial class Chart_View
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
            _formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            timer1 = new System.Windows.Forms.Timer(components);
            flowLayoutPanel1 = new FlowLayoutPanel();
            _CheckBox1 = new EQ.UI.Controls._CheckBox();
            _CheckBox2 = new EQ.UI.Controls._CheckBox();
            _CheckBox3 = new EQ.UI.Controls._CheckBox();
            _CheckBox4 = new EQ.UI.Controls._CheckBox();
            _PanelMain.SuspendLayout();
            _Panel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // _LabelTitle
            // 
            _LabelTitle.Text = "Chart";
            // 
            // _PanelMain
            // 
            _PanelMain.Controls.Add(_formsPlot1);
            _PanelMain.Controls.Add(flowLayoutPanel1);
            // 
            // _formsPlot1
            // 
            _formsPlot1.DisplayScale = 1F;
            _formsPlot1.Dock = DockStyle.Fill;
            _formsPlot1.Location = new Point(0, 37);
            _formsPlot1.Name = "_formsPlot1";
            _formsPlot1.Size = new Size(363, 276);
            _formsPlot1.TabIndex = 0;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(_CheckBox1);
            flowLayoutPanel1.Controls.Add(_CheckBox2);
            flowLayoutPanel1.Controls.Add(_CheckBox3);
            flowLayoutPanel1.Controls.Add(_CheckBox4);
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(363, 37);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // _CheckBox1
            // 
            _CheckBox1.AutoSize = true;
            _CheckBox1.BackColor = SystemColors.Control;
            _CheckBox1.Checked = true;
            _CheckBox1.CheckState = CheckState.Checked;
            _CheckBox1.Font = new Font("D2Coding", 12F);
            _CheckBox1.ForeColor = SystemColors.ControlText;
            _CheckBox1.Location = new Point(3, 3);
            _CheckBox1.Name = "_CheckBox1";
            _CheckBox1.Size = new Size(67, 22);
            _CheckBox1.TabIndex = 0;
            _CheckBox1.Text = "Zone1";
            _CheckBox1.ThemeStyle = UI.Controls.ThemeStyle.Default;
            _CheckBox1.UseVisualStyleBackColor = false;
            _CheckBox1.CheckedChanged += _CheckBox1_CheckedChanged;
            // 
            // _CheckBox2
            // 
            _CheckBox2.AutoSize = true;
            _CheckBox2.BackColor = SystemColors.Control;
            _CheckBox2.Checked = true;
            _CheckBox2.CheckState = CheckState.Checked;
            _CheckBox2.Font = new Font("D2Coding", 12F);
            _CheckBox2.ForeColor = SystemColors.ControlText;
            _CheckBox2.Location = new Point(76, 3);
            _CheckBox2.Name = "_CheckBox2";
            _CheckBox2.Size = new Size(67, 22);
            _CheckBox2.TabIndex = 0;
            _CheckBox2.Text = "Zone2";
            _CheckBox2.ThemeStyle = UI.Controls.ThemeStyle.Default;
            _CheckBox2.UseVisualStyleBackColor = false;
            _CheckBox2.CheckedChanged += _CheckBox1_CheckedChanged;
            // 
            // _CheckBox3
            // 
            _CheckBox3.AutoSize = true;
            _CheckBox3.BackColor = SystemColors.Control;
            _CheckBox3.Checked = true;
            _CheckBox3.CheckState = CheckState.Checked;
            _CheckBox3.Font = new Font("D2Coding", 12F);
            _CheckBox3.ForeColor = SystemColors.ControlText;
            _CheckBox3.Location = new Point(149, 3);
            _CheckBox3.Name = "_CheckBox3";
            _CheckBox3.Size = new Size(51, 22);
            _CheckBox3.TabIndex = 0;
            _CheckBox3.Text = "RPM";
            _CheckBox3.ThemeStyle = UI.Controls.ThemeStyle.Default;
            _CheckBox3.UseVisualStyleBackColor = false;
            _CheckBox3.CheckedChanged += _CheckBox1_CheckedChanged;
            // 
            // _CheckBox4
            // 
            _CheckBox4.AutoSize = true;
            _CheckBox4.BackColor = SystemColors.Control;
            _CheckBox4.Checked = true;
            _CheckBox4.CheckState = CheckState.Checked;
            _CheckBox4.Font = new Font("D2Coding", 12F);
            _CheckBox4.ForeColor = SystemColors.ControlText;
            _CheckBox4.Location = new Point(206, 3);
            _CheckBox4.Name = "_CheckBox4";
            _CheckBox4.Size = new Size(75, 22);
            _CheckBox4.TabIndex = 0;
            _CheckBox4.Text = "TORQUE";
            _CheckBox4.ThemeStyle = UI.Controls.ThemeStyle.Default;
            _CheckBox4.UseVisualStyleBackColor = false;
            _CheckBox4.CheckedChanged += _CheckBox1_CheckedChanged;
            // 
            // Chart_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "Chart_View";
            Load += Chart_View_Load;
            _PanelMain.ResumeLayout(false);
            _Panel1.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot _formsPlot1;
        private System.Windows.Forms.Timer timer1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Controls._CheckBox _CheckBox1;
        private Controls._CheckBox _CheckBox2;
        private Controls._CheckBox _CheckBox3;
        private Controls._CheckBox _CheckBox4;
    }
}
