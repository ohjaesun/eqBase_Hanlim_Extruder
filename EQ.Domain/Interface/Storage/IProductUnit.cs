using System;

namespace EQ.Domain.Entities
{
    public enum ProductUnitChipGrade : byte
    {
        None = 0,
        Good = 1,
        Fail = 2,
        Skip = 3,
        Empty = 4,
    }

    public interface IProductUnit
    {
        int X { get; set; }
        int Y { get; set; }
        ProductUnitChipGrade Grade { get; set; }

      
        Buffer64<char> UnitID { get; set; }
    }
}