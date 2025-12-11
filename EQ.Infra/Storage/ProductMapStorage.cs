using EQ.Domain.Entities;
using EQ.Domain.Interface;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;

namespace EQ.Infra.Storage
{
    public class ProductMapStorage<T> : IDataStorage<ProductMap<T>> where T : struct, IProductUnit
    {
        private const string DB_FILE_NAME = "_ProductBackup.db";

        public void Save(ProductMap<T> data, string path, string key)
        {
            if (data == null) return;
            Directory.CreateDirectory(path);

            // 1. 파일 저장 (.bin)
            string binPath = Path.Combine(path, $"{key}.bin");
            File.WriteAllBytes(binPath, data.ToByteArray());

            // 2. DB 저장 (SQLite BLOB)
            SaveToDb(data, path, key);
        }

        public ProductMap<T> Load(string path, string key)
        {
            string binPath = Path.Combine(path, $"{key}.bin");

            if (File.Exists(binPath))
            {
                try
                {
                    return ProductMap<T>.FromByteArray(File.ReadAllBytes(binPath));
                }
                catch { /* 파일 오류 시 DB 로드 시도 */ }
            }

            return LoadFromDb(path, key);
        }

        // --- [추가됨] 백업 삭제 및 최적화 기능 ---
        // [수정] 반환 타입을 void -> int로 변경 (삭제된 개수 반환)
        public int DeleteOldBackups(string path, string key, TimeSpan olderThan, long maxSizeBytes = 0, bool performVacuum = true)
        {
            int deletedCount = 0; // 삭제된 개수

            try
            {
                string dbPath = Path.Combine(path, DB_FILE_NAME);
                if (!File.Exists(dbPath)) return 0;

                long cutoffTimestamp = DateTimeOffset.UtcNow.Subtract(olderThan).ToUnixTimeSeconds();

                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();
                    var command = connection.CreateCommand();

                    // 1. 데이터 삭제
                    command.CommandText = $"DELETE FROM {key} WHERE Timestamp < @cutoffTimestamp";
                    command.Parameters.AddWithValue("@cutoffTimestamp", cutoffTimestamp);

                    try
                    {                      
                        deletedCount = command.ExecuteNonQuery();
                    }
                    catch { return 0; }

                    // 2. VACUUM 수행 (옵션이 켜져있고, 삭제된 데이터가 있을 때만 하는 것이 좋음)
                    // 하지만 MagazineStorage에서 일괄 처리할 것이므로 여기서는 옵션에 따름
                    if (performVacuum && deletedCount > 0)
                    {
                        command.CommandText = "VACUUM;";
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                // Log...
            }

            return deletedCount; // 삭제된 수 반환
        }

        private long GetPragmaValue(SqliteConnection conn, string pragmaName)
        {
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = $"PRAGMA {pragmaName};";
                object res = cmd.ExecuteScalar();
                return res != null ? Convert.ToInt64(res) : 0;
            }
        }

        // --- 내부 저장 로직 ---
        private void SaveToDb(ProductMap<T> data, string path, string key)
        {
            try
            {
                string dbPath = Path.Combine(path, DB_FILE_NAME);
                InitializeTable(dbPath, key);

                using (var conn = new SqliteConnection($"Data Source={dbPath}"))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = $"INSERT INTO {key} (Timestamp, Value) VALUES (@Time, @Val)";
                    cmd.Parameters.AddWithValue("@Time", DateTimeOffset.UtcNow.ToUnixTimeSeconds());
                    cmd.Parameters.AddWithValue("@Val", data.ToByteArray());
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
        }

        private ProductMap<T> LoadFromDb(string path, string key)
        {
            try
            {
                string dbPath = Path.Combine(path, DB_FILE_NAME);
                if (!File.Exists(dbPath)) return null;

                using (var conn = new SqliteConnection($"Data Source={dbPath}"))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = $"SELECT Value FROM {key} ORDER BY Timestamp DESC LIMIT 1";

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return ProductMap<T>.FromByteArray((byte[])reader["Value"]);
                        }
                    }
                }
            }
            catch { }
            return null;
        }

        private void InitializeTable(string dbPath, string tableName)
        {
            using (var conn = new SqliteConnection($"Data Source={dbPath}"))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = $"CREATE TABLE IF NOT EXISTS {tableName} (Id INTEGER PRIMARY KEY, Timestamp INTEGER, Value BLOB)";
                cmd.ExecuteNonQuery();
            }
        }
    }
}