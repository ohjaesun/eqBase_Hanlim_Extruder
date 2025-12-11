namespace EQ.UI.UserViews
{
    partial class UserControlBase
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

       

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            _Panel1 = new EQ.UI.Controls._Panel();
            _LabelTitle = new EQ.UI.Controls._Label();
            _ButtonSave = new EQ.UI.Controls._Button();
            _PanelMain = new EQ.UI.Controls._Panel();
            timer1 = new System.Windows.Forms.Timer(components);
            timer2 = new System.Windows.Forms.Timer(components);
            _Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // _Panel1
            // 
            _Panel1.BackColor = SystemColors.Control;
            _Panel1.Controls.Add(_LabelTitle);
            _Panel1.Controls.Add(_ButtonSave);
            _Panel1.Dock = DockStyle.Top;
            _Panel1.ForeColor = SystemColors.ControlText;
            _Panel1.Location = new Point(0, 0);
            _Panel1.Name = "_Panel1";
            _Panel1.Size = new Size(363, 59);
            _Panel1.TabIndex = 0;
            // 
            // _LabelTitle
            // 
            _LabelTitle.BackColor = Color.FromArgb(149, 165, 166);
            _LabelTitle.Dock = DockStyle.Fill;
            _LabelTitle.Font = new Font("D2Coding", 12F);
            _LabelTitle.ForeColor = Color.White;
            _LabelTitle.Location = new Point(0, 0);
            _LabelTitle.Name = "_LabelTitle";
            _LabelTitle.Size = new Size(263, 59);
            _LabelTitle.TabIndex = 1;
            _LabelTitle.Text = "Title";
            _LabelTitle.TextAlign = ContentAlignment.MiddleCenter;
            _LabelTitle.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _LabelTitle.TooltipText = null;
            // 
            // _ButtonSave
            // 
            _ButtonSave.BackColor = Color.FromArgb(52, 73, 94);
            _ButtonSave.Dock = DockStyle.Right;
            _ButtonSave.Font = new Font("D2Coding", 12F);
            _ButtonSave.ForeColor = Color.White;
            _ButtonSave.Location = new Point(263, 0);
            _ButtonSave.Name = "_ButtonSave";
            _ButtonSave.Size = new Size(100, 59);
            _ButtonSave.TabIndex = 0;
            _ButtonSave.Text = "SAVE";
            _ButtonSave.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _ButtonSave.TooltipText = null;
            _ButtonSave.UseVisualStyleBackColor = false;
            // 
            // _PanelMain
            // 
            _PanelMain.BackColor = SystemColors.Control;
            _PanelMain.Dock = DockStyle.Fill;
            _PanelMain.ForeColor = SystemColors.ControlText;
            _PanelMain.Location = new Point(0, 59);
            _PanelMain.Name = "_PanelMain";
            _PanelMain.Size = new Size(363, 313);
            _PanelMain.TabIndex = 0;
            // 
            // UserControlBase
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(_PanelMain);
            Controls.Add(_Panel1);
            Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            Margin = new Padding(3, 4, 3, 4);
            Name = "UserControlBase";
            Size = new Size(363, 372);
            _Panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        public Controls._Label _LabelTitle;
        public Controls._Button _ButtonSave;
        public Controls._Panel _PanelMain;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        public Controls._Panel _Panel1;
    }
}
