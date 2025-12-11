namespace EQ.UI.UserViews
{
    partial class IO_View
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            tableLayoutPanel1 = new TableLayoutPanel();
            _dataGridView1 = new EQ.UI.Controls._DataGridView();
            _dataGridView2 = new EQ.UI.Controls._DataGridView();
            _PanelMain.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // _LabelTitle
            // 
            _LabelTitle.Size = new Size(731, 59);
            // 
            // _ButtonSave
            // 
            _ButtonSave.Location = new Point(731, 0);
            // 
            // _Panel2
            // 
            _PanelMain.Controls.Add(tableLayoutPanel1);
            _PanelMain.Size = new Size(831, 425);
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(_dataGridView1, 0, 0);
            tableLayoutPanel1.Controls.Add(_dataGridView2, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(831, 425);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // _dataGridView1
            // 
            _dataGridView1.AllowUserToAddRows = false;
            _dataGridView1.AllowUserToDeleteRows = false;
            _dataGridView1.AllowUserToResizeRows = false;
            _dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dataGridView1.BackgroundColor = Color.FromArgb(149, 165, 166);
            _dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(149, 165, 166);
            dataGridViewCellStyle1.Font = new Font("D2Coding", 12F);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            _dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            _dataGridView1.Dock = DockStyle.Fill;
            _dataGridView1.Font = new Font("D2Coding", 12F);
            _dataGridView1.Location = new Point(3, 3);
            _dataGridView1.Name = "_dataGridView1";
            _dataGridView1.RowHeadersVisible = false;
            _dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dataGridView1.Size = new Size(409, 419);
            _dataGridView1.TabIndex = 0;
            _dataGridView1.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _dataGridView1.CellContentClick += _DataGridView1_CellContentClick;
            // 
            // _dataGridView2
            // 
            _dataGridView2.AllowUserToAddRows = false;
            _dataGridView2.AllowUserToDeleteRows = false;
            _dataGridView2.AllowUserToResizeRows = false;
            _dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _dataGridView2.BackgroundColor = Color.FromArgb(149, 165, 166);
            _dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(149, 165, 166);
            dataGridViewCellStyle2.Font = new Font("D2Coding", 12F);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            _dataGridView2.DefaultCellStyle = dataGridViewCellStyle2;
            _dataGridView2.Dock = DockStyle.Fill;
            _dataGridView2.Font = new Font("D2Coding", 12F);
            _dataGridView2.Location = new Point(418, 3);
            _dataGridView2.Name = "_dataGridView2";
            _dataGridView2.RowHeadersVisible = false;
            _dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _dataGridView2.Size = new Size(410, 419);
            _dataGridView2.TabIndex = 0;
            _dataGridView2.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _dataGridView2.CellContentClick += _DataGridView1_CellContentClick;
            // 
            // IO_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "IO_View";
            Size = new Size(831, 484);
            Load += IO_View_Load;
            _PanelMain.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)_dataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Controls._DataGridView _dataGridView1;
        private Controls._DataGridView _dataGridView2;
    }
}
