namespace Application.Contracts.Infrastructure.Services
{
    public interface ICurrentLoggedInService
    {
        Guid GetCurrentLoggedInId();
        string GetCurrentLoggedInUsername();
        string GetCurrentLoggedInPhoneNumber();
        string GetCurrentLoggedInEmail();
        string GetCurrentLoggedInRole();
        int GetCurrentLoggedInPriority();
    }
}
