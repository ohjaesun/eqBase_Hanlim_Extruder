using EQ.Core.Act;
using EQ.Domain.Entities;
using EQ.Domain.Enums;
using EQ.Infra.Storage;
using System;
using System.IO;

using static EQ.Core.Globals;

namespace EQ.Core.Act.Composition
{
    // 이 클래스가 이제 "Magazines" 역할을 수행합니다.
    public class ActMagazine<T> : ActComponent where T : struct, IProductUnit
    {
        private MagazineStorage<T> _storage;
        private MagazineSet<T> _set;

        // [추가] 데이터 저장 폴더를 구분하기 위한 카테고리 이름
        private readonly string _categoryName;

        public Magazine<T> CurrentMagazine { get; private set; }

        // [수정] 생성자에서 categoryName을 받도록 복구
        public ActMagazine(ACT act, string categoryName) : base(act)
        {
            _categoryName = categoryName; // 예: "TrayMag", "WaferMag"
            _set = new MagazineSet<T>();
            CurrentMagazine = new Magazine<T>((MagazineName.None), 0, 0, 0);
        }

        public void RegisterStorage(MagazineStorage<T> storage)
        {
            _storage = storage;
        }

        /// <summary>
        /// [핵심] 프로젝트에 필요한 매거진을 생성하고 등록합니다.
        /// (ACT 생성자가 아닌, FormSplash의 초기화 단계나 Config 로드 시 호출)
        /// </summary>
        public void CreateMagazine(MagazineName name, int capacity, int rows, int cols)
        {
            Magazine<T> mag;

            if (_storage != null)
            {
                string path = GetRecipePath();
                // 저장소 로드 (파일명은 MagazineName Enum 사용)
                mag = _storage.LoadWithInit(path, name.ToString(), name, capacity, rows, cols);
            }
            else
            {
                mag = new Magazine<T>(name, capacity, rows, cols);
            }

            _set.Add(mag);

            if (CurrentMagazine.Name == (MagazineName.None))
            {
                CurrentMagazine = mag;
            }
        }

        public Magazine<T> GetMagazine(MagazineName name)
        {
            var mag = _set.Get(name);
            if (mag == null)
            {
                _act.PopupAlarm(ErrorList.DATA관련, L("Magazine [{0}] Not Found", name));
                return null;
            }
            return mag;
        }

        public void SelectMagazine(MagazineName name)
        {
            var mag = GetMagazine(name);
            if (mag != null)
            {
                CurrentMagazine = mag;
            }
        }

        public void SaveAll()
        {
            if (_storage == null) return;
            string path = GetRecipePath();

            foreach (var mag in _set.GetAll())
            {
                // 각 매거진의 Name을 파일명 키로 사용
                _storage.Save(mag, path, mag.Name.ToString());
            }
        }

        // 저장/로드 시에도 MagazineName 사용
        public void SaveSlot(MagazineName name, int slotIndex)
        {
            var mag = GetMagazine(name);
            if (mag != null && _storage != null)
            {
                string path = GetRecipePath();
                _storage.SaveSlot(mag, slotIndex, path, name.ToString());
            }
        }

        private string GetRecipePath()
        {
            // [수정] 경로에 _categoryName을 포함하여 Tray/Wafer 데이터 섞임 방지
            // 예: ProductData/RecipeA/TrayMag/Magazines/
            return Path.Combine(Environment.CurrentDirectory, "ProductData", _act.Recipe.CurrentRecipeName, _categoryName, "Magazines");
        }
    }
}