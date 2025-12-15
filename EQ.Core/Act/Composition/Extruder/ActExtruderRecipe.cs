using EQ.Common.Logs;
using EQ.Core.Sequence;
using EQ.Core.Service;
using EQ.Domain.Entities.Extruder;
using EQ.Domain.Enums;
using EQ.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EQ.Core.Act.Composition.Extruder
{
    /// <summary>
    /// ExtruderRecipe 리스트(30개 고정)를 관리하는 Act Composition
    /// DualStorage를 통해 JSON과 SQLite에 동시 저장합니다.
    /// </summary>
    public class ActExtruderRecipe : ActComponent
    {
        private IDataStorage<List<ExtruderRecipe>> _storage;
        private List<ExtruderRecipe> _recipes;
        private const string STORAGE_KEY = "ExtruderRecipes";
        private const int RECIPE_COUNT = 30;

        /// <summary>
        /// 전체 레시피 리스트 (읽기 전용)
        /// </summary>
        public IReadOnlyList<ExtruderRecipe> Recipes => _recipes;

        public ActExtruderRecipe(ACT act) : base(act)
        {
            _recipes = new List<ExtruderRecipe>();
        }

        /// <summary>
        /// DualStorage 등록 (FormSplash에서 호출)
        /// </summary>
        public void RegisterStorageService(IDataStorage<List<ExtruderRecipe>> storage)
        {
            _storage = storage;
        }

        /// <summary>
        /// 현재 레시피 경로에서 ExtruderRecipe 리스트를 로드합니다.
        /// 파일이 없으면 30개의 기본 레시피를 생성합니다.
        /// </summary>
        public void Load()
        {
            if (_storage == null)
            {
                Log.Instance.Warning("ActExtruderRecipe: Storage가 등록되지 않았습니다.");
                return;
            }

            string path = _act.Recipe.GetCurrentRecipePath();
            if (string.IsNullOrEmpty(path)) return;

            try
            {
                _recipes = _storage.Load(path, STORAGE_KEY);
                
                // 파일이 없거나 비어있으면 30개 기본 레시피 생성
                if (_recipes == null || _recipes.Count == 0)
                {
                    _recipes = CreateDefaultRecipes();
                    // 생성 후 바로 저장
                    _storage.Save(_recipes, path, STORAGE_KEY);
                    Log.Instance.Info($"ExtruderRecipe 기본 {RECIPE_COUNT}개 생성 및 저장: {path}");
                }
                // 개수가 부족하면 채우기
                else if (_recipes.Count < RECIPE_COUNT)
                {
                    int startIndex = _recipes.Count;
                    for (int i = startIndex; i < RECIPE_COUNT; i++)
                    {
                        _recipes.Add(new ExtruderRecipe { Name = $"Recipe_{i + 1:D2}" });
                    }
                    _storage.Save(_recipes, path, STORAGE_KEY);
                    Log.Instance.Info($"ExtruderRecipe {RECIPE_COUNT - startIndex}개 추가 생성: {path}");
                }
                
                Log.Instance.Info($"ExtruderRecipe {_recipes.Count}개 로드 완료: {path}");
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"ExtruderRecipe 로드 실패: {ex.Message}");
                _recipes = CreateDefaultRecipes();
            }
        }

        /// <summary>
        /// 30개의 기본 레시피를 생성합니다.
        /// </summary>
        private List<ExtruderRecipe> CreateDefaultRecipes()
        {
            var recipes = new List<ExtruderRecipe>();
            for (int i = 0; i < RECIPE_COUNT; i++)
            {
                recipes.Add(new ExtruderRecipe { Name = $"Recipe_{i + 1:D2}" });
            }
            return recipes;
        }

        /// <summary>
        /// 이름으로 레시피를 조회합니다.
        /// </summary>
        public ExtruderRecipe GetByName(string name)
        {
            return _recipes.FirstOrDefault(r => r.Name == name);
        }

        /// <summary>
        /// 인덱스로 레시피를 조회합니다. (0-based)
        /// </summary>
        public ExtruderRecipe GetByIndex(int index)
        {
            if (index >= 0 && index < _recipes.Count)
            {
                return _recipes[index];
            }
            return null;
        }

        /// <summary>
        /// 전체 레시피 리스트를 저장합니다.
        /// </summary>
        public async Task<bool> Save(bool confirm = true)
        {
            if (_storage == null)
            {
                _act.PopupNoti("저장 실패", "Storage가 등록되지 않았습니다.", NotifyType.Error);
                return false;
            }

            // 1. 시퀀스 실행 중인지 확인
            if (IsAnySequenceRunning())
            {
                var result = await _act.PopupYesNo.ConfirmAsync(
                    "위험: 설정 저장 경고",
                    "현재 시퀀스가 실행 중입니다.\n데이터 불일치로 장비 오동작이 발생할 수 있습니다.\n\n그래도 저장하시겠습니까?",
                    NotifyType.Warning
                );

                if (result != YesNoResult.Yes) return false;
            }
            // 2. 일반 확인 팝업
            else if (confirm)
            {
                var result = await _act.PopupYesNo.ConfirmAsync(
                    "설정 저장",
                    "ExtruderRecipe를 저장하시겠습니까?",
                    NotifyType.Info
                );

                if (result != YesNoResult.Yes) return false;
            }

            // 3. 저장 수행
            string path = _act.Recipe.GetCurrentRecipePath();

            try
            {
                await Task.Run(() =>
                {
                    _storage.Save(_recipes, path, STORAGE_KEY);
                    _act.PopupNoti("저장 완료", "ExtruderRecipe가 정상적으로 저장되었습니다.", NotifyType.Info);
                });

                Log.Instance.Info($"ExtruderRecipe {_recipes.Count}개 저장 완료: {path}");
                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"ExtruderRecipe 저장 실패: {ex.Message}");
                _act.PopupNoti("저장 실패", $"오류가 발생했습니다.\n{ex.Message}", NotifyType.Error);
                return false;
            }
        }

        /// <summary>
        /// 시퀀스 상태 체크 헬퍼
        /// </summary>
        private bool IsAnySequenceRunning()
        {
            var seqManager = SeqManager.Instance.Seq;
            if (seqManager == null) return false;

            foreach (SEQ.SeqName name in Enum.GetValues(typeof(SEQ.SeqName)))
            {
                var seq = seqManager.GetSequence(name);
                if (seq != null)
                {
                    if (seq._Status == SeqStatus.RUN || seq._Status == SeqStatus.SEQ_STOPPING)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
