namespace EQ.Domain.Entities.Extruder
{

    [AttributeUsage(AttributeTargets.Property)]
    public class ExtruderParameterAttribute : Attribute
    {
        public string Category { get; private set; }
        public string Unit { get; private set; }
        public string Description { get; private set; }
        public string Range { get; private set; }
        public object DefaultValue { get; private set; }

        public ExtruderParameterAttribute(string category, string unit, string description, string range, object defaultValue)
        {
            Category = category;
            Unit = unit;
            Description = description;
            Range = range;
            DefaultValue = defaultValue;
        }
    }


    public class ExtruderRecipe
    {
        // --- Recipe Name ---
        /// <summary>
        /// 레시피 이름 (고유 식별자)
        /// </summary>
        public string Name { get; set; } = "Recipe";

        // --- Cooling ---
        [ExtruderParameter("Cooling", "°C", "칠러(냉각조)의 목표 온도", "0...30", 10.0)]
        public double CirculationBathTemp { get; set; } = 10.0;

        [ExtruderParameter("Cooling", "°C", "냉각조 온도 설정 최소값", "0...30", 5.0)]
        public double CirculationBathTempEditRangeLowerLimit { get; set; } = 5.0;

        [ExtruderParameter("Cooling", "°C", "냉각조 온도 설정 최대값", "0...30", 25.0)]
        public double CirculationBathTempEditRangeUpperLimit { get; set; } = 25.0;

        // --- PID ---
        [ExtruderParameter("PID", "#", "PID 제어 데드밴드 값", "0...1", 0.01)]
        public double DB { get; set; } = 0.01;

        [ExtruderParameter("PID", "#", "PID 미분(D) 게인", "0...0", 0.0)]
        public double Kd { get; set; } = 0.0;

        [ExtruderParameter("PID", "#", "PID 적분(I) 게인", "0...1", 0.42)]
        public double Ki { get; set; } = 0.42;

        [ExtruderParameter("PID", "#", "PID 비례(P) 게인", "0...1", 0.35)]
        public double Kp { get; set; } = 0.35;

        [ExtruderParameter("PID", "s", "PID 측정 샘플링 주기", "0.5...5", 1.0)]
        public double PidSamplingTime { get; set; } = 1.0;

        [ExtruderParameter("PID", "#", "데드타임 보상 지연 버퍼 개수 (0=미사용)", "0...15", 10)]
        public int DelayBufferCount { get; set; } = 10;

        // --- Quality ---
        [ExtruderParameter("Quality", "mm", "필라멘트 목표 직경", "0.1...1.6", 0.8)]
        public double Diameter { get; set; } = 0.8;

        [ExtruderParameter("Quality", "mm", "허용 최대 직경", "0.1...1.6", 1.0)]
        public double MaxDiameter { get; set; } = 1.0;

        [ExtruderParameter("Quality", "mm", "허용 최대 타원도", "0...1.6", 0.5)]
        public double MaxOvality { get; set; } = 0.5;

        [ExtruderParameter("Quality", "mm", "허용 최소 직경", "0.1...1.6", 0.6)]
        public double MinDiameter { get; set; } = 0.6;

        [ExtruderParameter("Quality", "%", "규격 직경 만족 길이 비율", "1...100", 1.0)]
        public double DispositionLength { get; set; } = 1.0;

        // --- Extruder ---
        [ExtruderParameter("Extruder", "rpm", "압출기 모터 목표 속도", "1...360", 10.0)]
        public double ExtruderSpeed { get; set; } = 10.0;

        [ExtruderParameter("Extruder", "°C", "압출기 배럴 1번 목표 온도", "0...280", 105.0)]
        public double ExtruderTemperature1 { get; set; } = 105.0;

        [ExtruderParameter("Extruder", "°C", "압출기 배럴 2번 목표 온도", "0...280", 105.0)]
        public double ExtruderTemperature2 { get; set; } = 105.0;

        [ExtruderParameter("Extruder", "Ncm", "압출 종료 시 모터 정지를 위한 최소 토크", "2...500", 499.0)]
        public double ExtruderTorqueMin { get; set; } = 499.0;

        // --- Feeder ---
        [ExtruderParameter("Feeder", "#", "피더 제어 루프 강도 (1=기본)", "0...5", 1.0)]
        public double FeederControllerAdaption { get; set; } = 1.0;

        [ExtruderParameter("Feeder", "#", "피더 모터 기어비", "0...1000", 400.0)]
        public double FeederGearbox { get; set; } = 400.0;

        [ExtruderParameter("Feeder", "rpm", "피더 모터 최대 속도", "0...10000", 2400.0)]
        public double FeederMotorSpeedMax { get; set; } = 2400.0;

        [ExtruderParameter("Feeder", "#", "피더 모터 1회전당 엔코더 펄스 수", "11...13", 12.0)]
        public double FeederPulsePerRevolution { get; set; } = 12.0;

        [ExtruderParameter("Feeder", "rpm", "생산 사이클용 재료 공급 속도", "0.2...6", 3.0)]
        public double FeederSetPoint { get; set; } = 3.0;

        // --- Process ---
        [ExtruderParameter("Process", "mm", "필라멘트 목표 길이", "10...250", 80.0)]
        public double FilamentLength { get; set; } = 80.0;

        [ExtruderParameter("Process", "s", "목표 온도 도달 후 안정화 대기 시간", "1...6000", 10.0)]
        public double HeatUpTime { get; set; } = 10.0;

        [ExtruderParameter("Process", "", "초기 압출 공정 여부", "no;yes", true)]
        public bool IsFirstExtrusion { get; set; } = true; // yes maps to true

        // --- HeatingPlate ---
        [ExtruderParameter("HeatingPlate", "°C", "히팅 플레이트 우측 목표 온도", "0...150", 30.0)]
        public double HeatingPlate1Temp { get; set; } = 30.0;

        [ExtruderParameter("HeatingPlate", "°C", "히팅 플레이트 우측 설정 최소 온도", "0...150", 20.0)]
        public double HeatingPlate1TempEditRangeLowerLimit { get; set; } = 20.0;

        [ExtruderParameter("HeatingPlate", "°C", "히팅 플레이트 우측 설정 최대 온도", "0...150", 70.0)]
        public double HeatingPlate1TempEditRangeUpperLimit { get; set; } = 70.0;

        [ExtruderParameter("HeatingPlate", "°C", "히팅 플레이트 좌측 목표 온도", "0...150", 30.0)]
        public double HeatingPlate2Temp { get; set; } = 30.0;

        [ExtruderParameter("HeatingPlate", "°C", "히팅 플레이트 좌측 설정 최소 온도", "0...150", 20.0)]
        public double HeatingPlate2TempEditRangeLowerLimit { get; set; } = 20.0;

        [ExtruderParameter("HeatingPlate", "°C", "히팅 플레이트 좌측 설정 최대 온도", "0...150", 70.0)]
        public double HeatingPlate2TempEditRangeUpperLimit { get; set; } = 70.0;

        // --- PID_Limit ---
        [ExtruderParameter("PID_Limit", "#", "PID CV 최대 제한값", "0...100", 97.0)]
        public double MAXCV { get; set; } = 97.0;
        [ExtruderParameter("PID_Limit", "#", "PID I 최대 제한값", "0...100", 97.0)]
        public double MAXI { get; set; } = 97.0;
        [ExtruderParameter("PID_Limit", "#", "PID S 최대 제한값", "0...100", 97.0)]
        public double MAXS { get; set; } = 97.0;
        [ExtruderParameter("PID_Limit", "#", "PID TIE 최대 제한값", "0...500", 99.0)]
        public double MAXTIE { get; set; } = 99.0;
        [ExtruderParameter("PID_Limit", "#", "PID CV 최소 제한값", "0...50", 0.0)]
        public double MINCV { get; set; } = 0.0;
        [ExtruderParameter("PID_Limit", "#", "PID I 최소 제한값", "0...50", 0.0)]
        public double MINI { get; set; } = 0.0;
        [ExtruderParameter("PID_Limit", "#", "PID S 최소 제한값", "0...50", 0.0)]
        public double MINS { get; set; } = 0.0;
        [ExtruderParameter("PID_Limit", "#", "PID TIE 최소 제한값", "0...500", 0.0)]
        public double MINTIE { get; set; } = 0.0;

        // --- Counter ---
        [ExtruderParameter("Counter", "#", "양품 Bin 경고 기준 수량 1", "", 5000)]
        public int MaxGoodBinCount1 { get; set; } = 5000;
        [ExtruderParameter("Counter", "#", "양품 Bin 경고 기준 수량 2", "", 3000)]
        public int MaxGoodBinCount2 { get; set; } = 3000;
        [ExtruderParameter("Counter", "#", "불량 Bin 경고 기준 수량", "", 10000)]
        public int MaxRejectBinCount { get; set; } = 10000;

        // --- Trend (Barrel) ---
        [ExtruderParameter("Trend", "°C", "트렌드용 배럴1 최소 온도", "0...250", 20.0)]
        public double PrefRangeBarrelTemp1LowerLimit { get; set; } = 20.0;
        [ExtruderParameter("Trend", "°C", "트렌드용 배럴1 최대 온도", "0...250", 200.0)]
        public double PrefRangeBarrelTemp1UpperLimit { get; set; } = 200.0;
        [ExtruderParameter("Trend", "°C", "트렌드용 배럴2 최소 온도", "0...250", 20.0)]
        public double PrefRangeBarrelTemp2LowerLimit { get; set; } = 20.0;
        [ExtruderParameter("Trend", "°C", "트렌드용 배럴2 최대 온도", "0...250", 200.0)]
        public double PrefRangeBarrelTemp2UpperLimit { get; set; } = 200.0;

        // --- Trend (Etc) ---
        [ExtruderParameter("Trend", "°C", "트렌드용 냉각조 최소 온도", "5...25", 5.0)]
        public double PrefRangeCirculationBathTempLowerLimit { get; set; } = 5.0;
        [ExtruderParameter("Trend", "°C", "트렌드용 냉각조 최대 온도", "5...25", 15.0)]
        public double PrefRangeCirculationBathTempUpperLimit { get; set; } = 15.0;
        [ExtruderParameter("Trend", "Ncm", "트렌드용 압출 토크 최소값", "2...500", 20.0)]
        public double PrefRangeExtruderTorqueLowerLimit { get; set; } = 20.0;
        [ExtruderParameter("Trend", "Ncm", "트렌드용 압출 토크 최대값", "2...500", 495.0)]
        public double PrefRangeExtruderTorqueUpperLimit { get; set; } = 495.0;
        [ExtruderParameter("Trend", "°C", "트렌드용 플레이트1 최소 온도", "0...150", 20.0)]
        public double PrefRangeHeatingPlate1TempLowerLimit { get; set; } = 20.0;
        [ExtruderParameter("Trend", "°C", "트렌드용 플레이트1 최대 온도", "0...150", 70.0)]
        public double PrefRangeHeatingPlate1TempUpperLimit { get; set; } = 70.0;
        [ExtruderParameter("Trend", "°C", "트렌드용 플레이트2 최소 온도", "0...150", 20.0)]
        public double PrefRangeHeatingPlate2TempLowerLimit { get; set; } = 20.0;
        [ExtruderParameter("Trend", "°C", "트렌드용 플레이트2 최대 온도", "0...150", 70.0)]
        public double PrefRangeHeatingPlate2TempUpperLimit { get; set; } = 70.0;
        [ExtruderParameter("Trend", "mm", "트렌드용 타원도 최소값", "0...1.6", 0.0)]
        public double PrefRangeOvalityLowerLimit { get; set; } = 0.0;
        [ExtruderParameter("Trend", "mm", "트렌드용 타원도 최대값", "0...1.6", 0.5)]
        public double PrefRangeOvalityUpperLimit { get; set; } = 0.5;

        // --- Puller ---
        [ExtruderParameter("Puller", "mm/s", "트렌드용 풀러 최소 속도", "0...50", 0.1)]
        public double PrefRangePullerMotorSpeedLowerLimit { get; set; } = 0.1;
        [ExtruderParameter("Puller", "mm/s", "트렌드용 풀러 최대 속도", "0...50", 15.0)]
        public double PrefRangePullerMotorSpeedUpperLimit { get; set; } = 15.0;
        [ExtruderParameter("Puller", "mm/s", "풀러 휠 현재 속도", "0...50", 0.0)]
        public double PullerMotorSpeed { get; set; } = 0.0;
        [ExtruderParameter("Puller", "mm/s", "풀러 속도 설정 최소값", "0...50", 0.0)]
        public double PullerMotorSpeedEditRangeLowerLimit { get; set; } = 0.0;
        [ExtruderParameter("Puller", "mm/s", "풀러 속도 설정 최대값", "0...120", 99.0)]
        public double PullerMotorSpeedEditRangeUpperLimit { get; set; } = 99.0;
        [ExtruderParameter("Puller", "mm/s", "풀러 시작 속도", "0...50", 0.0)]
        public double PullerStartSpeed { get; set; } = 0.0;

        // --- Sorter ---
        [ExtruderParameter("Sorter", "ms", "컷팅 후 슬라이드/소터 동작 지연 시간", "0...1000", 500.0)]
        public double SlideAndSorterDelay { get; set; } = 500.0;

        [ExtruderParameter("Sorter", "", "슬라이드 사용 여부", "no;yes", false)]
        public bool UseSlide { get; set; } = false; // no maps to false
    }
}