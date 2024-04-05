using System.ComponentModel.DataAnnotations;
using Application.Contracts.Infrastructure.Services;
using Application.Contracts.Persistance.Repositories;
using Application.DTO.User.AuthenticationDTO.DTO;
using Application.DTO.User.UserDTO.Validations;
using Application.Exceptions;
using Application.Features.User_Features.Authentication.Requests.Commands;
using Application.Response;
using AutoMapper;
using MediatR;

namespace Application.Features.User_Features.Authentication.Handlers.Commands
{
    public class LoginUserHandler
        : IRequestHandler<LoginUserRequest, BaseResponse<AuthenticationResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOtpService _otpService;
        private readonly IAuthenticationService _authenticationService;

        public LoginUserHandler(
            IUnitOfWork unitOfWork,
            IOtpService otpService,
            IAuthenticationService authenticationService
        )
        {
            _unitOfWork = unitOfWork;
            _otpService = otpService;
            _authenticationService = authenticationService;
        }

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
                var user = await _unitOfWork.UserRepository.GetByEmail(request.LoginRequest.Email);
                if (user == null)
                    throw new NotFoundException("User not found");

                if (user.IsEmailVerified == false)
                {
                    user.EmailVerificationCode = await _otpService.SendVerificationEmailAsync(
                        user,
                        5
                    );
                    user.EmailVerificationCodeExpiration = DateTime.Now.AddMinutes(5);
                    await _unitOfWork.UserRepository.Update(user);
                    throw new BadRequestException("Email not verified");
                }

                var token = _authenticationService.Login(request.LoginRequest, user, true);

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
