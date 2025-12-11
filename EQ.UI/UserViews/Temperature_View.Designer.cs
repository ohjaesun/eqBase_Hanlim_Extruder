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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();

            this._GridTemp = new EQ.UI.Controls._DataGridView();
            this._updateTimer = new System.Windows.Forms.Timer(this.components);

            this._PanelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._GridTemp)).BeginInit();
            this.SuspendLayout();
            // 
            // _LabelTitle (Base)
            // 
            this._LabelTitle.Size = new System.Drawing.Size(800, 59);
            this._LabelTitle.Text = "Temperature Control";
            // 
            // _ButtonSave (Base)
            // 
            this._ButtonSave.Visible = false; // 저장 버튼 미사용
            // 
            // _PanelMain (Base)
            // 
            this._PanelMain.Controls.Add(this._GridTemp);
            this._PanelMain.Size = new System.Drawing.Size(800, 541);
            // 
            // _GridTemp
            // 
            this._GridTemp.AllowUserToAddRows = false;
            this._GridTemp.AllowUserToDeleteRows = false;
            this._GridTemp.AllowUserToResizeRows = false;
            this._GridTemp.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._GridTemp.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this._GridTemp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("D2Coding", 12F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._GridTemp.DefaultCellStyle = dataGridViewCellStyle1;
            this._GridTemp.Dock = System.Windows.Forms.DockStyle.Fill;
            this._GridTemp.Font = new System.Drawing.Font("D2Coding", 12F);
            this._GridTemp.Location = new System.Drawing.Point(0, 0);
            this._GridTemp.MultiSelect = false;
            this._GridTemp.Name = "_GridTemp";
            this._GridTemp.RowHeadersVisible = false;
            this._GridTemp.RowTemplate.Height = 40; // 행 높이 넉넉하게
            this._GridTemp.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._GridTemp.Size = new System.Drawing.Size(800, 541);
            this._GridTemp.TabIndex = 0;
            this._GridTemp.ThemeStyle = EQ.UI.Controls.ThemeStyle.Primary_Indigo;
            this._GridTemp.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._GridTemp_CellContentClick);
            // 
            // _updateTimer
            // 
            this._updateTimer.Interval = 1000; // 1초 주기
            this._updateTimer.Tick += new System.EventHandler(this._updateTimer_Tick);
            // 
            // Temperature_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Temperature_View";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.Temperature_View_Load);
            this._PanelMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._GridTemp)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private EQ.UI.Controls._DataGridView _GridTemp;
        private System.Windows.Forms.Timer _updateTimer;
    }
}