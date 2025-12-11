using EQ.Core.Service;
using EQ.Domain.Entities.LaserMeasure;
using EQ.Domain.Enums.LaserMeasure;
using static EQ.Core.Globals;

namespace EQ.UI.UserViews.LaserMeasure
{
    /// <summary>
    /// 레이저 계측기 모니터링 및 제어 UserControl
    /// </summary>
    public partial class LaserMeasure_View : UserControlBaseWithTitle
    {
        private System.Windows.Forms.Timer? _refreshTimer;

        public LaserMeasure_View()
        {
            InitializeComponent();
        }

        private void LaserMeasure_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            // DataGridView 컬럼 설정
            InitializeDataGridView();

            // 데이터 로드
            LoadLaserData();

            // 상태 갱신 타이머 시작
            _refreshTimer = new System.Windows.Forms.Timer();
            _refreshTimer.Interval = 500;
            _refreshTimer.Tick += RefreshTimer_Tick;
            _refreshTimer.Start();

            // 이벤트 구독
            SafeSubscribe(
                () => ActManager.Instance.Act.LaserMeasure.OnMeasured += LaserMeasure_OnMeasured,
                () => ActManager.Instance.Act.LaserMeasure.OnMeasured -= LaserMeasure_OnMeasured
            );
        }

        #region DataGridView
        private void InitializeDataGridView()
        {
            _DataGridViewLasers.Columns.Clear();
            _DataGridViewLasers.Columns.Add("Id", "ID");
            _DataGridViewLasers.Columns.Add("Name", L("이름"));
            _DataGridViewLasers.Columns.Add("Type", L("타입"));
            _DataGridViewLasers.Columns.Add("Connected", L("연결"));
            _DataGridViewLasers.Columns.Add("Value", L("측정값 (mm)"));
            _DataGridViewLasers.Columns.Add("Continuous", L("연속측정"));
            _DataGridViewLasers.Columns.Add("SupportsContinuous", L("연속지원"));

            _DataGridViewLasers.Columns["Id"].Width = 60;
            _DataGridViewLasers.Columns["Connected"].Width = 60;
            _DataGridViewLasers.Columns["Value"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _DataGridViewLasers.Columns["Continuous"].Width = 80;
            _DataGridViewLasers.Columns["SupportsContinuous"].Width = 80;
        }

        private void LoadLaserData()
        {
            _DataGridViewLasers.Rows.Clear();

            try
            {
                var laserMeasure = ActManager.Instance.Act.LaserMeasure;
                foreach (var id in laserMeasure.GetRegisteredIds())
                {
                    var config = laserMeasure.GetConfig(id);
                    if (config == null) continue;

                    bool connected = laserMeasure.IsConnected(id);
                    double value = laserMeasure.GetLastValue(id);
                    bool running = laserMeasure.IsContinuousRunning(id);
                    bool supportsContinuous = laserMeasure.SupportsContinuous(id);

                    int rowIndex = _DataGridViewLasers.Rows.Add(
                        id.ToString(),
                        config.Name,
                        config.Type.ToString(),
                        connected ? "●" : "○",
                        value.ToString("F3"),
                        running ? L("실행중") : "-",
                        supportsContinuous ? "O" : "X"
                    );

                    // 연결 상태에 따라 색상 변경
                    var row = _DataGridViewLasers.Rows[rowIndex];
                    row.Cells["Connected"].Style.ForeColor = connected ? Color.Green : Color.Gray;
                }

                UpdateStatusLed();
            }
            catch
            {
                // 초기화 전 상태
            }
        }
        #endregion

        #region Event Handlers
        private void RefreshTimer_Tick(object? sender, EventArgs e)
        {
            UpdateValues();
        }

        private void UpdateValues()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(UpdateValues));
                return;
            }

            try
            {
                var laserMeasure = ActManager.Instance.Act.LaserMeasure;
                foreach (DataGridViewRow row in _DataGridViewLasers.Rows)
                {
                    if (row.Cells["Id"].Value == null) continue;

                    if (Enum.TryParse<LaserMeasureId>(row.Cells["Id"].Value.ToString(), out var id))
                    {
                        bool connected = laserMeasure.IsConnected(id);
                        double value = laserMeasure.GetLastValue(id);
                        bool running = laserMeasure.IsContinuousRunning(id);

                        row.Cells["Connected"].Value = connected ? "●" : "○";
                        row.Cells["Connected"].Style.ForeColor = connected ? Color.Green : Color.Gray;
                        row.Cells["Value"].Value = value.ToString("F3");
                        row.Cells["Continuous"].Value = running ? L("실행중") : "-";
                    }
                }

                UpdateStatusLed();
            }
            catch
            {
                // 예외 무시
            }
        }

        private void UpdateStatusLed()
        {
            try
            {
                var laserMeasure = ActManager.Instance.Act.LaserMeasure;
                bool anyConnected = laserMeasure.GetRegisteredIds().Any(id => laserMeasure.IsConnected(id));

                _PanelStatusLed.BackColor = anyConnected ? Color.LimeGreen : Color.Gray;
                _LabelStatus.Text = anyConnected ? L("연결됨") : L("연결 없음");
            }
            catch
            {
                _PanelStatusLed.BackColor = Color.Gray;
                _LabelStatus.Text = L("초기화 전");
            }
        }

        private void LaserMeasure_OnMeasured(object? sender, (LaserMeasureId Id, LaserMeasureEventArgs Args) e)
        {
            // 이벤트 발생 시 UI 업데이트
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => UpdateSingleRow(e.Id, e.Args)));
            }
            else
            {
                UpdateSingleRow(e.Id, e.Args);
            }
        }

        private void UpdateSingleRow(LaserMeasureId id, LaserMeasureEventArgs args)
        {
            foreach (DataGridViewRow row in _DataGridViewLasers.Rows)
            {
                if (row.Cells["Id"].Value?.ToString() == id.ToString())
                {
                    row.Cells["Value"].Value = args.Value.ToString("F3");
                    if (args.IsError)
                    {
                        row.Cells["Value"].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        row.Cells["Value"].Style.ForeColor = Color.Black;
                    }
                    break;
                }
            }
        }
        #endregion

        #region Button Events
        private void _ButtonRefresh_Click(object sender, EventArgs e)
        {
            LoadLaserData();
        }

        private async void _ButtonStartContinuous_Click(object sender, EventArgs e)
        {
            if (_DataGridViewLasers.SelectedRows.Count == 0)
            {
                MessageBox.Show(L("채널을 선택하세요."), L("Info"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = _DataGridViewLasers.SelectedRows[0];
            if (Enum.TryParse<LaserMeasureId>(selectedRow.Cells["Id"].Value?.ToString(), out var id))
            {
                try
                {
                    ActManager.Instance.Act.LaserMeasure.StartContinuous(id, 0, 100);
                }
                catch (NotSupportedException)
                {
                    MessageBox.Show(L("선택한 계측기는 연속 측정을 지원하지 않습니다."), L("Warning"),
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(L("연속 측정 시작 실패: {0}", ex.Message), L("Error"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void _ButtonStopContinuous_Click(object sender, EventArgs e)
        {
            if (_DataGridViewLasers.SelectedRows.Count == 0)
            {
                MessageBox.Show(L("채널을 선택하세요."), L("Info"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = _DataGridViewLasers.SelectedRows[0];
            if (Enum.TryParse<LaserMeasureId>(selectedRow.Cells["Id"].Value?.ToString(), out var id))
            {
                ActManager.Instance.Act.LaserMeasure.StopContinuous(id);
            }
        }
        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _refreshTimer?.Stop();
                _refreshTimer?.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion
    }
}
