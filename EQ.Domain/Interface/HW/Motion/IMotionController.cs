using EQ.Domain.Entities;
using System.Data;

public interface IMotionController
{
    // 초기화 및 종료
    int Init(string parameterPath);
    void Close(); // clsoe 오타 수정

    // Status
    bool ServoOn(int id, int onOff);
    bool HomeClear(int id);
    bool Home(int id);
    bool HomeDone(int id);
    string GetErrorStatus();

    void GetStatus(ref DataTable dt);
    double GetEncoderPosition(int motorIndex); // Postition 오타 수정
    bool GetInPosition(int motorIndex);        // Postition 오타 수정
    bool AlarmReset(int id);

    // Motion
    // (참고: posCommand 클래스도 PosCommand로 변경하는 것을 권장합니다)
    bool MoveAbs(posCommand cmd);
    bool MoveAbs(posCommand[] cmd);

    bool MoveTrq(int motorIndex, double torque, double motorRpm); // _torque -> torque

    bool MoveRel(posCommand cmd);
    bool MoveRel(posCommand[] cmd);

    bool MoveVel(posCommand cmd);
    bool MoveStop(int idx);
    bool MoveEStop(int idx);

    bool JogMoveStart(posCommand cmd, bool dirPositive);
    bool JogMoveStop(int motorIndex);

    // Sync
    bool SyncSet(int masterId, int slaveId);
    bool SyncReset(int masterId, int slaveId); // ReSet -> Reset

    // Torque Control
    bool SetTrq(int motorIndex, double maxTrq, double positiveTrq, double negativeTrq);
    (double, double, double) GetTrq(int motorIndex);

    // Info
    MotionStatus GetMotionStatus(int motorIndex);
    bool GetAbsType(int idx);

    bool AbsoluteHome(int id = -1); // Absolut -> Absolute, 언더스코어 제거
    bool HomeCancel(int id);
    double GetAbsHomePos(int idx);
    bool SaveParameter();
    string GetEcatStatus();
    bool HotConnect();

    bool SetProfile(bool[] profile, double[] jerkRatio);

    #region API_BUFFER
    bool ApiBufferExecute(int chnl);
    bool ApiBufferExecute(params int[] chnl);

    bool SetEventOverride(int ch, bool dirPositive, posCommand[] cmd);

    bool EventOverrideExecute(int ch, bool run); // Excute -> Execute 오타 수정

    // 언더스코어 제거 및 매개변수명 통일
    bool ApiRecordSoftLandingBasic(int chnl, int motorId, double startPos, double endPos, double startVel, double startAcc, double trgPos, double trgtVel, double trgTrq);

    byte ApiBufferGetUserMemoryByte(uint addr);
    #endregion

    // 반환 튜플 이름은 유지하되 매개변수는 소문자로 변경
    (bool singleTurn, uint singleTurnCount, double PosW, double inPosW, string homeType, string homeDir) GetSysParam(int motorIndex);

    #region EtherCAT SDO/PDO
    /// <summary>
    /// SDO Write (Service Data Object)
    /// </summary>
    bool SDO_Write(int slaveId, int sdoIndex, int sdoSubIndex, int writeData);

    /// <summary>
    /// SDO Read (Service Data Object)
    /// </summary>
    byte[] SDO_Read(int slaveId, int sdoIndex, int sdoSubIndex);

    /// <summary>
    /// PDO Read (Process Data Object)
    /// </summary>
    byte[] PDO_Read(int masterId, int slaveId, int pdoIndex, int pdoSubIndex);

    /// <summary>
    /// PDO Write (Process Data Object)
    /// </summary>
    byte[] PDO_Write(int masterId, int slaveId, int pdoIndex, int pdoSubIndex, int writeData);
    #endregion
}