// EQ.Domain/Interface/IModbusClient.cs
using System;

namespace EQ.Domain.Interface
{
    /// <summary>
    /// Modbus 통신을 위한 인터페이스 (Domain 계층)
    /// (NModbus 라이브러리의 Master 기능을 추상화)
    /// </summary>
    public interface IModbusClient : IDisposable
    {
        /// <summary>
        /// Modbus 서버 연결 상태
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// Modbus 클라이언트 초기화 및 연결
        /// </summary>
        /// <param name="name">클라이언트 이름 (로그용)</param>
        /// <param name="ip">서버 IP</param>
        /// <param name="port">서버 Port</param>
        void Init(string name, string ip, int port);

        /// <summary>
        /// 연결 종료
        /// </summary>
        void Close();

        // --- NModbus 표준 함수 ---

        // FC01 - Read Coils (0xxxx)
        bool[] ReadCoils(ushort address, ushort count);

        // FC02 - Read Discrete Inputs (1xxxx)
        bool[] ReadInputs(ushort address, ushort count);

        // FC03 - Read Holding Registers (4xxxx)
        ushort[] ReadHoldingRegisters(ushort address, ushort count);

        // FC04 - Read Input Registers (3xxxx)
        ushort[] ReadInputRegisters(ushort address, ushort count);

        // FC05 - Write Single Coil (0xxxx)
        void WriteSingleCoil(ushort address, bool value);

        // FC06 - Write Single Register (4xxxx)
        void WriteSingleRegister(ushort address, ushort value);

        // FC16 - Write Multiple Registers (4xxxx)
        void WriteMultipleRegisters(ushort startAddress, ushort[] values);

        // FC15 - Write Multiple Coils (0xxxx)
        void WriteMultipleCoils(ushort startAddress, bool[] values);

        // --- 데이터 변환 쓰기 (편의 메서드) ---
        // (Core 계층이 사용하기 편하도록 오버로딩 제공)

        /// <summary>
        /// 32비트 정수(int)를 2개의 Holding Register에 씁니다.
        /// </summary>
        /// <param name="address">시작 주소 (예: 40001번지 -> 0)</param>
        /// <param name="value">쓸 값</param>
        /// <param name="useBigEndian">Modbus 표준 Big-Endian 변환 사용 여부</param>
        void WriteRegisters(ushort address, int value, bool useBigEndian = true);

        /// <summary>
        /// 32비트 실수(float)를 2개의 Holding Register에 씁니다.
        /// </summary>
        void WriteRegisters(ushort address, float value, bool useBigEndian = true);

        /// <summary>
        /// 64비트 실수(double)를 4개의 Holding Register에 씁니다.
        /// </summary>
        void WriteRegisters(ushort address, double value, bool useBigEndian = true);
    }
}