using Application.DTO.ProductDTO.DTO;
using Application.Response;
using MediatR;

namespace Application.Features.Product.Requests.Commands
{
    public class DeleteProductRequest : IRequest<BaseResponse<ProductResponseDTO>>
    {
        public string Id { get; set; } = string.Empty;
    }
}