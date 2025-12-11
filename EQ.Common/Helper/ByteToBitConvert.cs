using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Common.Helper
{
    /// <summary>
    /// Byte 배열을 Bit(bool) 리스트로 변환하는 헬퍼 클래스
    /// </summary>
    static public class ByteToBitConvert
    {
        /// <summary>
        /// byte 배열을 bit(bool) 리스트로 변환합니다.
        /// </summary>
        /// <param name="bytes">IO 컨트롤러에서 읽어온 원본 byte 배열</param>
        /// <returns>bit 상태(bool) 리스트</returns>
        public static List<bool> Convert(byte[] bytes)
        {
            // (예) 2바이트 배열 -> 16개 bool 리스트
            var bits = new List<bool>(bytes.Length * 8);

            foreach (byte b in bytes)
            {
                // 0번 비트부터 7번 비트까지 순서대로 추가
                for (int i = 0; i < 8; i++)
                {
                    // (b & (1 << i)) != 0  -> 0번 비트가 'true'인지 'false'인지 확인
                    bits.Add((b & (1 << i)) != 0);
                }
            }
            return bits;
        }
    }
}
