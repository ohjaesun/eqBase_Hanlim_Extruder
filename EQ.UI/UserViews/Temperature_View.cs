using EQ.Common.Logs;
using EQ.Core.Service;
using EQ.Domain.Enums;
using EQ.Domain.Interface;
using EQ.UI.Controls;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EQ.UI.UserViews
{
    public partial class Temperature_View : UserControlBase
    {
        private DataTable _dt;
        private bool _isUpdating = false; // 중복 업데이트 방지

        public Temperature_View()
        {
            InitializeComponent();
        }

        private void Temperature_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            _LabelTitle.Text = "Temperature Control Monitor";
            _ButtonSave.Visible = false; // 저장 버튼 숨김

            InitGrid();
            LoadZones();

            _updateTimer.Start();
            this.Disposed += (s, ev) => _updateTimer.Stop();
        }

        private void InitGrid()
        {
            _dt = new DataTable();
            _dt.Columns.Add("Zone", typeof(string));      // Zone 이름 (Enum)
            _dt.Columns.Add("PV", typeof(string));        // 현재 온도
            _dt.Columns.Add("SV_Read", typeof(string));   // 설정된 온도 (읽기값)
            _dt.Columns.Add("Status", typeof(string));    // Run/Stop 상태

            // DataTable을 소스로 쓰지만 버튼 컬럼은 따로 추가
            _GridTemp.DataSource = _dt;

            // --- 컬럼 스타일 설정 ---
            _GridTemp.Columns["Zone"].ReadOnly = true;
            _GridTemp.Columns["Zone"].Width = 150;

            _GridTemp.Columns["PV"].ReadOnly = true;
            _GridTemp.Columns["PV"].DefaultCellStyle.Font = new Font("D2Coding", 14F, FontStyle.Bold);
            _GridTemp.Columns["PV"].DefaultCellStyle.ForeColor = Color.Lime; // PV 강조

            _GridTemp.Columns["SV_Read"].ReadOnly = true;
            _GridTemp.Columns["Status"].ReadOnly = true;

            // --- 버튼 컬럼 추가 (SV 설정, Run/Stop 제어) ---
            AddButtonColumn("Btn_SetSV", "Set SV", "Change");
            AddButtonColumn("Btn_RunStop", "Control", "Run/Stop");

            // 정렬 금지
            foreach (DataGridViewColumn col in _GridTemp.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void AddButtonColumn(string name, string header, string text)
        {
            if (_GridTemp.Columns.Contains(name)) return;

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = name;
            btn.HeaderText = header;
            btn.Text = text;
            btn.UseColumnTextForButtonValue = true;
            btn.FlatStyle = FlatStyle.Popup;
            btn.DefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219); // Info_Sky
            btn.DefaultCellStyle.ForeColor = Color.White;
            _GridTemp.Columns.Add(btn);
        }

        /// <summary>
        /// ActTemperature에 등록된 컨트롤러들을 그리드에 추가
        /// </summary>
        private void LoadZones()
        {
            var actTemp = ActManager.Instance.Act.Temp;
            _dt.Rows.Clear();

            // TempZone Enum을 순회
            foreach (TempID zone in Enum.GetValues(typeof(TempID)))
            {
                var ctrl = actTemp.Get(zone);
                if (ctrl != null)
                {
                    _dt.Rows.Add(zone.ToString(), "0.0", "0.0", "Unknown");
                }
            }
        }

        /// <summary>
        /// 주기적 상태 업데이트 (비동기 처리로 UI 프리징 방지)
        /// </summary>
        private async void _updateTimer_Tick(object sender, EventArgs e)
        {
            // 이전 업데이트가 아직 안 끝났거나, 화면이 안 보이면 스킵
            if (_isUpdating || !this.Visible) return;

            _isUpdating = true;

            try
            {
                var actTemp = ActManager.Instance.Act.Temp;

                // 1. [Background] 데이터 읽기 (통신)
                // UI 스레드가 멈추지 않도록 Task.Run 사용
                var updates = await Task.Run(() =>
                {
                    var results = new System.Collections.Generic.List<(string Zone, double PV, double SV, bool Run)>();

                    foreach (DataRow row in _dt.Rows)
                    {
                        string zoneName = row["Zone"].ToString();
                        // Enum 변환
                        if (Enum.TryParse(zoneName, out TempID zone))
                        {
                            var ctrl = actTemp.Get(zone);
                            if (ctrl != null)
                            {
                                // ※ 통신 에러 시 0 또는 예외가 발생할 수 있음 (Controller 내부 로그 확인)
                                double pv = ctrl.ReadPV();
                                double sv = ctrl.ReadSV();
                                bool isRun = ctrl.IsRunning();
                                results.Add((zoneName, pv, sv, isRun));
                            }
                        }
                    }
                    return results;
                });

                // 2. [UI Thread] 그리드 업데이트
                foreach (var data in updates)
                {
                    // Zone 이름으로 행 찾기
                    DataRow[] rows = _dt.Select($"Zone = '{data.Zone}'");
                    if (rows.Length > 0)
                    {
                        rows[0]["PV"] = data.PV.ToString("F1");
                        rows[0]["SV_Read"] = data.SV.ToString("F1");
                        rows[0]["Status"] = data.Run ? "RUN" : "STOP";
                    }
                }

                // 상태에 따른 스타일 업데이트 (Run이면 초록색, Stop이면 회색 등)
                UpdateGridStyle();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"[TempView] Update Error: {ex.Message}");
            }
            finally
            {
                _isUpdating = false;
            }
        }

        private void UpdateGridStyle()
        {
            foreach (DataGridViewRow row in _GridTemp.Rows)
            {
                var statusCell = row.Cells["Status"];
                var runStopBtn = row.Cells["Btn_RunStop"];

                if (statusCell.Value?.ToString() == "RUN")
                {
                //    statusCell.Style.ForeColor = Color.Lime;
                    runStopBtn.Style.BackColor = Color.Lime; // Stop 버튼(Red)
                    runStopBtn.Value = "STOP";
                }
                else
                {
                    //     statusCell.Style.ForeColor = Color.Silver;
                    runStopBtn.Style.BackColor = Color.Gray;//FromArgb(46, 204, 113); // Run 버튼(Green)
                    runStopBtn.Value = "RUN";
                }
            }

        }

        /// <summary>
        /// 버튼 클릭 이벤트 (설정 변경)
        /// </summary>
        private async void _GridTemp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string colName = _GridTemp.Columns[e.ColumnIndex].Name;
            string zoneName = _GridTemp.Rows[e.RowIndex].Cells["Zone"].Value.ToString();

            if (!Enum.TryParse(zoneName, out TempID zone)) return;
            var ctrl = ActManager.Instance.Act.Temp.Get(zone);
            if (ctrl == null) return;

            // [1] SV 설정 버튼
            if (colName == "Btn_SetSV")
            {
                // 간단한 입력 팝업 (Microsoft.VisualBasic 참조가 없으면 커스텀 폼 사용 필요)
                // 여기서는 간단히 TextBox를 가진 커스텀 팝업을 띄운다고 가정하거나
                // ActUserOption처럼 PropertyGrid 방식을 쓸 수도 있지만,
                // 가장 쉬운 방법은 InputBox 구현입니다.

                string input = Microsoft.VisualBasic.Interaction.InputBox(
                    $"Enter new SV for {zoneName}", "Set Temperature", "0");

                if (double.TryParse(input, out double newSv))
                {
                    var confirm = await ActManager.Instance.Act.PopupYesNo.ConfirmAsync(
                        "Temperature Set",
                        $"[{zoneName}] 설정 온도를 {newSv}도로 변경하시겠습니까?",
                        Domain.Enums.NotifyType.Info);

                    if (confirm == Domain.Enums.YesNoResult.Yes)
                    {
                        // 통신 수행 (비동기 래핑 권장)
                        await Task.Run(() => ctrl.WriteSV(newSv));
                        Log.Instance.Info($"[{zoneName}] SV Changed to {newSv} by User.");
                    }
                }
            }
            // [2] Run/Stop 제어 버튼
            else if (colName == "Btn_RunStop")
            {
                bool isCurrentlyRun = _GridTemp.Rows[e.RowIndex].Cells["Status"].Value?.ToString() == "RUN";
                bool nextState = !isCurrentlyRun;
                string nextStateStr = nextState ? "RUN" : "STOP";

                var confirm = await ActManager.Instance.Act.PopupYesNo.ConfirmAsync(
                       "Control",
                       $"[{zoneName}] 장비를 {nextStateStr} 하시겠습니까?",
                       Domain.Enums.NotifyType.Warning);

                if (confirm == Domain.Enums.YesNoResult.Yes)
                {
                    await Task.Run(() => ctrl.SetRun(nextState));
                    Log.Instance.Info($"[{zoneName}] Control set to {nextStateStr} by User.");
                }
            }
        }
    }
}