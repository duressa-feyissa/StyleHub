using Application.DTO.Product.MaterialDTO.DTO;
using MediatR;

namespace Application.Features.Product_Features.Material.Requests.Queries
{
    public class GetMaterialById : IRequest<MaterialResponseDTO>
    {
        public string Id { get; set; } = string.Empty;
    }
}