namespace Application.Contracts.Infrastructure.Services
{
    public interface IPhoneOTPSender
    {
        Task SendOtpAsync(string from, string to, string body);
    }
}
