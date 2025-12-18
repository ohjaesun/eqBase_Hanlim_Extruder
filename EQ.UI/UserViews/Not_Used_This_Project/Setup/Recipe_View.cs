// EQ.UI/UserViews/Recipe_View.cs
using EQ.Core.Act;
using EQ.Core.Act;
using EQ.Core.Service;
using EQ.Domain.Enums;
using System;
using System.Linq;
using System.Windows.Forms;

namespace EQ.UI.UserViews
{
    public partial class Recipe_View : UserControlBase
    {
        private readonly ACT _act;

        public Recipe_View()
        {
            InitializeComponent();
            _act = ActManager.Instance.Act;
        }

        private void Recipe_View_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            // _ButtonSave는 이 화면에서 사용하지 않음
            _ButtonSave.Enabled = false;
            _ButtonSave.Visible = false;
            _LabelTitle.Text = "Recipe Management";

            RefreshRecipeList();
        }

        /// <summary>
        /// 리스트박스와 현재 레시피 레이블을 새로고침
        /// </summary>
        private void RefreshRecipeList()
        {
            var currentRecipe = _act.Recipe.CurrentRecipeName;
            var allRecipes = _act.Recipe.GetAllRecipeNames();

            _ListBoxRecipes.Items.Clear();
            foreach (var recipe in allRecipes)
            {
                _ListBoxRecipes.Items.Add(recipe);
            }

            _LabelCurrent.Text = currentRecipe;

            // 현재 레시피를 리스트에서 선택
            if (allRecipes.Contains(currentRecipe))
            {
                _ListBoxRecipes.SelectedItem = currentRecipe;
            }
        }

        /// <summary>
        /// (공용) 텍스트박스 입력 유효성 검사
        /// </summary>
        private bool IsValidInput(out string recipeName)
        {
            recipeName = _TextBoxNewName.Text.Trim();
            if (string.IsNullOrEmpty(recipeName))
            {
                _act.PopupNoti("입력 오류", "레시피 이름을 입력하세요.", NotifyType.Warning);
                _TextBoxNewName.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// (공용) 리스트박스 선택 유효성 검사
        /// </summary>
        private bool IsValidSelection(out string selectedRecipe)
        {
            selectedRecipe = _ListBoxRecipes.SelectedItem as string;
            if (string.IsNullOrEmpty(selectedRecipe))
            {
                _act.PopupNoti("선택 오류", "목록에서 레시피를 먼저 선택하세요.", NotifyType.Warning);
                return false;
            }
            return true;
        }


        private async void _ButtonSetCurrent_Click(object sender, EventArgs e)
        {
            if (!IsValidSelection(out string selectedRecipe))
                return;

            if (selectedRecipe == _act.Recipe.CurrentRecipeName)
            {
                _act.PopupNoti("정보", "이미 현재 레시피로 선택되어 있습니다.", NotifyType.Info);
                return;
            }

            var result = await _act.PopupYesNo.ConfirmAsync(
                "레시피 변경",
                $"현재 레시피를 '{selectedRecipe}'(으)로 변경하시겠습니까?\n(옵션 등 모든 설정이 다시 로드됩니다)",
                NotifyType.Warning
            );

            if (result == YesNoResult.Yes)
            {
                _act.Recipe.SetCurrentRecipe(selectedRecipe);

                // (중요) 옵션 등을 파일에서 다시 로드
                _act.Option.LoadAllOptionsFromStorage();

                RefreshRecipeList();
                _act.PopupNoti("변경 완료", $"현재 레시피가 '{selectedRecipe}'(으)로 변경되었습니다.", NotifyType.Info);
            }
        }

        private async void _ButtonNew_Click(object sender, EventArgs e)
        {
            if (!IsValidInput(out string newRecipeName))
                return;

            if (_act.Recipe.GetAllRecipeNames().Contains(newRecipeName))
            {
                _act.PopupNoti("생성 실패", "이미 동일한 이름의 레시피가 존재합니다.", NotifyType.Error);
                return;
            }

            var result = await _act.PopupYesNo.ConfirmAsync(
                "레시피 생성",
                $"현재 레시피를 '{newRecipeName}'생성 하시겠습니까?\n(옵션 등 모든 설정이 다시 로드됩니다)",
                NotifyType.Warning
            );

            if (result != YesNoResult.Yes) return;

                // SetCurrentRecipe가 폴더 생성 및 C.ini 저장을 모두 처리함
                _act.Recipe.SetCurrentRecipe(newRecipeName);
            _act.Option.LoadAllOptionsFromStorage(); // 새 레시피(기본값) 로드
            RefreshRecipeList();
            _TextBoxNewName.Text = "";
            _act.PopupNoti("생성 완료", $"'{newRecipeName}' 레시피를 생성하고 현재 레시피로 설정했습니다.", NotifyType.Info);
        }

        private async void _ButtonCopy_Click(object sender, EventArgs e)
        {
            if (!IsValidSelection(out string sourceRecipe))
                return;

            if (!IsValidInput(out string newRecipeName))
                return;

            if (sourceRecipe == newRecipeName)
            {
                _act.PopupNoti("복사 오류", "원본과 대상 레시피 이름이 동일합니다.", NotifyType.Warning);
                return;
            }

            var result = await _act.PopupYesNo.ConfirmAsync(
                "레시피 복사",
                $"'{sourceRecipe}' 레시피를 '{newRecipeName}'(으)로 복사하시겠습니까?",
                NotifyType.Info
            );

            if (result == YesNoResult.Yes)
            {
                if (_act.Recipe.CopyRecipe(sourceRecipe, newRecipeName))
                {
                    RefreshRecipeList();
                    _TextBoxNewName.Text = "";
                    _act.PopupNoti("복사 완료", $"'{newRecipeName}' 레시피가 생성되었습니다.", NotifyType.Info);

                    // 즉시 복사한 레시피로 전환
                    if(false)
                    {
                        _act.Recipe.SetCurrentRecipe(newRecipeName);
                        _act.Option.LoadAllOptionsFromStorage();
                    }                    
                }
                // (실패 팝업은 ActRecipe.CopyRecipe 내부에서 처리)
            }
        }

        private async void _ButtonDelete_Click(object sender, EventArgs e)
        {
            if (!IsValidSelection(out string selectedRecipe))
                return;

            if (selectedRecipe == _act.Recipe.CurrentRecipeName)
            {
                _act.PopupNoti("삭제 불가", "현재 사용 중인 레시피는 삭제할 수 없습니다.", NotifyType.Warning);
                return;
            }

            var result = await _act.PopupYesNo.ConfirmAsync(
                "레시피 삭제",
                $"'{selectedRecipe}' 레시피를 영구적으로 삭제하시겠습니까?\n(이 작업은 되돌릴 수 없습니다)",
                NotifyType.Error // 위험한 작업이므로 Error 타입 사용
            );

            if (result == YesNoResult.Yes)
            {
                if (_act.Recipe.DeleteRecipe(selectedRecipe))
                {
                    RefreshRecipeList();
                    _act.PopupNoti("삭제 완료", $"'{selectedRecipe}' 레시피가 삭제되었습니다.", NotifyType.Info);
                }
                // (실패 팝업은 ActRecipe.DeleteRecipe 내부에서 처리)
            }
        }
    }
}