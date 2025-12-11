using EQ.Domain.Enums;


namespace EQ.Domain.Interface
{
    public interface IPIOHandover
    {
        /// <summary>
        /// PIO 신호의 값을 설정합니다.
        /// </summary>
        /// <param name="signal">설정할 PIO 신호</param>
        /// <param name="value">설정할 값 (true: ON, false: OFF)</param>
        void SetSignal(PIOSignal signal, bool value);

        /// <summary>
        /// PIO 신호의 현재 값을 가져옵니다.
        /// </summary>
        /// <param name="signal">가져올 PIO 신호</param>
        /// <returns>신호의 현재 값 (true: ON, false: OFF)</returns>
        bool GetSignal(PIOSignal signal);

        /// <summary>
        /// 여러개의 PIO가 있을때 각각의 시작 IO 번호를 넣어줌
        /// </summary>        
        void SetIOStartIndex(IO_IN _input, IO_OUT _output);

        /// <summary>
        /// 특정 PIO 신호가 변경될 때 발생하는 이벤트를 정의할 수 있습니다.
        /// (필요시 추후 추가)
        /// </summary>
        // event Action<PIOSignal, bool> OnSignalChanged;
    }
}