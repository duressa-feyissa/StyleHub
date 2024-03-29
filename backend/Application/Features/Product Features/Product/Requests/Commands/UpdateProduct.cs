using Application.DTO.Product.ProductDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Product_Features.Product.Requests.Commands
{
    public class UpdateProductRequest : IRequest<BaseResponse<ProductResponseDTO>>
    {
        public UpdateProductDTO? Product { get; set; }
    }
}