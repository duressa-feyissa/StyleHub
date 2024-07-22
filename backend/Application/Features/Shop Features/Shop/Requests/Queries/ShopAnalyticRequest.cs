using backend.Application.DTO.Shop.ShopDTO.DTO;
using MediatR;

namespace backend.Application.Features.Shop_Features.Shop.Requests.Queries;

public class ShopAnalyticRequest: IRequest<ShopAnalyticsDTO>
{
    public required string ShopId { get; set; }
}