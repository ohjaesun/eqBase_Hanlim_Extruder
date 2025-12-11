namespace EQ.Domain.Entities.SecsGem
{
    /// <summary>
    /// Status Variable (SVID) 정의
    /// SEMI E5 표준의 상태 변수를 정의합니다.
    /// 호스트에서 S1F3/S1F4로 조회 가능한 장비 상태값입니다.
    /// </summary>
    public class StatusVariable
    {
        /// <summary>
        /// Status Variable ID
        /// </summary>
        public int SVID { get; set; }

        /// <summary>
        /// 변수 이름
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 데이터 포맷 (A, U1, U2, U4, I1, I2, I4, F4, F8, BOOL, B, L, OBJECT 등)
        /// Array의 경우 "U2_ARRAY" 형식 사용
        /// </summary>
        public string Format { get; set; } = "A";

        /// <summary>
        /// 단위 (mm, sec, % 등)
        /// </summary>
        public string Unit { get; set; } = string.Empty;

        /// <summary>
        /// 현재 값 (문자열로 저장)
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// 설명
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 기본 생성자
        /// </summary>
        public StatusVariable()
        {
        }

        /// <summary>
        /// 파라미터 생성자
        /// </summary>
        /// <param name="svid">Status Variable ID</param>
        /// <param name="name">변수 이름</param>
        /// <param name="format">데이터 포맷</param>
        /// <param name="unit">단위</param>
        public StatusVariable(int svid, string name, string format, string unit = "")
        {
            SVID = svid;
            Name = name;
            Format = format;
            Unit = unit;
        }

        /// <summary>
        /// 문자열 표현
        /// </summary>
        public override string ToString()
        {
            return string.Format("SVID[{0}] {1} = {2} ({3})", SVID, Name, Value, Format);
        }
    }
}
