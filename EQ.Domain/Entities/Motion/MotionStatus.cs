using EQ.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Domain.Entities
{
    public struct MotionStatus
    {
        public double CommandPos;
        public double ActualPos;
        public double ActualVelocity;
        public double ActualTorque;
        public bool AmpAlarm;
        public double AmpAlarmCode;
        public bool ServoOn;
        public bool HomeDone;
        public bool InPos;
        public bool NegativeLS;
        public bool PositiveLS;
        public bool HomeSwitch;
        public string OP;

        public double TorqueLimitMax;
        public double TorqueLimitPositive;
        public double TorqueLimitNegative;
    }

    public class posCommand
    {
        public MotionID idx;
        public double targetPostition;
        public double velocity = 0;
        public double acc = 0;
        public double dec = 0;
        public double JerkRatio = 0;        
    }
}
