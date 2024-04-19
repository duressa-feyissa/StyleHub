using backend.Application.DTO.Product.ProductDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Requests.Commands
{
    public class CreateProductRequest : IRequest<BaseResponse<ProductResponseDTO>>
    {
        public required CreateProductDTO Product { get; set; }
        public required string UserId { get; set; }
    }
}
