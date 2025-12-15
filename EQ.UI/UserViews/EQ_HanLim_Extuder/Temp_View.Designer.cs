namespace EQ.UI.UserViews.EQ_HanLim_Extuder
{
    partial class Temp_View
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
            _Label1 = new EQ.UI.Controls._Label();
            _Label2 = new EQ.UI.Controls._Label();
            _LabelTemp1 = new EQ.UI.Controls._Label();
            _LabelTemp2 = new EQ.UI.Controls._Label();
            _LabeRun1 = new EQ.UI.Controls._Label();
            _LabeRun2 = new EQ.UI.Controls._Label();
            _GroupBox1 = new EQ.UI.Controls._GroupBox();
            timer1 = new System.Windows.Forms.Timer(components);
            _GroupBox2 = new EQ.UI.Controls._GroupBox();
            _Label4 = new EQ.UI.Controls._Label();
            _Label3 = new EQ.UI.Controls._Label();
            _Label5 = new EQ.UI.Controls._Label();
            _Label6 = new EQ.UI.Controls._Label();
            _GroupBox1.SuspendLayout();
            _GroupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // _Label1
            // 
            _Label1.AutoSize = true;
            _Label1.BackColor = Color.Black;
            _Label1.Font = new Font("D2Coding", 12F);
            _Label1.ForeColor = Color.White;
            _Label1.Location = new Point(6, 22);
            _Label1.Name = "_Label1";
            _Label1.Size = new Size(56, 18);
            _Label1.TabIndex = 0;
            _Label1.Text = "ZONE 1";
            _Label1.ThemeStyle = UI.Controls.ThemeStyle.Black_White;
            _Label1.TooltipText = null;
            // 
            // _Label2
            // 
            _Label2.AutoSize = true;
            _Label2.BackColor = Color.Black;
            _Label2.Font = new Font("D2Coding", 12F);
            _Label2.ForeColor = Color.White;
            _Label2.Location = new Point(6, 51);
            _Label2.Name = "_Label2";
            _Label2.Size = new Size(56, 18);
            _Label2.TabIndex = 0;
            _Label2.Text = "ZONE 2";
            _Label2.ThemeStyle = UI.Controls.ThemeStyle.Black_White;
            _Label2.TooltipText = null;
            // 
            // _LabelTemp1
            // 
            _LabelTemp1.BackColor = Color.FromArgb(46, 204, 113);
            _LabelTemp1.Font = new Font("D2Coding", 12F);
            _LabelTemp1.ForeColor = Color.Black;
            _LabelTemp1.Location = new Point(68, 22);
            _LabelTemp1.Name = "_LabelTemp1";
            _LabelTemp1.Size = new Size(73, 18);
            _LabelTemp1.TabIndex = 0;
            _LabelTemp1.Text = "100 ℃";
            _LabelTemp1.TextAlign = ContentAlignment.MiddleRight;
            _LabelTemp1.ThemeStyle = UI.Controls.ThemeStyle.Success_Green;
            _LabelTemp1.TooltipText = null;
            // 
            // _LabelTemp2
            // 
            _LabelTemp2.BackColor = Color.FromArgb(46, 204, 113);
            _LabelTemp2.Font = new Font("D2Coding", 12F);
            _LabelTemp2.ForeColor = Color.Black;
            _LabelTemp2.Location = new Point(68, 51);
            _LabelTemp2.Name = "_LabelTemp2";
            _LabelTemp2.Size = new Size(73, 18);
            _LabelTemp2.TabIndex = 0;
            _LabelTemp2.Text = "200 ℃";
            _LabelTemp2.TextAlign = ContentAlignment.MiddleRight;
            _LabelTemp2.ThemeStyle = UI.Controls.ThemeStyle.Success_Green;
            _LabelTemp2.TooltipText = null;
            // 
            // _LabeRun1
            // 
            _LabeRun1.BackColor = Color.FromArgb(149, 165, 166);
            _LabeRun1.Font = new Font("D2Coding", 12F);
            _LabeRun1.ForeColor = Color.White;
            _LabeRun1.Location = new Point(157, 22);
            _LabeRun1.Name = "_LabeRun1";
            _LabeRun1.Size = new Size(56, 18);
            _LabeRun1.TabIndex = 0;
            _LabeRun1.Text = "STOP";
            _LabeRun1.TextAlign = ContentAlignment.MiddleCenter;
            _LabeRun1.TooltipText = null;
            // 
            // _LabeRun2
            // 
            _LabeRun2.BackColor = Color.FromArgb(149, 165, 166);
            _LabeRun2.Font = new Font("D2Coding", 12F);
            _LabeRun2.ForeColor = Color.White;
            _LabeRun2.Location = new Point(157, 51);
            _LabeRun2.Name = "_LabeRun2";
            _LabeRun2.Size = new Size(56, 18);
            _LabeRun2.TabIndex = 0;
            _LabeRun2.Text = "STOP";
            _LabeRun2.TextAlign = ContentAlignment.MiddleCenter;
            _LabeRun2.TooltipText = null;
            // 
            // _GroupBox1
            // 
            _GroupBox1.BackColor = SystemColors.Control;
            _GroupBox1.Controls.Add(_Label1);
            _GroupBox1.Controls.Add(_LabeRun2);
            _GroupBox1.Controls.Add(_LabelTemp1);
            _GroupBox1.Controls.Add(_LabeRun1);
            _GroupBox1.Controls.Add(_LabelTemp2);
            _GroupBox1.Controls.Add(_Label2);
            _GroupBox1.Dock = DockStyle.Top;
            _GroupBox1.Font = new Font("D2Coding", 12F);
            _GroupBox1.ForeColor = SystemColors.ControlText;
            _GroupBox1.Location = new Point(0, 0);
            _GroupBox1.Name = "_GroupBox1";
            _GroupBox1.Size = new Size(219, 83);
            _GroupBox1.TabIndex = 1;
            _GroupBox1.TabStop = false;
            _GroupBox1.Text = "Temperature";
            _GroupBox1.ThemeStyle = UI.Controls.ThemeStyle.Default;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // _GroupBox2
            // 
            _GroupBox2.BackColor = SystemColors.Control;
            _GroupBox2.Controls.Add(_Label4);
            _GroupBox2.Controls.Add(_Label3);
            _GroupBox2.Controls.Add(_Label6);
            _GroupBox2.Controls.Add(_Label5);
            _GroupBox2.Dock = DockStyle.Top;
            _GroupBox2.Font = new Font("D2Coding", 12F);
            _GroupBox2.ForeColor = SystemColors.ControlText;
            _GroupBox2.Location = new Point(0, 83);
            _GroupBox2.Name = "_GroupBox2";
            _GroupBox2.Size = new Size(219, 100);
            _GroupBox2.TabIndex = 2;
            _GroupBox2.TabStop = false;
            _GroupBox2.Text = "Motor";
            _GroupBox2.ThemeStyle = UI.Controls.ThemeStyle.Default;
            // 
            // _Label4
            // 
            _Label4.BackColor = Color.Black;
            _Label4.Font = new Font("D2Coding", 12F);
            _Label4.ForeColor = Color.White;
            _Label4.Location = new Point(6, 49);
            _Label4.Name = "_Label4";
            _Label4.Size = new Size(56, 18);
            _Label4.TabIndex = 0;
            _Label4.Text = "TORQUE";
            _Label4.TextAlign = ContentAlignment.MiddleCenter;
            _Label4.ThemeStyle = UI.Controls.ThemeStyle.Black_White;
            _Label4.TooltipText = null;
            // 
            // _Label3
            // 
            _Label3.BackColor = Color.Black;
            _Label3.Font = new Font("D2Coding", 12F);
            _Label3.ForeColor = Color.White;
            _Label3.Location = new Point(6, 22);
            _Label3.Name = "_Label3";
            _Label3.Size = new Size(56, 18);
            _Label3.TabIndex = 0;
            _Label3.Text = "RPM";
            _Label3.TextAlign = ContentAlignment.MiddleCenter;
            _Label3.ThemeStyle = UI.Controls.ThemeStyle.Black_White;
            _Label3.TooltipText = null;
            // 
            // _Label5
            // 
            _Label5.BackColor = Color.FromArgb(46, 204, 113);
            _Label5.Font = new Font("D2Coding", 12F);
            _Label5.ForeColor = Color.Black;
            _Label5.Location = new Point(68, 22);
            _Label5.Name = "_Label5";
            _Label5.Size = new Size(73, 18);
            _Label5.TabIndex = 0;
            _Label5.Text = "0";
            _Label5.TextAlign = ContentAlignment.MiddleRight;
            _Label5.ThemeStyle = UI.Controls.ThemeStyle.Success_Green;
            _Label5.TooltipText = null;
            // 
            // _Label6
            // 
            _Label6.BackColor = Color.FromArgb(46, 204, 113);
            _Label6.Font = new Font("D2Coding", 12F);
            _Label6.ForeColor = Color.Black;
            _Label6.Location = new Point(68, 49);
            _Label6.Name = "_Label6";
            _Label6.Size = new Size(73, 18);
            _Label6.TabIndex = 0;
            _Label6.Text = "0";
            _Label6.TextAlign = ContentAlignment.MiddleRight;
            _Label6.ThemeStyle = UI.Controls.ThemeStyle.Success_Green;
            _Label6.TooltipText = null;
            // 
            // Temp_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(_GroupBox2);
            Controls.Add(_GroupBox1);
            Name = "Temp_View";
            Size = new Size(219, 231);
            Load += Temp_View_Load;
            _GroupBox1.ResumeLayout(false);
            _GroupBox1.PerformLayout();
            _GroupBox2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Controls._Label _Label1;
        private Controls._Label _Label2;
        private Controls._Label _LabelTemp1;
        private Controls._Label _LabelTemp2;
        private Controls._Label _LabeRun1;
        private Controls._Label _LabeRun2;
        private Controls._GroupBox _GroupBox1;
        private System.Windows.Forms.Timer timer1;
        private Controls._GroupBox _GroupBox2;
        private Controls._Label _Label4;
        private Controls._Label _Label3;
        private Controls._Label _Label6;
        private Controls._Label _Label5;
    }
}
