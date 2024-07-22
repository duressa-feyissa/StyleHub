using backend.Application.DTO.Product.ProductDTO.DTO;
using MediatR;

namespace backend.Application.Features.Shop_Features.Shop.Requests.Queries;

public class GetShopProductFollowedByUser: IRequest<List<ProductResponseDTO>>
{
    public required string UserId { get; set; }
    public int Skip { get; set; } = 0;
    public int Limit { get; set; } = 10;
}
