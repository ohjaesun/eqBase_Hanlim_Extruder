using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.UI.Controls;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EQ.UI.UserViews
{
    public partial class MotorPosition_View : UserControlBase
    {
        private DataTable _dt;
        private DataView _dv;

        private int _targetRowIndex = -1;
        private int _targetColIndex = -1;

        public MotorPosition_View()
        {
            InitializeComponent();
        }

        private void MotorPosition_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            _LabelTitle.Text = "Motor Position Teaching";
            _ButtonSave.Click += _ButtonSave_Click;

            InitGrid();
            InitFilters();
            LoadData();

            timer1.Interval = 1000;
            timer1.Start();
        }

        private void InitGrid()
        {
            _dt = new DataTable();

            _dt.Columns.Add("AxisInt", typeof(int));
            // [추가] MotionKey와 매치되는 Key 컬럼
            _dt.Columns.Add("Key", typeof(string));
            _dt.Columns.Add("Axis", typeof(string));
            _dt.Columns.Add("Name", typeof(string));
            _dt.Columns.Add("Group", typeof(string));
            _dt.Columns.Add("Position", typeof(double));
            _dt.Columns.Add("Speed", typeof(double));
            _dt.Columns.Add("Acc", typeof(double));
            _dt.Columns.Add("Dec", typeof(double));
            _dt.Columns.Add("Desc", typeof(string));

            _dv = new DataView(_dt);
            dataGridView1.DataSource = _dv;

            // --- 그리드 속성 설정 ---
            dataGridView1.Columns["AxisInt"].Visible = false;

            // Key 컬럼 설정
            dataGridView1.Columns["Key"].Visible = false;
            dataGridView1.Columns["Key"].ReadOnly = true;
            dataGridView1.Columns["Key"].Width = 180; // 키는 좀 더 넓게
            dataGridView1.Columns["Key"].DefaultCellStyle.BackColor = Color.WhiteSmoke; // 읽기 전용 느낌

            // Axis 컬럼 설정
            dataGridView1.Columns["Axis"].ReadOnly = true;
            dataGridView1.Columns["Axis"].Width = 120;

            dataGridView1.Columns["Name"].ReadOnly = true;
            dataGridView1.Columns["Name"].Width = 100;

            dataGridView1.Columns["Group"].ReadOnly = true;
            dataGridView1.Columns["Group"].Width = 80;

            // Position
            dataGridView1.Columns["Position"].Width = 80;
            dataGridView1.Columns["Position"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["Position"].DefaultCellStyle.Font = new Font("D2Coding", 12F, FontStyle.Bold); // 강조

            dataGridView1.Columns["Speed"].Width = 70;
            dataGridView1.Columns["Speed"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["Acc"].Width = 70;
            dataGridView1.Columns["Acc"].DefaultCellStyle.Format = "N0";
            dataGridView1.Columns["Dec"].Width = 70;
            dataGridView1.Columns["Dec"].DefaultCellStyle.Format = "N0";

            dataGridView1.Columns["Desc"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void InitFilters()
        {
            // 1. Axis ComboBox
            _ComboAxis.Items.Clear();
            _ComboAxis.Items.Add("All");
            foreach (MotionID id in Enum.GetValues(typeof(MotionID)))
            {
                _ComboAxis.Items.Add(id.ToString());
            }
            _ComboAxis.SelectedIndex = 0;

            // 2. Group ComboBox
            _ComboGroup.Items.Clear();
            _ComboGroup.Items.Add("All");
            foreach (PosGroup group in Enum.GetValues(typeof(PosGroup)))
            {
                _ComboGroup.Items.Add(group.ToString());
            }
            _ComboGroup.SelectedIndex = 0;
        }

        private void LoadData()
        {
            _dt.Rows.Clear();

            var items = ActManager.Instance.Act.Option.MotionPos.Items;

            foreach (var item in items)
            {
                // [요청사항 반영]
                // 1. Key 생성: Axis_Name
                string keyDisplay = $"{item.Axis}_{item.Name}";

                // 2. Axis 표시 포맷: 이름[번호]
                string axisDisplay = $"{item.Axis}[{(int)item.Axis}]";

                _dt.Rows.Add(
                    (int)item.Axis,
                    keyDisplay,   // Key
                    axisDisplay,  // Axis (Format: Name[No])
                    item.Name,
                    item.Group.ToString(),
                    item.Position,
                    item.Speed,
                    item.Acc,
                    item.Dec,
                    item.Description
                );
            }
        }

        private void _ComboFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dv == null) return;

            string filter = "";

            // 필터 로직은 AxisInt(Hidden)를 기준으로 하거나, 
            // 표시된 문자열(Axis)에 대해 'Like' 검색을 해야 하는데,
            // 가장 깔끔한 건 AxisInt를 사용하는 것입니다.

            if (_ComboAxis.SelectedIndex > 0)
            {
                // ComboBox는 순수 Enum 이름이므로, 원래 Axis 컬럼 필터링은 동작하지 않을 수 있습니다.
                // Axis 컬럼 값이 "STAGE_X[7]"로 바뀌었기 때문입니다.
                // 따라서 AxisInt를 이용하여 필터링합니다.

                // 선택된 텍스트(Enum이름)를 Enum으로 변환 후 Int로 변환
                if (Enum.TryParse(_ComboAxis.SelectedItem.ToString(), out MotionID id))
                {
                    filter += $"AxisInt = {(int)id}";
                }
            }

            if (_ComboGroup.SelectedIndex > 0)
            {
                string selectedGroup = _ComboGroup.SelectedItem.ToString();
                if (filter.Length > 0) filter += " AND ";
                filter += $"Group = '{selectedGroup}'";
            }

            _dv.RowFilter = filter;
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                // "Position" 컬럼 이름으로 확인
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Position")
                {
                    _targetRowIndex = e.RowIndex;
                    _targetColIndex = e.ColumnIndex;

                    DataRowView rowView = dataGridView1.Rows[e.RowIndex].DataBoundItem as DataRowView;
                    if (rowView == null) return;

                    int motorIdx = (int)rowView["AxisInt"];
                    MotionID motorId = (MotionID)motorIdx;

                    double actPos = ActManager.Instance.Act.Motion.GetStatus(motorId).ActualPos;

                    var _name = (string)rowView["Name"];


                    ContextMenuStrip menuStrip = new ContextMenuStrip();
                    ToolStripMenuItem item = new ToolStripMenuItem($"{motorId} : {_name} => {actPos:F0}");
                    item.Click += (s, ev) =>
                    {
                        dataGridView1.Rows[_targetRowIndex].Cells[_targetColIndex].Value = actPos;
                    };
                    menuStrip.Items.Add(item);
                    menuStrip.Show(Cursor.Position);
                }
            }
        }

        private async void _ButtonSave_Click(object sender, EventArgs e)
        {
            var act = ActManager.Instance.Act;

            //   var r = await act.PopupYesNo.ConfirmAsync("저장", "변경된 포지션 데이터를 저장하시겠습니까?", Domain.Enums.NotifyType.Info);
            //   if (r != Domain.Enums.YesNoResult.Yes) return;

            try
            {
                var posOption = act.Option.MotionPos;

                foreach (DataRow row in _dt.Rows)
                {
                    // Key 컬럼을 이용해 원본 데이터 찾기 (가장 확실함)
                    string key = row["Key"].ToString();
                    var targetItem = posOption.Get(key);

                    if (targetItem != null)
                    {
                        targetItem.Position = Convert.ToDouble(row["Position"]);
                        targetItem.Speed = Convert.ToDouble(row["Speed"]);
                        targetItem.Acc = Convert.ToDouble(row["Acc"]);
                        targetItem.Dec = Convert.ToDouble(row["Dec"]);
                        targetItem.Description = row["Desc"].ToString();
                    }
                }

                await act.Option.Save<UserOptionMotionPos>();
                //  act.PopupNoti("저장 완료", "포지션 데이터가 저장되었습니다.", Domain.Enums.NotifyType.Info);
            }
            catch (Exception ex)
            {
                act.PopupNoti("Error", $"Save Failed: {ex.Message}", Domain.Enums.NotifyType.Error);
            }
        }

        // 셀 포맷팅 (필요시)
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var act = ActManager.Instance.Act;

            if (dataGridView1.CurrentCell.RowIndex != -1)
            {
                var pos = act.Motion.GetStatus((MotionID)dataGridView1.CurrentCell.RowIndex).ActualPos;
                _LabelCurrentPos.Text = $"{(MotionID)dataGridView1.CurrentCell.RowIndex}[{dataGridView1.CurrentCell.RowIndex}] Pos:{pos}";
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid == null || e.RowIndex < 0) return;

            DataRowView rowView = dataGridView1.Rows[e.RowIndex].DataBoundItem as DataRowView;
            if (rowView == null) return;

            int motorIdx = (int)rowView["AxisInt"];
            MotionID motorId = (MotionID)motorIdx;

            double Pos = (double)rowView["Position"];
            

            motionMove_View1.setMotion(motorId);
            motionMove_View1.setDefinePos(Pos);
        }
    }
}