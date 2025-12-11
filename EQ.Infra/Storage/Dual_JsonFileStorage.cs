using EQ.Common.Logs;
using EQ.Domain.Interface;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace EQ.Infra.Storage
{
    public class JsonFileStorage<T> : IDataStorage<T> where T : class, new()
    {
        // [변경] 생성자에서 경로를 받지 않습니다.
        public JsonFileStorage()
        {
        }

        public void Save(T data, string path, string key)
        {
            string fullPath = Path.Combine(path, $"{key}.json");
            string tempPath = Path.Combine(path, $"{key}.json.tmp"); // 임시 파일

            try
            {
                Directory.CreateDirectory(path);

                // 1. 데이터를 JSON 문자열로 변환
                string strJson = JsonConvert.SerializeObject(data, Formatting.Indented);

                // 2. 문자열을 UTF-8 바이트 배열로 변환 (StreamWriter 없이 직접 쓰기 위함)
                byte[] dataBytes = Encoding.UTF8.GetBytes(strJson);

                // 3. 임시 파일에 쓰기 (FileShare.None으로 독점적 접근)
                using (var fs = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    // 데이터를 파일 버퍼에 씀
                    fs.Write(dataBytes, 0, dataBytes.Length);

                    // ★ [핵심] OS 캐시를 넘어 물리 디스크에 강제로 기록
                    fs.Flush(true);
                }

                // 4. 원본 파일 교체 (Atomic Move)
                // 파일 이름 변경은 매우 빨라서 이 순간에 전원이 나가도 안전함
                File.Move(tempPath, fullPath, overwrite: true);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[JsonStorage] Save Failed ({key}): {ex.Message}");

                // 실패 시 임시 파일 삭제 (청소)
                try { if (File.Exists(tempPath)) File.Delete(tempPath); } catch { }

                throw; // 상위로 예외 전파
            }
        }

        public T Load(string path, string key)
        {
            // (Load 로직은 기존과 동일하게 유지하거나, 0byte 체크 로직 추가 권장)
            var filePath = Path.Combine(path, $"{key}.json");
            if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0) return new T();

            try
            {
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(filePath));
            }
            catch
            {
                return new T();
            }
        }
    }
}