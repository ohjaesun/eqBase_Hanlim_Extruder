namespace EQ.Domain.Entities.SecsGem
{
    /// <summary>
    /// Equipment Constant (ECID) 정의
    /// SEMI E5 표준의 장비 상수를 정의합니다.
    /// 호스트에서 S2F13/S2F14로 조회, S2F15/S2F16으로 변경 가능한 설정값입니다.
    /// </summary>
    public class EquipmentConstant
    {
        /// <summary>
        /// Equipment Constant ID
        /// </summary>
        public int ECID { get; set; }

        /// <summary>
        /// 상수 이름
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 데이터 포맷 (A, U1, U2, U4, I1, I2, I4, F4, F8, BOOL 등)
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
        /// 최소값 (숫자형인 경우)
        /// </summary>
        public string MinValue { get; set; } = string.Empty;

        /// <summary>
        /// 최대값 (숫자형인 경우)
        /// </summary>
        public string MaxValue { get; set; } = string.Empty;

        /// <summary>
        /// 기본값
        /// </summary>
        public string DefaultValue { get; set; } = string.Empty;

        /// <summary>
        /// 설명
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 기본 생성자
        /// </summary>
        public EquipmentConstant()
        {
        }

        /// <summary>
        /// 파라미터 생성자
        /// </summary>
        /// <param name="ecid">Equipment Constant ID</param>
        /// <param name="name">상수 이름</param>
        /// <param name="unit">단위</param>
        /// <param name="format">데이터 포맷</param>
        public EquipmentConstant(int ecid, string name, string unit, string format)
        {
            ECID = ecid;
            Name = name;
            Unit = unit;
            Format = format;
        }

        /// <summary>
        /// 범위 설정
        /// </summary>
        /// <param name="minValue">최소값</param>
        /// <param name="maxValue">최대값</param>
        public void SetRange(string minValue, string maxValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        /// <summary>
        /// 값이 범위 내에 있는지 확인
        /// </summary>
        /// <param name="value">확인할 값</param>
        /// <returns>범위 내이면 true</returns>
        public bool IsInRange(string value)
        {
            if (string.IsNullOrEmpty(MinValue) && string.IsNullOrEmpty(MaxValue))
            {
                return true; // 범위가 설정되지 않은 경우 항상 true
            }

            if (double.TryParse(value, out double numValue) &&
                double.TryParse(MinValue, out double min) &&
                double.TryParse(MaxValue, out double max))
            {
                return numValue >= min && numValue <= max;
            }

            return true; // 숫자가 아닌 경우 범위 검사 생략
        }

        /// <summary>
        /// 문자열 표현
        /// </summary>
        public override string ToString()
        {
            string range = string.Empty;
            if (!string.IsNullOrEmpty(MinValue) && !string.IsNullOrEmpty(MaxValue))
            {
                range = string.Format(" [{0}~{1}]", MinValue, MaxValue);
            }
            return string.Format("ECID[{0}] {1} = {2}{3} ({4})", ECID, Name, Value, range, Format);
        }
    }
}
