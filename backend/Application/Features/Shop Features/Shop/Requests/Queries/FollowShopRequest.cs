using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Shop_Features.Shop.Requests.Queries;

public class FollowShopRequest: IRequest<BaseResponse<string>>
{
    public string UserId { get; set; }
    public string ShopId { get; set; }
}