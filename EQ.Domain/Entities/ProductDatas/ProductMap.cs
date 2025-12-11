using System;
using System.IO;
using System.Runtime.InteropServices;

namespace EQ.Domain.Entities
{
    public class ProductMap<T> where T : struct, IProductUnit
    {
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public T[] Units; // 실제 데이터 (Struct Array)

        public ProductMap(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Units = new T[rows * cols];
            Initialize();
        }

        // 기본 생성자 (Serializer용)
        public ProductMap() { Rows = 0; Cols = 0; Units = Array.Empty<T>(); }

        private void Initialize()
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Cols; x++)
                {
                    int idx = y * Cols + x;
                    Units[idx].X = x;
                    Units[idx].Y = y;
                    Units[idx].Grade = ProductUnitChipGrade.None;
                }
            }
        }

        public ref T this[int x, int y]
        {
            get
            {
                if (x < 0 || x >= Cols || y < 0 || y >= Rows) throw new IndexOutOfRangeException();
                return ref Units[y * Cols + x];
            }
        }

        // --- BLOB 변환 (고속 저장) ---
        public byte[] ToByteArray()
        {
            using (var ms = new MemoryStream())
            using (var bw = new BinaryWriter(ms))
            {
                bw.Write(Rows);
                bw.Write(Cols);
                var byteSpan = MemoryMarshal.AsBytes(new ReadOnlySpan<T>(Units));
                bw.Write(byteSpan);
                return ms.ToArray();
            }
        }

        public static ProductMap<T> FromByteArray(byte[] data)
        {
            if (data == null || data.Length == 0) return null;

            using (var ms = new MemoryStream(data))
            using (var br = new BinaryReader(ms))
            {
                int rows = br.ReadInt32();
                int cols = br.ReadInt32();
                var map = new ProductMap<T>(rows, cols);

                var byteSpan = MemoryMarshal.AsBytes(new Span<T>(map.Units));
                br.Read(byteSpan); // 메모리 덤프 로드
                return map;
            }
        }
    }
}