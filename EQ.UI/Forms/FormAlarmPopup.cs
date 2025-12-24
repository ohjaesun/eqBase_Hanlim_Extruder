using EQ.Core.Service;
using EQ.Domain.Entities; // AlarmSolutionStorage, AlarmSolutionData 사용
using EQ.Domain.Enums;    // ErrorList 사용
using EQ.UI.Controls;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EQ.UI.Forms
{
    public partial class FormAlarmPopup : FormBase
    {
        public FormAlarmPopup() // Designer support
        {
            InitializeComponent();
        }

        public FormAlarmPopup(string title, string message)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.TopMost = true;
            this.ShowInTaskbar = false;

            // [수정] 파일에서 원인/조치 내용을 찾아 메시지에 추가
            var detailedMessage = AppendSolutionInfo(title);

            // UI 설정
            this._LabelTitle.Text = title + $" [{detailedMessage.No}]";

            //this.labelMessage.Text = $"{detailedMessage}\n{message}" ;
            this.labelMessage.Text = $"{message}";
            label1.Text = $"Cause: {detailedMessage.cause}";
            label2.Text = $"Solution: {detailedMessage.solution}";

            // 스타일 설정 (알람이므로 기본 Red)
            SetTheme(ThemeStyle.Danger_Red);

            // 이벤트 연결
            this._ButtonSilence.Click += _ButtonSilence_Click;
            this._ButtonReset.Click += _ButtonReset_Click;
            this._ButtonClose.Click += _ButtonClose_Click;
        }

        /// <summary>
        /// 에러 제목(Enum String)을 기반으로 JSON 파일에서 원인/조치를 찾아 메시지에 덧붙입니다.
        /// </summary>
        private (int No,string cause , string solution) AppendSolutionInfo(string errorTitle )
        {
            int no = -1;
            try
            {
                // 파일 경로: 실행 폴더/CommonData/AlarmSolutions.json
                string filePath = Path.Combine(Environment.CurrentDirectory, "CommonData", "AlarmSolutions.json");

               
                string cause = "";
                string solution = "";

                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var data = JsonConvert.DeserializeObject<AlarmSolutionStorage>(json);

                    if (data != null && data.Items != null)
                    {
                        // 1. 전달받은 타이틀(String)을 Enum으로 변환
                        if (Enum.TryParse(errorTitle, out ErrorList errorEnum))
                        {
                            // 2. 리스트에서 해당 에러 코드 검색
                            var solutionData = data.Items.FirstOrDefault(x => x.ErrorCode == errorEnum);

                            no = (int)errorEnum;
                            // 3. 데이터가 존재하면 메시지 포맷팅
                            if (solutionData != null)
                            {
                                string addMsg = "";
                                if (!string.IsNullOrWhiteSpace(solutionData.Cause))
                                    cause = $"{solutionData.Cause}";

                                if (!string.IsNullOrWhiteSpace(solutionData.Solution))
                                    solution = $"{solutionData.Solution}";

                                return (no,cause, solution);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 파일 읽기 실패 시 로그만 남기고 원본 메시지 리턴 (팝업은 띄워야 하므로)
                EQ.Common.Logs.Log.Instance.Error($"[FormAlarmPopup] Solution Load Fail: {ex.Message}");
            }

            return (no,"","");
        }

        private void SetTheme(ThemeStyle style)
        {
            this._PanelTitle.ThemeStyle = style;
            this._LabelTitle.ThemeStyle = style;
        }

        // ... (기존 버튼 이벤트 핸들러들: _ButtonSilence_Click, _ButtonReset_Click, _ButtonClose_Click 유지) ...

        private void _ButtonSilence_Click(object sender, EventArgs e)
        {
            ActManager.Instance.Act.TowerLamp.SilenceBuzzer();
            _ButtonSilence.Enabled = false;
            _ButtonSilence.Text = "Muted";
        }

        private void _ButtonReset_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            this.Close();
        }

        private void _ButtonClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}