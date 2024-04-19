using backend.Application.DTO.Common.Role.DTO;
using MediatR;

namespace backend.Application.Features.Common_Features.Role.Requests.Queries
{
    public class GetRoleById : IRequest<RoleResponseDTO>
    {
        public string Id { get; set; } = string.Empty;
    }
}
