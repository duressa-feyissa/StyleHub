using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Common.Image.DTO;
using backend.Application.Features.Shop_Features.Shop.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Shop_Features.Shop.Handlers.Queries;

public class GetShopProductVideosHandler(IUnitOfWork unitOfWork, IMapper mapper): IRequestHandler<GetShopProductVideosRequest, List<string>>
{
    public async Task<List<string>> Handle(GetShopProductVideosRequest request, CancellationToken cancellationToken)
    {
    var videos = await unitOfWork.ShopRepository.GetShopVideosAsync(
            shopId: request.ShopId,
            skip: request.Skip,
            limit: request.Limit
        );
        return videos.ToList();
    }
}