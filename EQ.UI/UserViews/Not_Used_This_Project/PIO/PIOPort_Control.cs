using EQ.Core;
using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Enums;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EQ.UI.UserViews.Setup.Components
{
    public partial class PIOPort_Control : UserControl
    {
        private PIOId _pioId;
        private Dictionary<PIOSignal, Label> _signalLeds = new Dictionary<PIOSignal, Label>();

        public PIOPort_Control()
        {
            InitializeComponent();
        }

        public void Initialize(PIOId id)
        {
            _pioId = id;
            this.grpBox.Text = $"{Globals.L("Port")}: {id}";
            this.lblStateTitle.Text = Globals.L("State");
            this.btnLoad.Text = Globals.L("Load Sequence");
            this.lblTxHeader.Text = "Tx (Output)";
            this.lblRxHeader.Text = "Rx (Input)";

            InitSignalLeds();
        }

        private void InitSignalLeds()
        {
            // 1. Tx (Output) 8점 정의 (순서대로 배치)
            var txSignals = new List<PIOSignal> {
                PIOSignal.L_REQ,
                PIOSignal.U_REQ,
                PIOSignal.READY,
                PIOSignal.HO_AVBL,
                PIOSignal.ES,
                PIOSignal.RES_OUT_5,
                PIOSignal.RES_OUT_6,
                PIOSignal.RES_OUT_7
            };

            // 2. Rx (Input) 8점 정의 (순서대로 배치)
            var rxSignals = new List<PIOSignal> {
                PIOSignal.VALID,
                PIOSignal.CS_0,
                PIOSignal.CS_1,
                PIOSignal.TR_REQ,
                PIOSignal.BUSY,
                PIOSignal.COMPT,
                PIOSignal.CONT,
                PIOSignal.AM_AVBL
            };

            // 기존 컨트롤 초기화 (헤더 0,1행 제외하고 제거 or 전체 클리어 후 재생성)
            // 여기서는 안전하게 레이아웃을 초기화
            tableLayoutPanelSignals.Controls.Clear();
            tableLayoutPanelSignals.RowStyles.Clear();

            // 헤더 다시 추가
            tableLayoutPanelSignals.Controls.Add(lblTxHeader, 0, 0);
            tableLayoutPanelSignals.Controls.Add(lblRxHeader, 1, 0);

            // 8비트 + 헤더(1행) = 총 9행
            int totalRows = 9;
            tableLayoutPanelSignals.RowCount = totalRows;

            // Row Style 설정 (균등 분할)
            float rowHeight = 100f / totalRows;
            for (int i = 0; i < totalRows; i++)
            {
                tableLayoutPanelSignals.RowStyles.Add(new RowStyle(SizeType.Percent, rowHeight));
            }

            // LED 생성 및 배치
            for (int i = 0; i < 8; i++)
            {
                int rowIndex = i + 1; // 헤더 다음부터 시작

                // Tx LED 추가 (Column 0)
                if (i < txSignals.Count)
                    AddLedToGrid(txSignals[i], 0, rowIndex);

                // Rx LED 추가 (Column 1)
                if (i < rxSignals.Count)
                    AddLedToGrid(rxSignals[i], 1, rowIndex);
            }
        }

        private void AddLedToGrid(PIOSignal sig, int col, int row)
        {
            Label led = new Label();

            // 예약 신호는 조금 흐리게 표시하거나 텍스트를 간소화할 수 있음
            string text = sig.ToString();
            if (text.StartsWith("RES_"))
            {
                text = "Reserved";
                led.ForeColor = Color.Gray;
            }
            else
            {
                led.ForeColor = Color.Black;
            }

            led.Text = text;
            led.TextAlign = ContentAlignment.MiddleCenter;
            led.Dock = DockStyle.Fill;
            led.Margin = new Padding(2);
            led.BackColor = Color.WhiteSmoke; // OFF Color
            led.BorderStyle = BorderStyle.FixedSingle;
            led.Font = new Font("D2Coding", 9F);

            // 신호 ID를 키로 저장
            _signalLeds[sig] = led;
            tableLayoutPanelSignals.Controls.Add(led, col, row);
        }

        public void UpdateUI()
        {
            if (DesignMode) return;
            var actPIO = ActManager.Instance.Act.PIO;
            if (actPIO == null) return;

            // 1. 상태 업데이트
            var state = actPIO.GetCurrentState(_pioId);
            lblStateValue.Text = state.ToString();
            lblStateValue.ForeColor = Color.Black;
            lblStateValue.BackColor = (state == PIOState.Idle) ? Color.White : Color.Lime;

            // 2. 신호 LED 업데이트
            foreach (var kvp in _signalLeds)
            {
                // 실제 IO 컨트롤러에서 해당 신호 값을 가져옴
                bool isOn = actPIO.GetSignal(_pioId, kvp.Key);
                UpdateLedColor(kvp.Value, isOn);
            }
        }

        private void UpdateLedColor(Label led, bool isOn)
        {
            // Reserved 핀이라도 신호가 들어오면 표시 (디버깅용)
            if (isOn)
            {
                led.BackColor = Color.LimeGreen;
                led.Font = new Font(led.Font, FontStyle.Bold);
            }
            else
            {
                led.BackColor = Color.WhiteSmoke;
                led.Font = new Font(led.Font, FontStyle.Regular);
            }
        }

        private async void btnLoad_Click(object sender, EventArgs e)
        {
            var confirm = await ActManager.Instance.Act.PopupYesNo.ConfirmAsync(
                Globals.L("Start Load"),
                Globals.L("Start loading sequence for {0}?", _pioId));

            if (confirm == YesNoResult.Yes)
            {
                await RunLoadAction();
            }
        }

        private async Task RunLoadAction()
        {
            try
            {
                btnLoad.Enabled = false;
                var result = await ActManager.Instance.Act.PIO.LoadReqAsync(_pioId);

                if (result == ActionStatus.Finished)
                {
                    ActManager.Instance.Act.PopupNoti(Globals.L("Load Finished"), NotifyType.Info);
                }
                else
                {
                    ActManager.Instance.Act.PopupNoti(Globals.L("Load Error"), NotifyType.Error);
                }
            }
            catch (Exception ex)
            {
                ActManager.Instance.Act.PopupAlarm(ErrorList.ACT_ERROR, ex.Message);
            }
            finally
            {
                btnLoad.Enabled = true;
            }
        }
    }
}