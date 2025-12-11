using EQ.Core.Act;
using EQ.Domain.Entities;
using EQ.Domain.Interface;
using System;
using System.IO;

namespace EQ.Core.Act.Composition
{
    public class ActProduct<T> : ActComponent where T : struct, IProductUnit
    {
        private IDataStorage<ProductMap<T>> _storage;
        private readonly string _dataKey;

        public ProductMap<T> CurrentMap { get; private set; }

        public ActProduct(ACT act, string dataKey = "ProductData") : base(act)
        {
            _dataKey = dataKey;
            CurrentMap = new ProductMap<T>(0, 0);
        }

        public void RegisterStorage(IDataStorage<ProductMap<T>> storage)
        {
            _storage = storage;
        }

        public void CreateNewMap(int rows, int cols)
        {
            CurrentMap = new ProductMap<T>(rows, cols);
        }

        public void SaveMap()
        {
            if (_storage == null) return;
            string path = GetRecipePath();
            _storage.Save(CurrentMap, path, _dataKey);
        }

        public void LoadMap()
        {
            if (_storage == null) return;
            string path = GetRecipePath();
            var loaded = _storage.Load(path, _dataKey);
            if (loaded != null) CurrentMap = loaded;
        }

        // --- [수정] ID 설정 헬퍼 ---
        public void SetUnitId(int x, int y, string id)
        {
            if (IsValid(x, y))
            {
                // 1. 구조체 참조 가져오기
                ref T unit = ref CurrentMap[x, y];

                // [수정] 인터페이스 프로퍼티는 ref 전달 불가하므로 로컬 변수에 복사
                var buffer = unit.UnitID;

                // 2. 로컬 버퍼 수정 (확장 메서드 SetText는 ref로 동작)
                buffer.SetText(id);

                // 3. 수정된 버퍼를 다시 구조체 프로퍼티에 할당 (값 복사)
                unit.UnitID = buffer;
            }
        }

        public void SetUnitStatus(int x, int y, ProductUnitChipGrade grade)
        {
            if (IsValid(x, y))
            {
                ref T unit = ref CurrentMap[x, y];
                unit.Grade = grade; // 프로퍼티 Setter 호출
            }
        }

        private string GetRecipePath()
        {
            return Path.Combine(Environment.CurrentDirectory, "ProductData", _act.Recipe.CurrentRecipeName);
        }

        private bool IsValid(int x, int y)
        {
            return CurrentMap != null && x >= 0 && x < CurrentMap.Cols && y >= 0 && y < CurrentMap.Rows;
        }

        public void LoadMapFromObject(object mapObj)
        {
            if (mapObj is ProductMap<T> map)
            {
                CurrentMap = map;
            }
        }
    }
}