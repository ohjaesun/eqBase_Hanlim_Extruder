namespace EQ.UI.UserViews
{
    partial class AlarmSolution_View
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this._GridList = new EQ.UI.Controls._DataGridView();
            this._PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._GridList)).BeginInit();
            this.SuspendLayout();
            // 
            // _LabelTitle (Base)
            // 
            this._LabelTitle.Size = new System.Drawing.Size(800, 59);
            this._LabelTitle.Text = "Alarm Solution Management";
            // 
            // _ButtonSave (Base)
            // 
            this._ButtonSave.Location = new System.Drawing.Point(800, 0);
            this._ButtonSave.Visible = true;
            // 
            // _PanelMain (Base)
            // 
            this._PanelMain.Controls.Add(this._GridList);
            this._PanelMain.Size = new System.Drawing.Size(900, 541);
            // 
            // _GridList
            // 
            this._GridList.AllowUserToAddRows = false;
            this._GridList.AllowUserToDeleteRows = false;
            this._GridList.AllowUserToResizeRows = false;
            this._GridList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._GridList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._GridList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._GridList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._GridList.Font = new System.Drawing.Font("D2Coding", 12F);
            this._GridList.Location = new System.Drawing.Point(0, 0);
            this._GridList.Name = "_GridList";
            this._GridList.RowHeadersVisible = false;
            this._GridList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect; // 셀 단위 선택 (수정 편의)
            this._GridList.Size = new System.Drawing.Size(900, 541);
            this._GridList.TabIndex = 0;
            this._GridList.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // AlarmSolution_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "AlarmSolution_View";
            this.Size = new System.Drawing.Size(900, 600);
            this.Load += new System.EventHandler(this.AlarmSolution_View_Load);
            this._PanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._GridList)).EndInit();
            this.ResumeLayout(false);
        }

        private EQ.UI.Controls._DataGridView _GridList;
    }
}