namespace EQ.UI
{
    partial class Form10Alarm
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
            alarmSolution_View1 = new EQ.UI.UserViews.AlarmSolution_View();
            SuspendLayout();
            // 
            // alarmSolution_View1
            // 
            alarmSolution_View1.Dock = DockStyle.Fill;
            alarmSolution_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            alarmSolution_View1.Location = new Point(0, 0);
            alarmSolution_View1.Margin = new Padding(3, 4, 3, 4);
            alarmSolution_View1.Name = "alarmSolution_View1";
            alarmSolution_View1.Size = new Size(1920, 850);
            alarmSolution_View1.TabIndex = 0;
            // 
            // Form10Alarm
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1920, 850);
            Controls.Add(alarmSolution_View1);
            Name = "Form10Alarm";
            Text = "Form8";
            Load += Form08REV_Load;
            ResumeLayout(false);
        }

        #endregion

        private UserViews.AlarmSolution_View alarmSolution_View1;
    }
}