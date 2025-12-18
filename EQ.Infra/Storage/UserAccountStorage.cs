using EQ.Domain.Entities;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EQ.Infra.Storage
{
    /// <summary>
    /// UserAccount 전용 SQLite 스토리지
    /// 다중 사용자 계정 관리 (9.9.4.1~9.9.4.4)
    /// </summary>
    public class UserAccountStorage
    {
        private const string DB_FILE_NAME = "_Backup.db";
        private const string TABLE_NAME = "UserAccount";

        private readonly string _dbPath;

        /// <summary>
        /// 생성자: DB 경로를 지정합니다
        /// </summary>
        /// <param name="path">DB 파일이 저장될 폴더 경로 (예: CommonData)</param>
        public UserAccountStorage(string path)
        {
            _dbPath = Path.Combine(path, DB_FILE_NAME);
            InitializeTable();
        }

        /// <summary>
        /// 테이블 초기화 (UserAccount 테이블이 없으면 생성)
        /// </summary>
        private void InitializeTable()
        {
            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText =
                    $"CREATE TABLE IF NOT EXISTS {TABLE_NAME} (" +
                    "  UserId TEXT PRIMARY KEY," +       // 고유 키
                    "  UserName TEXT NOT NULL," +
                    "  Level INTEGER NOT NULL," +        // UserLevel enum을 int로 저장
                    "  PasswordHash TEXT NOT NULL," +
                    "  IsLocked INTEGER NOT NULL," +     // bool -> int (0/1)
                    "  FailedAttempts INTEGER NOT NULL," +
                    "  LastLoginTime INTEGER," +         // DateTime? -> UnixTime (nullable)
                    "  CreatedTime INTEGER NOT NULL" +   // DateTime -> UnixTime
                    ")";
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 모든 사용자 계정 로드
        /// </summary>
        public List<UserAccount> LoadAll()
        {
            var result = new List<UserAccount>();

            if (!File.Exists(_dbPath))
                return result; // DB 파일이 없으면 빈 리스트 반환

            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM {TABLE_NAME}";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(ReadUserAccount(reader));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 특정 사용자 계정 조회
        /// </summary>
        public UserAccount? FindById(string userId)
        {
            if (!File.Exists(_dbPath))
                return null;

            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM {TABLE_NAME} WHERE UserId = @UserId";
                command.Parameters.AddWithValue("@UserId", userId);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return ReadUserAccount(reader);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 사용자 계정 저장 (INSERT or UPDATE)
        /// </summary>
        public void Save(UserAccount account)
        {
            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();

                // UPSERT (SQLite 3.24.0+)
                command.CommandText =
                    $"INSERT INTO {TABLE_NAME} (UserId, UserName, Level, PasswordHash, IsLocked, FailedAttempts, LastLoginTime, CreatedTime) " +
                    "VALUES(@UserId, @UserName, @Level, @PasswordHash, @IsLocked, @FailedAttempts, @LastLoginTime, @CreatedTime) " +
                    "ON CONFLICT(UserId) DO UPDATE SET " +
                    "  UserName = excluded.UserName, " +
                    "  Level = excluded.Level, " +
                    "  PasswordHash = excluded.PasswordHash, " +
                    "  IsLocked = excluded.IsLocked, " +
                    "  FailedAttempts = excluded.FailedAttempts, " +
                    "  LastLoginTime = excluded.LastLoginTime";

                command.Parameters.AddWithValue("@UserId", account.UserId);
                command.Parameters.AddWithValue("@UserName", account.UserName);
                command.Parameters.AddWithValue("@Level", (int)account.Level);
                command.Parameters.AddWithValue("@PasswordHash", account.PasswordHash);
                command.Parameters.AddWithValue("@IsLocked", account.IsLocked ? 1 : 0);
                command.Parameters.AddWithValue("@FailedAttempts", account.FailedAttempts);
                command.Parameters.AddWithValue("@LastLoginTime", 
                    account.LastLoginTime.HasValue 
                        ? (object)((DateTimeOffset)account.LastLoginTime.Value).ToUnixTimeSeconds() 
                        : DBNull.Value);
                command.Parameters.AddWithValue("@CreatedTime", 
                    ((DateTimeOffset)account.CreatedTime).ToUnixTimeSeconds());

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 사용자 계정 삭제
        /// </summary>
        public bool Delete(string userId)
        {
            if (!File.Exists(_dbPath))
                return false;

            using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM {TABLE_NAME} WHERE UserId = @UserId";
                command.Parameters.AddWithValue("@UserId", userId);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }

        /// <summary>
        /// SqlDataReader에서 UserAccount 객체 생성
        /// </summary>
        private UserAccount ReadUserAccount(SqliteDataReader reader)
        {
            var account = new UserAccount
            {
                UserId = reader.GetString(reader.GetOrdinal("UserId")),
                UserName = reader.GetString(reader.GetOrdinal("UserName")),
                Level = (Domain.Enums.UserLevel)reader.GetInt32(reader.GetOrdinal("Level")),
                PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                IsLocked = reader.GetInt32(reader.GetOrdinal("IsLocked")) == 1,
                FailedAttempts = reader.GetInt32(reader.GetOrdinal("FailedAttempts")),
            };

            // LastLoginTime (nullable)
            int lastLoginOrdinal = reader.GetOrdinal("LastLoginTime");
            if (!reader.IsDBNull(lastLoginOrdinal))
            {
                long unixTime = reader.GetInt64(lastLoginOrdinal);
                account.LastLoginTime = DateTimeOffset.FromUnixTimeSeconds(unixTime).LocalDateTime;
            }

            // CreatedTime
            long createdUnixTime = reader.GetInt64(reader.GetOrdinal("CreatedTime"));
            account.CreatedTime = DateTimeOffset.FromUnixTimeSeconds(createdUnixTime).LocalDateTime;

            return account;
        }
    }
}
