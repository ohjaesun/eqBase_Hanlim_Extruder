using EQ.Common.Logs;
using EQ.Domain.Entities.Unit;
using EQ.Domain.Interface;
using Modbus.Device; // NModbus4 참조
using System;

namespace EQ.Infra.Device
{
    public class TOHO_Controller : ITemperatureController
    {
        // NModbus의 Serial Master 인터페이스 직접 사용
        private readonly IModbusSerialMaster _master;
        private readonly byte _slaveId;

        /// <summary>
        /// 생성자: 이미 열린 Modbus Master 인스턴스를 주입받습니다.
        /// </summary>
        public TOHO_Controller(IModbusSerialMaster master, byte slaveId)
        {
            _master = master;
            _slaveId = slaveId;
        }

        // --- ITemperatureController 구현 ---

        public double ReadPV()
        {
            return ReadRegister(TOHO_RegMap.PV_Read);
        }

        public double ReadSV()
        {
            return ReadRegister(TOHO_RegMap.SV_Read);
        }

        public bool IsRunning()
        {
            // 예: 1이면 동작 중
            return ReadRegister(TOHO_RegMap.Status_Read) == 1;
        }

        public void WriteSV(double value)
        {
            WriteRegister(TOHO_RegMap.SV_Write, (ushort)value);
        }

        public void SetRun(bool run)
        {
            ushort val = run ? (ushort)1 : (ushort)0;
            WriteRegister(TOHO_RegMap.RunStop_Write, val);
        }

        // --- 내부 Helper (NModbus 직접 호출) ---

        private ushort ReadRegister(TOHO_RegMap reg)
        {
            try
            {
                // NModbus는 SlaveID를 파라미터로 기본 지원합니다.
                ushort[] res = _master.ReadHoldingRegisters(_slaveId, (ushort)reg, 1);
                return (res != null && res.Length > 0) ? res[0] : (ushort)0;
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[TOHO_ID{_slaveId}] Read Error: {ex.Message}");
                return 0;
            }
        }

        private void WriteRegister(TOHO_RegMap reg, ushort value)
        {
            try
            {
                _master.WriteSingleRegister(_slaveId, (ushort)reg, value);
                Log.Instance.Info($"[TOHO_ID{_slaveId}] Write {reg} -> {value}");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[TOHO_ID{_slaveId}] Write Error: {ex.Message}");
            }
        }
    }
}