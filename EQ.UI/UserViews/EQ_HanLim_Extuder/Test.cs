using EQ.Common.Logs;
using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EQ.UI.UserViews.EQ_HanLim_Extuder
{
    public partial class Test : UserControlBaseplain
    {
        private readonly Core.Act.ACT _act;
        private System.Windows.Forms.Timer _updateTimer;
        private DataTable _dataTable;

        public Test()
        {
            InitializeComponent();
            _act = ActManager.Instance.Act;
            
            this.Load += Test_Load;
            this.Disposed += Test_Disposed;
        }

        private void Test_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            InitGrid();

            _updateTimer = new System.Windows.Forms.Timer();
            _updateTimer.Interval = 200;
            _updateTimer.Tick += UpdateTimer_Tick;
            _updateTimer.Start();
        }

        private void Test_Disposed(object sender, EventArgs e)
        {
            if (_updateTimer != null)
            {
                _updateTimer.Stop();
                _updateTimer.Tick -= UpdateTimer_Tick;
                _updateTimer.Dispose();
                _updateTimer = null;
            }
            this.Disposed -= Test_Disposed;
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
            _dataTable.Columns.Add("SetVel", typeof(double));
            _dataTable.Columns.Add("SetTrq", typeof(double));

            // 로직용 컬럼 (Grid에는 미표시)
            _dataTable.Columns.Add("Servo", typeof(bool));
            _dataTable.Columns.Add("HomeDone", typeof(bool));
            _dataTable.Columns.Add("Busy", typeof(bool));

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
                row["SetVel"] = 0.0;
                row["SetTrq"] = 0.0;
                row["Servo"] = false;
                row["HomeDone"] = false;
                row["Busy"] = false;
                _dataTable.Rows.Add(row);
            }

            _DataGridView1.DataSource = _dataTable;

            ConfigureGridColumns();
        }

        private void ConfigureGridColumns()
        {
            // 읽기 전용 컬럼 설정
            _DataGridView1.Columns["No"].ReadOnly = true;
            _DataGridView1.Columns["Name"].ReadOnly = true;
            _DataGridView1.Columns["CmdPos"].ReadOnly = true;
            _DataGridView1.Columns["ActPos"].ReadOnly = true;
            _DataGridView1.Columns["Follow"].ReadOnly = true;
            _DataGridView1.Columns["Vel"].ReadOnly = true;
            _DataGridView1.Columns["Trq"].ReadOnly = true;
            _DataGridView1.Columns["Alarm"].ReadOnly = true;

            // 컬럼 너비 설정
            _DataGridView1.Columns["No"].Width = 40;
            _DataGridView1.Columns["Name"].Width = 150;
            _DataGridView1.Columns["CmdPos"].Width = 80;
            _DataGridView1.Columns["ActPos"].Width = 80;
            _DataGridView1.Columns["Follow"].Width = 60;
            _DataGridView1.Columns["Vel"].Width = 60;
            _DataGridView1.Columns["Trq"].Width = 60;
            _DataGridView1.Columns["Alarm"].Width = 60;

            // 숫자 포맷 설정
            _DataGridView1.Columns["CmdPos"].DefaultCellStyle.Format = "F1";
            _DataGridView1.Columns["ActPos"].DefaultCellStyle.Format = "F1";
            _DataGridView1.Columns["Follow"].DefaultCellStyle.Format = "F1";
            _DataGridView1.Columns["Vel"].DefaultCellStyle.Format = "F1";
            _DataGridView1.Columns["Trq"].DefaultCellStyle.Format = "F1";

            // Status 버튼 추가 (상태에 따라 Run/Stop 표시)
            AddButtonColumn("Btn_Set", "Set", 60);

            // 편집 가능한 설정 컬럼 추가
            _DataGridView1.Columns["SetVel"].ReadOnly = false;
            _DataGridView1.Columns["SetVel"].Width = 80;
            _DataGridView1.Columns["SetVel"].DefaultCellStyle.Format = "F1";
            _DataGridView1.Columns["SetVel"].HeaderText = "SetVel";

            // SetVel UP/DN 버튼 추가
            AddButtonColumn("Btn_VelUp", "▲", 40);
            AddButtonColumn("Btn_VelDn", "▼", 40);

            _DataGridView1.Columns["SetTrq"].ReadOnly = false;
            _DataGridView1.Columns["SetTrq"].Width = 80;
            _DataGridView1.Columns["SetTrq"].DefaultCellStyle.Format = "F1";
            _DataGridView1.Columns["SetTrq"].HeaderText = "SetTrq";

            // Set 버튼 추가
            AddButtonColumn("Btn_Run", "Run", 60);

            // 숨김 컬럼
            _DataGridView1.Columns["CmdPos"].Visible = false;
            _DataGridView1.Columns["ActPos"].Visible = false;
            _DataGridView1.Columns["Follow"].Visible = false;
            _DataGridView1.Columns["Servo"].Visible = false;
            _DataGridView1.Columns["HomeDone"].Visible = false;
            _DataGridView1.Columns["Busy"].Visible = false;

            // 셀 클릭 이벤트 연결
            _DataGridView1.CellContentClick += DataGridView1_CellContentClick;
        }

        private void AddButtonColumn(string name, string headerText, int width)
        {
            if (_DataGridView1.Columns.Contains(name)) return;

            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.Name = name;
            btn.HeaderText = headerText;
            btn.Text = headerText;
            btn.UseColumnTextForButtonValue = true;
            btn.Width = width;

            btn.FlatStyle = FlatStyle.Popup;
            btn.DefaultCellStyle.BackColor = Color.LightBlue;
            btn.DefaultCellStyle.ForeColor = Color.Black;
            btn.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            btn.DefaultCellStyle.SelectionForeColor = Color.Black;

            _DataGridView1.Columns.Add(btn);
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (_act == null || _dataTable == null) return;

           // return;
            // 현재 편집 중인 행 인덱스 확인
            int editingRowIndex = -1;
            if (_DataGridView1.CurrentCell != null && _DataGridView1.IsCurrentCellInEditMode)
            {
                editingRowIndex = _DataGridView1.CurrentCell.RowIndex;
            }

            try
            {
                foreach (MotionID id in Enum.GetValues(typeof(MotionID)))
                {
                    int axisNo = (int)id;
                    if (axisNo >= _dataTable.Rows.Count) continue;

                    // 현재 편집 중인 행은 업데이트 건너뛰기 (SetVel, SetTrq 값 보존)
                    if (axisNo == editingRowIndex)
                        continue;

                    MotionStatus status = _act.Motion.GetStatus(id);

                    DataRow row = _dataTable.Rows[axisNo];
                    row["CmdPos"] = status.CommandPos;
                    row["ActPos"] = status.ActualPos;
                    row["Follow"] = status.CommandPos - status.ActualPos;
                    row["Vel"] = status.ActualVelocity;
                    row["Trq"] = status.ActualTorque;
                    row["Alarm"] = status.AmpAlarm ? status.AmpAlarmCode.ToString() : "OK";
                    row["Servo"] = status.ServoOn;
                    row["HomeDone"] = status.HomeDone;
                    row["Busy"] = !status.InPos;

                    UpdateRowStyle(axisNo, status);
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"Test MotorView Update Error: {ex.Message}");
            }
        }

        private void UpdateRowStyle(int rowIndex, MotionStatus status)
        {
            if (rowIndex < 0 || rowIndex >= _DataGridView1.Rows.Count) return;

            // Alarm 셀 스타일 업데이트
            var alarmCell = _DataGridView1.Rows[rowIndex].Cells["Alarm"];
            bool isAlarm = status.AmpAlarm || (status.AmpAlarmCode != 0);

            if (isAlarm)
            {
                if (alarmCell.Style.ForeColor != Color.Red)
                {
                    alarmCell.Style.ForeColor = Color.Red;
                    alarmCell.Style.Font = new Font(_DataGridView1.Font, FontStyle.Bold);
                }
            }
            else
            {
                if (alarmCell.Style.ForeColor != Color.Black)
                {
                    alarmCell.Style.ForeColor = Color.Black;
                    alarmCell.Style.Font = _DataGridView1.Font;
                }
            }

            // Status 버튼 텍스트를 Run/Stop으로 업데이트
            var statusCell = _DataGridView1.Rows[rowIndex].Cells["Btn_Run"];
            bool isBusy = status.OP == "Idle" ? false : true;
            string statusText = isBusy ? "Run" : "Stop";
            
            if (statusCell.Value?.ToString() != statusText)
            {
                
                statusCell.Value = statusText;
            }

            // Status 버튼 색상 업데이트 (Run: Yellow, Stop: WhiteSmoke)
            Color statusBackColor = isBusy ? Color.Yellow : Color.WhiteSmoke;
            if (statusCell.Style.BackColor != statusBackColor)
            {
                statusCell.Style.BackColor = statusBackColor;
                statusCell.Style.SelectionBackColor = statusBackColor;
            }
        }

        private async void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid == null || e.RowIndex < 0) return;

            string colName = grid.Columns[e.ColumnIndex].Name;
            int axisNo = (int)_dataTable.Rows[e.RowIndex]["No"];
            MotionID motorId = (MotionID)axisNo;

            if (colName == "Btn_VelUp")
            {
                // SetVel UP 버튼 클릭
                try
                {
                    double currentVel = Convert.ToDouble(_dataTable.Rows[e.RowIndex]["SetVel"]);
                    _dataTable.Rows[e.RowIndex]["SetVel"] = currentVel + 1.0;
                    Log.Instance.Info($"Motor {axisNo} SetVel UP: {currentVel} -> {currentVel + 1.0}");

                    var status = _act.Motion.GetStatus((MotionID)axisNo);
                    if (status.OP == "Velocity") // 동작 중이면
                    {
                        await _act.Motion.MoveVelAsync((MotionID)axisNo, currentVel + 1.0);
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"SetVel UP error: {ex.Message}");
                }
            }
            else if (colName == "Btn_VelDn")
            {
                // SetVel DN 버튼 클릭
                try
                {
                    double currentVel = Convert.ToDouble(_dataTable.Rows[e.RowIndex]["SetVel"]);
                    _dataTable.Rows[e.RowIndex]["SetVel"] = currentVel - 1.0;
                    Log.Instance.Info($"Motor {axisNo} SetVel DN: {currentVel} -> {currentVel - 1.0}");

                    var status = _act.Motion.GetStatus((MotionID)axisNo);
                    if (status.OP == "Velocity") // 동작 중이면
                    {                       
                        await _act.Motion.MoveVelAsync((MotionID)axisNo, currentVel - 1.0);
                    }
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"SetVel DN error: {ex.Message}");
                }
            }
            else if (colName == "Btn_Set")
            {
                // Status 버튼 클릭 시 현재 상태 표시
                Log.Instance.Info($"Status button clicked for Motor {axisNo} ({motorId})");
                
                try
                {
                    double setVel = Convert.ToDouble(_dataTable.Rows[e.RowIndex]["SetVel"]);
                    double setTrq = Convert.ToDouble(_dataTable.Rows[e.RowIndex]["SetTrq"]);

                    double actVel = Convert.ToDouble(_dataTable.Rows[e.RowIndex]["Vel"]);

                    Log.Instance.Info($"Set button clicked for Motor {axisNo} ({motorId}) - Vel: {setVel}, Trq: {setTrq}");

                    var status = _act.Motion.GetStatus((MotionID)axisNo);

                    if (status.OP == "Velocity") // 동작 중이면
                    {
                        await _act.Motion.SetTorqueAsync((MotionID)axisNo , setTrq);
                        await _act.Motion.MoveVelAsync((MotionID)axisNo, setVel);
                    }

                 //   _act.PopupNoti($"Motor {motorId}\nVel: {setVel}, Trq: {setTrq} 설정됨", NotifyType.Info);
                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"Set button error: {ex.Message}");
                    _act.PopupNoti($"설정 실패: {ex.Message}", NotifyType.Error);
                }
            }
            else if (colName == "Btn_Run")
            {
                // Set 버튼 클릭 시 SetVel, SetTrq 값 적용
                try
                {
                    double setVel = Convert.ToDouble(_dataTable.Rows[e.RowIndex]["SetVel"]);
                    double setTrq = Convert.ToDouble(_dataTable.Rows[e.RowIndex]["SetTrq"]);
                    double actVel = Convert.ToDouble(_dataTable.Rows[e.RowIndex]["Vel"]);

                    Log.Instance.Info($"Set button clicked for Motor {axisNo} ({motorId}) - Vel: {setVel}, Trq: {setTrq}");

                    var status = _act.Motion.GetStatus((MotionID)axisNo);
                    if (status.OP == "Velocity") // 동작 중이면
                    {
                        _act.Motion.MoveStop((MotionID)axisNo);
                    }


                    if (actVel > 0.1) // 동작 중이면 stop
                    {
                        _act.Motion.MoveStop((MotionID)axisNo);
                    }
                    else
                    {                       

                        if(status.AmpAlarm)
                        {
                            _act.Motion.AlarmReset((MotionID)axisNo);
                            await Task.Delay(1000);
                        }
                        if(!status.ServoOn)
                        {
                            _act.Motion.ServoOn((MotionID)axisNo, true);
                            await Task.Delay(1000);
                        }
                        if (!status.HomeDone)
                        {
                            _act.Motion.HomeSearchAsync((MotionID)axisNo);
                            await Task.Delay(1000);
                        }
                       
                        await _act.Motion.SetTorqueAsync((MotionID)axisNo, setTrq);
                        await Task.Delay(100);

                        await _act.Motion.MoveVelAsync((MotionID)axisNo, setVel);
                    }

                }
                catch (Exception ex)
                {
                    Log.Instance.Error($"Set button error: {ex.Message}");
                    _act.PopupNoti($"run 실패: {ex.Message}", NotifyType.Error);
                }
            }
        }
    }
}
