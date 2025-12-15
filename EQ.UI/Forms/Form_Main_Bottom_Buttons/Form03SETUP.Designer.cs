namespace EQ.UI
{
    partial class Form03SETUP
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
            PanelMain = new EQ.UI.Controls._Panel();
            _Panel1 = new EQ.UI.Controls._Panel();
            _Panel2 = new EQ.UI.Controls._Panel();
            _Button1 = new EQ.UI.Controls._Button();
            _Button2 = new EQ.UI.Controls._Button();
            _Button3 = new EQ.UI.Controls._Button();
            PanelMain.SuspendLayout();
            _Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // PanelMain
            // 
            PanelMain.BackColor = SystemColors.Control;
            PanelMain.Controls.Add(_Panel2);
            PanelMain.Controls.Add(_Panel1);
            PanelMain.Dock = DockStyle.Fill;
            PanelMain.ForeColor = SystemColors.ControlText;
            PanelMain.Location = new Point(0, 0);
            PanelMain.Name = "PanelMain";
            PanelMain.Size = new Size(1920, 851);
            PanelMain.TabIndex = 0;
            // 
            // _Panel1
            // 
            _Panel1.BackColor = SystemColors.Control;
            _Panel1.Controls.Add(_Button3);
            _Panel1.Controls.Add(_Button2);
            _Panel1.Controls.Add(_Button1);
            _Panel1.Dock = DockStyle.Left;
            _Panel1.ForeColor = SystemColors.ControlText;
            _Panel1.Location = new Point(0, 0);
            _Panel1.Name = "_Panel1";
            _Panel1.Size = new Size(155, 851);
            _Panel1.TabIndex = 0;
            // 
            // _Panel2
            // 
            _Panel2.BackColor = SystemColors.Control;
            _Panel2.Dock = DockStyle.Fill;
            _Panel2.ForeColor = SystemColors.ControlText;
            _Panel2.Location = new Point(155, 0);
            _Panel2.Name = "_Panel2";
            _Panel2.Size = new Size(1765, 851);
            _Panel2.TabIndex = 0;
            // 
            // _Button1
            // 
            _Button1.BackColor = Color.FromArgb(48, 63, 159);
            _Button1.Font = new Font("D2Coding", 12F);
            _Button1.ForeColor = Color.White;
            _Button1.Location = new Point(12, 12);
            _Button1.Name = "_Button1";
            _Button1.Size = new Size(137, 62);
            _Button1.TabIndex = 0;
            _Button1.Text = "Group 1";
            _Button1.TooltipText = null;
            _Button1.UseVisualStyleBackColor = false;
            // 
            // _Button2
            // 
            _Button2.BackColor = Color.FromArgb(48, 63, 159);
            _Button2.Font = new Font("D2Coding", 12F);
            _Button2.ForeColor = Color.White;
            _Button2.Location = new Point(12, 97);
            _Button2.Name = "_Button2";
            _Button2.Size = new Size(137, 62);
            _Button2.TabIndex = 0;
            _Button2.Text = "Group 2";
            _Button2.TooltipText = null;
            _Button2.UseVisualStyleBackColor = false;
            // 
            // _Button3
            // 
            _Button3.BackColor = Color.FromArgb(48, 63, 159);
            _Button3.Font = new Font("D2Coding", 12F);
            _Button3.ForeColor = Color.White;
            _Button3.Location = new Point(12, 189);
            _Button3.Name = "_Button3";
            _Button3.Size = new Size(137, 62);
            _Button3.TabIndex = 0;
            _Button3.Text = "PID";
            _Button3.TooltipText = null;
            _Button3.UseVisualStyleBackColor = false;
            // 
            // Form03SETUP
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1920, 851);
            Controls.Add(PanelMain);
            Name = "Form03SETUP";
            Text = "Form3";
            PanelMain.ResumeLayout(false);
            _Panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Controls._Panel PanelMain;
        private Controls._Panel _Panel2;
        private Controls._Panel _Panel1;
        private Controls._Button _Button1;
        private Controls._Button _Button3;
        private Controls._Button _Button2;
    }
}