using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Enums;
using System;
using System.Windows.Forms;

using static EQ.Core.Globals;

namespace EQ.UI.UserViews.Extruder
{
    /// <summary>
    /// Extruder 설정 화면 - HMI 스타일 UI
    /// Recipe/Batch, Parameter, Safety, Part bins 섹션으로 구성
    /// </summary>
    public partial class ExtruderSetup_View : UserControlBaseplain
    {
        // Target 값 저장 (Parameter 섹션)
        private double[] _targetValues = new double[5];

        public ExtruderSetup_View()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            InitializeRecipeComboBox();
            InitializeParameterEvents();
            InitializeButtonEvents();

            timer1.Interval = 1000;
            timer1.Start();
        }

        #region Recipe/Batch ID Section

        /// <summary>
        /// Recipe 콤보박스 초기화
        /// </summary>
        private void InitializeRecipeComboBox()
        {
            // TODO: ActManager에서 레시피 목록 로드
            _comboRecipe.Items.Clear();

            var act = ActManager.Instance.Act;
            var _recipes = act.ExtruderRecipe.Recipes.ToList();

            _comboRecipe.Items.Clear();
            foreach (var recipe in _recipes)
            {
                _comboRecipe.Items.Add(recipe.Name);
            }

            // ActExtruderRecipe의 현재 레시피 인덱스를 사용
            int currentIndex = act.ExtruderRecipe.CurrentRecipeIndex;
            if (currentIndex >= 0 && currentIndex < _comboRecipe.Items.Count)
            {
                _comboRecipe.SelectedIndex = currentIndex;
            }
            else if (_comboRecipe.Items.Count > 0)
            {
                _comboRecipe.SelectedIndex = 0;
            }

            _comboRecipe.SelectedIndexChanged += OnRecipeChanged;
        }

        private void OnRecipeChanged(object sender, EventArgs e)
        {
            // TODO: 선택된 레시피에 따른 파라미터 값 로드
        }

        #endregion

        #region Parameter Section Events

        /// <summary>
        /// Parameter 섹션 이벤트 초기화
        /// </summary>
        private void InitializeParameterEvents()
        {
            var _act = ActManager.Instance.Act;

            // Target 초기값 설정
            _targetValues[0] = _act.Temp.GetCachedSV(TempID.Zone1);
            _targetValues[1] = _act.Temp.GetCachedSV(TempID.Zone2);
            _targetValues[2] = 0.0;
            _targetValues[3] = 8.9;
            _targetValues[4] = 80.0;

            UpdateTargetLabels();

            // Up/Down 버튼 이벤트 연결
            EQ.UI.Controls._Button[] upDownButtons = {
                _btnUp1S, _btnUp1L, _btnDown1S, _btnDown1L,
                _btnUp2S, _btnUp2L, _btnDown2S, _btnDown2L,
                _btnUp3S, _btnUp3L, _btnDown3S, _btnDown3L,
                _btnUp4S, _btnUp4L, _btnDown4S, _btnDown4L,
                _btnUp5S, _btnUp5L, _btnDown5S, _btnDown5L
            };

            foreach (var btn in upDownButtons)
            {
                btn.Click += OnTargetUpDownClick;
            }

            // Set 버튼 이벤트 연결
            EQ.UI.Controls._Button[] setButtons = { _btnSet1, _btnSet2, _btnSet3, _btnSet4, _btnSet5 };
            foreach (var btn in setButtons)
            {
                btn.Click += OnSetButtonClick;
            }

            // Input TextBox 클릭 이벤트 연결 (FormKeypad 호출)
            EQ.UI.Controls._TextBox[] inputBoxes = { _txtInput1, _txtInput2, _txtInput3, _txtInput4, _txtInput5 };
            foreach (var txt in inputBoxes)
            {
                txt.Click += OnInputTextBoxClick;
            }
        }

        /// <summary>
        /// Target Up/Down 버튼 클릭 처리
        /// </summary>
        private void OnTargetUpDownClick(object sender, EventArgs e)
        {
            if (sender is EQ.UI.Controls._Button btn && btn.Tag is object[] tagData)
            {
                int index = (int)tagData[0];
                double delta = (double)tagData[1];

                _targetValues[index] += delta;

                // 음수 방지
                if (_targetValues[index] < 0)
                    _targetValues[index] = 0;

                UpdateTargetLabels();
            }
        }

        /// <summary>
        /// Target 라벨 업데이트
        /// </summary>
        private void UpdateTargetLabels()
        {
            EQ.UI.Controls._TextBox[] targetLabels = { _txtInput1, _txtInput2, _txtInput3, _txtInput4, _txtInput5 };
            for (int i = 0; i < 5; i++)
            {
                targetLabels[i].Text = _targetValues[i].ToString("F1");
            }
        }

        /// <summary>
        /// Set 버튼 클릭 처리
        /// </summary>
        private void OnSetButtonClick(object sender, EventArgs e)
        {
            if (sender is EQ.UI.Controls._Button btn && btn.Tag is int index)
            {
                EQ.UI.Controls._TextBox[] inputBoxes = { _txtInput1, _txtInput2, _txtInput3, _txtInput4, _txtInput5 };

                if (double.TryParse(inputBoxes[index].Text, out double value))
                {
                    _targetValues[index] = value;
                    UpdateTargetLabels();

                    if (index == 0)
                        ActManager.Instance.Act.Temp.Get(TempID.Zone1).WriteSV(value);
                    if (index == 1)
                        ActManager.Instance.Act.Temp.Get(TempID.Zone2).WriteSV(value);

                    // TODO: 실제 장비로 값 전송
                    // ActManager.Instance.Act.Extruder.SetParameter(index, value);
                }
            }
        }

        /// <summary>
        /// Input TextBox 클릭 시 키패드 표시
        /// </summary>
        private void OnInputTextBoxClick(object sender, EventArgs e)
        {
            if (sender is EQ.UI.Controls._TextBox txt)
            {
                double initialValue = 0.0;
                double.TryParse(txt.Text, out initialValue);

                string title = L("Enter Value");

                using (var keypad = new EQ.UI.Forms.FormKeypad(title, initialValue, null, null))
                {
                    if (keypad.ShowDialog() == DialogResult.OK)
                    {
                        txt.Text = keypad.ResultValue.ToString("F1");

                        // _targetValues 배열에 값 저장
                        EQ.UI.Controls._TextBox[] inputBoxes = { _txtInput1, _txtInput2, _txtInput3, _txtInput4, _txtInput5 };
                        int index = Array.IndexOf(inputBoxes, txt);
                        if (index >= 0 && index < _targetValues.Length)
                        {
                            _targetValues[index] = keypad.ResultValue;
                        }
                    }
                }
            }
        }

        #endregion

        #region Button Events

        /// <summary>
        /// 버튼 이벤트 초기화
        /// </summary>
        private void InitializeButtonEvents()
        {
            _btnRecipeFolder.Click += OnRecipeFolderClick;
            _btnRecipeRefresh.Click += OnRecipeRefreshClick;
            _btnPlay.Click += OnPlayClick;
            _btnStop.Click += OnStopClick;
        }

        private void OnRecipeFolderClick(object sender, EventArgs e)
        {
            // TODO: 레시피 폴더 열기
        }

        private void OnRecipeRefreshClick(object sender, EventArgs e)
        {
            InitializeRecipeComboBox();
        }

        private void OnPlayClick(object sender, EventArgs e)
        {
            // TODO: Batch 시작
        }

        private void OnStopClick(object sender, EventArgs e)
        {
            // TODO: Batch 중지
        }

        #endregion

        #region Public Methods for External Updates

        /// <summary>
        /// Actual 값 업데이트 (외부에서 호출)
        /// </summary>
        public void UpdateActualValues(double[] values)
        {
            if (values == null || values.Length < 5) return;

            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateActualValues(values)));
                return;
            }

            EQ.UI.Controls._Label[] actualLabels = { _lblActual1, _lblActual2, _lblActual3, _lblActual4, _lblActual5 };
            for (int i = 0; i < 5; i++)
            {
                actualLabels[i].Text = values[i].ToString("F1");
            }
        }

        /// <summary>
        /// Safety 상태 업데이트 (외부에서 호출)
        /// </summary>
        /// <param name="index">0-9 인덱스</param>
        /// <param name="isOk">정상 여부</param>
        public void UpdateSafetyStatus(int index, bool isOk)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateSafetyStatus(index, isOk)));
                return;
            }

            EQ.UI.Controls._Label[] statusLabels = {
                _lblSafetyStatus1, _lblSafetyStatus2, _lblSafetyStatus3,
                _lblSafetyStatus4, _lblSafetyStatus5, _lblSafetyStatus6,
                _lblSafetyStatus7, _lblSafetyStatus8, _lblSafetyStatus9, _lblSafetyStatus10
            };

            if (index >= 0 && index < statusLabels.Length)
            {
                statusLabels[index].Text = isOk ? "Ok" : "Error";
                statusLabels[index].ThemeStyle = isOk
                    ? EQ.UI.Controls.ThemeStyle.Success_Green
                    : EQ.UI.Controls.ThemeStyle.Danger_Red;
            }
        }

        /// <summary>
        /// Part bin 상태 업데이트 (외부에서 호출)
        /// </summary>
        /// <param name="index">0-3 인덱스</param>
        /// <param name="isPresent">Present 여부</param>
        public void UpdatePartBinStatus(int index, bool isPresent)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdatePartBinStatus(index, isPresent)));
                return;
            }

            EQ.UI.Controls._Label[] statusLabels = {
                _lblPartBinStatus1, _lblPartBinStatus2,
                _lblPartBinStatus3, _lblPartBinStatus4
            };

            if (index >= 0 && index < statusLabels.Length)
            {
                statusLabels[index].Text = isPresent ? "Present" : "Empty";
                statusLabels[index].ThemeStyle = isPresent
                    ? EQ.UI.Controls.ThemeStyle.Success_Green
                    : EQ.UI.Controls.ThemeStyle.Warning_Yellow;
            }
        }

        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            var _act = ActManager.Instance.Act;

            double[] actualValues = new double[5];
            actualValues[0] = _act.Temp.GetCachedPV(TempID.Zone1);
            actualValues[1] = _act.Temp.GetCachedPV(TempID.Zone2);
            UpdateActualValues(actualValues);

            _lblTarget1.Text = _act.Temp.GetCachedSV(TempID.Zone1).ToString("F1");
            _lblTarget2.Text = _act.Temp.GetCachedSV(TempID.Zone2).ToString("F1");
        }

        private void ExtruderSetup_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;          
        }
    }
}
