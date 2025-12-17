namespace EQ.UI
{
    partial class Form04Trends
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
            extruderChart_View1 = new EQ.UI.UserViews.Extruder.ExtruderChart_View();
            SuspendLayout();
            // 
            // extruderChart_View1
            // 
            extruderChart_View1.Dock = DockStyle.Fill;
            extruderChart_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            extruderChart_View1.Location = new Point(0, 0);
            extruderChart_View1.Margin = new Padding(3, 4, 3, 4);
            extruderChart_View1.Name = "extruderChart_View1";
            extruderChart_View1.Size = new Size(993, 832);
            extruderChart_View1.TabIndex = 0;
            // 
            // Form04Trends
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(993, 832);
            Controls.Add(extruderChart_View1);
            Name = "Form04Trends";
            Text = "Form4";
            ResumeLayout(false);
        }

        #endregion


        private UserViews.Extruder.ExtruderChart_View extruderChart_View1;
    }
}