using EQ.Core.Act;
using EQ.Domain.Enums;
using EQ.Common.Logs;
using EQ.Common.Helper; // CIni 사용
using System.Collections.Generic;
using System.IO; // Path 사용
using System;
using System.Security.Cryptography; // (신규) SHA-256 사용
using System.Text; // (신규) Encoding 사용

namespace EQ.Core.Act
{
    /// <summary>
    /// 사용자 인증 및 권한 관리를 담당하는 모듈
    /// (리팩토링: 3개 고정 레벨(Operator/Engineer/Admin)의 암호 관리)
    /// </summary>
    public class ActUser : ActComponent
    {
        /// <summary>
        /// 현재 로그인된 사용자 레벨 (기본값: Operator)
        /// </summary>
        public UserLevel CurrentUserLevel { get; private set; }

        // 암호를 저장할 INI 파일
        private CIni _passwordStorage;

        // Engineer와 Admin의 암호 "해시"를 메모리에 보관
        private Dictionary<UserLevel, string> _passwordHashes;

        // 사용자 DB 저장 경로/키
        private readonly string _userDbPath;
        private readonly string _userDbKey = "UserData"; // (파일 이름)

        public ActUser(ACT act) : base(act)
        {
            _passwordHashes = new Dictionary<UserLevel, string>();

            // (요청 3) 프로그램 시작 시 기본 Operator로 설정
            CurrentUserLevel = UserLevel.Operator;

            // 사용자 암호는 레시피와 무관하게 공통 "UserData.ini" 파일에 저장
            _userDbPath = Path.Combine(Environment.CurrentDirectory, "CommonData");
            Directory.CreateDirectory(_userDbPath);
            _passwordStorage = new CIni(_userDbKey); // CommonData/UserData.ini
        }

        /// <summary>
        /// (신규) 문자열을 SHA-256 해시로 변환하는 헬퍼 메서드
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
                    builder.Append(bytes[i].ToString("x2")); // 16진수 문자열로 변환
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// (수정) FormSplash에서 호출. 파일에서 "해시"를 로드합니다.
        /// </summary>
        public void LoadPasswords()
        {
            // 기본값이 없으면 생성
            string defaultEngHash = ComputeSha256Hash("engineer");
            string defaultAdminHash = ComputeSha256Hash("admin");

            // INI에서 저장된 해시를 읽어옴. 없으면 기본 해시 사용
            string engHash = _passwordStorage.ReadString("Password", UserLevel.Engineer.ToString(), defaultEngHash);
            string adminHash = _passwordStorage.ReadString("Password", UserLevel.Admin.ToString(), defaultAdminHash);

            // 평문 마이그레이션 (초기 실행 시 admin/engineer를 해시로 자동 변환)
            if (engHash == "engineer") engHash = defaultEngHash;
            if (adminHash == "admin") adminHash = defaultAdminHash;

            _passwordHashes[UserLevel.Engineer] = engHash;
            _passwordHashes[UserLevel.Admin] = adminHash;

            // INI 파일에 해시값 저장 (없었을 경우 기본값 저장)
            _passwordStorage.WriteString("Password", UserLevel.Engineer.ToString(), engHash);
            _passwordStorage.WriteString("Password", UserLevel.Admin.ToString(), adminHash);

            Log.Instance.Info("사용자 암호(해시) 로드 완료.");
        }

        /// <summary>
        /// (수정) FormLogin에서 호출. 로그인을 시도합니다.
        /// </summary>
        public bool Login(UserLevel level, string password)
        {
            // (요청 3 & 4) Operator는 암호가 없으며 로그인 대상이 아님
            if (level == UserLevel.Operator)
            {
                Log.Instance.Warning("[Login] Operator는 로그인 대상이 아닙니다.");
                return false;
            }

            if (string.IsNullOrEmpty(password))
                return false;

            string providedHash = ComputeSha256Hash(password);

            if (_passwordHashes.TryGetValue(level, out string storedHash) && storedHash == providedHash)
            {
                CurrentUserLevel = level;
                Log.Instance.Info($"[Login] '{level}' 로그인 성공");
                return true;
            }

            Log.Instance.Warning($"[Login] '{level}' 로그인 실패 (암호 불일치)");
            return false;
        }

        /// <summary>
        /// (수정) 로그아웃 시 기본 Operator로 돌아갑니다.
        /// </summary>
        public void Logout()
        {
            if (CurrentUserLevel != UserLevel.Operator)
            {
                Log.Instance.Info($"[Logout] '{CurrentUserLevel}' 로그아웃. Operator로 전환.");
                CurrentUserLevel = UserLevel.Operator;
            }
        }

        /// <summary>
        /// UI 등에서 특정 기능에 대한 권한이 있는지 확인합니다.
        /// </summary>
        public bool CheckAccess(UserLevel requiredLevel)
        {
            // 현재 사용자 레벨이 요구 레벨보다 높거나 같으면 통과
            return CurrentUserLevel >= requiredLevel;
        }

       

        /// <summary>
        /// (신규) FormLogin에서 암호 변경 시 호출
        /// </summary>
        public bool ChangePassword(UserLevel level, string oldPassword, string newPassword)
        {
            if (level == UserLevel.Operator) return false;

            // 1. 기존 암호 확인 (해시 비교)
            string oldPasswordHash = ComputeSha256Hash(oldPassword);
            if (!_passwordHashes.TryGetValue(level, out string storedHash) || storedHash != oldPasswordHash)
            {
                Log.Instance.Warning($"[PWChange] 암호 변경 실패: 기존 암호 불일치 (User: {level})");
                return false;
            }

            // (요청 4) 2자리 이하 설정 불가
            if (string.IsNullOrEmpty(newPassword) || newPassword.Length <= 2)
            {
                Log.Instance.Warning($"[PWChange] 암호 변경 실패: 새 암호가 너무 짧음 (User: {level})");
                return false;
            }

            // 2. 새 암호를 해시로 변환
            string newPasswordHash = ComputeSha256Hash(newPassword);

            // 3. 새 해시로 변경
            _passwordHashes[level] = newPasswordHash;

            // 4. INI 파일에 "새 해시" 저장
            try
            {
                _passwordStorage.WriteString("Password", level.ToString(), newPasswordHash);
                Log.Instance.Info($"[PWChange] '{level}' 사용자의 암호를 성공적으로 변경했습니다.");
                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[PWChange] 암호 변경 저장 실패 (User: {level}): {ex.Message}");
                // (롤백)
                _passwordHashes[level] = storedHash;
                return false;
            }
        }
    }
}