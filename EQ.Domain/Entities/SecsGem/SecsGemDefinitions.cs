using EQ.Domain.Enums.SecsGem;

namespace EQ.Domain.Entities.SecsGem
{
    /// <summary>
    /// SECS/GEM 정의 데이터
    /// CommonData\SecsGem 폴더에 JSON 파일로 저장/로드됩니다.
    /// </summary>
    public class SecsGemDefinitions
    {
        /// <summary>
        /// SVID 목록
        /// </summary>
        public List<StatusVariable> SVIDs { get; set; } = new();

        /// <summary>
        /// ECID 목록
        /// </summary>
        public List<EquipmentConstant> ECIDs { get; set; } = new();

        /// <summary>
        /// CEID 목록
        /// </summary>
        public List<CollectionEvent> CEIDs { get; set; } = new();

        /// <summary>
        /// ALID 목록
        /// </summary>
        public List<AlarmDefinition> ALIDs { get; set; } = new();

        /// <summary>
        /// RCMD 목록
        /// </summary>
        public List<RemoteCommandDef> RCMDs { get; set; } = new();

        /// <summary>
        /// 기본값으로 초기화된 Definitions 생성
        /// </summary>
        public static SecsGemDefinitions CreateDefault()
        {
            var defs = new SecsGemDefinitions();

            // 기본 SVID 추가
            defs.SVIDs.Add(new StatusVariable(1, "EquipmentStatus", "A") { Description = "장비 상태" });
            defs.SVIDs.Add(new StatusVariable(2, "ProcessState", "A") { Description = "공정 상태" });
            defs.SVIDs.Add(new StatusVariable(3, "AlarmCount", "U4") { Description = "현재 알람 개수" });

            // 기본 ECID 추가
            defs.ECIDs.Add(new EquipmentConstant(1, "Speed", "mm/s", "U4") { Description = "이동 속도" });
            defs.ECIDs.Add(new EquipmentConstant(2, "Acceleration", "mm/s2", "U4") { Description = "가속도" });

            // 기본 CEID 추가
            defs.CEIDs.Add(new CollectionEvent(1, "EquipmentOnline", "장비 온라인"));
            defs.CEIDs.Add(new CollectionEvent(2, "EquipmentOffline", "장비 오프라인"));
            defs.CEIDs.Add(new CollectionEvent(3, "ProcessStarted", "공정 시작"));
            defs.CEIDs.Add(new CollectionEvent(4, "ProcessCompleted", "공정 완료"));
            defs.CEIDs.Add(new CollectionEvent(5, "AlarmSet", "알람 발생"));
            defs.CEIDs.Add(new CollectionEvent(6, "AlarmCleared", "알람 해제"));

            // 기본 ALID 추가
            defs.ALIDs.Add(new AlarmDefinition(1, "System Error", "1") { Description = "시스템 에러" });
            defs.ALIDs.Add(new AlarmDefinition(2, "Motion Error", "1") { Description = "모션 에러" });
            defs.ALIDs.Add(new AlarmDefinition(3, "Sensor Error", "1") { Description = "센서 에러" });

            // 기본 RCMD 추가
            defs.RCMDs.Add(new RemoteCommandDef("START", "공정 시작"));
            defs.RCMDs.Add(new RemoteCommandDef("STOP", "공정 정지"));
            defs.RCMDs.Add(new RemoteCommandDef("PAUSE", "일시 정지"));
            defs.RCMDs.Add(new RemoteCommandDef("RESUME", "재개"));

            return defs;
        }
    }
}
