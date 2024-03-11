using MediatR;
using SocialSync.Application.DTO.UserDTO.DTO;

namespace StyleHub.Application.Features.Requests.Queries
{
    public class GetAllUsersQuery : IRequest<List<UserResponseDTO>>
    {
    }
}