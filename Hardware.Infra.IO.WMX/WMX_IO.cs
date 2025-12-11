using EQ.Domain.Enums;
using EQ.Domain.Interface;
using System;
using System.Reflection;
using WMX3ApiCLR;
using WMX3ApiCLR.SimuApiCLR;

namespace Hardware.Infra.IO.WMX
{
    public class WMX_IO : IIoController
    {
        protected WMX3Api Wmx3Lib = new WMX3Api();
        Io _io;
        Simu simu = new Simu();

        private byte[] InByte;
        private byte[] OutByte;

        int nNumChIn = 16 ;
        int nNumChOut = 16;

        private CancellationTokenSource _cts;
        private Task _pollingTask;
        private readonly object _lock = new object();

        public void Close()
        {
            Wmx3Lib.StopCommunication(0xFFFFFFFF);

            //Quit device.
            Wmx3Lib.CloseDevice();
            Wmx3Lib.Dispose();
        }


        public bool Init(string configPath)
        {
            _io = new Io(Wmx3Lib);
            Wmx3Lib.CreateDevice("C:\\Program Files\\SoftServo\\WMX3\\", DeviceType.DeviceTypeNormal, 0xFFFFFFFF);

            // Set Device Name.
            Wmx3Lib.SetDeviceName("ControlIO");
            simu = new Simu(Wmx3Lib);

            // Start Communication.
            Wmx3Lib.StartCommunication(0xFFFFFFFF);

            nNumChIn = Enum.GetValues(typeof(IO_IN)).Length;
            nNumChOut = Enum.GetValues(typeof(IO_OUT)).Length;

            InByte = new byte[nNumChIn / 8];
            OutByte = new byte[nNumChOut / 8];

            _cts = new CancellationTokenSource();
            _pollingTask = Task.Run(() => PollingLoop(_cts.Token), _cts.Token);
            return true;
        }

        public bool ReadInput(int index)
        {
            int addr = index / 8;
            int bit = index % 8;
            byte outData = 0;
            _io.GetInBitEx(0x00 + addr, bit, ref outData);
            return outData == 1 ? true : false;
        }
        public void WriteInput(int index, byte value)
        {
            int addr = index / 8;
            int bit = index % 8;
            simu.SetInBit(addr, bit, value);
        }

        public bool ReadOutput(int index)
        {
            int addr = index / 8;
            int bit = index % 8;
            byte outData = 0;
            _io.GetOutBitEx(0x00 + addr, bit, ref outData);

            return outData == 1 ? true : false;
        }

        public void WriteOutput(int index, byte onOff)
        {
            int addr = index / 8;
            int bit = index % 8;
            _io.SetOutBitEx(0x00 + addr, bit, onOff);
        }

        public (byte[] _in, byte[] _out) GetCachedData()
        {
            lock (_lock)
            {
               return ((byte[])InByte.Clone(), (byte[])OutByte.Clone());                
            }
        }

        public double ReadAnalogInput(int idx)
        {
            short rValue = 0;
            _io.GetInAnalogDataShortEx(idx, ref rValue);
            return rValue;
        }
        public void WriteAnalogOutput(int idx , double rValue)
        {          
            var r = _io.SetOutAnalogDataShortEx(idx, (short)rValue);            
        }


        private async Task PollingLoop(CancellationToken token)
        {
            byte[] live_In = new byte[nNumChIn / 8];
            byte[] live_Out = new byte[nNumChOut / 8];

            while (!token.IsCancellationRequested)
            {                               
                _io.GetInBytesEx(0x0 , live_In.Length , ref live_In);
                _io.GetOutBytesEx(0x0, live_Out.Length, ref live_Out);
                
                lock (_lock)
                {
                    Buffer.BlockCopy(live_In, 0, InByte, 0, live_In.Length);
                    Buffer.BlockCopy(live_Out, 0, OutByte, 0, live_Out.Length);
                }
                
                await Task.Delay(20, token);
            }
        }

        
    }
}
