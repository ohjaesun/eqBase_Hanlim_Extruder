namespace EQ.UI.UserViews.EQ_HanLim_Extuder
{
    partial class Test
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
            _GroupBox1 = new EQ.UI.Controls._GroupBox();
            temperature_View1 = new Temperature_View();
            _GroupBox2 = new EQ.UI.Controls._GroupBox();
            _DataGridView1 = new EQ.UI.Controls._DataGridView();
            _GroupBox1.SuspendLayout();
            _GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_DataGridView1).BeginInit();
            SuspendLayout();
            // 
            // _GroupBox1
            // 
            _GroupBox1.BackColor = SystemColors.Control;
            _GroupBox1.Controls.Add(temperature_View1);
            _GroupBox1.Dock = DockStyle.Fill;
            _GroupBox1.Font = new Font("D2Coding", 12F);
            _GroupBox1.ForeColor = SystemColors.ControlText;
            _GroupBox1.Location = new Point(0, 202);
            _GroupBox1.Name = "_GroupBox1";
            _GroupBox1.Size = new Size(1147, 439);
            _GroupBox1.TabIndex = 0;
            _GroupBox1.TabStop = false;
            _GroupBox1.Text = "Temperature";
            _GroupBox1.ThemeStyle = UI.Controls.ThemeStyle.Default;
            // 
            // temperature_View1
            // 
            temperature_View1.Dock = DockStyle.Fill;
            temperature_View1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            temperature_View1.Location = new Point(3, 22);
            temperature_View1.Margin = new Padding(3, 4, 3, 4);
            temperature_View1.Name = "temperature_View1";
            temperature_View1.Size = new Size(1141, 414);
            temperature_View1.TabIndex = 0;
            // 
            // _GroupBox2
            // 
            _GroupBox2.BackColor = SystemColors.Control;
            _GroupBox2.Controls.Add(_DataGridView1);
            _GroupBox2.Dock = DockStyle.Top;
            _GroupBox2.Font = new Font("D2Coding", 12F);
            _GroupBox2.ForeColor = SystemColors.ControlText;
            _GroupBox2.Location = new Point(0, 0);
            _GroupBox2.Name = "_GroupBox2";
            _GroupBox2.Size = new Size(1147, 202);
            _GroupBox2.TabIndex = 0;
            _GroupBox2.TabStop = false;
            _GroupBox2.Text = "Motor";
            _GroupBox2.ThemeStyle = UI.Controls.ThemeStyle.Default;
            // 
            // _DataGridView1
            // 
            _DataGridView1.AllowUserToAddRows = false;
            _DataGridView1.AllowUserToDeleteRows = false;
            _DataGridView1.AllowUserToResizeRows = false;
            _DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _DataGridView1.BackgroundColor = Color.FromArgb(255, 255, 225);
            _DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(255, 255, 225);
            dataGridViewCellStyle1.Font = new Font("D2Coding", 12F);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            _DataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            _DataGridView1.Dock = DockStyle.Fill;
            _DataGridView1.Font = new Font("D2Coding", 12F);
            _DataGridView1.Location = new Point(3, 22);
            _DataGridView1.Name = "_DataGridView1";
            _DataGridView1.RowHeadersVisible = false;
            _DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _DataGridView1.Size = new Size(1141, 177);
            _DataGridView1.TabIndex = 0;
            // 
            // Test
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(_GroupBox1);
            Controls.Add(_GroupBox2);
            Name = "Test";
            Size = new Size(1147, 641);
            _GroupBox1.ResumeLayout(false);
            _GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_DataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Controls._GroupBox _GroupBox1;
        private Controls._GroupBox _GroupBox2;
        private Controls._DataGridView _DataGridView1;
        private Temperature_View temperature_View1;
    }
}
