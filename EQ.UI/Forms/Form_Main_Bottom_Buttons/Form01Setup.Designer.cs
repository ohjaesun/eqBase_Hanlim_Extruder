namespace EQ.UI
{
    partial class Form01Setup
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
            _Panel2 = new EQ.UI.Controls._Panel();
            extruderSetup_View1 = new EQ.UI.UserViews.Extruder.ExtruderSetup_View();
            _Panel2.SuspendLayout();
            SuspendLayout();
            // 
            // _Panel2
            // 
            _Panel2.BackColor = SystemColors.Control;
            _Panel2.Controls.Add(extruderSetup_View1);
            _Panel2.Dock = DockStyle.Fill;
            _Panel2.ForeColor = SystemColors.ControlText;
            _Panel2.Location = new Point(0, 0);
            _Panel2.Name = "_Panel2";
            _Panel2.Size = new Size(993, 832);
            _Panel2.TabIndex = 2;
            // 
            // extruderSetup_View1
            // 
            extruderSetup_View1.BackColor = Color.FromArgb(80, 80, 80);
            extruderSetup_View1.Dock = DockStyle.Fill;
            extruderSetup_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            extruderSetup_View1.Location = new Point(0, 0);
            extruderSetup_View1.Margin = new Padding(3, 4, 3, 4);
            extruderSetup_View1.Name = "extruderSetup_View1";
            extruderSetup_View1.Size = new Size(993, 832);
            extruderSetup_View1.TabIndex = 0;
            // 
            // Form01AUTO
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(993, 832);
            Controls.Add(_Panel2);
            Name = "Form01AUTO";
            Text = "Form1";
            _Panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Controls._Panel _Panel2;
        private UserViews.Extruder.ExtruderSetup_View extruderSetup_View1;
    }
}