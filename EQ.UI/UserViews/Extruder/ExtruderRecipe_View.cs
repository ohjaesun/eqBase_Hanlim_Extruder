using EQ.Core.Service;
using EQ.Domain.Entities.Extruder;
using EQ.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using static EQ.Core.Globals;

namespace EQ.UI.UserViews.Extruder
{
    /// <summary>
    /// ExtruderRecipe를 DataGridView로 표시하는 View
    /// 콤보박스로 레시피 선택, 선택된 레시피만 표시
    /// </summary>
    public partial class ExtruderRecipe_View : UserControlBase
    {
        private DataTable _dataTable;
        private List<ExtruderRecipe> _recipes;
        private ExtruderRecipe _selectedRecipe;

        // 카테고리별 배경색 (2가지)
        private static readonly Color _color1 = Color.FromArgb(255, 255, 225); // 연한 노란색
        private static readonly Color _color2 = Color.FromArgb(225, 240, 255); // 연한 파란색

        public ExtruderRecipe_View()
        {
            InitializeComponent();
        }

        private void ExtruderRecipe_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            InitializeComboBox();
            InitializeGrid();
            LoadData();
            Invalidate();
        }

        private void InitializeComboBox()
        {
            var act = ActManager.Instance.Act;
            _recipes = act.ExtruderRecipe.Recipes.ToList();

            _comboRecipe.Items.Clear();
            foreach (var recipe in _recipes)
            {
                _comboRecipe.Items.Add(recipe.Name);
            }

            // ActExtruderRecipe의 현재 레시피 인덱스를 사용
            int currentIndex = act.ExtruderRecipe.CurrentRecipeIndex;
            if (currentIndex >= 0 && currentIndex < _comboRecipe.Items.Count)
            {
                _comboRecipe.SelectedIndex = currentIndex;
            }
            else if (_comboRecipe.Items.Count > 0)
            {
                _comboRecipe.SelectedIndex = 0;
            }

            _comboRecipe.SelectedIndexChanged += _comboRecipe_SelectedIndexChanged;
        }

        private void _comboRecipe_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ActExtruderRecipe의 현재 레시피 인덱스 업데이트
            var act = ActManager.Instance.Act;
            act.ExtruderRecipe.SetCurrentRecipe(_comboRecipe.SelectedIndex);
            
            LoadData();
            UpdateIndexLabel();
        }

        private void UpdateIndexLabel()
        {
            // 1-based 인덱스 표시
            int index = _comboRecipe.SelectedIndex + 1;
            int total = _comboRecipe.Items.Count;
            _labelRecipe.Text = string.Format("{0}/{1}", index, total);
        }

        private void InitializeGrid()
        {
            _dataGridView1.AllowUserToAddRows = false;
            _dataGridView1.AllowUserToDeleteRows = false;
            _dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            _dataGridView1.AllowUserToResizeColumns = true; // 컬럼 너비 수동 변경 허용
            _dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            _dataGridView1.MultiSelect = false;
            _dataGridView1.RowHeadersVisible = false;
            _dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically; // 직접 편집 비활성화

            // 저장 버튼 이벤트 연결
            _ButtonSave.Click += _ButtonSave_Click;

            // 셀 클릭 이벤트 등록
            _dataGridView1.CellClick += _dataGridView1_CellClick;

            // 데이터 바인딩 완료 이벤트 등록 (색상 적용)
            _dataGridView1.DataBindingComplete += _dataGridView1_DataBindingComplete;
        }

        private void _dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            // 데이터 바인딩 완료 후 카테고리별 색상 적용
            ApplyCategoryRowColors();
        }

        private void LoadData()
        {
            if (_comboRecipe.SelectedIndex < 0 || _comboRecipe.SelectedIndex >= _recipes.Count)
                return;

            _selectedRecipe = _recipes[_comboRecipe.SelectedIndex];
            _dataTable = CreateDataTable();
            _dataGridView1.DataSource = _dataTable;

            // Category 컬럼 숨기기
            if (_dataGridView1.Columns.Contains("Category"))
            {
                _dataGridView1.Columns["Category"].Visible = false;
            }

            // 열 스타일 설정 및 기본 너비 (UI 가로 1920 기준)
            // Value(100) + Unit(60) + Range(100) = 260
            // Property(380) + Description(나머지 약 1280)
            foreach (DataGridViewColumn col in _dataGridView1.Columns)
            {
                if (col.Name == "Property")
                {
                    col.ReadOnly = true;
                    col.Width = 380; // 가장 긴 속성명도 보이도록
                }
                else if (col.Name == "Value")
                {
                    col.ReadOnly = false; // Value 열만 편집 가능
                    col.Width = 100;
                }
                else if (col.Name == "Unit")
                {
                    col.ReadOnly = true;
                    col.Width = 60;
                }
                else if (col.Name == "Range")
                {
                    col.ReadOnly = true;
                    col.Width = 100;
                }
                else if (col.Name == "Description")
                {
                    col.ReadOnly = true;
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; // 나머지 공간 채움
                }
            }

            // 인덱스 레이블 업데이트
            UpdateIndexLabel();
        }

        /// <summary>
        /// 카테고리별 행 배경색을 2가지 색으로 교차 적용
        /// </summary>
        private void ApplyCategoryRowColors()
        {
            if (_dataGridView1.Rows.Count == 0) return;

            string lastCategory = null;
            int colorIndex = -1;
            Color[] colors = { _color1, _color2 };

            foreach (DataGridViewRow row in _dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                string category = row.Cells["Category"].Value?.ToString() ?? "";
                
                // 카테고리가 바뀌면 색상 전환
                if (category != lastCategory)
                {
                    colorIndex = (colorIndex + 1) % 2;
                    lastCategory = category;
                }

                row.DefaultCellStyle.BackColor = colors[colorIndex];
            }
        }

        /// <summary>
        /// 선택된 ExtruderRecipe 하나에 대한 DataTable 생성
        /// 컬럼 순서: Category, Property, Value, Unit, Description
        /// </summary>
        private DataTable CreateDataTable()
        {
            var table = new DataTable();

            // 열 추가 (순서: Category, Property, Value, Unit, Range, Description)
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("Property", typeof(string));
            table.Columns.Add("Value", typeof(object));
            table.Columns.Add("Unit", typeof(string));
            table.Columns.Add("Range", typeof(string));
            table.Columns.Add("Description", typeof(string));

            // Name 속성 행 추가 (첫 번째 행)
            var nameRow = table.NewRow();
            nameRow["Category"] = "Info";
            nameRow["Property"] = "Name";
            nameRow["Value"] = _selectedRecipe.Name;
            nameRow["Unit"] = "";
            nameRow["Range"] = "";
            nameRow["Description"] = L("레시피 이름");
            table.Rows.Add(nameRow);

            // ExtruderRecipe 속성 정보 가져오기
            var properties = typeof(ExtruderRecipe).GetProperties()
                .Where(p => p.GetCustomAttribute<ExtruderParameterAttribute>() != null)
                .OrderBy(p => p.GetCustomAttribute<ExtruderParameterAttribute>()?.Category)
                .ThenBy(p => p.Name)
                .ToList();

            foreach (var prop in properties)
            {
                var attr = prop.GetCustomAttribute<ExtruderParameterAttribute>();
                if (attr == null) continue;

                var row = table.NewRow();
                row["Category"] = attr.Category;
                row["Property"] = prop.Name;
                row["Value"] = prop.GetValue(_selectedRecipe);
                row["Unit"] = attr.Unit;
                row["Range"] = attr.Range;
                // 현재 언어에 따라 Description을 번역
                row["Description"] = L(attr.Description);

                table.Rows.Add(row);
            }

            return table;
        }

        private void _ButtonSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private async void SaveData()
        {
            if (_selectedRecipe == null) return;

            // DataTable -> Recipe 객체로 반영
            var allProperties = typeof(ExtruderRecipe).GetProperties()
                .ToDictionary(p => p.Name, p => p);

            foreach (DataRow row in _dataTable.Rows)
            {
                string propName = row["Property"].ToString();
                if (!allProperties.ContainsKey(propName)) continue;

                var prop = allProperties[propName];
                var value = row["Value"];
                if (value == DBNull.Value) continue;

                try
                {
                    // 타입 변환
                    object convertedValue = Convert.ChangeType(value, prop.PropertyType);
                    prop.SetValue(_selectedRecipe, convertedValue);
                }
                catch
                {
                    // 변환 실패 시 무시
                }
            }

            // 콤보박스 항목도 업데이트 (이름이 변경된 경우)
            if (_comboRecipe.SelectedIndex >= 0)
            {
                _comboRecipe.Items[_comboRecipe.SelectedIndex] = _selectedRecipe.Name;
            }

            // 저장
            var act = ActManager.Instance.Act;
            await act.ExtruderRecipe.Save();
        }

        private void _ButtonRefresh_Click(object sender, EventArgs e)
        {
            var act = ActManager.Instance.Act;
            _recipes = act.ExtruderRecipe.Recipes.ToList();
            LoadData();
        }

        private void _ButtonUp_Click(object sender, EventArgs e)
        {
            if (_comboRecipe.SelectedIndex > 0)
            {
                _comboRecipe.SelectedIndex--;
            }
        }

        private void _ButtonDown_Click(object sender, EventArgs e)
        {
            if (_comboRecipe.SelectedIndex < _comboRecipe.Items.Count - 1)
            {
                _comboRecipe.SelectedIndex++;
            }
        }

        private void _ButtonScrollUp_Click(object sender, EventArgs e)
        {
            //5보다 크면 5칸 이동 아니면 1칸 이동 처음이면 무시
            if (_dataGridView1.FirstDisplayedScrollingRowIndex > 5)
            {
                _dataGridView1.FirstDisplayedScrollingRowIndex -= 5;
            }
            else if (_dataGridView1.FirstDisplayedScrollingRowIndex > 0)
            {
                _dataGridView1.FirstDisplayedScrollingRowIndex--;
            }
        }

        private void _ButtonScrollDown_Click(object sender, EventArgs e)
        {
            //5보다 크면 5칸 이동 아니면 1칸 이동 마지막이면 무시
            int lastVisibleRow = _dataGridView1.FirstDisplayedScrollingRowIndex + _dataGridView1.DisplayedRowCount(false);
            if (lastVisibleRow < _dataGridView1.RowCount)
            {
                if (_dataGridView1.FirstDisplayedScrollingRowIndex + 5 < _dataGridView1.RowCount)
                {
                    _dataGridView1.FirstDisplayedScrollingRowIndex += 5;
                }
                else if (_dataGridView1.FirstDisplayedScrollingRowIndex < _dataGridView1.RowCount - 1)
                {
                    _dataGridView1.FirstDisplayedScrollingRowIndex++;
                }
            }
        }

        /// <summary>
        /// Value 셀 클릭 시 키패드 표시
        /// </summary>
        private void _dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 헤더 클릭 무시
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var cell = _dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var columnName = _dataGridView1.Columns[e.ColumnIndex].Name;

            // Value 컬럼만 처리
            if (columnName != "Value") return;

            // 현재 행의 Property 이름과 Range 가져오기
            var row = _dataGridView1.Rows[e.RowIndex];
            string propertyName = row.Cells["Property"].Value?.ToString() ?? "";
            string rangeStr = row.Cells["Range"].Value?.ToString() ?? "";
            object currentValue = cell.Value;

            // 값 타입 확인
            if (currentValue == null || currentValue == DBNull.Value || currentValue is string)
            {
                // 문자열 타입 - FormKeyboard 호출
                string initialValue = currentValue?.ToString() ?? "";
                string title = propertyName;
                
                using (var keyboard = new EQ.UI.Forms.FormKeyboard(title, initialValue))
                {
                    if (keyboard.ShowDialog() == DialogResult.OK)
                    {
                        cell.Value = keyboard.ResultValue;
                    }
                }
                return;
            }

            // 숫자 타입인 경우 FormKeypad 호출
            if (IsNumericType(currentValue))
            {
                double initialValue = Convert.ToDouble(currentValue);
                double? minValue = null;
                double? maxValue = null;

                // Range 파싱 (예: "0...30", "10...150")
                ParseRange(rangeStr, out minValue, out maxValue);

                string title = propertyName;
                using (var keypad = new EQ.UI.Forms.FormKeypad(title, initialValue, minValue, maxValue))
                {
                    if (keypad.ShowDialog() == DialogResult.OK)
                    {
                        double newValue = keypad.ResultValue;

                        // Range 유효성 검사 (FormKeypad 내부에서도 체크하지만 추가 확인)
                        if (minValue.HasValue && newValue < minValue.Value)
                        {
                            ActManager.Instance.Act.PopupNoti(
                                L("Range Warning"),
                                L("Value {0} is below minimum {1}", newValue, minValue.Value),
                                NotifyType.Warning);
                        }
                        else if (maxValue.HasValue && newValue > maxValue.Value)
                        {
                            ActManager.Instance.Act.PopupNoti(
                                L("Range Warning"),
                                L("Value {0} is above maximum {1}", newValue, maxValue.Value),
                                NotifyType.Warning);
                        }

                        // DataTable에 값 반영
                        cell.Value = newValue;
                    }
                }
            }
        }

        /// <summary>
        /// 숫자 타입 여부 확인
        /// </summary>
        private bool IsNumericType(object value)
        {
            return value is byte || value is sbyte ||
                   value is short || value is ushort ||
                   value is int || value is uint ||
                   value is long || value is ulong ||
                   value is float || value is double || value is decimal;
        }

        /// <summary>
        /// Range 문자열 파싱 ("0...30" -> min=0, max=30)
        /// </summary>
        private void ParseRange(string rangeStr, out double? min, out double? max)
        {
            min = null;
            max = null;

            if (string.IsNullOrEmpty(rangeStr)) return;

            // "0...30" 형태 파싱
            string[] parts = rangeStr.Split(new[] { "..." }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2)
            {
                if (double.TryParse(parts[0].Trim(), out double minVal))
                    min = minVal;
                if (double.TryParse(parts[1].Trim(), out double maxVal))
                    max = maxVal;
            }
        }
    }
}

