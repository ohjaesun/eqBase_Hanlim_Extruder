using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.Domain.Interface;
using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace EQ.Core.Act
{
    /// <summary>
    /// 알람 이력을 DB에 저장하는 ActComponent
    /// </summary>
    public class ActAlarmDB : ActComponent
    {
        private IDataStorage<AlarmData> _storage;
        private readonly string _alarmDbPath;
        private readonly string _alarmDbKey = "AlarmHistory";

        public ActAlarmDB(ACT act) : base(act)
        {
            // 알람 DB는 레시피와 무관하게 "CommonData" 폴더에 저장
            _alarmDbPath = Path.Combine(Environment.CurrentDirectory, "CommonData");
            Directory.CreateDirectory(_alarmDbPath);
        }

        /// <summary>
        /// FormSplash에서 Storage 서비스를 주입받음
        /// </summary>
        public void RegisterStorageService(IDataStorage<AlarmData> storageService)
        {
            _storage = storageService;
        }

        /// <summary>
        /// 에러 알람 발생
        /// </summary>
        internal void WriteLog(ErrorList title, string message, string callerName, string filePath)
        {
            string errTitle = title.ToString();

            // 로그 파일 기록
            string logStr = $"{errTitle},{message},{callerName},{Path.GetFileName(filePath)}";
            Log.Instance.Error(logStr);

            // 1. 알람 이력 DB 저장 (기존 로직 유지)
            SaveAlarm(errTitle, message, callerName, Path.GetFileName(filePath));
                       
        }
        /// <summary>
        /// 알람을 DB에 저장 (PopupNoti에서 호출됨)
        /// </summary>
        private void SaveAlarm(string id, string info, string callName, string filePath)
        {
            if (_storage == null) return;           

            try
            {
                var alarmData = new AlarmData(id, info,callName, filePath);             
                _storage.Save(alarmData, _alarmDbPath, _alarmDbKey);
            }
            catch (Exception ex)
            {
                Common.Logs.Log.Instance.Error($"ActAlarm.SaveAlarm 실패: {ex.Message}");
            }
        }

        /// <summary>
        /// (신규) UI에서 알람 기록을 읽기 위한 헬퍼
        /// </summary>
        public string GetAlarmDbPath()
        {
            // SqliteStorage가 사용할 DB 파일의 전체 경로 반환
            return Path.Combine(_alarmDbPath, "_Backup.db");
        }

        /// <summary>
        /// (신규) UI에서 테이블 이름을 얻기 위한 헬퍼
        /// </summary>
        public string GetAlarmDbKey()
        {
            return _alarmDbKey;
        }
    }
}