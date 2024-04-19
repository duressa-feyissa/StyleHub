using backend.Application.DTO.Product.MaterialDTO.DTO;
using MediatR;

namespace backend.Application.Features.Product_Features.Material.Requests.Queries
{
    public class GetAllMaterial : IRequest<List<MaterialResponseDTO>>
    {
    }
}
