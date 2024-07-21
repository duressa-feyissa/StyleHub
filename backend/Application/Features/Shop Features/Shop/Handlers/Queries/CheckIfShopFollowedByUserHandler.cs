using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Shop.ShopDTO.DTO;
using backend.Application.Features.Shop_Features.Shop.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Shop_Features.Shop.Handlers.Queries;

public class CheckIfShopFollowedByUserHandler(IUnitOfWork unitOfWork, IMapper mapper): IRequestHandler<CheckIfShopFollowedByUser, ShopFollowStatusDTO>
{
    public async Task<ShopFollowStatusDTO> Handle(CheckIfShopFollowedByUser request, CancellationToken cancellationToken)
    {
        var data = await unitOfWork.ShopRepository.IsShopFollowedByUserAsync(request.ShopId, request.UserId);

        return new ShopFollowStatusDTO
        {
            IsFollowed = data
        };
    }
}