using Application.DTO.User.UserDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.User_Features.User.Requests.Commands
{
	public class UpdateUserProfileRequest : IRequest<BaseResponse<UserResponseDTO>>
	{
		public required string Id { get; set; }
		public required UpdateUserProfileDTO updateUserProfileDTO { get; set; }
	}
}
