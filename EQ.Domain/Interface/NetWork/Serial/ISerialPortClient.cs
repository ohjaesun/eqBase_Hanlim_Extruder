using System;
using System.IO.Ports;

using Tcp; 

namespace EQ.Domain.Interface
{
    /// <summary>
    /// 범용 시리얼(RS-232/485) 통신을 위한 인터페이스 (Domain 계층)
    /// </summary>
    public interface ISerialPortClient
    {
        // 이벤트
        event Action<PacketData> OnRead;
        event Action OnConnected;
        event Action OnDisconnected;

        // 속성
        bool IsConnected { get; }

        // 메서드
        /// <summary>
        /// 시리얼 포트를 초기화하고 엽니다.
        /// </summary>
        void Init(string name, string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits, EndType endType = EndType.None);

        /// <summary>
        /// 포트를 닫습니다.
        /// </summary>
        void Close();

        /// <summary>
        /// 데이터를 비동기로 전송합니다. (EndType에 맞춰 종료자 자동 추가)
        /// </summary>
        Task SendData(string data);

        /// <summary>
        /// 원본 바이트 데이터를 비동기로 전송합니다. (EndType에 맞춰 종료자 자동 추가)
        /// </summary>
        Task SendData(byte[] data);
    }
}