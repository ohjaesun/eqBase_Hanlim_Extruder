using EQ.Common.Logs;
using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.UI.Controls;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace EQ.UI.UserViews
{
    public partial class Motor_View : UserControlBase
    {
        private readonly Core.Act.ACT _act;
        private System.Windows.Forms.Timer _updateTimer;
        private System.Windows.Forms.Timer _updateTimer2;
        private DataTable _dataTable;

        private int MOTOR_COUNT = Enum.GetValues(typeof(MotionID)).Length;

        public Motor_View()
        {
            InitializeComponent();
            _act = ActManager.Instance.Act;
        }

        private void Motor_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            _LabelTitle.Text = "Motor Status";
            _ButtonSave.Visible = false;

            InitGrid();

            _updateTimer = new System.Windows.Forms.Timer();
            _updateTimer.Interval = 200;
            _updateTimer.Tick += UpdateTimer_Tick;
            _updateTimer.Start();

            _updateTimer2 = new System.Windows.Forms.Timer();
            _updateTimer2.Interval = 1000;
            _updateTimer2.Tick += UpdateTimer_Tick2;
            _updateTimer2.Start();

            this.Disposed += Motor_View_Disposed;

            motionMove_View1.setMotion((MotionID)0);
        }

        private void UpdateTimer_Tick2(object? sender, EventArgs e)
        {
            DispInfo();
        }

        private void Motor_View_Disposed(object sender, EventArgs e)
        {
            if (_updateTimer != null)
            {
                _updateTimer.Stop();
                _updateTimer.Tick -= UpdateTimer_Tick;
                _updateTimer.Dispose();
                _updateTimer = null;

                _updateTimer2.Stop();
                _updateTimer2.Tick -= UpdateTimer_Tick2;
                _updateTimer2.Dispose();
                _updateTimer2 = null;
            }
            this.Disposed -= Motor_View_Disposed;
        }

        private void InitGrid()
        {
            _dataTable = new DataTable();

            _dataTable.Columns.Add("No", typeof(int));
            _dataTable.Columns.Add("Name", typeof(string));
            _dataTable.Columns.Add("CmdPos", typeof(double));
            _dataTable.Columns.Add("ActPos", typeof(double));
            _dataTable.Columns.Add("Follow", typeof(double));
            _dataTable.Columns.Add("Vel", typeof(double));
            _dataTable.Columns.Add("Trq", typeof(double));
            _dataTable.Columns.Add("Alarm", typeof(string));

            // 로직용 컬럼 (Grid에는 미표시)
            _dataTable.Columns.Add("Servo", typeof(bool));
            _dataTable.Columns.Add("HomeDone", typeof(bool));
            _dataTable.Columns.Add("Busy", typeof(bool));
            _dataTable.Columns.Add("InPos", typeof(bool));
            _dataTable.Columns.Add("AbsType", typeof(bool));

            foreach (MotionID id in Enum.GetValues(typeof(MotionID)))
            {
                DataRow row = _dataTable.NewRow();
                row["No"] = (int)id;
                row["Name"] = id.ToString();
                row["CmdPos"] = 0.0;
                row["ActPos"] = 0.0;
                row["Follow"] = 0.0;
                row["Vel"] = 0.0;
                row["Trq"] = 0.0;
                row["Alarm"] = "0";
                row["Servo"] = false;
                row["HomeDone"] = false;
                row["Busy"] = false;
                row["InPos"] = false;
                row["AbsType"] = false;
                _dataTable.Rows.Add(row);
            }

            dataGridView1.DataSource = _dataTable;

            ConfigureGridColumns();
        }

        private void ConfigureGridColumns()
        {
            dataGridView1.Columns["No"].Width = 40;
            dataGridView1.Columns["Name"].Width = 150;
            dataGridView1.Columns["CmdPos"].Width = 80;
            dataGridView1.Columns["ActPos"].Width = 80;
            dataGridView1.Columns["Follow"].Width = 60;
            dataGridView1.Columns["Vel"].Width = 60;
            dataGridView1.Columns["Trq"].Width = 60;
            dataGridView1.Columns["Alarm"].Width = 60;

            dataGridView1.Columns["CmdPos"].DefaultCellStyle.Format = "F1";
            dataGridView1.Columns["ActPos"].DefaultCellStyle.Format = "F1";
            dataGridView1.Columns["Follow"].DefaultCellStyle.Format = "F1";
            dataGridView1.Columns["Vel"].DefaultCellStyle.Format = "F1";
            dataGridView1.Columns["Trq"].DefaultCellStyle.Format = "F1";

            AddButtonColumn("Btn_Servo", "Servo", 60);
            AddButtonColumn("Btn_Reset", "Reset", 60);
            AddButtonColumn("Btn_Home", "Home", 60);

            dataGridView1.Columns["Servo"].Visible = false;
            dataGridView1.Columns["HomeDone"].Visible = false;
            dataGridView1.Columns["Busy"].Visible = false;
            dataGridView1.Columns["InPos"].Visible = false;
            dataGridView1.Columns["AbsType"].Visible = false;
        }

        private void AddButtonColumn(string name, string headerText, int width)
        {
            if (dataGridView1.Columns.Contains(name)) return;

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = name;
            btn.HeaderText = headerText;
            btn.Text = headerText;
            btn.UseColumnTextForButtonValue = true;
            btn.Width = width;

            btn.FlatStyle = FlatStyle.Popup;
            btn.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            btn.DefaultCellStyle.ForeColor = Color.Black;
            btn.DefaultCellStyle.SelectionBackColor = Color.WhiteSmoke;
            btn.DefaultCellStyle.SelectionForeColor = Color.Black;

            dataGridView1.Columns.Add(btn);
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (_act == null || _dataTable == null) return;

            try
            {
                foreach (MotionID id in Enum.GetValues(typeof(MotionID)))
                {
                    int axisNo = (int)id;
                    if (axisNo >= _dataTable.Rows.Count) continue;

                    // [수정] int axisNo 대신 MotorID id를 직접 전달
                    MotionStatus status = _act.Motion.GetStatus(id);
                    bool isAbs = _act.Motion.GetAbsType(id);

                    DataRow row = _dataTable.Rows[axisNo];
                    row["CmdPos"] = status.CommandPos;
                    row["ActPos"] = status.ActualPos;
                    row["Follow"] = status.CommandPos - status.ActualPos;
                    row["Vel"] = status.ActualVelocity;
                    row["Trq"] = status.ActualTorque;
                    row["Alarm"] = status.AmpAlarm ? status.AmpAlarmCode.ToString() : "OK";
                    row["Servo"] = status.ServoOn;
                    row["HomeDone"] = status.HomeDone;
                    row["InPos"] = status.InPos;
                    row["Busy"] = !status.InPos;
                    row["AbsType"] = isAbs;

                    UpdateRowStyle(axisNo, status, isAbs);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"MotorView Update Error: {ex.Message}");
            }


        }

        private void UpdateRowStyle(int rowIndex, MotionStatus status, bool isAbsType)
        {
            if (rowIndex < 0 || rowIndex >= dataGridView1.Rows.Count) return;

            var nameCell = dataGridView1.Rows[rowIndex].Cells["Name"];
            Color nameColor = isAbsType ? Color.Blue : Color.Black;

            if (nameCell.Style.ForeColor != nameColor)
            {
                nameCell.Style.ForeColor = nameColor;
                nameCell.Style.SelectionForeColor = nameColor;

                if (isAbsType) nameCell.Style.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                else nameCell.Style.Font = dataGridView1.Font;
            }

            var servoCell = dataGridView1.Rows[rowIndex].Cells["Btn_Servo"];
            Color servoBackColor = status.ServoOn ? Color.Lime : Color.WhiteSmoke;

            if (servoCell.Style.BackColor != servoBackColor)
            {
                servoCell.Style.BackColor = servoBackColor;
                servoCell.Style.ForeColor = Color.Black;
                servoCell.Style.SelectionBackColor = servoBackColor;
                servoCell.Style.SelectionForeColor = Color.Black;
            }

            var homeCell = dataGridView1.Rows[rowIndex].Cells["Btn_Home"];
            Color homeBackColor = status.HomeDone ? Color.Lime : Color.WhiteSmoke;

            if (homeCell.Style.BackColor != homeBackColor)
            {
                homeCell.Style.BackColor = homeBackColor;
                homeCell.Style.ForeColor = Color.Black;
                homeCell.Style.SelectionBackColor = homeBackColor;
                homeCell.Style.SelectionForeColor = Color.Black;
            }

            var resetCell = dataGridView1.Rows[rowIndex].Cells["Btn_Reset"];
            if (resetCell.Style.BackColor != Color.WhiteSmoke)
            {
                resetCell.Style.BackColor = Color.WhiteSmoke;
                resetCell.Style.SelectionBackColor = Color.WhiteSmoke;
                resetCell.Style.ForeColor = Color.Black;
                resetCell.Style.SelectionForeColor = Color.Black;
            }

            var alarmCell = dataGridView1.Rows[rowIndex].Cells["Alarm"];
            bool isAlarm = status.AmpAlarm || (status.AmpAlarmCode != 0);

            if (isAlarm)
            {
                if (alarmCell.Style.ForeColor != Color.Red)
                {
                    alarmCell.Style.ForeColor = Color.Red;
                    alarmCell.Style.Font = new Font(dataGridView1.Font, FontStyle.Bold);
                }
            }
            else
            {
                if (alarmCell.Style.ForeColor != Color.Black)
                {
                    alarmCell.Style.ForeColor = Color.Black;
                    alarmCell.Style.Font = dataGridView1.Font;
                }
            }
        }

        private void DispInfo()
        {
            if (dataGridView1.CurrentCell == null) return;
            int rowidx = dataGridView1.CurrentCell.RowIndex;
            if (rowidx < 0 || rowidx >= _dataTable.Rows.Count) return;

            int axisNo = (int)_dataTable.Rows[rowidx]["No"];

            // [수정] int axisNo를 MotorID로 캐스팅하여 호출
            MotionStatus status = _act.Motion.GetStatus((MotionID)axisNo);
            bool isAbs = _act.Motion.GetAbsType((MotionID)axisNo);

            richTextBox1.Clear();
            richTextBox1.AppendText($"[Axis {axisNo} Details]\n");
            richTextBox1.AppendText($"------------------------------------------------\n");
            richTextBox1.AppendText($"Pos : {status.ActualPos:F3} / Vel : {status.ActualVelocity:F1} / Trq : {status.ActualTorque:F1}\n");
            richTextBox1.AppendText($"Mode: {(isAbs ? "Absolute" : "Incremental")} / Home : {status.HomeDone} / InPos : {status.InPos}\n");
            richTextBox1.AppendText($"Status: Servo:{status.ServoOn} / Alarm:{status.AmpAlarm} ({status.AmpAlarmCode}) / Op:{status.OP}\n");
        }

        private async void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid == null || e.RowIndex < 0) return;

            string colName = grid.Columns[e.ColumnIndex].Name;
            int axisNo = (int)_dataTable.Rows[e.RowIndex]["No"];

            // [수정] MotorID 변수 준비
            MotionID motorId = (MotionID)axisNo;

            if (colName == "Btn_Servo")
            {
                bool currentServo = (bool)_dataTable.Rows[e.RowIndex]["Servo"];
                bool newServoState = !currentServo;


                _act.Motion.ServoOn(motorId, newServoState);
                Log.Instance.Info($"Request Servo {(newServoState ? "ON" : "OFF")} : Axis {axisNo}");
            }
            else if (colName == "Btn_Reset")
            {

                _act.Motion.AlarmReset(motorId);
                Log.Instance.Info($"Request Alarm Reset : Axis {axisNo}");
            }
            else if (colName == "Btn_Home")
            {
                bool currentServo = (bool)_dataTable.Rows[e.RowIndex]["Servo"];
                if (!currentServo)
                {
                    _act.PopupNoti($"축 {axisNo}번 Servo Off", NotifyType.Warning);
                    return;
                }

                bool isAbs = _act.Motion.GetAbsType(motorId);
                if (isAbs)
                {
                    // 2. ABS 모터일 경우 강력한 경고 팝업 (Error 타입) 표시
                    var absResult = await _act.PopupYesNo.ConfirmAsync(
                        "Absolute Axis Warning",
                        $"축 {axisNo}번은 [Absolute] 모터입니다.\n절대 위치 정보(HomeOffset)가 변경됩니다.\n정말 진행하시겠습니까?",
                        Domain.Enums.NotifyType.Error
                    );

                    // '아니오' 선택 시 중단
                    if (absResult != Domain.Enums.YesNoResult.Yes)
                        return;
                }

                var result = await _act.PopupYesNo.ConfirmAsync("Home Search", $"축 {axisNo} 원점 복귀를 하시겠습니까?", Domain.Enums.NotifyType.Warning);
                if (result == Domain.Enums.YesNoResult.Yes)
                {

                    _act.Motion.Home(motorId);
                    Log.Instance.Info($"Request Home : Axis {axisNo}");
                }
            }

          

        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 필요한 경우 커스텀 드로잉 로직 추가
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid == null || e.RowIndex < 0) return;

            string colName = grid.Columns[e.ColumnIndex].Name;
            int axisNo = (int)_dataTable.Rows[e.RowIndex]["No"];

            // [수정] MotorID 변수 준비
            MotionID motorId = (MotionID)axisNo;
            motionMove_View1.setMotion(motorId);
        }
    }
}