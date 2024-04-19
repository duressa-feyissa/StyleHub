using backend.Application.DTO.Product.ProductDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Requests.Commands
{
    public class UpdateProductRequest : IRequest<BaseResponse<ProductResponseDTO>>
    {
        public required string Id { get; set; }
        public required UpdateProductDTO Product { get; set; }
    }
}
