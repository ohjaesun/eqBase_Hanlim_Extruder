using EQ.Common.Logs;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.UI.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace EQ.UI.UserViews
{
    public partial class AlarmSolution_View : UserControlBase
    {
        private DataTable _dt;
        private readonly string _filePath;

        public AlarmSolution_View()
        {
            InitializeComponent();

            string folder = Path.Combine(Environment.CurrentDirectory, "CommonData");
            Directory.CreateDirectory(folder);
            _filePath = Path.Combine(folder, "AlarmSolutions.json");
        }

        private void AlarmSolution_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            _LabelTitle.Text = "Alarm Solution Management";
            _ButtonSave.Click += _ButtonSave_Click;

            InitGrid();
            LoadData();
        }

        private void InitGrid()
        {
            _dt = new DataTable();
            // 컬럼 정의: 번호(No), 이름(Name)은 읽기 전용 성격
            _dt.Columns.Add("ErrorNo", typeof(int));
            _dt.Columns.Add("ErrorName", typeof(string));
            _dt.Columns.Add("Cause", typeof(string));
            _dt.Columns.Add("Solution", typeof(string));

            _GridList.DataSource = _dt;

            // 그리드 스타일 및 읽기 전용 설정
            _GridList.Columns["ErrorNo"].Width = 80;
            _GridList.Columns["ErrorNo"].ReadOnly = true;
            _GridList.Columns["ErrorNo"].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            _GridList.Columns["ErrorNo"].HeaderText = "No";

            _GridList.Columns["ErrorName"].Width = 200;
            _GridList.Columns["ErrorName"].ReadOnly = true;
            _GridList.Columns["ErrorName"].DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
            _GridList.Columns["ErrorName"].HeaderText = "Alarm Name";

            _GridList.Columns["Cause"].Width = 300;
            _GridList.Columns["Cause"].HeaderText = "Cause (원인)";

            _GridList.Columns["Solution"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            _GridList.Columns["Solution"].HeaderText = "Solution (조치)";

            // 정렬 금지 (Enum 순서 유지)
            foreach (DataGridViewColumn col in _GridList.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void LoadData()
        {
            _dt.Rows.Clear();

            // 1. 파일에서 기존 데이터 로드 (딕셔너리로 변환하여 검색 속도 향상)
            Dictionary<ErrorList, AlarmSolutionData> savedDataMap = new Dictionary<ErrorList, AlarmSolutionData>();

            if (File.Exists(_filePath))
            {
                try
                {
                    string json = File.ReadAllText(_filePath);
                    var storage = JsonConvert.DeserializeObject<AlarmSolutionStorage>(json);
                    if (storage?.Items != null)
                    {
                        foreach (var item in storage.Items)
                        {
                            if (!savedDataMap.ContainsKey(item.ErrorCode))
                                savedDataMap.Add(item.ErrorCode, item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"[AlarmSolution] Load Failed: {ex.Message}");
                }
            }

            // 2. Enum의 모든 항목을 순회하며 그리드에 추가
            // (파일에 없는 새로운 에러 코드도 자동으로 리스트에 표시됨)
            foreach (ErrorList err in Enum.GetValues(typeof(ErrorList)))
            {
                string cause = "";
                string solution = "";

                // 저장된 데이터가 있으면 불러옴
                if (savedDataMap.TryGetValue(err, out var savedItem))
                {
                    cause = savedItem.Cause;
                    solution = savedItem.Solution;
                }

                _dt.Rows.Add((int)err, err.ToString(), cause, solution);
            }
        }

        private async void _ButtonSave_Click(object sender, EventArgs e)
        {
            // 포커스를 잃게 하여 현재 편집 중인 셀의 데이터를 커밋
            _LabelTitle.Focus();

            var storage = new AlarmSolutionStorage();

            foreach (DataRow row in _dt.Rows)
            {
                string errName = row["ErrorName"].ToString();

                // Cause나 Solution에 내용이 있는 경우만 저장 (용량 절약)
                string cause = row["Cause"].ToString();
                string solution = row["Solution"].ToString();

                if (string.IsNullOrWhiteSpace(cause) && string.IsNullOrWhiteSpace(solution))
                    continue;

                if (Enum.TryParse(errName, out ErrorList errEnum))
                {
                    storage.Items.Add(new AlarmSolutionData
                    {
                        ErrorCode = errEnum,
                        Cause = cause,
                        Solution = solution
                    });
                }
            }

            try
            {
                string json = JsonConvert.SerializeObject(storage, Formatting.Indented);
                await System.Threading.Tasks.Task.Run(() => File.WriteAllText(_filePath, json));

                EQ.Core.Service.ActManager.Instance.Act.PopupNoti("저장 완료", "알람 솔루션 데이터가 저장되었습니다.", NotifyType.Info);
            }
            catch (Exception ex)
            {
                EQ.Core.Service.ActManager.Instance.Act.PopupNoti("저장 실패", ex.Message, NotifyType.Error);
            }
        }
    }
}