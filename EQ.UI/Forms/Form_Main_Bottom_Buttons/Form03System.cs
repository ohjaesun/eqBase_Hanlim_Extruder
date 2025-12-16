using EQ.Common.Helper;
using EQ.Core.Service;
using EQ.UI.Forms;
using EQ.UI.UserViews;

namespace EQ.UI
{
    public partial class Form03System : FormBase
    {
        public Form03System()
        {
            InitializeComponent();
        }

     

        private void extruderSystemGroup1_View1_Load(object sender, EventArgs e)
        {

        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            TabPage tabPage = tabControl1.TabPages[e.Index];
            Rectangle tabBounds = tabControl1.GetTabRect(e.Index);

            // 배경 그리기
            if (e.State == DrawItemState.Selected)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(230, 230, 230)), tabBounds);
            }
            else
            {
                g.FillRectangle(new SolidBrush(Color.White), tabBounds);
            }

            // 텍스트를 세로로 그리기 (위에서 아래로)
            g.TranslateTransform(tabBounds.X, tabBounds.Y);
            g.RotateTransform(90);

            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            using (SolidBrush brush = new SolidBrush(Color.Black))
            {
                g.DrawString(tabPage.Text,
                    new Font("D2Coding", 11F, FontStyle.Bold),
                    brush,
                    new RectangleF(0, 0, tabBounds.Height, tabBounds.Width),
                    sf);
            }

            g.ResetTransform();
        }
    }
}
