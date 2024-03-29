using Application.DTO.Product.ProductDTO.DTO;
using MediatR;

namespace Application.Features.Product_Features.Product.Requests.Queries
{
    public class GetProductById : IRequest<ProductResponseDTO>
    {
        public string Id { get; set; } = string.Empty;
    }
}