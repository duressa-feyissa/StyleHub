using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Shop.ShopDTO.DTO;
using backend.Application.Features.Shop_Features.Shop.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Shop_Features.Shop.Handlers.Queries;

public class GetShopFollowedByUserHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetShopFollowedByUser, List<ShopResponseDTO>>
{
    public async Task<List<ShopResponseDTO>> Handle(GetShopFollowedByUser request, CancellationToken cancellationToken)
    {
        var shops = await unitOfWork.ShopRepository.GetShopFollowedUsersAsync(
            userId: request.UserId,
            skip: request.Skip,
            limit: request.Limit
        );
        return mapper.Map<List<ShopResponseDTO>>(shops);
    }
}