using Application.DTO.MaterialDTO.DTO;
using MediatR;

namespace Application.Features.Material.Requests.Queries
{
    public class GetAllMaterial : IRequest<List<MaterialResponseDTO>>
    {
    }
}
