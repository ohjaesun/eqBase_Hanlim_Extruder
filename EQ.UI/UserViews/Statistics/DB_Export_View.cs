using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.Infra.Storage;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json.Linq;

namespace EQ.UI.UserViews
{
    /// <summary>
    /// DB에 저장된 데이터들을 외부로 백업(export)하는 뷰
    /// 레시피 파일등 잘못 건드려서 문제가 생겼을 때 원복하거나 비교하기 위해
    /// </summary>
    public partial class DB_Export_View : UserControlBase
    {
        public DB_Export_View()
        {
            InitializeComponent();

        }

        private void _Button1_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = Environment.CurrentDirectory;
                dialog.Filter = "DB 파일 (*.db)|*.db|모든 파일 (*.*)|*.*";
                dialog.Title = "DB 파일을 선택하세요.";

                DialogResult result = dialog.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.FileName))
                {
                    string fullFilePath = dialog.FileName; // 예: "C:\MyProject\Data\recipe.json"

                    // 1. 파일의 상위 폴더 경로 가져오기
                    string directoryPath = Path.GetDirectoryName(fullFilePath); // "C:\MyProject\Data"

                    // 2. 상위 폴더 경로에서 마지막 폴더명만 추출
                    string folderName = Path.GetFileName(directoryPath); // "Data"

                    _Label1.Text = fullFilePath;

                    LoadDbTableInfo(fullFilePath);

                }
            }
        }


        private void LoadDbTableInfo(string dbPath)
        {
            _GridDBInfo.Columns.Clear();

            _GridDBInfo.Columns.Add("No", "No");
            _GridDBInfo.Columns.Add("TableName", "Table Name");
            _GridDBInfo.Columns.Add("RowCount", "Rows");
            _GridDBInfo.Columns.Add("LastTime", "Last Update (Approx)");

            // 스타일
            _GridDBInfo.Columns["No"].Width = 50;
            _GridDBInfo.Columns["TableName"].Width = 250;
            _GridDBInfo.Columns["RowCount"].Width = 100;
            _GridDBInfo.Columns["LastTime"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            _GridDBInfo.Rows.Clear();

            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbPath}"))
                {
                    connection.Open();

                    // 1. 테이블 목록 가져오기 (sqlite_sequence 등 시스템 테이블 제외)
                    var tableNames = new List<string>();
                    var cmdTables = connection.CreateCommand();
                    cmdTables.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';";

                    using (var reader = cmdTables.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tableNames.Add(reader.GetString(0));
                        }
                    }

                    int no = 1;
                    // 2. 각 테이블의 정보(행 개수, 최근 시간) 조회
                    foreach (var table in tableNames)
                    {
                        long rowCount = 0;
                        string lastTime = "-";

                        // 행 개수 조회
                        var cmdCount = connection.CreateCommand();
                        cmdCount.CommandText = $"SELECT COUNT(*) FROM {table}";
                        object countObj = cmdCount.ExecuteScalar();
                        if (countObj != null) rowCount = Convert.ToInt64(countObj);
                      
                        try
                        {
                            var cmdTime = connection.CreateCommand();
                            cmdTime.CommandText = $"SELECT Timestamp FROM {table} ORDER BY Timestamp DESC LIMIT 1";
                            object timeObj = cmdTime.ExecuteScalar();
                            if (timeObj != null)
                            {
                                long unixTime = Convert.ToInt64(timeObj);
                                lastTime = DateTimeOffset.FromUnixTimeSeconds(unixTime).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
                            }
                        }
                        catch
                        {
                           
                        }
                      
                        _GridDBInfo.Rows.Add(no++, table, rowCount, lastTime);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"DB 정보 로드 실패: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 유저 데이터에 대해 30일치 변경분은 백업 시켜 놓음
        /// 필요시 이를 추출하여 사용
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _Button2_Click(object sender, EventArgs e)
        {
            string dbFilePath = _Label1.Text;

            if (string.IsNullOrEmpty(dbFilePath) || !File.Exists(dbFilePath))
            {
               ActManager.Instance.Act.PopupNoti("파일이 없습니다.",NotifyType.Warning);
                return;
            }

            var act = ActManager.Instance.Act;
            string basePath = Path.GetDirectoryName(dbFilePath);
            string historyPath = Path.Combine(basePath, "History");
            Directory.CreateDirectory(historyPath);

            try
            {
                using (var connection = new SqliteConnection($"Data Source={dbFilePath}"))
                {
                    connection.Open();

                    // 1. 테이블 목록 가져오기
                    var tables = new List<string>();
                    var cmdTables = connection.CreateCommand();
                    cmdTables.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';";

                    using (var reader = cmdTables.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tables.Add(reader.GetString(0));
                        }
                    }

                    int totalFiles = 0;

                    // 2. 각 테이블 데이터 내보내기
                    foreach (var tableName in tables)
                    {
                        string exportFolder = Path.Combine(historyPath, tableName);
                        Directory.CreateDirectory(exportFolder);

                        var cmdData = connection.CreateCommand();
                        cmdData.CommandText = $"SELECT Value, Timestamp FROM {tableName} ORDER BY Timestamp ASC";

                        using (var reader = cmdData.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // [수정 핵심] 데이터 타입을 확인하여 분기 처리
                                object rawValue = reader.GetValue(0);
                                long unixTimestamp = reader.GetInt64(1);

                                DateTimeOffset dateTime = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp);
                                string timeStr = dateTime.ToLocalTime().ToString("yyyy-MM-dd_HH-mm-ss");

                                // A. 바이너리 데이터 (WaferMap 등) -> .bin 저장
                                if (rawValue is byte[] blobData)
                                {
                                    string fileName = timeStr + ".bin";
                                    string filePath = Path.Combine(exportFolder, fileName);

                                    // 중복 파일명 처리
                                    int dupCount = 1;
                                    while (File.Exists(filePath))
                                    {
                                        fileName = timeStr + $"_{dupCount}.bin";
                                        filePath = Path.Combine(exportFolder, fileName);
                                        dupCount++;
                                    }

                                    File.WriteAllBytes(filePath, blobData);
                                }
                                // B. 텍스트 데이터 (UserOption 등 JSON) -> .json 저장
                                else if (rawValue is string jsonValue)
                                {
                                    string fileName = timeStr + ".json";
                                    string filePath = Path.Combine(exportFolder, fileName);

                                    int dupCount = 1;
                                    while (File.Exists(filePath))
                                    {
                                        fileName = timeStr + $"_{dupCount}.json";
                                        filePath = Path.Combine(exportFolder, fileName);
                                        dupCount++;
                                    }

                                    try
                                    {
                                        // JSON 포맷팅 시도
                                        string indentedJson = JToken.Parse(jsonValue).ToString(Newtonsoft.Json.Formatting.Indented);
                                        File.WriteAllText(filePath, indentedJson);
                                    }
                                    catch
                                    {
                                        // 실패 시 원본 저장
                                        File.WriteAllText(filePath, jsonValue);
                                    }
                                }
                                totalFiles++;
                            }
                        }
                    }
                    act.PopupNoti("복원 완료", $"총 {tables.Count}개 테이블, {totalFiles}개 파일 추출됨.", NotifyType.Info);
                }
            }
            catch (Exception ex)
            {
                act.PopupNoti("복원 실패", $"복원 중 오류가 발생했습니다.\n{ex.Message}", NotifyType.Error);
            }
        }
    }
}
