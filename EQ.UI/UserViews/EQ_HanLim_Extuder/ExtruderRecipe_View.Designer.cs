
namespace EQ.UI.UserViews.EQ_HanLim_Extuder
{
    partial class ExtruderRecipe_View
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
            _gridRecipes = new EQ.UI.Controls._DataGridView();
            _colNo = new DataGridViewTextBoxColumn();
            _colName = new DataGridViewTextBoxColumn();
            _colZone1 = new DataGridViewTextBoxColumn();
            _colZone2 = new DataGridViewTextBoxColumn();
            _colSpeed = new DataGridViewTextBoxColumn();
            _btnSave = new EQ.UI.Controls._Button();
            ((System.ComponentModel.ISupportInitialize)_gridRecipes).BeginInit();
            SuspendLayout();
            // 
            // _gridRecipes
            // 
            _gridRecipes.AllowUserToAddRows = false;
            _gridRecipes.AllowUserToDeleteRows = false;
            _gridRecipes.AllowUserToResizeRows = false;
            _gridRecipes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _gridRecipes.BackgroundColor = Color.FromArgb(255, 255, 225);
            _gridRecipes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _gridRecipes.Columns.AddRange(new DataGridViewColumn[] { _colNo, _colName, _colZone1, _colZone2, _colSpeed });
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(255, 255, 225);
            dataGridViewCellStyle1.Font = new Font("D2Coding", 12F);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            _gridRecipes.DefaultCellStyle = dataGridViewCellStyle1;
            _gridRecipes.Font = new Font("D2Coding", 12F);
            _gridRecipes.Location = new Point(22, 22);
            _gridRecipes.Margin = new Padding(3, 4, 3, 4);
            _gridRecipes.Name = "_gridRecipes";
            _gridRecipes.RowHeadersVisible = false;
            _gridRecipes.RowTemplate.Height = 30;
            _gridRecipes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _gridRecipes.Size = new Size(686, 387);
            _gridRecipes.TabIndex = 0;
            // 
            // _colNo
            // 
            _colNo.HeaderText = "No.";
            _colNo.Name = "_colNo";
            _colNo.ReadOnly = true;
            // 
            // _colName
            // 
            _colName.HeaderText = "Name";
            _colName.Name = "_colName";
            // 
            // _colZone1
            // 
            _colZone1.HeaderText = "Zone1";
            _colZone1.Name = "_colZone1";
            // 
            // _colZone2
            // 
            _colZone2.HeaderText = "Zone2";
            _colZone2.Name = "_colZone2";
            // 
            // _colSpeed
            // 
            _colSpeed.HeaderText = "Speed";
            _colSpeed.Name = "_colSpeed";
            // 
            // _btnSave
            // 
            _btnSave.BackColor = Color.FromArgb(46, 204, 113);
            _btnSave.Font = new Font("D2Coding", 12F);
            _btnSave.ForeColor = Color.Black;
            _btnSave.Location = new Point(571, 417);
            _btnSave.Margin = new Padding(3, 4, 3, 4);
            _btnSave.Name = "_btnSave";
            _btnSave.Size = new Size(137, 60);
            _btnSave.TabIndex = 1;
            _btnSave.Text = "Save";
            _btnSave.ThemeStyle = UI.Controls.ThemeStyle.Success_Green;
            _btnSave.TooltipText = null;
            _btnSave.UseVisualStyleBackColor = false;
            _btnSave.Click += _btnSave_Click;
            // 
            // ExtruderRecipe_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(_btnSave);
            Controls.Add(_gridRecipes);
            Margin = new Padding(3, 6, 3, 6);
            Name = "ExtruderRecipe_View";
            Size = new Size(731, 483);
            ((System.ComponentModel.ISupportInitialize)_gridRecipes).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private EQ.UI.Controls._DataGridView _gridRecipes;
        private EQ.UI.Controls._Button _btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colZone1;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colZone2;
        private System.Windows.Forms.DataGridViewTextBoxColumn _colSpeed;
    }
}
