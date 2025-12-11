namespace EQ.Domain.Interface
{
    public interface ITemperatureController
    {
        // 읽기
        double ReadPV();       // 현재 온도
        double ReadSV();       // 설정 온도
        bool IsRunning();      // 동작 중 여부

        // 쓰기
        void WriteSV(double value); // 온도 설정
        void SetRun(bool run);      // 동작 제어 (Run/Stop)
    }
}