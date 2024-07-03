using backend.Application.Contracts.Infrastructure.Services;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.User.AuthenticationDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.User_Features.Authentication.Requests.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace backend.Application.Features.User_Features.Authentication.Handlers.Commands
{
    public class PasswordResetCodeVerificationHandler : IRequestHandler<PasswordResetCodeVerificationRequest, PasswordResetCodeVerificationResponseDTO>
    {
        private readonly IUnitOfWork unitOfWork;

        public PasswordResetCodeVerificationHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<PasswordResetCodeVerificationResponseDTO> Handle(PasswordResetCodeVerificationRequest request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.UserRepository.GetByEmail(request.Email);
            if (user == null)
                throw new NotFoundException("User not found");

            if (user.ResetPasswordCode != request.Code)
                throw new BadRequestException("Invalid password reset code");

            if (user.ResetPasswordCodeExpiration < DateTime.Now)
                throw new BadRequestException("Password reset code expired");

            return new PasswordResetCodeVerificationResponseDTO
            {
                Email = user.Email!,
                IsVerified = true,
                Message = "Password reset code verified successfully",
                VerificationDate = DateTime.Now
            };
        }
    }
}
