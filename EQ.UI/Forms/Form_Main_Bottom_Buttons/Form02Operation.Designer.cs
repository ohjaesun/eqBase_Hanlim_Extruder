namespace EQ.UI
{
    partial class Form02Operation
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
            extruderOperation_View1 = new EQ.UI.UserViews.Extruder.ExtruderOperation_View();
            SuspendLayout();
            // 
            // extruderOperation_View1
            // 
            extruderOperation_View1.BackColor = Color.FromArgb(80, 80, 80);
            extruderOperation_View1.Dock = DockStyle.Fill;
            extruderOperation_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            extruderOperation_View1.Location = new Point(0, 0);
            extruderOperation_View1.Margin = new Padding(3, 4, 3, 4);
            extruderOperation_View1.Name = "extruderOperation_View1";
            extruderOperation_View1.Size = new Size(1920, 851);
            extruderOperation_View1.TabIndex = 0;
            // 
            // Form02Operation
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1920, 851);
            Controls.Add(extruderOperation_View1);
            Name = "Form02Operation";
            Text = "Form2";
            ResumeLayout(false);
        }

        #endregion

        private UserViews.Extruder.ExtruderOperation_View extruderOperation_View1;
    }
}