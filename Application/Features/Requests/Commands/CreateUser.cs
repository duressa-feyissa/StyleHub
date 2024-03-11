
using MediatR;
using SocialSync.Application.DTO.UserDTO.DTO;
using StyleHub.Application.DTO.UserDTO.DTO;
using SytleHub.Application.Response;

namespace StyleHub.Application.Features.Requests.Commands
{
    public class CreateUserCommand : IRequest<BaseResponse<UserResponseDTO>>
    {
        public UserCreateDTO? User { get; set; }
    }
}