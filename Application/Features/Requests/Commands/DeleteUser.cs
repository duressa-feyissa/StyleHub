using MediatR;
using SocialSync.Application.DTO.UserDTO.DTO;
using StyleHub.Application.DTO.UserDTO.DTO;
using SytleHub.Application.Response;

namespace StyleHub.Application.Features.Requests.Commands
{
    public class DeleteUserCommand : IRequest<BaseResponse<UserResponseDTO>>
    {
        public string userId { get; set; } = string.Empty;
    }
}
