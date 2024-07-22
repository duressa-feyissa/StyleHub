using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.Features.Shop_Features.Shop.Requests.Queries;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Shop_Features.Shop.Handlers.Queries;

public class FollowShopHandler(IUnitOfWork unitOfWork, IMapper mapper): IRequestHandler<FollowShopRequest, BaseResponse<string>>
{
    public async Task<BaseResponse<string>> Handle(FollowShopRequest request, CancellationToken cancellationToken)
    {
        var data = await unitOfWork.ShopRepository.FollowShopAsync(request.ShopId, request.UserId);

        return new BaseResponse<string>
        {
            Success = true,
            Message = "Shop followed successfully",
            Data = data
        };
    }
}