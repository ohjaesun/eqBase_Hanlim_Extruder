namespace EQ.Domain.Entities.Unit
{
    /// <summary>
    /// TOHO
    /// </summary>
    public enum TOHO_RegMap : ushort
    {
        // Read
        PV_Read = 0x0000,
        SV_Read = 0x0402,
        Status_Read = 0x0408,

        // Write
        SV_Write = 0x0402,
        RunStop_Write = 0x0408
    }

    /// <summary>
    /// 한영 넉스
    /// </summary>
    public enum VX4_RegMap : ushort
    {
        // Read
        PV = 0x0000,
        SV = 0x0001,
        Status = 0x000A,

        // Write
        SV_Write = 0x0067,
        RunStop_Write = 0x0021
    }
}