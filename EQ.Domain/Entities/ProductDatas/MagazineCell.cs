using EQ.Domain.Enums;
using System;

namespace EQ.Domain.Entities
{
    public class Magazine<T> where T : struct, IProductUnit
    {
        // [변경] 식별자를 Enum으로 변경
        public MagazineName Name { get; private set; }
        public int Capacity { get; private set; }
        public int Rows { get; private set; }
        public int Cols { get; private set; }

        public ProductMap<T>[] Slots { get; private set; }

        public Magazine()
        {
            Name = (MagazineName.None);
            Slots = Array.Empty<ProductMap<T>>();
        }

        // 생성자 변경
        public Magazine(MagazineName name, int capacity, int rows, int cols)
        {
            Name = name;
            Capacity = capacity;
            Rows = rows;
            Cols = cols;
            Slots = new ProductMap<T>[capacity];

            for (int i = 0; i < capacity; i++)
            {
                Slots[i] = new ProductMap<T>(rows, cols);
            }
        }

        public ProductMap<T> GetSlot(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= Capacity) return null;
            return Slots[slotIndex];
        }
    }
}