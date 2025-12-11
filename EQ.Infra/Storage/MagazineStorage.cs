using EQ.Common.Logs;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.Domain.Interface;
using Microsoft.Data.Sqlite;
using System;
using System.IO;
using System.Xml.Linq;

namespace EQ.Infra.Storage
{
    public class MagazineStorage<T> : IDataStorage<Magazine<T>> where T : struct, IProductUnit
    {
        private readonly ProductMapStorage<T> _trayStorage = new ProductMapStorage<T>();

        public void Save(Magazine<T> data, string path, string key)
        {
            string magPath = Path.Combine(path, $"{key}_{data.Name}");
            Directory.CreateDirectory(magPath);

            for (int i = 0; i < data.Capacity; i++)
            {
                // 슬롯별 테이블/파일 생성 (Key: Slot_0, Slot_1 ...)
                _trayStorage.Save(data.Slots[i], magPath, $"Slot_{i}");
            }
        }

        public Magazine<T> Load(string path, string key)
        {
            return null; // LoadWithInit 사용 권장
        }

        /// <summary>
        /// [신규] 특정 매거진의 특정 슬롯만 저장
        /// </summary>
        public void SaveSlot(Magazine<T> data, int slotIndex, string path, string key)
        {
            if (data == null) return;
            if (slotIndex < 0 || slotIndex >= data.Capacity) return;

            // 경로: .../ProductData/RecipeName/Key_ID/Slot_0.bin
            string magPath = Path.Combine(path, $"{key}_{data.Name}");
            Directory.CreateDirectory(magPath);

            // 해당 슬롯만 저장
            _trayStorage.Save(data.Slots[slotIndex], magPath, $"Slot_{slotIndex}");
        }

        /// <summary>
        /// [신규] 특정 매거진의 특정 슬롯만 파일에서 로드하여 메모리 갱신
        /// </summary>
        public void LoadSlot(Magazine<T> data, int slotIndex, string path, string key)
        {
            if (data == null) return;
            if (slotIndex < 0 || slotIndex >= data.Capacity) return;

            string magPath = Path.Combine(path, $"{key}_{data.Name}");

            // 해당 슬롯 로드
            var loadedMap = _trayStorage.Load(magPath, $"Slot_{slotIndex}");

            // 로드 성공 시 메모리 교체 (실패 시 기존 데이터 유지 또는 null 처리 정책 결정)
            if (loadedMap != null)
            {
                data.Slots[slotIndex] = loadedMap;
            }
        }

        public Magazine<T> LoadWithInit(string path, string key, MagazineName name, int capacity, int rows, int cols)
        {
            var mag = new Magazine<T>(name, capacity, rows, cols);
            string magPath = Path.Combine(path, $"{key}_{name}");

            if (!Directory.Exists(magPath)) return mag;

            for (int i = 0; i < capacity; i++)
            {
                var tray = _trayStorage.Load(magPath, $"Slot_{i}");
                if (tray != null) mag.Slots[i] = tray;
            }
            return mag;
        }

        // --- [추가됨] 매거진 전체 슬롯 정리 ---
        public void DeleteOldBackups(string path, string key, TimeSpan olderThan)
        {
            if (!Directory.Exists(path)) return;

            // 1. 해당 Key로 시작하는 모든 매거진 폴더 검색 (예: WaferMag_1, WaferMag_2 ...)
            string[] magazineDirectories = Directory.GetDirectories(path, $"{key}_*");

            foreach (string magPath in magazineDirectories)
            {
                // 2. 각 매거진 폴더 내의 DB 파일 경로 확인
                // (ProductMapStorage에서 사용하는 DB 파일명과 일치해야 함)
                string dbPath = Path.Combine(magPath, "_ProductBackup.db");

                if (!File.Exists(dbPath)) continue;

                try
                {
                    var slotTables = new List<string>();
                    int totalDeletedRows = 0; // [핵심] 전체 삭제된 행 개수 누적

                    // 1. 테이블 목록 조회
                    using (var conn = new SqliteConnection($"Data Source={dbPath}"))
                    {
                        conn.Open();
                        var cmd = conn.CreateCommand();
                        cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name LIKE 'Slot_%';";
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read()) slotTables.Add(reader.GetString(0));
                        }
                    }

                    // 2. 각 슬롯별 삭제 수행
                    foreach (string slotKey in slotTables)
                    {
                        // performVacuum: false로 설정하고, 삭제된 개수를 받아 누적
                        totalDeletedRows += _trayStorage.DeleteOldBackups(magPath, slotKey, olderThan, 0, false);
                    }

                    // 3. [조건부 최적화] 실제로 지워진 데이터가 있을 때만 VACUUM 실행
                    if (totalDeletedRows > 0)
                    {
                        using (var conn = new SqliteConnection($"Data Source={dbPath}"))
                        {
                            conn.Open();
                            var cmd = conn.CreateCommand();
                            cmd.CommandText = "VACUUM;";
                            cmd.ExecuteNonQuery();

                            // Log.Instance.Info($"[DB] Optimized ({totalDeletedRows} rows deleted)");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[MagazineStorage] Backup Delete Error: {ex.Message}");
                }
            }
        }
    }
}