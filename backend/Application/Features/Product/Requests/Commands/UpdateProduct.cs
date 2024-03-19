using Application.DTO.ProductDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Product.Requests.Commands
{
    public class UpdateProductRequest : IRequest<BaseResponse<ProductResponseDTO>>
    {
        public BaseProductDTO? Product { get; set; }
    }
}