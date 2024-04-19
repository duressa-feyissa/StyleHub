using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using backend.Application.Common;
using backend.Application.Contracts.Infrastructure.Services;
using backend.Application.DTO.User.AuthenticationDTO.DTO;
using backend.Application.Exceptions;
using backend.Domain.Entities.User;
using backend.Infrastructure.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace backend.Infrastructure.Repository
{
	public class AuthenticationService(
		IOptions<JwtSettings> jwtSettings,
		IMapper mapper,
		IOptions<ApiSettings> apiSettings)
		: IAuthenticationService
	{
		private readonly JwtSettings _jwtSettings = jwtSettings.Value;
		private readonly ApiSettings _apiSettings = apiSettings.Value;

		public AuthenticationResponseDTO Login(
			LoginRequestDTO user,
			User userEntity,
			bool LoginAfterOtpVerification = false
		)
		{
			if (LoginAfterOtpVerification)
			{
				if (userEntity.Password != HashPassword(user.Password ?? ""))
				{
					throw new BadRequestException("Invalid password");
				}
			}

			var token = GenerateTokenAsync(userEntity);
			var response = mapper.Map<AuthenticationResponseDTO>(userEntity);
			response.Token = token;
			return response;
		}

		public string GenerateTokenAsync(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty),
				new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Role, user.Role.Name ?? string.Empty)
			};

			var key = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(_jwtSettings.Key ?? string.Empty)
			);
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
				signingCredentials: credentials
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public bool ValidateTokenAsync(string token)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = new SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(_jwtSettings.Key ?? string.Empty)
			);

			try
			{
				tokenHandler.ValidateToken(
					token,
					new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = key,
						ValidateIssuer = true,
						ValidIssuer = _jwtSettings.Issuer,
						ValidateAudience = true,
						ValidAudience = _jwtSettings.Audience,
						ValidateLifetime = true,
						ClockSkew = TimeSpan.Zero
					},
					out SecurityToken validatedToken
				);
				return true;
			}
			catch
			{
				return false;
			}
		}

		private string HashPassword(string password)
		{
			Console.WriteLine("Salt: " + _apiSettings.SecretKey);
			Console.WriteLine("Password: " + password);
			Console.WriteLine("JwtKey: " + _jwtSettings.Key);
	
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
