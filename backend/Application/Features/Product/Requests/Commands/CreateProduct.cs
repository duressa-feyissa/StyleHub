using Application.DTO.ProductDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Product.Requests.Commands
{
    public class CreateProductRequest : IRequest<BaseResponse<ProductResponseDTO>>
    {
        public required BaseProductDTO Product { get; set; }
    }
}