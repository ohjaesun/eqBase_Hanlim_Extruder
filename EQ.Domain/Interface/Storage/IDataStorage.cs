namespace EQ.Domain.Interface
{
    /// <summary>
    /// 데이터 저장/로드를 위한 범용 인터페이스 (리포지토리)
    /// </summary>
    public interface IDataStorage<T> where T : class, new()
    {
        /// <summary>
        /// 데이터를 지정된 경로에 저장합니다.
        /// </summary>
        /// <param name="data">저장할 객체</param>
        /// <param name="path">저장될 폴더 경로 (동적으로 변경됨)</param>
        /// <param name="key">저장될 파일명 (또는 DB 키)</param>
        void Save(T data, string path, string key);

        /// <summary>
        /// 지정된 경로에서 데이터를 로드합니다.
        /// </summary>
        /// <param name="path">로드할 폴더 경로 (동적으로 변경됨)</param>
        /// <param name="key">로드할 파일명 (또는 DB 키)</param>
        /// <returns>로드된 객체 (없으면 new T())</returns>
        T Load(string path, string key);
    }
}