// EQ.Infra/Network/Modbus/NModbusClient.cs
using EQ.Common.Logs;
using EQ.Domain.Interface;
using Modbus.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EQ.Infra.Network.Modbus
{
    /// <summary>
    /// NModbus 라이브러리를 사용한 IModbusClient 구현체 (Infra 계층)
    /// </summary>
    public class NModbusClient : IModbusClient
    {
        private TcpClient _client;
        private ModbusIpMaster _master;
        private string _name;
        private readonly object _lock = new object(); // 스레드 안전성 확보

        public bool IsConnected => _client?.Connected ?? false;

        public void Init(string name, string ip, int port)
        {
            try
            {
                if (_client == null || !_client.Connected)
                {
                    _name = name;
                    _client = new TcpClient(ip, port);
                    _master = ModbusIpMaster.CreateIp(_client);
                    Log.Instance.Info($"[NModbusClient {_name}] Initialized and connected to {ip}:{port}.");
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[NModbusClient {_name}] Init failed for {ip}:{port}: {ex.Message}");
                Close(); // 실패 시 자원 해제
            }
        }

        public void Close()
        {
            lock (_lock)
            {
                _master?.Dispose();
                _master = null;
                _client?.Close(); // TcpClient.Close()는 Dispose()를 포함
                _client = null;
            }
            Log.Instance.Info($"[NModbusClient {_name}] Connection closed.");
        }

        public void Dispose()
        {
            Close();
        }

        #region --- 표준 Modbus 함수 (스레드 안전 래핑) ---

        // 모든 master 호출을 lock으로 감싸 다중 스레드(시퀀스) 접근 시 충돌 방지

        public bool[] ReadCoils(ushort address, ushort count)
        {
            try
            {
                lock (_lock)
                {
                    if (!IsConnected) return null;
                    return _master.ReadCoils(address, count);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[NModbusClient {_name}] ReadCoils (Addr:{address}) failed: {ex.Message}");
                return null;
            }
        }

        public bool[] ReadInputs(ushort address, ushort count)
        {
            try
            {
                lock (_lock)
                {
                    if (!IsConnected) return null;
                    return _master.ReadInputs(address, count);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[NModbusClient {_name}] ReadInputs (Addr:{address}) failed: {ex.Message}");
                return null;
            }
        }

        public ushort[] ReadHoldingRegisters(ushort address, ushort count)
        {
            try
            {
                lock (_lock)
                {
                    if (!IsConnected) return null;
                    return _master.ReadHoldingRegisters(address, count);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[NModbusClient {_name}] ReadHoldingRegisters (Addr:{address}) failed: {ex.Message}");
                return null;
            }
        }

        public ushort[] ReadInputRegisters(ushort address, ushort count)
        {
            try
            {
                lock (_lock)
                {
                    if (!IsConnected) return null;
                    return _master.ReadInputRegisters(address, count);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[NModbusClient {_name}] ReadInputRegisters (Addr:{address}) failed: {ex.Message}");
                return null;
            }
        }

        public void WriteSingleCoil(ushort address, bool value)
        {
            try
            {
                lock (_lock)
                {
                    if (!IsConnected) return;
                    _master.WriteSingleCoil(address, value);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[NModbusClient {_name}] WriteSingleCoil (Addr:{address}) failed: {ex.Message}");
            }
        }

        public void WriteSingleRegister(ushort address, ushort value)
        {
            try
            {
                lock (_lock)
                {
                    if (!IsConnected) return;
                    _master.WriteSingleRegister(address, value);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[NModbusClient {_name}] WriteSingleRegister (Addr:{address}) failed: {ex.Message}");
            }
        }

        public void WriteMultipleRegisters(ushort startAddress, ushort[] values)
        {
            try
            {
                lock (_lock)
                {
                    if (!IsConnected) return;
                    _master.WriteMultipleRegisters(startAddress, values);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[NModbusClient {_name}] WriteMultipleRegisters (Addr:{startAddress}, Cnt:{values.Length}) failed: {ex.Message}");
            }
        }

        public void WriteMultipleCoils(ushort startAddress, bool[] values)
        {
            try
            {
                lock (_lock)
                {
                    if (!IsConnected) return;
                    _master.WriteMultipleCoils(startAddress, values);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[NModbusClient {_name}] WriteMultipleCoils (Addr:{startAddress}, Cnt:{values.Length}) failed: {ex.Message}");
            }
        }

        #endregion

        #region --- 편의 메서드 (데이터 변환) ---

        // (제공된 코드의 RegisterBufWrite 로직을 재사용 및 개선)

        public void WriteRegisters(ushort address, int value, bool useBigEndian = true)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            ushort[] registers = ConvertBytesToUshorts(bytes, useBigEndian);
            WriteMultipleRegisters(address, registers);
        }

        public void WriteRegisters(ushort address, float value, bool useBigEndian = true)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            ushort[] registers = ConvertBytesToUshorts(bytes, useBigEndian);
            WriteMultipleRegisters(address, registers);
        }

        public void WriteRegisters(ushort address, double value, bool useBigEndian = true)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            ushort[] registers = ConvertBytesToUshorts(bytes, useBigEndian);
            WriteMultipleRegisters(address, registers);
        }

        /// <summary>
        /// .NET 바이트 배열(Little-Endian)을 Modbus 레지스터(ushort[])로 변환합니다.
        /// (제공된 코드의 변환 로직 반영)
        /// </summary>
        private ushort[] ConvertBytesToUshorts(byte[] bytes, bool useBigEndian)
        {
            // Intel CPU (BitConverter.IsLittleEndian=true) 기준
            // bytes = [ 78 56 34 12 ] (float 123.45f의 Little-Endian 바이트 배열)

            if (BitConverter.IsLittleEndian && useBigEndian)
            {
                // Modbus 표준(Big-Endian)으로 변환
                // bytes = [ 12 34 56 78 ]
                Array.Reverse(bytes);
            }
            // else
            //   useBigEndian=false이면 Little-Endian (Word-Swap) 모드로 전송
            //   bytes = [ 78 56 34 12 ] (그대로 둠)

            int count = bytes.Length / 2;
            ushort[] registers = new ushort[count];
            for (int i = 0; i < count; i++)
            {
                // 항상 (High-Byte << 8 | Low-Byte) 순서로 ushort를 조합
                registers[i] = (ushort)((bytes[i * 2] << 8) | bytes[i * 2 + 1]);
            }

            // useBigEndian=true  -> [ 0x1234, 0x5678 ] (Big-Endian)
            // useBigEndian=false -> [ 0x7856, 0x3412 ] (Little-Endian / Word-Swap)
            return registers;
        }

        #endregion
    }
}