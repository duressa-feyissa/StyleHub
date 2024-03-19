using Application.DTO.ProductDTO.DTO;
using MediatR;

namespace Application.Features.Product.Requests.Queries
{
    public class GetAllProduct : IRequest<List<ProductResponseDTO>>
    {
    }
}
