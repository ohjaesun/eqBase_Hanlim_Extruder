using EQ.Common.Logs;
using EQ.Domain.Entities;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EQ.Infra.Storage
{
    /// <summary>
    /// 차트 데이터 SQLite 저장소
    /// </summary>
    public class ChartDataStorage
    {
        private readonly string _dbPath;

        public ChartDataStorage(string dbPath)
        {
            _dbPath = dbPath;
            
            // DB 디렉토리 생성
            var directory = Path.GetDirectoryName(_dbPath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }
            
            InitializeDatabase();
        }

        /// <summary>
        /// 데이터베이스 초기화
        /// </summary>
        private void InitializeDatabase()
        {
            try
            {
                using var connection = new SqliteConnection($"Data Source={_dbPath}");
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = @"
                    CREATE TABLE IF NOT EXISTS ChartDataRuns (
                        RunName TEXT PRIMARY KEY,
                        StartTime INTEGER NOT NULL,
                        EndTime INTEGER,
                        DataCount INTEGER
                    );

                    CREATE TABLE IF NOT EXISTS ChartDataPoints (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        RunName TEXT NOT NULL,
                        ItemName TEXT NOT NULL,
                        Timestamp INTEGER NOT NULL,
                        Value REAL NOT NULL,
                        FOREIGN KEY (RunName) REFERENCES ChartDataRuns(RunName)
                    );

                    CREATE INDEX IF NOT EXISTS idx_runname_item 
                    ON ChartDataPoints(RunName, ItemName);
                    
                    CREATE INDEX IF NOT EXISTS idx_timestamp 
                    ON ChartDataPoints(Timestamp);
                ";

                command.ExecuteNonQuery();
                Log.Instance.Info($"Chart data database initialized: {_dbPath}");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"Failed to initialize chart data database: {ex.Message}");
            }
        }

        /// <summary>
        /// Run 데이터를 DB에 저장
        /// </summary>
        public bool SaveRun(string runName, Dictionary<string, List<DataPoint>> dataBuffers)
        {
            try
            {
                using var connection = new SqliteConnection($"Data Source={_dbPath}");
                connection.Open();

                using var transaction = connection.BeginTransaction();

                // 1. Run 정보 저장
                var firstPoint = dataBuffers.Values.FirstOrDefault()?.FirstOrDefault();
                var lastPoint = dataBuffers.Values.FirstOrDefault()?.LastOrDefault();
                var totalCount = dataBuffers.Values.Sum(list => list.Count);

                var runCmd = connection.CreateCommand();
                runCmd.CommandText = @"
                    INSERT OR REPLACE INTO ChartDataRuns 
                    (RunName, StartTime, EndTime, DataCount) 
                    VALUES (@RunName, @StartTime, @EndTime, @DataCount)";

                runCmd.Parameters.AddWithValue("@RunName", runName);
                runCmd.Parameters.AddWithValue("@StartTime",
                    firstPoint != null ? new DateTimeOffset(firstPoint.Timestamp).ToUnixTimeSeconds() : 0);
                runCmd.Parameters.AddWithValue("@EndTime",
                    lastPoint != null ? new DateTimeOffset(lastPoint.Timestamp).ToUnixTimeSeconds() : 0);
                runCmd.Parameters.AddWithValue("@DataCount", totalCount);
                runCmd.ExecuteNonQuery();

                // 2. 기존 데이터 삭제 (덮어쓰기)
                var deleteCmd = connection.CreateCommand();
                deleteCmd.CommandText = "DELETE FROM ChartDataPoints WHERE RunName = @RunName";
                deleteCmd.Parameters.AddWithValue("@RunName", runName);
                deleteCmd.ExecuteNonQuery();

                // 3. 데이터 포인트 저장
                var pointCmd = connection.CreateCommand();
                pointCmd.CommandText = @"
                    INSERT INTO ChartDataPoints 
                    (RunName, ItemName, Timestamp, Value) 
                    VALUES (@RunName, @ItemName, @Timestamp, @Value)";

                foreach (var item in dataBuffers)
                {
                    string itemName = item.Key;
                    var points = item.Value;

                    foreach (var point in points)
                    {
                        pointCmd.Parameters.Clear();
                        pointCmd.Parameters.AddWithValue("@RunName", runName);
                        pointCmd.Parameters.AddWithValue("@ItemName", itemName);
                        pointCmd.Parameters.AddWithValue("@Timestamp",
                            new DateTimeOffset(point.Timestamp).ToUnixTimeSeconds());
                        pointCmd.Parameters.AddWithValue("@Value", point.Value);
                        pointCmd.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
                Log.Instance.Info($"Chart data saved: {runName}, {totalCount} points");
                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"Failed to save chart data: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// DB에서 Run 데이터 로딩
        /// </summary>
        public Dictionary<string, List<DataPoint>>? LoadRun(string runName)
        {
            try
            {
                using var connection = new SqliteConnection($"Data Source={_dbPath}");
                connection.Open();

                var dataBuffers = new Dictionary<string, List<DataPoint>>();

                // 데이터 포인트 로딩
                var command = connection.CreateCommand();
                command.CommandText = @"
                    SELECT ItemName, Timestamp, Value 
                    FROM ChartDataPoints 
                    WHERE RunName = @RunName 
                    ORDER BY ItemName, Timestamp";

                command.Parameters.AddWithValue("@RunName", runName);

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string itemName = reader.GetString(0);
                    long unixTime = reader.GetInt64(1);
                    double value = reader.GetDouble(2);

                    if (!dataBuffers.ContainsKey(itemName))
                    {
                        dataBuffers[itemName] = new List<DataPoint>(15000);
                    }

                    dataBuffers[itemName].Add(new DataPoint
                    {
                        Timestamp = DateTimeOffset.FromUnixTimeSeconds(unixTime).LocalDateTime,
                        Value = value
                    });
                }

                Log.Instance.Info($"Chart data loaded: {runName}, {dataBuffers.Values.Sum(l => l.Count)} points");
                return dataBuffers;
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"Failed to load chart data: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 저장된 Run 목록 조회
        /// </summary>
        public List<string> GetRunNames()
        {
            var runNames = new List<string>();

            try
            {
                using var connection = new SqliteConnection($"Data Source={_dbPath}");
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT RunName FROM ChartDataRuns ORDER BY StartTime DESC";

                using var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    runNames.Add(reader.GetString(0));
                }

                Log.Instance.Info($"Found {runNames.Count} saved runs");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"Failed to get run names: {ex.Message}");
            }

            return runNames;
        }
    }
}
