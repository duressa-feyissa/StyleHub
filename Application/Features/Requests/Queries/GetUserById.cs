using MediatR;
using SocialSync.Application.DTO.UserDTO.DTO;

namespace StyleHub.Application.Features.Requests.Queries
{
    public class GetUserByIdQuery : IRequest<UserResponseDTO>
    {
        public string Id { get; set; } = string.Empty;
    }
}