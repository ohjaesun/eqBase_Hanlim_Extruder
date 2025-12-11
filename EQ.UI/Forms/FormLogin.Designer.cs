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
            this._ButtonConfirm = new EQ.UI.Controls._Button();
            this._ButtonCancel = new EQ.UI.Controls._Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._TextBoxPW = new EQ.UI.Controls._TextBox();
            this._LabelStatus = new EQ.UI.Controls._Label();
            this.label3 = new System.Windows.Forms.Label();
            this._ComboBoxLevel = new EQ.UI.Controls._ComboBox();
            this._CheckBoxChangePassword = new EQ.UI.Controls._CheckBox();
            this._LabelNewPassword = new System.Windows.Forms.Label();
            this._TextBoxNewPassword = new EQ.UI.Controls._TextBox();
            this._LabelConfirmPassword = new System.Windows.Forms.Label();
            this._TextBoxConfirmPassword = new EQ.UI.Controls._TextBox();
            this.SuspendLayout();
            // 
            // _ButtonConfirm
            // 
            this._ButtonConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this._ButtonConfirm.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ButtonConfirm.ForeColor = System.Drawing.Color.White;
            this._ButtonConfirm.Location = new System.Drawing.Point(58, 381);
            this._ButtonConfirm.Name = "_ButtonConfirm";
            this._ButtonConfirm.Size = new System.Drawing.Size(130, 55);
            this._ButtonConfirm.TabIndex = 4;
            this._ButtonConfirm.Text = "Login";
            this._ButtonConfirm.ThemeStyle = EQ.UI.Controls.ThemeStyle.Success_Green;
            this._ButtonConfirm.Click += new System.EventHandler(this._ButtonConfirm_Click);
            // 
            // _ButtonCancel
            // 
            this._ButtonCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._ButtonCancel.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ButtonCancel.ForeColor = System.Drawing.Color.Black;
            this._ButtonCancel.Location = new System.Drawing.Point(212, 381);
            this._ButtonCancel.Name = "_ButtonCancel";
            this._ButtonCancel.Size = new System.Drawing.Size(130, 55);
            this._ButtonCancel.TabIndex = 5;
            this._ButtonCancel.Text = "Close";
            this._ButtonCancel.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            this._ButtonCancel.Click += new System.EventHandler(this._ButtonCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(82, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Level:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(54, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Password:";
            // 
            // _TextBoxPW
            // 
            this._TextBoxPW.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._TextBoxPW.Enabled = false;
            this._TextBoxPW.Font = new System.Drawing.Font("D2Coding", 12F);
            this._TextBoxPW.ForeColor = System.Drawing.Color.Black;
            this._TextBoxPW.Location = new System.Drawing.Point(140, 141);
            this._TextBoxPW.Name = "_TextBoxPW";
            this._TextBoxPW.Size = new System.Drawing.Size(202, 26);
            this._TextBoxPW.TabIndex = 1;
            this._TextBoxPW.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            this._TextBoxPW.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // _LabelStatus
            // 
            this._LabelStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this._LabelStatus.Font = new System.Drawing.Font("D2Coding", 12F);
            this._LabelStatus.ForeColor = System.Drawing.Color.White;
            this._LabelStatus.Location = new System.Drawing.Point(58, 327);
            this._LabelStatus.Name = "_LabelStatus";
            this._LabelStatus.Size = new System.Drawing.Size(284, 23);
            this._LabelStatus.TabIndex = 8;
            this._LabelStatus.Text = "Select Level to Login.";
            this._LabelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._LabelStatus.ThemeStyle = EQ.UI.Controls.ThemeStyle.Primary_Indigo;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("D2Coding", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(100, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(194, 31);
            this.label3.TabIndex = 9;
            this.label3.Text = "EqBase Login";
            // 
            // _ComboBoxLevel
            // 
            this._ComboBoxLevel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this._ComboBoxLevel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this._ComboBoxLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._ComboBoxLevel.Font = new System.Drawing.Font("D2Coding", 12F);
            this._ComboBoxLevel.ForeColor = System.Drawing.Color.White;
            this._ComboBoxLevel.FormattingEnabled = true;
            this._ComboBoxLevel.ItemHeight = 20;
            this._ComboBoxLevel.Location = new System.Drawing.Point(140, 95);
            this._ComboBoxLevel.Name = "_ComboBoxLevel";
            this._ComboBoxLevel.Size = new System.Drawing.Size(202, 26);
            this._ComboBoxLevel.TabIndex = 0;
            this._ComboBoxLevel.ThemeStyle = EQ.UI.Controls.ThemeStyle.Highlight_DeepYellow;
            this._ComboBoxLevel.SelectedIndexChanged += new System.EventHandler(this._ComboBoxLevel_SelectedIndexChanged);
            // 
            // _CheckBoxChangePassword
            // 
            this._CheckBoxChangePassword.AutoSize = true;
            this._CheckBoxChangePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this._CheckBoxChangePassword.Font = new System.Drawing.Font("D2Coding", 12F);
            this._CheckBoxChangePassword.ForeColor = System.Drawing.Color.White;
            this._CheckBoxChangePassword.Location = new System.Drawing.Point(140, 193);
            this._CheckBoxChangePassword.Name = "_CheckBoxChangePassword";
            this._CheckBoxChangePassword.Size = new System.Drawing.Size(155, 22);
            this._CheckBoxChangePassword.TabIndex = 2;
            this._CheckBoxChangePassword.Text = "Change Password";
            this._CheckBoxChangePassword.ThemeStyle = EQ.UI.Controls.ThemeStyle.Info_Sky;
            this._CheckBoxChangePassword.UseVisualStyleBackColor = false;
            this._CheckBoxChangePassword.Visible = false;
            this._CheckBoxChangePassword.CheckedChanged += new System.EventHandler(this._CheckBoxChangePassword_CheckedChanged);
            // 
            // _LabelNewPassword
            // 
            this._LabelNewPassword.AutoSize = true;
            this._LabelNewPassword.ForeColor = System.Drawing.Color.White;
            this._LabelNewPassword.Location = new System.Drawing.Point(22, 240);
            this._LabelNewPassword.Name = "_LabelNewPassword";
            this._LabelNewPassword.Size = new System.Drawing.Size(112, 18);
            this._LabelNewPassword.TabIndex = 10;
            this._LabelNewPassword.Text = "New Password:";
            this._LabelNewPassword.Visible = false;
            // 
            // _TextBoxNewPassword
            // 
            this._TextBoxNewPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._TextBoxNewPassword.Font = new System.Drawing.Font("D2Coding", 12F);
            this._TextBoxNewPassword.ForeColor = System.Drawing.Color.Black;
            this._TextBoxNewPassword.Location = new System.Drawing.Point(140, 237);
            this._TextBoxNewPassword.Name = "_TextBoxNewPassword";
            this._TextBoxNewPassword.Size = new System.Drawing.Size(202, 26);
            this._TextBoxNewPassword.TabIndex = 3;
            this._TextBoxNewPassword.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            this._TextBoxNewPassword.Visible = false;
            this._TextBoxNewPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // _LabelConfirmPassword
            // 
            this._LabelConfirmPassword.AutoSize = true;
            this._LabelConfirmPassword.ForeColor = System.Drawing.Color.White;
            this._LabelConfirmPassword.Location = new System.Drawing.Point(14, 280);
            this._LabelConfirmPassword.Name = "_LabelConfirmPassword";
            this._LabelConfirmPassword.Size = new System.Drawing.Size(120, 18);
            this._LabelConfirmPassword.TabIndex = 11;
            this._LabelConfirmPassword.Text = "Confirm Pass:";
            this._LabelConfirmPassword.Visible = false;
            // 
            // _TextBoxConfirmPassword
            // 
            this._TextBoxConfirmPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this._TextBoxConfirmPassword.Font = new System.Drawing.Font("D2Coding", 12F);
            this._TextBoxConfirmPassword.ForeColor = System.Drawing.Color.Black;
            this._TextBoxConfirmPassword.Location = new System.Drawing.Point(140, 277);
            this._TextBoxConfirmPassword.Name = "_TextBoxConfirmPassword";
            this._TextBoxConfirmPassword.Size = new System.Drawing.Size(202, 26);
            this._TextBoxConfirmPassword.TabIndex = 4;
            this._TextBoxConfirmPassword.ThemeStyle = EQ.UI.Controls.ThemeStyle.Neutral_Gray;
            this._TextBoxConfirmPassword.Visible = false;
            this._TextBoxConfirmPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox_KeyDown);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
            this.ClientSize = new System.Drawing.Size(400, 461); // (세로 크기 증가)
            this.Controls.Add(this._TextBoxConfirmPassword);
            this.Controls.Add(this._LabelConfirmPassword);
            this.Controls.Add(this._TextBoxNewPassword);
            this.Controls.Add(this._LabelNewPassword);
            this.Controls.Add(this._CheckBoxChangePassword);
            this.Controls.Add(this._ComboBoxLevel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._LabelStatus);
            this.Controls.Add(this._TextBoxPW);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._ButtonCancel);
            this.Controls.Add(this._ButtonConfirm);
            this.Font = new System.Drawing.Font("D2Coding", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login / Change Password";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls._Button _ButtonConfirm;
        private Controls._Button _ButtonCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Controls._TextBox _TextBoxPW;
        private Controls._Label _LabelStatus;
        private System.Windows.Forms.Label label3;
        private Controls._ComboBox _ComboBoxLevel;
        private Controls._CheckBox _CheckBoxChangePassword;
        private System.Windows.Forms.Label _LabelNewPassword;
        private Controls._TextBox _TextBoxNewPassword;
        private System.Windows.Forms.Label _LabelConfirmPassword;
        private Controls._TextBox _TextBoxConfirmPassword;
    }
}