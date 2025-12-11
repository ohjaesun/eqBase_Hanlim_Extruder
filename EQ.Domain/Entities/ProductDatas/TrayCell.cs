using System.Runtime.InteropServices;

namespace EQ.Domain.Entities
{
    // 메모리 레이아웃 고정 (고속 바이너리 저장용)
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct TrayCell : IProductUnit
    {
        // [변경 1] 데이터 멤버를 'Property'에서 'Field'로 변경
        // 필드는 ref로 전달 가능하며, 메모리 오프셋이 확실하게 보장됩니다.
        public int X;
        public int Y;
        public ProductUnitChipGrade Grade;
        public Buffer64<char> UnitID; // Field로 선언

        // [추가 데이터]
        public Buffer16<float> Temperatures;
        public Buffer32<int> InspectCodes;

        // [변경 2] IProductUnit 인터페이스 명시적 구현 (Interface 호환성 유지)
        // 외부에서 IProductUnit으로 캐스팅해서 쓸 때 사용됩니다.
        int IProductUnit.X { get => X; set => X = value; }
        int IProductUnit.Y { get => Y; set => Y = value; }
        ProductUnitChipGrade IProductUnit.Grade { get => Grade; set => Grade = value; }
        Buffer64<char> IProductUnit.UnitID { get => UnitID; set => UnitID = value; }
       
        public string ID
        {
            get => UnitID.GetText();
            set => UnitID.SetText(value);
        }
    }
}