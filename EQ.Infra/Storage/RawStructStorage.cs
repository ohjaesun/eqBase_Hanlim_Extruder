using EQ.Common.Logs;
using System;
using System.IO;
using System.Runtime.InteropServices; // MemoryMarshal 사용

namespace EQ.Infra.Storage
{
    /// <summary>
    /// 구조체 배열을 바이너리 파일로 고속 저장/로드하는 범용 클래스      
    /// </summary>
    public static class RawStructStorage
    {
        public static void Save<T>(string filePath, int rows, int cols, T[] data) where T : struct
        {
            string tempPath = filePath + ".tmp"; // 임시 파일

            try
            {
                string dir = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(dir)) Directory.CreateDirectory(dir);

                // 1. 임시 파일 생성
                using (var fs = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None))
                using (var bw = new BinaryWriter(fs))
                {
                    // 헤더 저장
                    bw.Write(rows);
                    bw.Write(cols);

                    // 데이터 본문 저장 (고속 쓰기)
                    var byteSpan = MemoryMarshal.AsBytes(new ReadOnlySpan<T>(data));
                    fs.Write(byteSpan);

                    // ★ [핵심] 물리 디스크 동기화
                    fs.Flush(true);
                }

                // 2. 원본 파일 교체 (Atomic Move)
                File.Move(tempPath, filePath, overwrite: true);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[RawStructStorage] Save Failed: {ex.Message}");
                try { if (File.Exists(tempPath)) File.Delete(tempPath); } catch { }
                throw;
            }
        }

        
        public static (T[] data, int rows, int cols) Load<T>(string filePath) where T : struct
        {
            if (!File.Exists(filePath)) return (null, 0, 0);
           
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs))
            {
                if (fs.Length < 8) return (null, 0, 0); // 최소 헤더 크기 체크

                int rows = br.ReadInt32();
                int cols = br.ReadInt32();
                T[] data = new T[rows * cols];

                var byteSpan = MemoryMarshal.AsBytes(new Span<T>(data));
                int read = fs.Read(byteSpan);

                return (data, rows, cols);
            }
        }

        /// <summary>
        /// 구조체 배열을 byte[] (BLOB)로 변환 (DB 저장용)
        /// </summary>
        public static byte[] ToByteArray<T>(int rows, int cols, T[] data) where T : struct
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(rows);
                bw.Write(cols);

                var byteSpan = MemoryMarshal.AsBytes(new ReadOnlySpan<T>(data));
                bw.Write(byteSpan);

                return ms.ToArray();
            }
        }

        /// <summary>
        /// byte[] (BLOB)에서 구조체 배열로 복원
        /// </summary>
        public static (T[] data, int rows, int cols) FromByteArray<T>(byte[] blob) where T : struct
        {
            if (blob == null || blob.Length == 0) return (null, 0, 0);

            using (var ms = new MemoryStream(blob))
            using (var br = new BinaryReader(ms))
            {
                int rows = br.ReadInt32();
                int cols = br.ReadInt32();
                int totalCount = rows * cols;

                T[] data = new T[totalCount];

                var byteSpan = MemoryMarshal.AsBytes(new Span<T>(data));
                br.Read(byteSpan);

                return (data, rows, cols);
            }
        }
    }
}