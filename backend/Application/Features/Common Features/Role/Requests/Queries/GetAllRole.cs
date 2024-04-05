using Application.DTO.Common.Role.DTO;
using MediatR;

namespace Application.Features.Common_Features.Role.Requests.Queries
{
    public class GetAllRole : IRequest<List<RoleResponseDTO>>
    {
    }
}
