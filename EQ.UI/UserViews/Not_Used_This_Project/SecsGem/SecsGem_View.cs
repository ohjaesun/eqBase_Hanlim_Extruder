using EQ.Core.Service;
using EQ.Domain.Entities.SecsGem;
using EQ.Domain.Enums.SecsGem;
using static EQ.Core.Globals;

namespace EQ.UI.UserViews.SecsGem
{
    /// <summary>
    /// SECS/GEM 상태 모니터링 및 제어 UserControl
    /// </summary>
    public partial class SecsGem_View : UserControlBaseWithTitle
    {
        private System.Windows.Forms.Timer? _refreshTimer;

        public SecsGem_View()
        {
            InitializeComponent();
        }

        private void SecsGem_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            // CEID/ALID 목록 초기화
            LoadCEIDList();
            LoadALIDList();

            // 상태 갱신 타이머 시작
            _refreshTimer = new System.Windows.Forms.Timer();
            _refreshTimer.Interval = 500;
            _refreshTimer.Tick += RefreshTimer_Tick;
            _refreshTimer.Start();

            // 초기 상태 표시
            UpdateStatus();

            // 이벤트 구독
            SafeSubscribe(
                () => ActManager.Instance.Act.SecsGem.OnConnectionChanged += SecsGem_OnConnectionChanged,
                () => ActManager.Instance.Act.SecsGem.OnConnectionChanged -= SecsGem_OnConnectionChanged
            );

            SafeSubscribe(
                () => ActManager.Instance.Act.SecsGem.OnControlStateChanged += SecsGem_OnControlStateChanged,
                () => ActManager.Instance.Act.SecsGem.OnControlStateChanged -= SecsGem_OnControlStateChanged
            );

            // Data Tab 초기화
            InitializeDataGridViews();
            LoadDataTab();
        }

        #region Status Update
        private void RefreshTimer_Tick(object? sender, EventArgs e)
        {
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(UpdateStatus));
                return;
            }

            try
            {
                var secsGem = ActManager.Instance.Act.SecsGem;

                // 연결 상태
                bool isConnected = secsGem.IsConnected;
                _PanelStatusLed.BackColor = isConnected ? Color.LimeGreen : Color.Red;
                _LabelConnectionValue.Text = isConnected ? "Connected" : "Disconnected";
                _LabelConnectionValue.ForeColor = isConnected ? Color.Green : Color.Red;

                // Control State
                var controlState = secsGem.ControlState;
                _LabelControlStateValue.Text = controlState.ToString();
                _LabelControlStateValue.ForeColor = controlState switch
                {
                    ControlState.OnlineRemote => Color.Green,
                    ControlState.OnlineLocal => Color.Blue,
                    _ => Color.Gray
                };

                // Communicating
                bool isCommunicating = secsGem.IsCommunicating;
                _LabelCommunicatingValue.Text = isCommunicating ? "Yes" : "No";
                _LabelCommunicatingValue.ForeColor = isCommunicating ? Color.Green : Color.Red;
            }
            catch
            {
                // 초기화 전 상태
            }
        }

        private void SecsGem_OnConnectionChanged(object? sender, ConnectionChangedEventArgs e)
        {
            UpdateStatus();
        }

        private void SecsGem_OnControlStateChanged(object? sender, ControlStateChangedEventArgs e)
        {
            UpdateStatus();
        }
        #endregion

        #region Control Buttons
        private void _ButtonGoOnlineRemote_Click(object sender, EventArgs e)
        {
            ActManager.Instance.Act.SecsGem.GoOnlineRemote();
        }

        private void _ButtonGoOnlineLocal_Click(object sender, EventArgs e)
        {
            ActManager.Instance.Act.SecsGem.GoOnlineLocal();
        }

        private void _ButtonGoOffline_Click(object sender, EventArgs e)
        {
            ActManager.Instance.Act.SecsGem.GoOffline();
        }

        private void _ButtonStart_Click(object sender, EventArgs e)
        {
            int result = ActManager.Instance.Act.SecsGem.Start();
            if (result != 0)
            {
                MessageBox.Show(L("Start 실패 (Code: {0})", result), L("Error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _ButtonStop_Click(object sender, EventArgs e)
        {
            ActManager.Instance.Act.SecsGem.Stop();
        }
        #endregion

        #region CEID Management
        private void LoadCEIDList()
        {
            _ComboBoxCEID.Items.Clear();

            try
            {
                var definitions = ActManager.Instance.Act.SecsGem.GetCurrentDefinitions();
                foreach (var ceid in definitions.CEIDs)
                {
                    _ComboBoxCEID.Items.Add(new ComboBoxItem<CollectionEvent>(ceid, 
                        string.Format("[{0}] {1}", ceid.CEID, ceid.Name)));
                }

                if (_ComboBoxCEID.Items.Count > 0)
                    _ComboBoxCEID.SelectedIndex = 0;
            }
            catch
            {
                // 초기화 전 상태
            }
        }

        private void _ButtonSendCEID_Click(object sender, EventArgs e)
        {
            if (_ComboBoxCEID.SelectedItem is ComboBoxItem<CollectionEvent> item)
            {
                int result = ActManager.Instance.Act.SecsGem.SendEvent(item.Value.CEID);
                if (result != 0)
                {
                    MessageBox.Show(L("CEID 전송 실패 (Code: {0})", result), L("Error"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region ALID Management
        private void LoadALIDList()
        {
            _ComboBoxALID.Items.Clear();

            try
            {
                var definitions = ActManager.Instance.Act.SecsGem.GetCurrentDefinitions();
                foreach (var alid in definitions.ALIDs)
                {
                    _ComboBoxALID.Items.Add(new ComboBoxItem<AlarmDefinition>(alid,
                        string.Format("[{0}] {1}", alid.ALID, alid.AlarmText)));
                }

                if (_ComboBoxALID.Items.Count > 0)
                    _ComboBoxALID.SelectedIndex = 0;
            }
            catch
            {
                // 초기화 전 상태
            }
        }

        private void _ButtonAlarmSet_Click(object sender, EventArgs e)
        {
            if (_ComboBoxALID.SelectedItem is ComboBoxItem<AlarmDefinition> item)
            {
                int result = ActManager.Instance.Act.SecsGem.SendAlarm(item.Value.ALID, true);
                if (result != 0)
                {
                    MessageBox.Show(L("Alarm Set 실패 (Code: {0})", result), L("Error"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void _ButtonAlarmClear_Click(object sender, EventArgs e)
        {
            if (_ComboBoxALID.SelectedItem is ComboBoxItem<AlarmDefinition> item)
            {
                int result = ActManager.Instance.Act.SecsGem.SendAlarm(item.Value.ALID, false);
                if (result != 0)
                {
                    MessageBox.Show(L("Alarm Clear 실패 (Code: {0})", result), L("Error"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #region Data Tab Management
        /// <summary>
        /// DataGridView 컬럼 초기화
        /// </summary>
        private void InitializeDataGridViews()
        {
            // SVID DataGridView 컬럼 설정
            _DataGridViewSVID.Columns.Clear();
            _DataGridViewSVID.Columns.Add("SVID", "SVID");
            _DataGridViewSVID.Columns.Add("Name", L("이름"));
            _DataGridViewSVID.Columns.Add("Format", L("포맷"));
            _DataGridViewSVID.Columns.Add("Unit", L("단위"));
            _DataGridViewSVID.Columns.Add("Value", L("값"));
            _DataGridViewSVID.Columns.Add("Description", L("설명"));

            // SVID 컬럼 속성 설정
            _DataGridViewSVID.Columns["SVID"].Width = 60;
            _DataGridViewSVID.Columns["SVID"].ReadOnly = true;
            _DataGridViewSVID.Columns["Name"].ReadOnly = true;
            _DataGridViewSVID.Columns["Format"].Width = 60;
            _DataGridViewSVID.Columns["Format"].ReadOnly = true;
            _DataGridViewSVID.Columns["Unit"].Width = 60;
            _DataGridViewSVID.Columns["Unit"].ReadOnly = true;
            _DataGridViewSVID.Columns["Value"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _DataGridViewSVID.Columns["Description"].Width = 150;
            _DataGridViewSVID.Columns["Description"].ReadOnly = true;

            // ECID DataGridView 컬럼 설정
            _DataGridViewECID.Columns.Clear();
            _DataGridViewECID.Columns.Add("ECID", "ECID");
            _DataGridViewECID.Columns.Add("Name", L("이름"));
            _DataGridViewECID.Columns.Add("Format", L("포맷"));
            _DataGridViewECID.Columns.Add("Unit", L("단위"));
            _DataGridViewECID.Columns.Add("Value", L("값"));
            _DataGridViewECID.Columns.Add("MinValue", L("최소값"));
            _DataGridViewECID.Columns.Add("MaxValue", L("최대값"));
            _DataGridViewECID.Columns.Add("Description", L("설명"));

            // ECID 컬럼 속성 설정
            _DataGridViewECID.Columns["ECID"].Width = 60;
            _DataGridViewECID.Columns["ECID"].ReadOnly = true;
            _DataGridViewECID.Columns["Name"].ReadOnly = true;
            _DataGridViewECID.Columns["Format"].Width = 60;
            _DataGridViewECID.Columns["Format"].ReadOnly = true;
            _DataGridViewECID.Columns["Unit"].Width = 60;
            _DataGridViewECID.Columns["Unit"].ReadOnly = true;
            _DataGridViewECID.Columns["Value"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _DataGridViewECID.Columns["MinValue"].Width = 70;
            _DataGridViewECID.Columns["MinValue"].ReadOnly = true;
            _DataGridViewECID.Columns["MaxValue"].Width = 70;
            _DataGridViewECID.Columns["MaxValue"].ReadOnly = true;
            _DataGridViewECID.Columns["Description"].Width = 120;
            _DataGridViewECID.Columns["Description"].ReadOnly = true;
        }

        /// <summary>
        /// Data 탭 데이터 로드
        /// </summary>
        private void LoadDataTab()
        {
            LoadSVIDData();
            LoadECIDData();
        }

        /// <summary>
        /// SVID 데이터 로드
        /// </summary>
        private void LoadSVIDData()
        {
            _DataGridViewSVID.Rows.Clear();

            try
            {
                var definitions = ActManager.Instance.Act.SecsGem.GetCurrentDefinitions();
                foreach (var sv in definitions.SVIDs)
                {
                    _DataGridViewSVID.Rows.Add(
                        sv.SVID,
                        sv.Name,
                        sv.Format,
                        sv.Unit,
                        sv.Value,
                        sv.Description
                    );
                }
            }
            catch
            {
                // 초기화 전 상태
            }
        }

        /// <summary>
        /// ECID 데이터 로드
        /// </summary>
        private void LoadECIDData()
        {
            _DataGridViewECID.Rows.Clear();

            try
            {
                var definitions = ActManager.Instance.Act.SecsGem.GetCurrentDefinitions();
                foreach (var ec in definitions.ECIDs)
                {
                    _DataGridViewECID.Rows.Add(
                        ec.ECID,
                        ec.Name,
                        ec.Format,
                        ec.Unit,
                        ec.Value,
                        ec.MinValue,
                        ec.MaxValue,
                        ec.Description
                    );
                }
            }
            catch
            {
                // 초기화 전 상태
            }
        }

        private void _ButtonRefreshData_Click(object sender, EventArgs e)
        {
            LoadDataTab();
        }

        private void _ButtonSaveData_Click(object sender, EventArgs e)
        {
            try
            {
                // SVID 값 업데이트
                foreach (DataGridViewRow row in _DataGridViewSVID.Rows)
                {
                    if (row.Cells["SVID"].Value != null)
                    {
                        int svid = Convert.ToInt32(row.Cells["SVID"].Value);
                        string value = row.Cells["Value"].Value?.ToString() ?? "";
                        ActManager.Instance.Act.SecsGem.UpdateSVIDValue(svid, value);
                    }
                }

                // ECID 값 업데이트
                foreach (DataGridViewRow row in _DataGridViewECID.Rows)
                {
                    if (row.Cells["ECID"].Value != null)
                    {
                        int ecid = Convert.ToInt32(row.Cells["ECID"].Value);
                        string value = row.Cells["Value"].Value?.ToString() ?? "";
                        ActManager.Instance.Act.SecsGem.UpdateECValue(ecid, value);
                    }
                }

                // 정의 파일로 저장
                ActManager.Instance.Act.SecsGem.SaveDefinitionsToFile(Application.StartupPath);

                MessageBox.Show(L("저장되었습니다."), L("Info"),
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(L("저장 실패: {0}", ex.Message), L("Error"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

    /// <summary>
    /// ComboBox 아이템 래퍼
    /// </summary>
    internal class ComboBoxItem<T>
    {
        public T Value { get; }
        public string DisplayText { get; }

        public ComboBoxItem(T value, string displayText)
        {
            Value = value;
            DisplayText = displayText;
        }

        public override string ToString() => DisplayText;
    }
}
