using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace EQ.UI.UserViews
{
    public partial class UserOption_View : UserControl
    {
        public UserOption_View()
        {
            InitializeComponent();
        }

        private void UserOption_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return; // 코드 디자인 모드에서는 탭 보이도록 함

            userControlBase1._LabelTitle.Text = "User Options1";
            userControlBase2._LabelTitle.Text = "User Options2";
            userControlBase3._LabelTitle.Text = "User Options3";
            userControlBase4._LabelTitle.Text = "User Options4";

            userControlBase1._ButtonSave.Click += _ButtonSave_Click;
            userControlBase2._ButtonSave.Click += _ButtonSave_Click;
            userControlBase3._ButtonSave.Click += _ButtonSave_Click;
            userControlBase4._ButtonSave.Click += _ButtonSave_Click;

            var act = ActManager.Instance.Act;
            act.Option.LoadAllOptionsFromStorage();

            //관리자 레벨 옵션
            bool isEngineerOrAdmin = act.User.CheckAccess(UserLevel.Engineer);
            userControlBase4.Enabled = isEngineerOrAdmin;

            // 직접 참조하면 UI 변경시 바로 값 바뀜으로 깊은 복사본 (새로운 객체)을 만들어서 사용
            var original1 = ActManager.Instance.Act.Option.Get<UserOption1>();
            var original2 = ActManager.Instance.Act.Option.Get<UserOption2>();
            var original3 = ActManager.Instance.Act.Option.Get<UserOption3>();
            var original4 = ActManager.Instance.Act.Option.Get<UserOption4>();

            PropertyGrid grid1 = new PropertyGrid();
            grid1.Dock = DockStyle.Fill;
            grid1.PropertySort = PropertySort.Categorized;            
            grid1.SelectedObject = CreateDeepCopy(original1);             
            userControlBase1._PanelMain.Controls.Add(grid1);

            PropertyGrid grid2 = new PropertyGrid();
            grid2.Dock = DockStyle.Fill;
            grid2.PropertySort = PropertySort.Categorized;
            grid2.SelectedObject = CreateDeepCopy(original2);
            userControlBase2._PanelMain.Controls.Add(grid2);

            PropertyGrid grid3 = new PropertyGrid();
            grid3.Dock = DockStyle.Fill;
            grid3.PropertySort = PropertySort.Categorized;
            grid3.SelectedObject = CreateDeepCopy(original3);
            userControlBase3._PanelMain.Controls.Add(grid3);

            PropertyGrid grid4 = new PropertyGrid();
            grid4.Dock = DockStyle.Fill;
            grid4.PropertySort = PropertySort.Categorized;
            grid4.SelectedObject = CreateDeepCopy(original4);
            userControlBase4._PanelMain.Controls.Add(grid4);        

            Disposed += UserOption_View_Disposed;

           
        }
        private T CreateDeepCopy<T>(T obj) // 새로 객체를 만든다
        {           
            var json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(json);
        }

        private void UserOption_View_Disposed(object? sender, EventArgs e)
        {
            userControlBase1._ButtonSave.Click -= _ButtonSave_Click;
            userControlBase2._ButtonSave.Click -= _ButtonSave_Click;
            userControlBase3._ButtonSave.Click -= _ButtonSave_Click;
            userControlBase4._ButtonSave.Click -= _ButtonSave_Click;
        }

        private async void _ButtonSave_Click(object? sender, EventArgs e)
        {
            var s = sender as Button;

            var act = ActManager.Instance.Act;
            string savedOption = "";

            if (s.Parent?.Parent is UserControlBase parentUserControl)
            {
            //    var r = await act.PopupYesNo.ConfirmAsync("SAVE", "SAVE", NotifyType.Info);
            //   if( r != YesNoResult.Yes)                
            //        return;
                
                if (parentUserControl == this.userControlBase1)
                {
                    savedOption = nameof(UserOption1);                    
                    var modifiedCopy = (UserOption1)(parentUserControl._PanelMain.Controls[0] as PropertyGrid).SelectedObject;                  
                    act.Option.Set<UserOption1>(modifiedCopy);
                    await act.Option.Save<UserOption1>();

                    act.Language.ChangeLanguage(); // 언어 설정 변경 이벤트 날림
                }
                else if (parentUserControl == this.userControlBase2)
                {
                    savedOption = nameof(UserOption2);
                    var modifiedCopy = (UserOption2)(parentUserControl._PanelMain.Controls[0] as PropertyGrid).SelectedObject;
                    act.Option.Set<UserOption2>(modifiedCopy);
                    await act.Option.Save<UserOption2>();
                }
                else if (parentUserControl == this.userControlBase3)
                {
                   

                    savedOption = nameof(UserOption3);
                    var modifiedCopy = (UserOption3)(parentUserControl._PanelMain.Controls[0] as PropertyGrid).SelectedObject;
                    act.Option.Set<UserOption3>(modifiedCopy);
                    await act.Option.Save<UserOption3>();
                  
                }
                else if (parentUserControl == this.userControlBase4)
                {
                    savedOption = nameof(UserOption4);
                    var modifiedCopy = (UserOption4)(parentUserControl._PanelMain.Controls[0] as PropertyGrid).SelectedObject;
                    act.Option.Set<UserOption4>(modifiedCopy);
                    await act.Option.Save<UserOption4>();
                }                
            }
        }
    }
}
