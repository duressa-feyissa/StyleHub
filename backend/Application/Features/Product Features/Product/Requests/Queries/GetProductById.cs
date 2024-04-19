using backend.Application.DTO.Product.ProductDTO.DTO;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Requests.Queries
{
    public class GetProductById : IRequest<ProductResponseDTO>
    {
        public string Id { get; set; } = string.Empty;
    }
}