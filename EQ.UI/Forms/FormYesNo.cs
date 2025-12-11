// EQ.UI/FormYesNo.cs
using EQ.Domain.Enums;
using EQ.UI.Controls;
using System.Windows.Forms;

namespace EQ.UI
{
    // (이 폼은 FormNotify.Show()처럼 정적으로 호출되지 않고
    //  아래 UIConfirmationService에 의해서만 사용됩니다)
    public partial class FormYesNo : FormBase
    {
        public FormYesNo() // 디자이너용
        {
            InitializeComponent();
        }

        public FormYesNo(string title, string message, NotifyType type)
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog; // 테두리
            this.StartPosition = FormStartPosition.CenterParent;
            this.TopMost = true;
            this.ShowInTaskbar = false;

            // 내용 및 테마 적용
            this._LabelTitle.Text = title;
            this.labelMessage.Text = message;
            SetTheme(type);

            // (중요) 버튼 클릭 시 DialogResult 설정
            this._ButtonYes.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Yes;
                this.Close();
            };
            this._ButtonNo.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            };
        }

        private void SetTheme(NotifyType type)
        {
            // (FormNotify와 동일한 테마 설정 로직)
            ThemeStyle style = type switch
            {
                NotifyType.Info => ThemeStyle.Info_Sky,
                NotifyType.Error => ThemeStyle.Danger_Red,
                _ => ThemeStyle.Warning_Yellow, // 기본값 경고
            };

            this._PanelTitle.ThemeStyle = style;
            this._LabelTitle.ThemeStyle = style;
            this._ButtonYes.ThemeStyle = style;
            this._ButtonNo.ThemeStyle = ThemeStyle.Neutral_Gray; // No는 회색
        }
    }
}