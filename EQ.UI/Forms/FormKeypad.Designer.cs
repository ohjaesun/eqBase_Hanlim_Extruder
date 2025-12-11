namespace EQ.UI.Forms
{
    partial class FormKeypad
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
            _LabelDisplay = new EQ.UI.Controls._Label();
            tableLayoutPanelMain = new TableLayoutPanel();
            _btn7 = new EQ.UI.Controls._Button();
            _btn8 = new EQ.UI.Controls._Button();
            _btn9 = new EQ.UI.Controls._Button();
            _btnBack = new EQ.UI.Controls._Button();
            _btn4 = new EQ.UI.Controls._Button();
            _btn5 = new EQ.UI.Controls._Button();
            _btn6 = new EQ.UI.Controls._Button();
            _btnClear = new EQ.UI.Controls._Button();
            _btn1 = new EQ.UI.Controls._Button();
            _btn2 = new EQ.UI.Controls._Button();
            _btn3 = new EQ.UI.Controls._Button();
            _btnSign = new EQ.UI.Controls._Button();
            _btnOk = new EQ.UI.Controls._Button();
            _btnCancel = new EQ.UI.Controls._Button();
            _btn0 = new EQ.UI.Controls._Button();
            _btnDot = new EQ.UI.Controls._Button();
            _PanelTitle.SuspendLayout();
            tableLayoutPanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // _PanelTitle
            // 
            _PanelTitle.BackColor = SystemColors.Control;
            _PanelTitle.Controls.Add(_LabelTitle);
            _PanelTitle.Dock = DockStyle.Top;
            _PanelTitle.ForeColor = SystemColors.ControlText;
            _PanelTitle.Location = new Point(0, 0);
            _PanelTitle.Margin = new Padding(3, 4, 3, 4);
            _PanelTitle.Name = "_PanelTitle";
            _PanelTitle.Size = new Size(434, 60);
            _PanelTitle.TabIndex = 0;
            // 
            // _LabelTitle
            // 
            _LabelTitle.BackColor = Color.FromArgb(149, 165, 166);
            _LabelTitle.Dock = DockStyle.Fill;
            _LabelTitle.Font = new Font("D2Coding", 12F);
            _LabelTitle.ForeColor = Color.White;
            _LabelTitle.Location = new Point(0, 0);
            _LabelTitle.Name = "_LabelTitle";
            _LabelTitle.Size = new Size(434, 60);
            _LabelTitle.TabIndex = 0;
            _LabelTitle.Text = "Keypad";
            _LabelTitle.TextAlign = ContentAlignment.MiddleCenter;
            _LabelTitle.TooltipText = null;
            // 
            // _LabelDisplay
            // 
            _LabelDisplay.BackColor = SystemColors.Control;
            _LabelDisplay.Dock = DockStyle.Top;
            _LabelDisplay.Font = new Font("D2Coding", 24F, FontStyle.Regular, GraphicsUnit.Point, 129);
            _LabelDisplay.ForeColor = SystemColors.ControlText;
            _LabelDisplay.Location = new Point(0, 60);
            _LabelDisplay.Name = "_LabelDisplay";
            _LabelDisplay.Size = new Size(434, 89);
            _LabelDisplay.TabIndex = 1;
            _LabelDisplay.Text = "0";
            _LabelDisplay.TextAlign = ContentAlignment.MiddleRight;
            _LabelDisplay.ThemeStyle = UI.Controls.ThemeStyle.Default;
            _LabelDisplay.TooltipText = null;
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.ColumnCount = 4;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tableLayoutPanelMain.Controls.Add(_btn7, 0, 0);
            tableLayoutPanelMain.Controls.Add(_btn8, 1, 0);
            tableLayoutPanelMain.Controls.Add(_btn9, 2, 0);
            tableLayoutPanelMain.Controls.Add(_btnBack, 3, 0);
            tableLayoutPanelMain.Controls.Add(_btn4, 0, 1);
            tableLayoutPanelMain.Controls.Add(_btn5, 1, 1);
            tableLayoutPanelMain.Controls.Add(_btn6, 2, 1);
            tableLayoutPanelMain.Controls.Add(_btnClear, 3, 1);
            tableLayoutPanelMain.Controls.Add(_btn1, 0, 2);
            tableLayoutPanelMain.Controls.Add(_btn2, 1, 2);
            tableLayoutPanelMain.Controls.Add(_btn3, 2, 2);
            tableLayoutPanelMain.Controls.Add(_btnSign, 3, 2);
            tableLayoutPanelMain.Controls.Add(_btnOk, 2, 3);
            tableLayoutPanelMain.Controls.Add(_btnCancel, 3, 3);
            tableLayoutPanelMain.Controls.Add(_btn0, 1, 3);
            tableLayoutPanelMain.Controls.Add(_btnDot, 0, 3);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(0, 149);
            tableLayoutPanelMain.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 4;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            tableLayoutPanelMain.Size = new Size(434, 335);
            tableLayoutPanelMain.TabIndex = 2;
            // 
            // _btn7
            // 
            _btn7.BackColor = Color.FromArgb(149, 165, 166);
            _btn7.Dock = DockStyle.Fill;
            _btn7.Font = new Font("D2Coding", 17.9999981F);
            _btn7.ForeColor = Color.White;
            _btn7.Location = new Point(3, 4);
            _btn7.Margin = new Padding(3, 4, 3, 4);
            _btn7.Name = "_btn7";
            _btn7.Size = new Size(102, 75);
            _btn7.TabIndex = 0;
            _btn7.Text = "7";
            _btn7.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _btn7.TooltipText = null;
            _btn7.UseVisualStyleBackColor = false;
            // 
            // _btn8
            // 
            _btn8.BackColor = Color.FromArgb(149, 165, 166);
            _btn8.Dock = DockStyle.Fill;
            _btn8.Font = new Font("D2Coding", 17.9999981F);
            _btn8.ForeColor = Color.White;
            _btn8.Location = new Point(111, 4);
            _btn8.Margin = new Padding(3, 4, 3, 4);
            _btn8.Name = "_btn8";
            _btn8.Size = new Size(102, 75);
            _btn8.TabIndex = 1;
            _btn8.Text = "8";
            _btn8.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _btn8.TooltipText = null;
            _btn8.UseVisualStyleBackColor = false;
            // 
            // _btn9
            // 
            _btn9.BackColor = Color.FromArgb(149, 165, 166);
            _btn9.Dock = DockStyle.Fill;
            _btn9.Font = new Font("D2Coding", 17.9999981F);
            _btn9.ForeColor = Color.White;
            _btn9.Location = new Point(219, 4);
            _btn9.Margin = new Padding(3, 4, 3, 4);
            _btn9.Name = "_btn9";
            _btn9.Size = new Size(102, 75);
            _btn9.TabIndex = 2;
            _btn9.Text = "9";
            _btn9.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _btn9.TooltipText = null;
            _btn9.UseVisualStyleBackColor = false;
            // 
            // _btnBack
            // 
            _btnBack.BackColor = Color.Black;
            _btnBack.Dock = DockStyle.Fill;
            _btnBack.Font = new Font("D2Coding", 12F);
            _btnBack.ForeColor = Color.White;
            _btnBack.Location = new Point(327, 4);
            _btnBack.Margin = new Padding(3, 4, 3, 4);
            _btnBack.Name = "_btnBack";
            _btnBack.Size = new Size(104, 75);
            _btnBack.TabIndex = 3;
            _btnBack.Text = "BS";
            _btnBack.ThemeStyle = UI.Controls.ThemeStyle.Black_White;
            _btnBack.TooltipText = null;
            _btnBack.UseVisualStyleBackColor = false;
            // 
            // _btn4
            // 
            _btn4.BackColor = Color.FromArgb(149, 165, 166);
            _btn4.Dock = DockStyle.Fill;
            _btn4.Font = new Font("D2Coding", 17.9999981F);
            _btn4.ForeColor = Color.White;
            _btn4.Location = new Point(3, 87);
            _btn4.Margin = new Padding(3, 4, 3, 4);
            _btn4.Name = "_btn4";
            _btn4.Size = new Size(102, 75);
            _btn4.TabIndex = 4;
            _btn4.Text = "4";
            _btn4.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _btn4.TooltipText = null;
            _btn4.UseVisualStyleBackColor = false;
            // 
            // _btn5
            // 
            _btn5.BackColor = Color.FromArgb(149, 165, 166);
            _btn5.Dock = DockStyle.Fill;
            _btn5.Font = new Font("D2Coding", 17.9999981F);
            _btn5.ForeColor = Color.White;
            _btn5.Location = new Point(111, 87);
            _btn5.Margin = new Padding(3, 4, 3, 4);
            _btn5.Name = "_btn5";
            _btn5.Size = new Size(102, 75);
            _btn5.TabIndex = 5;
            _btn5.Text = "5";
            _btn5.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _btn5.TooltipText = null;
            _btn5.UseVisualStyleBackColor = false;
            // 
            // _btn6
            // 
            _btn6.BackColor = Color.FromArgb(149, 165, 166);
            _btn6.Dock = DockStyle.Fill;
            _btn6.Font = new Font("D2Coding", 17.9999981F);
            _btn6.ForeColor = Color.White;
            _btn6.Location = new Point(219, 87);
            _btn6.Margin = new Padding(3, 4, 3, 4);
            _btn6.Name = "_btn6";
            _btn6.Size = new Size(102, 75);
            _btn6.TabIndex = 6;
            _btn6.Text = "6";
            _btn6.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _btn6.TooltipText = null;
            _btn6.UseVisualStyleBackColor = false;
            // 
            // _btnClear
            // 
            _btnClear.BackColor = Color.Black;
            _btnClear.Dock = DockStyle.Fill;
            _btnClear.Font = new Font("D2Coding", 12F);
            _btnClear.ForeColor = Color.White;
            _btnClear.Location = new Point(327, 87);
            _btnClear.Margin = new Padding(3, 4, 3, 4);
            _btnClear.Name = "_btnClear";
            _btnClear.Size = new Size(104, 75);
            _btnClear.TabIndex = 7;
            _btnClear.Text = "CLR";
            _btnClear.ThemeStyle = UI.Controls.ThemeStyle.Black_White;
            _btnClear.TooltipText = null;
            _btnClear.UseVisualStyleBackColor = false;
            // 
            // _btn1
            // 
            _btn1.BackColor = Color.FromArgb(149, 165, 166);
            _btn1.Dock = DockStyle.Fill;
            _btn1.Font = new Font("D2Coding", 17.9999981F);
            _btn1.ForeColor = Color.White;
            _btn1.Location = new Point(3, 170);
            _btn1.Margin = new Padding(3, 4, 3, 4);
            _btn1.Name = "_btn1";
            _btn1.Size = new Size(102, 75);
            _btn1.TabIndex = 8;
            _btn1.Text = "1";
            _btn1.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _btn1.TooltipText = null;
            _btn1.UseVisualStyleBackColor = false;
            // 
            // _btn2
            // 
            _btn2.BackColor = Color.FromArgb(149, 165, 166);
            _btn2.Dock = DockStyle.Fill;
            _btn2.Font = new Font("D2Coding", 17.9999981F);
            _btn2.ForeColor = Color.White;
            _btn2.Location = new Point(111, 170);
            _btn2.Margin = new Padding(3, 4, 3, 4);
            _btn2.Name = "_btn2";
            _btn2.Size = new Size(102, 75);
            _btn2.TabIndex = 9;
            _btn2.Text = "2";
            _btn2.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _btn2.TooltipText = null;
            _btn2.UseVisualStyleBackColor = false;
            // 
            // _btn3
            // 
            _btn3.BackColor = Color.FromArgb(149, 165, 166);
            _btn3.Dock = DockStyle.Fill;
            _btn3.Font = new Font("D2Coding", 17.9999981F);
            _btn3.ForeColor = Color.White;
            _btn3.Location = new Point(219, 170);
            _btn3.Margin = new Padding(3, 4, 3, 4);
            _btn3.Name = "_btn3";
            _btn3.Size = new Size(102, 75);
            _btn3.TabIndex = 10;
            _btn3.Text = "3";
            _btn3.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _btn3.TooltipText = null;
            _btn3.UseVisualStyleBackColor = false;
            // 
            // _btnSign
            // 
            _btnSign.BackColor = Color.Black;
            _btnSign.Dock = DockStyle.Fill;
            _btnSign.Font = new Font("D2Coding", 12F);
            _btnSign.ForeColor = Color.White;
            _btnSign.Location = new Point(327, 170);
            _btnSign.Margin = new Padding(3, 4, 3, 4);
            _btnSign.Name = "_btnSign";
            _btnSign.Size = new Size(104, 75);
            _btnSign.TabIndex = 11;
            _btnSign.Text = "+/-";
            _btnSign.ThemeStyle = UI.Controls.ThemeStyle.Black_White;
            _btnSign.TooltipText = null;
            _btnSign.UseVisualStyleBackColor = false;
            // 
            // _btnOk
            // 
            _btnOk.BackColor = Color.FromArgb(48, 63, 159);
            _btnOk.Dock = DockStyle.Fill;
            _btnOk.Font = new Font("D2Coding", 12F);
            _btnOk.ForeColor = Color.White;
            _btnOk.Location = new Point(219, 253);
            _btnOk.Margin = new Padding(3, 4, 3, 4);
            _btnOk.Name = "_btnOk";
            _btnOk.Size = new Size(102, 78);
            _btnOk.TabIndex = 14;
            _btnOk.Text = "ENTER";
            _btnOk.TooltipText = null;
            _btnOk.UseVisualStyleBackColor = false;
            // 
            // _btnCancel
            // 
            _btnCancel.BackColor = Color.FromArgb(241, 196, 15);
            _btnCancel.Dock = DockStyle.Fill;
            _btnCancel.Font = new Font("D2Coding", 12F);
            _btnCancel.ForeColor = Color.Black;
            _btnCancel.Location = new Point(327, 253);
            _btnCancel.Margin = new Padding(3, 4, 3, 4);
            _btnCancel.Name = "_btnCancel";
            _btnCancel.Size = new Size(104, 78);
            _btnCancel.TabIndex = 15;
            _btnCancel.Text = "ESC";
            _btnCancel.ThemeStyle = UI.Controls.ThemeStyle.Warning_Yellow;
            _btnCancel.TooltipText = null;
            _btnCancel.UseVisualStyleBackColor = false;
            // 
            // _btn0
            // 
            _btn0.BackColor = Color.FromArgb(149, 165, 166);
            _btn0.Dock = DockStyle.Fill;
            _btn0.Font = new Font("D2Coding", 17.9999981F);
            _btn0.ForeColor = Color.White;
            _btn0.Location = new Point(111, 253);
            _btn0.Margin = new Padding(3, 4, 3, 4);
            _btn0.Name = "_btn0";
            _btn0.Size = new Size(102, 78);
            _btn0.TabIndex = 12;
            _btn0.Text = "0";
            _btn0.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _btn0.TooltipText = null;
            _btn0.UseVisualStyleBackColor = false;
            // 
            // _btnDot
            // 
            _btnDot.BackColor = Color.FromArgb(149, 165, 166);
            _btnDot.Dock = DockStyle.Fill;
            _btnDot.Font = new Font("D2Coding", 17.9999981F);
            _btnDot.ForeColor = Color.White;
            _btnDot.Location = new Point(3, 253);
            _btnDot.Margin = new Padding(3, 4, 3, 4);
            _btnDot.Name = "_btnDot";
            _btnDot.Size = new Size(102, 78);
            _btnDot.TabIndex = 13;
            _btnDot.Text = ".";
            _btnDot.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _btnDot.TooltipText = null;
            _btnDot.UseVisualStyleBackColor = false;
            // 
            // FormKeypad
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(434, 484);
            Controls.Add(tableLayoutPanelMain);
            Controls.Add(_LabelDisplay);
            Controls.Add(_PanelTitle);
            Margin = new Padding(3, 6, 3, 6);
            Name = "FormKeypad";
            StartPosition = FormStartPosition.CenterParent;
            Text = "FormKeypad";
            _PanelTitle.ResumeLayout(false);
            tableLayoutPanelMain.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private EQ.UI.Controls._Panel _PanelTitle;
        private EQ.UI.Controls._Label _LabelTitle;
        private EQ.UI.Controls._Label _LabelDisplay;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private EQ.UI.Controls._Button _btn7;
        private EQ.UI.Controls._Button _btn8;
        private EQ.UI.Controls._Button _btn9;
        private EQ.UI.Controls._Button _btnBack;
        private EQ.UI.Controls._Button _btn4;
        private EQ.UI.Controls._Button _btn5;
        private EQ.UI.Controls._Button _btn6;
        private EQ.UI.Controls._Button _btnClear;
        private EQ.UI.Controls._Button _btn1;
        private EQ.UI.Controls._Button _btn2;
        private EQ.UI.Controls._Button _btn3;
        private EQ.UI.Controls._Button _btnSign;
        private EQ.UI.Controls._Button _btn0;
        private EQ.UI.Controls._Button _btnDot;
        private EQ.UI.Controls._Button _btnOk;
        private EQ.UI.Controls._Button _btnCancel;
    }
}
