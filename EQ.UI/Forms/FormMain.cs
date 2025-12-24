using EQ.Common.Helper;
using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Enums;
using EQ.UI.Forms;
using System.Reflection;

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

        }

        DateTime preDateTime = DateTime.Now;

        protected override void WndProc(ref Message m)
        {
            const int WM_PARENTNOTIFY = 0x0210;
            // 마우스 메시지
            const int WM_MOUSEMOVE = 0x0200;
            const int WM_LBUTTONDOWN = 0x0201;
            const int WM_LBUTTONUP = 0x0202;
            const int WM_RBUTTONDOWN = 0x0204;
            const int WM_RBUTTONUP = 0x0205;
            const int WM_MBUTTONDOWN = 0x0207;
            const int WM_MBUTTONUP = 0x0208;
            const int WM_MOUSEWHEEL = 0x020A;
            // 키보드 메시지
            const int WM_KEYDOWN = 0x0100;
            const int WM_KEYUP = 0x0101;
            const int WM_SYSKEYDOWN = 0x0104;
            const int WM_SYSKEYUP = 0x0105;
            // 터치 메시지
            const int WM_TOUCH = 0x0240;
            switch (m.Msg)
            {
                case WM_PARENTNOTIFY:
                case WM_MOUSEMOVE:
                case WM_LBUTTONDOWN:
                case WM_LBUTTONUP:
                case WM_RBUTTONDOWN:
                case WM_RBUTTONUP:
                case WM_MBUTTONDOWN:
                case WM_MBUTTONUP:
                case WM_MOUSEWHEEL:
                case WM_KEYDOWN:
                case WM_KEYUP:
                case WM_SYSKEYDOWN:
                case WM_SYSKEYUP:
                case WM_TOUCH:
                    // 특정 시간동안 입력이 없는것 감지 하기 위해... 현재는 아무것도 안함
                    preDateTime = DateTime.Now;
                    break;
            }

            base.WndProc(ref m);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);

            timerToolStrip.Interval = 2000;
            timerToolStrip.Start();

            buildDate = GetDisplayVersion();

            // Start 1s timer to update date/time label
            timer1000.Interval = 1000;
            timer1000.Start();
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

                            if (idx == 1) fm = new Form01Setup();
                            if (idx == 2) fm = new Form02Operation();
                            if (idx == 3) fm = new Form03System();
                            if (idx == 4) fm = new Form04Trends();
                            if (idx == 5) fm = new Form05User();
                            if (idx == 6) fm = new Form06Parameter();
                            if (idx == 7) fm = new Form07Reporting();
                            if (idx == 8) fm = new FormTest();
                            
                            if (idx == 10) fm = new Form10Alarm();

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

            // 3. 
            // 예: "[MY-PC] 241126:1645"
            string buildDate = $"[{name}] {System.IO.File.GetLastWriteTime(assembly.Location).ToString("yyMMdd:HHmm")}";
            
            if(File.Exists(Application.StartupPath + "\\git_info.txt"))
            {
                //첫째줄만 읽어서 = 뒤에 값 가져옴
                string gitInfo = File.ReadLines(Application.StartupPath + "\\git_info.txt").First();
                string gitHash = gitInfo.Split('=')[1];
                buildDate += $" [{gitHash}]";
                
            }
               
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

        private void timer1000_Tick(object sender, EventArgs e)
        {
            //login level display
            var level = _act.User.CurrentUserLevel;
            if (level == UserLevel.Lock && _LabelLogin.ThemeStyle != UI.Controls.ThemeStyle.Neutral_Gray)
            {
                _LabelLogin.ThemeStyle = UI.Controls.ThemeStyle.Neutral_Gray;
                _LabelLogin.Text = "LOCK";
            }
            else if (level == UserLevel.Operator && _LabelLogin.ThemeStyle != UI.Controls.ThemeStyle.Success_Green)
            {
                _LabelLogin.ThemeStyle = UI.Controls.ThemeStyle.Success_Green;
                _LabelLogin.Text = "OPERATOR";
            }

            //Flicker 기능
            if (level == UserLevel.Engineer)
            {
                _LabelLogin.ThemeStyle = (_LabelLogin.ThemeStyle == UI.Controls.ThemeStyle.Info_Sky) ? UI.Controls.ThemeStyle.Highlight_DeepYellow : UI.Controls.ThemeStyle.Info_Sky;
                _LabelLogin.Text = "ENGINEER";
            }
            if (level == UserLevel.Admin)
            {
                _LabelLogin.ThemeStyle = (_LabelLogin.ThemeStyle == UI.Controls.ThemeStyle.Info_Sky) ? UI.Controls.ThemeStyle.Danger_Red : UI.Controls.ThemeStyle.Info_Sky;
                _LabelLogin.Text = "ADMIN";
            }


            // Update top-right date/time label once per second
            try
            {
                var now = DateTime.Now;
                string text = now.ToString("yyyy-MM-dd HH:mm:ss");

                if (_LabelDate.Text != text)
                    _LabelDate.Text = text;
            }
            catch
            {
                // ignore UI update errors
            }
        }

        private void _Label8_Click(object sender, EventArgs e)
        {
            //레벨에 안맞는 폼 열려 있을 수 있음 다 닫기
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


            Form login = (FormLogin)Application.OpenForms["FormLogin"];
            if (login == null)
            {
                login = new FormLogin();
                login.ShowDialog();
            }
        }

        private void _Button2_Click(object sender, EventArgs e)
        {
            Form fm = (FormAdminTest)Application.OpenForms["FormAdminTest"];
            if (fm == null)
            {
                fm = new FormAdminTest();
                fm.Show();
            }
        }

        private async void _Label5_Click(object sender, EventArgs e)
        {
            //Exit confirmation
            var r = ActManager.Instance.Act.PopupYesNo.ConfirmAsync("EXIT", "EXIT");

            if (r.Result == YesNoResult.Yes)
            {                
                _act.AuditTrail.RecordSystemShutdown();

                //FormSplash 종료 처리 실행
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

        }
    }
}
