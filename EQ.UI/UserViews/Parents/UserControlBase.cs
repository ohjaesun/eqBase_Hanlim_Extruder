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

namespace EQ.UI.UserViews
{
    /// <summary>
    /// title 또는 SAVE 버튼이 필요한 유저 컨트롤의 부모 클래스
    /// </summary>
    public partial class UserControlBase : UserControl
    {
        private List<Action> _cleanupActions = new List<Action>();

        // 최초 로드된 영어 텍스트를 저장하는 캐시
        private Dictionary<Control, string> _originalTexts = new Dictionary<Control, string>();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
            {
                // 1. 현재 화면의 모든 컨트롤 텍스트(영어)를 캡처
                CaptureOriginalTexts(this);

                // 2. 현재 언어로 번역 적용
                UpdateLanguage();

                // 3. 언어 변경 이벤트 구독
                SafeSubscribe(
                    () => ActManager.Instance.Act.Language.OnLanguageChanged += UpdateLanguage,
                    () => ActManager.Instance.Act.Language.OnLanguageChanged -= UpdateLanguage
                );
            }
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
                // 번역된 텍스트가 비어있지 않은 경우에만 업데이트
                if (!string.IsNullOrEmpty(translated) && kvp.Key.Text != translated)
                {
                    kvp.Key.Text = translated;
                }
            }
        }

        protected void SafeSubscribe(Action subscribe, Action unsubscribe)
        {
            // 1. 구독 실행
            subscribe();
            // 2. 해제 로직을 리스트에 보관
            _cleanupActions.Add(unsubscribe);
        }

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var cleanup in _cleanupActions)
                {
                    try
                    {
                        cleanup();
                    }
                    catch { /* 무시 (이미 해제된 경우 등) */ }
                }
                _cleanupActions.Clear();

                EQ.Common.Helper.LeakDetector.Register(this, this.Name);
            }

            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public UserControlBase()
        {
            InitializeComponent();          
        }

        /// <summary>
        /// 디자인 모드인지 확실하게 확인하는 프로퍼티
        /// (중첩된 컨트롤이나 생성자에서도 정확하게 동작함)
        /// </summary>
        [Browsable(false)]
        public new bool DesignMode
        {
            get
            {
                // Visual Studio 디자이너 프로세스인지 확인
                return System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime
                       || base.DesignMode;
            }
        }


    }
}
