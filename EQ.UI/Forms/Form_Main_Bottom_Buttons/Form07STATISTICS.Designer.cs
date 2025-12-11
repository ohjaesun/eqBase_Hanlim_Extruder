namespace EQ.UI
{
    partial class Form07STATISTICS
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
            flowLayoutPanel1 = new FlowLayoutPanel();
            _Button1 = new EQ.UI.Controls._Button();
            _Button2 = new EQ.UI.Controls._Button();
            _Button3 = new EQ.UI.Controls._Button();
            _Button4 = new EQ.UI.Controls._Button();
            _Button5 = new EQ.UI.Controls._Button();
            panelMain = new EQ.UI.Controls._Panel();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(_Button1);
            flowLayoutPanel1.Controls.Add(_Button2);
            flowLayoutPanel1.Controls.Add(_Button3);
            flowLayoutPanel1.Controls.Add(_Button4);
            flowLayoutPanel1.Controls.Add(_Button5);
            flowLayoutPanel1.Dock = DockStyle.Top;
            flowLayoutPanel1.Location = new Point(0, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(993, 65);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // _Button1
            // 
            _Button1.BackColor = Color.FromArgb(48, 63, 159);
            _Button1.Font = new Font("D2Coding", 12F);
            _Button1.ForeColor = Color.White;
            _Button1.Location = new Point(3, 3);
            _Button1.Name = "_Button1";
            _Button1.Size = new Size(100, 55);
            _Button1.TabIndex = 0;
            _Button1.Text = "Alarm";
            _Button1.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _Button1.TooltipText = null;
            _Button1.UseVisualStyleBackColor = false;
            _Button1.Click += _Button1_Click;
            // 
            // _Button2
            // 
            _Button2.BackColor = Color.FromArgb(48, 63, 159);
            _Button2.Font = new Font("D2Coding", 12F);
            _Button2.ForeColor = Color.White;
            _Button2.Location = new Point(109, 3);
            _Button2.Name = "_Button2";
            _Button2.Size = new Size(100, 55);
            _Button2.TabIndex = 1;
            _Button2.Text = "DB_Export";
            _Button2.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _Button2.TooltipText = null;
            _Button2.UseVisualStyleBackColor = false;
            _Button2.Click += _Button1_Click;
            // 
            // _Button3
            // 
            _Button3.BackColor = Color.FromArgb(48, 63, 159);
            _Button3.Font = new Font("D2Coding", 12F);
            _Button3.ForeColor = Color.White;
            _Button3.Location = new Point(215, 3);
            _Button3.Name = "_Button3";
            _Button3.Size = new Size(100, 55);
            _Button3.TabIndex = 2;
            _Button3.Text = "Times";
            _Button3.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _Button3.TooltipText = null;
            _Button3.UseVisualStyleBackColor = false;
            _Button3.Click += _Button1_Click;
            // 
            // _Button4
            // 
            _Button4.BackColor = Color.FromArgb(48, 63, 159);
            _Button4.Font = new Font("D2Coding", 12F);
            _Button4.ForeColor = Color.White;
            _Button4.Location = new Point(321, 3);
            _Button4.Name = "_Button4";
            _Button4.Size = new Size(100, 55);
            _Button4.TabIndex = 3;
            _Button4.Text = "_Button4";
            _Button4.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _Button4.TooltipText = null;
            _Button4.UseVisualStyleBackColor = false;
            _Button4.Click += _Button1_Click;
            // 
            // _Button5
            // 
            _Button5.BackColor = Color.FromArgb(48, 63, 159);
            _Button5.Font = new Font("D2Coding", 12F);
            _Button5.ForeColor = Color.White;
            _Button5.Location = new Point(427, 3);
            _Button5.Name = "_Button5";
            _Button5.Size = new Size(100, 55);
            _Button5.TabIndex = 4;
            _Button5.Text = "_Button5";
            _Button5.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _Button5.TooltipText = null;
            _Button5.UseVisualStyleBackColor = false;
            _Button5.Click += _Button1_Click;
            // 
            // panelMain
            // 
            panelMain.BackColor = SystemColors.Control;
            panelMain.Dock = DockStyle.Fill;
            panelMain.ForeColor = SystemColors.ControlText;
            panelMain.Location = new Point(0, 65);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(993, 767);
            panelMain.TabIndex = 1;
            // 
            // Form07STATISTICS
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(993, 832);
            Controls.Add(panelMain);
            Controls.Add(flowLayoutPanel1);
            Name = "Form07STATISTICS";
            Text = "Form7";
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Controls._Button _Button1;
        private Controls._Button _Button2;
        private Controls._Button _Button3;
        private Controls._Button _Button4;
        private Controls._Button _Button5;
        private Controls._Panel panelMain;
    }
}