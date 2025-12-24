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
    public partial class Form10Alarm : FormBase
    {
        public Form10Alarm()
        {
            InitializeComponent();
        }

        private void Form08REV_Load(object sender, EventArgs e)
        {
            var fm = new FormTest();
           
            fm.TopLevel = false;
            fm.Dock = DockStyle.Fill;
            fm.Show();

            this.Controls.Add(fm);
        }
    }
}
