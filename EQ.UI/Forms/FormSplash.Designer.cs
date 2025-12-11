namespace EQ.UI
{
    partial class FormSplash
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
            components = new System.ComponentModel.Container();
            lblStatus = new Label();
            richTextBox1 = new RichTextBox();
            _timerAutoClose = new System.Windows.Forms.Timer(components);
            _ButtonStart = new EQ.UI.Controls._Button();
            SuspendLayout();
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 9);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(56, 18);
            lblStatus.TabIndex = 0;
            lblStatus.Text = "label1";
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(12, 30);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(776, 361);
            richTextBox1.TabIndex = 1;
            richTextBox1.Text = "";
            // 
            // _timerAutoClose
            // 
            _timerAutoClose.Interval = 1000;
            _timerAutoClose.Tick += _timerAutoClose_Tick;
            // 
            // _ButtonStart
            // 
            _ButtonStart.BackColor = Color.FromArgb(52, 73, 94);
            _ButtonStart.Font = new Font("D2Coding", 12F);
            _ButtonStart.ForeColor = Color.White;
            _ButtonStart.Location = new Point(688, 397);
            _ButtonStart.Name = "_ButtonStart";
            _ButtonStart.Size = new Size(100, 55);
            _ButtonStart.TabIndex = 2;
            _ButtonStart.Text = "_Button1";
            _ButtonStart.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _ButtonStart.TooltipText = null;
            _ButtonStart.UseVisualStyleBackColor = false;
            _ButtonStart.Click += _ButtonStart_Click;
            // 
            // FormSplash
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 464);
            Controls.Add(_ButtonStart);
            Controls.Add(richTextBox1);
            Controls.Add(lblStatus);
            Margin = new Padding(3, 5, 3, 5);
            Name = "FormSplash";
            Text = "FormSplash";
            Shown += FormSplash_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblStatus;
        private RichTextBox richTextBox1;
        private System.Windows.Forms.Timer _timerAutoClose;
        private Controls._Button _ButtonStart;
    }
}