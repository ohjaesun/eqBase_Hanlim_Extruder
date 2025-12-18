// EQ.UI/UserViews/GVisionView.cs
using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Entities; // Domain으로 옮긴 모델 사용
using EQ.Domain.Enums;
using EQ.UI.Controls; // EqBase 컨트롤 사용
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Reflection.Emit;
using System.Windows.Forms;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;

namespace EQ.UI.UserViews
{
    // 1. UserControlBase 상속
    public partial class GVision_View : UserControlBase
    {
        private readonly ACT _act;

        // 2. 마지막 수신 데이터를 저장 (PropertyGrid2 표시용)
        private readonly ConcurrentDictionary<string, object> _lastReceivedData = new();

        public GVision_View()
        {
            InitializeComponent();
            _act = ActManager.Instance.Act; // 3. ActManager에서 ACT 인스턴스 가져오기
        }

        private void GVisionView_Load(object sender, EventArgs e)
        {
            // 4. UserControlBase 컨트롤 설정
            _LabelTitle.Text = "Global Vision (Debug View)";
            _ButtonSave.Visible = false;

            if (DesignMode) return;

            // 5. 콤보박스 데이터 바인딩
            comboBox1.DataSource = Enum.GetValues(typeof(GVisionType));
            comboBox2.DataSource = Enum.GetValues(typeof(VisionCommandType));
            comboBox3.DataSource = Enum.GetValues(typeof(VisionCommandType));

            // 6. ActVision 이벤트 구독
            // (RawData: richTextBox1 / Command: propertyGrid2 업데이트용)
            //  _act.Vision.OnRawDataReceived += Vision_OnRawDataReceived;
            //  _act.Vision.OnCommandReceived += Vision_OnCommandReceived;
            SafeSubscribe(
          () => _act.Vision.OnRawDataReceived += Vision_OnRawDataReceived,
          () => _act.Vision.OnRawDataReceived -= Vision_OnRawDataReceived
      );
            SafeSubscribe(
        () => _act.Vision.OnCommandReceived += Vision_OnCommandReceived,
        () => _act.Vision.OnCommandReceived -= Vision_OnCommandReceived
    );

            timer1.Interval = 500; // 0.5초마다 연결 상태 체크
            timer1.Start();
            Disposed += GVisionView_Disposed;
        }

        private void GVisionView_Disposed(object sender, EventArgs e)
        {
            timer1.Stop();
            // 7. 이벤트 구독 해제 (메모리 누수 방지)
            if (_act != null)
            {
                _act.Vision.OnRawDataReceived -= Vision_OnRawDataReceived;
                _act.Vision.OnCommandReceived -= Vision_OnCommandReceived;
            }
            Disposed -= GVisionView_Disposed;
        }

        // 8. (신규) ActVision의 Raw 데이터 수신 핸들러
        private void Vision_OnRawDataReceived(string clientName, string jsonMessage)
        {
            if (this.InvokeRequired)
            {
                // 3. 백그라운드 스레드일 경우, UI 스레드로 이 메서드 자체를 다시 호출
                this.Invoke((Action)(() => Vision_OnRawDataReceived(clientName, jsonMessage)));
                return;
            }

            // 선택된 비전 채널의 메시지만 로그에 표시
            if (comboBox1.SelectedValue?.ToString() == clientName)
            {
                LogToRichTextBox($"[RECV] {jsonMessage}");
            }
        }

        // 9. (신규) ActVision의 C# 객체 수신 핸들러
        private void Vision_OnCommandReceived(string clientName, VisionCommandType cmdType, object data)
        {
            // 마지막 수신 데이터를 딕셔너리에 저장 (타이머가 propertyGrid2에 표시)
            _lastReceivedData[clientName + cmdType.ToString()] = data;
        }

        // 10. (수정) 타이머: 연결 상태(Label1) 및 수신 객체(PropertyGrid2) 업데이트
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null) return;

            string clientName = comboBox1.SelectedValue.ToString();

            // 11. ActVision의 헬퍼 메서드로 클라이언트 인스턴스 가져오기
            var client = _act.Vision.GetClient(clientName);
            bool connect = (client != null && client.IsConnected);

            // 12. EqBase 테마로 상태 표시
            var style = connect ? ThemeStyle.Success_Green : ThemeStyle.Danger_Red;
            if (_Label1.ThemeStyle != style)
            {
                _Label1.ThemeStyle = style;
                _Label1.Text = connect ? "Connect" : "Disconnect";
            }

            // 13. (수정) _Label2 (GetInfo)는 ActVision에 없으므로 제거 (또는 주석)
            // _Label2.Text = ...

            // 14. (수정) PropertyGrid2 업데이트 (리플렉션 대신 딕셔너리 조회)
            if (comboBox3.SelectedValue == null) return;
            string cmdName = comboBox3.SelectedValue.ToString();

            if (_lastReceivedData.TryGetValue(clientName + cmdName, out object lastData))
            {
                if (!object.ReferenceEquals(propertyGrid2.SelectedObject, lastData))
                {
                    propertyGrid2.SelectedObject = lastData; // 마지막 수신 객체 표시
                }
            }
        }

        // 15. (유지) Send 객체 선택 시 PropertyGrid1 업데이트
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = sender as ComboBox;
            if (item.SelectedValue == null) return;

            // 1. (수정) Enum.TryParse 사용
            if (!Enum.TryParse<VisionCommandType>(item.SelectedValue.ToString(), out var cmdType))
            {
                propertyGrid1.SelectedObject = null;
                return;
            }

            // 2. (수정) 리플렉션 대신 switch 문 사용 (더 안전하고 빠름)
            object instance = null;
            switch (cmdType)
            {
                case VisionCommandType.SOT:
                    instance = new SOT();
                    break;
                case VisionCommandType.JobChange:
                    instance = new JobChange();
                    break;
                case VisionCommandType.StartPack:
                    instance = new StartPack();
                    break;
               
                case VisionCommandType.LotEnd:
                    instance = new LotEnd();
                    break;

                case VisionCommandType.EOT:
                    instance = new EOT();
                    break;
                    // (EOT, GrabDone은 수신 전용이므로 생략)
            }

            // 3. (수정) GVisionView는 "Send"용이므로 propertyGrid1만 설정
            if (item.Name == "comboBox2")
            {
                propertyGrid1.SelectedObject = instance;
            }

            // (comboBox3은 수신용이므로 이 핸들러를 분리하는 것이 좋습니다)
            if (item.Name == "comboBox3")
            {
                propertyGrid2.SelectedObject = instance;
            }
        }

        // 16. (수정) Send 버튼: ActVision의 범용 메서드 호출 (리플렉션 제거)
        private void _Button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null || propertyGrid1.SelectedObject == null)
                return;

            string clientName = comboBox1.SelectedValue.ToString();
            object cmdObj = propertyGrid1.SelectedObject;

            // 17. 리플렉션 대신 ActVision의 헬퍼 메서드 호출
            _act.Vision.SendGenericCommand(clientName, cmdObj);

            LogToRichTextBox($"[SEND] {JsonConvert.SerializeObject(cmdObj)}");
        }

        // 18. (유지) RichTextBox 로거 (Invoke 사용)
        private void LogToRichTextBox(string msg)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke((MethodInvoker)delegate { LogToRichTextBox(msg); });
                return;
            }
            if (richTextBox1.Lines.Length > 500)
            {
                richTextBox1.Clear();
            }
            string timestamp = $"[{DateTime.Now:HH:mm:ss.fff}] ";
            Color msgColor;

            if (msg.StartsWith("[SEND]"))
            {
                msgColor = Color.Blue;
            }
            else if (msg.StartsWith("[RECV]"))
            {
                msgColor = Color.Red;
            }
            else
            {
                msgColor = richTextBox1.ForeColor; // (기본색, 예: Black)
            }

            // 4. (수정) 텍스트 추가 로직 변경
            //    AppendText()는 현재 SelectionColor 설정을 따릅니다.

            // 4a. 타임스탬프 추가 (기본색)
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.SelectionLength = 0;
            richTextBox1.SelectionColor = richTextBox1.ForeColor; // 기본색
            richTextBox1.AppendText(timestamp);

            // 4b. 메시지 추가 (지정된 색상)
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.SelectionLength = 0;
            richTextBox1.SelectionColor = msgColor; // 파란색 또는 붉은색
            richTextBox1.AppendText(msg + Environment.NewLine);

            // 4c. (중요) 스크롤 및 색상 초기화
            richTextBox1.SelectionColor = richTextBox1.ForeColor; // 다음 입력을 위해 기본색으로 복원
            richTextBox1.ScrollToCaret();
        }

        // 19. (수정) 비전 채널 변경 시 로그 초기화
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            propertyGrid2.SelectedObject = null;
            _lastReceivedData.Clear(); // 마지막 수신 데이터 초기화
        }
    }
}