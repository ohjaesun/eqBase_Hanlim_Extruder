// EQ.UI/Services/UIConfirmationService.cs
using EQ.Domain.Enums;
using EQ.Domain.Interface;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;

namespace EQ.UI.Services
{
    /// <summary>
    /// YesNo 확인 팝업을 UI 스레드에서 띄우는 서비스 구현체
    /// </summary>
    public class UIConfirmationService : IConfirmationService
    {
        public Task<YesNoResult> ConfirmAsync(string title, string message, NotifyType type)
        {           
            var tcs = new TaskCompletionSource<YesNoResult>();
            
            Form mainForm = Application.OpenForms.Cast<Form>().FirstOrDefault();
            if (mainForm == null)
            {
                tcs.SetResult(YesNoResult.Cancel); // 메인 폼이 없으면 취소
                return tcs.Task;
            }

            mainForm.Invoke((Action)(() =>
            {
                Point currentMousePosition = Cursor.Position;
                Screen activeScreen = Screen.FromPoint(currentMousePosition);

                using (var form = new FormYesNo(title, message, type))
                {
                    form.StartPosition = FormStartPosition.Manual;
                    Rectangle bounds = activeScreen.WorkingArea;
                    form.Left = bounds.Left + (bounds.Width - form.Width) / 2;
                    form.Top = bounds.Top + (bounds.Height - form.Height) / 2;

                    var result = form.ShowDialog();

                    // 결과를 Task에 설정
                    tcs.SetResult(result switch
                    {
                        DialogResult.Yes => YesNoResult.Yes,
                        DialogResult.No => YesNoResult.No,
                        _ => YesNoResult.Cancel
                    });
                }
            }));
           
            return tcs.Task;
        }
    }
}