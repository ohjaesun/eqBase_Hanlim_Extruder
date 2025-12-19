using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Domain.Enums
{

    public enum ChartTypes
    {
        //모터 관련
        FEEDER_RPM,
        FEEDER_SPEED,
        FEEDER_TRQ,

        EXTRUDER_RPM,
        EXTRUDER_SPEED,
        EXTRUDER_TRQ,
       
        PULLER_RPM,
        PULLER_SPEED,
        PULLER_TRQ,

        //온도 관련
        ZONE1_TEMP_SV, // 히터
        ZONE1_TEMP_PV,
        ZONE2_TEMP_SV,
        ZONE2_TEMP_PV,
        ZONE3_TEMP_SV, // 칠러
        ZONE3_TEMP_PV,
        ZONE4_TEMP_SV,
        ZONE4_TEMP_PV,


        //데이터 관련
        DIAMETER,        
        OVALITY,
        GOOD_COUNT,
        BAD_COUNT
    }
  

}
