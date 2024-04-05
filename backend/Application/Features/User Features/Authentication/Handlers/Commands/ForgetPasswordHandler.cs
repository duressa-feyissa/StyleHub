using Application.Contracts.Infrastructure.Services;
using Application.Contracts.Persistance.Repositories;
using Application.DTO.User.AuthenticationDTO.DTO;
using Application.Exceptions;
using Application.Features.User_Features.Authentication.Requests.Commands;
using MediatR;

namespace Application.Features.User_Features.Authentication.Handlers.Commands
{
    public class ForgetPasswordHandler
        : IRequestHandler<ForgetPasswordRequest, ForgetPasswordResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOtpService _otpService;

        public ForgetPasswordHandler(
            IUnitOfWork unitOfWork,
            IOtpService otpService
        )
        {
            _unitOfWork = unitOfWork;
            _otpService = otpService;
        }

        public async Task<ForgetPasswordResponse> Handle(
            ForgetPasswordRequest request,
            CancellationToken cancellationToken
        )
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(request.Email);
            if (user == null)
                throw new NotFoundException("User not found");
            if (user.IsEmailVerified == false)
                throw new BadRequestException("Email is already verified");

            user.ResetPasswordCode = await _otpService.SendVerificationEmailAsync(user, 5);
            user.ResetPasswordCodeExpiration = DateTime.Now.AddMinutes(5);
            await _unitOfWork.UserRepository.Update(user);

            return new ForgetPasswordResponse
            {
                Email = user.Email!,
                Message = "Reset password code sent",
                Status = "Pending",
                ExpirationDate = user.ResetPasswordCodeExpiration!.Value
            };
        }
    }
}
