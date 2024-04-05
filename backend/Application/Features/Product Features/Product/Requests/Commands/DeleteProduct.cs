using Application.DTO.Product.ProductDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Product_Features.Product.Requests.Commands
{
    public class DeleteProductRequest : IRequest<BaseResponse<ProductResponseDTO>>
    {
        public string Id { get; set; } = string.Empty;
    }
}