namespace EQ.UI.UserViews
{
    partial class Users_View
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관�되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
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
            _gridUsers = new EQ.UI.Controls._DataGridView();
            _btnAdd = new EQ.UI.Controls._Button();
            _btnDelete = new EQ.UI.Controls._Button();
            _btnUnlock = new EQ.UI.Controls._Button();
            _btnResetPassword = new EQ.UI.Controls._Button();
            _TextBox1 = new EQ.UI.Controls._TextBox();
            _Label1 = new EQ.UI.Controls._Label();
            _Label2 = new EQ.UI.Controls._Label();
            _TextBox2 = new EQ.UI.Controls._TextBox();
            _RadioButton3 = new EQ.UI.Controls._RadioButton();
            _RadioButton2 = new EQ.UI.Controls._RadioButton();
            _RadioButton1 = new EQ.UI.Controls._RadioButton();
            ((System.ComponentModel.ISupportInitialize)_gridUsers).BeginInit();
            SuspendLayout();
            // 
            // _gridUsers
            // 
            _gridUsers.AllowUserToAddRows = false;
            _gridUsers.AllowUserToDeleteRows = false;
            _gridUsers.AllowUserToResizeRows = false;
            _gridUsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            _gridUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _gridUsers.BackgroundColor = Color.FromArgb(255, 255, 225);
            _gridUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(255, 255, 225);
            dataGridViewCellStyle1.Font = new Font("D2Coding", 12F);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            _gridUsers.DefaultCellStyle = dataGridViewCellStyle1;
            _gridUsers.Font = new Font("D2Coding", 12F);
            _gridUsers.Location = new Point(29, 218);
            _gridUsers.Margin = new Padding(4, 6, 4, 6);
            _gridUsers.MultiSelect = false;
            _gridUsers.Name = "_gridUsers";
            _gridUsers.ReadOnly = true;
            _gridUsers.RowHeadersVisible = false;
            _gridUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _gridUsers.Size = new Size(2464, 1312);
            _gridUsers.TabIndex = 0;
            _gridUsers.SelectionChanged += _gridUsers_SelectionChanged;
            // 
            // _btnAdd
            // 
            _btnAdd.BackColor = Color.FromArgb(46, 204, 113);
            _btnAdd.Font = new Font("D2Coding", 12F);
            _btnAdd.ForeColor = Color.Black;
            _btnAdd.Location = new Point(29, 17);
            _btnAdd.Margin = new Padding(4, 6, 4, 6);
            _btnAdd.Name = "_btnAdd";
            _btnAdd.Size = new Size(214, 80);
            _btnAdd.TabIndex = 1;
            _btnAdd.Text = "Add User";
            _btnAdd.ThemeStyle = UI.Controls.ThemeStyle.Success_Green;
            _btnAdd.TooltipText = null;
            _btnAdd.UseVisualStyleBackColor = false;
            _btnAdd.Click += _btnAdd_Click;
            // 
            // _btnDelete
            // 
            _btnDelete.BackColor = Color.FromArgb(231, 76, 60);
            _btnDelete.Font = new Font("D2Coding", 12F);
            _btnDelete.ForeColor = Color.Black;
            _btnDelete.Location = new Point(29, 126);
            _btnDelete.Margin = new Padding(4, 6, 4, 6);
            _btnDelete.Name = "_btnDelete";
            _btnDelete.Size = new Size(214, 80);
            _btnDelete.TabIndex = 2;
            _btnDelete.Text = "Delete User";
            _btnDelete.ThemeStyle = UI.Controls.ThemeStyle.Danger_Red;
            _btnDelete.TooltipText = null;
            _btnDelete.UseVisualStyleBackColor = false;
            _btnDelete.Click += _btnDelete_Click;
            // 
            // _btnUnlock
            // 
            _btnUnlock.BackColor = Color.FromArgb(52, 152, 219);
            _btnUnlock.Font = new Font("D2Coding", 12F);
            _btnUnlock.ForeColor = Color.Black;
            _btnUnlock.Location = new Point(270, 126);
            _btnUnlock.Margin = new Padding(4, 6, 4, 6);
            _btnUnlock.Name = "_btnUnlock";
            _btnUnlock.Size = new Size(214, 80);
            _btnUnlock.TabIndex = 3;
            _btnUnlock.Text = "Unlock Account";
            _btnUnlock.ThemeStyle = UI.Controls.ThemeStyle.Info_Sky;
            _btnUnlock.TooltipText = null;
            _btnUnlock.UseVisualStyleBackColor = false;
            _btnUnlock.Click += _btnUnlock_Click;
            // 
            // _btnResetPassword
            // 
            _btnResetPassword.BackColor = Color.FromArgb(241, 196, 15);
            _btnResetPassword.Font = new Font("D2Coding", 12F);
            _btnResetPassword.ForeColor = Color.Black;
            _btnResetPassword.Location = new Point(514, 126);
            _btnResetPassword.Margin = new Padding(4, 6, 4, 6);
            _btnResetPassword.Name = "_btnResetPassword";
            _btnResetPassword.Size = new Size(214, 80);
            _btnResetPassword.TabIndex = 4;
            _btnResetPassword.Text = "Reset Password";
            _btnResetPassword.ThemeStyle = UI.Controls.ThemeStyle.Warning_Yellow;
            _btnResetPassword.TooltipText = null;
            _btnResetPassword.UseVisualStyleBackColor = false;
            _btnResetPassword.Click += _btnResetPassword_Click;
            // 
            // _TextBox1
            // 
            _TextBox1.BackColor = SystemColors.Control;
            _TextBox1.Font = new Font("D2Coding", 17.9999981F, FontStyle.Regular, GraphicsUnit.Point, 129);
            _TextBox1.ForeColor = SystemColors.ControlText;
            _TextBox1.Location = new Point(384, 17);
            _TextBox1.Name = "_TextBox1";
            _TextBox1.Size = new Size(344, 35);
            _TextBox1.TabIndex = 5;
            _TextBox1.ThemeStyle = UI.Controls.ThemeStyle.Default;
            // 
            // _Label1
            // 
            _Label1.BackColor = Color.FromArgb(149, 165, 166);
            _Label1.Font = new Font("D2Coding", 17.9999981F, FontStyle.Regular, GraphicsUnit.Point, 129);
            _Label1.ForeColor = Color.White;
            _Label1.Location = new Point(270, 17);
            _Label1.Name = "_Label1";
            _Label1.Size = new Size(104, 37);
            _Label1.TabIndex = 6;
            _Label1.Text = "ID";
            _Label1.TextAlign = ContentAlignment.MiddleCenter;
            _Label1.TooltipText = null;
            // 
            // _Label2
            // 
            _Label2.BackColor = Color.FromArgb(149, 165, 166);
            _Label2.Font = new Font("D2Coding", 17.9999981F, FontStyle.Regular, GraphicsUnit.Point, 129);
            _Label2.ForeColor = Color.White;
            _Label2.Location = new Point(270, 66);
            _Label2.Name = "_Label2";
            _Label2.Size = new Size(104, 37);
            _Label2.TabIndex = 6;
            _Label2.Text = "Name";
            _Label2.TextAlign = ContentAlignment.MiddleCenter;
            _Label2.TooltipText = null;
            // 
            // _TextBox2
            // 
            _TextBox2.BackColor = SystemColors.Control;
            _TextBox2.Font = new Font("D2Coding", 17.9999981F, FontStyle.Regular, GraphicsUnit.Point, 129);
            _TextBox2.ForeColor = SystemColors.ControlText;
            _TextBox2.Location = new Point(384, 68);
            _TextBox2.Name = "_TextBox2";
            _TextBox2.Size = new Size(344, 35);
            _TextBox2.TabIndex = 5;
            _TextBox2.ThemeStyle = UI.Controls.ThemeStyle.Default;
            // 
            // _RadioButton3
            // 
            _RadioButton3.AutoSize = true;
            _RadioButton3.BackColor = Color.FromArgb(231, 76, 60);
            _RadioButton3.Font = new Font("D2Coding", 17.9999981F);
            _RadioButton3.ForeColor = Color.Black;
            _RadioButton3.Location = new Point(1036, 19);
            _RadioButton3.Name = "_RadioButton3";
            _RadioButton3.Size = new Size(90, 32);
            _RadioButton3.TabIndex = 0;
            _RadioButton3.Text = "Admin";
            _RadioButton3.ThemeStyle = UI.Controls.ThemeStyle.Danger_Red;
            _RadioButton3.UseVisualStyleBackColor = false;
            // 
            // _RadioButton2
            // 
            _RadioButton2.AutoSize = true;
            _RadioButton2.BackColor = Color.FromArgb(52, 152, 219);
            _RadioButton2.Font = new Font("D2Coding", 17.9999981F);
            _RadioButton2.ForeColor = Color.Black;
            _RadioButton2.Location = new Point(894, 19);
            _RadioButton2.Name = "_RadioButton2";
            _RadioButton2.Size = new Size(126, 32);
            _RadioButton2.TabIndex = 0;
            _RadioButton2.Text = "Engineer";
            _RadioButton2.UseVisualStyleBackColor = false;
            // 
            // _RadioButton1
            // 
            _RadioButton1.AutoSize = true;
            _RadioButton1.BackColor = Color.FromArgb(46, 204, 113);
            _RadioButton1.Checked = true;
            _RadioButton1.Font = new Font("D2Coding", 17.9999981F);
            _RadioButton1.ForeColor = Color.Black;
            _RadioButton1.Location = new Point(750, 19);
            _RadioButton1.Name = "_RadioButton1";
            _RadioButton1.Size = new Size(126, 32);
            _RadioButton1.TabIndex = 0;
            _RadioButton1.TabStop = true;
            _RadioButton1.Text = "Operator";
            _RadioButton1.ThemeStyle = UI.Controls.ThemeStyle.Success_Green;
            _RadioButton1.UseVisualStyleBackColor = false;
            // 
            // Users_View
            // 
            AutoScaleDimensions = new SizeF(10F, 24F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(_RadioButton3);
            Controls.Add(_RadioButton2);
            Controls.Add(_Label2);
            Controls.Add(_RadioButton1);
            Controls.Add(_Label1);
            Controls.Add(_TextBox2);
            Controls.Add(_TextBox1);
            Controls.Add(_btnResetPassword);
            Controls.Add(_btnUnlock);
            Controls.Add(_btnDelete);
            Controls.Add(_btnAdd);
            Controls.Add(_gridUsers);
            Margin = new Padding(6, 10, 6, 10);
            Name = "Users_View";
            Size = new Size(2521, 1570);
            Load += Users_View_Load;
            ((System.ComponentModel.ISupportInitialize)_gridUsers).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private Controls._DataGridView _gridUsers;
        private Controls._Button _btnAdd;
        private Controls._Button _btnDelete;
        private Controls._Button _btnUnlock;
        private Controls._Button _btnResetPassword;
        private Controls._TextBox _TextBox1;
        private Controls._Label _Label1;
        private Controls._Label _Label2;
        private Controls._TextBox _TextBox2;
        private Controls._RadioButton _RadioButton3;
        private Controls._RadioButton _RadioButton2;
        private Controls._RadioButton _RadioButton1;
    }
}
