namespace EQ.UI.Forms
{
    partial class FormAlarmPopup
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
            _PanelTitle = new EQ.UI.Controls._Panel();
            _LabelTitle = new EQ.UI.Controls._Label();
            labelMessage = new Label();
            tableLayoutPanel1 = new TableLayoutPanel();
            _ButtonSilence = new EQ.UI.Controls._Button();
            _ButtonReset = new EQ.UI.Controls._Button();
            _ButtonClose = new EQ.UI.Controls._Button();
            _Panel1 = new EQ.UI.Controls._Panel();
            label2 = new Label();
            label1 = new Label();
            _PanelTitle.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            _Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // _PanelTitle
            // 
            _PanelTitle.BackColor = SystemColors.Control;
            _PanelTitle.Controls.Add(_LabelTitle);
            _PanelTitle.Dock = DockStyle.Top;
            _PanelTitle.ForeColor = SystemColors.ControlText;
            _PanelTitle.Location = new Point(0, 0);
            _PanelTitle.Name = "_PanelTitle";
            _PanelTitle.Size = new Size(683, 40);
            _PanelTitle.TabIndex = 0;
            // 
            // _LabelTitle
            // 
            _LabelTitle.BackColor = Color.FromArgb(231, 76, 60);
            _LabelTitle.Dock = DockStyle.Fill;
            _LabelTitle.Font = new Font("D2Coding", 14F, FontStyle.Bold);
            _LabelTitle.ForeColor = Color.Black;
            _LabelTitle.Location = new Point(0, 0);
            _LabelTitle.Name = "_LabelTitle";
            _LabelTitle.Size = new Size(683, 40);
            _LabelTitle.TabIndex = 0;
            _LabelTitle.Text = "ALARM";
            _LabelTitle.TextAlign = ContentAlignment.MiddleCenter;
            _LabelTitle.ThemeStyle = UI.Controls.ThemeStyle.Danger_Red;
            _LabelTitle.TooltipText = null;
            // 
            // labelMessage
            // 
            labelMessage.BorderStyle = BorderStyle.FixedSingle;
            labelMessage.Dock = DockStyle.Top;
            labelMessage.Font = new Font("D2Coding", 12F);
            labelMessage.Location = new Point(0, 0);
            labelMessage.Name = "labelMessage";
            labelMessage.Padding = new Padding(10);
            labelMessage.Size = new Size(683, 64);
            labelMessage.TabIndex = 1;
            labelMessage.Text = "Alarm Message Here...";
            labelMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tableLayoutPanel1.Controls.Add(_ButtonSilence, 0, 0);
            tableLayoutPanel1.Controls.Add(_ButtonReset, 1, 0);
            tableLayoutPanel1.Controls.Add(_ButtonClose, 2, 0);
            tableLayoutPanel1.Dock = DockStyle.Bottom;
            tableLayoutPanel1.Location = new Point(0, 238);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(683, 50);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // _ButtonSilence
            // 
            _ButtonSilence.BackColor = Color.FromArgb(241, 196, 15);
            _ButtonSilence.Dock = DockStyle.Fill;
            _ButtonSilence.Font = new Font("D2Coding", 12F);
            _ButtonSilence.ForeColor = Color.Black;
            _ButtonSilence.Location = new Point(3, 3);
            _ButtonSilence.Name = "_ButtonSilence";
            _ButtonSilence.Size = new Size(221, 44);
            _ButtonSilence.TabIndex = 0;
            _ButtonSilence.Text = "부저 끄기";
            _ButtonSilence.ThemeStyle = UI.Controls.ThemeStyle.Warning_Yellow;
            _ButtonSilence.TooltipText = null;
            _ButtonSilence.UseVisualStyleBackColor = false;
            // 
            // _ButtonReset
            // 
            _ButtonReset.BackColor = Color.FromArgb(48, 63, 159);
            _ButtonReset.Dock = DockStyle.Fill;
            _ButtonReset.Font = new Font("D2Coding", 12F);
            _ButtonReset.ForeColor = Color.White;
            _ButtonReset.Location = new Point(230, 3);
            _ButtonReset.Name = "_ButtonReset";
            _ButtonReset.Size = new Size(221, 44);
            _ButtonReset.TabIndex = 1;
            _ButtonReset.Text = "초기화 (Reset)";
            _ButtonReset.TooltipText = null;
            _ButtonReset.UseVisualStyleBackColor = false;
            // 
            // _ButtonClose
            // 
            _ButtonClose.BackColor = Color.FromArgb(149, 165, 166);
            _ButtonClose.Dock = DockStyle.Fill;
            _ButtonClose.Font = new Font("D2Coding", 12F);
            _ButtonClose.ForeColor = Color.White;
            _ButtonClose.Location = new Point(457, 3);
            _ButtonClose.Name = "_ButtonClose";
            _ButtonClose.Size = new Size(223, 44);
            _ButtonClose.TabIndex = 2;
            _ButtonClose.Text = "닫기";
            _ButtonClose.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _ButtonClose.TooltipText = null;
            _ButtonClose.UseVisualStyleBackColor = false;
            // 
            // _Panel1
            // 
            _Panel1.BackColor = SystemColors.Control;
            _Panel1.Controls.Add(label2);
            _Panel1.Controls.Add(label1);
            _Panel1.Controls.Add(labelMessage);
            _Panel1.Dock = DockStyle.Fill;
            _Panel1.ForeColor = SystemColors.ControlText;
            _Panel1.Location = new Point(0, 40);
            _Panel1.Name = "_Panel1";
            _Panel1.Size = new Size(683, 198);
            _Panel1.TabIndex = 3;
            // 
            // label2
            // 
            label2.BorderStyle = BorderStyle.FixedSingle;
            label2.Dock = DockStyle.Top;
            label2.Font = new Font("D2Coding", 12F);
            label2.Location = new Point(0, 128);
            label2.Name = "label2";
            label2.Padding = new Padding(10);
            label2.Size = new Size(683, 64);
            label2.TabIndex = 3;
            label2.Text = "Alarm Message Here...";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("D2Coding", 12F);
            label1.Location = new Point(0, 64);
            label1.Name = "label1";
            label1.Padding = new Padding(10);
            label1.Size = new Size(683, 64);
            label1.TabIndex = 2;
            label1.Text = "Alarm Message Here...";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FormAlarmPopup
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(683, 288);
            Controls.Add(_Panel1);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(_PanelTitle);
            Name = "FormAlarmPopup";
            Text = "Alarm";
            _PanelTitle.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            _Panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Controls._Panel _PanelTitle;
        private Controls._Label _LabelTitle;
        private System.Windows.Forms.Label labelMessage;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Controls._Button _ButtonSilence;
        private Controls._Button _ButtonReset;
        private Controls._Button _ButtonClose;
        private Controls._Panel _Panel1;
        private Label label1;
        private Label label2;
    }
}