using EQ.Domain.Enums;
using System.ComponentModel;

namespace EQ.Domain.Entities
{
    // 개별 모터 속도 클래스   
    public class MotionSpeed
    {
        [ReadOnly(true)]
        public MotionID Axis { get; set; } // ID를 Enum으로 변경

        [Category("Speed")] public double AutoSpeed { get; set; } = 300;
        [Category("Speed")] public double ManualSpeed { get; set; } = 100;
        [Category("Speed")] public double HomeSpeed { get; set; } = 50;
        [Category("Jog")] public double JogPastSpeed { get; set; } = 300;
        [Category("Jog")] public double JogNormalSpeed { get; set; } = 100;
        [Category("Accel")] public double Accel { get; set; } = 5000;
        [Category("Accel")] public double Deaccel { get; set; } = 5000;
        [Category("Profile")] public bool SCurve { get; set; } = true;
        [Category("Profile")] public double JerkRatio { get; set; } = 0.75;

        public MotionSpeed() { } // 빈 생성자 (Serializer용)
        public MotionSpeed(MotionID id) { Axis = id; }
    }

    // 설정 파일 단위가 될 클래스
    public class UserOptionMotionSpeed
    {
        // Dictionary 대신 List 사용 (JSON 직렬화 및 PropertyGrid 호환성 위함)
        public List<MotionSpeed> SpeedList { get; set; } = new List<MotionSpeed>();

        public UserOptionMotionSpeed()
        {
           
        }
        
        public MotionSpeed Get(MotionID id)
        {
            return SpeedList.FirstOrDefault(x => x.Axis == id) ?? new MotionSpeed(id);
        }

        public void Synchronize()
        {
            var currentIds = Enum.GetValues(typeof(MotionID)).Cast<MotionID>().ToList();

            // 1. [삭제 대응] 현재 Enum에 없는 ID는 리스트에서 제거 (Garbage 정리)
            SpeedList.RemoveAll(item => !currentIds.Contains(item.Axis));

            // 2. [추가 대응] 리스트에 없는 ID는 새로 추가 (기본값 생성)
            foreach (var id in currentIds)
            {
                if (!SpeedList.Any(item => item.Axis == id))
                {
                    SpeedList.Add(new MotionSpeed(id));
                }
            }

            // 3. [정렬] 보기 좋게 Enum 순서대로 정렬
            SpeedList = SpeedList.OrderBy(item => item.Axis).ToList();

            // 4. [중복 제거] 혹시 모를 중복 방지
            SpeedList = SpeedList.GroupBy(x => x.Axis).Select(g => g.First()).ToList();
        }
    }
}