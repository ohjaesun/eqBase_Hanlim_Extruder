using EQ.Core.Service;
using EQ.Domain.Entities.EQ_Hanlim_Extuder;
using EQ.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EQ.Core.Globals;

namespace EQ.UI.UserViews.EQ_HanLim_Extuder
{
    public partial class ExtruderRecipe_View : UserControlBaseplain
    {
        private const int FixedRowCount = 10;

        public ExtruderRecipe_View()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.DesignMode) return;

            InitText();
            InitializeFixedRows();
            LoadDataToGrid();

            // 셀 클릭 이벤트 등록
            _gridRecipes.CellClick += _gridRecipes_CellClick;
        }

        private void _gridRecipes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 헤더 클릭 무시
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var cell = _gridRecipes.Rows[e.RowIndex].Cells[e.ColumnIndex];

            // Name 컬럼 (인덱스 1) -> 화상 키보드
            if (e.ColumnIndex == 1)
            {
                ShowOnScreenKeyboard();
            }
            // Zone1, Zone2, Speed 컬럼 (인덱스 2, 3, 4) -> FormKeypad
            else if (e.ColumnIndex >= 2 && e.ColumnIndex <= 4)
            {
                string title = _gridRecipes.Columns[e.ColumnIndex].HeaderText;
                double currentValue = 0;
                double.TryParse(cell.Value?.ToString(), out currentValue);

                using (var keypad = new EQ.UI.Forms.FormKeypad(title, currentValue))
                {
                    if (keypad.ShowDialog() == DialogResult.OK)
                    {
                        cell.Value = keypad.ResultValue;
                    }
                }
            }
        }

        // [Win32 API] 창 활성화용
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        /// <summary>
        /// Windows 터치 키보드 호출 (권한 문제 없음)
        /// </summary>
        private void ShowOnScreenKeyboard()
        {
            try
            {
                var procs = Process.GetProcessesByName("osk");
                if (procs.Length > 0)
                {
                    // 이미 실행 중이면 해당 창을 맨 앞으로 가져옴
                    foreach (var proc in procs)
                    {
                        proc.Kill(); // 프로세스 죽이기
                        proc.WaitForExit(100); // (선택) 종료될 때까지 잠시 대기
                                               // SetForegroundWindow(proc.MainWindowHandle);
                    }
                 
                }

                // 2. 경로 설정 (System32)
                string oskPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "osk.exe");

                // 3. 실행 (파일이 존재하면)
                if (File.Exists(oskPath))
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = oskPath,
                        UseShellExecute = true, // .NET 8.0 필수
                        WindowStyle = ProcessWindowStyle.Normal
                    };
                    Process.Start(startInfo);
                }

               
            }
            catch (Exception ex)
            {
                EQ.Common.Logs.Log.Instance.Error($"On-Screen Keyboard Error: {ex.Message}");
            }
        }

        private void InitText()
        {
            _btnSave.Text = L("Save");

            _colNo.HeaderText = L("No.");
            _colName.HeaderText = L("Name");
            _colZone1.HeaderText = L("Zone1");
            _colZone2.HeaderText = L("Zone2");
            _colSpeed.HeaderText = L("Speed");
        }

        /// <summary>
        /// 10개의 고정 Row를 초기화합니다.
        /// </summary>
        private void InitializeFixedRows()
        {
            _gridRecipes.Rows.Clear();
            for (int i = 0; i < FixedRowCount; i++)
            {
                int rowIndex = _gridRecipes.Rows.Add();
                _gridRecipes.Rows[rowIndex].Cells[0].Value = i + 1; // No.
            }
        }

        /// <summary>
        /// ActUserOption에서 데이터를 로드하여 Grid에 표시합니다.
        /// </summary>
        private void LoadDataToGrid()
        {
            var recipes = ActManager.Instance.Act.Option.ExtruderRecipes;

            for (int i = 0; i < FixedRowCount; i++)
            {
                if (i < recipes.Count)
                {
                    var recipe = recipes[i];
                    _gridRecipes.Rows[i].Cells[1].Value = recipe.RecipeName;
                    _gridRecipes.Rows[i].Cells[2].Value = recipe.Zone1_Temp;
                    _gridRecipes.Rows[i].Cells[3].Value = recipe.Zone2_Temp;
                    _gridRecipes.Rows[i].Cells[4].Value = recipe.Extrude_Speed;
                }
                else
                {
                    // 빈 Row 초기화 (-999로 설정)
                    _gridRecipes.Rows[i].Cells[1].Value = "";
                    _gridRecipes.Rows[i].Cells[2].Value = -999;
                    _gridRecipes.Rows[i].Cells[3].Value = -999;
                    _gridRecipes.Rows[i].Cells[4].Value = -999;
                }
            }
        }

        /// <summary>
        /// Grid 데이터를 ActUserOption에 저장합니다.
        /// </summary>
        /// <returns>유효성 검사 실패 시 false, 성공 시 true</returns>
        private bool SaveGridToData()
        {
            var recipes = new List<Extuder_Recipe>();

            for (int i = 0; i < FixedRowCount; i++)
            {
                var row = _gridRecipes.Rows[i];
                string name = row.Cells[1].Value?.ToString() ?? "";

                // 이름이 비어있으면 건너뜀
            //    if (string.IsNullOrWhiteSpace(name)) continue;

                string zone1Str = row.Cells[2].Value?.ToString() ?? "";
                string zone2Str = row.Cells[3].Value?.ToString() ?? "";
                string speedStr = row.Cells[4].Value?.ToString() ?? "";

                // 숫자 유효성 검사
                if (!double.TryParse(zone1Str, out double z1))
                {
                    ActManager.Instance.Act.PopupNoti(
                        L("Validation Error"),
                        L("Row {0}: Zone1 must be a number.", i + 1),
                        EQ.Domain.Enums.NotifyType.Warning);
                    return false;
                }

                if (!double.TryParse(zone2Str, out double z2))
                {
                    ActManager.Instance.Act.PopupNoti(
                        L("Validation Error"),
                        L("Row {0}: Zone2 must be a number.", i + 1),
                        EQ.Domain.Enums.NotifyType.Warning);
                    return false;
                }

                if (!double.TryParse(speedStr, out double spd))
                {
                    ActManager.Instance.Act.PopupNoti(
                        L("Validation Error"),
                        L("Row {0}: Speed must be a number.", i + 1),
                        EQ.Domain.Enums.NotifyType.Warning);
                    return false;
                }

                var recipe = new Extuder_Recipe
                {
                    RecipeName = name,
                    Zone1_Temp = z1,
                    Zone2_Temp = z2,
                    Extrude_Speed = spd
                };

                recipes.Add(recipe);
            }

            // 유효성 검사 통과 후 실제 데이터에 반영
            var targetRecipes = ActManager.Instance.Act.Option.ExtruderRecipes;
            targetRecipes.Clear();
            targetRecipes.AddRange(recipes);

            return true;
        }

        private void _btnSave_Click(object sender, EventArgs e)
        {
            if (SaveGridToData())
            {
                _ = ActManager.Instance.Act.Option.Save<List<Extuder_Recipe>>();
            }
        }
    }
}
