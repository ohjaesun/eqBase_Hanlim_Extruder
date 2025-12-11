namespace EQ.UI.Forms
{
    partial class FormUserOptionUI
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panel2 = new EQ.UI.Controls._Panel();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            _TextBox2 = new EQ.UI.Controls._TextBox();
            _TextBox1 = new EQ.UI.Controls._TextBox();
            _ListBox1 = new EQ.UI.Controls._ListBox();
            groupBox2 = new EQ.UI.Controls._GroupBox();
            _RadioButton4 = new EQ.UI.Controls._RadioButton();
            _RadioButton3 = new EQ.UI.Controls._RadioButton();
            groupBox1 = new EQ.UI.Controls._GroupBox();
            _RadioButton1 = new EQ.UI.Controls._RadioButton();
            _RadioButton2 = new EQ.UI.Controls._RadioButton();
            _ComboBox1 = new EQ.UI.Controls._ComboBox();
            _CheckBox2 = new EQ.UI.Controls._CheckBox();
            _CheckBox1 = new EQ.UI.Controls._CheckBox();
            tabPage2 = new TabPage();
            flowLayoutPanel1 = new FlowLayoutPanel();
            panelTop = new Panel();
            _ButtonSave = new EQ.UI.Controls._Button();
            _LabelTitle = new EQ.UI.Controls._Label();
            panel2.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            panelTop.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.Control;
            panel2.Controls.Add(tabControl1);
            panel2.Controls.Add(flowLayoutPanel1);
            panel2.Dock = DockStyle.Fill;
            panel2.ForeColor = SystemColors.ControlText;
            panel2.Location = new Point(0, 60);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 390);
            panel2.TabIndex = 1;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(650, 390);
            tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(_TextBox2);
            tabPage1.Controls.Add(_TextBox1);
            tabPage1.Controls.Add(_ListBox1);
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Controls.Add(_ComboBox1);
            tabPage1.Controls.Add(_CheckBox2);
            tabPage1.Controls.Add(_CheckBox1);
            tabPage1.Location = new Point(4, 27);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(642, 359);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "General";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // _TextBox2
            // 
            _TextBox2.BackColor = Color.FromArgb(149, 165, 166);
            _TextBox2.Font = new Font("D2Coding", 12F);
            _TextBox2.ForeColor = Color.White;
            _TextBox2.Location = new Point(56, 320);
            _TextBox2.Name = "_TextBox2";
            _TextBox2.Size = new Size(100, 26);
            _TextBox2.TabIndex = 6;
            _TextBox2.Text = "Default";
            _TextBox2.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // _TextBox1
            // 
            _TextBox1.BackColor = Color.FromArgb(149, 165, 166);
            _TextBox1.Font = new Font("D2Coding", 12F);
            _TextBox1.ForeColor = Color.White;
            _TextBox1.Location = new Point(56, 280);
            _TextBox1.Name = "_TextBox1";
            _TextBox1.Size = new Size(100, 26);
            _TextBox1.TabIndex = 6;
            _TextBox1.Text = "0";
            _TextBox1.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // _ListBox1
            // 
            _ListBox1.BackColor = Color.FromArgb(149, 165, 166);
            _ListBox1.DrawMode = DrawMode.OwnerDrawFixed;
            _ListBox1.Font = new Font("D2Coding", 12F);
            _ListBox1.ForeColor = Color.White;
            _ListBox1.FormattingEnabled = true;
            _ListBox1.Items.AddRange(new object[] { "Option A", "Option B", "Option C", "333" });
            _ListBox1.Location = new Point(495, 20);
            _ListBox1.Name = "_ListBox1";
            _ListBox1.Size = new Size(120, 84);
            _ListBox1.TabIndex = 5;
            _ListBox1.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.FromArgb(155, 89, 182);
            groupBox2.Controls.Add(_RadioButton4);
            groupBox2.Controls.Add(_RadioButton3);
            groupBox2.Font = new Font("D2Coding", 12F);
            groupBox2.ForeColor = Color.Black;
            groupBox2.Location = new Point(262, 150);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 100);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Mode Select";
            groupBox2.ThemeStyle = UI.Controls.ThemeStyle.Highlight_DeepYellow;
            // 
            // _RadioButton4
            // 
            _RadioButton4.AutoSize = true;
            _RadioButton4.BackColor = Color.FromArgb(52, 152, 219);
            _RadioButton4.Font = new Font("D2Coding", 12F);
            _RadioButton4.ForeColor = Color.Black;
            _RadioButton4.Location = new Point(6, 48);
            _RadioButton4.Name = "_RadioButton4";
            _RadioButton4.Size = new Size(74, 22);
            _RadioButton4.TabIndex = 3;
            _RadioButton4.Text = "Mode B";
            _RadioButton4.ThemeStyle = UI.Controls.ThemeStyle.Info_Sky;
            _RadioButton4.UseVisualStyleBackColor = false;
            // 
            // _RadioButton3
            // 
            _RadioButton3.AutoSize = true;
            _RadioButton3.BackColor = Color.FromArgb(52, 152, 219);
            _RadioButton3.Font = new Font("D2Coding", 12F);
            _RadioButton3.ForeColor = Color.Black;
            _RadioButton3.Location = new Point(6, 20);
            _RadioButton3.Name = "_RadioButton3";
            _RadioButton3.Size = new Size(74, 22);
            _RadioButton3.TabIndex = 3;
            _RadioButton3.Text = "Mode A";
            _RadioButton3.ThemeStyle = UI.Controls.ThemeStyle.Info_Sky;
            _RadioButton3.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.FromArgb(155, 89, 182);
            groupBox1.Controls.Add(_RadioButton1);
            groupBox1.Controls.Add(_RadioButton2);
            groupBox1.Font = new Font("D2Coding", 12F);
            groupBox1.ForeColor = Color.Black;
            groupBox1.Location = new Point(56, 150);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 100);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Type Select";
            groupBox1.ThemeStyle = UI.Controls.ThemeStyle.Highlight_DeepYellow;
            // 
            // _RadioButton1
            // 
            _RadioButton1.AutoSize = true;
            _RadioButton1.BackColor = Color.FromArgb(52, 152, 219);
            _RadioButton1.Font = new Font("D2Coding", 12F);
            _RadioButton1.ForeColor = Color.Black;
            _RadioButton1.Location = new Point(6, 20);
            _RadioButton1.Name = "_RadioButton1";
            _RadioButton1.Size = new Size(74, 22);
            _RadioButton1.TabIndex = 3;
            _RadioButton1.Text = "Type 1";
            _RadioButton1.ThemeStyle = UI.Controls.ThemeStyle.Info_Sky;
            _RadioButton1.UseVisualStyleBackColor = false;
            // 
            // _RadioButton2
            // 
            _RadioButton2.AutoSize = true;
            _RadioButton2.BackColor = Color.FromArgb(52, 152, 219);
            _RadioButton2.Font = new Font("D2Coding", 12F);
            _RadioButton2.ForeColor = Color.Black;
            _RadioButton2.Location = new Point(6, 48);
            _RadioButton2.Name = "_RadioButton2";
            _RadioButton2.Size = new Size(74, 22);
            _RadioButton2.TabIndex = 3;
            _RadioButton2.Text = "Type 2";
            _RadioButton2.ThemeStyle = UI.Controls.ThemeStyle.Info_Sky;
            _RadioButton2.UseVisualStyleBackColor = false;
            // 
            // _ComboBox1
            // 
            _ComboBox1.BackColor = Color.FromArgb(155, 89, 182);
            _ComboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            _ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            _ComboBox1.Font = new Font("D2Coding", 12F);
            _ComboBox1.ForeColor = Color.Black;
            _ComboBox1.FormattingEnabled = true;
            _ComboBox1.Items.AddRange(new object[] { "Item 1", "Item 2", "Item 3" });
            _ComboBox1.Location = new Point(240, 60);
            _ComboBox1.Name = "_ComboBox1";
            _ComboBox1.Size = new Size(121, 27);
            _ComboBox1.TabIndex = 2;
            _ComboBox1.ThemeStyle = UI.Controls.ThemeStyle.Highlight_DeepYellow;
            _ComboBox1.TooltipText = null;
            // 
            // _CheckBox2
            // 
            _CheckBox2.AutoSize = true;
            _CheckBox2.BackColor = Color.FromArgb(52, 152, 219);
            _CheckBox2.Font = new Font("D2Coding", 12F);
            _CheckBox2.ForeColor = Color.Black;
            _CheckBox2.Location = new Point(56, 100);
            _CheckBox2.Name = "_CheckBox2";
            _CheckBox2.Size = new Size(91, 22);
            _CheckBox2.TabIndex = 1;
            _CheckBox2.Text = "Option 2";
            _CheckBox2.ThemeStyle = UI.Controls.ThemeStyle.Info_Sky;
            _CheckBox2.UseVisualStyleBackColor = false;
            // 
            // _CheckBox1
            // 
            _CheckBox1.AutoSize = true;
            _CheckBox1.BackColor = Color.FromArgb(52, 152, 219);
            _CheckBox1.Font = new Font("D2Coding", 12F);
            _CheckBox1.ForeColor = Color.Black;
            _CheckBox1.Location = new Point(56, 60);
            _CheckBox1.Name = "_CheckBox1";
            _CheckBox1.Size = new Size(91, 22);
            _CheckBox1.TabIndex = 1;
            _CheckBox1.Text = "Option 1";
            _CheckBox1.ThemeStyle = UI.Controls.ThemeStyle.Info_Sky;
            _CheckBox1.UseVisualStyleBackColor = false;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 27);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(642, 359);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Advanced";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Dock = DockStyle.Right;
            flowLayoutPanel1.Location = new Point(650, 0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(150, 390);
            flowLayoutPanel1.TabIndex = 6;
            // 
            // panelTop
            // 
            panelTop.Controls.Add(_ButtonSave);
            panelTop.Controls.Add(_LabelTitle);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(800, 60);
            panelTop.TabIndex = 2;
            // 
            // _ButtonSave
            // 
            _ButtonSave.BackColor = Color.FromArgb(52, 73, 94);
            _ButtonSave.Dock = DockStyle.Right;
            _ButtonSave.Font = new Font("D2Coding", 12F);
            _ButtonSave.ForeColor = Color.White;
            _ButtonSave.Location = new Point(700, 0);
            _ButtonSave.Name = "_ButtonSave";
            _ButtonSave.Size = new Size(100, 60);
            _ButtonSave.TabIndex = 1;
            _ButtonSave.Text = "SAVE";
            _ButtonSave.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _ButtonSave.TooltipText = null;
            _ButtonSave.UseVisualStyleBackColor = false;
            _ButtonSave.Click += _ButtonSave_Click;
            // 
            // _LabelTitle
            // 
            _LabelTitle.BackColor = Color.FromArgb(149, 165, 166);
            _LabelTitle.Dock = DockStyle.Fill;
            _LabelTitle.Font = new Font("D2Coding", 12F);
            _LabelTitle.ForeColor = Color.White;
            _LabelTitle.Location = new Point(0, 0);
            _LabelTitle.Name = "_LabelTitle";
            _LabelTitle.Size = new Size(800, 60);
            _LabelTitle.TabIndex = 0;
            _LabelTitle.Text = "User Options (UI Controls)";
            _LabelTitle.TextAlign = ContentAlignment.MiddleCenter;
            _LabelTitle.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _LabelTitle.TooltipText = null;
            // 
            // FormUserOptionUI
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel2);
            Controls.Add(panelTop);
            Name = "FormUserOptionUI";
            Text = "User Options UI";
            Load += FormUserOptionUI_Load;
            panel2.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panelTop.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private EQ.UI.Controls._Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private EQ.UI.Controls._TextBox _TextBox2;
        private EQ.UI.Controls._TextBox _TextBox1;
        private EQ.UI.Controls._ListBox _ListBox1;
        private EQ.UI.Controls._GroupBox groupBox2;
        private EQ.UI.Controls._RadioButton _RadioButton4;
        private EQ.UI.Controls._RadioButton _RadioButton3;
        private EQ.UI.Controls._GroupBox groupBox1;
        private EQ.UI.Controls._RadioButton _RadioButton1;
        private EQ.UI.Controls._RadioButton _RadioButton2;
        private EQ.UI.Controls._ComboBox _ComboBox1;
        private EQ.UI.Controls._CheckBox _CheckBox2;
        private EQ.UI.Controls._CheckBox _CheckBox1;
        private System.Windows.Forms.Panel panelTop;
        private EQ.UI.Controls._Button _ButtonSave;
        private EQ.UI.Controls._Label _LabelTitle;
    }
}