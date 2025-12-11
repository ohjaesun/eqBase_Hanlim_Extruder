using EQ.Common.Helper;
using EQ.UI.UserViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EQ.UI
{
    public partial class Form07STATISTICS : FormBase
    {
        public Form07STATISTICS()
        {
            InitializeComponent();
        }

        private void _Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (panelMain.Controls.Count > 0)
                {                  
                    for (int i = panelMain.Controls.Count - 1; i >= 0; i--)
                    {
                        Control ctrl = panelMain.Controls[i];
                        if (ctrl is Form form)
                        {
                            form.Close(); 
                        }
                        ctrl.Dispose(); 
                    }
                }             
                panelMain.Controls.Clear();

                Button btn = sender as Button;
                var idx = Utils.GetButtonIdx(btn.Name);

                UserControl user = null;
                switch(idx)
                {

                    case 1:
                        {
                            user = new Alarm_View();
                            break;
                        }
                    case 2:
                        {
                            user = new DB_Export_View();
                            break;
                        }
                    case 3:
                        {
                            user = new Statistics_ScottPlot_View();
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
                }

                if(user != null)
                {
                    user.Dock = DockStyle.Fill;
                    panelMain.Controls.Add(user);
                }

            }
            catch { }
        }
    }
}
