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
    public partial class TrayMap_View : ProductMap_ViewBase
    {
        private Timer _uiUpdateTimer;
        private ProductMap<TrayCell> _localMap;

        public TrayMap_View()
        {
            InitializeComponent();
        }

        private ActProduct<TrayCell> _actTray => ActManager.Instance.Act.Tray;
        private ActMagazine<TrayCell> _actMagazine => ActManager.Instance.Act.TrayMagazine;

        private void TrayMap_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            InitMagazineList();

            if (_comboMagazine.SelectedItem == null)
                LoadMapData();

            FitToScreen();

            _uiUpdateTimer = new Timer();
            _uiUpdateTimer.Interval = 300;
            _uiUpdateTimer.Tick += (s, ev) => RefreshView();
            _uiUpdateTimer.Start();

            this.Disposed += (s, ev) => _uiUpdateTimer?.Stop();
        }

        private void InitMagazineList()
        {
            _comboMagazine.Items.Clear();

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
                _comboMagazine.SelectedIndex = 0;
            }
            else
            {
                _panelTopControl.Visible = false;
            }
        }

        private void _comboMagazine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_comboMagazine.SelectedItem is MagazineName selectedName)
            {
                InitSlotList(selectedName);
                LoadMapData();
                RefreshView();
            }
        }

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

            // 단일 모드
            _localMap = _actTray.CurrentMap;
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
            ref TrayCell cell = ref _localMap[x, y];

            if (btn == MouseButtons.Left)
            {
                cell.Grade = ProductUnitChipGrade.Fail;
                RefreshView();
            }
            else if (btn == MouseButtons.Right)
            {
                float temp = cell.Temperatures[0];
                string msg = $"[Tray {x},{y}] ID: {cell.ID}, Grade: {cell.Grade}, Temp: {temp:F1}°C";
                MessageBox.Show(msg);
            }
        }
    }
}