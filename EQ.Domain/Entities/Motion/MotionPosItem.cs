using EQ.Domain.Enums;
using Newtonsoft.Json;
using System.ComponentModel;

namespace EQ.Domain.Entities
{
    /// <summary>
    /// 포지션의 성격/분류 (필터링 및 구분용)
    /// </summary>
    public enum PosGroup
    {
        Stage,
        Loader,
        SectionXXX,
    }

    // 1. 포지션 이름
    public enum DefinePos { Wait, Target , VisionStart , VisionEnd }   


    // 4. 실제 데이터가 담길 그릇 (사용자의 _MotionPos 역할)
    public class MotionPosItem
    {
        public MotionID Axis { get; set; }       // 모터 ID (예: STAGE_X)
        public string Name { get; set; }         // Enum.ToString() 값
        public string Key => $"{Axis}_{Name}";   // ★ 고유 키 (예: STAGE_X_Wait)

        public double Position { get; set; }     // 위치 값
        public double Speed { get; set; } = 0;       // 설정하면 여기값 0이면 모션speed 설정값
        public double Acc { get; set; } = 0;       // 설정하면 여기값 0이면 모션speed 설정값
        public double Dec { get; set; } = 0;       // 설정하면 여기값 0이면 모션speed 설정값
        public PosGroup Group { get; set; }      // 그룹 (대기, 이동 등)
        public string Description { get; set; }  // 설명

        public MotionPosItem() { } // for serialization
        public MotionPosItem(MotionID axis, string name, PosGroup group, string desc)
        {
            Axis = axis;
            Name = name;
            Group = group;
            Description = desc;
        }
    }

    public class UserOptionMotionPos
    {
        // 저장용 데이터
        public List<MotionPosItem> Items { get; set; } = new List<MotionPosItem>();

        // 런타임 검색용 맵
        [JsonIgnore]
        public Dictionary<string, MotionPosItem> _dicMap = new Dictionary<string, MotionPosItem>();

        public UserOptionMotionPos() { }

        public void Synchronize()
        {
            var validItems = new List<MotionPosItem>();
            var allMotors = Enum.GetValues(typeof(MotionID)).Cast<MotionID>();

            foreach (var motor in allMotors)
            {
                // ★ 변경 포인트: Key를 'DefinePos'로 고정
                var targetEnum = new Dictionary<DefinePos, (PosGroup, string)>();

                // 사용자 요청 로직 적용
                switch (motor)
                {
                    case MotionID.STAGE_X:
                    case MotionID.STAGE_Y:
                        targetEnum.Add(DefinePos.Wait, (PosGroup.Stage, "대기 위치"));
                        targetEnum.Add(DefinePos.Target, (PosGroup.Stage, "작업 위치"));
                        targetEnum.Add(DefinePos.VisionStart, (PosGroup.Stage, "비전 시작"));
                        targetEnum.Add(DefinePos.VisionEnd, (PosGroup.Stage, "비전 종료"));
                        break;

                    case MotionID.STAGE_Z:
                        targetEnum.Add(DefinePos.Wait, (PosGroup.Stage, "대기 위치"));
                        targetEnum.Add(DefinePos.Target, (PosGroup.Stage, "상승"));
                        targetEnum.Add(DefinePos.VisionStart, (PosGroup.Stage, "하강"));
                        break;

                    default: // 나머지 모터 공통
                        targetEnum.Add(DefinePos.Wait, (PosGroup.Stage, "기본 대기"));
                        targetEnum.Add(DefinePos.Target, (PosGroup.Stage, "유지보수"));
                        break;
                }

                // 리스트 생성 로직
                foreach (var kvp in targetEnum)
                {
                    DefinePos posEnum = kvp.Key;
                    string posName = posEnum.ToString();

                    // ★ 고유 키 생성: "축이름_DefinePos이름" (예: STAGE_X_Wait)
                    string key = $"{motor}_{posName}";

                    var existing = Items.FirstOrDefault(x => x.Key == key);

                    if (existing != null)
                    {
                        // 기존 데이터 유지 (설명 업데이트 필요 시 여기 추가)
                        validItems.Add(existing);
                    }
                    else
                    {
                        // 신규 생성
                        var newItem = new MotionPosItem(motor, posName, kvp.Value.Item1, kvp.Value.Item2);
                        validItems.Add(newItem);
                    }
                }
            }

            Items = validItems.OrderBy(x => x.Axis).ThenBy(x => x.Group).ToList();
            _dicMap = Items.ToDictionary(x => x.Key);
        }

        public MotionPosItem Get(string key)
        {
            return _dicMap.TryGetValue(key, out var item) ? item : null;
        }
    }
}