using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.Infra.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EQ.Core.Service
{
    /// <summary>
    /// Audit Trail 이력 관리 서비스
    /// (사양서 9.9.3, 8.12 - Audit Trail 요구사항 구현)
    /// </summary>
    public class AuditTrailService
    {
        private readonly AuditTrailStorage _storage;
        private string _currentUserId = "SYSTEM";
        private string _currentUserName = "시스템";

        public AuditTrailService(string dbPath)
        {
            _storage = new AuditTrailStorage(dbPath);
        }

        /// <summary>
        /// 현재 로그인한 사용자 설정
        /// </summary>
        public void SetCurrentUser(string userId, string userName)
        {
            _currentUserId = userId;
            _currentUserName = userName;
        }

        #region 간편 기록 메서드

        /// <summary>
        /// 로그인 이력 기록
        /// (사양서 8.12, 9.9.3.7 - Login/Logout 이력)
        /// </summary>
        public void RecordLogin(string userId, string userName, bool success)
        {
            var eventType = success ? AuditEventType.LoginSuccess : AuditEventType.LoginFailed;
            var description = success
                ? $"사용자 '{userName}' 로그인 성공"
                : $"사용자 '{userId}' 로그인 실패";

            var entry = new AuditTrailEntry(
                eventType,
                userId,
                userName,
                description
            );

            _storage.AddEntry(entry);
        }

        /// <summary>
        /// 로그아웃 이력 기록
        /// (사양서 8.12, 9.9.3.7 - Login/Logout 이력)
        /// </summary>
        public void RecordLogout()
        {
            var entry = new AuditTrailEntry(
                AuditEventType.Logout,
                _currentUserId,
                _currentUserName,
                $"사용자 '{_currentUserName}' 로그아웃"
            );

            _storage.AddEntry(entry);
        }

        public void RecordSystemCrash()
        {
            var entry = new AuditTrailEntry(
                AuditEventType.SystemCrash,
                _currentUserId,
                _currentUserName,
                $"사용자 '{_currentUserName}' SystemCrash"
            );

            _storage.AddEntry(entry);
        }

        /// <summary>
        /// 사용자 생성 이력 기록
        /// </summary>
        public void RecordUserCreated(string newUserId, string newUserName)
        {
            var entry = new AuditTrailEntry(
                AuditEventType.UserCreated,
                _currentUserId,
                _currentUserName,
                $"새 사용자 생성: {newUserId} ({newUserName})"
            );

            _storage.AddEntry(entry);
        }

        /// <summary>
        /// 사용자 삭제 이력 기록
        /// </summary>
        public void RecordUserDeleted(string deletedUserId, string deletedUserName)
        {
            var entry = new AuditTrailEntry(
                AuditEventType.UserDeleted,
                _currentUserId,
                _currentUserName,
                $"사용자 삭제: {deletedUserId} ({deletedUserName})"
            );

            _storage.AddEntry(entry);
        }

        /// <summary>
        /// 사용자 잠금 이력 기록
        /// </summary>
        public void RecordUserLocked(string lockedUserId, string reason)
        {
            var entry = new AuditTrailEntry(
                AuditEventType.UserLocked,
                lockedUserId,
                lockedUserId,
                $"사용자 계정 잠금: {reason}"
            );

            _storage.AddEntry(entry);
        }

        /// <summary>
        /// 사용자 잠금 해제 이력 기록
        /// </summary>
        public void RecordUserUnlocked(string unlockedUserId, string unlockedUserName)
        {
            var entry = new AuditTrailEntry(
                AuditEventType.UserUnlocked,
                _currentUserId,
                _currentUserName,
                $"사용자 잠금 해제: {unlockedUserId} ({unlockedUserName})"
            );

            _storage.AddEntry(entry);
        }

        /// <summary>
        /// 비밀번호 변경 이력 기록
        /// </summary>
        public void RecordPasswordChanged(string targetUserId, string targetUserName, bool isSelfChange)
        {
            var eventType = isSelfChange ? AuditEventType.PasswordChanged : AuditEventType.PasswordReset;
            var description = isSelfChange
                ? $"사용자 '{targetUserName}' 비밀번호 변경"
                : $"관리자가 '{targetUserName}' 비밀번호 재설정";

            var entry = new AuditTrailEntry(
                eventType,
                _currentUserId,
                _currentUserName,
                description
            );

            _storage.AddEntry(entry);
        }

        /// <summary>
        /// Recipe 로드 이력 기록
        /// (사양서 8.12, 9.9.3.7 - Recipe open/edit 이력)
        /// </summary>
        public void RecordRecipeLoaded(string recipeName)
        {
            var detail = new { RecipeName = recipeName };

            var entry = new AuditTrailEntry(
                AuditEventType.RecipeLoaded,
                _currentUserId,
                _currentUserName,
                $"Recipe 로드: {recipeName}",
                JsonConvert.SerializeObject(detail)
            );

            _storage.AddEntry(entry);
        }

        /// <summary>
        /// Recipe 수정 이력 기록
        /// (사양서 8.12, 9.9.3.7 - Recipe open/edit 이력)
        /// </summary>
        public void RecordRecipeModified(string recipeName , int recipeNo)
        {
            var detail = new { RecipeName = recipeName , Index = recipeNo };

            var entry = new AuditTrailEntry(
                AuditEventType.RecipeModified,
                _currentUserId,
                _currentUserName,
                $"Modify: [{recipeNo}]{recipeName}",
                JsonConvert.SerializeObject(detail)
            );

            _storage.AddEntry(entry);
        }

        /// <summary>
        /// 파라미터 변경 이력 기록
        /// (사양서 5.4.2, 9.9.3.7 - 파라미터 변경 이력)
        /// </summary>
        public void RecordParameterChanged(int recipeNo , string parameterName, object oldValue, object newValue)
        {
            var detail = new
            {
                RecipeName = recipeNo ,
                ParameterName = parameterName,
                OldValue = oldValue?.ToString() ?? "null",
                NewValue = newValue?.ToString() ?? "null"
            };

            var entry = new AuditTrailEntry(
                AuditEventType.ParameterChanged,
                _currentUserId,
                _currentUserName,
                $"{parameterName} ({oldValue} → {newValue})",
                JsonConvert.SerializeObject(detail)
            );

            _storage.AddEntry(entry);
        }

        /// <summary>
        /// 시퀀스 시작 이력 기록
        /// (사양서 8.12 - Run/Stop 이력)
        /// </summary>
        public void RecordSequenceStarted(string sequenceName = "")
        {
            var entry = new AuditTrailEntry(
                AuditEventType.SequenceStarted,
                _currentUserId,
                _currentUserName,
                $"시퀀스 시작{(string.IsNullOrEmpty(sequenceName) ? "" : $": {sequenceName}")}"
            );

            _storage.AddEntry(entry);
        }

        /// <summary>
        /// 시퀀스 정지 이력 기록
        /// (사양서 8.12 - Run/Stop 이력)
        /// </summary>
        public void RecordSequenceStopped(string reason = "")
        {
            var entry = new AuditTrailEntry(
                AuditEventType.SequenceStopped,
                _currentUserId,
                _currentUserName,
                $"시퀀스 정지{(string.IsNullOrEmpty(reason) ? "" : $": {reason}")}"
            );

            _storage.AddEntry(entry);
        }

        

        /// <summary>
        /// 비상정지 이력 기록
        /// (사양서 9.5.4 - 비상정지 Audit Trail 기록)
        /// </summary>
        public void RecordEmergencyStop(string reason)
        {
            var entry = new AuditTrailEntry(
                AuditEventType.EmergencyStop,
                _currentUserId,
                _currentUserName,
                $"비상정지 발생: {reason}"
            );

            _storage.AddEntry(entry);
        }

        /// <summary>
        /// 알람 발생 이력 기록
        /// </summary>
        public void RecordAlarm(string alarmCode, string message)
        {
            var detail = new { AlarmCode = alarmCode, Message = message };

            var entry = new AuditTrailEntry(
                AuditEventType.AlarmOccurred,
                _currentUserId,
                _currentUserName,
                $"알람 발생: [{alarmCode}] {message}",
                JsonConvert.SerializeObject(detail)
            );

            _storage.AddEntry(entry);
        }

        /// <summary>
        /// 시스템 시작 이력 기록
        /// </summary>
        public void RecordSystemStartup()
        {
            var entry = new AuditTrailEntry(
                AuditEventType.SystemStartup,
                "SYSTEM",
                "SYSTEM",
                "Program Start"
            );

            _storage.AddEntry(entry);
        }

        /// <summary>
        /// 시스템 종료 이력 기록
        /// </summary>
        public void RecordSystemShutdown()
        {
            var entry = new AuditTrailEntry(
                AuditEventType.SystemShutdown,
                _currentUserId,
                _currentUserName,
                "Program End"
            );

            _storage.AddEntry(entry);
        }

        #endregion

        #region 조회 메서드

        /// <summary>
        /// 최근 이력 조회
        /// </summary>
        public List<AuditTrailEntry> GetRecentEntries(int count = 100)
        {
            var all = _storage.LoadAll();
            return all.Count > count ? all.GetRange(0, count) : all;
        }

        /// <summary>
        /// 날짜 범위로 이력 조회
        /// </summary>
        public List<AuditTrailEntry> GetEntriesByDateRange(DateTime start, DateTime end)
        {
            return _storage.LoadByDateRange(start, end);
        }

        /// <summary>
        /// 이벤트 유형별 조회
        /// </summary>
        public List<AuditTrailEntry> GetEntriesByEventType(AuditEventType eventType)
        {
            return _storage.LoadByEventType(eventType);
        }

        /// <summary>
        /// 사용자별 조회
        /// </summary>
        public List<AuditTrailEntry> GetEntriesByUser(string userId)
        {
            return _storage.LoadByUser(userId);
        }

        #endregion

        #region 내보내기 메서드

        /// <summary>
        /// CSV로 내보내기
        /// (사양서 9.9.3.1, 9.9.3.3 - Export 기능)
        /// </summary>
        public bool ExportToCsv(string filePath, DateTime? start = null, DateTime? end = null)
        {
            return _storage.ExportToCsv(filePath, start, end);
        }

        /// <summary>
        /// CSV로 내보내기 (필터링된 데이터 직접 전달)
        /// </summary>
        public bool ExportToCsv(string filePath, List<AuditTrailEntry> entries)
        {
            return _storage.ExportToCsv(filePath, entries);
        }

        /// <summary>
        /// PDF로 내보내기
        /// (사양서 9.9.3.1 - PDF Export 기능)
        /// </summary>
        public bool ExportToPdf(string filePath, DateTime? start = null, DateTime? end = null)
        {
            return _storage.ExportToPdf(filePath, start, end);
        }

        /// <summary>
        /// PDF로 내보내기 (필터링된 데이터 직접 전달)
        /// </summary>
        public bool ExportToPdf(string filePath,string id, List<AuditTrailEntry> entries)
        {
            return _storage.ExportToPdf(filePath,id, entries);
        }

        #endregion
    }
}
