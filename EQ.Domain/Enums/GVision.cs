using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Domain.Enums
{
    public enum GVisionType //여기 이름 포맷에 맞춰서 userOption2 네이밍 해줘야함
    {
        GVisionTOP,
        GVisionBOTTOM,
        GVisionSIDE,
    }

    public enum VisionCommandType
    {
        SOT, // Start of Transmission
        EOT, // End of Transmission
        LotEnd,
        GrabDone,
        StartPack,
        JobChange,
        PackInfo,
    }

}
