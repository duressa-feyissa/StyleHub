using backend.Application.DTO.Common.Role.DTO;
using MediatR;

namespace backend.Application.Features.Common_Features.Role.Requests.Queries
{
    public class GetAllRole : IRequest<List<RoleResponseDTO>>
    {
    }
}
