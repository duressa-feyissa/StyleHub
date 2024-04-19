using backend.Application.Contracts.Infrastructure.Services;
using backend.Application.DTO.User.AuthenticationDTO.DTO;
using backend.Domain.Entities.User;
using backend.Infrastructure.Configuration;

namespace backend.Infrastructure.Repository
{
    public class OtpService(PhoneNumberOTPManager phoneOTPManager, IEmailSender emailSender)
        : IOtpService
    {
        public Task<int> GenerateOtpAsync()
        {
            return Task.FromResult(new Random().Next(1000, 9999));
        }

        public async Task<string> SendVerificationEmailAsync(
            User user,
            int otpExpirationMinutes = 5
        )
        {
            var otp = await GenerateOtpAsync();

            var email = new EmailDTO
            {
                To = user.Email!,
                Subject = "Verification OTP",
                Body = GenerateEmailBody(
                    "Verification OTP",
                    user.FirstName ?? "User",
                    otpExpirationMinutes,
                    "verifying your email",
                    otp
                )
            };
            await emailSender.SendEmail(email);
            return otp.ToString();
        }

        public async Task<string> SendVerificationOtpAsync(User user)
        {
            var otp = "1234"; //await GenerateOtpAsync();
            await phoneOTPManager.SendOTPAsync(user.PhoneNumber!, otp.ToString());
            return otp.ToString();
        }

        private string GenerateEmailBody(
            string title,
            string name,
            int expirationMinutes,
            string action,
            int otp
        )
        {
            return $@"
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset=""UTF-8"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>{title}</title>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    padding: 20px;
                }}
                .container {{
                    max-width: 600px;
                    margin: 0 auto;
                    background-color: #fff;
                    border-radius: 8px;
                    padding: 30px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                }}
                h1 {{
                    color: #333;
                }}
                p {{
                    color: #666;
                    margin-bottom: 20px;
                }}
                strong {{
                    color: #007bff;
                }}
                .info {{
                    padding: 10px;
                    background-color: #f0f0f0;
                    border-radius: 5px;
                }}
            </style>
        </head>
        <body>
            <div class=""container"">
                <h1>Hi {name},</h1>
                <p>
                    Your OTP for {action} is <strong>{otp}</strong>. 
                    This OTP will expire in {expirationMinutes} minutes.
                </p>
                <div class=""info"">
                    <p>For your security, please do not share this OTP with anyone.</p>
                    <p>If you didn't request this OTP, please ignore this email.</p>
                </div>
            </div>
        </body>
        </html>
    ";
        }
    }
}
