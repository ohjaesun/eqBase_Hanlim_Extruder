using EQ.Core.Service;
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
    public partial class FormBase : Form
    {
        // 최초 로드된 영어 텍스트를 저장하는 캐시
        private Dictionary<Control, string> _originalTexts = new Dictionary<Control, string>();

        public FormBase()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                // 1. 현재 화면의 모든 컨트롤 텍스트(영어)를 캡처
                CaptureOriginalTexts(this);

                // 2. 현재 언어로 번역 적용
                UpdateLanguage();

                ActManager.Instance.Act.Language.OnLanguageChanged += UpdateLanguage;

                Disposed += FormBase_Disposed;
            }
        }

        private void FormBase_Disposed(object? sender, EventArgs e)
        {
            Disposed -= FormBase_Disposed;
            ActManager.Instance.Act.Language.OnLanguageChanged -= UpdateLanguage;
        }

        private void CaptureOriginalTexts(Control parent)
        {
            if (!string.IsNullOrEmpty(parent.Text) && !_originalTexts.ContainsKey(parent))
                _originalTexts[parent] = parent.Text;

            foreach (Control c in parent.Controls)
            {
                if (!string.IsNullOrEmpty(c.Text) && !_originalTexts.ContainsKey(c))
                    _originalTexts[c] = c.Text;

                if (c.Controls.Count > 0) CaptureOriginalTexts(c);
            }
        }

        protected virtual void UpdateLanguage()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UpdateLanguage));
                return;
            }

            var actLang = ActManager.Instance.Act.Language;
            foreach (var kvp in _originalTexts)
            {
                // 원본(영어)을 키로 사용하여 번역된 텍스트를 가져옴
                string translated = actLang.GetText(kvp.Value);
                if (kvp.Key.Text != translated) kvp.Key.Text = translated;
            }
        }
    }
}
