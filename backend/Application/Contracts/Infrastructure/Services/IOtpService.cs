using backend.Domain.Entities.User;

namespace backend.Application.Contracts.Infrastructure.Services
{
    public interface IOtpService
    {
        public Task<string> SendVerificationOtpAsync(User user);
        public Task<string> SendVerificationEmailAsync(User user, int otpExpirationMinutes = 5);
    }
}
