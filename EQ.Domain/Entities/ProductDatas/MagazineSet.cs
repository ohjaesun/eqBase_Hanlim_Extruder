using EQ.Domain.Enums;
using System.Collections.Generic;
using System.Linq;

namespace EQ.Domain.Entities
{
    public class MagazineSet<T> where T : struct, IProductUnit
    {
        // [변경] int -> MagazineName
        private Dictionary<MagazineName, Magazine<T>> _magazines = new Dictionary<MagazineName, Magazine<T>>();

        public void Add(Magazine<T> magazine)
        {
            if (magazine == null) return;

            if (!_magazines.ContainsKey(magazine.Name))
            {
                _magazines.Add(magazine.Name, magazine);
            }
            else
            {
                _magazines[magazine.Name] = magazine;
            }
        }

        // [변경] int id -> MagazineName name
        public Magazine<T> Get(MagazineName name)
        {
            return _magazines.TryGetValue(name, out var mag) ? mag : null;
        }

        public List<Magazine<T>> GetAll()
        {
            // Enum 순서대로 정렬
            return _magazines.Values.OrderBy(m => m.Name).ToList();
        }

        public void Clear() => _magazines.Clear();
    }
}