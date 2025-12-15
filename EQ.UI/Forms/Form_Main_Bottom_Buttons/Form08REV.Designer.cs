namespace EQ.UI
{
    partial class Form08REV
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
            extruderSetup_View1 = new EQ.UI.UserViews.Extruder.ExtruderSetup_View();
            SuspendLayout();
            // 
            // extruderSetup_View1
            // 
            extruderSetup_View1.BackColor = Color.FromArgb(80, 80, 80);
            extruderSetup_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            extruderSetup_View1.Location = new Point(12, 13);
            extruderSetup_View1.Margin = new Padding(3, 4, 3, 4);
            extruderSetup_View1.Name = "extruderSetup_View1";
            extruderSetup_View1.Size = new Size(1900, 800);
            extruderSetup_View1.TabIndex = 0;
            // 
            // Form08REV
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1920, 850);
            Controls.Add(extruderSetup_View1);
            Name = "Form08REV";
            Text = "Form8";
            Load += Form08REV_Load;
            ResumeLayout(false);
        }

        #endregion

        private UserViews.Extruder.ExtruderSetup_View extruderSetup_View1;
    }
}