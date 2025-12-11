namespace EQ.Domain.Enums
{
    public enum PIOId
    {
        LoadPort1,
        UnLoadPort1,
    }

    /// <summary>
    /// SEMI E84 PIO Interface Signals (8-Bit I/O)  
    /// </summary>
    public enum PIOSignal
    {
        // ==========================================
        //  OUTPUTS (Tx): Equipment -> OHT (Passive -> Active)
        // ==========================================

        /// <summary>Bit 0: Load Request</summary>
        L_REQ,

        /// <summary>Bit 1: Unload Request</summary>
        U_REQ,

        /// <summary>Bit 2: Ready</summary>
        READY,

        /// <summary>Bit 3: Handoff Available</summary>
        HO_AVBL,

        /// <summary>Bit 4: Emergency Stop (옵션)</summary>
        ES,

        /// <summary>Bit 5: Reserved (Output)</summary>
        RES_OUT_5,

        /// <summary>Bit 6: Reserved (Output)</summary>
        RES_OUT_6,

        /// <summary>Bit 7: Reserved (Output)</summary>
        RES_OUT_7,


        // ==========================================
        //  INPUTS (Rx): OHT -> Equipment (Active -> Passive)
        // ==========================================

        /// <summary>Bit 0: Valid</summary>
        VALID,

        /// <summary>Bit 1: Carrier Select 0</summary>
        CS_0,

        /// <summary>Bit 2: Carrier Select 1</summary>
        CS_1,

        /// <summary>Bit 3: Transfer Request</summary>
        TR_REQ,

        /// <summary>Bit 4: Busy</summary>
        BUSY,

        /// <summary>Bit 5: Complete</summary>
        COMPT,

        /// <summary>Bit 6: Continue</summary>
        CONT,

        /// <summary>Bit 7: AM Available (또는 Reserved)</summary>
        AM_AVBL,
    }

    /// <summary>
    /// PIO(Parallel I/O) 핸드오버 시퀀스의 현재 상태를 나타냅니다.
    /// </summary>
    public enum PIOState
    {
        /// <summary>
        /// 유휴 상태 (아무런 핸드오버 시퀀스가 진행 중이지 않음)
        /// </summary>
        Idle,

        /// <summary>
        /// 재료 반입 (Load)을 요청하는 중
        /// </summary>
        RequestingLoad,

        /// <summary>
        /// 재료 반입이 진행 중
        /// </summary>
        LoadInProgress,

        /// <summary>
        /// 재료 반출 (Unload)을 요청하는 중
        /// </summary>
        RequestingUnload,

        /// <summary>
        /// 재료 반출이 진행 중
        /// </summary>
        UnloadInProgress,

        /// <summary>
        /// 핸드오버 시퀀스 완료
        /// </summary>
        Completed,

        /// <summary>
        /// 핸드오버 시퀀스 중 오류 발생
        /// </summary>
        Error,

        /// <summary>
        /// 핸드오버 시퀀스 중 타임아웃 발생
        /// </summary>
        Timeout,

        /// <summary>
        /// 핸드오버 시퀀스 강제 중단
        /// </summary>
        Aborted
    }
}
