using System;
using System.Windows.Forms;
using EQ.UI.Controls;
using EQ.Core.Service;
using EQ.Domain.Enums;

using static EQ.Core.Globals;

namespace EQ.UI.Forms
{
    /// <summary>
    /// 문자열 입력용 QWERTY 키보드 (숫자 포함)
    /// </summary>
    public partial class FormKeyboard : FormBase
    {
        private string _inputString = "";
        private bool _isCapsLock = false;
        private System.Drawing.Point formMove;

        public string ResultValue { get; private set; }

        public FormKeyboard(string title = "Input", string initialValue = "")
        {
            InitializeComponent();
            
            if (!string.IsNullOrEmpty(title))
            {
                _LabelTitle.Text = title;
            }

            _inputString = initialValue ?? "";
            ResultValue = _inputString;
            UpdateDisplay();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            // 문자/숫자 키 이벤트 등록
            foreach (Control ctrl in tableLayoutPanelKeys.Controls)
            {
                if (ctrl is _Button btn && btn.Tag != null && btn.Tag.ToString() == "char")
                {
                    btn.Click += Char_Click;
                }
            }

            // 특수 키 이벤트
            _btnBack.Click += Back_Click;
            _btnEnter.Click += Enter_Click;
            _btnSpace.Click += Space_Click;
            _btnCapsLock.Click += CapsLock_Click;
            _btnUnderscore.Click += Underscore_Click;
            _btnCancel.Click += Cancel_Click;

            // 타이틀 영역 드래그 이동
            _LabelTitle.MouseDown += (s, e1) =>
            {
                formMove = new System.Drawing.Point(e1.X, e1.Y);
            };
            _LabelTitle.MouseDoubleClick += (s, e1) =>
            {
                this.Location = new System.Drawing.Point(0, 0);
            };
            _LabelTitle.MouseMove += (s, e1) =>
            {
                if ((e1.Button & MouseButtons.Left) == MouseButtons.Left)
                {
                    this.Location = new System.Drawing.Point(this.Left - (formMove.X - e1.X), this.Top - (formMove.Y - e1.Y));
                }
            };
        }

        private void UpdateDisplay()
        {
            _LabelDisplay.Text = _inputString;
        }

        private void Char_Click(object sender, EventArgs e)
        {
            if (sender is _Button btn)
            {
                string ch = btn.Text;
                // 숫자키는 대소문자 변환 안함
                if (!char.IsDigit(ch[0]) && !_isCapsLock)
                {
                    ch = ch.ToLower();
                }
                _inputString += ch;
                UpdateDisplay();
            }
        }

        private void Underscore_Click(object sender, EventArgs e)
        {
            _inputString += "_";
            UpdateDisplay();
        }

        private void Space_Click(object sender, EventArgs e)
        {
            _inputString += " ";
            UpdateDisplay();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (_inputString.Length > 0)
            {
                _inputString = _inputString.Substring(0, _inputString.Length - 1);
            }
            UpdateDisplay();
        }

        private void CapsLock_Click(object sender, EventArgs e)
        {
            _isCapsLock = !_isCapsLock;
            _btnCapsLock.BackColor = _isCapsLock 
                ? System.Drawing.Color.FromArgb(46, 204, 113)  // Green when active
                : System.Drawing.Color.FromArgb(52, 73, 94);   // Dark when inactive
            
            // 문자 버튼만 대소문자 전환 (숫자는 제외)
            foreach (Control ctrl in tableLayoutPanelKeys.Controls)
            {
                if (ctrl is _Button btn && btn.Tag != null && btn.Tag.ToString() == "char")
                {
                    if (btn.Text.Length == 1 && char.IsLetter(btn.Text[0]))
                    {
                        btn.Text = _isCapsLock ? btn.Text.ToUpper() : btn.Text.ToLower();
                    }
                }
            }
        }

        private void Enter_Click(object sender, EventArgs e)
        {
            // 빈 문자열 검증
            if (string.IsNullOrEmpty(_inputString))
            {
                ActManager.Instance.Act.PopupNoti(
                    L("Input Error"), 
                    L("Please enter a value"), 
                    NotifyType.Warning);
                return;
            }

            ResultValue = _inputString;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
