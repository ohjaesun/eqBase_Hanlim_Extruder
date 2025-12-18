using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.UI.Controls;
using EQ.UI.Forms;
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

            // 기본 메시지
            _LabelStatus.Text = "Enter User ID and Password.";
            _LabelStatus.ThemeStyle = ThemeStyle.Primary_Indigo;

            // FormKeyboard 연동 (터치스크린용)
            _TextBoxUserId.Click += TextBoxUserId_Click;
            _TextBoxPW.Click += TextBoxPW_Click;
            _TextBoxNewPassword.Click += TextBoxNewPassword_Click;
            _TextBoxConfirmPassword.Click += TextBoxConfirmPassword_Click;

            var list = ReadUsers();

            _ComboBox1.Items.AddRange(list);
        }

        private void _TextBoxUserId_TextChanged(object sender, EventArgs e)
        {
            // UserId 입력 시 Change Password 체크박스 표시 여부 결정
            string userId = _TextBoxUserId.Text.Trim().ToLower();

            // 비어있지 않으면 Change Password 체크박스 표시
            _CheckBoxChangePassword.Visible = !string.IsNullOrEmpty(userId);
        }

        private void _CheckBoxChangePassword_CheckedChanged(object sender, EventArgs e)
        {
            UpdateUIState();
        }

        /// <summary>
        /// UI 상태 업데이트 (비밀번호 변경 모드)
        /// </summary>
        private void UpdateUIState()
        {
            bool isChangeMode = _CheckBoxChangePassword.Checked;

            // 비밀번호 변경 필드 표시/숨김
            _LabelNewPassword.Visible = isChangeMode;
            _TextBoxNewPassword.Visible = isChangeMode;
            _LabelConfirmPassword.Visible = isChangeMode;
            _TextBoxConfirmPassword.Visible = isChangeMode;

            // 버튼 텍스트 변경
            _ButtonConfirm.Text = isChangeMode ? "Change" : "Login";

            // 기존 비밀번호 레이블 변경
            label2.Text = isChangeMode ? "Old Password:" : "Password:";

            // 상태 레이블 초기화
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
                PerformLogin();
            }
        }

        private void _ButtonCancel_Click(object sender, EventArgs e)
        {
            // Logout 처리 (Lock 상태로 전환)
            // 로그아웃 이력 기록 (로그인 상태에서만)
            if (_act.User.CurrentUser != null)
            {
                _act.AuditTrail.RecordLogout();
            }

            _act.User.Logout();

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 로그인 수행 (모든 사용자 암호 입력 필요)
        /// </summary>
        private void PerformLogin()
        {
            string userId = _TextBoxUserId.Text.Trim();
            string password = _TextBoxPW.Text;

            // 입력 검증
            if (string.IsNullOrEmpty(userId))
            {
                _LabelStatus.Text = "Please enter User ID.";
                _LabelStatus.ThemeStyle = ThemeStyle.Warning_Yellow;
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                _LabelStatus.Text = "Please enter Password.";
                _LabelStatus.ThemeStyle = ThemeStyle.Warning_Yellow;
                return;
            }

            if (userId == password)
            {
                _LabelStatus.Text = "Password is same as User ID.";
                _LabelStatus.ThemeStyle = ThemeStyle.Warning_Yellow;
                return;
            }

            // 로그인 시도
            if (_act.User.Login(userId, password))
            {              
                // 로그인 성공
                var currentUser = _act.User.CurrentUser;
                _act.AuditTrail.SetCurrentUser(currentUser.UserId, currentUser.UserName);
                _act.AuditTrail.RecordLogin(currentUser.UserId, currentUser.UserName, true);

                SaveRecent(userId);
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                // 로그인 실패
                var user = _act.User.GetUserById(userId);
                if (user == null)
                {
                    _LabelStatus.Text = "User ID not found.";
                    _LabelStatus.ThemeStyle = ThemeStyle.Danger_Red;
                }
                else if (user.IsLocked)
                {
                    _LabelStatus.Text = "Account is locked. Contact Admin.";
                    _LabelStatus.ThemeStyle = ThemeStyle.Danger_Red;
                }
                else
                {
                    // 실패 횟수 표시
                    int remaining = 5 - user.FailedAttempts;
                    _LabelStatus.Text = $"Invalid Password. Attempts remaining: {remaining}/5";
                    _LabelStatus.ThemeStyle = ThemeStyle.Danger_Red;

                    // 로그인 실패 이력 기록
                    _act.AuditTrail.RecordLogin(userId, user.UserName, false);
                }
            }
        }

        string[] ReadUsers()
        {
            string filePath = Application.StartupPath + "\\ModelData\\recent_users.txt";

            if (!File.Exists(filePath))
                return Array.Empty<string>();

            return File.ReadAllLines(filePath)
                       .Where(x => !string.IsNullOrWhiteSpace(x))
                       .Take(5)
                       .ToArray();
        }
        void SaveRecent(string newValue)
        {
            string filePath = Application.StartupPath + "\\ModelData\\recent_users.txt";
            List<string> list = new List<string>();

            // 파일 있으면 읽기
            if (File.Exists(filePath))
                list = File.ReadAllLines(filePath).ToList();

            // 공백 제거
            list = list.Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            // 이미 있으면 제거
            list.Remove(newValue);

            // 맨 앞(1번)에 추가
            list.Insert(0, newValue);

            // 5개 초과 제거
            if (list.Count > 5)
                list = list.Take(5).ToList();

            // 파일 저장
            File.WriteAllLines(filePath, list);
        }

        /// <summary>
        /// 비밀번호 변경
        /// </summary>
        private void PerformChangePassword()
        {
            string userId = _TextBoxUserId.Text.Trim();
            string oldPassword = _TextBoxPW.Text;
            string newPassword = _TextBoxNewPassword.Text;
            string confirmPassword = _TextBoxConfirmPassword.Text;

            // 입력 검증
            if (string.IsNullOrEmpty(userId))
            {
                _LabelStatus.Text = "Please enter User ID.";
                _LabelStatus.ThemeStyle = ThemeStyle.Warning_Yellow;
                return;
            }

            // 비밀번호 길이 검증 (최소 3자)
            if (string.IsNullOrEmpty(newPassword) || newPassword.Length <= 2)
            {
                _LabelStatus.Text = "New Password must be at least 3 characters.";
                _LabelStatus.ThemeStyle = ThemeStyle.Warning_Yellow;
                return;
            }

            // 비밀번호 일치 확인
            if (newPassword != confirmPassword)
            {
                _LabelStatus.Text = "New passwords do not match.";
                _LabelStatus.ThemeStyle = ThemeStyle.Warning_Yellow;
                return;
            }

            // 비밀번호 변경 시도
            if (_act.User.ChangePassword(userId, oldPassword, newPassword))
            {
                _LabelStatus.Text = $"'{userId}' password changed!";
                _LabelStatus.ThemeStyle = ThemeStyle.Success_Green;

                // 비밀번호 변경 이력 기록
                var user = _act.User.GetUserById(userId);
                if (user != null)
                {
                    _act.AuditTrail.RecordPasswordChanged(userId, user.UserName, true);
                }

                // 성공 후 폼 초기화
                _TextBoxPW.Text = "";
                _TextBoxNewPassword.Text = "";
                _TextBoxConfirmPassword.Text = "";
                _CheckBoxChangePassword.Checked = false;
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
                    // 비밀번호 변경 모드에서 엔터
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
                    PerformLogin();
                }
            }
        }

        #region FormKeyboard 연동 (터치스크린용)

        private void TextBoxUserId_Click(object sender, EventArgs e)
        {
            using (var keyboard = new FormKeyboard("User ID", _TextBoxUserId.Text))
            {
                if (keyboard.ShowDialog() == DialogResult.OK)
                {
                    _TextBoxUserId.Text = keyboard.ResultValue;
                }
            }
        }

        private void TextBoxPW_Click(object sender, EventArgs e)
        {
            // 비밀번호는 초기값 없이 시작
            using (var keyboard = new FormKeyboard(label2.Text, ""))
            {
                if (keyboard.ShowDialog() == DialogResult.OK)
                {
                    _TextBoxPW.Text = keyboard.ResultValue;
                }
            }
        }

        private void TextBoxNewPassword_Click(object sender, EventArgs e)
        {
            using (var keyboard = new FormKeyboard("New Password", ""))
            {
                if (keyboard.ShowDialog() == DialogResult.OK)
                {
                    _TextBoxNewPassword.Text = keyboard.ResultValue;
                }
            }
        }

        private void TextBoxConfirmPassword_Click(object sender, EventArgs e)
        {
            using (var keyboard = new FormKeyboard("Confirm Password", ""))
            {
                if (keyboard.ShowDialog() == DialogResult.OK)
                {
                    _TextBoxConfirmPassword.Text = keyboard.ResultValue;
                }
            }
        }

        #endregion

        private void _ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            _TextBoxUserId.Text = _ComboBox1.SelectedItem.ToString();
        }
    }
}