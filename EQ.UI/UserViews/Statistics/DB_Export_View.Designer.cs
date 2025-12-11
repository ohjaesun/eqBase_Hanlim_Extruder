namespace EQ.UI.UserViews
{
    partial class DB_Export_View
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
            _Button1 = new EQ.UI.Controls._Button();
            _Label1 = new EQ.UI.Controls._Label();
            _Button2 = new EQ.UI.Controls._Button();
            _Label2 = new EQ.UI.Controls._Label();
            _Label3 = new EQ.UI.Controls._Label();
            _GridDBInfo = new EQ.UI.Controls._DataGridView();
            _PanelMain.SuspendLayout();
            _Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_GridDBInfo).BeginInit();
            SuspendLayout();
            // 
            // _LabelTitle
            // 
            _LabelTitle.Size = new Size(589, 59);
            // 
            // _ButtonSave
            // 
            _ButtonSave.Location = new Point(589, 0);
            _ButtonSave.Visible = false;
            // 
            // _PanelMain
            // 
            _PanelMain.Controls.Add(_GridDBInfo);
            _PanelMain.Controls.Add(_Label3);
            _PanelMain.Controls.Add(_Label2);
            _PanelMain.Controls.Add(_Label1);
            _PanelMain.Controls.Add(_Button2);
            _PanelMain.Controls.Add(_Button1);
            _PanelMain.Size = new Size(689, 476);
            // 
            // _Panel1
            // 
            _Panel1.Size = new Size(689, 59);
            // 
            // _Button1
            // 
            _Button1.BackColor = Color.FromArgb(48, 63, 159);
            _Button1.Font = new Font("D2Coding", 12F);
            _Button1.ForeColor = Color.White;
            _Button1.Location = new Point(13, 6);
            _Button1.Name = "_Button1";
            _Button1.Size = new Size(100, 55);
            _Button1.TabIndex = 0;
            _Button1.Text = "DB 선택";
            _Button1.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _Button1.TooltipText = null;
            _Button1.UseVisualStyleBackColor = false;
            _Button1.Click += _Button1_Click;
            // 
            // _Label1
            // 
            _Label1.AutoSize = true;
            _Label1.BackColor = Color.FromArgb(155, 89, 182);
            _Label1.Font = new Font("D2Coding", 12F);
            _Label1.ForeColor = Color.Black;
            _Label1.Location = new Point(13, 74);
            _Label1.Name = "_Label1";
            _Label1.Size = new Size(64, 18);
            _Label1.TabIndex = 1;
            _Label1.Text = "_Label1";
            _Label1.ThemeStyle = UI.Controls.ThemeStyle.Highlight_DeepYellow;
            _Label1.TooltipText = null;
            // 
            // _Button2
            // 
            _Button2.BackColor = Color.FromArgb(48, 63, 159);
            _Button2.Font = new Font("D2Coding", 12F);
            _Button2.ForeColor = Color.White;
            _Button2.Location = new Point(13, 99);
            _Button2.Name = "_Button2";
            _Button2.Size = new Size(100, 55);
            _Button2.TabIndex = 0;
            _Button2.Text = "복원";
            _Button2.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _Button2.TooltipText = null;
            _Button2.UseVisualStyleBackColor = false;
            _Button2.Click += _Button2_Click;
            // 
            // _Label2
            // 
            _Label2.AutoSize = true;
            _Label2.BackColor = Color.FromArgb(149, 165, 166);
            _Label2.Font = new Font("D2Coding", 12F);
            _Label2.ForeColor = Color.White;
            _Label2.Location = new Point(119, 12);
            _Label2.Name = "_Label2";
            _Label2.Size = new Size(208, 18);
            _Label2.TabIndex = 2;
            _Label2.Text = "RECIPE 폴더 : 설정 데이터";
            _Label2.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _Label2.TooltipText = null;
            // 
            // _Label3
            // 
            _Label3.AutoSize = true;
            _Label3.BackColor = Color.FromArgb(149, 165, 166);
            _Label3.Font = new Font("D2Coding", 12F);
            _Label3.ForeColor = Color.White;
            _Label3.Location = new Point(119, 43);
            _Label3.Name = "_Label3";
            _Label3.Size = new Size(248, 18);
            _Label3.TabIndex = 2;
            _Label3.Text = "ProductData 폴더 : 검사 데이터";
            _Label3.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _Label3.TooltipText = null;
            // 
            // _GridDBInfo
            // 
            _GridDBInfo.AllowUserToAddRows = false;
            _GridDBInfo.AllowUserToDeleteRows = false;
            _GridDBInfo.AllowUserToResizeRows = false;
            _GridDBInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _GridDBInfo.BackgroundColor = Color.FromArgb(149, 165, 166);
            _GridDBInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(149, 165, 166);
            dataGridViewCellStyle1.Font = new Font("D2Coding", 12F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            _GridDBInfo.DefaultCellStyle = dataGridViewCellStyle1;
            _GridDBInfo.Font = new Font("D2Coding", 12F);
            _GridDBInfo.Location = new Point(13, 160);
            _GridDBInfo.Name = "_GridDBInfo";
            _GridDBInfo.RowHeadersVisible = false;
            _GridDBInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _GridDBInfo.Size = new Size(673, 313);
            _GridDBInfo.TabIndex = 3;
            _GridDBInfo.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // DB_Export_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Name = "DB_Export_View";
            Size = new Size(689, 535);
            _PanelMain.ResumeLayout(false);
            _PanelMain.PerformLayout();
            _Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)_GridDBInfo).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Controls._Label _Label1;
        private Controls._Button _Button2;
        private Controls._Button _Button1;
        private Controls._Label _Label3;
        private Controls._Label _Label2;
        private Controls._DataGridView _GridDBInfo;
    }
}
