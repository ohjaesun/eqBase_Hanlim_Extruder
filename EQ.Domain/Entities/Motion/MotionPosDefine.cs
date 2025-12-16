using EQ.Domain.Enums;

namespace EQ.Domain.Entities
{
    // 1. 리턴 타입 정의 (Key 생성 로직 포함)
    public struct MotionKey
    {
        public MotionID Axis { get; }
        public string Key { get; }

        // 생성자에서 DefinePos를 받도록 변경
        public MotionKey(MotionID axis, DefinePos pos)
        {
            Axis = axis;
            // UserOptionMotionPos의 키 생성 규칙과 일치해야 함
            Key = $"{axis}_{pos}";
        }
    }

    // 2. 사용 편의성을 위한 정적 클래스 모음

    public static class STAGE_X
    {
        public static MotionKey Wait => new MotionKey(MotionID.PULLER_T, DefinePos.Wait);
        public static MotionKey Target => new MotionKey(MotionID.PULLER_T, DefinePos.Target);
        public static MotionKey VisionStart => new MotionKey(MotionID.PULLER_T, DefinePos.VisionStart);
        public static MotionKey VisionEnd => new MotionKey(MotionID.PULLER_T, DefinePos.VisionEnd);
    }

    public static class STAGE_Y
    {
        public static MotionKey Wait => new MotionKey(MotionID.FEEDER_T, DefinePos.Wait);
        public static MotionKey Target => new MotionKey(MotionID.FEEDER_T, DefinePos.Target);
        public static MotionKey VisionStart => new MotionKey(MotionID.FEEDER_T, DefinePos.VisionStart);
        public static MotionKey VisionEnd => new MotionKey(MotionID.FEEDER_T, DefinePos.VisionEnd);
    }

    public static class STAGE_Z
    {
        public static MotionKey Wait => new MotionKey(MotionID.SCREW_T, DefinePos.Wait);
        public static MotionKey Up => new MotionKey(MotionID.SCREW_T, DefinePos.Target); // Target을 Up으로 매핑
        public static MotionKey Down => new MotionKey(MotionID.SCREW_T, DefinePos.VisionStart); // VisionStart를 Down으로 매핑
    }

    // 필요한 다른 축들도 동일한 패턴으로 추가...
}