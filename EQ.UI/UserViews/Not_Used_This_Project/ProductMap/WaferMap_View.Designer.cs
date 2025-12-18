namespace EQ.UI.UserViews
{
    partial class WaferMap_View
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
            this._LabelTitle = new EQ.UI.Controls._Label();
            this._panelTopControl = new System.Windows.Forms.Panel();
            this._btnRefresh = new EQ.UI.Controls._Button();
            this._lblSlot = new System.Windows.Forms.Label();
            this._comboSlot = new EQ.UI.Controls._ComboBox();
            this._lblMagazine = new EQ.UI.Controls._Label();
            this._comboMagazine = new EQ.UI.Controls._ComboBox();
            this._panelTopControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // _LabelTitle
            // 
            this._LabelTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._LabelTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this._LabelTitle.Font = new System.Drawing.Font("D2Coding", 12F);
            this._LabelTitle.ForeColor = System.Drawing.Color.White;
            this._LabelTitle.Location = new System.Drawing.Point(0, 0);
            this._LabelTitle.Name = "_LabelTitle";
            this._LabelTitle.Size = new System.Drawing.Size(600, 40);
            this._LabelTitle.TabIndex = 0;
            this._LabelTitle.Text = "Wafer Map Monitor";
            this._LabelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._LabelTitle.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // _panelTopControl
            // 
            this._panelTopControl.BackColor = System.Drawing.Color.WhiteSmoke;
            this._panelTopControl.Controls.Add(this._btnRefresh);
            this._panelTopControl.Controls.Add(this._lblSlot);
            this._panelTopControl.Controls.Add(this._comboSlot);
            this._panelTopControl.Controls.Add(this._lblMagazine);
            this._panelTopControl.Controls.Add(this._comboMagazine);
            this._panelTopControl.Dock = System.Windows.Forms.DockStyle.Top;
            this._panelTopControl.Location = new System.Drawing.Point(0, 40);
            this._panelTopControl.Name = "_panelTopControl";
            this._panelTopControl.Size = new System.Drawing.Size(600, 40);
            this._panelTopControl.TabIndex = 1;
            this._panelTopControl.Visible = false;
            // 
            // _btnRefresh
            // 
            this._btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this._btnRefresh.Dock = System.Windows.Forms.DockStyle.Right;
            this._btnRefresh.Font = new System.Drawing.Font("D2Coding", 12F);
            this._btnRefresh.ForeColor = System.Drawing.Color.White;
            this._btnRefresh.Location = new System.Drawing.Point(520, 0);
            this._btnRefresh.Name = "_btnRefresh";
            this._btnRefresh.Size = new System.Drawing.Size(80, 40);
            this._btnRefresh.TabIndex = 4;
            this._btnRefresh.Text = "Refresh";
            this._btnRefresh.ThemeStyle = EQ.UI.Controls.ThemeStyle.Primary_Indigo;
            this._btnRefresh.Click += new System.EventHandler(this._btnRefresh_Click);
            // 
            // _lblMagazine
            // 
            this._lblMagazine.AutoSize = true;
            this._lblMagazine.Font = new System.Drawing.Font("D2Coding", 12F);
            this._lblMagazine.Location = new System.Drawing.Point(10, 12);
            this._lblMagazine.Name = "_lblMagazine";
            this._lblMagazine.Size = new System.Drawing.Size(80, 18);
            this._lblMagazine.TabIndex = 0;
            this._lblMagazine.Text = "Magazine:";
            this._lblMagazine.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // _comboMagazine
            // 
            this._comboMagazine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this._comboMagazine.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._comboMagazine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboMagazine.Font = new System.Drawing.Font("D2Coding", 12F);
            this._comboMagazine.ForeColor = System.Drawing.Color.Black;
            this._comboMagazine.FormattingEnabled = true;
            this._comboMagazine.Location = new System.Drawing.Point(95, 8);
            this._comboMagazine.Name = "_comboMagazine";
            this._comboMagazine.Size = new System.Drawing.Size(140, 27);
            this._comboMagazine.TabIndex = 1;
            this._comboMagazine.ThemeStyle = EQ.UI.Controls.ThemeStyle.Highlight_DeepYellow;
            this._comboMagazine.SelectedIndexChanged += new System.EventHandler(this._comboMagazine_SelectedIndexChanged);
            // 
            // _lblSlot
            // 
            this._lblSlot.AutoSize = true;
            this._lblSlot.Font = new System.Drawing.Font("D2Coding", 12F);
            this._lblSlot.Location = new System.Drawing.Point(250, 12);
            this._lblSlot.Name = "_lblSlot";
            this._lblSlot.Size = new System.Drawing.Size(48, 18);
            this._lblSlot.TabIndex = 2;
            this._lblSlot.Text = "Slot:";
            // 
            // _comboSlot
            // 
            this._comboSlot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this._comboSlot.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._comboSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._comboSlot.Font = new System.Drawing.Font("D2Coding", 12F);
            this._comboSlot.ForeColor = System.Drawing.Color.Black;
            this._comboSlot.FormattingEnabled = true;
            this._comboSlot.Location = new System.Drawing.Point(300, 8);
            this._comboSlot.Name = "_comboSlot";
            this._comboSlot.Size = new System.Drawing.Size(100, 27);
            this._comboSlot.TabIndex = 3;
            this._comboSlot.ThemeStyle = EQ.UI.Controls.ThemeStyle.Highlight_DeepYellow;
            this._comboSlot.SelectedIndexChanged += new System.EventHandler(this._comboSlot_SelectedIndexChanged);
            // 
            // WaferMap_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panelTopControl);
            this.Controls.Add(this._LabelTitle);
            this.Name = "WaferMap_View";
            this.Size = new System.Drawing.Size(600, 500);
            this.Load += new System.EventHandler(this.WaferMap_View_Load);
            this._panelTopControl.ResumeLayout(false);
            this._panelTopControl.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private EQ.UI.Controls._Label _LabelTitle;
        private System.Windows.Forms.Panel _panelTopControl;
        private EQ.UI.Controls._Button _btnRefresh;
        private System.Windows.Forms.Label _lblSlot;
        private EQ.UI.Controls._ComboBox _comboSlot;
        private EQ.UI.Controls._Label _lblMagazine;
        private EQ.UI.Controls._ComboBox _comboMagazine;
    }
}