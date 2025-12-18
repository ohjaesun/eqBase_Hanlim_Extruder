namespace EQ.UI.UserViews
{
    partial class MotionSpeed_View
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new EQ.UI.Controls._DataGridView();
            this.panelBottom = new System.Windows.Forms.Panel();
            this._ButtonCalc = new EQ.UI.Controls._Button();
            this._LabelUnit = new EQ.UI.Controls._Label();
            this._TextBox1 = new EQ.UI.Controls._TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this._PanelMain.SuspendLayout();
            this._Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // _LabelTitle
            // 
            this._LabelTitle.Size = new System.Drawing.Size(700, 59);
            this._LabelTitle.Text = "Motion Speed Parameter";
            // 
            // _ButtonSave
            // 
            this._ButtonSave.Location = new System.Drawing.Point(700, 0);
            this._ButtonSave.Visible = true;
            // 
            // _PanelMain
            // 
            this._PanelMain.Controls.Add(this.dataGridView1);
            this._PanelMain.Controls.Add(this.panelBottom);
            this._PanelMain.Size = new System.Drawing.Size(800, 541);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(225)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("D2Coding", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 35;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Font = new System.Drawing.Font("D2Coding", 12F);
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(800, 354);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.ThemeStyle = EQ.UI.Controls.ThemeStyle.Display_LightYellow;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panelBottom.Controls.Add(this._ButtonCalc);
            this.panelBottom.Controls.Add(this._LabelUnit);
            this.panelBottom.Controls.Add(this._TextBox1);
            this.panelBottom.Controls.Add(this.richTextBox1);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 354);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(800, 187);
            this.panelBottom.TabIndex = 1;
            // 
            // _ButtonCalc
            // 
            this._ButtonCalc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this._ButtonCalc.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ButtonCalc.ForeColor = System.Drawing.Color.White;
            this._ButtonCalc.Location = new System.Drawing.Point(139, 7);
            this._ButtonCalc.Name = "_ButtonCalc";
            this._ButtonCalc.Size = new System.Drawing.Size(75, 35);
            this._ButtonCalc.TabIndex = 3;
            this._ButtonCalc.Text = "계산";
            this._ButtonCalc.ThemeStyle = EQ.UI.Controls.ThemeStyle.Primary_Indigo;
            this._ButtonCalc.UseVisualStyleBackColor = false;
            this._ButtonCalc.Click += new System.EventHandler(this._ButtonCalc_Click);
            // 
            // _LabelUnit
            // 
            this._LabelUnit.AutoSize = true;
            this._LabelUnit.BackColor = System.Drawing.SystemColors.ControlLight;
            this._LabelUnit.Font = new System.Drawing.Font("D2Coding", 12F);
            this._LabelUnit.ForeColor = System.Drawing.Color.Black;
            this._LabelUnit.Location = new System.Drawing.Point(109, 14);
            this._LabelUnit.Name = "_LabelUnit";
            this._LabelUnit.Size = new System.Drawing.Size(24, 18);
            this._LabelUnit.TabIndex = 2;
            this._LabelUnit.Text = "mm";
            this._LabelUnit.ThemeStyle = EQ.UI.Controls.ThemeStyle.Default;
            // 
            // _TextBox1
            // 
            this._TextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._TextBox1.Font = new System.Drawing.Font("D2Coding", 12F);
            this._TextBox1.ForeColor = System.Drawing.Color.Black;
            this._TextBox1.Location = new System.Drawing.Point(3, 11);
            this._TextBox1.Name = "_TextBox1";
            this._TextBox1.Size = new System.Drawing.Size(100, 26);
            this._TextBox1.TabIndex = 1;
            this._TextBox1.Text = "100";
            this._TextBox1.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.richTextBox1.Font = new System.Drawing.Font("D2Coding", 10F);
            this.richTextBox1.ForeColor = System.Drawing.Color.White;
            this.richTextBox1.Location = new System.Drawing.Point(243, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(557, 187);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // MotionSpeed_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "MotionSpeed_View";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.MotionSpeed_View_Load);
            this._PanelMain.ResumeLayout(false);
            this._Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private EQ.UI.Controls._DataGridView dataGridView1;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private EQ.UI.Controls._Button _ButtonCalc;
        private EQ.UI.Controls._Label _LabelUnit;
        private EQ.UI.Controls._TextBox _TextBox1;
    }
}