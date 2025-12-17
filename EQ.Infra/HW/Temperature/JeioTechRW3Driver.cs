using EQ.Common.Logs;
using EQ.Domain.Interface;
using Modbus.Device;
using System;

namespace EQ.Infra.Device
{
    /// <summary>
    /// Jeio Tech RW3 Series Refrigerated & Heating Bath Circulator Driver
    /// Protocol: Modbus RTU (RS-232C)
    /// </summary>
    public class JeioTechRW3Driver : ITemperatureController
    {
        private readonly IModbusSerialMaster _master;
        private readonly byte _slaveId;
        
        // Modbus Addresses (Base-1 from manual, subtract 1 for Base-0)
        private const ushort ADDR_RUN_STOP = 993 - 1;       // W/S(06) 1:Run, 0:Stop
        private const ushort ADDR_SV_SET = 981 - 1;         // W/M(16) Double (8 bytes = 4 words)
        private const ushort ADDR_REPORT = 994 - 1;         // R/I(03) Report Structure (Check PV)

        public JeioTechRW3Driver(IModbusSerialMaster master, byte slaveId)
        {
            _master = master;
            _slaveId = slaveId;
        }

        #region ITemperatureController Implementation

        public double ReadPV()
        {
            try
            {
                // PV is at Word Offset 8 of the Report (Length 4 Words)
                // Report Start: ADDR_REPORT (994)
                // We read enough words to cover until PV.
                
                // Reading 20 words to be safe and cover PV
                ushort[] registers = _master.ReadHoldingRegisters(_slaveId, ADDR_REPORT, 20);

                if (registers.Length >= 12)
                {
                    // PV is at offset 8
                    return ConvertDeviceDoubleToDouble(registers, 8);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[JeioTechRW3_slave{_slaveId}] ReadPV error: {ex.Message}");
            }
            return 0.0;
        }

        public double ReadSV()
        {
            try
            {
                // SV is at 981 (4 Words)
                var words = _master.ReadHoldingRegisters(_slaveId, ADDR_SV_SET, 4);
                return ConvertDeviceDoubleToDouble(words, 0);
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[JeioTechRW3_slave{_slaveId}] ReadSV error: {ex.Message}");
            }
            return 0.0;
        }

        public void WriteSV(double value)
        {
            try
            {
                ushort[] words = ConvertDoubleToDeviceDouble(value);
                _master.WriteMultipleRegisters(_slaveId, ADDR_SV_SET, words);
                Log.Instance.Info($"[JeioTechRW3_slave{_slaveId}] SV Set: {value}");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[JeioTechRW3_slave{_slaveId}] WriteSV error: {ex.Message}");
            }
        }

        public bool IsRunning()
        {
            try
            {
                // Read Run/Stop status
                var words = _master.ReadHoldingRegisters(_slaveId, ADDR_RUN_STOP, 1);
                return words[0] == 1;
            }
            catch (Exception ex)
            {
                // Log.Instance.Warning($"[JeioTechRW3_slave{_slaveId}] IsRunning check failed: {ex.Message}");
            }
            return false;
        }

        public void SetRun(bool run)
        {
            try
            {
                ushort value = run ? (ushort)1 : (ushort)0;
                _master.WriteSingleRegister(_slaveId, ADDR_RUN_STOP, value);
                Log.Instance.Info($"[JeioTechRW3_slave{_slaveId}] SetRun: {run}");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[JeioTechRW3_slave{_slaveId}] SetRun error: {ex.Message}");
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Convert 4 ushorts (from Modbus) to Double
        /// </summary>
        private double ConvertDeviceDoubleToDouble(ushort[] words, int startIndex)
        {
            byte[] rawBytes = new byte[8];
            
            for(int i=0; i<4; i++)
            {
                rawBytes[i*2] = (byte)(words[startIndex + i] >> 8);
                rawBytes[i*2+1] = (byte)(words[startIndex + i] & 0xFF);
            }
            
            return BitConverter.ToDouble(rawBytes, 0);
        }

        private ushort[] ConvertDoubleToDeviceDouble(double value)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            ushort[] words = new ushort[4];
            
            for(int i=0; i<4; i++)
            {
                words[i] = (ushort)((bytes[i*2] << 8) | bytes[i*2+1]);
            }
            return words;
        }

        #endregion
    }
}
