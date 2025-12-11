// EQ.Domain/Interface/IConfirmationService.cs
using EQ.Domain.Enums;
using System.Threading.Tasks;

namespace EQ.Domain.Interface
{
    /// <summary>
    /// Core가 UI에 Yes/No 확인을 요청하고
    /// 비동기(await)로 결과를 기다릴 수 있게 하는 서비스
    /// </summary>
    public interface IConfirmationService
    {
        Task<YesNoResult> ConfirmAsync(string title, string message, NotifyType type = NotifyType.Info);
    }
}