using EQ.Common.Helper;
using EQ.Common.Logs;
using EQ.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQ.Core.Act.EQ_Hanlim_Extuder
{
    public class ActExtuder : ActComponent
    {
        
        public bool IsSoaking { get; set; } = false;
        long SoakingElspTiem { get; set; } = 1000 * 60; // 최소 1분
        double _SoakingOffPersent = 10;
        public struct RunData
        {
            public double Zone1;
            public double Zone2;
            public double Rpm;
            public double Torque;
        }

        public List<RunData> RunDatas { get; set; } = new List<RunData>();

        public ActExtuder(ACT act) : base(act)
        {
          
        }

        private System.Timers.Timer _timer;



        public void Init()
        {
            IsSoaking = false;
            SoakingElspTiem = _act.Option.Option1.SoakingTime * 1000 * 60;
            _SoakingOffPersent = _act.Option.Option1.SoakingOffMargin;

            _timer = new System.Timers.Timer();
            _timer.Interval = 1000;
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();

          
        }

        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            var _temp1 =_act.Temp.Get(TempID.Zone1).ReadPV();
            var _temp2 = _act.Temp.Get(TempID.Zone2).ReadPV();

            var _run1 = _act.Temp.Get(TempID.Zone1).IsRunning();
            var _run2 = _act.Temp.Get(TempID.Zone2).IsRunning();          
            
            if(IsSoaking) // 온도가 목표 온도에 미달이면 IsSoaking false
            {
                double _temp1Persent = _temp1 * _SoakingOffPersent / 100;
                double _temp2Persent = _temp2 * _SoakingOffPersent / 100;
                
                if(_temp1 < _temp1Persent || _temp2 < _temp2Persent)
                {
                    IsSoaking = false;
                    SoakingElspTiem = _act.Option.Option1.SoakingTime * 1000 * 60;

                    Log.Instance.Info($"[Extuder] Soaking Off Temp1:{_temp1} Temp2:{_temp2}");
                }
            }
            else
            {
                if (_temp1 >= _act.Option.Option1.SoakingZone1 && _temp2 >= _act.Option.Option1.SoakingZone2)
                {
                    SoakingElspTiem -= 1000;
                    if (SoakingElspTiem <= 0)
                    {
                        IsSoaking = true;
                        Log.Instance.Info($"[Extuder] Soaking On Temp1:{_temp1} Temp2:{_temp2}");
                    }
                }
            }

            RunData runData = new RunData()
            {
                Zone1 = _temp1,
                Zone2 = _temp2,
                Rpm = 0,
                Torque = 5
            };
            RunDatas.Add(runData);

        }
    }
}
