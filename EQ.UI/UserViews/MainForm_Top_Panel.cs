using EQ.Common.Helper;
using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Core.Sequence;
using EQ.Core.Service;
using EQ.Domain.Enums;
using EQ.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EQ.Core.Sequence.SEQ;
using static OpenTK.Graphics.OpenGL.GL;

namespace EQ.UI.UserViews
{
    public partial class MainForm_Top_Panel : UserControl
    {
        private Point formMove = new Point();
        public MainForm_Top_Panel()
        {
            InitializeComponent();
        }

        private void MainForm_Top_Panel_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            // 버전 표시 
            _LabelVer.Text = VersionHelper.GetDisplayVersion();

            _Label_rcpName.MouseDoubleClick += (s, e1) =>
            {
                Form parentForm = this.FindForm();
                if (parentForm != null)
                {
                    if (parentForm.WindowState == FormWindowState.Maximized)
                        parentForm.WindowState = FormWindowState.Normal;
                    else
                        parentForm.WindowState = FormWindowState.Maximized;
                }
            };

            //타이틀 영역을 이용해 메인폼 마우스로 이동 
            _Label_Title.MouseDown += (s, e1) =>
            {
                formMove = e1.Location;
            };
            _Label_Title.MouseDoubleClick += (s, e1) =>
            {
                Form parentForm = this.FindForm();

                if (parentForm != null)
                {
                    // 부모 폼의 위치를 (0,0)으로 이동
                    parentForm.Location = new Point(0, 0);
                }
            };
            _Label_Title.MouseMove += (s, e1) =>
            {
                if ((e1.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    // 부모 폼(MainForm)을 찾음
                    Form parentForm = this.FindForm();

                    if (parentForm != null)
                    {
                        // 이동 거리 계산 (현재 마우스 위치 - 클릭했던 위치)
                        int deltaX = e1.X - formMove.X;
                        int deltaY = e1.Y - formMove.Y;

                        // 부모 폼의 위치 이동
                        parentForm.Location = new Point(parentForm.Left + deltaX, parentForm.Top + deltaY);
                    }
                }
            };


            timer100.Interval = 100;
            timer100.Start();
            timerFlicker.Interval = 500;
            timerFlicker.Start();
            timer1000.Interval = 1000;
            timer1000.Start();

            timer100.Tick += Timer100_Tick;
            timerFlicker.Tick += timerFlicker_Tick;
            timer1000.Tick += Timer1000_Tick;

            Disposed += MainForm_Top_Panel_Disposed;

            updateTitle();
                        
            
        }

        private void updateTitle()
        {
            CIni ini = new CIni();
            if (ini.ReadBool("SYSTEM", "Simulation"))
            {
                _Label_Title.Text = "시뮬레이션";
                _Label_Title.ThemeStyle = UI.Controls.ThemeStyle.Danger_Red;
            }
        }

        private void MainForm_Top_Panel_Disposed(object? sender, EventArgs e)
        {
            timer100.Tick -= Timer100_Tick;
            timerFlicker.Tick -= timerFlicker_Tick;
            timer1000.Tick -= Timer1000_Tick;

            Disposed -= MainForm_Top_Panel_Disposed;
        }

        private void Timer1000_Tick(object? sender, EventArgs e)
        {

        }

        private void timerFlicker_Tick(object? sender, EventArgs e)
        {
            var act = ActManager.Instance.Act;
            var level = act.User.CurrentUserLevel;

            // 현재 색상이 '기본(회색)'이면 -> 레벨 색상(강조)으로 변경
            if (_LabelUser.ThemeStyle == UI.Controls.ThemeStyle.Neutral_Gray)
            {
                if (level == UserLevel.Admin)
                    _LabelUser.ThemeStyle = UI.Controls.ThemeStyle.Danger_Red;
                else // Engineer
                    _LabelUser.ThemeStyle = UI.Controls.ThemeStyle.Info_Sky;
            }
            // 현재 색상이 '강조' 상태면 -> 기본(회색)으로 변경
            else
            {
                _LabelUser.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
            }
        }

        private void Timer100_Tick(object? sender, EventArgs e)
        {

        }

        private void timer1000_Tick_1(object sender, EventArgs e)
        {
            UpdateTiem();
            UpdateLoginUser();
            UpdateRcpName();
            UpdateSequenceStatus();
            UpdateActionStatus();
            UpdateLampStatus();
        }

        private void UpdateLampStatus()
        {
            // 1. ActTowerLamp에서 계산된 상태 가져오기
            var state = ActManager.Instance.Act.TowerLamp.GetState();

            ThemeStyle style = ThemeStyle.Neutral_Gray;

            // 2. 상태에 따른 색상 결정
            switch (state)
            {
                case EqState.Error:
                    style = ThemeStyle.Danger_Red;
                    break;

                case EqState.Running:
                    style = ThemeStyle.Warning_Yellow;
                    break;

                case EqState.Idle:
                    style = ThemeStyle.Success_Green;
                    break;

                case EqState.Init:
                default:
                    style = ThemeStyle.Neutral_Gray;
                    break;
            }           

            // 4. 스타일(색상)만 업데이트
            if (_LabelLamp.ThemeStyle != style)
            {
                _LabelLamp.ThemeStyle = style;              
            }
        }

        private void UpdateRcpName()
        {
            var act = ActManager.Instance.Act;

            if(_Label_rcpName.Text != act.Recipe.CurrentRecipeName)
                _Label_rcpName.Text = act.Recipe.CurrentRecipeName;
        }

        private void UpdateActionStatus()
        {
            // 1. 상태 가져오기
            ActionStatus currentStatus = ActManager.Instance.Act.GetActionStatus();

            ThemeStyle themeStyle;

            // 2. 상태에 따른 색상(ThemeStyle) 결정 (텍스트 로직 제거)
            switch (currentStatus)
            {
                case ActionStatus.Error:
                case ActionStatus.Timeout:
                    themeStyle = ThemeStyle.Danger_Red;
                    break;

                case ActionStatus.Running:
                    themeStyle = ThemeStyle.Success_Green;
                    break;

                case ActionStatus.Finished:
                default:
                    themeStyle = ThemeStyle.Neutral_Gray;
                    break;
            }

            // 3. 테마 스타일 업데이트 (값이 변경된 경우에만 적용하여 깜빡임 방지)
            if (_Label4.ThemeStyle != themeStyle)
                _Label4.ThemeStyle = themeStyle;
        }

        private void UpdateSequenceStatus()
        {
            var seqManager = SeqManager.Instance.Seq;
            bool anyError = false;
            bool anyRun = false;

            // 모든 시퀀스 상태 확인
            foreach (SEQ.SeqName name in Enum.GetValues(typeof(SEQ.SeqName)))
            {
                var sequence = seqManager.GetSequence(name);
                if (sequence == null) continue;

                // 1. 에러 상태 확인 (최우선)
                if (sequence._Status == SeqStatus.ERROR || sequence._Status == SeqStatus.TIMEOUT)
                {
                    anyError = true;
                    break;
                }
                // 2. 실행 중 상태 확인
                else if (sequence._Status == SeqStatus.RUN || sequence._Status == SeqStatus.SEQ_STOPPING)
                {
                    anyRun = true;
                }
            }

            // 상태에 따른 색상 결정 (텍스트 로직 제거)
            ThemeStyle themeStyle;

            if (anyError)
            {
                ActManager.Instance.Act.TowerLamp.SetState(EqState.Error);                                            
                themeStyle = ThemeStyle.Danger_Red;
            }
            else if (anyRun)
            {
                ActManager.Instance.Act.TowerLamp.SetState(EqState.Running);
                themeStyle = ThemeStyle.Success_Green;
            }
            else
            {
                ActManager.Instance.Act.TowerLamp.SetState(EqState.Idle);
                themeStyle = ThemeStyle.Neutral_Gray;
            }

            // UI 업데이트 (색상만 변경)
            if (_Label_SeqStatus.ThemeStyle != themeStyle)
                _Label_SeqStatus.ThemeStyle = themeStyle;
        }

        private void UpdateLoginUser()
        {
            var act = ActManager.Instance.Act;
            var level = act.User.CurrentUserLevel;
            string levelText = level.ToString();

            // 1. 텍스트 업데이트 (변경되었을 때만 수행하여 깜박임 방지)
            if (_LabelUser.Text != levelText)
            {
                _LabelUser.Text = levelText;
            }

            // 2. 권한에 따른 타이머 제어 (핵심 로직)
            if (level >= UserLevel.Engineer) // Engineer(5) 또는 Admin(10)
            {
                // 플리커 타이머가 꺼져있다면 -> 켠다
                if (!timerFlicker.Enabled)
                {
                    timerFlicker.Start();
                }
                // (참고: 1초마다 호출되어도 Enabled 상태면 아무것도 안 하므로 안전함)
            }
            else // Operator (1)
            {
                // 플리커 타이머가 켜져있다면 -> 끈다
                if (timerFlicker.Enabled)
                {
                    timerFlicker.Stop();
                    // 끄면서 색상을 즉시 '기본색'으로 원복 (깔끔한 마무리)
                    _LabelUser.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
                }
                // (혹시 타이머 없이 색상이 잘못되어 있을 경우를 대비해 강제 고정)
                else if (_LabelUser.ThemeStyle != UI.Controls.ThemeStyle.Neutral_Gray)
                {
                    _LabelUser.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
                }
            }
        }

        private void UpdateTiem()
        {
            _LabelTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void _Label8_Click(object sender, EventArgs e)
        {
            FormLogin loginForm = new FormLogin();
            loginForm.ShowDialog();
        }
    }
}
