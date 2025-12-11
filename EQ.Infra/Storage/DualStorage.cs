using EQ.Domain.Interface; // IDataStorage

namespace EQ.Infra.Storage
{
    /// <summary>
    /// 데이터를 두 개의 저장소(예: JSON과 SQLite)에 동시에 저장합니다.
    /// 'IDataStorage' 인터페이스를 구현합니다.
    /// </summary>
    public class DualStorage<T> : IDataStorage<T> where T : class, new()
    {
        private readonly IDataStorage<T> _primaryStorage; // 주 저장소 (예: JsonFileStorage)
        private readonly IDataStorage<T> _backupStorage;  // 백업 저장소 (예: SqliteStorage)

        /// <summary>
        /// 두 개의 저장소 구현체를 주입받습니다.
        /// </summary>
        public DualStorage(IDataStorage<T> primaryStorage, IDataStorage<T> backupStorage)
        {
            _primaryStorage = primaryStorage;
            _backupStorage = backupStorage;
        }

        /// <summary>
        /// [핵심] Save 호출 시, 두 개의 저장소에 모두 Save를 실행합니다.
        /// (변경된 인터페이스: 'path' 매개변수 추가)
        /// </summary>
        public void Save(T data, string path, string key)
        {
            // 1. 주 저장소(JSON)에 저장
            _primaryStorage?.Save(data, path, key);

            // 2. 백업 저장소(SQLite)에 저장
            _backupStorage?.Save(data, path, key);
        }

        /// <summary>
        /// Load 호출 시, 주 저장소(Primary)의 데이터만 반환합니다.
        /// (변경된 인터페이스: 'path' 매개변수 추가)
        /// </summary>
        public T Load(string path, string key)
        {
            // (백업 기능이므로 로드는 주 저장소에서만 수행)
            return _primaryStorage.Load(path, key);
        }
    }
}