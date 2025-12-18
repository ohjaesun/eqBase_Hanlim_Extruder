using EQ.Core.Act;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.Common.Logs;
using EQ.Common.Helper; // CIni 사용 (마이그레이션용)
using EQ.Infra.Storage;
using System.Collections.Generic;
using System.IO;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace EQ.Core.Act
{
    /// <summary>
    /// 사용자 인증 및 권한 관리를 담당하는 모듈
    /// (리팩토링: 다중 사용자 계정 시스템 - 9.9.4.1~9.9.4.4)
    /// </summary>
    public class ActUser : ActComponent
    {
        // 로그인 실패 제한 (9.9.4.2)
        private const int MAX_FAILED_ATTEMPTS = 5;

        /// <summary>
        /// 현재 로그인된 사용자 (null이면 Lock 상태)
        /// </summary>
        public UserAccount? CurrentUser { get; private set; }

        /// <summary>
        /// 현재 사용자 레벨 (Lock: 잠금 상태, Operator: 기본, Engineer/Admin: 로그인 필요)
        /// </summary>
        public UserLevel CurrentUserLevel => CurrentUser?.Level ?? UserLevel.Lock;

        /// <summary>
        /// 현재 사용자 ID (Operator면 "Operator" 반환)
        /// </summary>
        public string CurrentUserId => CurrentUser?.UserId ?? "Operator";

        // 사용자 DB 저장소
        private readonly UserAccountStorage _storage;
        private readonly string _userDbPath;

        // 마이그레이션용 (기존 INI 파일)
        private readonly string _legacyIniPath;

        public ActUser(ACT act) : base(act)
        {
            // 사용자 데이터는 레시피와 무관하게 공통 "CommonData" 폴더에 저장
            _userDbPath = Path.Combine(Environment.CurrentDirectory, "CommonData");
            Directory.CreateDirectory(_userDbPath);

            _storage = new UserAccountStorage(_userDbPath);
            _legacyIniPath = Path.Combine(_userDbPath, "UserData.ini");

            // 기본값: Lock 상태로 설정 (로그인 필요)
            CurrentUser = null;
        }

        /// <summary>
        /// FormSplash에서 호출. 사용자 계정 초기화 및 마이그레이션
        /// </summary>
        public void LoadUsers()
        {
            // 1. 기존 계정이 있는지 확인
            var existingUsers = _storage.LoadAll();

            // 2. 계정이 없으면 마이그레이션 시도
            if (existingUsers.Count == 0)
            {
                MigrateFromLegacySystem();
            }

            Log.Instance.Info($"[ActUser] 사용자 계정 로드 완료. 계정 수: {_storage.LoadAll().Count}");
        }

        /// <summary>
        /// 기존 INI 파일에서 Engineer/Admin 계정을 마이그레이션
        /// </summary>
        private void MigrateFromLegacySystem()
        {
            if (!File.Exists(_legacyIniPath))
            {
                // INI 파일이 없으면 기본 계정 생성
                CreateDefaultAccounts();
                return;
            }

            try
            {
                var iniStorage = new CIni("UserData"); // CommonData/UserData.ini

                // Engineer 계정 마이그레이션
                string engHash = iniStorage.ReadString("Password", UserLevel.Engineer.ToString(), "");
                if (!string.IsNullOrEmpty(engHash))
                {
                    var engAccount = new UserAccount("engineer", "Engineer", UserLevel.Engineer, engHash);
                    _storage.Save(engAccount);
                    Log.Instance.Info("[ActUser] Engineer 계정 마이그레이션 완료.");
                }

                // Admin 계정 마이그레이션
                string adminHash = iniStorage.ReadString("Password", UserLevel.Admin.ToString(), "");
                if (!string.IsNullOrEmpty(adminHash))
                {
                    var adminAccount = new UserAccount("admin", "Administrator", UserLevel.Admin, adminHash);
                    _storage.Save(adminAccount);
                    Log.Instance.Info("[ActUser] Admin 계정 마이그레이션 완료.");
                }

                // INI 파일 백업
                string backupPath = _legacyIniPath + ".backup";
                File.Move(_legacyIniPath, backupPath);
                Log.Instance.Info($"[ActUser] 기존 INI 파일 백업: {backupPath}");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[ActUser] 마이그레이션 실패: {ex.Message}");
                CreateDefaultAccounts();
            }
        }

        /// <summary>
        /// 기본 계정 생성 (engineer/admin)
        /// </summary>
        private void CreateDefaultAccounts()
        {
            string engHash = ComputeSha256Hash("engineer");
            string adminHash = ComputeSha256Hash("admin");

            var engAccount = new UserAccount("engineer", "Engineer", UserLevel.Engineer, engHash);
            var adminAccount = new UserAccount("admin", "Administrator", UserLevel.Admin, adminHash);

            _storage.Save(engAccount);
            _storage.Save(adminAccount);

            Log.Instance.Info("[ActUser] 기본 계정(engineer/admin) 생성 완료.");
        }

        #region 로그인/로그아웃

        /// <summary>
        /// 사용자 로그인 (9.9.4.1, 9.9.4.2)
        /// </summary>
        /// <param name="userId">사용자 ID</param>
        /// <param name="password">비밀번호</param>
        /// <returns>로그인 성공 여부</returns>
        public bool Login(string userId, string password)
        {
            var user = _storage.FindById(userId);
            if (user == null)
            {
                Log.Instance.Warning($"[Login] 사용자 '{userId}' 존재하지 않음.");
                return false;
            }

            // 9.9.4.2: 계정 잠금 확인
            if (user.IsLocked)
            {
                Log.Instance.Warning($"[Login] 사용자 '{userId}' 계정 잠김.");
                return false;
            }

            string providedHash = ComputeSha256Hash(password);

            if (user.PasswordHash == providedHash)
            {
                // 로그인 성공
                ResetFailedAttempts(user);
                user.LastLoginTime = DateTime.Now;
                _storage.Save(user);

                CurrentUser = user;
                Log.Instance.Info($"[Login] '{userId}' 로그인 성공 (Level: {user.Level})");
                return true;
            }
            else
            {
                // 로그인 실패
                IncrementFailedAttempts(user);
                Log.Instance.Warning($"[Login] '{userId}' 로그인 실패 (실패 횟수: {user.FailedAttempts}/{MAX_FAILED_ATTEMPTS})");
                return false;
            }
        }

        /// <summary>
        /// 로그아웃 (Lock 상태로 전환)
        /// </summary>
        public void Logout()
        {
            if (CurrentUser != null)
            {
                Log.Instance.Info($"[Logout] '{CurrentUser.UserId}' 로그아웃. Lock 상태로 전환.");
                CurrentUser = null;
            }
        }

        #endregion

        #region 로그인 실패 추적 (9.9.4.2)

        /// <summary>
        /// 로그인 실패 횟수 증가 및 잠금 처리
        /// </summary>
        private void IncrementFailedAttempts(UserAccount user)
        {
            user.FailedAttempts++;

            if (user.FailedAttempts >= MAX_FAILED_ATTEMPTS)
            {
                user.IsLocked = true;
                Log.Instance.Warning($"[ActUser] 사용자 '{user.UserId}' 계정 잠김 (실패 {MAX_FAILED_ATTEMPTS}회).");
            }

            _storage.Save(user);
        }

        /// <summary>
        /// 로그인 성공 시 실패 횟수 초기화
        /// </summary>
        private void ResetFailedAttempts(UserAccount user)
        {
            if (user.FailedAttempts > 0)
            {
                user.FailedAttempts = 0;
                _storage.Save(user);
            }
        }

        #endregion

        #region 계정 관리 (9.9.4.1, 9.9.4.3)

        /// <summary>
        /// 새 사용자 계정 생성 (9.9.4.1)
        /// </summary>
        public bool CreateUser(string userId, string userName, UserLevel level, string password)
        {
            // 중복 확인
            if (_storage.FindById(userId) != null)
            {
                Log.Instance.Warning($"[CreateUser] 사용자 ID '{userId}' 이미 존재함.");
                return false;
            }

            // 비밀번호 유효성 검사 (최소 3자)
            if (string.IsNullOrEmpty(password) || password.Length <= 2)
            {
                Log.Instance.Warning($"[CreateUser] 비밀번호가 너무 짧음 (최소 3자 필요).");
                return false;
            }

            string passwordHash = ComputeSha256Hash(password);
            var newUser = new UserAccount(userId, userName, level, passwordHash);

            _storage.Save(newUser);
            Log.Instance.Info($"[CreateUser] 사용자 '{userId}' 생성 완료 (Level: {level}).");
            return true;
        }

        /// <summary>
        /// 사용자 계정 삭제
        /// </summary>
        public bool DeleteUser(string userId)
        {
            // 본인 삭제 방지
            if (CurrentUser?.UserId == userId)
            {
                Log.Instance.Warning($"[DeleteUser] 현재 로그인한 사용자는 삭제할 수 없음.");
                return false;
            }

            bool deleted = _storage.Delete(userId);
            if (deleted)
            {
                Log.Instance.Info($"[DeleteUser] 사용자 '{userId}' 삭제 완료.");
            }
            return deleted;
        }

        /// <summary>
        /// 모든 사용자 목록 조회
        /// </summary>
        public List<UserAccount> GetAllUsers()
        {
            return _storage.LoadAll();
        }

        /// <summary>
        /// 특정 사용자 조회
        /// </summary>
        public UserAccount? GetUserById(string userId)
        {
            return _storage.FindById(userId);
        }

        /// <summary>
        /// 계정 잠금 해제 (9.9.4.3 - Admin만 가능)
        /// </summary>
        public bool UnlockUser(string userId)
        {
            // Admin 권한 확인
            if (CurrentUserLevel != UserLevel.Admin)
            {
                Log.Instance.Warning($"[UnlockUser] 권한 없음 (Admin만 가능).");
                return false;
            }

            var user = _storage.FindById(userId);
            if (user == null) return false;

            user.IsLocked = false;
            user.FailedAttempts = 0;
            _storage.Save(user);

            Log.Instance.Info($"[UnlockUser] 사용자 '{userId}' 잠금 해제 완료.");
            return true;
        }

        /// <summary>
        /// 계정 강제 잠금 (Admin 전용)
        /// </summary>
        public bool LockUser(string userId)
        {
            // Admin 권한 확인
            if (CurrentUserLevel != UserLevel.Admin)
            {
                Log.Instance.Warning($"[LockUser] 권한 없음 (Admin만 가능).");
                return false;
            }

            var user = _storage.FindById(userId);
            if (user == null) return false;

            user.IsLocked = true;
            _storage.Save(user);

            Log.Instance.Info($"[LockUser] 사용자 '{userId}' 잠금 완료.");
            return true;
        }

        #endregion

        #region 비밀번호 관리 (9.9.4.4)

        /// <summary>
        /// 비밀번호 변경 (9.9.4.4)
        /// </summary>
        public bool ChangePassword(string userId, string oldPassword, string newPassword)
        {
            var user = _storage.FindById(userId);
            if (user == null)
            {
                Log.Instance.Warning($"[ChangePassword] 사용자 '{userId}' 존재하지 않음.");
                return false;
            }

            // 기존 비밀번호 확인
            string oldPasswordHash = ComputeSha256Hash(oldPassword);
            if (user.PasswordHash != oldPasswordHash)
            {
                Log.Instance.Warning($"[ChangePassword] 기존 비밀번호 불일치 (User: {userId}).");
                return false;
            }

            // 새 비밀번호 유효성 검사 (최소 3자)
            if (string.IsNullOrEmpty(newPassword) || newPassword.Length <= 2)
            {
                Log.Instance.Warning($"[ChangePassword] 새 비밀번호가 너무 짧음 (User: {userId}).");
                return false;
            }

            // 비밀번호 변경
            user.PasswordHash = ComputeSha256Hash(newPassword);
            _storage.Save(user);

            Log.Instance.Info($"[ChangePassword] 사용자 '{userId}' 비밀번호 변경 완료.");
            return true;
        }

        /// <summary>
        /// Admin이 다른 사용자 비밀번호 재설정
        /// </summary>
        public bool ResetPassword(string userId, string newPassword)
        {
            // Admin 권한 확인
            if (CurrentUserLevel != UserLevel.Admin)
            {
                Log.Instance.Warning($"[ResetPassword] 권한 없음 (Admin만 가능).");
                return false;
            }

            var user = _storage.FindById(userId);
            if (user == null) return false;

            // 새 비밀번호 유효성 검사
            if (string.IsNullOrEmpty(newPassword) || newPassword.Length <= 2)
            {
                Log.Instance.Warning($"[ResetPassword] 새 비밀번호가 너무 짧음.");
                return false;
            }

            user.PasswordHash = ComputeSha256Hash(newPassword);
            _storage.Save(user);

            Log.Instance.Info($"[ResetPassword] 사용자 '{userId}' 비밀번호 재설정 완료.");
            return true;
        }

        #endregion

        #region 권한 체크

        /// <summary>
        /// 특정 기능에 대한 권한 확인
        /// </summary>
        public bool CheckAccess(UserLevel requiredLevel)
        {
            return CurrentUserLevel >= requiredLevel;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// SHA-256 해시 생성
        /// </summary>
        private string ComputeSha256Hash(string rawData)
        {
            if (string.IsNullOrEmpty(rawData)) return "";
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        #endregion
    }
}