// EQ.UI/UserViews/GVisionView.Designer.cs
namespace EQ.UI.UserViews
{
    partial class GVision_View
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

        #region Component Designer generated code
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            comboBox1 = new ComboBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            propertyGrid1 = new PropertyGrid();
            propertyGrid2 = new PropertyGrid();
            panel1 = new Panel();
            _Button1 = new EQ.UI.Controls._Button();
            comboBox2 = new ComboBox();
            panel2 = new Panel();
            comboBox3 = new ComboBox();
            panel3 = new Panel();
            _Label2 = new EQ.UI.Controls._Label();
            _Label1 = new EQ.UI.Controls._Label();
            timer1 = new System.Windows.Forms.Timer(components);
            richTextBox1 = new RichTextBox();
            _PanelMain.SuspendLayout();
            _Panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // _LabelTitle
            // 
            _LabelTitle.Size = new Size(603, 59);
            // 
            // _ButtonSave
            // 
            _ButtonSave.Location = new Point(603, 0);
            // 
            // _PanelMain
            // 
            _PanelMain.Controls.Add(richTextBox1);
            _PanelMain.Controls.Add(tableLayoutPanel1);
            _PanelMain.Size = new Size(703, 580);
            // 
            // _Panel1
            // 
            _Panel1.Size = new Size(703, 59);
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(3, 3);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(194, 26);
            comboBox1.TabIndex = 0;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(comboBox1, 0, 0);
            tableLayoutPanel1.Controls.Add(propertyGrid1, 0, 2);
            tableLayoutPanel1.Controls.Add(propertyGrid2, 1, 2);
            tableLayoutPanel1.Controls.Add(panel1, 0, 1);
            tableLayoutPanel1.Controls.Add(panel2, 1, 1);
            tableLayoutPanel1.Controls.Add(panel3, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Top;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.Size = new Size(703, 284);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // propertyGrid1
            // 
            propertyGrid1.Dock = DockStyle.Fill;
            propertyGrid1.HelpVisible = false;
            propertyGrid1.Location = new Point(3, 63);
            propertyGrid1.Name = "propertyGrid1";
            propertyGrid1.PropertySort = PropertySort.NoSort;
            propertyGrid1.Size = new Size(345, 218);
            propertyGrid1.TabIndex = 2;
            propertyGrid1.ToolbarVisible = false;
            // 
            // propertyGrid2
            // 
            propertyGrid2.Dock = DockStyle.Fill;
            propertyGrid2.HelpVisible = false;
            propertyGrid2.Location = new Point(354, 63);
            propertyGrid2.Name = "propertyGrid2";
            propertyGrid2.PropertySort = PropertySort.NoSort;
            propertyGrid2.Size = new Size(346, 218);
            propertyGrid2.TabIndex = 2;
            propertyGrid2.ToolbarVisible = false;
            // 
            // panel1
            // 
            panel1.Controls.Add(_Button1);
            panel1.Controls.Add(comboBox2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(3, 33);
            panel1.Name = "panel1";
            panel1.Size = new Size(345, 24);
            panel1.TabIndex = 3;
            // 
            // _Button1
            // 
            _Button1.BackColor = Color.FromArgb(52, 73, 94);
            _Button1.Dock = DockStyle.Left;
            _Button1.Font = new Font("D2Coding", 12F);
            _Button1.ForeColor = Color.White;
            _Button1.Location = new Point(194, 0);
            _Button1.Name = "_Button1";
            _Button1.Size = new Size(75, 24);
            _Button1.TabIndex = 1;
            _Button1.Text = "Send";
            _Button1.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _Button1.TooltipText = null;
            _Button1.UseVisualStyleBackColor = false;
            _Button1.Click += _Button1_Click;
            // 
            // comboBox2
            // 
            comboBox2.Dock = DockStyle.Left;
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(0, 0);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(194, 26);
            comboBox2.TabIndex = 0;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // panel2
            // 
            panel2.Controls.Add(comboBox3);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(354, 33);
            panel2.Name = "panel2";
            panel2.Size = new Size(346, 24);
            panel2.TabIndex = 3;
            // 
            // comboBox3
            // 
            comboBox3.Dock = DockStyle.Left;
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(0, 0);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(194, 26);
            comboBox3.TabIndex = 0;
            comboBox3.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // panel3
            // 
            panel3.Controls.Add(_Label2);
            panel3.Controls.Add(_Label1);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(354, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(346, 24);
            panel3.TabIndex = 4;
            // 
            // _Label2
            // 
            _Label2.AutoSize = true;
            _Label2.BackColor = Color.FromArgb(241, 196, 15);
            _Label2.Dock = DockStyle.Left;
            _Label2.Font = new Font("D2Coding", 12F);
            _Label2.ForeColor = Color.Black;
            _Label2.Location = new Point(64, 0);
            _Label2.Name = "_Label2";
            _Label2.Padding = new Padding(10, 0, 0, 0);
            _Label2.Size = new Size(74, 18);
            _Label2.TabIndex = 2;
            _Label2.Text = "_Label2";
            _Label2.ThemeStyle = UI.Controls.ThemeStyle.Warning_Yellow;
            _Label2.TooltipText = null;
            // 
            // _Label1
            // 
            _Label1.AutoSize = true;
            _Label1.BackColor = Color.FromArgb(241, 196, 15);
            _Label1.Dock = DockStyle.Left;
            _Label1.Font = new Font("D2Coding", 12F);
            _Label1.ForeColor = Color.Black;
            _Label1.Location = new Point(0, 0);
            _Label1.Name = "_Label1";
            _Label1.Size = new Size(64, 18);
            _Label1.TabIndex = 1;
            _Label1.Text = "Connect";
            _Label1.ThemeStyle = UI.Controls.ThemeStyle.Warning_Yellow;
            _Label1.TooltipText = null;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // richTextBox1
            // 
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Location = new Point(0, 284);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(703, 296);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            // 
            // GVision_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "GVision_View";
            Size = new Size(703, 639);
            Load += GVisionView_Load;
            _PanelMain.ResumeLayout(false);
            _Panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }
        #endregion

        private ComboBox comboBox1;
        private TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Timer timer1;
        private EQ.UI.Controls._Label _Label1;
        private PropertyGrid propertyGrid1;
        private PropertyGrid propertyGrid2;
        private RichTextBox richTextBox1;
        private Panel panel1;
        private EQ.UI.Controls._Button _Button1;
        private ComboBox comboBox2;
        private Panel panel2;
        private ComboBox comboBox3;
        private Panel panel3;
        private EQ.UI.Controls._Label _Label2;
    }
}