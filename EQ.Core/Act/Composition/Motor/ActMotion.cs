using EQ.Common.Helper;
using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Entities; // MotionStatus 사용
using EQ.Domain.Enums;    // MotorID 사용

using static EQ.Core.Globals;

namespace EQ.Core.Act
{
    public class ActMotion : ActComponent
    {
        private IMotionController _ioHardware;

        public ActMotion(ACT act) : base(act) { }

        public void SetHardwareController(IMotionController controller)
        {
            this._ioHardware = controller;
            _ioHardware.Init("ModelData/wmx_parameters.xml");
        }

        /// <summary>
        /// 특정 축의 상태 (인자 변경: int -> MotorID)
        /// </summary>
        public MotionStatus GetStatus(MotionID motorId)
        {
            if (_ioHardware == null) return new MotionStatus();
            // Infra 계층(IMotionController)은 int를 받으므로 캐스팅하여 전달
            return _ioHardware.GetMotionStatus((int)motorId);
        }


        public bool ServoOn(MotionID motorId, bool on)
        {
            return _ioHardware?.ServoOn((int)motorId, on ? 1 : 0) ?? false;
        }


        public bool AlarmReset(MotionID motorId)
        {
            return _ioHardware?.AlarmReset((int)motorId) ?? false;
        }


        public bool Home(MotionID motorId)
        {
            return _ioHardware?.Home((int)motorId) ?? false;
        }

        public bool MoveStop(MotionID motorId)
        {
            return _ioHardware?.MoveStop((int)motorId) ?? false;
        }


        public bool GetAbsType(MotionID motorId)
        {
            return _ioHardware?.GetAbsType((int)motorId) ?? false;
        }

        // ---------------------------

        private bool CheckServoReady(MotionID axisName)
        {
            var status = GetStatus(axisName);

            if (!status.ServoOn)
            {
                _act.PopupAlarm(ErrorList.MOTOR_SERVO_OFF, L("{0} Servo Off", axisName));
                return false;
            }
            if (status.AmpAlarm)
            {
                _act.PopupAlarm(ErrorList.MOTOR_ALARM_ON, L("{0} Amp Alarm", axisName));
                return false;
            }
            if (!status.HomeDone)
            {
                _act.PopupAlarm(ErrorList.MOTOR_HOME_DONE_OFF, L("{0} Home Check", axisName));
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="targetPos"></param>
        /// <param name="Mode">0:Abs 1:Rel 2:Jog + 3:Jog -</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        private bool CheckInterlock(MotionID id , double targetPos , int Mode , ref string msg)
        {            
            var interlock = ActManager.Instance.Act.Option.Interlock;

            var interlockItems = interlock.GetList(id);                      

            //IO 상태
            var (inData, outData) = ActManager.Instance.Act.IO.GetIoStatus();
            var inputBits = ByteToBitConvert.Convert(inData);
            var outputBits = ByteToBitConvert.Convert(outData);

            //현재 모터 상태
            var targetStatus = ActManager.Instance.Act.Motion.GetStatus(id);

            if (Mode == 0)
            { }
            else if (Mode == 1)
                targetPos = targetStatus.ActualPos + targetPos;
            else if (Mode == 2)
                targetPos = targetStatus.ActualPos + (5000); // Jog는 5mm 더 간다고 가정
            else if (Mode == 3)
                targetPos = targetStatus.ActualPos + (-5000);

            //방향 판단
            int dir = targetPos > targetStatus.ActualPos ? 1 : 2;

            string _msg = $"[{id}] => ";

            foreach (var item in interlockItems)
            {
                bool interlockSet = false;
                
                switch (item.Type)
                {
                    case InterLockType.Position:
                    case InterLockType.DefinedPos:
                        // 1. 이동 제한 방향 확인 (0:Both, 1:Positive, 2:Negative)
                        // 설정된 금지 방향이 '양방향(0)'이거나 '현재 이동 방향(dir)'과 같을 때만 체크
                        if (item.StopDir == StopDirection.Both || (int)item.StopDir == dir)
                        {
                            double checkValue = 0;

                            // 2. 비교할 값 결정 (자신 vs 타 축)
                            if (item.SourceAxis == id)
                            {
                                // 조건이 '자기 자신'인 경우: 이동하려는 '목표 위치(targetPos)'가 금지 구역인지 확인
                                checkValue = targetPos;
                            }
                            else
                            {
                                // 조건이 '다른 축'인 경우: 그 축의 '현재 위치(ActualPos)'가 조건에 맞는지 확인
                                var sourceStatus = ActManager.Instance.Act.Motion.GetStatus(item.SourceAxis);
                                checkValue = sourceStatus.ActualPos;
                            }

                            // 3. 비교 조건에 따른 인터락 여부 판단
                            switch (item.Condition)
                            {
                                case CompareCondition.Less: // (Value < 기준값) 이면 걸림
                                    if (checkValue < item.CompareValue) interlockSet = true;
                                    break;

                                case CompareCondition.Greater: // (Value > 기준값) 이면 걸림
                                    if (checkValue > item.CompareValue) interlockSet = true;
                                    break;

                                case CompareCondition.Equal: // (기준값 - Range <= Value <= 기준값 + Range) 범위 내면 걸림
                                    if (Math.Abs(checkValue - item.CompareValue) <= item.Range) interlockSet = true;
                                    break;

                                case CompareCondition.NotEqual: // 범위 밖이면 걸림
                                    if (Math.Abs(checkValue - item.CompareValue) > item.Range) interlockSet = true;
                                    break;
                            }

                            // 4. 인터락 발생 시 메시지 생성
                            if (interlockSet)
                            {
                                _msg += $"Position Limit! [Source: {item.SourceAxis}] Val({checkValue:F3}) {item.Condition} Ref({item.CompareValue:F3})";
                            }
                        }
                        break;
                    case InterLockType.IoInput:
                        {
                            if (item.IoIndex >= 0 && item.IoIndex < inputBits.Count)
                            {                            
                                if(item.StopDir == 0 || (int)item.StopDir == dir)
                                {
                                    interlockSet = (inputBits[item.IoIndex] == item.IoSignal);
                                    _msg += $"IO input {item.IoIndex} => {item.IoSignal} dir:{item.StopDir} InterLock Set!!";
                                }
                            }
                            else
                            {
                               interlockSet = true;
                               _msg += $"IO input 없는 인덱스 {item.IoIndex} 선택 함";                                
                            }
                           
                        }
                        break;
                    case InterLockType.IoOutput:
                        {
                            if (item.IoIndex >= 0 && item.IoIndex < outputBits.Count)
                            {
                                if (item.StopDir == 0 || (int)item.StopDir == dir)
                                {
                                    interlockSet = (outputBits[item.IoIndex] == item.IoSignal);
                                    _msg += $"IO output {item.IoIndex} => {item.IoSignal} dir:{item.StopDir} InterLock Set!!";
                                }                                    
                            }
                            else
                            {
                                interlockSet = true;
                                _msg += $"IO output 없는 인덱스 {item.IoIndex} 선택 함";
                            }
                          
                        }
                        break;
                }

                if (interlockSet)
                {
                    msg = _msg;
                    return true;
                }              
               
            }
            return false;
        }

        public async Task<ActionStatus> MoveAbsAsync(params MotionKey[] motionPosition)
        {
            string actionTitle = $"MoveAbs_{string.Join("_", motionPosition.Select(x => x.Axis))}";

            // [최적화] 루프 돌 때마다 찾지 않도록 미리 데이터 준비
            // Key: Axis, Value: MotionPosItem
            var targetDatas = new Dictionary<MotionID, MotionPosItem>();

            foreach (var target in motionPosition)
            {
                var posData = _act.Option.MotionPos.Get(target.Key);
                if (posData == null)
                {
                    _act.PopupAlarm(ErrorList.DATA관련, L("Position Data Not Found: {0}", target.Key));
                    return ActionStatus.Error;
                }
                targetDatas.Add(target.Axis, posData);
            }

            var resultStatus = await _act.ExecuteAction(
                title: actionTitle,
                stepNames: new List<string> { "Start", "InterLockCheck", "Move", "MoveDoneCheck", "End" },
                stepLogic: async (context, stepName) =>
                {
                    int nextStep = context.StepIndex;

                    switch (stepName)
                    {
                        case "Start":
                            // 로그 등 필요 시 추가
                            nextStep++;
                            break;

                        case "InterLockCheck":
                            {
                                foreach (var kvp in targetDatas)
                                {
                                    MotionID axis = kvp.Key;
                                    MotionPosItem posItem = kvp.Value;

                                    // 1. 서보 상태 확인
                                    if (!CheckServoReady(axis))
                                    {
                                        _act.PopupAlarm(ErrorList.MOTOR_SERVO_OFF, L("{0}", axis));
                                        context.Status = ActionStatus.Error;
                                        return nextStep;
                                    }

                                    // 2. 인터락 사전 체크 (Mode 0: 절대 위치 이동)
                                    string msg = string.Empty;
                                    if (CheckInterlock(axis, posItem.Position, 0, ref msg))
                                    {
                                        _act.PopupAlarm(ErrorList.MOTOR_INTERLOCK, L("{0}", msg));
                                        context.Status = ActionStatus.Error;
                                        return nextStep;
                                    }
                                }
                                nextStep++;
                            }
                            break;

                        case "Move":
                            {
                                var commands = new List<posCommand>();

                                foreach (var kvp in targetDatas)
                                {
                                    MotionID axis = kvp.Key;
                                    MotionPosItem posItem = kvp.Value;
                                    var defaultSpeed = _act.Option.MotionSpeed.Get(axis);

                                    commands.Add(new posCommand
                                    {
                                        idx = axis,
                                        targetPostition = posItem.Position,
                                        velocity = (posItem.Speed == 0) ? defaultSpeed.AutoSpeed * 1000 : posItem.Speed * 1000,
                                        acc = (posItem.Acc == 0) ? defaultSpeed.Accel * 1000 : posItem.Acc * 1000,
                                        dec = (posItem.Dec == 0) ? defaultSpeed.Deaccel * 1000 : posItem.Dec * 1000
                                    });
                                }

                                if (_ioHardware.MoveAbs(commands.ToArray()))
                                {
                                    nextStep++;
                                }
                                else
                                {
                                    context.Status = ActionStatus.Error;
                                }
                            }
                            break;

                        case "MoveDoneCheck":
                            {
                                bool allFinished = true;

                                foreach (var kvp in targetDatas)
                                {
                                    MotionID axis = kvp.Key;
                                    MotionPosItem posItem = kvp.Value;

                                    // 1. 도착 여부 확인
                                    if (!_ioHardware.GetInPosition((int)axis))
                                    {
                                        allFinished = false;
                                        // (주의) 여기서 break를 하면 아래 인터락 체크를 안 하게 되므로,
                                        // 이동 중인 축에 대해서도 인터락 검사를 하려면 break 위치를 조정해야 합니다.
                                        // 하지만 보통 하나라도 이동 중이면 계속 검사해야 하므로 아래 로직을 수행합니다.
                                    }

                                    // 2. [중요] 이동 중 실시간 인터락 감시
                                    string msg = string.Empty;
                                    if (CheckInterlock(axis, posItem.Position, 0, ref msg))
                                    {
                                        _act.PopupAlarm(ErrorList.MOTOR_INTERLOCK, L("Moving Interlock Detected: {0}", msg));
                                        context.Status = ActionStatus.Error; // 즉시 중단
                                        return nextStep;
                                    }
                                }

                                if (allFinished)
                                {
                                    nextStep++;
                                }
                                else
                                {
                                    await Task.Delay(10);
                                }
                            }
                            break;

                        case "End":
                            context.Status = ActionStatus.Finished;
                            break;

                        default:
                            context.Status = ActionStatus.Error;
                            break;
                    }
                    return nextStep;
                }
            );

            // [안전 정지] 에러 발생 또는 타임아웃 등으로 비정상 종료 시 모터 정지
            if (resultStatus != ActionStatus.Finished)
            {
                foreach (var target in motionPosition)
                {
                    MoveStop(target.Axis);
                }
            }

            return resultStatus;
        }

        public async Task<ActionStatus> MoveAbsAsync(params posCommand[] commands)
        {
            // 타이틀 생성 (디버깅용)
            string actionTitle = $"MoveAbs_Cmd_{string.Join("_", commands.Select(c => c.idx))}";

            var x = await _act.ExecuteAction(
                title: actionTitle,
                stepNames: new List<string> { "Start", "InterLockCheck", "paramCheck", "Move", "MoveDoneCheck", "End" },
                stepLogic: async (context, stepName) =>
                {
                    int nextStep = context.StepIndex;

                    switch (stepName)
                    {
                        case "Start":
                            nextStep++;
                            break;

                        case "InterLockCheck":
                            {
                                foreach (var target in commands)
                                {
                                    // 1. 서보 온 상태 체크
                                    if (!CheckServoReady((MotionID)target.idx))
                                    {
                                        _act.PopupAlarm(ErrorList.MOTOR_SERVO_OFF, L("{0}", target.idx));
                                        context.Status = ActionStatus.Error;
                                        return nextStep;
                                    }

                                    // 2. 인터락 사전 체크
                                    // posCommand는 이미 targetPosition을 가지고 있으므로 MotionPos.Get() 불필요
                                    string msg = string.Empty;
                                    if (CheckInterlock((MotionID)target.idx, target.targetPostition, 0, ref msg))
                                    {
                                        _act.PopupAlarm(ErrorList.MOTOR_INTERLOCK, L("{0}", msg));
                                        context.Status = ActionStatus.Error;
                                        return nextStep;
                                    }
                                }
                                nextStep++;
                            }
                            break;

                        case "paramCheck":
                            {
                                // 속도/가감속 계수 적용
                                SetCoefficient(ref commands);
                                nextStep++;
                            }
                            break;

                        case "Move":
                            {
                                bool result = _ioHardware.MoveAbs(commands);

                                if (result)
                                {
                                    nextStep++;
                                }
                                else
                                {
                                    Common.Logs.Log.Instance.Error($"[MoveAbs] Hardware Command Failed.");
                                    context.Status = ActionStatus.Error;
                                }
                            }
                            break;

                        case "MoveDoneCheck":
                            {
                                bool allFinished = true;

                                foreach (var cmd in commands)
                                {
                                    // 1. 인포지션 확인
                                    // (GetStatus 함수를 통해 하드웨어 상태 갱신)
                                    var status = GetStatus((MotionID)cmd.idx);

                                    if (!status.InPos)
                                    {
                                        allFinished = false;
                                        // 주의: 여기서 break 하지 않고 아래 인터락 체크를 계속 수행하는 것이 안전합니다.
                                    }

                                    // 2. [중요] 이동 중 실시간 인터락 감시
                                    string msg = string.Empty;
                                    if (CheckInterlock((MotionID)cmd.idx, cmd.targetPostition, 0, ref msg))
                                    {
                                        _act.PopupAlarm(ErrorList.MOTOR_INTERLOCK, L("Moving Interlock: {0}", msg));
                                        context.Status = ActionStatus.Error;
                                        return nextStep; // 루프를 즉시 탈출하여 정지 로직으로 이동
                                    }
                                }

                                if (allFinished)
                                {
                                    nextStep++;
                                }
                                else
                                {
                                    await Task.Delay(10); // 폴링 주기
                                }
                            }
                            break;

                        case "End":
                            context.Status = ActionStatus.Finished;
                            break;

                        default:
                            context.Status = ActionStatus.Error;
                            break;
                    }
                    return nextStep;
                }
            );

            // [안전 정지] 에러/인터락/타임아웃 발생 시 모든 관련 축 정지
            if (x != ActionStatus.Finished)
            {
                foreach (var target in commands)
                {
                    MoveStop((MotionID)target.idx);
                }
            }
            return x;
        }

        public async Task<ActionStatus> MoveRelAsync(params posCommand[] commands)
        {
            string actionTitle = $"MoveRel_Cmd_{string.Join("_", commands.Select(c => c.idx))}";

            // [핵심] 최종 도달할 절대 위치를 저장할 딕셔너리
            // Key: Motor ID(int), Value: Final Absolute Position
            var absTargets = new Dictionary<MotionID, double>();

            var x = await _act.ExecuteAction(
                title: actionTitle,
                stepNames: new List<string> { "Start", "InterLockCheck", "paramCheck", "Move", "MoveDoneCheck", "End" },
                stepLogic: async (context, stepName) =>
                {
                    int nextStep = context.StepIndex;

                    switch (stepName)
                    {
                        case "Start":
                            {
                                // [1] 이동 전, 최종 목적지(절대좌표)를 미리 계산하여 저장합니다.
                                absTargets.Clear();
                                foreach (var cmd in commands)
                                {
                                    var currentStatus = GetStatus((MotionID)cmd.idx);
                                    // 최종 위치 = 현재 위치 + 이동량
                                    double finalPos = currentStatus.ActualPos + cmd.targetPostition;
                                    absTargets[cmd.idx] = finalPos;
                                }
                                nextStep++;
                            }
                            break;

                        case "InterLockCheck":
                            {
                                foreach (var target in commands)
                                {
                                    if (!CheckServoReady((MotionID)target.idx))
                                    {
                                        _act.PopupAlarm(ErrorList.MOTOR_SERVO_OFF, L("{0}", target.idx));
                                        context.Status = ActionStatus.Error;
                                        return nextStep;
                                    }

                                    // [2] 미리 계산해둔 '절대 위치(finalPos)'와 'Mode 0(Absolute)'으로 검사합니다.
                                    double finalPos = absTargets[target.idx];
                                    string msg = string.Empty;

                                    if (CheckInterlock((MotionID)target.idx, finalPos, 0, ref msg))
                                    {
                                        _act.PopupAlarm(ErrorList.MOTOR_INTERLOCK, L("{0}", msg));
                                        context.Status = ActionStatus.Error;
                                        return nextStep;
                                    }
                                }
                                nextStep++;
                            }
                            break;

                        case "paramCheck":
                            {
                                SetCoefficient(ref commands);
                                nextStep++;
                            }
                            break;

                        case "Move":
                            {
                                // 하드웨어에는 원래대로 '이동량(Relative Distance)'을 전송
                                bool result = _ioHardware.MoveRel(commands);

                                if (result)
                                {
                                    nextStep++;
                                }
                                else
                                {
                                    Common.Logs.Log.Instance.Error($"[MoveRel] Hardware Command Failed.");
                                    context.Status = ActionStatus.Error;
                                }
                            }
                            break;

                        case "MoveDoneCheck":
                            {
                                bool allFinished = true;

                                foreach (var cmd in commands)
                                {
                                    var status = GetStatus((MotionID)cmd.idx);

                                    if (!status.InPos)
                                    {
                                        allFinished = false;
                                        // 여기서 break 하지 않고 아래 인터락 체크를 수행하여 안전성을 높입니다.
                                    }

                                    // [3] 이동 중에도 변하지 않는 '최종 절대 위치'로 인터락을 계속 감시합니다.
                                    double finalPos = absTargets[cmd.idx];
                                    string msg = string.Empty;

                                    if (CheckInterlock((MotionID)cmd.idx, finalPos, 0, ref msg))
                                    {
                                        _act.PopupAlarm(ErrorList.MOTOR_INTERLOCK, L("Moving Rel Interlock: {0}", msg));
                                        context.Status = ActionStatus.Error;
                                        return nextStep; // 즉시 정지
                                    }
                                }

                                if (allFinished)
                                {
                                    nextStep++;
                                }
                                else
                                {
                                    await Task.Delay(10);
                                }
                            }
                            break;

                        case "End":
                            context.Status = ActionStatus.Finished;
                            break;

                        default:
                            context.Status = ActionStatus.Error;
                            break;
                    }
                    return nextStep;
                }
            );

            // [4] 에러 발생 시 안전 정지
            if (x != ActionStatus.Finished)
            {
                foreach (var cmd in commands)
                {
                    MoveStop((MotionID)cmd.idx);
                }
            }

            return x;
        }

        public async Task<ActionStatus> MoveJogStartAsync(MotionID id, bool plusDir , bool speedFast = false)
        {
            string actionTitle = $"MoveJogAsync_Cmd_{id}_dir:{plusDir}";

            posCommand commands = new posCommand();

            int checkMode = plusDir ? 2 : 3;
            string msg = string.Empty;

            var resultStatus = await _act.ExecuteAction(
              title: actionTitle,
              stepNames: new List<string> { "Start", "InterLockCheck", "paramCheck", "Move", "MoveDoneCheck", "End" },
              stepLogic: async (context, stepName) =>
              {
                  int nextStep = context.StepIndex;

                  switch (stepName)
                  {
                      case "Start":
                          nextStep++;
                          break;

                      case "InterLockCheck":
                          {
                              if (!CheckServoReady(id))
                              {
                                  _act.PopupAlarm(ErrorList.MOTOR_SERVO_OFF, L("{0}", id));
                                  context.Status = ActionStatus.Error;
                                  return nextStep;
                              }

                              if (CheckInterlock(id, 0, checkMode, ref msg))
                              {
                                  _act.PopupAlarm(ErrorList.MOTOR_INTERLOCK, L("Jog Start Blocked: {0}", msg));
                                  context.Status = ActionStatus.Error;
                                  return nextStep;
                              }

                              nextStep++;
                          }
                          break;

                      case "paramCheck":  // 가감속등 값 설정
                          {
                              {
                                  var defaultSpeed = _act.Option.MotionSpeed.Get(id);
                                
                                  commands.idx = id;
                                  commands.velocity = speedFast ? defaultSpeed.JogPastSpeed  : defaultSpeed.JogNormalSpeed;
                                  SetCoefficient(ref commands);
                                  nextStep++;
                              }
                            
                          }
                          break;

                      case "Move":
                          {
                              bool result = _ioHardware.JogMoveStart(commands, plusDir);

                              if (result)
                              {
                                  nextStep++;
                              }
                              else
                              {
                                  Common.Logs.Log.Instance.Error($"[MoveAbs] Hardware Command Failed.");
                                  context.Status = ActionStatus.Error;
                              }
                          }
                          break;

                      case "MoveDoneCheck":
                          {
                              if (CheckInterlock(id, 0, checkMode, ref msg))
                              {
                                  _act.PopupAlarm(ErrorList.MOTOR_INTERLOCK, L("Jog Start Blocked: {0}", msg));
                                  context.Status = ActionStatus.Error;
                                  return nextStep;
                              }

                              var status = GetStatus(id);

                              if(status.InPos)
                                nextStep++;
                              else                              
                                  await Task.Delay(10); 
                              
                          }
                          break;

                      case "End":
                          context.Status = ActionStatus.Finished;
                          break;

                      default:
                          context.Status = ActionStatus.Error;
                          break;
                  }
                  return nextStep;
              }
          );

            if (resultStatus != ActionStatus.Finished)
            {
                MoveJogStopAsync(id);
                MoveStop(id);
            }

            return resultStatus;
        }

        public async Task<ActionStatus> MoveJogStopAsync(MotionID id)
        {
            string actionTitle = $"MoveJogStopAsync_Cmd_{id}";

            _ioHardware.JogMoveStop((int)id);

            return ActionStatus.Finished;         
        }

        public async Task<ActionStatus> MoveTrqAsync(MotionID id, double trq, double rpm = 300)
        {
            string actionTitle = $"MoveTrqAsync_Cmd_{id}_{trq}";

            int checkMode = trq >= 0 ? 2 : 3;
            string msg = string.Empty;

            var ret = await _act.ExecuteAction(
              title: actionTitle,
              stepNames: new List<string> { "Start", "InterLockCheck", "paramCheck", "Move", "MoveDoneCheck", "End" },
              stepLogic: async (context, stepName) =>
              {
                  int nextStep = context.StepIndex;

                  switch (stepName)
                  {
                      case "Start":
                          nextStep++;
                          break;

                      case "InterLockCheck":
                          {
                              if (!CheckServoReady(id))
                              {
                                  _act.PopupAlarm(ErrorList.MOTOR_SERVO_OFF, L("{0}", id));
                                  context.Status = ActionStatus.Error;
                                  return nextStep;
                              }

                              if (CheckInterlock(id, 0, checkMode, ref msg))
                              {
                                  _act.PopupAlarm(ErrorList.MOTOR_INTERLOCK, L("Torque Move Blocked: {0}", msg));
                                  context.Status = ActionStatus.Error;
                                  return nextStep;
                              }

                              nextStep++;
                          }
                          break;

                      case "paramCheck":  // 가감속등 값 설정
                          {
                              if (trq > 300 || trq < -300)
                              {
                                  _act.PopupAlarm(ErrorList.MOTOR_TORQUE_OVERRUN, L("{0} trq => {1}", id, trq));
                                  context.Status = ActionStatus.Error;
                              }
                              nextStep++;
                          }
                          break;

                      case "Move":
                          {

                              bool result = _ioHardware.MoveTrq((int)id, trq, rpm);

                              if (result)
                              {
                                  nextStep++;
                              }
                              else
                              {
                                  Common.Logs.Log.Instance.Error($"[MoveAbs] Hardware Command Failed.");
                                  context.Status = ActionStatus.Error;
                              }
                          }
                          break;

                      case "MoveDoneCheck":
                          {
                              if (CheckInterlock(id, 0, checkMode, ref msg))
                              {
                                  _act.PopupAlarm(ErrorList.MOTOR_INTERLOCK, L("Torque Move Blocked: {0}", msg));
                                  context.Status = ActionStatus.Error;
                                  return nextStep;
                              }

                              var status = GetStatus(id);

                              if (status.InPos)
                                  nextStep++;
                              else
                                  await Task.Delay(10);
                          }
                          break;

                      case "End":
                          context.Status = ActionStatus.Finished;
                          break;

                      default:
                          context.Status = ActionStatus.Error;
                          break;
                  }
                  return nextStep;
              }
          );

            if (ret != ActionStatus.Finished)
                _ioHardware.MoveStop((int)id);

            return ret;
        }


        public async Task<ActionStatus> MoveVelAsync(MotionID id, double vel)
        {
            string actionTitle = $"MoveVelAsync_Cmd_{id}_{vel}";

            posCommand commands = new posCommand();

            int checkMode = vel >= 0 ? 2 : 3;
            string msg = string.Empty;

            var ret = await _act.ExecuteAction(
              title: actionTitle,
              stepNames: new List<string> { "Start", "InterLockCheck", "paramCheck", "Move", "MoveDoneCheck", "End" },
              stepLogic: async (context, stepName) =>
              {
                  int nextStep = context.StepIndex;

                  switch (stepName)
                  {
                      case "Start":
                          nextStep++;
                          break;

                      case "InterLockCheck":
                          {
                              if (!CheckServoReady(id))
                              {
                                  _act.PopupAlarm(ErrorList.MOTOR_SERVO_OFF, L("{0}", id));
                                  context.Status = ActionStatus.Error;
                                  return nextStep;
                              }

                              if (CheckInterlock(id, 0, checkMode, ref msg))
                              {
                                  _act.PopupAlarm(ErrorList.MOTOR_INTERLOCK, L("Velocity Move Blocked: {0}", msg));
                                  context.Status = ActionStatus.Error;
                                  return nextStep;
                              }

                              nextStep++;
                          }
                          break;

                      case "paramCheck":  // 가감속등 값 설정
                          {                             
                              commands.idx = id;
                              commands.velocity = vel;
                              SetCoefficient(ref commands);
                              nextStep++;
                          }
                          break;

                      case "Move":
                          {

                              bool result = _ioHardware.MoveVel(commands);

                              if (result)
                              {
                                  nextStep++;
                              }
                              else
                              {
                                  Common.Logs.Log.Instance.Error($"Hardware Command Failed.");
                                  context.Status = ActionStatus.Error;
                              }
                          }
                          break;

                      case "MoveDoneCheck":
                          {
                              nextStep++;
                              /*
                              if (CheckInterlock(id, 0, checkMode, ref msg))
                              {
                                  _act.PopupAlarm(ErrorList.MOTOR_INTERLOCK, L("Velocity Move Blocked: {0}", msg));
                                  context.Status = ActionStatus.Error;
                                  return nextStep;
                              }

                              var status = GetStatus(id);

                              if (status.InPos)
                                  nextStep++;
                              else
                                  await Task.Delay(10);
                              */
                          }
                          break;

                      case "End":
                          context.Status = ActionStatus.Finished;
                          break;

                      default:
                          context.Status = ActionStatus.Error;
                          break;
                  }
                  return nextStep;
              }
          );

            if (ret != ActionStatus.Finished)
                _ioHardware.MoveStop((int)id);

            return ret;
        }

       

        const int coef = 1;

        private void SetCoefficient(ref posCommand[] commands)
        {
            var speedList = ActManager.Instance.Act.Option.MotionSpeed.SpeedList;
            for (int i = 0; i < commands.Length; i++)
            {
                commands[i].velocity =  commands[i].velocity* coef;
                commands[i].acc = commands[i].acc == 0 ? speedList[i].Accel * coef : commands[i].acc * coef;
                commands[i].dec = commands[i].dec == 0 ? speedList[i].Deaccel * coef : commands[i].dec * coef;
                commands[i].JerkRatio = commands[i].JerkRatio == 0 ? speedList[i].JerkRatio : commands[i].JerkRatio ;
            }
        }
        private void SetCoefficient(ref posCommand commands)
        {
            var speedList = ActManager.Instance.Act.Option.MotionSpeed.SpeedList[(int)commands.idx];

            commands.velocity =  commands.velocity * coef;
            commands.acc = commands.acc == 0 ? speedList.Accel * coef : commands.acc * coef;
            commands.dec = commands.dec == 0 ? speedList.Deaccel * coef : commands.dec * coef;
            commands.JerkRatio = commands.JerkRatio == 0 ? speedList.JerkRatio : commands.JerkRatio;

        }

        public async Task<ActionStatus> SetTorqueAsync(MotionID id, double torque , double pTorque = 0 , double nTorque = 0)
        {
            string actionTitle = $"SetTorqueAsync_Cmd_{id}_{torque}";

            posCommand commands = new posCommand();

          
            string msg = string.Empty;

            var ret = await _act.ExecuteAction(
              title: actionTitle,
              stepNames: new List<string> { "Start", "InterLockCheck", "paramCheck", "SetTrq", "GetTrq", "End" },
              stepLogic: async (context, stepName) =>
              {
                  int nextStep = context.StepIndex;

                  switch (stepName)
                  {
                      case "Start":
                          
                          nextStep++;
                          break;

                      case "InterLockCheck":
                          {
                              if (!CheckServoReady(id))
                              {
                                  _act.PopupAlarm(ErrorList.MOTOR_SERVO_OFF, L("{0}", id));
                                  context.Status = ActionStatus.Error;
                                  return nextStep;
                              }                             

                              nextStep++;
                          }
                          break;

                      case "paramCheck":  
                          {                            
                              nextStep++;
                          }
                          break;

                      case "SetTrq":
                          {
                              if (pTorque == 0 ) pTorque = torque;
                              if (nTorque == 0 ) nTorque = torque;

                              bool result = _ioHardware.SetTrq((int)id , torque, pTorque, nTorque);

                              nextStep++;
                          }
                          break;

                      case "GetTrq":
                          {
                              var result = _ioHardware.GetTrq((int)id);

                              if(torque == result.Item1 && pTorque == result.Item2 && nTorque == result.Item3)
                                  nextStep++;
                              else
                              {
                                  Common.Logs.Log.Instance.Error($"[SetTrq] Hardware Command Failed. Set:{torque}/{pTorque}/{nTorque}  Get:{result.Item1}/{result.Item2}/{result.Item3}");
                                  context.Status = ActionStatus.Error;
                              }
                          }
                          break;

                      case "End":
                          context.Status = ActionStatus.Finished;
                          break;

                      default:
                          context.Status = ActionStatus.Error;
                          break;
                  }
                  return nextStep;
              }
          );          

            return ret;
        }


        public async Task<ActionStatus> HomeSearchAsync(MotionID motionID)
        {
            return await _act.ExecuteAction(
                title: $"Motion_HomeSearch:[{motionID}]",
                stepNames: new List<string> { "Start", "CheckMotor", "Homing", "DoneCheck", "End" },
                stepLogic: async (context, stepName) =>
                {
                    int nextStep = context.StepIndex;

                    switch (stepName)
                    {
                        case "Start":
                            {
                                _act.Motion.AlarmReset(motionID);
                                await Task.Delay(100);
                                _act.Motion.ServoOn(motionID, true);
                                await Task.Delay(100);

                                nextStep++;
                            }
                            break;
                        case "CheckMotor":
                            {
                                var s = GetStatus(motionID);

                                if (s.AmpAlarm)
                                {
                                    _act.PopupAlarm(ErrorList.MOTOR_ALARM_ON, L("{0}", motionID));
                                    context.Status = ActionStatus.Error;
                                }
                                if (!s.ServoOn)
                                {
                                    _act.PopupAlarm(ErrorList.MOTOR_SERVO_OFF, L("{0}", motionID));
                                    context.Status = ActionStatus.Error;
                                }

                                nextStep++;
                            }

                            break;
                        case "Homing":
                            _act.Motion.Home(motionID);
                            await Task.Delay(500);
                            nextStep++;
                            break;

                        case "DoneCheck":
                            {
                                var s = GetStatus(motionID);

                                if (s.HomeDone)
                                    nextStep++;

                                if (s.OP == "IDLE") // 외부에서 STOP 시켰음
                                    nextStep++;
                            }
                            break;

                        case "End":
                            context.Status = ActionStatus.Finished;
                            break;
                    }
                    return nextStep;
                }
            );
        }

        #region EtherCAT SDO/PDO
        /// <summary>
        /// SDO Write (Service Data Object)
        /// </summary>
        public bool SDO_Write(int slaveId, int sdoIndex, int sdoSubIndex, int writeData)
        {
            return _ioHardware?.SDO_Write(slaveId, sdoIndex, sdoSubIndex, writeData) ?? false;
        }

        /// <summary>
        /// SDO Read (Service Data Object)
        /// </summary>
        public byte[] SDO_Read(int slaveId, int sdoIndex, int sdoSubIndex)
        {
            return _ioHardware?.SDO_Read(slaveId, sdoIndex, sdoSubIndex) ?? Array.Empty<byte>();
        }

        /// <summary>
        /// PDO Read (Process Data Object)
        /// </summary>
        public byte[] PDO_Read(int masterId, int slaveId, int pdoIndex, int pdoSubIndex)
        {
            return _ioHardware?.PDO_Read(masterId, slaveId, pdoIndex, pdoSubIndex) ?? Array.Empty<byte>();
        }

        /// <summary>
        /// PDO Write (Process Data Object)
        /// </summary>
        public byte[] PDO_Write(int masterId, int slaveId, int pdoIndex, int pdoSubIndex, int writeData)
        {
            return _ioHardware?.PDO_Write(masterId, slaveId, pdoIndex, pdoSubIndex, writeData) ?? Array.Empty<byte>();
        }
        #endregion
    }
}