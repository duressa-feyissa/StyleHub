using System.Text;
using Application.Common;
using Application.Contracts.Infrastructure.Services;
using Application.Contracts.Persistance.Repositories;
using Application.DTO.User.AuthenticationDTO.Validations;
using Application.DTO.User.UserDTO.Validations;
using Application.Exceptions;
using Application.Features.User_Features.Authentication.Requests.Commands;
using Application.Response;
using AutoMapper;
using Domain.Entities.User;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;

namespace Application.Features.User_Features.Authentication.Handlers.Commands
{
    public class RegisterUserHandler
        : IRequestHandler<RegisterUserRequest, BaseResponse<RegisterationResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private readonly ApiSettings _apiSettings;

        private readonly IOtpService _otpService;

        public RegisterUserHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IOtpService otpService,
            IOptions<ApiSettings> apiSettings
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _otpService = otpService;
            _apiSettings = apiSettings.Value;
        }

        public async Task<BaseResponse<RegisterationResponseDTO>> Handle(
            RegisterUserRequest request,
            CancellationToken cancellationToken
        )
        {
            var user = _mapper.Map<Domain.Entities.User.User>(request.Registeration);

            var validator = new RegisterUserValidation(_unitOfWork.UserRepository);

            var validationResult = await validator.ValidateAsync(request.Registeration!);
            if (!validationResult.IsValid)
            {
                throw new BadRequestException(validationResult.Errors.First().ErrorMessage);
            }

            var role = await _unitOfWork.RoleRepository.GetByName("user");
            if (role == null)
                throw new NotFoundException("Role Not Found");
            user.Role = role;
            if (request.Registeration.Email != null)
            {
                user.Password = HashPassword(request.Registeration.Password!);
                user.Email = request.Registeration.Email;
                user.EmailVerificationCode = await _otpService.SendVerificationEmailAsync(user);
                user.EmailVerificationCodeExpiration = DateTime.Now.AddMinutes(5);
                user.PhoneNumberVerificationCode = null;
                user = await _unitOfWork.UserRepository.Add(user);
                return new BaseResponse<RegisterationResponseDTO>
                {
                    Message = "User Registered Successfully",
                    Success = true,
                    Data = _mapper.Map<RegisterationResponseDTO>(user)
                };
            }
            // else if (request.Registeration.PhoneNumber != null)
            // {
            //     user.Password = HashPassword(request.Registeration.PhoneNumber);
            //     user.PhoneNumberVerificationCode = await _otpService.SendVerificationOtpAsync(user);
            //     user.PhoneNumberVerificationCodeExpiration = DateTime.Now.AddMinutes(5);
            //     user.Email = null;
            //     user = await _unitOfWork.UserRepository.Add(user);
            //     return new BaseResponse<RegisterationResponseDTO>
            //     {
            //         Message = "User Registered Successfully",
            //         Success = true,
            //         Data = _mapper.Map<RegisterationResponseDTO>(user)
            //     };
            // }
            else
            {
                throw new BadRequestException("Either Email or Phone Number is required");
            }
        }

        private string HashPassword(string password)
        {
            var saltBytes = Encoding.UTF8.GetBytes(_apiSettings.SecretKey ?? "SecretKey");

            string hashedPassword = Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    password: password,
                    salt: saltBytes,
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8
                )
            );
            return hashedPassword;
        }
    }
}
