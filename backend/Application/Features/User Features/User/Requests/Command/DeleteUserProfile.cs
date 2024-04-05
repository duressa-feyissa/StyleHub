using Application.DTO.User.UserDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.User_Features.User.Requests.Command
{
    public class DeleteUserProfileRequest : IRequest<BaseResponse<UserResponseDTO>>
    {
        public required string Id { get; set; }
    }
}
