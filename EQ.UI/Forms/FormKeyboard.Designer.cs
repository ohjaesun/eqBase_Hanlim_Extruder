namespace EQ.UI.Forms
{
    partial class FormKeyboard
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
            tableLayoutPanelKeys = new TableLayoutPanel();
            
            // Number row buttons
            _btn1 = new EQ.UI.Controls._Button();
            _btn2 = new EQ.UI.Controls._Button();
            _btn3 = new EQ.UI.Controls._Button();
            _btn4 = new EQ.UI.Controls._Button();
            _btn5 = new EQ.UI.Controls._Button();
            _btn6 = new EQ.UI.Controls._Button();
            _btn7 = new EQ.UI.Controls._Button();
            _btn8 = new EQ.UI.Controls._Button();
            _btn9 = new EQ.UI.Controls._Button();
            _btn0 = new EQ.UI.Controls._Button();
            _btnUnderscore = new EQ.UI.Controls._Button();
            _btnBack = new EQ.UI.Controls._Button();
            
            // QWERTY row buttons
            _btnQ = new EQ.UI.Controls._Button();
            _btnW = new EQ.UI.Controls._Button();
            _btnE = new EQ.UI.Controls._Button();
            _btnR = new EQ.UI.Controls._Button();
            _btnT = new EQ.UI.Controls._Button();
            _btnY = new EQ.UI.Controls._Button();
            _btnU = new EQ.UI.Controls._Button();
            _btnI = new EQ.UI.Controls._Button();
            _btnO = new EQ.UI.Controls._Button();
            _btnP = new EQ.UI.Controls._Button();
            
            // ASDF row buttons
            _btnA = new EQ.UI.Controls._Button();
            _btnS = new EQ.UI.Controls._Button();
            _btnD = new EQ.UI.Controls._Button();
            _btnF = new EQ.UI.Controls._Button();
            _btnG = new EQ.UI.Controls._Button();
            _btnH = new EQ.UI.Controls._Button();
            _btnJ = new EQ.UI.Controls._Button();
            _btnK = new EQ.UI.Controls._Button();
            _btnL = new EQ.UI.Controls._Button();
            _btnEnter = new EQ.UI.Controls._Button();
            
            // ZXCV row buttons
            _btnZ = new EQ.UI.Controls._Button();
            _btnX = new EQ.UI.Controls._Button();
            _btnC = new EQ.UI.Controls._Button();
            _btnV = new EQ.UI.Controls._Button();
            _btnB = new EQ.UI.Controls._Button();
            _btnN = new EQ.UI.Controls._Button();
            _btnM = new EQ.UI.Controls._Button();
            
            // Bottom row
            _btnCapsLock = new EQ.UI.Controls._Button();
            _btnSpace = new EQ.UI.Controls._Button();
            _btnCancel = new EQ.UI.Controls._Button();
            
            _PanelTitle.SuspendLayout();
            tableLayoutPanelKeys.SuspendLayout();
            SuspendLayout();
            
            // 
            // _PanelTitle
            // 
            _PanelTitle.BackColor = System.Drawing.SystemColors.Control;
            _PanelTitle.Controls.Add(_LabelTitle);
            _PanelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            _PanelTitle.Location = new System.Drawing.Point(0, 0);
            _PanelTitle.Name = "_PanelTitle";
            _PanelTitle.Size = new System.Drawing.Size(720, 50);
            _PanelTitle.TabIndex = 0;
            // 
            // _LabelTitle
            // 
            _LabelTitle.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            _LabelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            _LabelTitle.Font = new System.Drawing.Font("D2Coding", 12F);
            _LabelTitle.ForeColor = System.Drawing.Color.White;
            _LabelTitle.Location = new System.Drawing.Point(0, 0);
            _LabelTitle.Name = "_LabelTitle";
            _LabelTitle.Size = new System.Drawing.Size(720, 50);
            _LabelTitle.TabIndex = 0;
            _LabelTitle.Text = "Keyboard";
            _LabelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _LabelDisplay
            // 
            _LabelDisplay.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            _LabelDisplay.Dock = System.Windows.Forms.DockStyle.Top;
            _LabelDisplay.Font = new System.Drawing.Font("D2Coding", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 129);
            _LabelDisplay.ForeColor = System.Drawing.Color.White;
            _LabelDisplay.Location = new System.Drawing.Point(0, 50);
            _LabelDisplay.Name = "_LabelDisplay";
            _LabelDisplay.Size = new System.Drawing.Size(720, 60);
            _LabelDisplay.TabIndex = 1;
            _LabelDisplay.Text = "";
            _LabelDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            _LabelDisplay.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            // 
            // tableLayoutPanelKeys
            // 
            tableLayoutPanelKeys.ColumnCount = 12;
            tableLayoutPanelKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.33F));
            tableLayoutPanelKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.33F));
            tableLayoutPanelKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.33F));
            tableLayoutPanelKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.33F));
            tableLayoutPanelKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.33F));
            tableLayoutPanelKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.33F));
            tableLayoutPanelKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.33F));
            tableLayoutPanelKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.33F));
            tableLayoutPanelKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.33F));
            tableLayoutPanelKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.33F));
            tableLayoutPanelKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.33F));
            tableLayoutPanelKeys.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8.37F));
            
            // Row 0: Numbers 1-0, _, BACK
            tableLayoutPanelKeys.Controls.Add(_btn1, 0, 0);
            tableLayoutPanelKeys.Controls.Add(_btn2, 1, 0);
            tableLayoutPanelKeys.Controls.Add(_btn3, 2, 0);
            tableLayoutPanelKeys.Controls.Add(_btn4, 3, 0);
            tableLayoutPanelKeys.Controls.Add(_btn5, 4, 0);
            tableLayoutPanelKeys.Controls.Add(_btn6, 5, 0);
            tableLayoutPanelKeys.Controls.Add(_btn7, 6, 0);
            tableLayoutPanelKeys.Controls.Add(_btn8, 7, 0);
            tableLayoutPanelKeys.Controls.Add(_btn9, 8, 0);
            tableLayoutPanelKeys.Controls.Add(_btn0, 9, 0);
            tableLayoutPanelKeys.Controls.Add(_btnUnderscore, 10, 0);
            tableLayoutPanelKeys.Controls.Add(_btnBack, 11, 0);
            
            // Row 1: QWERTYUIOP (centered)
            tableLayoutPanelKeys.Controls.Add(_btnQ, 1, 1);
            tableLayoutPanelKeys.Controls.Add(_btnW, 2, 1);
            tableLayoutPanelKeys.Controls.Add(_btnE, 3, 1);
            tableLayoutPanelKeys.Controls.Add(_btnR, 4, 1);
            tableLayoutPanelKeys.Controls.Add(_btnT, 5, 1);
            tableLayoutPanelKeys.Controls.Add(_btnY, 6, 1);
            tableLayoutPanelKeys.Controls.Add(_btnU, 7, 1);
            tableLayoutPanelKeys.Controls.Add(_btnI, 8, 1);
            tableLayoutPanelKeys.Controls.Add(_btnO, 9, 1);
            tableLayoutPanelKeys.Controls.Add(_btnP, 10, 1);
            
            // Row 2: ASDFGHJKL + ENTER (spans 2 rows)
            tableLayoutPanelKeys.Controls.Add(_btnA, 1, 2);
            tableLayoutPanelKeys.Controls.Add(_btnS, 2, 2);
            tableLayoutPanelKeys.Controls.Add(_btnD, 3, 2);
            tableLayoutPanelKeys.Controls.Add(_btnF, 4, 2);
            tableLayoutPanelKeys.Controls.Add(_btnG, 5, 2);
            tableLayoutPanelKeys.Controls.Add(_btnH, 6, 2);
            tableLayoutPanelKeys.Controls.Add(_btnJ, 7, 2);
            tableLayoutPanelKeys.Controls.Add(_btnK, 8, 2);
            tableLayoutPanelKeys.Controls.Add(_btnL, 9, 2);
            tableLayoutPanelKeys.Controls.Add(_btnEnter, 10, 2);
            tableLayoutPanelKeys.SetColumnSpan(_btnEnter, 2);
            tableLayoutPanelKeys.SetRowSpan(_btnEnter, 2);
            
            // Row 3: ZXCVBNM
            tableLayoutPanelKeys.Controls.Add(_btnZ, 2, 3);
            tableLayoutPanelKeys.Controls.Add(_btnX, 3, 3);
            tableLayoutPanelKeys.Controls.Add(_btnC, 4, 3);
            tableLayoutPanelKeys.Controls.Add(_btnV, 5, 3);
            tableLayoutPanelKeys.Controls.Add(_btnB, 6, 3);
            tableLayoutPanelKeys.Controls.Add(_btnN, 7, 3);
            tableLayoutPanelKeys.Controls.Add(_btnM, 8, 3);
            
            // Row 4: CAPS, SPACE, ESC
            tableLayoutPanelKeys.Controls.Add(_btnCapsLock, 0, 4);
            tableLayoutPanelKeys.SetColumnSpan(_btnCapsLock, 2);
            tableLayoutPanelKeys.Controls.Add(_btnSpace, 2, 4);
            tableLayoutPanelKeys.SetColumnSpan(_btnSpace, 8);
            tableLayoutPanelKeys.Controls.Add(_btnCancel, 10, 4);
            tableLayoutPanelKeys.SetColumnSpan(_btnCancel, 2);
            
            tableLayoutPanelKeys.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanelKeys.Location = new System.Drawing.Point(0, 110);
            tableLayoutPanelKeys.Name = "tableLayoutPanelKeys";
            tableLayoutPanelKeys.RowCount = 5;
            tableLayoutPanelKeys.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanelKeys.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanelKeys.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanelKeys.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanelKeys.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanelKeys.Size = new System.Drawing.Size(720, 340);
            tableLayoutPanelKeys.TabIndex = 2;
            
            // Configure all character buttons
            ConfigureCharButton(_btn1, "1");
            ConfigureCharButton(_btn2, "2");
            ConfigureCharButton(_btn3, "3");
            ConfigureCharButton(_btn4, "4");
            ConfigureCharButton(_btn5, "5");
            ConfigureCharButton(_btn6, "6");
            ConfigureCharButton(_btn7, "7");
            ConfigureCharButton(_btn8, "8");
            ConfigureCharButton(_btn9, "9");
            ConfigureCharButton(_btn0, "0");
            
            ConfigureCharButton(_btnQ, "Q");
            ConfigureCharButton(_btnW, "W");
            ConfigureCharButton(_btnE, "E");
            ConfigureCharButton(_btnR, "R");
            ConfigureCharButton(_btnT, "T");
            ConfigureCharButton(_btnY, "Y");
            ConfigureCharButton(_btnU, "U");
            ConfigureCharButton(_btnI, "I");
            ConfigureCharButton(_btnO, "O");
            ConfigureCharButton(_btnP, "P");
            
            ConfigureCharButton(_btnA, "A");
            ConfigureCharButton(_btnS, "S");
            ConfigureCharButton(_btnD, "D");
            ConfigureCharButton(_btnF, "F");
            ConfigureCharButton(_btnG, "G");
            ConfigureCharButton(_btnH, "H");
            ConfigureCharButton(_btnJ, "J");
            ConfigureCharButton(_btnK, "K");
            ConfigureCharButton(_btnL, "L");
            
            ConfigureCharButton(_btnZ, "Z");
            ConfigureCharButton(_btnX, "X");
            ConfigureCharButton(_btnC, "C");
            ConfigureCharButton(_btnV, "V");
            ConfigureCharButton(_btnB, "B");
            ConfigureCharButton(_btnN, "N");
            ConfigureCharButton(_btnM, "M");
            
            // 
            // _btnUnderscore
            // 
            _btnUnderscore.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            _btnUnderscore.Dock = System.Windows.Forms.DockStyle.Fill;
            _btnUnderscore.Font = new System.Drawing.Font("D2Coding", 14F);
            _btnUnderscore.ForeColor = System.Drawing.Color.White;
            _btnUnderscore.Margin = new System.Windows.Forms.Padding(2);
            _btnUnderscore.Name = "_btnUnderscore";
            _btnUnderscore.Text = "_";
            _btnUnderscore.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // _btnBack
            // 
            _btnBack.BackColor = System.Drawing.Color.FromArgb(44, 62, 80);
            _btnBack.Dock = System.Windows.Forms.DockStyle.Fill;
            _btnBack.Font = new System.Drawing.Font("D2Coding", 11F);
            _btnBack.ForeColor = System.Drawing.Color.White;
            _btnBack.Margin = new System.Windows.Forms.Padding(2);
            _btnBack.Name = "_btnBack";
            _btnBack.Text = "BACK";
            _btnBack.ThemeStyle = UI.Controls.ThemeStyle.Black_White;
            // 
            // _btnEnter
            // 
            _btnEnter.BackColor = System.Drawing.Color.FromArgb(48, 63, 159);
            _btnEnter.Dock = System.Windows.Forms.DockStyle.Fill;
            _btnEnter.Font = new System.Drawing.Font("D2Coding", 12F);
            _btnEnter.ForeColor = System.Drawing.Color.White;
            _btnEnter.Margin = new System.Windows.Forms.Padding(2);
            _btnEnter.Name = "_btnEnter";
            _btnEnter.Text = "ENTER";
            _btnEnter.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            // 
            // _btnCapsLock
            // 
            _btnCapsLock.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            _btnCapsLock.Dock = System.Windows.Forms.DockStyle.Fill;
            _btnCapsLock.Font = new System.Drawing.Font("D2Coding", 10F);
            _btnCapsLock.ForeColor = System.Drawing.Color.White;
            _btnCapsLock.Margin = new System.Windows.Forms.Padding(2);
            _btnCapsLock.Name = "_btnCapsLock";
            _btnCapsLock.Text = "CAPS";
            _btnCapsLock.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // _btnSpace
            // 
            _btnSpace.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            _btnSpace.Dock = System.Windows.Forms.DockStyle.Fill;
            _btnSpace.Font = new System.Drawing.Font("D2Coding", 12F);
            _btnSpace.ForeColor = System.Drawing.Color.White;
            _btnSpace.Margin = new System.Windows.Forms.Padding(2);
            _btnSpace.Name = "_btnSpace";
            _btnSpace.Text = "SPACE";
            _btnSpace.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // _btnCancel
            // 
            _btnCancel.BackColor = System.Drawing.Color.FromArgb(241, 196, 15);
            _btnCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            _btnCancel.Font = new System.Drawing.Font("D2Coding", 11F);
            _btnCancel.ForeColor = System.Drawing.Color.Black;
            _btnCancel.Margin = new System.Windows.Forms.Padding(2);
            _btnCancel.Name = "_btnCancel";
            _btnCancel.Text = "ESC";
            _btnCancel.ThemeStyle = UI.Controls.ThemeStyle.Warning_Yellow;
            // 
            // FormKeyboard
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(720, 450);
            Controls.Add(tableLayoutPanelKeys);
            Controls.Add(_LabelDisplay);
            Controls.Add(_PanelTitle);
            Name = "FormKeyboard";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "FormKeyboard";
            _PanelTitle.ResumeLayout(false);
            tableLayoutPanelKeys.ResumeLayout(false);
            ResumeLayout(false);
        }
        
        private void ConfigureCharButton(EQ.UI.Controls._Button btn, string text)
        {
            btn.BackColor = System.Drawing.Color.FromArgb(52, 73, 94);
            btn.Dock = System.Windows.Forms.DockStyle.Fill;
            btn.Font = new System.Drawing.Font("D2Coding", 14F);
            btn.ForeColor = System.Drawing.Color.White;
            btn.Margin = new System.Windows.Forms.Padding(2);
            btn.Name = "_btn" + text;
            btn.Text = text;
            btn.Tag = "char";
            btn.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
        }

        #endregion

        private EQ.UI.Controls._Panel _PanelTitle;
        private EQ.UI.Controls._Label _LabelTitle;
        private EQ.UI.Controls._Label _LabelDisplay;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelKeys;
        
        // Number row
        private EQ.UI.Controls._Button _btn1;
        private EQ.UI.Controls._Button _btn2;
        private EQ.UI.Controls._Button _btn3;
        private EQ.UI.Controls._Button _btn4;
        private EQ.UI.Controls._Button _btn5;
        private EQ.UI.Controls._Button _btn6;
        private EQ.UI.Controls._Button _btn7;
        private EQ.UI.Controls._Button _btn8;
        private EQ.UI.Controls._Button _btn9;
        private EQ.UI.Controls._Button _btn0;
        private EQ.UI.Controls._Button _btnUnderscore;
        private EQ.UI.Controls._Button _btnBack;
        
        // QWERTY row
        private EQ.UI.Controls._Button _btnQ;
        private EQ.UI.Controls._Button _btnW;
        private EQ.UI.Controls._Button _btnE;
        private EQ.UI.Controls._Button _btnR;
        private EQ.UI.Controls._Button _btnT;
        private EQ.UI.Controls._Button _btnY;
        private EQ.UI.Controls._Button _btnU;
        private EQ.UI.Controls._Button _btnI;
        private EQ.UI.Controls._Button _btnO;
        private EQ.UI.Controls._Button _btnP;
        
        // ASDF row
        private EQ.UI.Controls._Button _btnA;
        private EQ.UI.Controls._Button _btnS;
        private EQ.UI.Controls._Button _btnD;
        private EQ.UI.Controls._Button _btnF;
        private EQ.UI.Controls._Button _btnG;
        private EQ.UI.Controls._Button _btnH;
        private EQ.UI.Controls._Button _btnJ;
        private EQ.UI.Controls._Button _btnK;
        private EQ.UI.Controls._Button _btnL;
        private EQ.UI.Controls._Button _btnEnter;
        
        // ZXCV row
        private EQ.UI.Controls._Button _btnZ;
        private EQ.UI.Controls._Button _btnX;
        private EQ.UI.Controls._Button _btnC;
        private EQ.UI.Controls._Button _btnV;
        private EQ.UI.Controls._Button _btnB;
        private EQ.UI.Controls._Button _btnN;
        private EQ.UI.Controls._Button _btnM;
        
        // Bottom row
        private EQ.UI.Controls._Button _btnCapsLock;
        private EQ.UI.Controls._Button _btnSpace;
        private EQ.UI.Controls._Button _btnCancel;
    }
}
