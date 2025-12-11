// EQ.Core/Action/Composition/ActRecipe.cs
using EQ.Common.Helper;
using EQ.Common.Logs;
using EQ.Core.Act;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EQ.Core.Act
{
    /// <summary>
    /// 현재 레시피의 이름과 경로를 관리하는 Action 클래스
    /// </summary>
    public class ActRecipe : ActComponent
    {
        private readonly string _baseRecipeFolder = Path.Combine(Environment.CurrentDirectory, "RECIPE");

        // (예: "Recipe_A")
        public string CurrentRecipeName { get; private set; } = "DefaultRecipe";

        public ActRecipe(ACT act) : base(act) { }

        /// <summary>
        /// (UI 시작 시 호출) 레시피의 최상위 폴더 경로를 설정합니다.
        /// </summary>
        public void Initialize()
        {
            Directory.CreateDirectory(_baseRecipeFolder);

            CIni ini = new CIni();
            var rcp = ini.ReadString("SYSTEM", "RCP_NAME", "DefaultRecipe");
            CurrentRecipeName = rcp;
            // (중요) 현재 레시피 폴더가 없으면 생성
            GetCurrentRecipePath();
        }

        /// <summary>
        /// (UI에서 호출) 현재 레시피를 변경합니다.
        /// </summary>
        public void SetCurrentRecipe(string recipeName)
        {
            if (string.IsNullOrEmpty(recipeName)) return;

            CIni ini = new CIni();
            ini.WriteString("SYSTEM", "RCP_NAME", recipeName);

            CurrentRecipeName = recipeName;

            // (중요) 새 레시피로 설정 시 폴더가 없으면 생성
            GetCurrentRecipePath();

            Log.Instance.Info($"레시피 변경: {recipeName}");
        }

        /// <summary>
        /// 현재 레시피의 전체 경로를 반환합니다.
        /// (경로가 없으면 생성합니다)
        /// </summary>
        public string GetCurrentRecipePath()
        {
            if (string.IsNullOrEmpty(_baseRecipeFolder)) return "";

            Directory.CreateDirectory(_baseRecipeFolder);
            string recipePath = Path.Combine(_baseRecipeFolder, CurrentRecipeName);
            Directory.CreateDirectory(recipePath);

            return recipePath;
        }

        // --- (신규) 레시피 관리 기능 ---

        /// <summary>
        /// 모든 레시피 폴더 이름 목록을 반환합니다.
        /// </summary>
        public List<string> GetAllRecipeNames()
        {
            Directory.CreateDirectory(_baseRecipeFolder);
            return Directory.GetDirectories(_baseRecipeFolder)
                            .Select(Path.GetFileName)
                            .ToList();
        }

        /// <summary>
        /// 레시피를 삭제합니다. (현재 레시피는 삭제 불가)
        /// </summary>
        public bool DeleteRecipe(string recipeName)
        {
            if (string.IsNullOrEmpty(recipeName) || recipeName == CurrentRecipeName)
            {
                Log.Instance.Warning($"현재 사용 중인 레시피({recipeName})는 삭제할 수 없습니다.");
                return false;
            }
            if (recipeName == "DefaultRecipe")
            {
                Log.Instance.Warning($"기본 레시피({recipeName})는 삭제할 수 없습니다.");
                _act.PopupNoti("삭제 불가", "기본(Default) 레시피는 삭제할 수 없습니다.", Domain.Enums.NotifyType.Warning);
                return false;
            }

            try
            {
                string path = Path.Combine(_baseRecipeFolder, recipeName);
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    Log.Instance.Info($"레시피 삭제: {recipeName}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"레시피 삭제 실패 ({recipeName}): {ex.Message}");
                _act.PopupNoti("삭제 실패", ex.Message, Domain.Enums.NotifyType.Error);
            }
            return false;
        }

        /// <summary>
        /// 기존 레시피를 새 이름으로 복사합니다.
        /// </summary>
        public bool CopyRecipe(string sourceRecipeName, string newRecipeName)
        {
            if (string.IsNullOrEmpty(sourceRecipeName) || string.IsNullOrEmpty(newRecipeName))
                return false;

            string sourcePath = Path.Combine(_baseRecipeFolder, sourceRecipeName);
            string destPath = Path.Combine(_baseRecipeFolder, newRecipeName);

            if (!Directory.Exists(sourcePath))
            {
                _act.PopupNoti("복사 실패", "원본 레시피 폴더를 찾을 수 없습니다.", Domain.Enums.NotifyType.Error);
                return false;
            }
            if (Directory.Exists(destPath))
            {
                _act.PopupNoti("복사 실패", "이미 동일한 이름의 레시피가 존재합니다.", Domain.Enums.NotifyType.Error);
                return false;
            }

            try
            {
                CopyDirectory(sourcePath, destPath);
                Log.Instance.Info($"레시피 복사: {sourceRecipeName} -> {newRecipeName}");
                return true;
            }
            catch (Exception ex)
            {
                Log.Instance.Error($"레시피 복사 실패: {ex.Message}");
                _act.PopupNoti("복사 실패", ex.Message, Domain.Enums.NotifyType.Error);
                return false;
            }
        }

        // 재귀적으로 폴더를 복사하는 헬퍼 메서드
        private void CopyDirectory(string sourceDir, string destDir)
        {
            Directory.CreateDirectory(destDir);

            foreach (string file in Directory.GetFiles(sourceDir))
            {
                if (Path.GetExtension(file).Equals(".db", StringComparison.OrdinalIgnoreCase))
                {
                    continue; // .db 파일이면 복사하지 않고 넘어감
                }

                string destFile = Path.Combine(destDir, Path.GetFileName(file));
                File.Copy(file, destFile);
            }

            foreach (string subDir in Directory.GetDirectories(sourceDir))
            {
                string destSubDir = Path.Combine(destDir, Path.GetFileName(subDir));
                CopyDirectory(subDir, destSubDir);
            }
        }
    }
}