using backend.Application.DTO.Shop.ShopDTO.DTO;
using MediatR;

namespace backend.Application.Features.Shop_Features.Shop.Requests.Queries;

public class CheckIfShopFollowedByUser: IRequest<ShopFollowStatusDTO>
{
    public required string UserId { get; set; }
    public required string ShopId { get; set; }
}