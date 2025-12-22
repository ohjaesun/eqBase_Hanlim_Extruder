namespace EQ.UI.Forms
{
    partial class FormAdminTest
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
            _Panel1 = new EQ.UI.Controls._Panel();
            _Panel2 = new EQ.UI.Controls._Panel();
            test1 = new EQ.UI.UserViews.EQ_HanLim_Extuder.Test();
            _Panel2.SuspendLayout();
            SuspendLayout();
            // 
            // _Panel1
            // 
            _Panel1.BackColor = SystemColors.Control;
            _Panel1.Dock = DockStyle.Top;
            _Panel1.ForeColor = SystemColors.ControlText;
            _Panel1.Location = new Point(0, 0);
            _Panel1.Name = "_Panel1";
            _Panel1.Size = new Size(922, 30);
            _Panel1.TabIndex = 0;
            // 
            // _Panel2
            // 
            _Panel2.BackColor = SystemColors.Control;
            _Panel2.Controls.Add(test1);
            _Panel2.Dock = DockStyle.Fill;
            _Panel2.ForeColor = SystemColors.ControlText;
            _Panel2.Location = new Point(0, 30);
            _Panel2.Name = "_Panel2";
            _Panel2.Size = new Size(922, 547);
            _Panel2.TabIndex = 0;
            // 
            // test1
            // 
            test1.BackColor = Color.FromArgb(80, 80, 80);
            test1.Dock = DockStyle.Fill;
            test1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            test1.Location = new Point(0, 0);
            test1.Margin = new Padding(3, 4, 3, 4);
            test1.Name = "test1";
            test1.Size = new Size(922, 547);
            test1.TabIndex = 0;
            // 
            // FormAdminTest
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(922, 577);
            Controls.Add(_Panel2);
            Controls.Add(_Panel1);
            Name = "FormAdminTest";
            Text = "FormAdminTest";
            TopMost = true;
            _Panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Controls._Panel _Panel1;
        private Controls._Panel _Panel2;
        private UserViews.EQ_HanLim_Extuder.Test test1;
    }
}