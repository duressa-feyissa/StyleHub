using Application.DTO.User.AuthenticationDTO.DTO;
using MediatR;
using Mysqlx.Session;

namespace Application.Features.User_Features.Authentication.Requests.Commands
{
	public class ResetPasswordRequest : IRequest< AuthenticationResponseDTO>
	{
		public required string Email { get; set; }
		public required string Code { get; set; }
		public required string Password { get; set; }
	}
}
