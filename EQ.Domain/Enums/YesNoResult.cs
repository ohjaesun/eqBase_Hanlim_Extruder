
namespace EQ.Domain.Enums
{
    /// <summary>
    /// 모달 다이얼로그의 응답 결과
    /// </summary>
    public enum YesNoResult
    {
        Yes,
        No,
        Cancel // (닫기 버튼 등 예외)
    }

    /// <summary>
    /// 팝업 알림의 종류 (색상/테마 결정)
    /// </summary>
    public enum NotifyType
    {
        Info,
        Warning,
        Error
    }
}