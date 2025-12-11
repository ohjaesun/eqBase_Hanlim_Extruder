using System.Runtime.InteropServices;

namespace EQ.Domain.Entities
{
    // [신규] 기존 WaferChips 대체
    // IProductUnit을 구현하여 ProductMap<WaferCell>에서 사용 가능하도록 함
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct WaferCell : IProductUnit
    {
        // --- [IProductUnit 인터페이스 명시적 구현] ---
        // 인터페이스 요구사항 (X, Y, Grade, UnitID)
        public int X;
        public int Y;
        public ProductUnitChipGrade Grade;
        public Buffer64<char> UnitID; // 64자 ID 버퍼

        // 외부에서 접근하기 위한 프로퍼티 연결
        int IProductUnit.X { get => X; set => X = value; }
        int IProductUnit.Y { get => Y; set => Y = value; }
        ProductUnitChipGrade IProductUnit.Grade { get => Grade; set => Grade = value; }
        Buffer64<char> IProductUnit.UnitID { get => UnitID; set => UnitID = value; }

        // --- [웨이퍼 전용 추가 데이터] ---
        // 기존 WaferChips의 데이터를 인라인 버퍼로 대체

        // 예: 전압, 전류 등 테스트 결과 16개
        public Buffer16<float> TestResults;

        // 예: Bin Code (분류 코드)
        public short BinCode;

        // --- [Helper] ---
        public string ChipID
        {
            get => UnitID.GetText();
            set => UnitID.SetText(value);
        }
    }
}