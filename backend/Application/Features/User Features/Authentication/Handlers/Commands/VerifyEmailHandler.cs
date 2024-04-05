using Application.Contracts.Infrastructure.Services;
using Application.Contracts.Persistance.Repositories;
using Application.DTO.User.AuthenticationDTO.DTO;
using Application.Exceptions;
using Application.Features.User_Features.Authentication.Requests.Commands;
using MediatR;

namespace Application.Features.User_Features.Authentication.Handlers.Commands
{
    public class VerifyEmailHandler : IRequestHandler<VerifyEmailRequest, VerifyEmailResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;

        public VerifyEmailHandler(
            IUnitOfWork unitOfWork,
            IAuthenticationService authenticationService
        )
        {
            _unitOfWork = unitOfWork;
            _authenticationService = authenticationService;
        }

        public async Task<VerifyEmailResponseDTO> Handle(
            VerifyEmailRequest request,
            CancellationToken cancellationToken
        )
        {
            var user = await _unitOfWork.UserRepository.GetByEmail(request.Email);
            if (user == null)
                throw new NotFoundException("User not found");
            if (user.IsEmailVerified)
                throw new BadRequestException("Email is already verified");
            if (user.EmailVerificationCode != request.Code)
                throw new BadRequestException("Invalid email verification code");

            if (user.EmailVerificationCodeExpiration < DateTime.Now)
                throw new BadRequestException("Email verification code has expired");

            var token = _authenticationService.Login(
                new LoginRequestDTO { Email = user.Email, Password = user.Password },
                user,
                false
            );

            user.IsEmailVerified = true;
            user.EmailVerificationCode = null;
            user.EmailVerificationCodeExpiration = null;

            await _unitOfWork.UserRepository.Update(user);

            return new VerifyEmailResponseDTO
            {
                Email = user.Email!,
                IsVerified = user.IsEmailVerified,
                Message = "Email verified successfully",
                VerificationDate = DateTime.Now,
                Token = token.Token!
            };
        }
    }
}
