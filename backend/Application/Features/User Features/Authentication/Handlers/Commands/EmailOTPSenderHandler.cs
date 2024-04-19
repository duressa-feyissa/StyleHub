using backend.Application.Contracts.Infrastructure.Services;
using backend.Application.Contracts.Persistence;
using backend.Application.Exceptions;
using backend.Application.Features.User_Features.Authentication.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.User_Features.Authentication.Handlers.Commands
{
    public class EmailOTPSenderHandler(IUnitOfWork unitOfWork, IOtpService otpService)
        : IRequestHandler<EmailOTPSenderRequest, BaseResponse<string>>
    {
        public async Task<BaseResponse<string>> Handle(
            EmailOTPSenderRequest request,
            CancellationToken cancellationToken
        )
        {
            var user = await unitOfWork.UserRepository.GetByEmail(request.Email);
            if (user == null)
                throw new NotFoundException("User not found");

            user.EmailVerificationCode = await otpService.SendVerificationEmailAsync(user, 5);
            user.EmailVerificationCodeExpiration = DateTime.Now.AddMinutes(5);
            await unitOfWork.UserRepository.Update(user);

            return new BaseResponse<string>
            {
                Data =
                    "The email verification code has been sent to your email address. Please check your email. If you do not receive the email, please check your spam folder.",
                Message = "Email verification code sent",
                Success = true
            };
        }
    }
}
