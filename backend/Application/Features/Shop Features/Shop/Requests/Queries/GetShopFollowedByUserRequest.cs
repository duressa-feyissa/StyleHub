using backend.Application.DTO.Shop.ShopDTO.DTO;
using MediatR;

namespace backend.Application.Features.Shop_Features.Shop.Requests.Queries;

public class GetShopFollowedByUserRequest: IRequest<List<ShopResponseDTO>>
{
    public required string UserId { get; set; }
    public int Skip { get; set; } = 0;
    public int Limit { get; set; } = 10;
}