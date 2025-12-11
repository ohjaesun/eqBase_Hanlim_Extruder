using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.UI.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EQ.UI
{
    public partial class FormLogin : Form
    {
        private readonly ACT _act;

        public FormLogin()
        {
            InitializeComponent();
            _act = ActManager.Instance.Act;

            this.Font = new Font("D2Coding", 12F, FontStyle.Regular, GraphicsUnit.Point, 129);
            this.BackColor = Color.FromArgb(52, 73, 94);
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            _TextBoxPW.PasswordChar = '●';
            _TextBoxNewPassword.PasswordChar = '●';
            _TextBoxConfirmPassword.PasswordChar = '●';

            // (요청 4) ActUser에서 목록을 가져오는 대신, Enum을 직접 바인딩
            _ComboBoxLevel.Items.Clear();
            _ComboBoxLevel.Items.Add(UserLevel.Operator);
            _ComboBoxLevel.Items.Add(UserLevel.Engineer);
            _ComboBoxLevel.Items.Add(UserLevel.Admin);

            // (요청 3) 기본값 Operator 선택
            _ComboBoxLevel.SelectedItem = UserLevel.Operator;

            UpdateUIState();
        }

        private void _ComboBoxLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateUIState();
        }

        private void _CheckBoxChangePassword_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUIState();
        }

        /// <summary>
        /// (요청 4) 콤보박스/체크박스 상태에 따라 UI를 갱신
        /// </summary>
        private void UpdateUIState()
        {
            if (_ComboBoxLevel.SelectedItem == null) return;

            UserLevel selectedLevel = (UserLevel)_ComboBoxLevel.SelectedItem;

            bool isOperator = (selectedLevel == UserLevel.Operator);

            // 1. (요청 4) Operator일 때 암호창 비활성화
            _TextBoxPW.Enabled = !isOperator;
            if (isOperator) _TextBoxPW.Text = "";

            // 2. (요청 4) Engineer/Admin일 때만 "암호 변경" 체크박스 보임
            _CheckBoxChangePassword.Visible = !isOperator;
            if (isOperator)
            {
                _CheckBoxChangePassword.Checked = false;
            }

            // 3. (요청 4) "암호 변경" 체크 시 새 암호/확인란 보임
            bool isChangeMode = _CheckBoxChangePassword.Checked && !isOperator;
            _LabelNewPassword.Visible = isChangeMode;
            _TextBoxNewPassword.Visible = isChangeMode;
            _LabelConfirmPassword.Visible = isChangeMode;
            _TextBoxConfirmPassword.Visible = isChangeMode;

            // 4. 버튼 텍스트 변경
            if (isOperator)
            {
                _ButtonConfirm.Text = "Select"; // Operator는 "로그인"이 아님
            }
            else
            {
                _ButtonConfirm.Text = isChangeMode ? "Change" : "Login";
            }

            // 5. 레이블 변경
            label2.Text = isChangeMode ? "Old Password:" : "Password:";

            // 6. 상태 레이블 초기화
            _LabelStatus.Text = "";
            _LabelStatus.ThemeStyle = ThemeStyle.Primary_Indigo;
        }

        private void _ButtonConfirm_Click(object sender, EventArgs e)
        {
            if (_CheckBoxChangePassword.Checked)
            {
                PerformChangePassword();
            }
            else
            {
                PerformLoginOrSelect();
            }
        }

        private void _ButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void PerformLoginOrSelect()
        {
            UserLevel selectedLevel = (UserLevel)_ComboBoxLevel.SelectedItem;
            string password = _TextBoxPW.Text;

            // (요청 3 & 4) Operator 선택 시
            if (selectedLevel == UserLevel.Operator)
            {
                _act.User.Logout(); // Operator로 복귀
                this.DialogResult = DialogResult.OK; // (성공으로 간주)
                this.Close();
                return;
            }

            // Engineer 또는 Admin 로그인
            if (_act.User.Login(selectedLevel, password))
            {
                this.DialogResult = DialogResult.OK; // 로그인 성공
                this.Close();
            }
            else
            {
                _LabelStatus.Text = "Invalid Password.";
                _LabelStatus.ThemeStyle = ThemeStyle.Danger_Red;
            }
        }

        /// <summary>
        /// (요청 4) 암호 변경 로직
        /// </summary>
        private void PerformChangePassword()
        {
            UserLevel selectedLevel = (UserLevel)_ComboBoxLevel.SelectedItem;
            string oldPassword = _TextBoxPW.Text;
            string newPassword = _TextBoxNewPassword.Text;
            string confirmPassword = _TextBoxConfirmPassword.Text;

            // (요청 4) 2자리 이하 설정 불가
            if (string.IsNullOrEmpty(newPassword) || newPassword.Length <= 2)
            {
                _LabelStatus.Text = "New Password must be at least 3 characters.";
                _LabelStatus.ThemeStyle = ThemeStyle.Warning_Yellow;
                return;
            }

            // (요청 4) 새 암호/확인 일치
            if (newPassword != confirmPassword)
            {
                _LabelStatus.Text = "New passwords do not match.";
                _LabelStatus.ThemeStyle = ThemeStyle.Warning_Yellow;
                return;
            }

            if (_act.User.ChangePassword(selectedLevel, oldPassword, newPassword))
            {
                _LabelStatus.Text = $"'{selectedLevel}' password changed!";
                _LabelStatus.ThemeStyle = ThemeStyle.Success_Green;

                // 성공 후 폼 초기화
                _TextBoxPW.Text = "";
                _TextBoxNewPassword.Text = "";
                _TextBoxConfirmPassword.Text = "";
                _CheckBoxChangePassword.Checked = false;
                // (폼은 닫지 않고 로그인 모드로 전환)
                UpdateUIState();
            }
            else
            {
                _LabelStatus.Text = "Password change failed. Check old password.";
                _LabelStatus.ThemeStyle = ThemeStyle.Danger_Red;
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (_CheckBoxChangePassword.Checked)
                {
                    // 암호 변경 모드에서 엔터
                    if (sender == _TextBoxConfirmPassword)
                    {
                        PerformChangePassword();
                    }
                    else
                    {
                        SendKeys.Send("{TAB}");
                    }
                }
                else
                {
                    // 로그인 모드에서 엔터
                    PerformLoginOrSelect();
                }
            }
        }
    }
}