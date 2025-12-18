namespace EQ.UI
{
    partial class Form05User
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
            users_View1 = new EQ.UI.UserViews.Users_View();
            SuspendLayout();
            // 
            // users_View1
            // 
            users_View1.BackColor = Color.FromArgb(80, 80, 80);
            users_View1.Dock = DockStyle.Fill;
            users_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            users_View1.Location = new Point(0, 0);
            users_View1.Margin = new Padding(3, 6, 3, 6);
            users_View1.Name = "users_View1";
            users_View1.Size = new Size(993, 832);
            users_View1.TabIndex = 0;
            // 
            // Form05User
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(993, 832);
            Controls.Add(users_View1);
            Name = "Form05User";
            Text = "Form5";
            ResumeLayout(false);
        }

        #endregion

        private UserViews.Users_View users_View1;
    }
}