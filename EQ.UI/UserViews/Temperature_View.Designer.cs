namespace EQ.UI.UserViews
{
    partial class Temperature_View
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            _GridTemp = new EQ.UI.Controls._DataGridView();
            _updateTimer = new System.Windows.Forms.Timer(components);
            _PanelMain.SuspendLayout();
            _Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_GridTemp).BeginInit();
            SuspendLayout();
            // 
            // _LabelTitle
            // 
            _LabelTitle.Size = new Size(700, 59);
            _LabelTitle.Text = "Temperature Control";
            // 
            // _ButtonSave
            // 
            _ButtonSave.Location = new Point(700, 0);
            _ButtonSave.Visible = false;
            // 
            // _PanelMain
            // 
            _PanelMain.Controls.Add(_GridTemp);
            _PanelMain.Size = new Size(800, 541);
            // 
            // _Panel1
            // 
            _Panel1.Size = new Size(800, 59);
            // 
            // _GridTemp
            // 
            _GridTemp.AllowUserToAddRows = false;
            _GridTemp.AllowUserToDeleteRows = false;
            _GridTemp.AllowUserToResizeRows = false;
            _GridTemp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _GridTemp.BackgroundColor = Color.Black;
            _GridTemp.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = Color.Black;
            dataGridViewCellStyle1.Font = new Font("D2Coding", 12F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            _GridTemp.DefaultCellStyle = dataGridViewCellStyle1;
            _GridTemp.Dock = DockStyle.Fill;
            _GridTemp.Font = new Font("D2Coding", 12F);
            _GridTemp.Location = new Point(0, 0);
            _GridTemp.MultiSelect = false;
            _GridTemp.Name = "_GridTemp";
            _GridTemp.RowHeadersVisible = false;
            _GridTemp.RowTemplate.Height = 40;
            _GridTemp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _GridTemp.Size = new Size(800, 541);
            _GridTemp.TabIndex = 0;
            _GridTemp.ThemeStyle = UI.Controls.ThemeStyle.Black_White;
            _GridTemp.CellContentClick += _GridTemp_CellContentClick;
            // 
            // _updateTimer
            // 
            _updateTimer.Interval = 1000;
            _updateTimer.Tick += _updateTimer_Tick;
            // 
            // Temperature_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "Temperature_View";
            Size = new Size(800, 600);
            Load += Temperature_View_Load;
            _PanelMain.ResumeLayout(false);
            _Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_GridTemp).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private EQ.UI.Controls._DataGridView _GridTemp;
        private System.Windows.Forms.Timer _updateTimer;
    }
}