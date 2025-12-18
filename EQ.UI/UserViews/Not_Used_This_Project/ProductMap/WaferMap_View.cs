using EQ.Core.Act.Composition;
using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.UI.Controls;
using System;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace EQ.UI.UserViews
{
    public partial class WaferMap_View : ProductMap_ViewBase
    {
        private Timer _uiUpdateTimer;
        private ProductMap<WaferCell> _localMap;

        public WaferMap_View()
        {
            InitializeComponent();
        }

        private ActProduct<WaferCell> _actWafer => ActManager.Instance.Act.Wafer;
        private ActMagazine<WaferCell> _actMagazine => ActManager.Instance.Act.WaferMagazine;

        private void WaferMap_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            // 1. 매거진 목록 초기화
            InitMagazineList();

            // 2. 초기 데이터 로드 (매거진 선택이 안 된 경우 기본 로드)
            if (_comboMagazine.SelectedItem == null)
                LoadMapData();

            // 3. 화면 맞춤
            FitToScreen();

            // 4. 자동 갱신 타이머 설정
            _uiUpdateTimer = new Timer();
            _uiUpdateTimer.Interval = 300;
            _uiUpdateTimer.Tick += (s, ev) => RefreshView();
            _uiUpdateTimer.Start();

            this.Disposed += (s, ev) => _uiUpdateTimer?.Stop();
        }

        // [매거진 콤보박스 초기화]
        private void InitMagazineList()
        {
            _comboMagazine.Items.Clear();

            // 등록된 매거진 확인 후 추가
            foreach (MagazineName name in Enum.GetValues(typeof(MagazineName)))
            {
                if (name == MagazineName.None) continue;

                var mag = _actMagazine.GetMagazine(name);
                if (mag != null)
                {
                    _comboMagazine.Items.Add(name);
                }
            }

            if (_comboMagazine.Items.Count > 0)
            {
                _panelTopControl.Visible = true;
                _comboMagazine.SelectedIndex = 0; // 첫 번째 자동 선택
            }
            else
            {
                _panelTopControl.Visible = false;
            }
        }

        // [매거진 변경 이벤트]
        private void _comboMagazine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_comboMagazine.SelectedItem is MagazineName selectedName)
            {
                // 해당 매거진의 슬롯 목록 갱신
                InitSlotList(selectedName);
                LoadMapData();
                RefreshView();
            }
        }

        // [슬롯 목록 초기화]
        private void InitSlotList(MagazineName magName)
        {
            _comboSlot.Items.Clear();

            var mag = _actMagazine.GetMagazine(magName);
            if (mag != null)
            {
                for (int i = 0; i < mag.Capacity; i++)
                {
                    _comboSlot.Items.Add($"Slot {i + 1}");
                }

                if (mag.Capacity > 0)
                    _comboSlot.SelectedIndex = 0;
            }
        }

        private void _comboSlot_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMapData();
            RefreshView();
        }

        private void _btnRefresh_Click(object sender, EventArgs e)
        {
            LoadMapData();
            RefreshView();
        }

        private void LoadMapData()
        {
            // 매거진 모드
            if (_panelTopControl.Visible &&
                _comboMagazine.SelectedItem is MagazineName magName &&
                _comboSlot.SelectedIndex >= 0)
            {
                int slotIdx = _comboSlot.SelectedIndex;
                var mag = _actMagazine.GetMagazine(magName);
                if (mag != null)
                {
                    var map = mag.GetSlot(slotIdx);
                    if (map != null)
                    {
                        _localMap = map;
                        return;
                    }
                }
            }

            // 기본 모드 (단일 맵)
            _localMap = _actWafer.CurrentMap;
        }

        // --- ProductMap_ViewBase 구현 ---

        protected override int GetCols() => _localMap?.Cols ?? 0;
        protected override int GetRows() => _localMap?.Rows ?? 0;

        protected override ProductUnitChipGrade GetChipGrade(int x, int y)
        {
            if (_localMap == null) return ProductUnitChipGrade.None;
            return _localMap[x, y].Grade;
        }

        protected override void OnChipClick(int x, int y, MouseButtons btn)
        {
            if (_localMap == null) return;
            ref WaferCell cell = ref _localMap[x, y];

            if (btn == MouseButtons.Left)
            {
                cell.Grade = ProductUnitChipGrade.Fail;
                RefreshView();
            }
            else if (btn == MouseButtons.Right)
            {
                string msg = $"[Wafer {x},{y}] ID: {cell.ChipID}, Grade: {cell.Grade}, Bin: {cell.BinCode}";
                MessageBox.Show(msg);
            }
        }
    }
}