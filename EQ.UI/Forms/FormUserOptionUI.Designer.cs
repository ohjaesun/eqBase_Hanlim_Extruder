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
            groupBox2 = new EQ.UI.Controls._GroupBox();
            _ComboBoxSelectRCP = new EQ.UI.Controls._ComboBox();
            groupBox1 = new EQ.UI.Controls._GroupBox();
            _RadioButtonCelsius = new EQ.UI.Controls._RadioButton();
            _RadioButton2 = new EQ.UI.Controls._RadioButton();
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
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Location = new Point(4, 27);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(642, 359);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "General";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.BackColor = SystemColors.Control;
            groupBox2.Controls.Add(_ComboBoxSelectRCP);
            groupBox2.Font = new Font("D2Coding", 12F);
            groupBox2.ForeColor = SystemColors.ControlText;
            groupBox2.Location = new Point(214, 14);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 90);
            groupBox2.TabIndex = 4;
            groupBox2.TabStop = false;
            groupBox2.Text = "Select Recpie";
            groupBox2.ThemeStyle = UI.Controls.ThemeStyle.Default;
            // 
            // _ComboBoxSelectRCP
            // 
            _ComboBoxSelectRCP.BackColor = SystemColors.Control;
            _ComboBoxSelectRCP.DrawMode = DrawMode.OwnerDrawFixed;
            _ComboBoxSelectRCP.DropDownStyle = ComboBoxStyle.DropDownList;
            _ComboBoxSelectRCP.Font = new Font("D2Coding", 12F);
            _ComboBoxSelectRCP.ForeColor = SystemColors.ControlText;
            _ComboBoxSelectRCP.FormattingEnabled = true;
            _ComboBoxSelectRCP.Location = new Point(6, 25);
            _ComboBoxSelectRCP.Name = "_ComboBoxSelectRCP";
            _ComboBoxSelectRCP.Size = new Size(188, 27);
            _ComboBoxSelectRCP.TabIndex = 2;
            _ComboBoxSelectRCP.ThemeStyle = UI.Controls.ThemeStyle.Default;
            _ComboBoxSelectRCP.TooltipText = null;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = SystemColors.Control;
            groupBox1.Controls.Add(_RadioButtonCelsius);
            groupBox1.Controls.Add(_RadioButton2);
            groupBox1.Font = new Font("D2Coding", 12F);
            groupBox1.ForeColor = SystemColors.ControlText;
            groupBox1.Location = new Point(8, 14);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(200, 90);
            groupBox1.TabIndex = 4;
            groupBox1.TabStop = false;
            groupBox1.Text = "Temperature Unit";
            groupBox1.ThemeStyle = UI.Controls.ThemeStyle.Default;
            // 
            // _RadioButtonCelsius
            // 
            _RadioButtonCelsius.AutoSize = true;
            _RadioButtonCelsius.BackColor = SystemColors.Control;
            _RadioButtonCelsius.Checked = true;
            _RadioButtonCelsius.Font = new Font("D2Coding", 12F);
            _RadioButtonCelsius.ForeColor = SystemColors.ControlText;
            _RadioButtonCelsius.Location = new Point(6, 20);
            _RadioButtonCelsius.Name = "_RadioButtonCelsius";
            _RadioButtonCelsius.Size = new Size(82, 22);
            _RadioButtonCelsius.TabIndex = 3;
            _RadioButtonCelsius.TabStop = true;
            _RadioButtonCelsius.Text = "Celsius";
            _RadioButtonCelsius.ThemeStyle = UI.Controls.ThemeStyle.Default;
            _RadioButtonCelsius.UseVisualStyleBackColor = false;
            // 
            // _RadioButton2
            // 
            _RadioButton2.AutoSize = true;
            _RadioButton2.BackColor = SystemColors.Control;
            _RadioButton2.Font = new Font("D2Coding", 12F);
            _RadioButton2.ForeColor = SystemColors.ControlText;
            _RadioButton2.Location = new Point(6, 54);
            _RadioButton2.Name = "_RadioButton2";
            _RadioButton2.Size = new Size(106, 22);
            _RadioButton2.TabIndex = 3;
            _RadioButton2.Text = "Fahrenheit";
            _RadioButton2.ThemeStyle = UI.Controls.ThemeStyle.Default;
            _RadioButton2.UseVisualStyleBackColor = false;
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
            _ButtonSave.BackColor = Color.FromArgb(48, 63, 159);
            _ButtonSave.Dock = DockStyle.Right;
            _ButtonSave.Font = new Font("D2Coding", 12F);
            _ButtonSave.ForeColor = Color.White;
            _ButtonSave.Location = new Point(700, 0);
            _ButtonSave.Name = "_ButtonSave";
            _ButtonSave.Size = new Size(100, 60);
            _ButtonSave.TabIndex = 1;
            _ButtonSave.Text = "SAVE";
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
            groupBox2.ResumeLayout(false);
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
        private EQ.UI.Controls._GroupBox groupBox2;
        private EQ.UI.Controls._GroupBox groupBox1;
        private EQ.UI.Controls._RadioButton _RadioButtonCelsius;
        private EQ.UI.Controls._RadioButton _RadioButton2;
        private EQ.UI.Controls._ComboBox _ComboBoxSelectRCP;
        private System.Windows.Forms.Panel panelTop;
        private EQ.UI.Controls._Button _ButtonSave;
        private EQ.UI.Controls._Label _LabelTitle;
    }
}