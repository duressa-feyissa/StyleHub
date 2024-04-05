using Application.Contracts.Infrastructure.Services;
using Application.Contracts.Persistance.Repositories;
using Application.Exceptions;
using Application.Features.User_Features.Authentication.Requests.Commands;
using Application.Response;
using MediatR;

namespace Application.Features.User_Features.Authentication.Handlers.Commands
{
    public class EmailOTPSenderHandler
        : IRequestHandler<EmailOTPSenderRequest, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOtpService _otpService;

        public EmailOTPSenderHandler(IUnitOfWork unitOfWork, IOtpService otpService)
        {
            _unitOfWork = unitOfWork;
            _otpService = otpService;
        }

        public async Task<BaseResponse<string>> Handle(
            EmailOTPSenderRequest request,
            CancellationToken cancellationToken
        )
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(request.Email);
            if (user == null)
                throw new NotFoundException("User not found");

            user.EmailVerificationCode = await _otpService.SendVerificationEmailAsync(user, 5);
            user.EmailVerificationCodeExpiration = DateTime.Now.AddMinutes(5);
            await _unitOfWork.UserRepository.Update(user);

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
