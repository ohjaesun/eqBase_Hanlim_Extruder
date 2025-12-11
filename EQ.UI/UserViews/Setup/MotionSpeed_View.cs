using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.UI.Controls;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EQ.UI.UserViews
{
    public partial class MotionSpeed_View : UserControlBase
    {
        private DataTable _dt;

        public MotionSpeed_View()
        {
            InitializeComponent();
        }

        private void MotionSpeed_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            // 상속받은 UserControlBase의 타이틀 설정
            _LabelTitle.Text = "Motion Speed Parameters";

            // 저장 버튼 이벤트 연결 (UserControlBase의 _ButtonSave)
            _ButtonSave.Click += _ButtonSave_Click;

            InitGrid();
            LoadData();
        }

        /// <summary>
        /// 그리드 컬럼 초기화
        /// </summary>
        private void InitGrid()
        {
            _dt = new DataTable();
            _dt.Columns.Add("ID", typeof(int));
            _dt.Columns.Add("Name", typeof(string));
            _dt.Columns.Add("Auto", typeof(double));
            _dt.Columns.Add("Manual", typeof(double));
            _dt.Columns.Add("Home", typeof(double));
            _dt.Columns.Add("JogFast", typeof(double));
            _dt.Columns.Add("JogSlow", typeof(double));
            _dt.Columns.Add("Accel", typeof(double));
            _dt.Columns.Add("Decel", typeof(double));
            _dt.Columns.Add("SCurve", typeof(bool));
            _dt.Columns.Add("Jerk", typeof(double));

            dataGridView1.DataSource = _dt;

            // 컬럼 포맷 및 설정
            dataGridView1.Columns["ID"].Visible = false; // ID 숨김
            dataGridView1.Columns["Home"].Visible = false; // ID 숨김
            dataGridView1.Columns["Name"].ReadOnly = true;
            dataGridView1.Columns["Name"].Width = 150;

            // 나머지 컬럼 정렬 금지 및 너비 설정
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                if (col.Name != "Name") col.Width = 70;
            }
        }

        /// <summary>
        /// 데이터 로드 (ActUserOption -> DataTable)
        /// </summary>
        private void LoadData()
        {
            _dt.Rows.Clear();

            // ActUserOption에서 데이터 가져오기 (UserOptionMotion 클래스 사용 가정)
            var speedList = ActManager.Instance.Act.Option.MotionSpeed.SpeedList;

            foreach (var item in speedList)
            {
                _dt.Rows.Add(
                    (int)item.Axis,
                    item.Axis.ToString(),
                    item.AutoSpeed,
                    item.ManualSpeed,
                    item.HomeSpeed,
                    item.JogPastSpeed,
                    item.JogNormalSpeed,
                    item.Accel,
                    item.Deaccel,
                    item.SCurve,
                    item.JerkRatio
                );
            }
        }

        /// <summary>
        /// 저장 (DataTable -> ActUserOption -> Save)
        /// </summary>
        private async void _ButtonSave_Click(object sender, EventArgs e)
        {
            var act = ActManager.Instance.Act;

            // YesNo 팝업 (비동기)
            var result = await act.PopupYesNo.ConfirmAsync("저장", "변경된 속도 데이터를 저장하시겠습니까?", Domain.Enums.NotifyType.Info);
            if (result != Domain.Enums.YesNoResult.Yes) return;

            try
            {
                // 1. DataTable 내용을 UserOptionMotion 리스트에 반영
                var speedOption = act.Option.MotionSpeed;

                foreach (DataRow row in _dt.Rows)
                {
                    MotionID id = (MotionID)row["ID"];
                    var targetItem = speedOption.Get(id); // 기존 객체 가져오기

                    if (targetItem != null)
                    {
                        targetItem.AutoSpeed = Convert.ToDouble(row["Auto"]);
                        targetItem.ManualSpeed = Convert.ToDouble(row["Manual"]);
                        targetItem.HomeSpeed = Convert.ToDouble(row["Home"]);
                        targetItem.JogPastSpeed = Convert.ToDouble(row["JogFast"]);
                        targetItem.JogNormalSpeed = Convert.ToDouble(row["JogSlow"]);
                        targetItem.Accel = Convert.ToDouble(row["Accel"]);
                        targetItem.Deaccel = Convert.ToDouble(row["Decel"]);
                        targetItem.SCurve = Convert.ToBoolean(row["SCurve"]);
                        targetItem.JerkRatio = Convert.ToDouble(row["Jerk"]);

                        // 유효성 검사 예시
                        if (targetItem.JerkRatio < 0 || targetItem.JerkRatio > 1)
                        {
                            act.PopupNoti("입력 오류", $"{id}의 Jerk 값은 0~1 사이여야 합니다.", Domain.Enums.NotifyType.Error);
                            return;
                        }
                    }
                }

                // 2. 파일 저장
                await act.Option.Save<UserOptionMotionSpeed>();
               
            }
            catch (Exception ex)
            {
                act.PopupNoti("저장 실패", ex.Message, Domain.Enums.NotifyType.Error);
            }
        }

        /// <summary>
        /// 계산 버튼 클릭
        /// </summary>
        private void _ButtonCalc_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            int rowIndex = dataGridView1.CurrentRow.Index;
            DataRow row = _dt.Rows[rowIndex];

            if (!double.TryParse(_TextBox1.Text, out double distance))
            {
                distance = 100; // 기본값
            }
            distance *= 1000; // mm -> um 단위 변환 가정 (단위 확인 필요)

            double velo = Convert.ToDouble(row["Auto"]);
            double acc = Convert.ToDouble(row["Accel"]);
            double dec = Convert.ToDouble(row["Decel"]);

            CalcTime(acc, dec, velo, distance);
        }

        /// <summary>
        /// 그리드 셀 클릭 시 시뮬레이션
        /// </summary>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataRow row = _dt.Rows[e.RowIndex];

            richTextBox1.Clear();
            richTextBox1.SelectionColor = System.Drawing.Color.Yellow;
            richTextBox1.AppendText($"[Simulate: {row["Name"]}]\n");
            richTextBox1.SelectionColor = System.Drawing.Color.White;

            double velo = Convert.ToDouble(row["Auto"]);
            double acc = Convert.ToDouble(row["Accel"]);
            double dec = Convert.ToDouble(row["Decel"]);

            double[] dists = { 10, 100, 500, 1000 };

            foreach (var d in dists)
            {
                CalcTime(acc, dec, velo, d * 1000);
            }
        }

        // 기존 로직 포팅 (단위: Velocity=mm/s, Acc=mm/s^2 가정 시 * 1000 제거 검토 필요)
        // 원본 로직 유지 (단위 변환 포함)
        private void CalcTime(double acc, double deAcc, double velocity, double Targetdistance)
        {
            // 원본 코드 로직 그대로 사용
            // (단, 입력값 단위가 Grid에 어떻게 저장되느냐에 따라 *1000 여부 결정)

            double acceleration = acc;      // 이미 단위가 맞춰져 있다고 가정하거나 원본대로 * 1000
            double deceleration = deAcc;
            double maxVelocity = velocity;

            // ※ 원본 코드는 Grid값(mm단위)에 *1000을 해서 um단위로 계산하는 것으로 보임
            // 여기서는 물리 연산 로직만 그대로 가져옴

            acceleration *= 1000;
            deceleration *= 1000;
            maxVelocity *= 1000;

            double distance = Targetdistance / 1000.0; // 다시 mm로 표시용

            double Gravity_mmps2 = 9806.65;

            double accelDistance = (maxVelocity * maxVelocity) / (2 * acceleration);
            double decelDistance = (maxVelocity * maxVelocity) / (2 * deceleration);
            double cruiseDistance = Math.Abs(Targetdistance) - (accelDistance + decelDistance);

            double totalTime = 0;
            string s = string.Empty;

            double accelTime_setting = maxVelocity / acceleration;
            double decelTime_setting = maxVelocity / deceleration;

            if (cruiseDistance >= 0)
            {
                double cruiseTime = cruiseDistance / maxVelocity;
                totalTime = accelTime_setting + cruiseTime + decelTime_setting;

                s = $"거리:{distance}mm, 도착:{totalTime:F3}s, " +
                    $"가속:{accelTime_setting:F2}s[{(acceleration / Gravity_mmps2):F1}g], " +
                    $"등속:{cruiseTime:F2}s, " +
                    $"감속:{decelTime_setting:F2}s[{(deceleration / Gravity_mmps2):F1}g]";
            }
            else
            {
                double peakVelocity = Math.Sqrt((2 * Math.Abs(Targetdistance) * acceleration * deceleration) / (acceleration + deceleration));
                double accelTime = peakVelocity / acceleration;
                double decelTime = peakVelocity / deceleration;

                totalTime = accelTime + decelTime;

                s = $"거리:{distance}mm, 도착:{totalTime:F3}s (최고속도 도달불가), " +
                    $"가속:{accelTime:F2}s, 감속:{decelTime:F2}s";
            }

            richTextBox1.AppendText(s + "\n");
            richTextBox1.ScrollToCaret();
        }
    }
}