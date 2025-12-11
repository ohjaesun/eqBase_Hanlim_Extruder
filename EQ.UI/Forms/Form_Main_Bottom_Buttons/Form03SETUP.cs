using EQ.Common.Helper;
using EQ.Core.Service;
using EQ.UI.Forms;
using EQ.UI.UserViews;

namespace EQ.UI
{
    public partial class Form03SETUP : FormBase
    {
        public Form03SETUP()
        {
            InitializeComponent();
        }

        private void _Button1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            var idx = Utils.GetButtonIdx(btn.Name);

            try
            {
                if (PanelMain.Controls.Count > 0)
                {
                    for (int i = PanelMain.Controls.Count - 1; i >= 0; i--)
                    {
                        Control ctrl = PanelMain.Controls[i];
                        if (ctrl is Form form)
                            form.Close();

                        ctrl.Dispose();
                    }
                }
                PanelMain.Controls.Clear();
            }
            catch { }

            ActManager.Instance.Act.Option.LoadAllOptionsFromStorage();

            switch (idx)
            {
                case 1:
                    {
                        var p = new UserOption_View();                       
                        p.Dock = DockStyle.Fill;
                        PanelMain.Controls.Add(p);
                    }
                    break;
                case 2:
                    {
                        var p = new FormUserOptionUI();
                        p.TopLevel = false;
                        p.Dock = DockStyle.Fill;
                        p.Show();
                        PanelMain.Controls.Add(p);
                    }
                    break;
                case 3:
                    {
                        var p = new Motor_View();                     
                        p.Dock = DockStyle.Fill;
                        PanelMain.Controls.Add(p);
                    }
                    break;
                case 4:
                    {
                        var p = new MotorPosition_View();
                        p.Dock = DockStyle.Fill;
                        PanelMain.Controls.Add(p);
                    }
                    break;
                case 5:
                    {
                        var p = new MotionSpeed_View();
                        p.Dock = DockStyle.Fill;
                        PanelMain.Controls.Add(p);
                    }
                    break;
                case 6:
                    {
                        var p = new IO_View();
                        p.Dock = DockStyle.Fill;
                        PanelMain.Controls.Add(p);
                    }
                    break;
                case 7:
                    {

                    }
                    break;
                case 8:
                    {

                    }
                    break;
                case 9:
                    {

                    }
                    break;
                case 10:
                    {
                        var p = new Recipe_View();
                        p.Dock = DockStyle.Fill;
                        PanelMain.Controls.Add(p);
                    }
                    break;
            }        


        }
    }
}
