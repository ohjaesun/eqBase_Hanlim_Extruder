using System;
using System.Windows.Forms;
using EQ.UI.Controls;

using static EQ.Core.Globals;

using EQ.Core.Service;
using EQ.Domain.Enums;

namespace EQ.UI.Forms
{
    public partial class FormKeypad : FormBase
    {
        private string _inputString = "0";
        private bool _isNewInput = true; // First key press clears initial value if user types number
        private System.Drawing.Point formMove;

        public double ResultValue { get; private set; }
        public double? MinValue { get; private set; }
        public double? MaxValue { get; private set; }

        public FormKeypad(string title = "Input", double initialValue = 0, double? min = null, double? max = null)
        {
            InitializeComponent();
            
            if (!string.IsNullOrEmpty(title))
            {
                _LabelTitle.Text = title;
            }

            _inputString = initialValue.ToString();
            ResultValue = initialValue;
            MinValue = min;
            MaxValue = max;
            UpdateDisplay();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            // Subscribe Events
            _btn0.Click += Number_Click;
            _btn1.Click += Number_Click;
            _btn2.Click += Number_Click;
            _btn3.Click += Number_Click;
            _btn4.Click += Number_Click;
            _btn5.Click += Number_Click;
            _btn6.Click += Number_Click;
            _btn7.Click += Number_Click;
            _btn8.Click += Number_Click;
            _btn9.Click += Number_Click;

            _btnDot.Click += Dot_Click;
            _btnSign.Click += Sign_Click;
            _btnBack.Click += Back_Click;
            _btnClear.Click += Clear_Click;
            
            _btnOk.Click += Ok_Click;
            _btnCancel.Click += Cancel_Click;

            //타이틀 영역을 이용해 메인폼 마우스로 이동 
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

        private void Number_Click(object sender, EventArgs e)
        {
            if (sender is _Button btn)
            {
                if (_isNewInput)
                {
                    _inputString = btn.Text;
                    _isNewInput = false;
                }
                else
                {
                    if (_inputString == "0") _inputString = btn.Text;
                    else _inputString += btn.Text;
                }
                UpdateDisplay();
            }
        }

        private void Dot_Click(object sender, EventArgs e)
        {
            if (_isNewInput)
            {
                _inputString = "0.";
                _isNewInput = false;
            }
            else
            {
                if (!_inputString.Contains("."))
                {
                    _inputString += ".";
                }
            }
            UpdateDisplay();
        }

        private void Sign_Click(object sender, EventArgs e)
        {
            if (double.TryParse(_inputString, out double val))
            {
                val = -val;
                _inputString = val.ToString();
                UpdateDisplay();
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (_inputString.Length > 0)
            {
                _inputString = _inputString.Substring(0, _inputString.Length - 1);
                if (_inputString.Length == 0 || _inputString == "-")
                {
                    _inputString = "0";
                    _isNewInput = true;
                }
            }
            UpdateDisplay();
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            _inputString = "0";
            _isNewInput = true;
            UpdateDisplay();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            if (double.TryParse(_inputString, out double val))
            {
                if (MinValue.HasValue && val < MinValue.Value)
                {
                    ActManager.Instance.Act.PopupNoti(L("Range Error"), L("Value must be greater than {0}", MinValue.Value), NotifyType.Warning);
                    return;
                }

                if (MaxValue.HasValue && val > MaxValue.Value)
                {
                    ActManager.Instance.Act.PopupNoti(L("Range Error"), L("Value must be less than {0}", MaxValue.Value), NotifyType.Warning);
                    return;
                }

                ResultValue = val;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                ActManager.Instance.Act.PopupNoti(L("Input Error"), L("Invalid Number Format"), NotifyType.Warning);
                _inputString = "0";
                _isNewInput = true;
                UpdateDisplay();
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
