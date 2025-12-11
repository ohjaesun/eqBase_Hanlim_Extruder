using EQ.Common.Helper;
using EQ.Core.Act;
using EQ.Core.Sequence;
using EQ.Core.Service;
using EQ.Domain.Enums;
using EQ.Domain.Interface;
using EQ.UI.Forms;
using EQ.UI.UserViews;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using static EQ.Core.Sequence.SEQ;
using static EQ.Infra.HW.IO.HardwareIOFactory;

namespace EQ.UI
{
    public partial class FormMain : FormBase
    {
        private readonly ACT _act;

        readonly DateTime StartTime = DateTime.Now;
        string buildDate;


        Point formMove = new Point();
        public FormMain()
        {
            InitializeComponent();
            _act = ActManager.Instance.Act;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            Form fm = new FormTest();
            fm.TopLevel = false;
            fm.Dock = DockStyle.Fill;
            fm.Show();

            panelMain.Controls.Add(fm);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            timerToolStrip.Interval = 2000;
            timerToolStrip.Start();

            buildDate = GetDisplayVersion();
        }

        private async void _Button_Bottom_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            var idx = Utils.GetButtonIdx(btn.Name);

            //버튼 name의 숫자값을 사용해 인덱싱
            switch (idx)
            {
                case -1: // not found
                    {
                        break;
                    }

                case 9: //admin
                    {
                        Form fm = (Form09Admin)Application.OpenForms["FormAdmin"];
                        if (fm == null)
                        {
                            fm = new Form09Admin();
                            fm.Show();
                        }
                        break;
                    }

                case 10: // EXIT
                    {
                        var r = await _act.PopupYesNo.ConfirmAsync("종료", "프로그램을 종료하시겠습니까?");
                        if (r == YesNoResult.Yes)
                        {
                            var splash = Application.OpenForms.OfType<FormSplash>().FirstOrDefault();

                            if (splash != null)
                            {
                                splash.Show();
                                splash.BringToFront();
                                Application.DoEvents();
                                splash.EndProgram();
                                await Task.Delay(1000);
                            }
                            Application.Exit();
                        }
                        break;
                    }

                default:
                    {
                        try
                        {
                            if (panelMain.Controls.Count > 0)
                            {
                                // 컨트롤이 여러 개일 수 있으므로 안전하게 뒤에서부터 제거
                                for (int i = panelMain.Controls.Count - 1; i >= 0; i--)
                                {
                                    Control ctrl = panelMain.Controls[i];

                                    if (ctrl is Form form)
                                    {
                                        form.Close(); // FormClosing, FormClosed 이벤트 발생 유도
                                    }

                                    ctrl.Dispose(); // 리소스 해제 (Controls 컬렉션에서도 제거됨)
                                }
                            }

                            // 혹시 모를 잔여물 제거 (보통 위 루프에서 다 제거됨)
                            panelMain.Controls.Clear();


                            // 폼
                            Form fm = null;

                            if (idx == 1) fm = new Form01AUTO();
                            if (idx == 2) fm = new Form02MANUAL();
                            if (idx == 3) fm = new Form03SETUP();
                            if (idx == 4) fm = new Form04REV();
                            if (idx == 5) fm = new Form05REV();
                            if (idx == 6) fm = new Form06REV();
                            if (idx == 7) fm = new Form07STATISTICS();
                            if (idx == 8) fm = new FormTest();

                            fm.TopLevel = false;
                            fm.Dock = DockStyle.Fill;
                            fm.Show();

                            panelMain.Controls.Add(fm);
                        }
                        catch
                        {

                        }
                    }
                    break;
            }
        }

        private async void RightClick(object sender, EventArgs e)
        {
            var _act = ActManager.Instance.Act;

            Button btn = sender as Button;
            var idx = Utils.GetButtonIdx(btn.Name);

            switch (idx)
            {
                case 1: // start
                    {

                    }
                    break;
                case 2: // stop
                    {
                        SeqManager.Instance.Seq.StoppingAllSequences();
                    }
                    break;
                case 3: // reset
                    {
                        await _act.SystemResetAsync();
                    }
                    break;
                case 4: // door open
                    {

                    }
                    break;
                case 5: // door close
                    {

                    }
                    break;
                case 6:
                    {

                    }
                    break;
                case 7:
                    {

                    }
                    break;
                case 8: // E-Stop
                    {
                        ActManager.Instance.Act.E_STOP();
                    }
                    break;
            }           

        }



        private string GetDisplayVersion()
        {
            // 1. 엔트리 어셈블리 가져오기
            Assembly assembly = Assembly.GetEntryAssembly();

            // 2. 컴퓨터 이름 가져오기
            string name = Environment.MachineName;

            // 3. 요청하신 포맷으로 빌드 날짜 및 컴퓨터 이름 문자열 생성
            // 예: "[MY-PC] 241126:1645"
            string buildDate = $"[{name}] {System.IO.File.GetLastWriteTime(assembly.Location).ToString("yyMMdd:HHmm")}";

            return buildDate;
        }

        private void timerToolStrip_Tick(object sender, EventArgs e)
        {
            //admin button
            var admin = _act.User.CheckAccess(UserLevel.Admin);
            if (_ButtonBottom9.Visible != admin)
                _ButtonBottom9.Visible = admin;

            // _act.
            int loopCnt = statusStrip1.Items.Count;

            for (int i = 0; i < loopCnt; i++)
            {
                var ctl = statusStrip1.Items[i];
                var strip = ctl as ToolStripStatusLabel;

                switch (i)
                {
                    case 0: //Door Open 
                        {

                        }
                        break;

                    case 1:
                        {
                            break;
                        }

                    case 2:
                        {
                            break;
                        }

                    case 3:
                        {
                            break;
                        }

                    case 4:
                        {
                            break;
                        }

                    case 5:
                        {
                            break;
                        }

                    case 6:
                        {
                            break;
                        }

                    case 7: //last Error No
                        {

                            break;
                        }

                    case 8: //프로그램 실행 시간      
                        {
                            var span = DateTime.Now - StartTime;
                            int totalDays = (int)span.TotalDays;
                            int hours = span.Hours;
                            int minutes = span.Minutes;

                            if (strip.Text != $"{totalDays}일{hours}시간{minutes}분")
                                strip.Text = $"{totalDays}일{hours}시간{minutes}분";
                        }
                        break;
                    case 9: //build date
                        {
                            if (strip.Text != buildDate)
                                strip.Text = buildDate;
                        }
                        break;
                }

                if (strip.Text == "empty")
                    strip.Visible = false;
                else
                    strip.Visible = true;
            }




        }


    }
}
