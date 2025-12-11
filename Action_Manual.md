# Action Components Manual

This document outlines the public functions and properties available in the `Action` components of the `EQ.Core` layer. These components are accessed via the `ActManager.Instance.Act` singleton.

---

## ActAlarm
알람 이력을 DB에 저장하는 컴포넌트입니다.
(Component that saves alarm history to the DB.)

### `RegisterStorageService(IDataStorage<AlarmData> storageService)`
- **Summary:** FormSplash에서 Storage 서비스를 주입받습니다. (Injects the Storage service from FormSplash.)
- **Parameters:**
  - `IDataStorage<AlarmData> storageService`: The storage service instance for alarm data.
- **Returns:** `void`

### `SaveAlarm(string id, string callName, string info)`
- **Summary:** 알람을 DB에 저장합니다 (PopupNoti에서 호출됨). (Saves an alarm to the DB (called from PopupNoti).)
- **Parameters:**
  - `string id`: The alarm ID.
  - `string callName`: The name of the calling function/sequence.
  - `string info`: Additional information about the alarm.
- **Returns:** `void`

### `GetAlarmDbPath()`
- **Summary:** UI에서 알람 기록을 읽기 위한 헬퍼 메서드입니다. (Helper method for the UI to read alarm history.)
- **Parameters:** None
- **Returns:** `string` (The full path to the alarm database file.)

### `GetAlarmDbKey()`
- **Summary:** UI에서 테이블 이름을 얻기 위한 헬퍼 메서드입니다. (Helper method for the UI to get the table name.)
- **Parameters:** None
- **Returns:** `string` (The key/table name for alarm history.)

---

## ActIO
실린더, 센서 등 입출력(IO) 관련 기능을 제어합니다.
(Controls IO-related functions such as cylinders and sensors.)

### `SetHardwareController(IIoController controller)`
- **Summary:** 실제 하드웨어(IO 컨트롤러)를 연결합니다. (Connects the actual hardware (IO controller).)
- **Parameters:**
  - `IIoController controller`: The concrete hardware controller instance.
- **Returns:** `void`

### `GetIoStatus()`
- **Summary:** 현재 캐시된 IO 상태를 읽습니다. (Reads the current cached IO status.)
- **Parameters:** None
- **Returns:** `(byte[] _input, byte[] _output)` (A tuple containing the input and output byte arrays.)

### `ReadInput(IO_IN name)`
- **Summary:** 특정 Input 신호를 읽습니다. (Reads a specific Input signal.)
- **Parameters:**
  - `IO_IN name`: The enum name of the input to read.
- **Returns:** `bool` (The state of the input.)

### `ReadOutput(IO_OUT name)`
- **Summary:** 특정 Output 신호의 현재 상태를 읽습니다. (Reads the current state of a specific Output signal.)
- **Parameters:**
  - `IO_OUT name`: The enum name of the output to read.
- **Returns:** `bool` (The state of the output.)

### `WriteInput(IO_IN name, bool On)`
- **Summary:** 특정 Input 신호를 강제로 씁니다 (시뮬레이션 용). (Forcibly writes to a specific Input signal (for simulation).)
- **Parameters:**
  - `IO_IN name`: The enum name of the input to write.
  - `bool On`: The state to write (`true` for On, `false` for Off).
- **Returns:** `void`

### `WriteOutput(IO_OUT name, bool On)`
- **Summary:** 특정 Output 신호를 씁니다. (Writes to a specific Output signal.)
- **Parameters:**
  - `IO_OUT name`: The enum name of the output to write.
  - `bool On`: The state to write (`true` for On, `false` for Off).
- **Returns:** `void`

### `doubleTypeOn(IO_OUT name, bool checkInputIO = true)`
- **Summary:** 복동실린더와 같이 ON/OFF가 쌍으로 된 신호를 제어합니다. (Controls signals that come in pairs like double-acting cylinders.)
- **Parameters:**
  - `IO_OUT name`: The output to turn ON (the corresponding OFF signal is inferred automatically).
  - `bool checkInputIO`: Whether to wait for the corresponding sensor input before completing the action.
- **Returns:** `Task<ActionStatus>`

---

## ActPIO
PIO (Parallel I/O) 통신 프로토콜을 제어하고 핸드오버 시퀀스를 관리합니다.
(Controls the PIO (Parallel I/O) communication protocol and manages handover sequences.)

### `CurrentState` (Property)
- **Summary**: 현재 PIO 핸드오버 시퀀스의 상태를 나타냅니다. (Indicates the current state of the PIO handover sequence.)
- **Type**: `PIOState` (Read-only)

### `RegisterHandoverController(IPIOHandover handover)`
- **Summary**: `FormSplash`에서 생성된 실제 하드웨어 PIO 컨트롤러(`IPIOHandover`) 구현체를 주입받습니다. (Injects the concrete `IPIOHandover` implementation created in `FormSplash`.)
- **Parameters**:
  - `IPIOHandover handover`: 실제 PIO 하드웨어를 제어하는 컨트롤러 인스턴스. (The controller instance that controls the actual PIO hardware.)
- **Returns**: `void`

### `GetSignal(PIOSignal signal)`
- **Summary**: 특정 PIO 신호의 현재 상태를 읽습니다. (Reads the current state of a specific PIO signal.)
- **Parameters**:
  - `PIOSignal signal`: 상태를 확인할 PIO 신호. (The PIO signal to check the state of.)
- **Returns**: `bool` (신호의 현재 값, `true`는 ON, `false`는 OFF). (The current value of the signal, `true` for ON, `false` for OFF.)

### `LoadAsync()`
- **Summary**: 재료 반입(Load) PIO 핸드오버 시퀀스를 시작합니다. `_act.ExecuteAction()` 템플릿을 사용하여 스텝 기반으로 동작합니다. (Initiates the material Load PIO handover sequence. Operates step-by-step using the `_act.ExecuteAction()` template.)
- **Parameters**: None
- **Returns**: `Task<ActionStatus>` (시퀀스 완료 상태). (The status of the sequence completion.)

---

## ActMotion
모터, 로봇 등 모션(Motion) 관련 기능을 제어합니다.
(Controls motion-related functions such as motors and robots.)

### `SetHardwareController(IMotionController controller)`
- **Summary:** 실제 하드웨어(모션 컨트롤러)를 연결합니다. (Connects the actual hardware (motion controller).)
- **Parameters:**
  - `IMotionController controller`: The concrete hardware controller instance.
- **Returns:** `void`

### `HomeSearchAsync()`
- **Summary:** 전체 축에 대한 원점 복귀 동작을 수행합니다. (Performs a home search operation for all axes.)
- **Parameters:** None
- **Returns:** `Task<ActionStatus>`

---

## ActRecipe
현재 레시피의 이름과 경로를 관리합니다.
(Manages the name and path of the current recipe.)

### `CurrentRecipeName` (Property)
- **Summary:** 현재 선택된 레시피의 이름입니다. (The name of the currently selected recipe.)
- **Type:** `string` (Read-only)

### `Initialize()`
- **Summary:** UI 시작 시 호출되어 레시피 시스템을 초기화합니다. (Called on UI start to initialize the recipe system.)
- **Parameters:** None
- **Returns:** `void`

### `SetCurrentRecipe(string recipeName)`
- **Summary:** 현재 활성화된 레시피를 변경합니다. (Changes the currently active recipe.)
- **Parameters:**
  - `string recipeName`: The name of the recipe to activate.
- **Returns:** `void`

### `GetCurrentRecipePath()`
- **Summary:** 현재 레시피의 전체 폴더 경로를 반환합니다 (경로가 없으면 생성). (Returns the full folder path of the current recipe (creates it if it doesn't exist).)
- **Parameters:** None
- **Returns:** `string`

### `GetAllRecipeNames()`
- **Summary:** 모든 레시피 폴더 이름 목록을 반환합니다. (Returns a list of all recipe folder names.)
- **Parameters:** None
- **Returns:** `List<string>`

### `DeleteRecipe(string recipeName)`
- **Summary:** 레시피를 삭제합니다 (현재 사용 중인 레시피는 삭제 불가). (Deletes a recipe (the current recipe cannot be deleted).)
- **Parameters:**
  - `string recipeName`: The name of the recipe to delete.
- **Returns:** `bool` (`true` if successful, `false` otherwise.)

### `CopyRecipe(string sourceRecipeName, string newRecipeName)`
- **Summary:** 기존 레시피를 새 이름으로 복사합니다. (Copies an existing recipe to a new name.)
- **Parameters:**
  - `string sourceRecipeName`: The name of the recipe to copy from.
  - `string newRecipeName`: The name for the new copied recipe.
- **Returns:** `bool` (`true` if successful, `false` otherwise.)

---

## ActTowerLamp
장비 상태(FSM)에 맞게 타워 램프 및 부저를 제어합니다.
(Controls the tower lamp and buzzer according to the equipment state (FSM).)

### `SetState(EqState state)`
- **Summary:** FSM 상태에 따라 램프와 부저 상태를 설정합니다. (Sets the lamp and buzzer state according to the FSM state.)
- **Parameters:**
  - `EqState state`: The new equipment state.
- **Returns:** `void`

### `SilenceBuzzer()`
- **Summary:** 부저만 강제로 끕니다 (UI의 Mute 버튼 등). (Forcibly turns off only the buzzer (e.g., for a Mute button in the UI).)
- **Parameters:** None
- **Returns:** `void`

---

## ActUser
사용자 인증 및 권한 관리를 담당합니다.
(Handles user authentication and permission management.)

### `CurrentUserLevel` (Property)
- **Summary:** 현재 로그인된 사용자의 권한 레벨입니다. (The permission level of the currently logged-in user.)
- **Type:** `UserLevel` (Read-only)

### `LoadPasswords()`
- **Summary:** 프로그램 시작 시 파일에서 암호 해시를 로드합니다. (Loads password hashes from the file on program start.)
- **Parameters:** None
- **Returns:** `void`

### `Login(UserLevel level, string password)`
- **Summary:** 로그인을 시도하고 성공 시 `CurrentUserLevel`을 변경합니다. (Attempts to log in and changes `CurrentUserLevel` on success.)
- **Parameters:**
  - `UserLevel level`: The level to log in as.
  - `string password`: The password to verify.
- **Returns:** `bool` (`true` for success, `false` for failure.)

### `Logout()`
- **Summary:** 로그아웃하고 `CurrentUserLevel`을 기본값(Operator)으로 되돌립니다. (Logs out and reverts `CurrentUserLevel` to the default (Operator).)
- **Parameters:** None
- **Returns:** `void`

### `CheckAccess(UserLevel requiredLevel)`
- **Summary:** 현재 사용자가 특정 기능에 대한 권한이 있는지 확인합니다. (Checks if the current user has permission for a specific feature.)
- **Parameters:**
  - `UserLevel requiredLevel`: The minimum level required for access.
- **Returns:** `bool` (`true` if access is granted, `false` otherwise.)

### `ChangePassword(UserLevel level, string oldPassword, string newPassword)`
- **Summary:** 특정 레벨의 암호를 변경합니다. (Changes the password for a specific level.)
- **Parameters:**
  - `UserLevel level`: The user level to change.
  - `string oldPassword`: The current password for verification.
  - `string newPassword`: The new password to set.
- **Returns:** `bool` (`true` if successful, `false` otherwise.)

---

## ActUserOption
장비의 파라미터(UserOption)를 관리합니다.
(Manages equipment parameters (UserOption).)

### `Option1`, `Option2`, `Option3`, `Option4`, `OptionUI` (Properties)
- **Summary:** 타입별 파라미터 객체에 직접 접근하기 위한 바로 가기 속성입니다. (Shortcut properties for direct access to parameter objects by type.)
- **Type:** `UserOption1`, `UserOption2`, etc. (Read-only)

### `RegisterStorageService<T>(IDataStorage<T> storageService)`
- **Summary:** `FormSplash`에서 파라미터 저장을 위한 스토리지 서비스를 주입합니다. (Injects a storage service for saving parameters from `FormSplash`.)
- **Parameters:**
  - `IDataStorage<T> storageService`: The storage service instance.
- **Returns:** `void`

### `LoadAllOptionsFromStorage()`
- **Summary:** 현재 레시피 경로를 기준으로 모든 파라미터를 스토리지에서 읽어와 캐시에 로드합니다. (Reads all parameters from storage based on the current recipe path and loads them into the cache.)
- **Parameters:** None
- **Returns:** `void`

### `Get<T>()`
- **Summary:** 캐시에서 제네릭 타입으로 파라미터 객체를 가져옵니다. (Gets a parameter object from the cache by its generic type.)
- **Parameters:** None
- **Returns:** `T` (The cached parameter object.)

### `Set<T>(T options)`
- **Summary:** UI 등에서 수정한 파라미터 객체로 캐시를 덮어씁니다. (Overwrites the cache with a parameter object modified by the UI or other sources.)
- **Parameters:**
  - `T options`: The modified parameter object.
- **Returns:** `void`

### `Save<T>()`
- **Summary:** 현재 캐시된 파라미터 객체를 스토리지(파일)에 저장합니다. (Saves the currently cached parameter object to storage (file).)
- **Parameters:** None
- **Returns:** `void`

### `GetUIValueByName<T>(string name, T defaultValue = default)`
- **Summary:** `OptionUI` 리스트에서 이름(name)으로 컨트롤 설정을 찾아 값을 반환합니다. (Finds a control setting by name in the `OptionUI` list and returns its value.)
- **Parameters:**
  - `string name`: The name of the control setting to find.
  - `T defaultValue`: The value to return if the name is not found or conversion fails.
- **Returns:** `T` (The converted value or the default value.)

---

## ActVision
비전 장비와의 TCP/IP 통신을 담당합니다.
(Handles TCP/IP communication with vision equipment.)

### `OnRawDataReceived` (Event)
- **Summary:** 비전 장비로부터 받은 모든 응답을 원본 JSON 문자열 그대로 전달하는 이벤트입니다. (An event that forwards all responses received from the vision equipment as a raw JSON string.)
- **Type:** `Action<string, string>` (clientName, jsonData)

### `OnCommandReceived` (Event)
- **Summary:** 파싱된 C# 객체를 전달하는 이벤트입니다. (An event that forwards the parsed C# object.)
- **Type:** `Action<string, VisionCommandType, object>` (clientName, commandType, parsedObject)

### `RegisterClient(string name, ITcpNetworkClient client)`
- **Summary:** 통신할 TCP 클라이언트를 등록하고 이벤트 핸들러를 연결합니다. (Registers a TCP client to communicate with and connects event handlers.)
- **Parameters:**
  - `string name`: A unique name for the client (e.g., "Vision1").
  - `ITcpNetworkClient client`: The client instance that implements the communication protocol.
- **Returns:** `void`

### `GetClient(string name)`
- **Summary:** UI 등에서 연결 상태를 확인하기 위해 등록된 클라이언트 인스턴스를 반환합니다. (Returns the registered client instance to check the connection status from the UI, etc.)
- **Parameters:**
  - `string name`: The name of the registered client.
- **Returns:** `ITcpNetworkClient`

### `SendGenericCommand(string clientName, object commandObject)`
- **Summary:** C# 객체를 JSON으로 직렬화하여 지정된 비전 장비로 전송합니다. (Serializes a C# object to JSON and sends it to the specified vision equipment.)
- **Parameters:**
  - `string clientName`: The name of the client to send the command to.
  - `object commandObject`: The command object to serialize and send.
- **Returns:** `Task`

---

## Action_Sample
단위 동작(Action) 구현을 위한 샘플 클래스입니다.
(A sample class for implementing a unit action.)

### `SampleAsync()`
- **Summary:** `ExecuteAction` 템플릿 메서드를 사용하는 방법에 대한 예시입니다. (An example of how to use the `ExecuteAction` template method.)
- **Parameters:** None
- **Returns:** `Task<ActionStatus>`

---

## PIO_View
PIO 신호의 현재 상태와 PIO 핸드오버 시퀀스 진행 상태를 시각적으로 모니터링하고, PIO 시퀀스를 시작할 수 있는 사용자 컨트롤입니다. (A user control for visually monitoring the current state of PIO signals and the PIO handover sequence progress, and initiating PIO sequences.)

### 사용 방법 (Usage)
`FormMain`과 같은 상위 Form의 `panelMain` 내부에 `PIO_View` 인스턴스를 추가하여 사용합니다.

### 컨트롤 (Controls)
- **PIO Signal Indicators**: 각 `PIOSignal`의 상태(ON/OFF)를 녹색(ON) 또는 회색(OFF) 패널로 표시합니다.
- **`lblPIOState`**: 현재 `ActPIO`의 전반적인 `PIOState`(예: Idle, RequestingLoad)를 표시합니다.
- **`Load Start` Button**: `ActPIO.LoadAsync()` 메서드를 호출하여 재료 반입 시퀀스를 시작합니다.
- **`Unload Start` Button**: 재료 반출 시퀀스 시작 (현재는 미구현 상태).

