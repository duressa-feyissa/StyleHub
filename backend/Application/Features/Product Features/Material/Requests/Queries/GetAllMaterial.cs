using Application.DTO.Product.MaterialDTO.DTO;
using MediatR;

namespace Application.Features.Product_Features.Material.Requests.Queries
{
    public class GetAllMaterial : IRequest<List<MaterialResponseDTO>>
    {
    }
}
