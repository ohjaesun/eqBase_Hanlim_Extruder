// using LogDLL; // (사용 시) using EQ.Common.Logs;

namespace Tcp
{
    /// <summary>
    /// 메시지 종료자 유형
    /// </summary>
    public enum EndType
    {
        /// <summary>
        /// 구분자 없음 (수신되는 즉시 처리)
        /// </summary>
        None,
        /// <summary>
        /// CR (0x0D)로 끝남 
        /// </summary>
        CR,
        /// <summary>
        /// LF (0x0A)로 끝남
        LF,
        /// <summary>
        /// CR (0x0D) + LF (0x0A) 로 끝남
        CRLF,
        /// <summary>
        /// STX (0x02)로 시작하고 ETX (0x03) 로 끝남
        /// </summary>
        ETX 
    }

    /// <summary>
    /// 수신된 데이터를 래핑하는 이벤트 객체
    /// </summary>
    public class PacketData
    {
        /// <summary>
        /// (서버용) 데이터를 보낸 클라이언트의 고유 ID (IP:Port)
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// (클라이언트용) 연결 설정 이름
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 연결 IP
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 연결 Port
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 수신된 데이터 (UTF8 문자열)
        /// </summary>
        public string Str { get; set; }

        /// <summary>
        /// 수신된 원본 바이트
        /// </summary>
        public byte[] Bytes { get; set; }
    }
}