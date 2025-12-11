namespace EQ.UI
{
    partial class FormTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            _Label1 = new EQ.UI.Controls._Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            _Button11 = new EQ.UI.Controls._Button();
            _Button10 = new EQ.UI.Controls._Button();
            _Button9 = new EQ.UI.Controls._Button();
            _Button8 = new EQ.UI.Controls._Button();
            _Button7 = new EQ.UI.Controls._Button();
            _Button6 = new EQ.UI.Controls._Button();
            _Button5 = new EQ.UI.Controls._Button();
            _Button4 = new EQ.UI.Controls._Button();
            _Button3 = new EQ.UI.Controls._Button();
            _Button2 = new EQ.UI.Controls._Button();
            _Button1 = new EQ.UI.Controls._Button();
            tabPage2 = new TabPage();
            waferMap_View2 = new EQ.UI.UserViews.WaferMap_View();
            waferMap_View1 = new EQ.UI.UserViews.WaferMap_View();
            tabPage3 = new TabPage();
            piO_View1 = new EQ.UI.UserViews.Setup.PIO_View();
            tabPage4 = new TabPage();
            laserMeasure_View1 = new EQ.UI.UserViews.LaserMeasure.LaserMeasure_View();
            secsGem_View1 = new EQ.UI.UserViews.SecsGem.SecsGem_View();
            tabPage5 = new TabPage();
            log_View1 = new EQ.UI.UserViews.Log_View();
            sequencesPanel_View1 = new EQ.UI.UserViews.SequencesPanel_View();
            tabPage6 = new TabPage();
            alarm_View1 = new EQ.UI.UserViews.Alarm_View();
            tabPage7 = new TabPage();
            gVision_View1 = new EQ.UI.UserViews.GVision_View();
            tabPage8 = new TabPage();
            motionSpeed_View1 = new EQ.UI.UserViews.MotionSpeed_View();
            tabPage9 = new TabPage();
            motor_View1 = new EQ.UI.UserViews.Motor_View();
            tabPage10 = new TabPage();
            motorPosition_View1 = new EQ.UI.UserViews.MotorPosition_View();
            tabPage11 = new TabPage();
            motorInterlock_View1 = new EQ.UI.UserViews.MotorInterlock_View();
            tabPage12 = new TabPage();
            alarmSolution_View1 = new EQ.UI.UserViews.AlarmSolution_View();
            tabPage13 = new TabPage();
            statistics_ScottPlot_View1 = new EQ.UI.UserViews.Statistics_ScottPlot_View();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            tabPage5.SuspendLayout();
            tabPage6.SuspendLayout();
            tabPage7.SuspendLayout();
            tabPage8.SuspendLayout();
            tabPage9.SuspendLayout();
            tabPage10.SuspendLayout();
            tabPage11.SuspendLayout();
            tabPage12.SuspendLayout();
            tabPage13.SuspendLayout();
            SuspendLayout();
            // 
            // _Label1
            // 
            _Label1.AutoSize = true;
            _Label1.BackColor = Color.LightYellow;
            _Label1.Font = new Font("D2Coding", 12F);
            _Label1.ForeColor = Color.Black;
            _Label1.Location = new Point(785, 19);
            _Label1.Name = "_Label1";
            _Label1.Size = new Size(192, 18);
            _Label1.TabIndex = 0;
            _Label1.Text = "각종 기능들 테스트용 폼";
            _Label1.ThemeStyle = UI.Controls.ThemeStyle.DesignModeOnly;
            _Label1.TooltipText = null;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Controls.Add(tabPage6);
            tabControl1.Controls.Add(tabPage7);
            tabControl1.Controls.Add(tabPage8);
            tabControl1.Controls.Add(tabPage9);
            tabControl1.Controls.Add(tabPage10);
            tabControl1.Controls.Add(tabPage11);
            tabControl1.Controls.Add(tabPage12);
            tabControl1.Controls.Add(tabPage13);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(993, 872);
            tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(_Button11);
            tabPage1.Controls.Add(_Button10);
            tabPage1.Controls.Add(_Button9);
            tabPage1.Controls.Add(_Button8);
            tabPage1.Controls.Add(_Button7);
            tabPage1.Controls.Add(_Button6);
            tabPage1.Controls.Add(_Button5);
            tabPage1.Controls.Add(_Button4);
            tabPage1.Controls.Add(_Button3);
            tabPage1.Controls.Add(_Button2);
            tabPage1.Controls.Add(_Button1);
            tabPage1.Location = new Point(4, 27);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(985, 841);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // _Button11
            // 
            _Button11.BackColor = Color.FromArgb(48, 63, 159);
            _Button11.Font = new Font("D2Coding", 12F);
            _Button11.ForeColor = Color.White;
            _Button11.Location = new Point(525, 25);
            _Button11.Name = "_Button11";
            _Button11.Size = new Size(100, 55);
            _Button11.TabIndex = 9;
            _Button11.Text = "secsgem";
            _Button11.TooltipText = null;
            _Button11.UseVisualStyleBackColor = false;
            _Button11.Click += _Button11_Click;
            // 
            // _Button10
            // 
            _Button10.BackColor = Color.FromArgb(48, 63, 159);
            _Button10.Font = new Font("D2Coding", 12F);
            _Button10.ForeColor = Color.White;
            _Button10.Location = new Point(30, 351);
            _Button10.Name = "_Button10";
            _Button10.Size = new Size(100, 55);
            _Button10.TabIndex = 8;
            _Button10.Text = "데이터개별저";
            _Button10.TooltipText = null;
            _Button10.UseVisualStyleBackColor = false;
            _Button10.Click += _Button10_Click;
            // 
            // _Button9
            // 
            _Button9.BackColor = Color.FromArgb(48, 63, 159);
            _Button9.Font = new Font("D2Coding", 12F);
            _Button9.ForeColor = Color.White;
            _Button9.Location = new Point(157, 351);
            _Button9.Name = "_Button9";
            _Button9.Size = new Size(100, 55);
            _Button9.TabIndex = 8;
            _Button9.Text = "데이터저장";
            _Button9.TooltipText = null;
            _Button9.UseVisualStyleBackColor = false;
            _Button9.Click += _Button9_Click;
            // 
            // _Button8
            // 
            _Button8.BackColor = Color.FromArgb(48, 63, 159);
            _Button8.Font = new Font("D2Coding", 12F);
            _Button8.ForeColor = Color.White;
            _Button8.Location = new Point(30, 260);
            _Button8.Name = "_Button8";
            _Button8.Size = new Size(100, 55);
            _Button8.TabIndex = 7;
            _Button8.Text = "알람발생";
            _Button8.TooltipText = null;
            _Button8.UseVisualStyleBackColor = false;
            _Button8.Click += _Button8_Click;
            // 
            // _Button7
            // 
            _Button7.BackColor = Color.FromArgb(48, 63, 159);
            _Button7.Font = new Font("D2Coding", 12F);
            _Button7.ForeColor = Color.White;
            _Button7.Location = new Point(157, 118);
            _Button7.Name = "_Button7";
            _Button7.Size = new Size(100, 55);
            _Button7.TabIndex = 6;
            _Button7.Text = "정의위치이동";
            _Button7.TooltipText = null;
            _Button7.UseVisualStyleBackColor = false;
            _Button7.Click += _Button7_Click;
            // 
            // _Button6
            // 
            _Button6.BackColor = Color.FromArgb(48, 63, 159);
            _Button6.Font = new Font("D2Coding", 12F);
            _Button6.ForeColor = Color.White;
            _Button6.Location = new Point(30, 179);
            _Button6.Name = "_Button6";
            _Button6.Size = new Size(100, 55);
            _Button6.TabIndex = 5;
            _Button6.Text = "로그인등급";
            _Button6.TooltipText = null;
            _Button6.UseVisualStyleBackColor = false;
            _Button6.Click += _Button6_Click;
            // 
            // _Button5
            // 
            _Button5.BackColor = Color.FromArgb(48, 63, 159);
            _Button5.Font = new Font("D2Coding", 12F);
            _Button5.ForeColor = Color.White;
            _Button5.Location = new Point(30, 118);
            _Button5.Name = "_Button5";
            _Button5.Size = new Size(100, 55);
            _Button5.TabIndex = 4;
            _Button5.Text = "로그인";
            _Button5.TooltipText = null;
            _Button5.UseVisualStyleBackColor = false;
            _Button5.Click += _Button5_Click;
            // 
            // _Button4
            // 
            _Button4.BackColor = Color.FromArgb(48, 63, 159);
            _Button4.Font = new Font("D2Coding", 12F);
            _Button4.ForeColor = Color.White;
            _Button4.Location = new Point(399, 25);
            _Button4.Name = "_Button4";
            _Button4.Size = new Size(100, 55);
            _Button4.TabIndex = 3;
            _Button4.Text = "타워램프";
            _Button4.TooltipText = null;
            _Button4.UseVisualStyleBackColor = false;
            _Button4.Click += _Button4_Click;
            // 
            // _Button3
            // 
            _Button3.BackColor = Color.FromArgb(48, 63, 159);
            _Button3.Font = new Font("D2Coding", 12F);
            _Button3.ForeColor = Color.White;
            _Button3.Location = new Point(279, 25);
            _Button3.Name = "_Button3";
            _Button3.Size = new Size(100, 55);
            _Button3.TabIndex = 2;
            _Button3.Text = "액션 실행";
            _Button3.TooltipText = null;
            _Button3.UseVisualStyleBackColor = false;
            _Button3.Click += _Button3_Click;
            // 
            // _Button2
            // 
            _Button2.BackColor = Color.FromArgb(48, 63, 159);
            _Button2.Font = new Font("D2Coding", 12F);
            _Button2.ForeColor = Color.White;
            _Button2.Location = new Point(157, 25);
            _Button2.Name = "_Button2";
            _Button2.Size = new Size(100, 55);
            _Button2.TabIndex = 1;
            _Button2.Text = "시퀀스 실행";
            _Button2.TooltipText = null;
            _Button2.UseVisualStyleBackColor = false;
            _Button2.Click += _Button2_Click;
            // 
            // _Button1
            // 
            _Button1.BackColor = Color.FromArgb(48, 63, 159);
            _Button1.Font = new Font("D2Coding", 12F);
            _Button1.ForeColor = Color.White;
            _Button1.Location = new Point(30, 25);
            _Button1.Name = "_Button1";
            _Button1.Size = new Size(100, 55);
            _Button1.TabIndex = 0;
            _Button1.Text = "팝업";
            _Button1.TooltipText = null;
            _Button1.UseVisualStyleBackColor = false;
            _Button1.Click += _Button1_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(waferMap_View2);
            tabPage2.Controls.Add(waferMap_View1);
            tabPage2.Location = new Point(4, 27);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(985, 841);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // waferMap_View2
            // 
            waferMap_View2.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            waferMap_View2.Location = new Point(467, 18);
            waferMap_View2.Margin = new Padding(3, 4, 3, 4);
            waferMap_View2.Name = "waferMap_View2";
            waferMap_View2.ShotSizeX = 5;
            waferMap_View2.ShotSizeY = 5;
            waferMap_View2.ShowGrid = true;
            waferMap_View2.Size = new Size(439, 423);
            waferMap_View2.TabIndex = 0;
            // 
            // waferMap_View1
            // 
            waferMap_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            waferMap_View1.Location = new Point(8, 18);
            waferMap_View1.Margin = new Padding(3, 4, 3, 4);
            waferMap_View1.Name = "waferMap_View1";
            waferMap_View1.ShotSizeX = 5;
            waferMap_View1.ShotSizeY = 5;
            waferMap_View1.ShowGrid = true;
            waferMap_View1.Size = new Size(425, 423);
            waferMap_View1.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(piO_View1);
            tabPage3.Location = new Point(4, 27);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(985, 841);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "tabPage3";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // piO_View1
            // 
            piO_View1.Dock = DockStyle.Fill;
            piO_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            piO_View1.Location = new Point(3, 3);
            piO_View1.Margin = new Padding(3, 6, 3, 6);
            piO_View1.Name = "piO_View1";
            piO_View1.Size = new Size(979, 835);
            piO_View1.TabIndex = 0;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(laserMeasure_View1);
            tabPage4.Controls.Add(secsGem_View1);
            tabPage4.Location = new Point(4, 27);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(985, 841);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "tabPage4";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // laserMeasure_View1
            // 
            laserMeasure_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            laserMeasure_View1.Location = new Point(8, 64);
            laserMeasure_View1.Margin = new Padding(3, 4, 3, 4);
            laserMeasure_View1.Name = "laserMeasure_View1";
            laserMeasure_View1.Size = new Size(945, 484);
            laserMeasure_View1.TabIndex = 1;
            // 
            // secsGem_View1
            // 
            secsGem_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            secsGem_View1.Location = new Point(8, 7);
            secsGem_View1.Margin = new Padding(3, 4, 3, 4);
            secsGem_View1.Name = "secsGem_View1";
            secsGem_View1.Size = new Size(297, 228);
            secsGem_View1.TabIndex = 0;
            // 
            // tabPage5
            // 
            tabPage5.Controls.Add(log_View1);
            tabPage5.Controls.Add(sequencesPanel_View1);
            tabPage5.Controls.Add(_Label1);
            tabPage5.Location = new Point(4, 27);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(3);
            tabPage5.Size = new Size(985, 841);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "tabPage5";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // log_View1
            // 
            log_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            log_View1.Location = new Point(258, 41);
            log_View1.Margin = new Padding(3, 4, 3, 4);
            log_View1.Name = "log_View1";
            log_View1.Size = new Size(719, 484);
            log_View1.TabIndex = 2;
            // 
            // sequencesPanel_View1
            // 
            sequencesPanel_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            sequencesPanel_View1.Location = new Point(17, 19);
            sequencesPanel_View1.Margin = new Padding(3, 4, 3, 4);
            sequencesPanel_View1.Name = "sequencesPanel_View1";
            sequencesPanel_View1.Size = new Size(203, 222);
            sequencesPanel_View1.TabIndex = 1;
            // 
            // tabPage6
            // 
            tabPage6.Controls.Add(alarm_View1);
            tabPage6.Location = new Point(4, 27);
            tabPage6.Name = "tabPage6";
            tabPage6.Padding = new Padding(3);
            tabPage6.Size = new Size(985, 841);
            tabPage6.TabIndex = 5;
            tabPage6.Text = "tabPage6";
            tabPage6.UseVisualStyleBackColor = true;
            // 
            // alarm_View1
            // 
            alarm_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            alarm_View1.Location = new Point(54, 7);
            alarm_View1.Margin = new Padding(3, 4, 3, 4);
            alarm_View1.Name = "alarm_View1";
            alarm_View1.Size = new Size(752, 572);
            alarm_View1.TabIndex = 0;
            // 
            // tabPage7
            // 
            tabPage7.Controls.Add(gVision_View1);
            tabPage7.Location = new Point(4, 27);
            tabPage7.Name = "tabPage7";
            tabPage7.Padding = new Padding(3);
            tabPage7.Size = new Size(985, 841);
            tabPage7.TabIndex = 6;
            tabPage7.Text = "tabPage7";
            tabPage7.UseVisualStyleBackColor = true;
            // 
            // gVision_View1
            // 
            gVision_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            gVision_View1.Location = new Point(58, 7);
            gVision_View1.Margin = new Padding(3, 4, 3, 4);
            gVision_View1.Name = "gVision_View1";
            gVision_View1.Size = new Size(703, 639);
            gVision_View1.TabIndex = 0;
            // 
            // tabPage8
            // 
            tabPage8.Controls.Add(motionSpeed_View1);
            tabPage8.Location = new Point(4, 27);
            tabPage8.Name = "tabPage8";
            tabPage8.Padding = new Padding(3);
            tabPage8.Size = new Size(985, 841);
            tabPage8.TabIndex = 7;
            tabPage8.Text = "tabPage8";
            tabPage8.UseVisualStyleBackColor = true;
            // 
            // motionSpeed_View1
            // 
            motionSpeed_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            motionSpeed_View1.Location = new Point(43, 4);
            motionSpeed_View1.Margin = new Padding(3, 4, 3, 4);
            motionSpeed_View1.Name = "motionSpeed_View1";
            motionSpeed_View1.Size = new Size(800, 600);
            motionSpeed_View1.TabIndex = 0;
            // 
            // tabPage9
            // 
            tabPage9.Controls.Add(motor_View1);
            tabPage9.Location = new Point(4, 27);
            tabPage9.Name = "tabPage9";
            tabPage9.Padding = new Padding(3);
            tabPage9.Size = new Size(985, 841);
            tabPage9.TabIndex = 8;
            tabPage9.Text = "tabPage9";
            tabPage9.UseVisualStyleBackColor = true;
            // 
            // motor_View1
            // 
            motor_View1.Dock = DockStyle.Fill;
            motor_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            motor_View1.Location = new Point(3, 3);
            motor_View1.Margin = new Padding(3, 4, 3, 4);
            motor_View1.Name = "motor_View1";
            motor_View1.Size = new Size(979, 835);
            motor_View1.TabIndex = 0;
            // 
            // tabPage10
            // 
            tabPage10.Controls.Add(motorPosition_View1);
            tabPage10.Location = new Point(4, 27);
            tabPage10.Name = "tabPage10";
            tabPage10.Padding = new Padding(3);
            tabPage10.Size = new Size(985, 841);
            tabPage10.TabIndex = 9;
            tabPage10.Text = "tabPage10";
            tabPage10.UseVisualStyleBackColor = true;
            // 
            // motorPosition_View1
            // 
            motorPosition_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            motorPosition_View1.Location = new Point(21, 7);
            motorPosition_View1.Margin = new Padding(3, 4, 3, 4);
            motorPosition_View1.Name = "motorPosition_View1";
            motorPosition_View1.Size = new Size(900, 600);
            motorPosition_View1.TabIndex = 0;
            // 
            // tabPage11
            // 
            tabPage11.Controls.Add(motorInterlock_View1);
            tabPage11.Location = new Point(4, 27);
            tabPage11.Name = "tabPage11";
            tabPage11.Padding = new Padding(3);
            tabPage11.Size = new Size(985, 841);
            tabPage11.TabIndex = 10;
            tabPage11.Text = "tabPage11";
            tabPage11.UseVisualStyleBackColor = true;
            // 
            // motorInterlock_View1
            // 
            motorInterlock_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            motorInterlock_View1.Location = new Point(25, 16);
            motorInterlock_View1.Margin = new Padding(3, 4, 3, 4);
            motorInterlock_View1.Name = "motorInterlock_View1";
            motorInterlock_View1.Size = new Size(900, 700);
            motorInterlock_View1.TabIndex = 0;
            // 
            // tabPage12
            // 
            tabPage12.Controls.Add(alarmSolution_View1);
            tabPage12.Location = new Point(4, 27);
            tabPage12.Name = "tabPage12";
            tabPage12.Padding = new Padding(3);
            tabPage12.Size = new Size(985, 841);
            tabPage12.TabIndex = 11;
            tabPage12.Text = "tabPage12";
            tabPage12.UseVisualStyleBackColor = true;
            // 
            // alarmSolution_View1
            // 
            alarmSolution_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            alarmSolution_View1.Location = new Point(19, 7);
            alarmSolution_View1.Margin = new Padding(3, 4, 3, 4);
            alarmSolution_View1.Name = "alarmSolution_View1";
            alarmSolution_View1.Size = new Size(900, 600);
            alarmSolution_View1.TabIndex = 0;
            // 
            // tabPage13
            // 
            tabPage13.Controls.Add(statistics_ScottPlot_View1);
            tabPage13.Location = new Point(4, 27);
            tabPage13.Name = "tabPage13";
            tabPage13.Padding = new Padding(3);
            tabPage13.Size = new Size(985, 841);
            tabPage13.TabIndex = 12;
            tabPage13.Text = "tabPage13";
            tabPage13.UseVisualStyleBackColor = true;
            // 
            // statistics_ScottPlot_View1
            // 
            statistics_ScottPlot_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            statistics_ScottPlot_View1.Location = new Point(8, 7);
            statistics_ScottPlot_View1.Margin = new Padding(3, 4, 3, 4);
            statistics_ScottPlot_View1.Name = "statistics_ScottPlot_View1";
            statistics_ScottPlot_View1.Size = new Size(900, 600);
            statistics_ScottPlot_View1.TabIndex = 0;
            // 
            // FormTest
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(993, 872);
            Controls.Add(tabControl1);
            Name = "FormTest";
            Text = "FormTest";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tabPage4.ResumeLayout(false);
            tabPage5.ResumeLayout(false);
            tabPage5.PerformLayout();
            tabPage6.ResumeLayout(false);
            tabPage7.ResumeLayout(false);
            tabPage8.ResumeLayout(false);
            tabPage9.ResumeLayout(false);
            tabPage10.ResumeLayout(false);
            tabPage11.ResumeLayout(false);
            tabPage12.ResumeLayout(false);
            tabPage13.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Controls._Label _Label1;
        private TabControl tabControl1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private UserViews.SequencesPanel_View sequencesPanel_View1;
        private UserViews.Log_View log_View1;
        private TabPage tabPage6;
        private TabPage tabPage7;
        private TabPage tabPage8;
        private TabPage tabPage9;
        private TabPage tabPage10;
        private TabPage tabPage11;
        private TabPage tabPage12;
        private UserViews.Alarm_View alarm_View1;
        private UserViews.GVision_View gVision_View1;
        private UserViews.Motor_View motor_View1;
        private UserViews.MotionSpeed_View motionSpeed_View1;
        private UserViews.MotorPosition_View motorPosition_View1;
        private UserViews.MotorInterlock_View motorInterlock_View1;
        private UserViews.AlarmSolution_View alarmSolution_View1;
        private TabPage tabPage1;
        private Controls._Button _Button8;
        private Controls._Button _Button7;
        private Controls._Button _Button6;
        private Controls._Button _Button5;
        private Controls._Button _Button4;
        private Controls._Button _Button3;
        private Controls._Button _Button2;
        private Controls._Button _Button1;
        private TabPage tabPage13;
        private Controls._Button _Button10;
        private Controls._Button _Button9;
        private UserViews.WaferMap_View waferMap_View2;
        private UserViews.WaferMap_View waferMap_View1;
        private UserViews.Statistics_ScottPlot_View statistics_ScottPlot_View1;
        private UserViews.Setup.PIO_View piO_View1;
        private Controls._Button _Button11;
        private UserViews.SecsGem.SecsGem_View secsGem_View1;
        private UserViews.LaserMeasure.LaserMeasure_View laserMeasure_View1;
    }
}