using EQ.Common.Logs;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace EQ.Infra.Storage
{
    /// <summary>
    /// Audit Trail 전용 SQLite 저장소
    /// (사양서 9.9.3 - Audit Trail 요구사항 구현)
    /// - 삭제 불가 (9.9.3.6)
    /// - 장기 보관 (9.9.3.2)
    /// - 데이터 보호 (9.9.3.5)
    /// </summary>
    public class AuditTrailStorage
    {
        private const string DB_FILE_NAME = "AuditTrail.db";
        private const string TABLE_NAME = "AuditTrail";

        private readonly string _dbPath;

        /// <summary>
        /// 생성자: DB 경로를 지정합니다
        /// </summary>
        /// <param name="path">DB 파일이 저장될 폴더 경로 (예: d:\HistoryDB)</param>
        public AuditTrailStorage(string path)
        {
            // 디렉토리 생성 (없으면)
            Directory.CreateDirectory(path);
            
            _dbPath = Path.Combine(path, DB_FILE_NAME);
            InitializeTable();
        }

        /// <summary>
        /// 테이블 초기화 (AuditTrail 테이블이 없으면 생성)
        /// </summary>
        private void InitializeTable()
        {
            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();

                // 테이블 생성
                command.CommandText =
                    $"CREATE TABLE IF NOT EXISTS {TABLE_NAME} (" +
                    "  Id INTEGER PRIMARY KEY AUTOINCREMENT," +  // 자동 증가 키
                    "  Timestamp INTEGER NOT NULL," +             // Unix Timestamp (KST)
                    "  EventType INTEGER NOT NULL," +             // AuditEventType enum
                    "  UserId TEXT NOT NULL," +
                    "  UserName TEXT NOT NULL," +
                    "  Description TEXT NOT NULL," +
                    "  DetailJson TEXT" +                         // JSON 직렬화된 세부정보
                    ")";
                command.ExecuteNonQuery();

                // 인덱스 생성 (조회 성능 향상)
                command.CommandText = $"CREATE INDEX IF NOT EXISTS idx_timestamp ON {TABLE_NAME}(Timestamp)";
                command.ExecuteNonQuery();

                command.CommandText = $"CREATE INDEX IF NOT EXISTS idx_eventtype ON {TABLE_NAME}(EventType)";
                command.ExecuteNonQuery();

                command.CommandText = $"CREATE INDEX IF NOT EXISTS idx_userid ON {TABLE_NAME}(UserId)";
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 새 이력 추가 (INSERT만 가능, UPDATE/DELETE 불가)
        /// (사양서 9.9.3.6 - 삭제 불가능)
        /// </summary>
        public void AddEntry(AuditTrailEntry entry)
        {
            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText =
                    $"INSERT INTO {TABLE_NAME} (Timestamp, EventType, UserId, UserName, Description, DetailJson) " +
                    "VALUES(@Timestamp, @EventType, @UserId, @UserName, @Description, @DetailJson)";

                command.Parameters.AddWithValue("@Timestamp",
                    ((DateTimeOffset)entry.Timestamp).ToUnixTimeSeconds());
                command.Parameters.AddWithValue("@EventType", (int)entry.EventType);
                command.Parameters.AddWithValue("@UserId", entry.UserId);
                command.Parameters.AddWithValue("@UserName", entry.UserName);
                command.Parameters.AddWithValue("@Description", entry.Description);
                command.Parameters.AddWithValue("@DetailJson", entry.DetailJson ?? string.Empty);

                command.ExecuteNonQuery();

                //Log로도 남긴다
                Log.Instance.SaveData($"{((DateTimeOffset)entry.Timestamp).ToUnixTimeSeconds()}^{entry.EventType}^{entry.UserId}^{entry.UserName}^{entry.Description}^{entry.DetailJson}");
            }
        }

        /// <summary>
        /// 모든 이력 조회 (최근 순으로 정렬)
        /// </summary>
        public List<AuditTrailEntry> LoadAll()
        {
            var result = new List<AuditTrailEntry>();

            if (!File.Exists(_dbPath))
                return result;

            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM {TABLE_NAME} ORDER BY Timestamp DESC";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(ReadEntry(reader));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 날짜 범위로 이력 조회
        /// (사양서 9.9.3.7 - 일시 기록)
        /// </summary>
        public List<AuditTrailEntry> LoadByDateRange(DateTime start, DateTime end)
        {
            var result = new List<AuditTrailEntry>();

            if (!File.Exists(_dbPath))
                return result;

            long startUnix = ((DateTimeOffset)start).ToUnixTimeSeconds();
            long endUnix = ((DateTimeOffset)end).ToUnixTimeSeconds();

            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = 
                    $"SELECT * FROM {TABLE_NAME} " +
                    "WHERE Timestamp >= @Start AND Timestamp <= @End " +
                    "ORDER BY Timestamp DESC";

                command.Parameters.AddWithValue("@Start", startUnix);
                command.Parameters.AddWithValue("@End", endUnix);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(ReadEntry(reader));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 이벤트 유형별 조회
        /// (사양서 9.9.3.7 - 유형별 분류)
        /// </summary>
        public List<AuditTrailEntry> LoadByEventType(AuditEventType eventType)
        {
            var result = new List<AuditTrailEntry>();

            if (!File.Exists(_dbPath))
                return result;

            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    $"SELECT * FROM {TABLE_NAME} " +
                    "WHERE EventType = @EventType " +
                    "ORDER BY Timestamp DESC";

                command.Parameters.AddWithValue("@EventType", (int)eventType);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(ReadEntry(reader));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 사용자별 조회
        /// (사양서 9.9.3.7 - 사용자별 이력)
        /// </summary>
        public List<AuditTrailEntry> LoadByUser(string userId)
        {
            var result = new List<AuditTrailEntry>();

            if (!File.Exists(_dbPath))
                return result;

            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText =
                    $"SELECT * FROM {TABLE_NAME} " +
                    "WHERE UserId = @UserId " +
                    "ORDER BY Timestamp DESC";

                command.Parameters.AddWithValue("@UserId", userId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(ReadEntry(reader));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// CSV로 내보내기
        /// (사양서 9.9.3.1, 9.9.3.3 - Print out, PDF export, USB 이동)
        /// </summary>
        public bool ExportToCsv(string filePath, DateTime? start = null, DateTime? end = null)
        {
            try
            {
                var entries = (start.HasValue && end.HasValue)
                    ? LoadByDateRange(start.Value, end.Value)
                    : LoadAll();

                using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    // 헤더
                    writer.WriteLine("DateTime,EventType,UserId,UserName,Description,DetailJson");

                    // 데이터
                    foreach (var entry in entries)
                    {
                        var line = $"\"{entry.Timestamp:yyyy-MM-dd HH:mm:ss}\"^" +
                                   $"\"{entry.EventType}\"^" +
                                   $"\"{EscapeCsv(entry.UserId)}\"^" +
                                   $"\"{EscapeCsv(entry.UserName)}\"^" +
                                   $"\"{EscapeCsv(entry.Description)}\"^" +
                                   $"\"{EscapeCsv(entry.DetailJson)}\"";
                        writer.WriteLine(line);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// CSV 이스케이프 처리
        /// </summary>
        private string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value.Replace("\"", "\"\"");
        }

        /// <summary>
        /// SqlDataReader에서 AuditTrailEntry 객체 생성
        /// </summary>
        private AuditTrailEntry ReadEntry(SqliteDataReader reader)
        {
            var entry = new AuditTrailEntry
            {
                Id = reader.GetInt64(reader.GetOrdinal("Id")),
                EventType = (AuditEventType)reader.GetInt32(reader.GetOrdinal("EventType")),
                UserId = reader.GetString(reader.GetOrdinal("UserId")),
                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                Description = reader.GetString(reader.GetOrdinal("Description")),
            };

            // Timestamp 변환
            long unixTime = reader.GetInt64(reader.GetOrdinal("Timestamp"));
            entry.Timestamp = DateTimeOffset.FromUnixTimeSeconds(unixTime).LocalDateTime;

            // DetailJson (nullable)
            int detailOrdinal = reader.GetOrdinal("DetailJson");
            if (!reader.IsDBNull(detailOrdinal))
            {
                entry.DetailJson = reader.GetString(detailOrdinal);
            }

            return entry;
        }
    }
}
