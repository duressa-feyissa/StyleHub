using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Shop.ShopDTO.DTO;
using backend.Application.Features.Shop_Features.Shop.Requests.Queries;
using MediatR;
using Newtonsoft.Json;

namespace backend.Application.Features.Shop_Features.Shop.Handlers.Queries;

public class GetAllShopHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler< GetAllShopRequest, List<ShopResponseDTO>>
{
    public async Task<List<ShopResponseDTO>> Handle(GetAllShopRequest request, CancellationToken cancellationToken)
    {
        var shops = await unitOfWork.ShopRepository.GetShopListAsync(
            search: request.Search,
            categories: request.Category,
            workingHours: request.WorkingHours,
            radiusInKilometers: request.RadiusInKilometers,
            latitude: request.Latitude,
            longitude: request.Longitude,
            rating: request.Rating,
            verified: request.Verified,
            active: request.Active,
            ownerId: request.OwnerId,
            sortBy: request.SortBy,
            sortOrder: request.SortOrder,
            skip: request.Skip,
            limit: request.Limit
        );
        
        var shopResponse = mapper.Map<List<ShopResponseDTO>>(shops);
        for (int i = 0; i < shopResponse.Count; i++)
        {
            shopResponse[i].Categories = JsonConvert.DeserializeObject<List<string>>(shops[i].Category) ?? new List<string>();
            shopResponse[i].SocialMediaLinks = JsonConvert.DeserializeObject<Dictionary<string, string>>(shops[i].SocialMedias) ?? new Dictionary<string, string>();
            shopResponse[i].Rating = await unitOfWork.ShopRepository.GetShopAverageRatingAsync(shopResponse[i].Id);
        }
        return shopResponse;
    }
}
