using backend.Application.DTO.User.UserDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.User_Features.User.Requests.Command
{
    public class DeleteUserProfileRequest : IRequest<BaseResponse<UserResponseDTO>>
    {
        public required string Id { get; set; }
    }
}
