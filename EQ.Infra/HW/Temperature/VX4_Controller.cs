using EQ.Common.Logs;
using EQ.Domain.Entities.Unit;
using EQ.Domain.Interface;
using Modbus.Device; // NModbus4 참조
using System;

namespace EQ.Infra.Device
{
    public class VX4_Controller : ITemperatureController
    {
        private readonly IModbusSerialMaster _master;
        private readonly byte _slaveId;

        public VX4_Controller(IModbusSerialMaster master, byte slaveId)
        {
            _master = master;
            _slaveId = slaveId;
        }

        // --- ITemperatureController 구현 ---

        public double ReadPV() => ReadRegister(VX4_RegMap.PV);
        public double ReadSV() => ReadRegister(VX4_RegMap.SV);
        public bool IsRunning() => ReadRegister(VX4_RegMap.Status) == 1;

        public void WriteSV(double value) => WriteRegister(VX4_RegMap.SV_Write, (ushort)(value*10));
        public void SetRun(bool run)
        {
            ushort val = run ? (ushort)1 : (ushort)0;
            WriteRegister(VX4_RegMap.RunStop_Write, val);
        }

        // --- 내부 Helper ---

        private ushort ReadRegister(VX4_RegMap reg)
        {
            try
            {
                ushort[] res = _master.ReadHoldingRegisters(_slaveId, (ushort)reg, 1);
                return (res != null && res.Length > 0) ? res[0] : (ushort)0;
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[VX4_ID{_slaveId}] Read Error: {ex.Message}");
                return 0;
            }
        }

        private void WriteRegister(VX4_RegMap reg, ushort value)
        {
            try
            {
                _master.WriteSingleRegister(_slaveId, (ushort)reg, value);
                Log.Instance.Info($"[VX4_ID{_slaveId}] Write {reg} -> {value}");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[VX4_ID{_slaveId}] Write Error: {ex.Message}");
            }
        }
    }
}