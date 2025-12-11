namespace EQ.UI.UserViews
{
    partial class UserOption_View
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
            tableLayoutPanel1 = new TableLayoutPanel();
            userControlBase1 = new UserControlBase();
            userControlBase2 = new UserControlBase();
            userControlBase3 = new UserControlBase();
            userControlBase4 = new UserControlBase();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(userControlBase1, 0, 0);
            tableLayoutPanel1.Controls.Add(userControlBase2, 1, 0);
            tableLayoutPanel1.Controls.Add(userControlBase3, 0, 1);
            tableLayoutPanel1.Controls.Add(userControlBase4, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(802, 655);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // userControlBase1
            // 
            userControlBase1.Dock = DockStyle.Fill;
            userControlBase1.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            userControlBase1.Location = new Point(3, 4);
            userControlBase1.Margin = new Padding(3, 4, 3, 4);
            userControlBase1.Name = "userControlBase1";
            userControlBase1.Size = new Size(395, 319);
            userControlBase1.TabIndex = 0;
            // 
            // userControlBase2
            // 
            userControlBase2.Dock = DockStyle.Fill;
            userControlBase2.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            userControlBase2.Location = new Point(404, 4);
            userControlBase2.Margin = new Padding(3, 4, 3, 4);
            userControlBase2.Name = "userControlBase2";
            userControlBase2.Size = new Size(395, 319);
            userControlBase2.TabIndex = 0;
            // 
            // userControlBase3
            // 
            userControlBase3.Dock = DockStyle.Fill;
            userControlBase3.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            userControlBase3.Location = new Point(3, 331);
            userControlBase3.Margin = new Padding(3, 4, 3, 4);
            userControlBase3.Name = "userControlBase3";
            userControlBase3.Size = new Size(395, 320);
            userControlBase3.TabIndex = 0;
            // 
            // userControlBase4
            // 
            userControlBase4.Dock = DockStyle.Fill;
            userControlBase4.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            userControlBase4.Location = new Point(404, 331);
            userControlBase4.Margin = new Padding(3, 4, 3, 4);
            userControlBase4.Name = "userControlBase4";
            userControlBase4.Size = new Size(395, 320);
            userControlBase4.TabIndex = 0;
            // 
            // UserOption_View
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            Margin = new Padding(3, 4, 3, 4);
            Name = "UserOption_View";
            Size = new Size(802, 655);
            Load += UserOption_View_Load;
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private UserControlBase userControlBase1;
        private UserControlBase userControlBase2;
        private UserControlBase userControlBase3;
        private UserControlBase userControlBase4;
    }
}
