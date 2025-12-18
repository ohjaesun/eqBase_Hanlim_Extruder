using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.UI.Controls;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace EQ.UI.UserViews
{
    // 1. UserControlBase 상속
    public partial class Alarm_View : UserControlBase
    {
        private readonly ACT _act;
        private readonly string _dbPath;
        private readonly string _tableName;

        private DataTable _historyTable;
        private DataTable _pivotTable;

        public Alarm_View()
        {
            InitializeComponent();
            _act = ActManager.Instance.Act;

            // 2. ActAlarm에서 DB 경로와 테이블 이름(Key)을 가져옴
            _dbPath = _act.AlarmDB.GetAlarmDbPath();
            _tableName = _act.AlarmDB.GetAlarmDbKey();
        }

        private void Alarm_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            _LabelTitle.Text = "Alarm History & Statistics";
            _ButtonSave.Visible = false; // 저장 버튼 숨김

            _DateTimePickerStart.Value = DateTime.Now.Date;
            _DateTimePickerEnd.Value = DateTime.Now.Date;

            // 3. EqBase 컨트롤 스타일 적용
            _ListViewStats.View = View.Details;
            _ListViewStats.Columns.Add("ID", 300, HorizontalAlignment.Left);
            _ListViewStats.Columns.Add("Count", 70, HorizontalAlignment.Right);
            _ListViewStats.Columns.Add("Rate", 100, HorizontalAlignment.Right);
        }

        private void _ButtonLoad_Click(object sender, EventArgs e)
        {
            LoadDataFromDb();
        }

        private void LoadDataFromDb()
        {
            // 1. 테이블 초기화
            _historyTable = new DataTable();
            _historyTable.Columns.Add("Timestamp", typeof(DateTime));

            // 2. 리플렉션을 이용해 AlarmData의 모든 속성을 컬럼으로 자동 추가
            var properties = typeof(AlarmData).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in properties)
            {
                var colType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                _historyTable.Columns.Add(prop.Name, colType);
            }

            List<AlarmData> alarmList = new List<AlarmData>();

            if (!File.Exists(_dbPath))
            {
                _act.PopupNoti("알림", "알람 DB 파일이 아직 생성되지 않았습니다.", Domain.Enums.NotifyType.Info);
                return;
            }

            // 3. DB 조회
            long startTimestamp = new DateTimeOffset(_DateTimePickerStart.Value.Date).ToUnixTimeSeconds();
            long endTimestamp = new DateTimeOffset(_DateTimePickerEnd.Value.Date.AddDays(1).AddTicks(-1)).ToUnixTimeSeconds();

            try
            {
                using (var connection = new SqliteConnection($"Data Source={_dbPath}"))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText =
                        $"SELECT Value, Timestamp FROM {_tableName} " +
                        "WHERE Timestamp >= @start AND Timestamp <= @end " +
                        "ORDER BY Timestamp ASC";
                    command.Parameters.AddWithValue("@start", startTimestamp);
                    command.Parameters.AddWithValue("@end", endTimestamp);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string jsonValue = reader.GetString(0);
                            long unixTimestamp = reader.GetInt64(1);

                            AlarmData alarm = JsonConvert.DeserializeObject<AlarmData>(jsonValue);
                            DateTime dt = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).ToLocalTime().DateTime;

                            alarmList.Add(alarm);

                            // 4. Row 데이터 채우기
                            DataRow row = _historyTable.NewRow();
                            row["Timestamp"] = dt;

                            foreach (var prop in properties)
                            {
                                row[prop.Name] = prop.GetValue(alarm) ?? DBNull.Value;
                            }
                            _historyTable.Rows.Add(row);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _act.PopupNoti("DB 오류", $"알람 DB를 읽는 중 오류가 발생했습니다.\n{ex.Message}", Domain.Enums.NotifyType.Error);
                return;
            }

            // 5. 데이터 바인딩
            _DataGridViewHistory.DataSource = _historyTable;

            // [수정] 컬럼 너비를 균등하게 설정 (전체 너비 / 컬럼 수)
            _DataGridViewHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // (기존의 내용 기반 너비 조절 코드는 제거)
            // _DataGridViewHistory.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);

            UpdateStatistics(alarmList);
            UpdatePivotTable();
        }

        /*
        private void UpdateStatistics(List<AlarmData> alarmList)
        {
            _ListViewStats.Items.Clear();
            if (alarmList.Count == 0) return;

            var idCounts = alarmList
                .GroupBy(alarm => alarm.IDs)
                .Select(group => new { Id = group.Key, Count = group.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            foreach (var item in idCounts)
            {
                var rate = item.Count / (double)alarmList.Count * 100;
                ListViewItem lvi = new ListViewItem(item.Id);
                lvi.SubItems.Add(item.Count.ToString());
                lvi.SubItems.Add($"{rate:F2} %");
                _ListViewStats.Items.Add(lvi);
            }
        }
        */
        private void UpdateStatistics(List<AlarmData> alarmList)
        {
            _ListViewStats.Items.Clear();

            // 1. 차트 초기화 및 설정
            Series series = chart1.Series[0]; // Designer에서 생성된 Series1 사용
            series.Points.Clear();
            series.ChartType = SeriesChartType.Pie; // 차트 타입: Pie
            series["PieLabelStyle"] = "Outside";    // (선택) 라벨을 파이 바깥쪽으로 표시하여 가독성 확보

            if (alarmList.Count == 0) return;

            // 데이터 그룹화 (기존 로직 동일)
            var idCounts = alarmList
                .GroupBy(alarm => alarm.IDs)
                .Select(group => new { Id = group.Key, Count = group.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();

            foreach (var item in idCounts)
            {
                // 2. ListView 아이템 추가
                var rate = item.Count / (double)alarmList.Count * 100;
                ListViewItem lvi = new ListViewItem(item.Id);
                lvi.SubItems.Add(item.Count.ToString());
                lvi.SubItems.Add($"{rate:F2} %");
                _ListViewStats.Items.Add(lvi);

                // 3. Chart 데이터 추가
                // AddXY(X축: 라벨/ID, Y축: 값/Count)
                int pointIndex = series.Points.AddXY(item.Id, item.Count);
              
                series.Points[pointIndex].Label = $"{item.Count}\n({rate:F1}%)";
                series.Points[pointIndex].LegendText = item.Id; // 범례 텍스트
                series.Points[pointIndex].ToolTip = $"{item.Id}: {item.Count}회"; // 마우스 오버 시 툴팁
            }
        }

        private void UpdatePivotTable()
        {           
            string idColumnName = nameof(AlarmData.IDs);

            _pivotTable = new DataTable();
            _pivotTable.Columns.Add("Date");

            // 해당 컬럼이 존재하는지 확인 
            if (!_historyTable.Columns.Contains(idColumnName))
            {
                _DataGridViewPivot.DataSource = null;
                return;
            }

            var uniqueIds = _historyTable.AsEnumerable()
                .Select(row => row.Field<string>(idColumnName)) // 동적 컬럼명 사용
                .Distinct()
                .ToList();

         
            foreach (string id in uniqueIds)
            {
                if (!string.IsNullOrEmpty(id))
                    _pivotTable.Columns.Add(id, typeof(int));
            }

            var groupedByDate = _historyTable.AsEnumerable()
                .GroupBy(row => row.Field<DateTime>("Timestamp").ToString("yy-MM-dd"));

            foreach (var group in groupedByDate)
            {
                DataRow newRow = _pivotTable.NewRow();
                newRow["Date"] = group.Key;

                foreach (string id in uniqueIds)
                {
                    if (string.IsNullOrEmpty(id)) continue;
                 
                    int count = group.Count(r => r.Field<string>(idColumnName) == id);
                    newRow[id] = count;
                }
                _pivotTable.Rows.Add(newRow);
            }
            _DataGridViewPivot.DataSource = _pivotTable;
        }

        private void _ButtonExport_Click(object sender, EventArgs e)
        {
            // 9. CSV 내보내기 
            if (_historyTable == null || _historyTable.Rows.Count == 0)
            {
                _act.PopupNoti("정보", "내보낼 데이터가 없습니다.", Domain.Enums.NotifyType.Warning);
                return;
            }

            string folderPath = Path.Combine(Environment.CurrentDirectory, "Log");
            Directory.CreateDirectory(folderPath);

            string start = _DateTimePickerStart.Value.ToString("yyyyMMdd");
            string end = _DateTimePickerEnd.Value.ToString("yyyyMMdd");
            string fileName = $"AlarmHistory_{start}_{end}.csv";
            string fullPath = Path.Combine(folderPath, fileName);

            try
            {
                StringBuilder csvContent = new StringBuilder();

                // 헤더
                IEnumerable<string> columnNames = _historyTable.Columns.Cast<DataColumn>()
                                    .Select(column => column.ColumnName);
                csvContent.AppendLine(string.Join(",", columnNames));

                // 데이터
                foreach (DataRow row in _historyTable.Rows)
                {
                    IEnumerable<string> fields = row.ItemArray.Select(field =>
                        $"\"{field.ToString().Replace("\"", "\"\"")}\""); // CSV 인코딩
                    csvContent.AppendLine(string.Join(",", fields));
                }

                File.WriteAllText(fullPath, csvContent.ToString(), Encoding.UTF8);

               
                OpenNotepad(fullPath);
            }
            catch (Exception ex)
            {
                _act.PopupNoti("내보내기 실패", ex.Message, Domain.Enums.NotifyType.Error);
            }
        }

        private void OpenNotepad(string filePath, int lineNo = -1)
        {
            string exePath = @"C:\Program Files\Notepad++\notepad++.exe";
            if (!File.Exists(exePath))
            {
                Process.Start("notepad.exe", filePath); // 폴백
                return;
            }

            string args = lineNo > 0 ? $"-n{lineNo} \"{filePath}\"" : $"\"{filePath}\"";
            Process.Start(exePath, args);
        }

        // 10. 로그 파일 바로가기 
        private void _DataGridViewHistory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DateTime dt = (DateTime)_DataGridViewHistory.Rows[e.RowIndex].Cells["Timestamp"].Value;
                string logFilePath = Path.Combine(
                    Environment.CurrentDirectory, "Log",
                    dt.ToString("yyyy"), dt.ToString("MM"), dt.ToString("dd"), "Log.txt");

                if (!File.Exists(logFilePath))
                {
                    _act.PopupNoti("파일 없음", "해당 날짜의 Log.txt 파일을 찾을 수 없습니다.", Domain.Enums.NotifyType.Warning);
                    return;
                }

                // 로그 파일에서 시간으로 라인 찾기
                string time24 = dt.ToString("HH:mm:ss");
                var lines = File.ReadAllLines(logFilePath);
                int lineNo = 0;
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(time24) && lines[i].Contains("[Error]"))
                    {
                        lineNo = i + 1; // Notepad++는 1-based
                        break;
                    }
                }

                OpenNotepad(logFilePath, lineNo);
            }
            catch (Exception ex)
            {
                _act.PopupNoti("오류", ex.Message, Domain.Enums.NotifyType.Error);
            }
        }
    }
}