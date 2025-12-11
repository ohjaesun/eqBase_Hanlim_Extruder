using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Domain.Interface
{
    public interface IIoController
    {
        bool Init(string configPath);
        void WriteInput(int address, byte value);
        bool ReadInput(int address);
        void WriteOutput(int address, byte value);
        bool ReadOutput(int address);
        
        // Analog IO
        double ReadAnalogInput(int address);
        void WriteAnalogOutput(int address, double value);

        void Close();

        (byte[] _in, byte[] _out) GetCachedData();
    }
}
