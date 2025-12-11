using EQ.Domain.Entities;
using EQ.Domain.Interface;
using System.Data;
using WMX3ApiCLR;
using WMX3ApiCLR.EcApiCLR;
using static WMX3ApiCLR.Config;
using static WMX3ApiCLR.EventControl;
using static WMX3ApiCLR.Motion;

namespace Hardware.Infra.Motion.WMX
{
    public class WMX_Motion : IMotionController
    {
        readonly int coefficient = 1; // Interface 호출부에서 적용 함

        protected WMX3Api Wmx3Lib = new WMX3Api();
        CoreMotionStatus CmStatus;     
        CoreMotion Wmx3Lib_cm;
        EventControl CmControl;
        ComparatorSource ComSrc;
        PSOOutput PsoOut;
        Ecat EcLib;
        EcMasterInfo MotorInfo;

        ProfileType[] MotorProfile;
        double[] MotorJerkRatio;

        // 폴링 제어
        private CancellationTokenSource _cts;
        private Task _pollingTask;
        // 폴링용 버퍼
        CoreMotionStatus _hwStatusBuffer = new CoreMotionStatus();
        // 상태 읽기 전용 Lock
        private readonly object _statusLock = new object();

        public int Init(string parameterPath)
        {
            CmControl = new EventControl();
            ComSrc = new ComparatorSource();
            PsoOut = new PSOOutput();

            Wmx3Lib = new WMX3Api();
            CmStatus = new CoreMotionStatus();         
            Wmx3Lib_cm = new CoreMotion(Wmx3Lib);

            EcLib = new Ecat(Wmx3Lib);
            MotorInfo = new EcMasterInfo();

            int ret = 0;

            // Create device.
            ret += Wmx3Lib.CreateDevice("C:\\Program Files\\SoftServo\\WMX3\\",
                DeviceType.DeviceTypeNormal,
                0xFFFFFFFF);

            // Set Device Name.
            ret += Wmx3Lib.SetDeviceName("MotorControl");

            paraPath = parameterPath; //modelData/wmx_parameters.xml

            Config.AxisParam Parm = new Config.AxisParam();
            ret += Wmx3Lib_cm.Config.Import(paraPath, ref Parm);
            ret += Wmx3Lib_cm.Config.SetAxisParam(Parm);

            //전체 파라메터 업로드
            ret += Wmx3Lib_cm.Config.ImportAndSetAll(paraPath);

            ret += Wmx3Lib.StartCommunication(0xFFFFFFFF);

            //API Buffer Init
         //   apiBufferInit();

            //앱솔루트 모터에 대해 포지션 잡아 줌
            //Absolut_Home();          

            EcMasterInfo ec = new EcMasterInfo();
            EcLib.GetMasterInfo(ec);
            MotorProfile = new ProfileType[ec.Slaves.Length];
            MotorJerkRatio = new double[ec.Slaves.Length];


            if (ret != 0)
            {             
                Wmx3Lib.CloseDevice();
                return -1;
            }

            _cts = new CancellationTokenSource();
            _pollingTask = Task.Run(() => PollingLoop(_cts.Token));

            return 1;
        }
        public void Close()
        {
            _cts?.Cancel();
            // 종료 대기 (선택 사항)
            // _pollingTask?.Wait(1000);

            if (Wmx3Lib != null)
                Wmx3Lib.CloseDevice();
        }


        public bool AbsoluteHome(int id = -1)
        {
            if(id == -1) return false;

            CoreMotionAxisStatus cmAxis = CmStatus.AxesStatus[id];
            var encPos = cmAxis.EncoderFeedback;

            bool absMode = false;
            Wmx3Lib_cm.Config.GetAbsoluteEncoderMode(id, ref absMode);

            if (absMode)
            {
                double homeOffset = 0;
                Wmx3Lib_cm.Config.GetAbsoluteEncoderHomeOffset(id, ref homeOffset       );

                Wmx3Lib_cm.Home.SetHomeDone(id, 0);
                //var nowPos = homeOffset - encPos;
                var nowPos = encPos - homeOffset;

                Wmx3Lib_cm.Home.SetCommandPos(id, (int)nowPos);
                Wmx3Lib_cm.Home.SetHomeDone(id, 1);
                return true;
            }
            return false;
        }

        public bool AlarmReset(int id)
        {
            Wmx3Lib_cm.AxisControl.ClearAmpAlarm(id);

            return true;
        }

        public bool ApiBufferExecute(int chnl)
        {
            throw new NotImplementedException();
        }

        public bool ApiBufferExecute(params int[] chnl)
        {
            throw new NotImplementedException();
        }

        public byte ApiBufferGetUserMemoryByte(uint addr)
        {
            throw new NotImplementedException();
        }

        public bool ApiRecordSoftLandingBasic(int chnl, int motorId, double startPos, double endPos, double startVel, double startAcc, double trgPos, double trgtVel, double trgTrq)
        {
            throw new NotImplementedException();
        }



        public bool EventOverrideExecute(int ch, bool run)
        {
            throw new NotImplementedException();
        }

        public double GetAbsHomePos(int idx)
        {
            bool absMode = false;
            Wmx3Lib_cm.Config.GetAbsoluteEncoderMode(idx, ref absMode);

            if (absMode == false) return 0;

            double homeOffset = 0;
            Wmx3Lib_cm.Config.GetAbsoluteEncoderHomeOffset(idx, ref homeOffset);

            return homeOffset;
        }

        public string GetEcatStatus()
        {
            string str = string.Empty;
            List<string> list = new List<string>();

            EcMasterInfo ec = new EcMasterInfo();
            EcLib.GetMasterInfo(ec);

            list.Add("\nEtherCat Info Start\n");
            list.Add(ec.StatisticsInfo.CycleCounter.ToString() + ","); // cycle no
            list.Add(ec.StatisticsInfo.PacketLoss.ToString() + ",");  // 전체 라인에 대한 packetLoss
            list.Add(ec.NumOfSlaves.ToString() + ",");  // 총 서보팩 수
            list.Add(ec.GetOfflineSlaveCount().ToString() + ","); // 응답 못한 서보팩 수 (네트워크 Line 연결이면 끊어진 축 뒤로 갯수 , 링 연결이면 1 나와야 됨)
            list.Add("\nSlaves Info\n");

            foreach (EcSlaveInfo slaveInfo in ec.Slaves)
            {
                if (slaveInfo.State != EcStateMachine.Op)
                {
                    list.Add(slaveInfo.Id.ToString() + ",");
                    list.Add(slaveInfo.VendorId.ToString() + ",");
                    list.Add(slaveInfo.State.ToString() + ","); // OP가 아니면 통신 불가한 상태임
                    list.Add("\n");
                }
            }
            list.Add("EtherCat Info End");

            str = string.Join("", list.ToArray());
            return str;
        }

        public double GetEncoderPosition(int motorIndex)
        {
            Wmx3Lib_cm.GetStatus(ref CmStatus);
            CoreMotionAxisStatus cmAxis = CmStatus.AxesStatus[motorIndex];
            return cmAxis.ActualPos;
        }

        public string GetErrorStatus()
        {
            throw new NotImplementedException();
        }

        public bool GetAbsType(int idx)
        {
            bool absMode = false;
            Wmx3Lib_cm.Config.GetAbsoluteEncoderMode(idx, ref absMode);

            return absMode;
        }

        public bool GetInPosition(int motorIndex)
        {
            Wmx3Lib_cm.GetStatus(ref CmStatus);
            CoreMotionAxisStatus cmAxis = CmStatus.AxesStatus[motorIndex];

            return cmAxis.PosSet & cmAxis.InPos;
        }

        public MotionStatus GetMotionStatus(int motorIndex)
        {
            Wmx3Lib_cm.GetStatus(ref CmStatus);
            CoreMotionAxisStatus cmAxis = CmStatus.AxesStatus[motorIndex];

            MotionStatus ss = new MotionStatus();
            ss.CommandPos = cmAxis.PosCmd;
            ss.ActualPos = cmAxis.ActualPos;
            ss.ActualVelocity = cmAxis.ActualVelocity;
            ss.ActualTorque = cmAxis.ActualTorque;
            ss.AmpAlarm = cmAxis.AmpAlarm;
            ss.AmpAlarmCode = cmAxis.AmpAlarmCode;
            ss.ServoOn = cmAxis.ServoOn;
            ss.HomeDone = cmAxis.HomeDone;
          
            ss.NegativeLS = cmAxis.NegativeLS;
            ss.PositiveLS = cmAxis.PositiveLS;
            ss.HomeSwitch = cmAxis.HomeSwitch;
            ss.OP = cmAxis.OpState.ToString();

            var trqLimit = GetTrq(motorIndex);
            ss.TorqueLimitMax = trqLimit.Item1;
            ss.TorqueLimitPositive = trqLimit.Item2;
            ss.TorqueLimitNegative = trqLimit.Item3;

            ss.InPos = cmAxis.PosSet & cmAxis.InPos; ;

            return ss;
        }

        private async Task PollingLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    // 하드웨어 통신 (가장 느린 작업)
                    Wmx3Lib_cm.GetStatus(ref _hwStatusBuffer);

                    // (옵션) 여기서 다른 주기적 작업 수행 가능
                }
                catch
                {
                    // 통신 실패 등 예외 처리
                }

                await Task.Delay(20, token); // 20ms 주기
            }
        }
        public void GetStatus(ref DataTable dt)
        {
            try
            {
                // 폴링 루프가 돌고 있지 않으면 강제 갱신 (안전장치)
                if (_pollingTask == null || _pollingTask.IsCompleted)
                {
                    Wmx3Lib_cm.GetStatus(ref _hwStatusBuffer);
                }

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int mID = (int)dt.Rows[i]["ID"];
                    if (mID >= _hwStatusBuffer.AxesStatus.Length) continue;

                    CoreMotionAxisStatus cmAxis = _hwStatusBuffer.AxesStatus[mID];

                    dt.Rows[i]["PosCmd"] = cmAxis.PosCmd;
                    dt.Rows[i]["ActPos"] = cmAxis.ActualPos;
                    dt.Rows[i]["Follow"] = cmAxis.ActualFollowingError;
                    dt.Rows[i]["ActVel"] = cmAxis.ActualVelocity;
                    dt.Rows[i]["ActTrq"] = cmAxis.ActualTorque;
                    dt.Rows[i]["AmpAlm"] = cmAxis.AmpAlarm ;
                    dt.Rows[i]["AmpAlmCode"] = cmAxis.AmpAlarmCode;
                    dt.Rows[i]["SrvOn"] = cmAxis.ServoOn;
                    dt.Rows[i]["HomeDone"] = cmAxis.HomeDone ;
                    dt.Rows[i]["InPos"] = cmAxis.InPos ;
                    dt.Rows[i]["NegLS"] = cmAxis.NegativeLS ;
                    dt.Rows[i]["PosLS"] = cmAxis.PositiveLS ;
                    dt.Rows[i]["HomeSw"] = cmAxis.HomeSwitch ;
                    dt.Rows[i]["OP"] = cmAxis.OpState.ToString();
                }
            }
            catch { }
        }

        public (bool singleTurn, uint singleTurnCount, double PosW, double inPosW, string homeType, string homeDir) GetSysParam(int motorIndex)
        {
            Config.AxisParam Parm = new Config.AxisParam();
            Wmx3Lib_cm.Config.GetAxisParam(motorIndex, ref Parm);

            var singleTurn = Parm.SingleTurnMode[motorIndex];
            var singleTurnCount = Parm.SingleTurnEncoderCount[motorIndex];


            SystemParam sysParam = new SystemParam();
            Wmx3Lib_cm.Config.GetParam(motorIndex, ref sysParam);

            var PosW = sysParam.FeedbackParam[motorIndex].PosSetWidth;
            var inPosW = sysParam.FeedbackParam[motorIndex].InPosWidth;

            var homeType = sysParam.HomeParam[motorIndex].HomeType.ToString();
            var homeDir = sysParam.HomeParam[motorIndex].HomeDirection.ToString();


            return (singleTurn, singleTurnCount, PosW, inPosW, homeType, homeDir);
        }

        public (double, double, double) GetTrq(int motorIndex)
        {
            double max = 0;
            double pos = 0;
            double neg = 0;
            Wmx3Lib_cm.Torque.GetMaxTrqLimit(motorIndex, ref max);
            Wmx3Lib_cm.Torque.GetPositiveTrqLimit(motorIndex, ref pos);
            Wmx3Lib_cm.Torque.GetNegativeTrqLimit(motorIndex, ref neg);

            return (max, pos, neg);
        }

        public bool Home(int id)
        {
            AxisCommandMode mode = AxisCommandMode.Position;
            Wmx3Lib_cm.AxisControl.GetAxisCommandMode(id, ref mode);
            if (mode == AxisCommandMode.Torque)
                Wmx3Lib_cm.Torque.StopTrq(id);

            Wmx3Lib_cm.AxisControl.SetAxisCommandMode(id, AxisCommandMode.Position);

            Wmx3Lib_cm.Home.StartHome(id);

            return true;
        }

        public bool HomeCancel(int id)
        {
            Wmx3Lib_cm.Home.Cancel(id);
            Wmx3Lib_cm.Home.SetHomeDone(id, 0);
            Wmx3Lib_cm.Motion.ExecQuickStop(id);
            return true;
        }

        public bool HomeClear(int id)
        {
            Wmx3Lib_cm.Home.SetHomeDone(id, 0);           
            return true;
        }

        public bool HomeDone(int id)
        {
            Wmx3Lib_cm.Home.SetHomeDone(id, 1);           
            return true;
        }

        public bool HotConnect()
        {
            EcMasterInfo ec = new EcMasterInfo();
            EcLib.GetMasterInfo(ec);

            if (MotorInfo.State != EcStateMachine.Op)
            {
                return false;
            }

            var isLost = false;
            var nowAlive = IsAllAlive(ref isLost);

            if (isLost)
            {
                EcLib.StartHotconnect();
            }

            nowAlive = IsAllAlive(ref isLost);

            if (nowAlive && isLost == false)
                return true;
            else
                return false;
        }
        bool IsAllAlive(ref bool isLost)
        {
            bool bRet = true;

            EcLib.GetMasterInfo(MotorInfo);

            // Check Slave alive.
            isLost = false;
            for (int i = 0; i < MotorInfo.NumOfSlaves; i++)
            {
                if (MotorInfo.Slaves[i].State != EcStateMachine.Op)
                {
                    bRet = false;

                    // Check Slave lost. 
                    if ((MotorInfo.Slaves[i].State == EcStateMachine.None) ||
                         (MotorInfo.Slaves[i].State == EcStateMachine.Init))
                    {
                        isLost = true;
                    }
                }
            }

            return bRet;
        }



        public bool JogMoveStart(posCommand cmd, bool dirPositive)
        {
            int dir = dirPositive ? 1 : -1;
            JogCommand _cmd = new JogCommand();

            _cmd.Axis = (int)cmd.idx;
            _cmd.Profile.Type = MotorProfile[(int)cmd.idx];
            _cmd.Profile.JerkAccRatio = MotorJerkRatio[(int)cmd.idx];
            _cmd.Profile.JerkDecRatio = MotorJerkRatio[(int)cmd.idx];
            //_cmd.Profile.AccTimeMilliseconds = cmd.acc;
            //_cmd.Profile.DecTimeMilliseconds = cmd.dec;

            _cmd.Profile.Velocity = cmd.velocity * dir * coefficient;
            _cmd.Profile.Acc = cmd.acc * coefficient;
            _cmd.Profile.Dec = cmd.dec * coefficient;

            Wmx3Lib_cm.AxisControl.SetAxisCommandMode((int)cmd.idx, AxisCommandMode.Position);

            Wmx3Lib_cm.Motion.StartJog(_cmd);


            return true;
        }

        public bool JogMoveStop(int motorIndex)
        {
            Wmx3Lib_cm.Motion.ExecQuickStop(motorIndex);
            return true;
        }

        public bool MoveAbs(posCommand cmd)
        {
            PosCommand posCommand = new PosCommand();

            // posCommand.Profile.Type = WMX3ApiCLR.ProfileType.TimeAccSCurve;
            posCommand.Profile.Type = MotorProfile[(int)cmd.idx]; ;
            posCommand.Profile.JerkAccRatio = MotorJerkRatio[(int)cmd.idx];
            posCommand.Profile.JerkDecRatio = MotorJerkRatio[(int)cmd.idx];
            posCommand.Profile.AccTimeMilliseconds = cmd.acc;
            posCommand.Profile.DecTimeMilliseconds = cmd.dec;


            //posCommand.Profile.JerkAcc = cmd.jerkAcc;
            //posCommand.Profile.JerkDec = cmd.jerkDcc;
            posCommand.Axis = (int)cmd.idx;
            posCommand.Target = cmd.targetPostition;
            posCommand.Profile.Velocity = cmd.velocity * coefficient;
            posCommand.Profile.Acc = cmd.acc * coefficient;
            posCommand.Profile.Dec = cmd.dec * coefficient;
            /*
            posCommand.Profile.Type = WMX3ApiCLR.ProfileType.SCurve;
            posCommand.Profile.JerkAcc = 0.5;
            posCommand.Profile.JerkDec = 0.5;
            posCommand.Axis = (int)cmd.idx;
            posCommand.Target = cmd.targetPostition;
            posCommand.Profile.Velocity = cmd.velocity * coefficient;
            posCommand.Profile.Acc = cmd.acc * coefficient;
            posCommand.Profile.Dec = cmd.dec * coefficient;
            */

            AxisCommandMode mode = AxisCommandMode.Position;
            Wmx3Lib_cm.AxisControl.GetAxisCommandMode((int)cmd.idx, ref mode);
            if (mode == AxisCommandMode.Torque)
                Wmx3Lib_cm.Torque.StopTrq((int)cmd.idx);

            Wmx3Lib_cm.AxisControl.SetAxisCommandMode((int)cmd.idx, AxisCommandMode.Position);

            Wmx3Lib_cm.Motion.StartPos(posCommand);

            //Wmx3Lib_cm.Motion.StartTrqToPos

            Wmx3Lib_cm.GetStatus(ref CmStatus);
            CoreMotionAxisStatus cmAxis = CmStatus.AxesStatus[(int)cmd.idx];

            return true;
        }

        public bool MoveAbs(posCommand[] cmd)
        {
            PosCommand[] posCommand = new PosCommand[cmd.Length];

            for (int i = 0; i < cmd.Length; i++)
            {
                posCommand[i] = new PosCommand();
                posCommand[i].Profile.Type = MotorProfile[(int)cmd[i].idx]; ;
                posCommand[i].Profile.JerkAccRatio = MotorJerkRatio[(int)cmd[i].idx];
                posCommand[i].Profile.JerkDecRatio = MotorJerkRatio[(int)cmd[i].idx];
                //posCommand[i].Profile.AccTimeMilliseconds = cmd[i].acc;
                //posCommand[i].Profile.DecTimeMilliseconds = cmd[i].dec;

                //posCommand[i].Profile.JerkAcc = cmd[i].jerkAcc;
                //posCommand[i].Profile.JerkDec = cmd[i].jerkDcc;
                posCommand[i].Axis = (int)cmd[i].idx;
                posCommand[i].Target = cmd[i].targetPostition;
                posCommand[i].Profile.Velocity = cmd[i].velocity * coefficient;
                posCommand[i].Profile.Acc = cmd[i].acc * coefficient;
                posCommand[i].Profile.Dec = cmd[i].dec * coefficient;

                AxisCommandMode mode = AxisCommandMode.Position;
                Wmx3Lib_cm.AxisControl.GetAxisCommandMode((int)cmd[i].idx, ref mode);
                if (mode == AxisCommandMode.Torque)
                    Wmx3Lib_cm.Torque.StopTrq((int)cmd[i].idx);

                Wmx3Lib_cm.AxisControl.SetAxisCommandMode((int)cmd[i].idx, AxisCommandMode.Position);
            }
            Wmx3Lib_cm.Motion.StartPos((uint)cmd.Length, posCommand);
            Wmx3Lib_cm.GetStatus(ref CmStatus);
          
            return true;
        }

        public bool MoveEStop(int idx)
        {
            Wmx3Lib_cm.ExecEStop(EStopLevel.Level1);
            return true;
        }

        public bool MoveRel(posCommand cmd)
        {
            PosCommand posCommand = new PosCommand();
            posCommand.Profile.Type = MotorProfile[(int)cmd.idx]; ;
            posCommand.Profile.JerkAccRatio = MotorJerkRatio[(int)cmd.idx];
            posCommand.Profile.JerkDecRatio = MotorJerkRatio[(int)cmd.idx];

            //posCommand.Profile.AccTimeMilliseconds = cmd.acc;
            //posCommand.Profile.DecTimeMilliseconds = cmd.dec;

            //posCommand.Profile.JerkAcc = cmd.jerkAcc;
            //posCommand.Profile.JerkDec = cmd.jerkDcc;
            posCommand.Axis = (int)cmd.idx;
            posCommand.Target = cmd.targetPostition;
            posCommand.Profile.Velocity = cmd.velocity * coefficient;
            posCommand.Profile.Acc = cmd.acc * coefficient;
            posCommand.Profile.Dec = cmd.dec * coefficient;

            AxisCommandMode mode = AxisCommandMode.Position;
            Wmx3Lib_cm.AxisControl.GetAxisCommandMode((int)cmd.idx, ref mode);
            if (mode == AxisCommandMode.Torque)
                Wmx3Lib_cm.Torque.StopTrq((int)cmd.idx);

            Wmx3Lib_cm.AxisControl.SetAxisCommandMode((int)cmd.idx, AxisCommandMode.Position);

            Wmx3Lib_cm.Motion.StartMov(posCommand);

            Wmx3Lib_cm.GetStatus(ref CmStatus);
            CoreMotionAxisStatus cmAxis = CmStatus.AxesStatus[(int)cmd.idx];

          
            return true;
        }

        public bool MoveRel(posCommand[] cmd)
        {
            PosCommand[] posCommand = new PosCommand[cmd.Length];

            for (int i = 0; i < cmd.Length; i++)
            {
                posCommand[i] = new PosCommand();
                posCommand[i].Profile.Type = MotorProfile[(int)cmd[i].idx]; ;
                posCommand[i].Profile.JerkAccRatio = MotorJerkRatio[(int)cmd[i].idx];
                posCommand[i].Profile.JerkDecRatio = MotorJerkRatio[(int)cmd[i].idx];

                //posCommand[i].Profile.AccTimeMilliseconds = cmd[i].acc;
                //posCommand[i].Profile.DecTimeMilliseconds = cmd[i].dec;
                //posCommand[i].Profile.JerkAcc = cmd[i].jerkAcc;
                //posCommand[i].Profile.JerkDec = cmd[i].jerkDcc;
                posCommand[i].Axis = (int)cmd[i].idx;
                posCommand[i].Target = cmd[i].targetPostition;
                posCommand[i].Profile.Velocity = cmd[i].velocity * coefficient;
                posCommand[i].Profile.Acc = cmd[i].acc * coefficient;
                posCommand[i].Profile.Dec = cmd[i].dec * coefficient;

                AxisCommandMode mode = AxisCommandMode.Position;
                Wmx3Lib_cm.AxisControl.GetAxisCommandMode((int)cmd[i].idx, ref mode);
                if (mode == AxisCommandMode.Torque)
                    Wmx3Lib_cm.Torque.StopTrq((int)cmd[i].idx);

                Wmx3Lib_cm.AxisControl.SetAxisCommandMode((int)cmd[i].idx, AxisCommandMode.Position);


            }

            Wmx3Lib_cm.Motion.StartMov((uint)cmd.Length, posCommand);
            Wmx3Lib_cm.GetStatus(ref CmStatus);
           
            return true;
        }

        public bool MoveStop(int idx)
        {
            //  Wmx3Lib_cm.Motion.Stop((int)idx);
            Wmx3Lib_cm.Motion.ExecQuickStop((int)idx);
            Wmx3Lib_cm.GetStatus(ref CmStatus);
            CoreMotionAxisStatus cmAxis = CmStatus.AxesStatus[(int)idx];

            AxisCommandMode mode = AxisCommandMode.Position;
            Wmx3Lib_cm.AxisControl.GetAxisCommandMode(idx, ref mode);

            if (mode == AxisCommandMode.Torque)
                Wmx3Lib_cm.Torque.StopTrq(idx);
            if (mode == AxisCommandMode.Velocity)
                Wmx3Lib_cm.Velocity.Stop(idx);

            if (cmAxis.InPos)
                return true;
            else
                return false;
        }

        public bool MoveTrq(int motorIndex, double torque, double motorRpm)
        {
            Wmx3Lib_cm.AxisControl.SetAxisCommandMode(motorIndex, AxisCommandMode.Torque);

            Torque.TrqCommand _trq = new Torque.TrqCommand();
            _trq.Axis = motorIndex;
            _trq.Torque = torque;

            Wmx3Lib_cm.Torque.StartTrq(_trq, motorRpm);

            return true;
        }

        public bool MoveVel(posCommand cmd)
        {
            AxisCommandMode mode = AxisCommandMode.Position;
            Wmx3Lib_cm.AxisControl.GetAxisCommandMode((int)cmd.idx, ref mode);
            if (mode == AxisCommandMode.Torque)
                Wmx3Lib_cm.Torque.StopTrq((int)cmd.idx);
            if (mode == AxisCommandMode.Position)
                Wmx3Lib_cm.Motion.ExecQuickStop((int)cmd.idx);

            Wmx3Lib_cm.AxisControl.SetAxisCommandMode((int)cmd.idx, AxisCommandMode.Velocity);

            Velocity.VelCommand posCommand = new Velocity.VelCommand();

            posCommand.Profile.Type = MotorProfile[(int)cmd.idx]; ;
            posCommand.Profile.JerkAccRatio = MotorJerkRatio[(int)cmd.idx];
            posCommand.Profile.JerkDecRatio = MotorJerkRatio[(int)cmd.idx];
            //posCommand.Profile.JerkAcc = cmd.jerkAcc;
            //posCommand.Profile.JerkDec = cmd.jerkDcc;
            posCommand.Axis = (int)cmd.idx;

            posCommand.Profile.Velocity = cmd.velocity * coefficient;
            posCommand.Profile.Acc = cmd.acc * coefficient;
            posCommand.Profile.Dec = cmd.dec * coefficient;

            Wmx3Lib_cm.Velocity.StartVel(posCommand);
            return true;
        }

        string paraPath = "";
        public bool SaveParameter()
        {
            Config.AxisParam Parm = new Config.AxisParam();
            var t = Wmx3Lib_cm.Config.GetAndExportAll(paraPath);

            return true;
        }

        public bool ServoOn(int id, int onOff)
        {
            Wmx3Lib_cm.AxisControl.SetServoOn(id, onOff);
            return true;
        }

        public bool SetEventOverride(int ch, bool dirPositive, posCommand[] cmd)
        {
            throw new NotImplementedException();
        }

        public bool SetProfile(bool[] profile, double[] jerkRatio)
        {
            if (MotorProfile.Length < profile.Length || MotorJerkRatio.Length < jerkRatio.Length)
            {
                MotorProfile = new ProfileType[profile.Length];
                MotorJerkRatio = new double[jerkRatio.Length];
            }

            for (int i = 0; i < profile.Length; i++)
            {
                if (profile[i])
                    MotorProfile[i] = ProfileType.SCurve;
                else
                    MotorProfile[i] = ProfileType.Trapezoidal;
                MotorJerkRatio[i] = jerkRatio[i];
            }

            return true;
        }

        public bool SetTrq(int motorIndex, double maxTrq, double positiveTrq, double negativeTrq)
        {
            maxTrq = Math.Abs(maxTrq);
            positiveTrq = Math.Abs(positiveTrq);
            negativeTrq = Math.Abs(negativeTrq);

            Wmx3Lib_cm.Torque.SetMaxTrqLimit(motorIndex, maxTrq);
            Wmx3Lib_cm.Torque.SetPositiveTrqLimit(motorIndex, positiveTrq);
            Wmx3Lib_cm.Torque.SetNegativeTrqLimit(motorIndex, negativeTrq);

            double readValue = 0;

            Wmx3Lib_cm.Torque.GetMaxTrqLimit(motorIndex, ref readValue);
            if (readValue != maxTrq) return false;

            Wmx3Lib_cm.Torque.GetPositiveTrqLimit(motorIndex, ref readValue);
            if (readValue != positiveTrq) return false;

            Wmx3Lib_cm.Torque.GetNegativeTrqLimit(motorIndex, ref readValue);
            if (readValue != negativeTrq) return false;

            return true;
        }

        public bool SyncReset(int masterId, int slaveId)
        {
            Wmx3Lib_cm.Sync.ResolveSync(slaveId);
            return true;
        }

        public bool SyncSet(int masterId, int slaveId)
        {
            Wmx3Lib_cm.Sync.SetSyncMasterSlave(masterId, slaveId);
            return true;
        }

        public bool SDO_Write(int slaveId, int SdoIndex, int SdoSubIndex, int writeData)
        {

            const int sdoBuffSize = 4;                 // [in]  SDO buffer size

            int index = SdoIndex;            // [in]  Index number of SDO
            int subIndex = SdoSubIndex;            // [in]  Sub index number of SDO

            uint waitTime = 3000;              // [in]  SDO Upload Process timeout time
            byte[] sdoWriteBuff = new Byte[sdoBuffSize];
            // [in]  SDO write buffer
            int writeSDOData = writeData;              // [in]  Write SDO data.

            uint errorCode = 0;                 // [out] SDO Error code
            uint actualSize = 0;                 // [out] Size of actually uploaded SDO data
            byte[] sdoReadBuff = new Byte[sdoBuffSize];
            // [out] SDO read buffer
            int readSDOData = 0;                 // [out] Read SDO data.

            sdoWriteBuff = BitConverter.GetBytes(writeSDOData);
            EcLib.SdoDownload(slaveId, index, subIndex, sdoWriteBuff, ref errorCode);

            EcLib.SdoUpload(slaveId, index, subIndex, sdoReadBuff, ref actualSize, ref errorCode, waitTime);
           
            return sdoReadBuff == sdoWriteBuff ? true : false;
            
        }

        public byte[] SDO_Read(int slaveId, int SdoIndex, int SdoSubIndex)
        {
            const int sdoBuffSize = 4;                 // [in]  SDO buffer size

            int index = SdoIndex;            // [in]  Index number of SDO
            int subIndex = SdoSubIndex;            // [in]  Sub index number of SDO

            uint waitTime = 3000;              // [in]  SDO Upload Process timeout time
            byte[] sdoWriteBuff = new Byte[sdoBuffSize];
            // [in]  SDO write buffer

            uint errorCode = 0;                 // [out] SDO Error code
            uint actualSize = 0;                 // [out] Size of actually uploaded SDO data
            byte[] sdoReadBuff = new Byte[sdoBuffSize];

            int readSDOData = 0;                 // [out] Read SDO data.

            EcLib.SdoUpload(slaveId, index, subIndex, sdoReadBuff, ref actualSize, ref errorCode, waitTime);
            return sdoReadBuff;
        }

        public byte[] PDO_Read(int masterId, int slaveId, int SdoIndex, int SdoSubIndex)
        {

            const int sdoBuffSize = 4;                 // [in]  SDO buffer size

            int index = SdoIndex;            // [in]  Index number of SDO
            int subIndex = SdoSubIndex;            // [in]  Sub index number of SDO

            uint waitTime = 3000;              // [in]  SDO Upload Process timeout time
            byte[] sdoWriteBuff = new Byte[sdoBuffSize];
            // [in]  SDO write buffer

            uint errorCode = 0;                 // [out] SDO Error code
            uint actualSize = 0;                 // [out] Size of actually uploaded SDO data
            byte[] sdoReadBuff = new Byte[sdoBuffSize];

            int readSDOData = 0;                 // [out] Read SDO data.

            EcLib.PdoRead(masterId, slaveId, index, subIndex, sdoReadBuff, ref actualSize);
            return sdoReadBuff;
        }

        public byte[] PDO_Write(int masterId, int slaveId, int SdoIndex, int SdoSubIndex, int writeData)
        {

            const int sdoBuffSize = 4;                 // [in]  SDO buffer size

            int index = SdoIndex;            // [in]  Index number of SDO
            int subIndex = SdoSubIndex;            // [in]  Sub index number of SDO
            int writeSDOData = writeData;

            uint waitTime = 3000;              // [in]  SDO Upload Process timeout time
            byte[] sdoWriteBuff = new Byte[sdoBuffSize];
            // [in]  SDO write buffer

            uint errorCode = 0;                 // [out] SDO Error code
            uint actualSize = 0;                 // [out] Size of actually uploaded SDO data
            byte[] sdoReadBuff = new Byte[sdoBuffSize];

            int readSDOData = 0;                 // [out] Read SDO data.

            sdoWriteBuff = BitConverter.GetBytes(writeSDOData);

            EcLib.TxPdoWrite(masterId, slaveId, index, subIndex, sdoReadBuff, 0);
            return sdoReadBuff;
        }
    }
}