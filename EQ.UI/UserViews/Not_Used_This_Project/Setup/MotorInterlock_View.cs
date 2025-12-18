using EQ.Common.Logs;
using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.UI.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace EQ.UI.UserViews
{
    public partial class MotorInterlock_View : UserControlBase
    {
        private DataTable _dt;

        public MotorInterlock_View()
        {
            InitializeComponent();
        }

        private void MotorInterlock_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            // UserControlBase 설정
            _LabelTitle.Text = "Motor Motion Interlock";
            _ButtonSave.Visible = true;
            _ButtonSave.Click += _ButtonSave_Click;

            InitControls();
            RefreshTargetList();
        }

        /// <summary>
        /// 콤보박스 및 그리드 초기화
        /// </summary>
        private void InitControls()
        {
            // 1. Position Condition 관련 초기화
            _ComboSourceAxis.DataSource = Enum.GetValues(typeof(MotionID));
            _ComboCondition.DataSource = Enum.GetValues(typeof(CompareCondition));
            _ComboPosDir.DataSource = Enum.GetValues(typeof(StopDirection));

            // 2. IO Condition 관련 초기화
            _ComboIOType.Items.Clear();
            _ComboIOType.Items.Add("Input");
            _ComboIOType.Items.Add("Output");
            _ComboIOType.SelectedIndex = 0; // Default: Input
            _ComboIOType.SelectedIndexChanged += (s, e) => RefreshIOList();

            _ComboIOSignal.Items.Clear();
            _ComboIOSignal.Items.Add("ON");
            _ComboIOSignal.Items.Add("OFF");
            _ComboIOSignal.SelectedIndex = 0; // Default: ON

            _ComboIODir.DataSource = Enum.GetValues(typeof(StopDirection));

            RefreshIOList(); // IO 목록 채우기

            // 3. 그리드 초기화
            _dt = new DataTable();
            _dt.Columns.Add("Object", typeof(object)); // 실제 엔티티 객체 (Hidden)
            _dt.Columns.Add("Target", typeof(string));
            _dt.Columns.Add("Type", typeof(string));
            _dt.Columns.Add("Rule", typeof(string));
            _dt.Columns.Add("Stop", typeof(string));
            _dt.Columns.Add("Desc", typeof(string));

            _GridRules.DataSource = _dt;

            // 그리드 스타일 설정
            _GridRules.Columns["Object"].Visible = false;
            _GridRules.Columns["Target"].Width = 120;
            _GridRules.Columns["Type"].Width = 80;
            _GridRules.Columns["Rule"].Width = 200;
            _GridRules.Columns["Stop"].Width = 80;
            _GridRules.Columns["Desc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // 정렬 비활성화
            foreach (DataGridViewColumn col in _GridRules.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        /// <summary>
        /// 좌측 리스트박스에 모든 모터 목록 표시
        /// </summary>
        private void RefreshTargetList()
        {
            _ListTargetAxis.Items.Clear();
            foreach (MotionID id in Enum.GetValues(typeof(MotionID)))
            {
                _ListTargetAxis.Items.Add(id);
            }
        }

        /// <summary>
        /// IO Type(Input/Output)에 따라 콤보박스 목록 갱신
        /// </summary>
        private void RefreshIOList()
        {
            _ComboIOIndex.Items.Clear();
            bool isInput = _ComboIOType.SelectedIndex == 0;

            if (isInput)
            {
                foreach (IO_IN item in Enum.GetValues(typeof(IO_IN)))
                    _ComboIOIndex.Items.Add(item);
            }
            else
            {
                foreach (IO_OUT item in Enum.GetValues(typeof(IO_OUT)))
                    _ComboIOIndex.Items.Add(item);
            }

            if (_ComboIOIndex.Items.Count > 0)
                _ComboIOIndex.SelectedIndex = 0;
        }

        /// <summary>
        /// 타겟 모터 선택 시 그리드 갱신
        /// </summary>
        private void _ListTargetAxis_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        /// <summary>
        /// 그리드 데이터 다시 그리기 (현재 선택된 타겟 기준)
        /// </summary>
        private void RefreshGrid()
        {
            _dt.Rows.Clear();
            if (_ListTargetAxis.SelectedItem == null) return;

            MotionID target = (MotionID)_ListTargetAxis.SelectedItem;

            // ActUserOption에서 데이터 가져오기
            var interlockOption = ActManager.Instance.Act.Option.Interlock;
            var list = interlockOption.GetList(target);

            foreach (var item in list)
            {
                // 규칙 요약 문자열 생성
                string ruleSummary = "";
                if (item.Type == InterLockType.Position)
                {
                    string op = item.Condition switch
                    {
                        CompareCondition.Less => "<",
                        CompareCondition.Greater => ">",
                        CompareCondition.Equal => "==",
                        CompareCondition.NotEqual => "!=",
                        _ => "?"
                    };
                    ruleSummary = $"[Ax:{item.SourceAxis}] {op} {item.CompareValue:F1}";
                }
                else
                {
                    string ioType = item.IsInput ? "IN" : "OUT";
                    string sig = item.IoSignal ? "ON" : "OFF";
                    ruleSummary = $"[{ioType}-{item.IoIndex}] == {sig}";
                }

                _dt.Rows.Add(
                    item,               // Hidden Object
                    item.TargetAxis,
                    item.Type,
                    ruleSummary,
                    item.StopDir,
                    item.Description
                );
            }
        }

        // --- [이벤트 핸들러: 추가/삭제/저장] ---

        private void _BtnAddPos_Click(object sender, EventArgs e)
        {
            if (_ListTargetAxis.SelectedItem == null)
            {
                ActManager.Instance.Act.PopupNoti("경고", "좌측 목록에서 타겟 모터를 먼저 선택하세요.", Domain.Enums.NotifyType.Warning);
                return;
            }

            // 유효성 검사
            if (!double.TryParse(_TextValue.Text, out double val))
            {
                _TextValue.Focus();
                return;
            }
            if (!double.TryParse(_TextRange.Text, out double range)) range = 10.0; // 기본값

            // [중복 검사 로직 추가]
            var target = (MotionID)_ListTargetAxis.SelectedItem;
            var source = (MotionID)_ComboSourceAxis.SelectedItem;
            var condition = (CompareCondition)_ComboCondition.SelectedItem;
            var dir = (StopDirection)_ComboPosDir.SelectedItem;

            var exists = ActManager.Instance.Act.Option.Interlock.Items.Any(item =>
                item.TargetAxis == target &&
                item.Type == InterLockType.Position &&
                item.SourceAxis == source &&
                item.Condition == condition &&
                Math.Abs(item.CompareValue - val) < 0.0001 && // double 값 비교 (오차 허용)
                                                              // item.Range == range && // (범위까지 같아야 중복으로 볼지는 선택 사항, 여기선 포함)
                item.StopDir == dir
            );

            if (exists)
            {
                ActManager.Instance.Act.PopupNoti("중복 경고", "이미 동일한 포지션 인터락 조건이 존재합니다.", Domain.Enums.NotifyType.Warning);
                return;
            }

            // 엔티티 생성
            var newItem = new MotionInterlockItem
            {
                TargetAxis = (MotionID)_ListTargetAxis.SelectedItem,
                Type = InterLockType.Position,

                SourceAxis = (MotionID)_ComboSourceAxis.SelectedItem,
                Condition = (CompareCondition)_ComboCondition.SelectedItem,
                CompareValue = val,
                Range = range,

                StopDir = (StopDirection)_ComboPosDir.SelectedItem
            };
            newItem.MakeDescription(); // 설명 자동 생성

            // 리스트에 추가 및 UI 갱신
            ActManager.Instance.Act.Option.Interlock.Items.Add(newItem);
            RefreshGrid();
        }

        private void _BtnAddIO_Click(object sender, EventArgs e)
        {
            if (_ListTargetAxis.SelectedItem == null)
            {
                ActManager.Instance.Act.PopupNoti("경고", "좌측 목록에서 타겟 모터를 먼저 선택하세요.", Domain.Enums.NotifyType.Warning);
                return;
            }
            if (_ComboIOIndex.SelectedItem == null) return;

            // 입력된 값으로 임시 변수 생성
            var target = (MotionID)_ListTargetAxis.SelectedItem;
            bool isInput = (_ComboIOType.SelectedIndex == 0);
            int ioIndex = (int)_ComboIOIndex.SelectedItem;
            bool ioSignal = (_ComboIOSignal.SelectedIndex == 0);
            var dir = (StopDirection)_ComboIODir.SelectedItem;

            // [중복 검사 로직 추가]
            var exists = ActManager.Instance.Act.Option.Interlock.Items.Any(item =>
                item.TargetAxis == target &&
                (item.Type == InterLockType.IoInput || item.Type == InterLockType.IoOutput) &&
                item.IsInput == isInput &&
                item.IoIndex == ioIndex &&
                item.IoSignal == ioSignal &&
                item.StopDir == dir
            );

            if (exists)
            {
                ActManager.Instance.Act.PopupNoti("중복 경고", "이미 동일한 I/O 인터락 조건이 존재합니다.", Domain.Enums.NotifyType.Warning);
                return;
            }

            // 엔티티 생성
            var newItem = new MotionInterlockItem
            {
                TargetAxis = (MotionID)_ListTargetAxis.SelectedItem,
                Type = (_ComboIOType.SelectedIndex == 0) ? InterLockType.IoInput : InterLockType.IoOutput,

                // 콤보박스 아이템 자체가 Enum이므로 int로 캐스팅
                IoIndex = (int)_ComboIOIndex.SelectedItem,
                IsInput = (_ComboIOType.SelectedIndex == 0),
                IoSignal = (_ComboIOSignal.SelectedIndex == 0), // 0:ON, 1:OFF

                StopDir = (StopDirection)_ComboIODir.SelectedItem
            };
            newItem.MakeDescription();

            // 리스트에 추가 및 UI 갱신
            ActManager.Instance.Act.Option.Interlock.Items.Add(newItem);
            RefreshGrid();
        }

        private void _BtnDelete_Click(object sender, EventArgs e)
        {
            if (_GridRules.SelectedRows.Count == 0) return;

            // 선택된 행 삭제
            foreach (DataGridViewRow row in _GridRules.SelectedRows)
            {
                var item = row.Cells["Object"].Value as MotionInterlockItem;
                if (item != null)
                {
                    ActManager.Instance.Act.Option.Interlock.Items.Remove(item);
                }
            }
            RefreshGrid();
        }

        private async void _ButtonSave_Click(object sender, EventArgs e)
        {
            var act = ActManager.Instance.Act;
            var r = await act.PopupYesNo.ConfirmAsync("저장", "인터락 설정을 저장하시겠습니까?", Domain.Enums.NotifyType.Info);

            if (r == Domain.Enums.YesNoResult.Yes)
            {
                try
                {
                    await act.Option.Save<UserOptionMotionInterlock>();
                   
                }
                catch (Exception ex)
                {
                  
                }
            }
        }
    }
}