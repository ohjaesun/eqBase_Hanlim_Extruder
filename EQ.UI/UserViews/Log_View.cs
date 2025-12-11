using EQ.Common.Logs;
using EQ.UI.Controls; // EqBase 컨트롤 사용
using System;
using System.Collections.Concurrent; // ConcurrentQueue 사용
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer; // WinForms Timer 명시

namespace EQ.UI.UserViews
{
    // 1. UserControlBase 상속
    public partial class Log_View : UserControlBase
    {
        // 2. 스레드 안전한 ConcurrentQueue 사용
        private ConcurrentQueue<string> _logQueue;
        private Timer _flushTimer;

        // 3. LogType별 필터링/색상 관리를 위해 Dictionary 사용 (효율적)
        private Dictionary<Log.LogType, _CheckBox> _filterCheckBoxes;
        private Dictionary<Log.LogType, Color> _logColors;

        private int _currentLineCount = 0;
        private const int MAX_LINES = 2000;

        public Log_View()
        {
            InitializeComponent();
        }

        private void Log_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            _LabelTitle.Text = "Log View";
            _ButtonSave.Visible = false; // 저장 버튼 숨김

            _logQueue = new ConcurrentQueue<string>();
            _filterCheckBoxes = new Dictionary<Log.LogType, _CheckBox>();
            _logColors = new Dictionary<Log.LogType, Color>();

            InitializeFilters();

            // 4. Log.Instance.OnMsg 구독 (EqBase 로그 시스템)
            //Log.Instance.OnMsg += Instance_OnMsg;
            SafeSubscribe(subscribe: () => Log.Instance.OnMsg += Instance_OnMsg,
                          unsubscribe: () => Log.Instance.OnMsg -= Instance_OnMsg);

            _flushTimer = new Timer();
            _flushTimer.Interval = 200;
            _flushTimer.Tick += FlushTimer_Tick;
            _flushTimer.Start();

            this.Disposed += Log_View_Disposed;           
        }

        private void _CheckBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            // _CheckBoxAll의 현재 체크 상태를 가져옴
            bool isChecked = _CheckBoxAll.Checked;

            // _filterCheckBoxes 딕셔너리 (Debug, Info, Error 등)에 보관된
            // 모든 개별 로그 타입 체크박스를 순회
            foreach (var filterCheckBox in _filterCheckBoxes.Values)
            {
                // ALL 체크박스의 상태와 동일하게 설정
                filterCheckBox.Checked = isChecked;
            }
        }

        private void InitializeFilters()
        {
            // EqBase의 LogType Enum 사용
            var logTypes = (Log.LogType[])Enum.GetValues(typeof(Log.LogType));
            var colors = new List<Color> {
                Color.Gray, Color.Black, Color.OrangeRed, Color.Red, Color.Blue,
                Color.DarkCyan, Color.Purple, Color.DarkGreen, Color.DarkBlue, Color.Green, Color.Brown
            };

            for (int i = 0; i < logTypes.Length; i++)
            {
                var logType = logTypes[i];
                var color = colors[i % colors.Count]; // 색상 순환

                _CheckBox chk = new _CheckBox
                {
                    Text = logType.ToString(),
                    Tag = logType,
                    Checked = true, // 기본값은 모두 체크
                    ThemeStyle = ThemeStyle.Info_Sky,
                    AutoSize = true,
                    Margin = new Padding(3)
                };

                chk.ForeColor = color; // 체크박스 텍스트에 색상 적용
                _filterCheckBoxes.Add(logType, chk);
                _logColors.Add(logType, color);
                _FlowLayoutPanel.Controls.Add(chk);
            }
        }

        // 5. [백그라운드 스레드] Log.Instance.OnMsg 이벤트 핸들러
        private void Instance_OnMsg(string result)
        {
            // 큐에 로그 메시지 추가 (스레드 안전)
            _logQueue.Enqueue(result);
        }

        // 6. [UI 스레드] Timer 틱 이벤트 (배치 처리)
        private void FlushTimer_Tick(object sender, EventArgs e)
        {
            // 일시정지 상태면 UI 업데이트 안 함
            if (_CheckBoxPause.Checked) return;
            if (_logQueue.Count <= 0) return;

            int messagesToProcess = 100; // 한 번에 100개까지만 처리

            _RichTextBoxLog.SuspendLayout();

            while (messagesToProcess-- > 0 && _logQueue.TryDequeue(out string message))
            {
                // 7. [개선된 로직] 로그 메시지 파싱
                (Log.LogType logType, bool known) = ParseLogType(message);

                bool isAllChecked = _CheckBoxAll.Checked;
                bool isTypeChecked = known && _filterCheckBoxes[logType].Checked;

                // 8. 필터링
                if (isAllChecked || isTypeChecked)
                {
                    // 9. 라인 수 관리
                    if (_currentLineCount > MAX_LINES)
                    {
                        _RichTextBoxLog.Clear();
                        _currentLineCount = 0;
                    }

                    _RichTextBoxLog.SelectionColor = known ? _logColors[logType] : Color.Black;
                    _RichTextBoxLog.AppendText(message + Environment.NewLine);
                    _currentLineCount++;
                }
            }

            _RichTextBoxLog.ResumeLayout();

            // 10. [개선된 기능] 자동 스크롤
            if (_CheckBoxAutoScroll.Checked)
            {
                _RichTextBoxLog.ScrollToCaret();
            }
        }

        // 11. [개선된 로직] 빠르고 정확한 파서
        private (Log.LogType, bool) ParseLogType(string message)
        {
            if (string.IsNullOrEmpty(message) || message[0] != '[')
                return (default, false);

            int endIndex = message.IndexOf(']');
            if (endIndex == -1)
                return (default, false);

            string typeString = message.Substring(1, endIndex - 1);

            if (Enum.TryParse<Log.LogType>(typeString, out var logType))
            {
                return (logType, true);
            }
            return (default, false);
        }

        private void Log_View_Disposed(object sender, EventArgs e)
        {
            _flushTimer?.Stop();
            Log.Instance.OnMsg -= Instance_OnMsg;
            this.Disposed -= Log_View_Disposed;
        }

        private void _ButtonClear_Click(object sender, EventArgs e)
        {
            _RichTextBoxLog.Clear();
            _currentLineCount = 0;
        }
    }
}