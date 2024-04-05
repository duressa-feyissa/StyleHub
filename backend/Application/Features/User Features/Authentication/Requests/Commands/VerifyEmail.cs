using Application.DTO.User.AuthenticationDTO.DTO;
using MediatR;

namespace Application.Features.User_Features.Authentication.Requests.Commands
{
	public class VerifyEmailRequest : IRequest<VerifyEmailResponseDTO>
	{
		public required string Email { get; set; }
		public required string Code { get; set; }
	}
}
