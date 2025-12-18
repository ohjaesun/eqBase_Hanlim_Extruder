using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.UI.Controls;
using EQ.UI.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Linq;
using System.Windows.Forms;

using static EQ.Core.Globals;

namespace EQ.UI.UserViews
{
    public partial class Users_View : UserControlBaseplain
    {
        public Users_View()
        {
            InitializeComponent();
        }

        private void Users_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            InitializeGrid();
            LoadUsers();
            UpdateButtonStates();
        }

        private void InitializeGrid()
        {
            _gridUsers.ColumnCount = 6;
            _gridUsers.Columns[0].Name = "User ID";
            _gridUsers.Columns[0].Width = 150;
            _gridUsers.Columns[1].Name = "User Name";
            _gridUsers.Columns[1].Width = 150;
            _gridUsers.Columns[2].Name = "Level";
            _gridUsers.Columns[2].Width = 100;
            _gridUsers.Columns[3].Name = "Locked";
            _gridUsers.Columns[3].Width = 80;
            _gridUsers.Columns[4].Name = "Failed Attempts";
            _gridUsers.Columns[4].Width = 120;
            _gridUsers.Columns[5].Name = "Last Login";
            _gridUsers.Columns[5].Width = 180;
        }

        private void LoadUsers()
        {
            _gridUsers.Rows.Clear();

            var users = ActManager.Instance.Act.User.GetAllUsers();
            foreach (var user in users)
            {
                _gridUsers.Rows.Add(
                    user.UserId,
                    user.UserName,
                    user.Level.ToString(),
                    user.IsLocked ? "Yes" : "No",
                    user.FailedAttempts,
                    user.LastLoginTime?.ToString("yyyy-MM-dd HH:mm:ss") ?? "-"
                );
            }
        }

        private void UpdateButtonStates()
        {
            // Admin 권한만 사용자 관리 가능
            bool isAdmin = ActManager.Instance.Act.User.CheckAccess(UserLevel.Admin);
            
            _btnAdd.Enabled = isAdmin;
            _btnDelete.Enabled = isAdmin && _gridUsers.SelectedRows.Count > 0;
            _btnUnlock.Enabled = isAdmin && _gridUsers.SelectedRows.Count > 0;
            _btnResetPassword.Enabled = isAdmin && _gridUsers.SelectedRows.Count > 0;
        }

        private void _btnAdd_Click(object sender, EventArgs e)
        {
            // Admin 권한 체크
            if (!ActManager.Instance.Act.User.CheckAccess(UserLevel.Admin))
            {
                ActManager.Instance.Act.PopupNoti(
                    L("Access Denied"),
                    L("Only Admin can add users"),
                    NotifyType.Warning);
                return;
            }

            var userId = _TextBox1.Text.Trim();
            var userName = _TextBox2.Text.Trim();
            UserLevel selectedLevel = UserLevel.Operator;

            if (_RadioButton1.Checked)
                 selectedLevel = UserLevel.Operator;
            else if(_RadioButton2.Checked)
                 selectedLevel = UserLevel.Engineer;
            else
                 selectedLevel = UserLevel.Operator;

            if (string.IsNullOrEmpty(userId))
            {
                ActManager.Instance.Act.PopupNoti(
                    L("Input Error"),
                    L("User ID cannot be empty"),
                    NotifyType.Warning);
                return;
            }

            // ID 중복 체크
            if (ActManager.Instance.Act.User.GetUserById(userId) != null)
            {
                ActManager.Instance.Act.PopupNoti(
                    L("Duplicate ID"),
                    L($"User ID '{userId}' already exists"),
                    NotifyType.Warning);
                return;
            }

            var t = ActManager.Instance.Act.PopupYesNo.ConfirmAsync("Add User", $"Are you sure you want to Add User '{userId}'?");
            
                if (t.Result == YesNoResult.Yes)
                {
                    try
                    {
                        ActManager.Instance.Act.User.CreateUser(userId, userName, selectedLevel, userId);
                        LoadUsers();
                    }
                    catch (Exception ex)
                    {
                     
                    }
                }                  
        }

        private void _btnDelete_Click(object sender, EventArgs e)
        {
            if (_gridUsers.SelectedRows.Count == 0)
                return;

            // Admin 권한 체크
            if (!ActManager.Instance.Act.User.CheckAccess(UserLevel.Admin))
            {
                ActManager.Instance.Act.PopupNoti(
                    L("Access Denied"),
                    L("Only Admin can delete users"),
                    NotifyType.Warning);
                return;
            }

            string userId = _gridUsers.SelectedRows[0].Cells[0].Value.ToString();

            var t =ActManager.Instance.Act.PopupYesNo.ConfirmAsync("Delete User", $"Are you sure you want to delete user '{userId}'?");
            
                if (t.Result == YesNoResult.Yes)
                {
                    try
                    {
                        ActManager.Instance.Act.User.DeleteUser(userId);                      
                        LoadUsers();
                    }
                    catch (Exception ex)
                    {
                        ActManager.Instance.Act.PopupNoti(
                            L("Error"),
                            L($"Failed to delete user: {ex.Message}"),
                            NotifyType.Error);
                    }
                }                     
        }

        private void _btnUnlock_Click(object sender, EventArgs e)
        {
            if (_gridUsers.SelectedRows.Count == 0)
                return;

            // Admin 권한 체크
            if (!ActManager.Instance.Act.User.CheckAccess(UserLevel.Admin))
            {
                ActManager.Instance.Act.PopupNoti(
                    L("Access Denied"),
                    L("Only Admin can unlock users"),
                    NotifyType.Warning);
                return;
            }

            string userId = _gridUsers.SelectedRows[0].Cells[0].Value.ToString();


            var t = ActManager.Instance.Act.PopupYesNo.ConfirmAsync("UnlockUser", $"Are you sure you want to UnlockUser '{userId}'?");
            
                if (t.Result == YesNoResult.Yes)
                {
                    try
                    {
                        ActManager.Instance.Act.User.UnlockUser(userId);
                        LoadUsers();
                    }
                    catch (Exception ex)
                    {

                    }
                }                    
        }

        private void _btnResetPassword_Click(object sender, EventArgs e)
        {
            if (_gridUsers.SelectedRows.Count == 0)
                return;

            // Admin 권한 체크
            if (!ActManager.Instance.Act.User.CheckAccess(UserLevel.Admin))
            {
                ActManager.Instance.Act.PopupNoti(
                    L("Access Denied"),
                    L("Only Admin can reset passwords"),
                    NotifyType.Warning);
                return;
            }

            string userId = _gridUsers.SelectedRows[0].Cells[0].Value.ToString();

            //Id를 비밀번호로 설정
            var t = ActManager.Instance.Act.PopupYesNo.ConfirmAsync("Reset Password", $"Are you sure you want to Reset Password '{userId}'?");
            
                if (t.Result == YesNoResult.Yes)
                {
                    try
                    {
                        ActManager.Instance.Act.User.ResetPassword(userId, userId);
                        LoadUsers();
                    }
                    catch (Exception ex)
                    {
                     
                    }
                }                  
        }

        private void _gridUsers_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }
    }
}
