using backend.Application.Contracts.Infrastructure.Services;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.User.AuthenticationDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.User_Features.Authentication.Requests.Commands;
using MediatR;

namespace backend.Application.Features.User_Features.Authentication.Handlers.Commands
{
    public class ForgetPasswordHandler(
        IUnitOfWork unitOfWork,
        IOtpService otpService) : IRequestHandler<ForgetPasswordRequest, ForgetPasswordResponse>
    {
        public async Task<ForgetPasswordResponse> Handle(
            ForgetPasswordRequest request,
            CancellationToken cancellationToken
        )
        {
            var user = await unitOfWork.UserRepository.GetByEmail(request.Email);
            if (user == null)
                throw new NotFoundException("User not found");
            if (user.IsEmailVerified == false)
                throw new BadRequestException("Email is already verified");

            user.ResetPasswordCode = await otpService.SendVerificationEmailAsync(user, 5);
            user.ResetPasswordCodeExpiration = DateTime.Now.AddMinutes(5);
            await unitOfWork.UserRepository.Update(user);

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
