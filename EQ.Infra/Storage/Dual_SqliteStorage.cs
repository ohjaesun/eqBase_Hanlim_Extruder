using EQ.Domain.Interface;
using Microsoft.Data.Sqlite; // (EQ.Infra 프로젝트에 NuGet 패키지 'Microsoft.Data.Sqlite' 설치 필요)
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace EQ.Infra.Storage
{
    /// <summary>
    /// 데이터를 SQLite DB에 백업용으로 저장합니다.
    /// (버전 관리, 자동 삭제, 내보내기 기능 포함)
    /// </summary>
    public class SqliteStorage<T> : IDataStorage<T> where T : class, new()
    {
        // DB 파일명은 고정입니다. (레시피 폴더마다 이 파일이 생성됨)
        private const string DB_FILE_NAME = "_Backup.db";

        // 생성자는 비어 있습니다. 모든 경로는 Save/Load에서 동적으로 받습니다.
        public SqliteStorage()
        {
        }

        /// <summary>
        /// (IDataStorage 구현)
        /// 데이터를 '새로운 버전'으로 백업 저장합니다. (INSERT)
        /// </summary>
        public void Save(T data, string path, string key)
        {
            string dbPath = Path.Combine(path, DB_FILE_NAME);
            string tableName = SanitizeTableName(key); // SQL 인젝션 방지

            InitializeTable(dbPath, tableName);

            string strJson = JsonConvert.SerializeObject(data); // 용량을 위해 Indented 안 함
            long currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText =
                    $"INSERT INTO {tableName} (Key, Value, Timestamp) " +
                    "VALUES(@Key, @Value, @Timestamp)";

                command.Parameters.AddWithValue("@Key", key); // (참고: Key 컬럼은 ExportAllByKey를 위해 유지)
                command.Parameters.AddWithValue("@Value", strJson);
                command.Parameters.AddWithValue("@Timestamp", currentTimestamp);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// (IDataStorage 구현)
        /// '가장 최신' 버전의 데이터를 로드합니다.
        /// </summary>
        public T Load(string path, string key)
        {
            string dbPath = Path.Combine(path, DB_FILE_NAME);
            string tableName = SanitizeTableName(key);

            if (!File.Exists(dbPath))
            {
                return new T(); // DB 파일 자체가 없으면 기본값 반환
            }

            InitializeTable(dbPath, tableName); // (테이블이 없을 수도 있으므로 안전하게 호출)

            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText =
                    $"SELECT Value FROM {tableName} " +
                    "WHERE Key = @Key " + // (동일 테이블에 여러 키가 저장될 경우 대비)
                    "ORDER BY Timestamp DESC LIMIT 1";
                command.Parameters.AddWithValue("@Key", key);

                var result = command.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                {
                    return new T(); // 백업이 없으면 기본값 반환
                }

                return JsonConvert.DeserializeObject<T>((string)result);
            }
        }

        #region (백업 전용) 부가 기능

        /// <summary>
        /// [백업 기능 1]
        /// 특정 기간이 지난 오래된 백업 데이터를 삭제합니다. (UI 시작 시 호출 권장)
        /// </summary>
        /// <param name="path">레시피 폴더 경로</param>
        /// <param name="key">정리할 데이터 키 (테이블 이름)</param>
        /// <param name="olderThan">삭제할 기간 (기본값: 30일)</param>
        public void DeleteOldBackups(string path, string key, TimeSpan? olderThan = null)
        {
            try
            {
                string dbPath = Path.Combine(path, DB_FILE_NAME);
                string tableName = SanitizeTableName(key);
                if (!File.Exists(dbPath)) return;

                var cutoff = olderThan ?? TimeSpan.FromDays(60);
                long cutoffTimestamp = DateTimeOffset.UtcNow.Subtract(cutoff).ToUnixTimeSeconds();

                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    // 1. 오래된 데이터 삭제
                    command.CommandText =
                        $"DELETE FROM {tableName} WHERE Timestamp < @cutoffTimestamp AND Key = @Key";
                    command.Parameters.AddWithValue("@cutoffTimestamp", cutoffTimestamp);
                    command.Parameters.AddWithValue("@Key", key);
                    command.ExecuteNonQuery();

                    // 2. [추가] DB 최적화 (파일 크기 축소 및 조각 모음)                    
                    command.CommandText = "VACUUM;";
                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                // (로그) 백업 정리 중 오류
            }
            
        }

        /// <summary>
        /// [백업 기능 2]
        /// 특정 키(Key)에 대해 저장된 모든 백업 기록을
        /// JSON 파일(파일명: 날짜.json)로 지정된 폴더에 내보냅니다.
        /// </summary>
        public int ExportAllByKey(string path, string key, string exportFolderPath)
        {
            try
            {
                string dbPath = Path.Combine(path, DB_FILE_NAME);
                string tableName = SanitizeTableName(key);
                if (!File.Exists(dbPath)) return 0;

                Directory.CreateDirectory(exportFolderPath);
                int filesExported = 0;

                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText =
                        $"SELECT Value, Timestamp FROM {tableName} " +
                        "WHERE Key = @Key ORDER BY Timestamp ASC";
                    command.Parameters.AddWithValue("@Key", key);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string jsonValue = reader.GetString(0);
                            long unixTimestamp = reader.GetInt64(1);
                            DateTimeOffset dateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);

                            string fileName = dateTime.ToLocalTime().ToString("yyyy-MM-dd_HH-mm-ss") + ".json";
                            string filePath = Path.Combine(exportFolderPath, fileName);

                            try
                            {
                                T dataObject = JsonConvert.DeserializeObject<T>(jsonValue);
                                string indentedJson = JsonConvert.SerializeObject(dataObject, Formatting.Indented);
                                File.WriteAllText(filePath, indentedJson);
                                filesExported++;
                            }
                            catch { /* (로그) 내보내기 중 개별 파일 오류 */ }
                        }
                    }
                }
                return filesExported;
            }
            catch
            {
                // (로그) 백업 내보내기 중 오류
                return 0;
            }            
        }

        #endregion

        #region private Helpers

        /// <summary>
        /// 테이블이 존재하는지 확인하고, 없으면 생성합니다.
        /// </summary>
        private void InitializeTable(string dbPath, string tableName)
        {
            using (var connection = new SqliteConnection($"Data Source={dbPath}"))
            {
                connection.Open();
                var command = connection.CreateCommand();

                command.CommandText =
                    $"CREATE TABLE IF NOT EXISTS {tableName} (" +
                    "  Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "  Key TEXT NOT NULL," +
                    "  Timestamp INTEGER NOT NULL," +
                    "  Value TEXT" +
                    ")";
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// SQL 인젝션 방지를 위해 테이블 이름을 검증/정리합니다.
        /// (알파벳, 숫자, 밑줄(_)만 허용)
        /// </summary>
        private string SanitizeTableName(string key)
        {
            // "List_UserOptionUI" -> "List_UserOptionUI"
            // "UserOption" -> "UserOption"
            // (악의적인 코드 "; DROP TABLE ..." 방지)
            var sanitizedKey = Regex.Replace(key, @"[^a-zA-Z0-9_]", "");
            if (string.IsNullOrEmpty(sanitizedKey))
            {
                throw new ArgumentException($"키(Key) '{key}'는 테이블 이름으로 사용할 수 없습니다.");
            }
            return sanitizedKey;
        }

        #endregion
    }
}