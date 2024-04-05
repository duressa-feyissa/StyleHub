using Application.DTO.User.UserDTO.DTO;
using MediatR;

namespace Application.Features.User_Features.User.Requests.Queries
{
    public class GetUserByIdRequest : IRequest<UserResponseDTO>
    {
        public required string Id { get; set; }
    }
}
