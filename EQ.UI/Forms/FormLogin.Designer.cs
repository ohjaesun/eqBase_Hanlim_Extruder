namespace EQ.UI
{
    partial class FormLogin
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            _ButtonConfirm = new EQ.UI.Controls._Button();
            _ButtonCancel = new EQ.UI.Controls._Button();
            label1 = new Label();
            label2 = new Label();
            _TextBoxPW = new EQ.UI.Controls._TextBox();
            _LabelStatus = new EQ.UI.Controls._Label();
            label3 = new Label();
            _TextBoxUserId = new EQ.UI.Controls._TextBox();
            _CheckBoxChangePassword = new EQ.UI.Controls._CheckBox();
            _LabelNewPassword = new Label();
            _TextBoxNewPassword = new EQ.UI.Controls._TextBox();
            _LabelConfirmPassword = new Label();
            _TextBoxConfirmPassword = new EQ.UI.Controls._TextBox();
            _ComboBox1 = new EQ.UI.Controls._ComboBox();
            SuspendLayout();
            // 
            // _ButtonConfirm
            // 
            _ButtonConfirm.BackColor = Color.FromArgb(46, 204, 113);
            _ButtonConfirm.Font = new Font("D2Coding", 12F);
            _ButtonConfirm.ForeColor = Color.Black;
            _ButtonConfirm.Location = new Point(58, 381);
            _ButtonConfirm.Name = "_ButtonConfirm";
            _ButtonConfirm.Size = new Size(130, 55);
            _ButtonConfirm.TabIndex = 4;
            _ButtonConfirm.Text = "Login";
            _ButtonConfirm.ThemeStyle = UI.Controls.ThemeStyle.Success_Green;
            _ButtonConfirm.TooltipText = null;
            _ButtonConfirm.UseVisualStyleBackColor = false;
            _ButtonConfirm.Click += _ButtonConfirm_Click;
            // 
            // _ButtonCancel
            // 
            _ButtonCancel.BackColor = Color.FromArgb(149, 165, 166);
            _ButtonCancel.DialogResult = DialogResult.Cancel;
            _ButtonCancel.Font = new Font("D2Coding", 12F);
            _ButtonCancel.ForeColor = Color.White;
            _ButtonCancel.Location = new Point(212, 381);
            _ButtonCancel.Name = "_ButtonCancel";
            _ButtonCancel.Size = new Size(130, 55);
            _ButtonCancel.TabIndex = 5;
            _ButtonCancel.Text = "Logout";
            _ButtonCancel.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            _ButtonCancel.TooltipText = null;
            _ButtonCancel.UseVisualStyleBackColor = false;
            _ButtonCancel.Click += _ButtonCancel_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(100, 98);
            label1.Name = "label1";
            label1.Size = new Size(32, 18);
            label1.TabIndex = 6;
            label1.Text = "ID:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.White;
            label2.Location = new Point(54, 144);
            label2.Name = "label2";
            label2.Size = new Size(80, 18);
            label2.TabIndex = 7;
            label2.Text = "Password:";
            // 
            // _TextBoxPW
            // 
            _TextBoxPW.BackColor = Color.FromArgb(149, 165, 166);
            _TextBoxPW.Font = new Font("D2Coding", 12F);
            _TextBoxPW.ForeColor = Color.White;
            _TextBoxPW.Location = new Point(140, 141);
            _TextBoxPW.Name = "_TextBoxPW";
            _TextBoxPW.Size = new Size(202, 26);
            _TextBoxPW.TabIndex = 1;
            _TextBoxPW.KeyDown += TextBox_KeyDown;
            // 
            // _LabelStatus
            // 
            _LabelStatus.BackColor = Color.FromArgb(48, 63, 159);
            _LabelStatus.Font = new Font("D2Coding", 12F);
            _LabelStatus.ForeColor = Color.White;
            _LabelStatus.Location = new Point(58, 327);
            _LabelStatus.Name = "_LabelStatus";
            _LabelStatus.Size = new Size(284, 23);
            _LabelStatus.TabIndex = 8;
            _LabelStatus.Text = "Enter User ID and Password.";
            _LabelStatus.TextAlign = ContentAlignment.MiddleCenter;
            _LabelStatus.ThemeStyle = UI.Controls.ThemeStyle.Primary_Indigo;
            _LabelStatus.TooltipText = null;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("D2Coding", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label3.ForeColor = Color.White;
            label3.Location = new Point(140, 9);
            label3.Name = "label3";
            label3.Size = new Size(84, 31);
            label3.TabIndex = 9;
            label3.Text = "Login";
            // 
            // _TextBoxUserId
            // 
            _TextBoxUserId.BackColor = Color.FromArgb(155, 89, 182);
            _TextBoxUserId.Font = new Font("D2Coding", 12F);
            _TextBoxUserId.ForeColor = Color.Black;
            _TextBoxUserId.Location = new Point(140, 95);
            _TextBoxUserId.Name = "_TextBoxUserId";
            _TextBoxUserId.Size = new Size(202, 26);
            _TextBoxUserId.TabIndex = 0;
            _TextBoxUserId.ThemeStyle = UI.Controls.ThemeStyle.Highlight_DeepYellow;
            _TextBoxUserId.TextChanged += _TextBoxUserId_TextChanged;
            // 
            // _CheckBoxChangePassword
            // 
            _CheckBoxChangePassword.AutoSize = true;
            _CheckBoxChangePassword.BackColor = Color.FromArgb(52, 152, 219);
            _CheckBoxChangePassword.Font = new Font("D2Coding", 12F);
            _CheckBoxChangePassword.ForeColor = Color.Black;
            _CheckBoxChangePassword.Location = new Point(140, 193);
            _CheckBoxChangePassword.Name = "_CheckBoxChangePassword";
            _CheckBoxChangePassword.Size = new Size(147, 22);
            _CheckBoxChangePassword.TabIndex = 2;
            _CheckBoxChangePassword.Text = "Change Password";
            _CheckBoxChangePassword.UseVisualStyleBackColor = false;
            _CheckBoxChangePassword.Visible = false;
            _CheckBoxChangePassword.CheckedChanged += _CheckBoxChangePassword_CheckedChanged;
            // 
            // _LabelNewPassword
            // 
            _LabelNewPassword.AutoSize = true;
            _LabelNewPassword.ForeColor = Color.White;
            _LabelNewPassword.Location = new Point(22, 240);
            _LabelNewPassword.Name = "_LabelNewPassword";
            _LabelNewPassword.Size = new Size(112, 18);
            _LabelNewPassword.TabIndex = 10;
            _LabelNewPassword.Text = "New Password:";
            _LabelNewPassword.Visible = false;
            // 
            // _TextBoxNewPassword
            // 
            _TextBoxNewPassword.BackColor = Color.FromArgb(149, 165, 166);
            _TextBoxNewPassword.Font = new Font("D2Coding", 12F);
            _TextBoxNewPassword.ForeColor = Color.White;
            _TextBoxNewPassword.Location = new Point(140, 237);
            _TextBoxNewPassword.Name = "_TextBoxNewPassword";
            _TextBoxNewPassword.Size = new Size(202, 26);
            _TextBoxNewPassword.TabIndex = 3;
            _TextBoxNewPassword.Visible = false;
            _TextBoxNewPassword.KeyDown += TextBox_KeyDown;
            // 
            // _LabelConfirmPassword
            // 
            _LabelConfirmPassword.AutoSize = true;
            _LabelConfirmPassword.ForeColor = Color.White;
            _LabelConfirmPassword.Location = new Point(14, 280);
            _LabelConfirmPassword.Name = "_LabelConfirmPassword";
            _LabelConfirmPassword.Size = new Size(112, 18);
            _LabelConfirmPassword.TabIndex = 11;
            _LabelConfirmPassword.Text = "Confirm Pass:";
            _LabelConfirmPassword.Visible = false;
            // 
            // _TextBoxConfirmPassword
            // 
            _TextBoxConfirmPassword.BackColor = Color.FromArgb(149, 165, 166);
            _TextBoxConfirmPassword.Font = new Font("D2Coding", 12F);
            _TextBoxConfirmPassword.ForeColor = Color.White;
            _TextBoxConfirmPassword.Location = new Point(140, 277);
            _TextBoxConfirmPassword.Name = "_TextBoxConfirmPassword";
            _TextBoxConfirmPassword.Size = new Size(202, 26);
            _TextBoxConfirmPassword.TabIndex = 4;
            _TextBoxConfirmPassword.Visible = false;
            _TextBoxConfirmPassword.KeyDown += TextBox_KeyDown;
            // 
            // _ComboBox1
            // 
            _ComboBox1.BackColor = Color.Black;
            _ComboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            _ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            _ComboBox1.Font = new Font("D2Coding", 12F);
            _ComboBox1.ForeColor = Color.White;
            _ComboBox1.FormattingEnabled = true;
            _ComboBox1.Location = new Point(139, 55);
            _ComboBox1.Name = "_ComboBox1";
            _ComboBox1.Size = new Size(203, 27);
            _ComboBox1.TabIndex = 12;
            _ComboBox1.ThemeStyle = UI.Controls.ThemeStyle.Black_White;
            _ComboBox1.TooltipText = null;
            _ComboBox1.SelectedIndexChanged += _ComboBox1_SelectedIndexChanged;
            // 
            // FormLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(52, 73, 94);
            ClientSize = new Size(400, 461);
            Controls.Add(_ComboBox1);
            Controls.Add(_TextBoxConfirmPassword);
            Controls.Add(_LabelConfirmPassword);
            Controls.Add(_TextBoxNewPassword);
            Controls.Add(_LabelNewPassword);
            Controls.Add(_CheckBoxChangePassword);
            Controls.Add(_TextBoxUserId);
            Controls.Add(label3);
            Controls.Add(_LabelStatus);
            Controls.Add(_TextBoxPW);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(_ButtonCancel);
            Controls.Add(_ButtonConfirm);
            Font = new Font("D2Coding", 12F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login / Change Password";
            Load += FormLogin_Load;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private Controls._Button _ButtonConfirm;
        private Controls._Button _ButtonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Controls._TextBox _TextBoxPW;
        private Controls._Label _LabelStatus;
        private System.Windows.Forms.Label label3;
        private Controls._TextBox _TextBoxUserId;
        private Controls._CheckBox _CheckBoxChangePassword;
        private System.Windows.Forms.Label _LabelNewPassword;
        private Controls._TextBox _TextBoxNewPassword;
        private System.Windows.Forms.Label _LabelConfirmPassword;
        private Controls._TextBox _TextBoxConfirmPassword;
        private Controls._ComboBox _ComboBox1;
    }
}