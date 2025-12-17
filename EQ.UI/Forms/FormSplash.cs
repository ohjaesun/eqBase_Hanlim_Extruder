using EQ.Common.Helper;
using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Entities.Extruder;

using EQ.Domain.Entities.LaserMeasure;
using EQ.Domain.Entities.SecsGem;
using EQ.Domain.Enums;
using EQ.Domain.Enums.LaserMeasure;
using EQ.Domain.Interface;
using EQ.Infra.Device;

using EQ.Infra.HW.IO;
using EQ.Infra.LaserMeasure;
using EQ.Infra.Mock;
using EQ.Infra.Network.Modbus;
using EQ.Infra.SecsGem;
using EQ.Infra.Serial;
using EQ.Infra.Storage;
using EQ.UI.Forms;
using EQ.UI.Services;
using Modbus.Device;
using SQLitePCL;
using System;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO.Ports;
using Tcp;

using static EQ.Infra.HW.IO.HardwareIOFactory;
using static EQ.Infra.HW.Motion.HardwareMotionFactory;
using static System.Net.Mime.MediaTypeNames;
using Application = System.Windows.Forms.Application;

namespace EQ.UI
{
    public partial class FormSplash : FormBase
    {
        private bool isSimulation = false;
        private static readonly Random rand = new Random();

        private int _autoCloseCountdown = 30;
        Stopwatch sw = new Stopwatch();

        public FormSplash()
        {
            InitializeComponent();
        }

        private void FormSplash_Shown(object sender, EventArgs e)
        {
            StartProgram();
        }

        private void updateLable(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action(() => richTextBox1.AppendText(text)));

                lblStatus.Invoke(new Action(() => lblStatus.Text = sw.ElapsedMilliseconds.ToString()));
            }
            else
            {
                richTextBox1.AppendText(text);
                lblStatus.Text = sw.ElapsedMilliseconds.ToString();
            }
        }

        enum LoadStep
        {
            Recipe,
            LoadUserOption,
            LoadLang,
            LoadProduct,
            LoadPasswords,           
            LoadGVision,
            LoadModbus,
            LoadTemp,
            LoadSerial,
            LoadLaserMeasure,
            InitAlarmStorage,
            InitHardware_IO,
            InitHardware_PIO, // IO 초기화 이후로 순서 변경
            InitHardware_Motion,
            InitHardware_SecsGem,

            Init_EQ_Exturder,
            Complete
        }

        public async void StartProgram()
        {
            Log.Instance.Info("프로그램 시작");

            _ButtonStart.Visible = false;

            CIni ini = new CIni();
            isSimulation = ini.ReadBool("SYSTEM", "Simulation", false);
            ini.WriteBool("SYSTEM", "Simulation", isSimulation);

            var act = ActManager.Instance.Act;
            // 1. (신규) 단방향 알람은 'HandleCoreNotification' 메서드로 연결
            act.OnNotificationRequest += HandleCoreNotification;

            // 2. (신규) 양방향(Yes/No) 확인 서비스 주입
            act.RegisterConfirmationService(new UIConfirmationService());

            act.OnAlarmRequest += HandleCoreAlarm;

            sw.Start();

            await Task.Run(async () =>
            {
                try
                {
                    LoadStep step = LoadStep.Recipe;
                    Stopwatch sw = new Stopwatch();
                    int _length = Enum.GetNames(typeof(LoadStep)).Length;
                    while (true)
                    {
                        sw.Restart();
                        updateLable($"[{(int)step + 1}/{_length}]\t{step} 로드 중...");

                        switch (step)
                        {
                            case LoadStep.Recipe:

                                Batteries.Init();
                                act.Recipe.Initialize();

                                break;

                            case LoadStep.LoadUserOption:
                                {
                                    //UserOption
                                    RegisterOption<UserOption1>(act);
                                    RegisterOption<UserOption2>(act);
                                    RegisterOption<UserOption3>(act);
                                    RegisterOption<UserOption4>(act);
                                    RegisterOption<List<UserOptionUI>>(act);
                                //    RegisterOption<List<Extuder_Recipe>>(act);

                                    //Motion
                                    RegisterOption<UserOptionMotionSpeed>(act);
                                    RegisterOption<UserOptionMotionPos>(act);
                                    RegisterOption<UserOptionMotionInterlock>(act);

                                    act.Option.LoadAllOptionsFromStorage();

                                    // ExtruderRecipe Storage 등록 및 로드
                                    RegisterExtruderRecipeStorage(act);
                                }
                                break;

                            case LoadStep.LoadLang:
                                {
                                    act.Language.ChangeLanguage();
                                }
                                break;

                            case LoadStep.LoadProduct:
                                {
                                    break;
                                    //장비의 형태에 따라 (웨이퍼 , 트레이 , 매거진 등 ) 로딩
                                 //   InitProduct(act);                                
                                                                
                                }
                                break;

                            

                            case LoadStep.LoadPasswords:
                                {
                                    act.User.LoadPasswords();
                                }
                                break;

                            case LoadStep.LoadGVision:
                                {
                                    break;
                                    InitVision(act);
                                }
                                break;

                            case LoadStep.LoadModbus:
                                {
                                    break;
                                    InitModbus(act);
                                }
                                break;

                            case LoadStep.LoadTemp: // 온도계 등록 
                                {
                                    InitTemp(act);                                  
                                }
                                break;

                            case LoadStep.LoadSerial:
                                {
                                    InitSerial(act);
                                }
                                break;

                            case LoadStep.LoadLaserMeasure:
                                {
                                    break;
                                    InitLaserMeasure(act);
                                }
                                break;

                            case LoadStep.InitAlarmStorage:
                                {
                                    // 알람은 JSON 백업이 필요 없으므로 SqliteStorage만 단독 사용
                                    IDataStorage<AlarmData> alarmStorage = new SqliteStorage<AlarmData>();
                                    act.AlarmDB.RegisterStorageService(alarmStorage);
                                    break;
                                }

                            case LoadStep.InitHardware_IO:
                                {
                                    string currentHardwareIoType = isSimulation ? "Simulation" : "WMX";

                                    // EQ.Infra의 팩토리를 호출하여 "실제" 하드웨어 인스턴스 생성
                                    IIoController mainIoController = IoFactory.CreateIoController(currentHardwareIoType);
                                    act.IO.SetHardwareController(mainIoController);
                                }
                                break;

                            case LoadStep.InitHardware_PIO:
                                {
                                    break;
                                    IIoController mainIoController = act.IO.GetHardwareController();
                                    if (mainIoController != null)
                                    {
                                        //각 PIO의 IO 주소 지정
                                        IO_IN[] _input = new IO_IN[] { (IO_IN)0, (IO_IN)8 };
                                        IO_OUT[] _output = new IO_OUT[] { (IO_OUT)0, (IO_OUT)8 };

                                        int idx = 0;
                                        foreach (PIOId id in Enum.GetValues(typeof(PIOId)))
                                        {
                                            IPIOHandover pioHandover = new PIOHandoverController(mainIoController);
                                            act.PIO.RegisterClient(id, pioHandover , _input[idx] , _output[idx]);
                                            idx++;
                                        }
                                    }
                                    else
                                    {
                                        Log.Instance.Error("PIO_Init", "Main IO Controller is not available from ActIO. PIO will not work.");
                                    }
                                }
                                break;

                            case LoadStep.InitHardware_Motion:
                                {
                                    string currentHardwareMotionType = isSimulation ? "Simulation" : "WMX";

                                    var act = ActManager.Instance.Act;

                                    IMotionController mainMotionController = MotionFactory.CreateIoController(currentHardwareMotionType);
                                    act.Motion.SetHardwareController(mainMotionController);
                                }

                                break;

                            case LoadStep.InitHardware_SecsGem:
                                {
                                    break;
                                    //EzGem DLL이 스레드 안에서 start를 시키면 동작 하지 않는다. COM 기반으로 구현 된 것 같음.
                                    //UI 스레드에서 실행 시키면 정상 동작 함

                                    this.BeginInvoke(new Action(() =>
                                    {
                                        InitSecsGem(act);
                                    }));                                  
                                }
                                break;


                            case LoadStep.Init_EQ_Exturder:
                                {
                                    act.Extuder.Init();
                                }
                                break;
                        }
                        updateLable($"  Done  Elsp:{sw.ElapsedMilliseconds}ms \n");
                        step++;
                        // enum 길이보다 크면 종료
                        if ((int)step >= Enum.GetNames(typeof(LoadStep)).Length)
                            break;
                    }                
                }
                catch (Exception ex)
                {
                    Log.Instance.Error(ex.ToString());
                    MessageBox.Show("프로그램 실행 중 오류가 발생하여 프로그램을 종료합니다.\n" + ex.Message, "프로그램 실행 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit(); // 프로그램 종료
                }
            });          


            sw.Stop(); 
            ActivateStartButton();         
            
        }

        private void InitLaserMeasure(ACT act)
        {
            CIni ini = new CIni();
            isSimulation = ini.ReadBool("SYSTEM", "Simulation", false);

            if(isSimulation)
            {
                Random r = new Random();
                int idx = 0;
                double min = 10.5;
                double max = 20.5;

                foreach (var p in Enum.GetValues(typeof(LaserMeasureId)))
                {
                    double BaseValue = min + (rand.NextDouble() * (max - min));

                    var config = new LaserMeasureConfig { Name = $"MockSensor_{idx}" };
                    var mockDriver = new MockLaserMeasureDriver();
                    mockDriver.BaseValue = BaseValue;//  15.0;    // 기본 값 15mm
                    mockDriver.NoiseRange = 0.1;    // ±0.1mm 노이즈
                    ActManager.Instance.Act.LaserMeasure.RegisterDriver(LaserMeasureId.Laser1+idx, mockDriver, config);
                    idx++;
                }              
            }
            else
            {

                //여러개 붙이면 위에 시뮬레이션 보고 구조 바꾸자.

                string _vendor = "";

                switch (_vendor)
                {
                    case "HL_G1":  // 파나소닉 RS 485
                        {
                            var config = new LaserMeasureConfig
                            {
                                Id = LaserMeasureId.Laser1,
                                Type = LaserMeasureType.HL_G1,
                                Name = "HL_G1_Sensor",
                                PortName = "COM1",
                                BaudRate = 9600
                            };
                            var driver = new HL_G1Driver();
                            ActManager.Instance.Act.LaserMeasure.RegisterDriver(LaserMeasureId.Laser1, driver, config);
                        }
                        break;
                    case "ZW7000": // 오므론 TCP
                        {
                            var config = new LaserMeasureConfig
                            {
                                Id = LaserMeasureId.Laser1,
                                Type = LaserMeasureType.ZW7000,
                                Name = "ZW7000_Sensor",
                                IpAddress = "127.0.0.1",
                                Port = 8080
                            };
                            var driver = new ZW7000Driver();
                            ActManager.Instance.Act.LaserMeasure.RegisterDriver(LaserMeasureId.Laser1, driver, config);
                        }
                        break;                    
                }
            }
        }

        private void InitSecsGem(ACT act)
        {
            string gemLib = "EZGEM";
            switch (gemLib)
            {
                case "EZGEM":  // 엔비아소프트
                    {
                        var driver = new EZGemPlusDriver();
                        var config = new SecsGemConfig { ModelName = "MyEquipment" };
                        act.SecsGem.Initialize(driver, config);

                        act.SecsGem.LoadDefinitionsFromFile(Application.StartupPath);
                        act.SecsGem.Start();
                    }
                    break;
                case "XCOM": // 링크 제네시스
                    {

                    }
                    break;
            }
        }

      

        private void InitTemp(ACT act)
        {
            // ActManager나 Config에서 시뮬레이션 여부를 확인한다고 가정
            // bool isSimulation = ...; (FormSplash에 이미 변수가 있음)

            try
            {
                if (isSimulation)
                {                   
                    Log.Instance.Info("[Simulation] 온도 컨트롤러 가상(Mock) 모드로 초기화합니다.");
               
                    var mockZone1 = new MockTempController("Zone1", initialTemp: 24.5);
                    var mockZone2 = new MockTempController("Zone2", initialTemp: 25.0);
                    var mockChamber = new MockTempController("Chamber_A", initialTemp: 60.0); // 이미 예열된 상태 가정

                
                    act.Temp.Register(TempID.Zone1, mockZone1);
                    act.Temp.Register(TempID.Zone2, mockZone2);
                    act.Temp.Register(TempID.BathCirculator, mockChamber);

                    Log.Instance.Info("온도 컨트롤러(MOCK) 등록 완료");
                }
                else
                {

                    // 1. 시리얼 포트 설정 및 오픈
                    var comPort = act.Option.Option4.Temperature_COMPort;
                    SerialPort port = new SerialPort(comPort, 9600, Parity.None, 8, StopBits.One);
                    port.Open();

                    // 2. NModbus RTU Master 생성
                    var modbusMaster = ModbusSerialMaster.CreateRtu(port);
                    modbusMaster.Transport.ReadTimeout = 300;
                    modbusMaster.Transport.WriteTimeout = 300;

                    if(!port.IsOpen)
                    {
                        updateLable($"[ModbusRTU] Port Open failed : {comPort}\n");
                        Log.Instance.Error($"[ModbusRTU] Port Open failed : {comPort}");
                    }                        

                    
                    var vx = new VX4_Controller(modbusMaster, slaveId: 1);
                    act.Temp.Register(TempID.Zone1, vx);

                    var vx2 = new VX4_Controller(modbusMaster, slaveId: 1);
                    act.Temp.Register(TempID.Zone2, vx2);


                    //칠러 등록
                    // 1. 시리얼 포트 설정 및 오픈
                    var comPort2 = act.Option.Option4.Temperature_COMPort;
                    SerialPort port2 = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
                    port2.Open();

                    // 2. NModbus RTU Master 생성
                    var modbusMaster2 = ModbusSerialMaster.CreateRtu(port2);
                    modbusMaster2.Transport.ReadTimeout = 300;
                    modbusMaster2.Transport.WriteTimeout = 300;

                    var rw3Driver = new JeioTechRW3Driver(modbusMaster2, slaveId: 1);
                    act.Temp.Register(TempID.BathCirculator, rw3Driver);

                    Log.Instance.Info("온도 컨트롤러(REAL) 등록 완료");
                }
                act.Temp.StartPolling(1000); // 1초 간격 폴링 시작
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"온도 컨트롤러 초기화 실패: {ex.Message}");
            }
        }

        public async void EndProgram()
        {
            await Task.Run(async () =>
            {
                try
                {
                    var act = ActManager.Instance.Act;

                    LoadStep step = LoadStep.Recipe;
                    Stopwatch sw = new Stopwatch();
                    int _length = Enum.GetNames(typeof(LoadStep)).Length;
                    while (true)
                    {
                        sw.Restart();
                        updateLable($"[{(int)step + 1}/{_length}]\t{step} 로드 중...");

                        switch (step)
                        {
                            case LoadStep.Recipe:                            

                                break;

                            case LoadStep.LoadUserOption:
                                {
                                    
                                }
                                break;


                            case LoadStep.LoadProduct:
                                {
                                   
                                }
                                break;



                            case LoadStep.LoadPasswords:
                                {
                                  
                                }
                                break;

                            case LoadStep.LoadGVision:
                                {
                                  
                                }
                                break;

                            case LoadStep.LoadModbus:
                                {
                                  
                                }
                                break;

                            case LoadStep.LoadTemp: 
                                {
                                   
                                }
                                break;

                            case LoadStep.LoadSerial:
                                {
                                   
                                }
                                break;

                            case LoadStep.InitAlarmStorage:
                                {
                                
                                }
                                break;
                            case LoadStep.InitHardware_IO:
                                {                                  


                                }
                                break;



                            case LoadStep.InitHardware_Motion:
                                {
                                 
                                }

                                break;
                        }
                        updateLable($"  Done  Elsp:{sw.ElapsedMilliseconds}ms \n");
                        step++;
                        // enum 길이보다 크면 종료
                        if ((int)step >= Enum.GetNames(typeof(LoadStep)).Length)
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error(ex.ToString());
                    MessageBox.Show("프로그램 실행 중 오류가 발생하여 프로그램을 종료합니다.\n" + ex.Message, "프로그램 실행 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit(); // 프로그램 종료
                }
            });
            ActManager.Instance.Act.OnNotificationRequest -= HandleCoreNotification;
            Log.Instance.Info("프로그램 종료");
        }

        private void HandleCoreAlarm(object sender, AlarmEventArgs e)
        {
            // UI 스레드 확인
            if (this.InvokeRequired)
            {
                this.Invoke(new EventHandler<AlarmEventArgs>(HandleCoreAlarm), sender, e);
                return;
            }

            // 1. [추가] 이미 열려있는 알람 팝업이 있는지 확인 (중복 방지)
            // Application.OpenForms에서 FormAlarmPopup 타입의 폼이 하나라도 있으면 무시
            if (Application.OpenForms.OfType<FormAlarmPopup>().Any())
            {
                return;
            }

            // 1. 팝업 그룹 관리용 리스트
            var popupList = new List<FormAlarmPopup>();

            // 2. 중복 처리 방지 플래그 (한 쪽에서 누르면 나머지는 무시/닫기만 수행)
            bool isHandled = false;

            foreach (Screen screen in Screen.AllScreens)
            {
                // 폼 생성
                var popup = new FormAlarmPopup(e.Error.ToString(), e.Message);

                // 위치 설정 (각 모니터 중앙)
                popup.StartPosition = FormStartPosition.Manual;
                Rectangle bounds = screen.WorkingArea;
                popup.Left = bounds.Left + (bounds.Width - popup.Width) / 2;
                popup.Top = bounds.Top + (bounds.Height - popup.Height) / 2;

                // 3. 폼 종료 이벤트 구독 (여기서 결과 처리)
                popup.FormClosed += async (s, args) =>
                {
                    // 이미 다른 팝업에 의해 처리가 시작되었으면 무시
                    if (isHandled) return;
                    isHandled = true;

                    // (A) Reset 버튼을 눌렀을 경우 -> 시스템 리셋 실행
                    if (popup.DialogResult == DialogResult.Retry)
                    {
                        // 폼들은 이미 닫히는 중이므로 비동기로 리셋 로직 수행
                        await ActManager.Instance.Act.SystemResetAsync();
                    }

                    // (B) 닫기(Cancel) 또는 Reset(Retry) 공통 -> 나머지 팝업 모두 닫기
                    foreach (var otherPopup in popupList)
                    {
                        // 자기 자신이 아니고, 아직 열려 있다면 닫기
                        if (otherPopup != popup && otherPopup.Visible)
                        {
                            otherPopup.Close();
                        }
                    }
                };

                popupList.Add(popup);

                // 4. 비모달(Non-Modal)로 표시 -> 코드 실행이 멈추지 않고 다음 모니터로 넘어감
                popup.Show();
            }
        }

        private void ActivateStartButton()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(ActivateStartButton));
                return;
            }

            _ButtonStart.Visible = true;
            _ButtonStart.Enabled = true;
            _ButtonStart.Text = $"Start System ({_autoCloseCountdown}s)";
            _ButtonStart.ThemeStyle = EQ.UI.Controls.ThemeStyle.Success_Green; // 완료 시 녹색으로 변경

            _timerAutoClose.Start();
        }

        /// <summary>
        /// [Helper] 저장소 생성, 등록, 백업 정리 자동화 함수
        /// T에 원하는 옵션 클래스(예: UserOptionMotionPos)만 넣으면 됩니다.
        /// </summary>
        private void RegisterOption<T>(ACT act) where T : class, new()
        {
            // 1. DualStorage 생성
            var storage = new DualStorage<T>(
                new JsonFileStorage<T>(),
                new SqliteStorage<T>()
            );

            // 2. ActUserOption에 등록
            act.Option.RegisterStorageService(storage);

            // 3. Old DB 백업 자동 삭제 (60일 경과)
            string currentRecipePath = act.Recipe.GetCurrentRecipePath();

            // (ActUserOption의 GetStorageKey 로직과 동일하게 키 생성)
            string key = typeof(T).Name;
            if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(List<>))
            {
                key = $"List_{typeof(T).GetGenericArguments()[0].Name}";
            }

            // 삭제 실행
            new SqliteStorage<T>().DeleteOldBackups(currentRecipePath, key);
        }

        /// <summary>
        /// ExtruderRecipe용 DualStorage 등록 및 로드
        /// </summary>
        private void RegisterExtruderRecipeStorage(ACT act)
        {
            // 1. DualStorage 생성 (List<ExtruderRecipe> 타입)
            var storage = new DualStorage<List<ExtruderRecipe>>(
                new JsonFileStorage<List<ExtruderRecipe>>(),
                new SqliteStorage<List<ExtruderRecipe>>()
            );

            // 2. ActExtruderRecipe에 등록
            act.ExtruderRecipe.RegisterStorageService(storage);

            // 3. 현재 레시피 경로에서 로드 (없으면 30개 기본 생성)
            act.ExtruderRecipe.Load();

            // 4. Old DB 백업 자동 삭제 (60일 경과)
            string currentRecipePath = act.Recipe.GetCurrentRecipePath();
            new SqliteStorage<List<ExtruderRecipe>>().DeleteOldBackups(currentRecipePath, "ExtruderRecipes");
        }

        private void InitSerial(ACT act)
        {
            if (false)
            {
                try
                {
                    bool useMock = isSimulation;
                    // 예: 1번 시리얼 포트
                    ISerialPortClient serial1 = new SystemSerialPortClient();
                    serial1.Init("Serial_Device1", "COM1", 9600, 8, System.IO.Ports.Parity.None, System.IO.Ports.StopBits.One, EndType.LF); // (포트 설정은 옵션에서 로드)
                    act.SerialPort.RegisterClient("Serial_Device1", serial1);
                    // 예: 2번 시리얼 포트
                    ISerialPortClient serial2 = new SystemSerialPortClient();
                    serial2.Init("Serial_Device2", "COM2", 115200, 8, System.IO.Ports.Parity.Even, System.IO.Ports.StopBits.One, EndType.CRLF);
                    act.SerialPort.RegisterClient("Serial_Device2", serial2);
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"시리얼 포트 클라이언트 초기화 실패: {ex.Message}");
                    // (필요시 프로그램 종료 또는 알림 처리)
                }
            }
        }

        private void InitModbus(ACT act)
        {
            if (false)
            {
                try
                {
                    bool useMock = isSimulation;

                    // 예: 1번 PLC
                    IModbusClient plc1 = new NModbusClient();
                    plc1.Init("PLC_Main", "192.168.1.10", 502); // (IP, Port는 옵션에서 로드)
                    act.Modbus.RegisterClient("PLC_Main", plc1);

                    // 예: 2번 온도 조절기
                    IModbusClient tempController = new NModbusClient();
                    tempController.Init("Temp_ZoneA", "192.168.1.11", 502);
                    act.Modbus.RegisterClient("Temp_ZoneA", tempController);
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"Modbus 클라이언트 초기화 실패: {ex.Message}");
                    // (필요시 프로그램 종료 또는 알림 처리)
                }
            }
        }

        private void InitVision(ACT act)
        {
            bool useMock = isSimulation;

            var visionOptions = act.Option.Option2;

            if (useMock)
            {
                foreach (var visionInfo in visionOptions.GVision)
                {
                  
                    ITcpNetworkClient visionClient;

                    var mockClient = new Mock_GVisionTcpNetworkClient();                  
                   
                    {
                        //Mock 규칙 정의                         
                        mockClient.AddRule(VisionCommandType.JobChange.ToString(), () =>
                        {
                            return new GrabDone { iCamNO = 0, iErrCode = 0, iNozlNo = 1, iChipNo = 1 };
                        });

                        mockClient.AddRule(VisionCommandType.SOT.ToString(), () =>
                        {
                            Log.Instance.Debug("[MockClient] 랜덤 Gvision.EOT 응답 생성 중...");

                            return new EOT
                            {
                                iCamNO = 10,
                                iErrorCode = 0,
                                iInspNO = (int)(rand.Next(0,10)),
                                sRslt = new List<MatchResult>
                                    {
                                        new MatchResult
                                        {
                                            iNozlID = 1,
                                            iInspectResult = rand.Next(0, 2),
                                            fCurrX = 100.0f + (float)(rand.NextDouble() * 1.0 - 0.5),
                                            fCurrY = 200.0f + (float)(rand.NextDouble() * 1.0 - 0.5),
                                            fCurrT = (float)(rand.NextDouble() * 0.2 - 0.1)
                                        }
                                    }
                            };
                        });
                       
                        //다른 메세지 정의 필요하면 여기 아래에 추가
                    }                  

                    visionClient = mockClient; 
                  
                    visionClient.Init(
                        visionInfo.Name,
                        visionInfo.IP,
                        visionInfo.Port,
                        autoReconnect: visionInfo.AutoReconnect,
                        endType: visionInfo.MsgEnd
                    );
                  
                    act.Vision.RegisterClient(visionInfo.Name, visionClient);
                }
            }
            else //실 연결
            {
                foreach (var visionInfo in visionOptions.GVision)
                {                  
                    ITcpNetworkClient visionClient;              
                    visionClient = new Tcp.TcpClient();                   

                 
                    visionClient.Init(
                        visionInfo.Name,
                        visionInfo.IP,
                        visionInfo.Port,
                        autoReconnect: visionInfo.AutoReconnect,
                        endType: visionInfo.MsgEnd
                    );
                 
                    act.Vision.RegisterClient(visionInfo.Name, visionClient);
                }
            }               
        }



        /// <summary>
        /// 알림 팝업 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleCoreNotification(object? sender, NotifyEventArgs e)
        {
            // 백그라운드 스레드에서 호출될 수 있으므로 UI 스레드로 전환
            Action showAction = () =>
            {
                var popupGroup = new List<FormNotify>();

                foreach (Screen screen in Screen.AllScreens)
                {
                    FormNotify notifyForm = new FormNotify(e.Title, e.Message, e.Type);

                    // 팝업 위치를 모니터 중앙으로 설정
                    Rectangle bounds = screen.WorkingArea;
                    notifyForm.StartPosition = FormStartPosition.Manual;
                    notifyForm.Left = bounds.Left + (bounds.Width - notifyForm.Width) / 2;
                    notifyForm.Top = bounds.Top + (bounds.Height - notifyForm.Height) / 2;

                    popupGroup.Add(notifyForm);
                }

                // 모든 팝업에게 "그룹" 정보 전달
                foreach (var form in popupGroup)
                {
                    form.SetSiblingGroup(popupGroup);
                }

                // 모든 팝업 표시
                foreach (var form in popupGroup)
                {
                    form.Show();
                }
            };

            // UI 스레드에서 실행
            Form mainForm = Application.OpenForms.Cast<Form>().FirstOrDefault();
            if (mainForm != null && mainForm.InvokeRequired)
            {
                mainForm.Invoke(showAction);
            }
            else
            {
                showAction();
            }
        }



        private void ShowMainForm()
        {

            FormMain mainForm = new FormMain();
            mainForm.FormClosed += (s, args) => this.Close();
            mainForm.Show();
            this.Hide();

            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action(() => richTextBox1.Clear()));
                lblStatus.Invoke(new Action(() => lblStatus.Text = ""));
            }
            else
            {
                richTextBox1.Clear();
                lblStatus.Text = "";
            }
        }

        private void _timerAutoClose_Tick(object sender, EventArgs e)
        {
            _autoCloseCountdown--;

            if (_autoCloseCountdown <= 0)
            {
                // 시간 종료 시 자동 진입
                _timerAutoClose.Stop();
                EnterMainSystem();
            }
            else
            {
                _ButtonStart.Text = $"Start({_autoCloseCountdown}s)";
            }
        }
        private void EnterMainSystem()
        {
            _timerAutoClose.Stop(); // 타이머 정지
            ShowMainForm();         // 메인 폼 표시 및 스플래시 숨김
        }

        private void _ButtonStart_Click(object sender, EventArgs e)
        {
            EnterMainSystem();
        }
    }
}
