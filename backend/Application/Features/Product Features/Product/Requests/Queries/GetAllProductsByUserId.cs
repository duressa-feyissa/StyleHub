using Application.DTO.Product.ProductDTO.DTO;
using MediatR;

namespace Application.Features.Product_Features.Product.Requests.Queries
{
    public class GetAllProductUserId : IRequest<List<ProductResponseDTO>>
    {
        public required string UserId { get; set; }
        public int Skip { get; set; }
        public int Limit { get; set; }
    }
}
