using backend.Application.DTO.Product.ProductDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Requests.Commands
{
    public class DeleteProductRequest : IRequest<BaseResponse<ProductResponseDTO>>
    {
        public string Id { get; set; } = string.Empty;
    }
}