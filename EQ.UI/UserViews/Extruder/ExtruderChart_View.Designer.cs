namespace EQ.UI.UserViews.Extruder
{
    partial class ExtruderChart_View
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
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            splitContainer1 = new SplitContainer();
            flowLayoutPanel1 = new FlowLayoutPanel();
            chkSyncXAxis = new CheckBox();
            flowLayoutPanel2 = new FlowLayoutPanel();
            _CheckBox1 = new EQ.UI.Controls._CheckBox();
            _CheckBox2 = new EQ.UI.Controls._CheckBox();
            _CheckBox3 = new EQ.UI.Controls._CheckBox();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // formsPlot1
            // 
            formsPlot1.DisplayScale = 1F;
            formsPlot1.Dock = DockStyle.Fill;
            formsPlot1.Location = new Point(0, 0);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new Size(775, 549);
            formsPlot1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(115, 0);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(formsPlot1);
            splitContainer1.Size = new Size(775, 618);
            splitContainer1.SplitterDistance = 65;
            splitContainer1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.Bisque;
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(775, 65);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // chkSyncXAxis
            // 
            chkSyncXAxis.Appearance = Appearance.Button;
            chkSyncXAxis.Location = new Point(3, 3);
            chkSyncXAxis.Name = "chkSyncXAxis";
            chkSyncXAxis.Size = new Size(100, 42);
            chkSyncXAxis.TabIndex = 0;
            chkSyncXAxis.Text = "Sync X Axis";
            chkSyncXAxis.UseVisualStyleBackColor = true;
            chkSyncXAxis.CheckedChanged += ChkSyncXAxis_CheckedChanged;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.BackColor = Color.Bisque;
            flowLayoutPanel2.Controls.Add(chkSyncXAxis);
            flowLayoutPanel2.Controls.Add(_CheckBox1);
            flowLayoutPanel2.Controls.Add(_CheckBox2);
            flowLayoutPanel2.Controls.Add(_CheckBox3);
            flowLayoutPanel2.Dock = DockStyle.Left;
            flowLayoutPanel2.Location = new Point(0, 0);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(115, 618);
            flowLayoutPanel2.TabIndex = 2;
            // 
            // _CheckBox1
            // 
            _CheckBox1.BackColor = Color.FromArgb(52, 152, 219);
            _CheckBox1.Checked = true;
            _CheckBox1.CheckState = CheckState.Checked;
            _CheckBox1.Font = new Font("D2Coding", 15.7499981F);
            _CheckBox1.ForeColor = Color.Black;
            _CheckBox1.Location = new Point(3, 51);
            _CheckBox1.Name = "_CheckBox1";
            _CheckBox1.Size = new Size(100, 55);
            _CheckBox1.TabIndex = 1;
            _CheckBox1.Text = "Motor";
            _CheckBox1.UseVisualStyleBackColor = false;
            _CheckBox1.CheckStateChanged += _CheckBox1_CheckStateChanged;
            // 
            // _CheckBox2
            // 
            _CheckBox2.BackColor = Color.FromArgb(52, 152, 219);
            _CheckBox2.Checked = true;
            _CheckBox2.CheckState = CheckState.Checked;
            _CheckBox2.Font = new Font("D2Coding", 15.7499981F);
            _CheckBox2.ForeColor = Color.Black;
            _CheckBox2.Location = new Point(3, 112);
            _CheckBox2.Name = "_CheckBox2";
            _CheckBox2.Size = new Size(100, 55);
            _CheckBox2.TabIndex = 1;
            _CheckBox2.Text = "Temp";
            _CheckBox2.UseVisualStyleBackColor = false;
            _CheckBox2.CheckStateChanged += _CheckBox1_CheckStateChanged;
            // 
            // _CheckBox3
            // 
            _CheckBox3.BackColor = Color.FromArgb(52, 152, 219);
            _CheckBox3.Checked = true;
            _CheckBox3.CheckState = CheckState.Checked;
            _CheckBox3.Font = new Font("D2Coding", 15.7499981F);
            _CheckBox3.ForeColor = Color.Black;
            _CheckBox3.Location = new Point(3, 173);
            _CheckBox3.Name = "_CheckBox3";
            _CheckBox3.Size = new Size(100, 55);
            _CheckBox3.TabIndex = 1;
            _CheckBox3.Text = "Data";
            _CheckBox3.UseVisualStyleBackColor = false;
            _CheckBox3.CheckStateChanged += _CheckBox1_CheckStateChanged;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // ExtruderChart_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(splitContainer1);
            Controls.Add(flowLayoutPanel2);
            Name = "ExtruderChart_View";
            Size = new Size(890, 618);
            Load += ExtruderChart_View_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private SplitContainer splitContainer1;
        private FlowLayoutPanel flowLayoutPanel1;
        private CheckBox chkSyncXAxis;
        private FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Timer timer1;
        private Controls._CheckBox _CheckBox1;
        private Controls._CheckBox _CheckBox2;
        private Controls._CheckBox _CheckBox3;
    }
}
