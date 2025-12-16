using EQ.Core.Service;
using System;
using System.Windows.Forms;

using static EQ.Core.Globals;

namespace EQ.UI.UserViews.Extruder
{
    /// <summary>
    /// Extruder 설정 화면 - HMI 스타일 UI
    /// Recipe/Batch, Parameter, Safety, Part bins 섹션으로 구성
    /// </summary>
    public partial class ExtruderOperation_View : UserControlBaseplain
    {       

        public ExtruderOperation_View()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;
           
        }     
      

    }
}
