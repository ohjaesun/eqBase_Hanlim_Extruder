using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.UI.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EQ.UI.Forms
{
    public partial class FormUserOptionUI : FormBase
    {
        public FormUserOptionUI()
        {
            InitializeComponent();
        }

        private void FormUserOptionUI_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            SetupTabButtons();
            LoadControlsFromOption();           
           
        }

        // 탭 전환 버튼 동적 생성
        private void SetupTabButtons()
        {
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            flowLayoutPanel1.Controls.Clear();

            for (int i = 0; i < tabControl1.TabCount; i++)
            {
                var tabPage = tabControl1.TabPages[i];
                var button = new _Button
                {
                    Text = tabPage.Text,
                    Size = new Size(flowLayoutPanel1.Width - 6, 60),
                    ThemeStyle = ThemeStyle.Info_Sky,
                    Tag = i // 탭 인덱스 저장
                };
                button.Click += TabButton_Click;
                flowLayoutPanel1.Controls.Add(button);
            }

            // 초기 타이틀 설정
            _LabelTitle.Text = tabControl1.SelectedTab.Text;

            for(int i=0; i < ActManager.Instance.Act.Option.ExtruderRecipes.Count(); i++)
            {
                var p = ActManager.Instance.Act.Option.ExtruderRecipes[i];
                _ComboBoxSelectRCP.Items.Add($"[{i+1:D2}] {p.RecipeName}");
            }
                
        }

        private void TabButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.Tag is int index)
            {
                tabControl1.SelectedIndex = index;
                _LabelTitle.Text = btn.Text;
            }
        }

        private async void _ButtonSave_Click(object sender, EventArgs e)
        {
            var act = ActManager.Instance.Act;          

            SaveControlsToOption();
        }

        // --- [핵심 로직] 재귀적으로 컨트롤 탐색 ---
        private IEnumerable<Control> GetAllControls(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                foreach (Control child in GetAllControls(control))
                    yield return child;

                yield return control;
            }
        }

        // --- [데이터 로드] List<UserOptionUI> -> Controls ---
        private void LoadControlsFromOption()
        {
            // 1. 현재 저장된 옵션 리스트 가져오기
            var optionList = ActManager.Instance.Act.Option.OptionUI;
            if (optionList == null) return;

            // 2. 폼 내 모든 컨트롤 순회
            foreach (Control control in GetAllControls(this))
            {
                // 3. 컨트롤 이름으로 저장된 값 찾기
                var item = optionList.FirstOrDefault(x => x.name == control.Name);
                if (item == null) continue;

                try
                {
                    // 4. 컨트롤 타입에 맞춰 값 적용
                    switch (control)
                    {
                        case ListBox listBox:
                            listBox.SelectedIndex = Convert.ToInt32(item.value);
                            break;
                        case CheckBox checkBox:
                            checkBox.Checked = Convert.ToBoolean(item.value);
                            break;
                        case RadioButton radioButton:
                            radioButton.Checked = Convert.ToBoolean(item.value);
                            break;
                        case ComboBox comboBox:
                            comboBox.SelectedIndex = Convert.ToInt32(item.value);
                            break;
                        case TextBox textBox:
                            textBox.Text = item.value;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // 타입 변환 실패 시 로그 (무시하거나 기본값)
                    EQ.Common.Logs.Log.Instance.Error($"UI Option Load Error [{control.Name}]: {ex.Message}");
                }
            }
        }

        // --- [데이터 저장] Controls -> List<UserOptionUI> ---
        private async void SaveControlsToOption()
        {
            var act = ActManager.Instance.Act;
            var optionList = act.Option.OptionUI; // 참조 가져오기

            // 리스트가 없으면 생성 (초기 실행 시)
            if (optionList == null)
            {
                optionList = new List<UserOptionUI>();
                // ActUserOption 내부 캐시에 할당해주기 위해 Set 호출 필요할 수 있음 (구조에 따라 다름)
                // 하지만 ActUserOption.OptionUI는 Get<List<UserOptionUI>> 이므로 
                // 여기서 수정한 optionList 내용이 캐시에 반영되려면 원본 객체를 수정해야 함.
            }

            foreach (Control control in GetAllControls(this))
            {
                string valueToSave = null;
                Type typeToSave = control.GetType();

                // 저장할 대상 컨트롤인지 확인 및 값 추출
                switch (control)
                {
                    case ListBox listBox:
                        valueToSave = listBox.SelectedIndex.ToString();
                        break;
                    case CheckBox checkBox:
                        valueToSave = checkBox.Checked.ToString();
                        break;
                    case RadioButton radioButton:
                        valueToSave = radioButton.Checked.ToString();
                        break;
                    case ComboBox comboBox:
                        valueToSave = comboBox.SelectedIndex.ToString();
                        break;
                    case TextBox textBox:
                        valueToSave = textBox.Text;
                        break;
                }

                if (valueToSave != null)
                {
                    // 기존 리스트에 있는지 확인
                    var existingItem = optionList.FirstOrDefault(x => x.name == control.Name);

                    if (existingItem != null)
                    {
                        // 값 업데이트
                        existingItem.value = valueToSave;
                        existingItem.uiType = typeToSave;
                    }
                    else
                    {
                        // 신규 추가
                        optionList.Add(new UserOptionUI
                        {
                            name = control.Name,
                            value = valueToSave,
                            uiType = typeToSave
                        });
                    }
                }
            }

            // 변경된 리스트 저장
            await act.Option.Save<List<UserOptionUI>>();
            
        }
    }
}