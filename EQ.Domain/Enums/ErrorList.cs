using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Domain.Enums
{
    /// <summary>
    /// 추후 SecsGem등에 사용시 List번호가 바뀌면 안됨.
    /// 라인 들어간 상태라면 각 그룹에 이미 지정된 번호 사이에 추가 하지 말고 이미 들어간 항목도 지우지 말자... 
    /// 아래에만 추가... 즉 기존의 번호를 변경시키는 행위는 하지 말것
    /// </summary>
   public enum ErrorList
    {
        //시스템 관련 ( 연기 센서, 공압 등 )
        None = 0,       
       

        // 100 번 단위로 비슷한 것들 묶어서 사용

        장비상태관련 = 0,   // 장비 상태 관련 , 도어 열림 연기 감지 등
        DOOR_OPEN,
        EMERGENCY_BUTTON_ON,
        FAN_OFF,

        MAIN_VACCUM,
        MAIN_AIR,
        POWER_BOX_SMOKE,
        POWER_BOX_TEMPERURE,
        TEMPERTURE_OVER_HEAT,


        모터상태관련 = 100, // 모터 상태  관련
        MOTOR_DEFINE_ERROR,
        MOTOR_SERVO_OFF,
        MOTOR_HOME_DONE_OFF,
        MOTOR_LIMIT,
        MOTOR_ALARM_ON,
        MOTOR_NETWORK_LOST,
        MOTOR_HOME_SEARCH_FAIL,
        MOTOR_SET_EVENT_OVERRIDE_FAIL,
        MOTOR_INTERLOCK,
        MOTOR_TORQUE_OVERRUN,


        IO관련 = 200, // IO 상태 관련
        IO_INIT_FAIL,
        PIO_INIT_FAIL,


        ACT동작관련 = 300, // ACT 동작 관련
        ACT_ERROR,
        ACT_TIMEOUT,
        
        
        SEQ_TIMEOUT = 400,
        SEQ_ERROR,


        DATA관련 = 500, // DATA 관련


        비전관련 = 600, // 비전 관련
        VISION_DISCONNECT,

        
        ERRLIST700 = 700,
        ERRLIST800 = 800,
        ERRLIST900 = 900,
        ERRLIST1000 = 1000,
        ERRLIST1100 = 1100,
        ERRLIST1200 = 1200,
        ERRLIST1300 = 1300,
        ERRLIST1400 = 1400,
        ERRLIST1500 = 1500,
        ERRLIST1600 = 1600,
        ERRLIST1700 = 1700,
        ERRLIST1800 = 1800,
        ERRLIST1900 = 1900,
        ERRLIST2000 = 2000,
    }
}
