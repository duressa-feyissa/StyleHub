using backend.Application.DTO.User.AuthenticationDTO.DTO;
using MediatR;

namespace backend.Application.Features.User_Features.Authentication.Requests.Commands
{
	public class ResetPasswordRequest : IRequest< AuthenticationResponseDTO>
	{
		public required string Email { get; set; }
		public required string Code { get; set; }
		public required string Password { get; set; }
	}
}
