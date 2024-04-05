using Domain.Entities.User;

namespace Application.Contracts.Infrastructure.Services
{
    public interface IOtpService
    {
        public Task<string> SendVerificationOtpAsync(User user);
        public Task<string> SendVerificationEmailAsync(User user, int otpExpirationMinutes = 5);
    }
}
