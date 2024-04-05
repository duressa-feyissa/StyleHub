using System.Text;
using Application.Common;
using Application.Contracts.Infrastructure.Services;
using Application.Contracts.Persistance.Repositories;
using Application.DTO.User.AuthenticationDTO.DTO;
using Application.Exceptions;
using Application.Features.User_Features.Authentication.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;

namespace Application.Features.User_Features.Authentication.Handlers.Commands
{
	public class ResetPasswordHandler
		: IRequestHandler<ResetPasswordRequest, AuthenticationResponseDTO>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IOtpService _otpService;
		private readonly IAuthenticationService _authenticationService;

		private readonly ApiSettings _apiSettings;

		public ResetPasswordHandler(
			IUnitOfWork unitOfWork,
			IOtpService otpService,
			IAuthenticationService authenticationService,
			IOptions<ApiSettings> apiSettings
		)
		{
			_unitOfWork = unitOfWork;
			_otpService = otpService;
			_authenticationService = authenticationService;
			_apiSettings = apiSettings.Value;
		}

		public async Task<AuthenticationResponseDTO> Handle(
			ResetPasswordRequest request,
			CancellationToken cancellationToken
		)
		{
			if (request.Email == null)
				throw new BadRequestException("Email is required");

			var user = await _unitOfWork.UserRepository.GetByEmail(request.Email!);
			if (user == null)
				throw new NotFoundException("User not found");

			if (user.IsEmailVerified == false)
			{
				user.EmailVerificationCode = await _otpService.SendVerificationEmailAsync(user, 5);
				user.EmailVerificationCodeExpiration = DateTime.Now.AddMinutes(5);
				await _unitOfWork.UserRepository.Update(user);
				throw new BadRequestException("Email not verified");
			}

			if (user.ResetPasswordCode != request.Code)
				throw new BadRequestException("Invalid reset password code");

			if (user.ResetPasswordCodeExpiration < DateTime.Now)
				throw new BadRequestException("Reset password code has expired");

			user.Password = HashPassword(request.Password);

			var token = _authenticationService.Login(
				new LoginRequestDTO { Email = user.Email, Password = user.Password },
				user,
				false
			);
			user.ResetPasswordCode = null;
			user.ResetPasswordCodeExpiration = null;
			await _unitOfWork.UserRepository.Update(user);

			return new AuthenticationResponseDTO
			{
				Id = user.Id,
				Email = user.Email!,
				FirstName = user.FirstName,
				LastName = user.LastName,
				PhoneNumber = user.PhoneNumber,
				Role = user.Role,
				Token = token.Token!,
			};
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
