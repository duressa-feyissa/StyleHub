using backend.Application.Contracts.Infrastructure.Services;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.User.AuthenticationDTO.DTO;
using backend.Application.DTO.User.AuthenticationDTO.Validations;
using backend.Application.Exceptions;
using backend.Application.Features.User_Features.Authentication.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.User_Features.Authentication.Handlers.Commands
{
    public class LoginUserHandler(
        IUnitOfWork unitOfWork,
        IOtpService otpService,
        IAuthenticationService authenticationService)
        : IRequestHandler<LoginUserRequest, BaseResponse<AuthenticationResponseDTO>>
    {
        public async Task<BaseResponse<AuthenticationResponseDTO>> Handle(
            LoginUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var validator = new LoginUserValidation();
            var validationResult = await validator.ValidateAsync(request.LoginRequest!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }
            if (request.LoginRequest.Email != null)
            {
                var user = await unitOfWork.UserRepository.GetByEmail(request.LoginRequest.Email);
                if (user == null)
                    throw new NotFoundException("User not found");

                if (user.IsEmailVerified == false)
                {
                    user.EmailVerificationCode = await otpService.SendVerificationEmailAsync(
                        user,
                        5
                    );
                    user.EmailVerificationCodeExpiration = DateTime.Now.AddMinutes(5);
                    await unitOfWork.UserRepository.Update(user);
                    throw new BadRequestException("Email not verified");
                }

                var token = authenticationService.Login(request.LoginRequest, user, true);

                return new BaseResponse<AuthenticationResponseDTO>
                {
                    Data = token,
                    Message = "Login successful",
                    Success = true
                };
            }
            // else if (request.LoginRequest.PhoneNumber != null)
            // {
            //     var user = await _unitOfWork.UserRepository.GetByPhoneNumber(
            //         request.LoginRequest.PhoneNumber
            //     );
            //     if (user == null)
            //         throw new NotFoundException("User not found");

            //     var token = _authenticationService.Login(request.LoginRequest, user, false);

            //     return new BaseResponse<AuthenticationResponseDTO>
            //     {
            //         Data = token,
            //         Message = "Login successful",
            //         Success = true
            //     };
            // }
            else
                throw new BadRequestException("Invalid request");
        }
    }
}
