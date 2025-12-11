using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Core.Sequence;
using EQ.Core.Service;
using EQ.Domain.Enums;
using EQ.UI.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;
using static EQ.Core.Sequence.SEQ;

namespace EQ.UI.UserViews
{
    // [변경] UserControlBase -> UserControlBaseplain 상속
    public partial class Sequence_View : UserControlBaseplain
    {
        private SEQ _seq;
        private SeqName _seqName;
        private ISeqInterface _sequence;

        public Sequence_View()
        {
            InitializeComponent();
        }

        private void Sequence_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            // _ButtonSave.Visible = false; // [삭제] 부모에 버튼이 없으므로 삭제

            // 그리드 스타일 설정
            _DataGridViewSteps.ColumnHeadersVisible = false;
            _DataGridViewSteps.Columns[0].DefaultCellStyle.BackColor = Color.Black;
            _DataGridViewSteps.Columns[0].DefaultCellStyle.ForeColor = Color.White;
            _DataGridViewSteps.Columns[0].ReadOnly = true;
            _DataGridViewSteps.Columns[1].Width = 60;
            _DataGridViewSteps.Columns[1].ReadOnly = true;
        }

        public void InitializeSequence(SeqName seqName)
        {
            _seq = SeqManager.Instance.Seq;
            _seqName = seqName;
            _sequence = _seq.GetSequence(_seqName);

            if (_sequence == null)
            {
                MessageBox.Show($"시퀀스 '{seqName}'가 SeqManager에 등록되지 않았습니다.");
                this.Enabled = false;
                return;
            }

            // [유지] 타이틀 라벨 설정 (이제 이 컨트롤의 멤버임)
            _LabelTitle.Text = _seqName.ToString();

            _DataGridViewSteps.DataSource = _sequence.GetDataTable();

            // 라벨 초기 표시 설정
            _LabelSet.Visible = true;
            _LabelWait.Visible = true;

            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            if (_sequence == null) return;

            var status = _sequence._Status;
            var stepString = _sequence._StepString;

            // 1. 상태 및 스텝 텍스트 업데이트
            if (_LabelStatus.Text != status.ToString())
                _LabelStatus.Text = status.ToString();

            if (_LabelStep.Text != stepString)
                _LabelStep.Text = stepString;

            // 2. 버튼 활성화 상태
            bool isStopped = (status == SeqStatus.STOP);
            if (_ButtonRun.Enabled != isStopped)
                _ButtonRun.Enabled = isStopped;
            if (_ButtonStep.Enabled != isStopped)
                _ButtonStep.Enabled = isStopped;
            if (_ButtonStop.Enabled == isStopped)
                _ButtonStop.Enabled = !isStopped;

            // 3. 상태별 테마
            var newTheme = status switch
            {
                SeqStatus.RUN => ThemeStyle.Success_Green,
                SeqStatus.SEQ_STOPPING => ThemeStyle.Warning_Yellow,
                SeqStatus.ERROR or SeqStatus.TIMEOUT => ThemeStyle.Danger_Red,
                _ => ThemeStyle.Neutral_Gray,
            };

            if (_LabelStatus.ThemeStyle != newTheme)
                _LabelStatus.ThemeStyle = newTheme;

            // -------------------------------------------------------
            // 4. 신호 상태 라벨 (Wait / Set) 업데이트
            // -------------------------------------------------------

            // (A) Wait Signal
            string waitSignal = _sequence._WaitSignalName;
            if (!string.IsNullOrEmpty(waitSignal))
            {
                _LabelWait.Text = $"Wait: {waitSignal}";
                // _LabelWait.Visible = true; // (Init에서 true로 설정됨)

                // 대기 중일 때 상태창 노란색 강조 (실행 중일 때만)
                if (status == SeqStatus.RUN && _LabelStatus.ThemeStyle != ThemeStyle.Warning_Yellow)
                    _LabelStatus.ThemeStyle = ThemeStyle.Warning_Yellow;
            }
            else
            {
                _LabelWait.Text = "Wait: None";
                // _LabelWait.Visible = false; // (요청하신대로 항상 보이게 하려면 주석 처리)
            }

            // (B) Set Signal
            string setSignal = _sequence._SetSignalName;
            if (!string.IsNullOrEmpty(setSignal))
            {
                _LabelSet.Text = $"Set: {setSignal}";
            }
            else
            {
                _LabelSet.Text = "Set: None";
            }
            // -------------------------------------------------------

            // 5. 그리드 시간 업데이트 및 하이라이트
            _DataGridViewSteps.SuspendLayout();
            var elsp = _sequence._StepTime;
            for (int i = 0; i < _DataGridViewSteps.Rows.Count; i++)
            {
                var row = _DataGridViewSteps.Rows[i];
                var name = (string)row.Cells[0].Value;

                if (elsp.ContainsKey(name))
                {
                    row.Cells[1].Value = elsp[name].ElapsedMilliseconds;
                }

                if (name == stepString)
                {
                    if (!row.Selected) row.Selected = true;
                }
                else
                {
                    if (row.Selected) row.Selected = false;
                }
            }
            _DataGridViewSteps.ResumeLayout(false);
        }

        private void _ButtonRun_Click(object sender, EventArgs e)
        {
            if (_sequence == null) return;
            _sequence._Step = 0;
            _seq.RunSequence(_seqName);
        }

        private async void _ButtonStep_Click(object sender, EventArgs e)
        {
            if (_sequence == null || _sequence._Status != SeqStatus.STOP) return;

            try
            {
                _sequence._Status = SeqStatus.RUN;
                await _sequence.doSequence();
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"Step 실행 오류 ({_seqName}): {ex.Message}");
                _sequence._Status = SeqStatus.ERROR;
            }
            finally
            {
                if (_sequence._Status == SeqStatus.RUN)
                {
                    _sequence._Status = SeqStatus.STOP;
                }
            }
            _timer_Tick(null, null);
        }

        private void _ButtonStop_Click(object sender, EventArgs e)
        {
            if (_sequence == null) return;
            if (_sequence._Status == SeqStatus.RUN)
            {
                _sequence._Status = SeqStatus.SEQ_STOPPING;
            }
        }

        private void _DataGridViewSteps_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (_sequence == null || e.RowIndex < 0) return;

            if (_sequence._Status == SeqStatus.STOP)
            {
                _sequence._Step = e.RowIndex;
                _LabelStep.Text = _sequence._StepString;
            }
        }

        private void Sequence_View_Disposed(object sender, EventArgs e)
        {
            _timer?.Stop();
        }
    }
}