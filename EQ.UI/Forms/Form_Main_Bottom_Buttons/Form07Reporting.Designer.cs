namespace EQ.UI
{
    partial class Form07Reporting
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
            auditTrail_View1 = new EQ.UI.UserViews.AuditTrail_View();
            SuspendLayout();
            // 
            // auditTrail_View1
            // 
            auditTrail_View1.BackColor = Color.FromArgb(80, 80, 80);
            auditTrail_View1.Dock = DockStyle.Fill;
            auditTrail_View1.Font = new Font("D2Coding", 15.7499981F, FontStyle.Regular, GraphicsUnit.Point, 129);
            auditTrail_View1.Location = new Point(0, 0);
            auditTrail_View1.Margin = new Padding(4, 5, 4, 5);
            auditTrail_View1.Name = "auditTrail_View1";
            auditTrail_View1.Size = new Size(993, 832);
            auditTrail_View1.TabIndex = 0;
            // 
            // Form07Reporting
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(993, 832);
            Controls.Add(auditTrail_View1);
            Name = "Form07Reporting";
            Text = "Form7";
            ResumeLayout(false);
        }

        #endregion

        private UserViews.AuditTrail_View auditTrail_View1;
    }
}