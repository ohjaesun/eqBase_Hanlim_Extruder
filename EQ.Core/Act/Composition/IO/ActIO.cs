using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Enums;
using EQ.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EQ.Core.Globals;

namespace EQ.Core.Act
{
    public class ActIO : ActComponent
    {
        private IIoController _ioHardware;

        public ActIO(ACT act) : base(act) { }

        private Dictionary<string, int> OutputNameToIndex = new Dictionary<string, int>();
        private Dictionary<string, int> InputNameToIndex = new Dictionary<string, int>();

        private Dictionary<string, int> AnalogInputNameToIndex = new Dictionary<string, int>();
        private Dictionary<string, int> AnalogOutputNameToIndex = new Dictionary<string, int>();
        /// <summary>
        /// 실제 HW 연결
        /// </summary>
        /// <param name="controller"></param>
        public void SetHardwareController(IIoController controller)
        {
            this._ioHardware = controller;

            _ioHardware.Init("");

            foreach (IO_OUT output in Enum.GetValues(typeof(IO_OUT)))
            {
                OutputNameToIndex.Add(output.ToString(), (int)output);
            }
            foreach (IO_IN input in Enum.GetValues(typeof(IO_IN)))
            {
                InputNameToIndex.Add(input.ToString(), (int)input);
            }
            foreach (IO_AIN input in Enum.GetValues(typeof(IO_AIN)))
            {
                AnalogInputNameToIndex.Add(input.ToString(), (int)input);
            }
            foreach (IO_AOUT output in Enum.GetValues(typeof(IO_AOUT)))
            {
                AnalogOutputNameToIndex.Add(output.ToString(), (int)output);
            }
        }

        /// <summary>
        /// 연결된 실제 HW 컨트롤러 인스턴스를 반환합니다.
        /// </summary>
        /// <returns>IIoController 인스턴스</returns>
        public IIoController GetHardwareController()
        {
            return this._ioHardware;
        }

        /// <summary>
        /// IO 상태 읽기
        /// </summary>
        /// <returns></returns>
        public (byte[] _input , byte[] _output) GetIoStatus()
        {
            if (this._ioHardware == null)
                return (null , null);

            return _ioHardware.GetCachedData();
        }

        /// <summary>
        /// Input 읽기
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ReadInput(IO_IN name)
        {
            return _ioHardware.ReadInput((int)name);
        }

        /// <summary>
        /// Output 읽기
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ReadOutput(IO_OUT name)
        {
            return _ioHardware.ReadOutput((int)name);
        }

        /// <summary>
        /// Input 쓰기 , 시뮬레이션 용
        /// </summary>
        /// <param name="name"></param>
        /// <param name="On"></param>
        public void WriteInput(IO_IN name, bool On)
        {
            if (_ioHardware == null) return;
            _ioHardware.WriteInput((int)name, On ? (byte)1 : (byte)0);
        }
        /// <summary>
        /// output 쓰기
        /// </summary>
        /// <param name="name"></param>
        /// <param name="On"></param>
        public void WriteOutput(IO_OUT name, bool On)
        {
            if (_ioHardware == null) return;

            _ioHardware.WriteOutput((int)name, On ? (byte)1 : (byte)0);

            //시뮬레이션용
            if (InputNameToIndex.TryGetValue(name.ToString(), out int index1))
            {
                _ioHardware.WriteInput(index1, On ? (byte)1 : (byte)0);
            }          
        }        
        
        /// <summary>
        /// Analog Input 읽기
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public double ReadAnalog(IO_AIN name)
        {
            return _ioHardware?.ReadAnalogInput((int)name) ?? 0.0;
        }

        /// <summary>
        /// Analog Output 쓰기
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void WriteAnalog(IO_AOUT name, double value)
        {
            _ioHardware?.WriteAnalogOutput((int)name, value);
        }
       

        /// <summary>
        /// 복동형의 신호 제어
        /// </summary>
        /// <param name="name">On 하려는 IO</param>
        /// <param name="checkInputIO">출력 신호를 볼 경우</param>
        /// <returns></returns>
        public async Task<ActionStatus> doubleTypeOnAsync(IO_OUT name , bool checkInputIO = true)
        {
            var stepString = new List<string> { "Start", "FindSuffix", "IOChange", "WaitResult", "End" };

            IO_OUT onIO  = name;
            IO_OUT? offIO = null;

            return await _act.ExecuteAction(
                title: $"doubleTypeOn:[{name}]",
                stepNames: stepString,
                stepLogic: async (context, stepName) =>
                {
                    int nextStep = context.StepIndex;

                    switch (stepName)
                    {
                        case "Start":
                            if (_ioHardware == null)
                            {
                                ActManager.Instance.Act.PopupAlarm(ErrorList.IO_INIT_FAIL, L("IO 초기화 안됨)"));
                                context.Status = ActionStatus.Error;
                            }
                            nextStep++;
                            break;

                        case "FindSuffix":
                            {
                                string input = name.ToString();
                                int lastIndex = input.LastIndexOf('_');
                                if (lastIndex == -1 || lastIndex == input.Length - 1)
                                {
                                    nextStep++;
                                }

                                string prefix = input.Substring(0, lastIndex);                               
                                string suffix = input.Substring(lastIndex);
                               
                                string newSuffix = suffix switch
                                {
                                    "_ON" => "_OFF",
                                    "_OFF" => "_ON",
                                    "_PUSH" => "_BACK",
                                    "_BACK" => "_PUSH",
                                    _ => "None" 
                                };

                                if( newSuffix == "None")
                                {
                                    nextStep++;
                                }
                                else
                                {
                                    string offIoName = prefix + newSuffix;
                                    if (OutputNameToIndex.TryGetValue(offIoName, out int index))                                                                        
                                        offIO = (IO_OUT)index;                                    
                                                                       
                                    nextStep++;
                                }
                            }
                            break;

                        case "IOChange":
                            {
                                WriteOutput(onIO, true);
                                if(offIO != null)
                                    WriteOutput((IO_OUT)offIO, false);

                                nextStep++;
                            }
                            break;

                            case "WaitResult":
                            {
                                if (checkInputIO)
                                {
                                    bool isOn = true;

                                    IO_IN? checkInputOn = null;
                                    IO_IN? checkInputOff = null;

                                    if (InputNameToIndex.TryGetValue(onIO.ToString(), out int index1))
                                    {
                                        checkInputOn = (IO_IN)index1;
                                        isOn = ReadInput((IO_IN)checkInputOn);
                                    }                                        

                                    if (InputNameToIndex.TryGetValue(offIO.ToString(), out int index2))
                                    {
                                        checkInputOff = (IO_IN)index2;
                                        isOn = !ReadInput((IO_IN)checkInputOff);
                                    }                                     

                                    if (isOn)                                    
                                        nextStep++;                                    
                                    else                                                                          
                                        await Task.Delay(10);                                    
                                }
                                else
                                {
                                    nextStep++;
                                }
                            }
                            break;

                        case "End":                          
                            context.Status = ActionStatus.Finished;
                            break;

                        default: //정의 되지 않은 step
                            context.Status = ActionStatus.Error;
                            break;
                    }
                    return nextStep;
                }
            );
        }



        /// <summary>
        /// 자기 유지 타입 신호 제어 , OFF쪽이 트리거 신호임
        /// </summary>
        /// <param name="name">이름에 _ON 이 있는것만 넣습니다.</param>
        /// <param name="checkInputIO"></param>
        /// <returns></returns>
        public async Task<ActionStatus> selfHoldingAsync(IO_OUT name, IO_SelfType type , bool checkInputIO = true)
        {
            var stepString = new List<string> { "Start", "FindSuffix", "IOChange", "WaitResult", "End" };

            IO_OUT onIO = name;
            IO_OUT? offIO = null;

            return await _act.ExecuteAction(
                title: $"selfTypeOn:[{name}]",
                stepNames: stepString,
                stepLogic: async (context, stepName) =>
                {
                    int nextStep = context.StepIndex;

                    switch (stepName)
                    {
                        case "Start":
                            if (_ioHardware == null)
                            {
                                ActManager.Instance.Act.PopupAlarm(ErrorList.IO_INIT_FAIL, L("IO 초기화 안됨)"));
                                context.Status = ActionStatus.Error;
                            }
                            nextStep++;
                            break;

                        case "FindSuffix":
                            {
                                string input = name.ToString();
                                int lastIndex = input.LastIndexOf('_');
                                if (lastIndex == -1 || lastIndex == input.Length - 1)
                                {
                                    nextStep++;
                                }

                                string prefix = input.Substring(0, lastIndex);
                                string suffix = input.Substring(lastIndex);

                                string newSuffix = suffix switch
                                {
                                    "_ON" => "_OFF",                                 
                                    _ => "None"
                                };

                                if (newSuffix == "None")
                                {
                                    ActManager.Instance.Act.PopupAlarm(ErrorList.IO_INIT_FAIL, L("Off IO 못찾음"));
                                    context.Status = ActionStatus.Error;
                                    nextStep++;
                                }                            
                                else
                                {
                                    string offIoName = prefix + newSuffix;
                                    if (OutputNameToIndex.TryGetValue(offIoName, out int index))
                                        offIO = (IO_OUT)index;

                                    nextStep++;
                                }
                            }
                            break;

                        case "IOChange":
                            {
                                if(type == IO_SelfType.Stop )
                                {
                                    WriteOutput(onIO, false);
                                    WriteOutput((IO_OUT)offIO, true);
                                    await Task.Delay(50);
                                    WriteOutput((IO_OUT)offIO, false);
                                }
                                else if (type == IO_SelfType.Vacuum)
                                {
                                    WriteOutput(onIO, true);
                                    WriteOutput((IO_OUT)offIO, false);                                  
                                }
                                else if(type == IO_SelfType.Blow)
                                {
                                    WriteOutput(onIO, false);
                                    WriteOutput((IO_OUT)offIO, true);
                                }
                                else
                                {
                                    ActManager.Instance.Act.PopupAlarm(ErrorList.IO_INIT_FAIL, L("Off IO 못찾음"));
                                    context.Status = ActionStatus.Error;
                                    nextStep++;
                                }                               

                                nextStep++;
                            }
                            break;

                        case "WaitResult":
                            {
                                if (checkInputIO)
                                {
                                    bool isOn = true;

                                    IO_IN? checkInputOn = null;
                                    IO_IN? checkInputOff = null;

                                    if (InputNameToIndex.TryGetValue(onIO.ToString(), out int index1))
                                    {
                                        checkInputOn = (IO_IN)index1;
                                        isOn = ReadInput((IO_IN)checkInputOn);

                                        if (type == IO_SelfType.Stop)
                                        {
                                            isOn = isOn == false ? true : false;
                                        }
                                        else if (type == IO_SelfType.Vacuum)
                                        {
                                            isOn = isOn == true ? true : false;
                                        }
                                        else if (type == IO_SelfType.Blow)
                                        {
                                            isOn = isOn == false ? true : false;
                                        }
                                    }

                                    if (InputNameToIndex.TryGetValue(offIO.ToString(), out int index2))
                                    {
                                        checkInputOff = (IO_IN)index2;
                                        isOn = !ReadInput((IO_IN)checkInputOff);

                                        if (type == IO_SelfType.Stop)
                                        {
                                            isOn = isOn == false ? true : false;
                                        }
                                        else if (type == IO_SelfType.Vacuum)
                                        {
                                            isOn = isOn == false ? true : false;
                                        }
                                        else if (type == IO_SelfType.Blow)
                                        {
                                            isOn = isOn == true ? true : false;
                                        }
                                    }

                                    if (isOn)
                                        nextStep++;
                                    else
                                        await Task.Delay(10);
                                }
                                else
                                {
                                    nextStep++;
                                }
                            }
                            break;

                        case "End":
                            context.Status = ActionStatus.Finished;
                            break;

                        default: //정의 되지 않은 step
                            context.Status = ActionStatus.Error;
                            break;
                    }
                    return nextStep;
                }
            );
        }
    }
}
