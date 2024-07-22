using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Shop.ShopDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Shop_Features.Shop.Requests.Queries;
using MediatR;
using Newtonsoft.Json;

namespace backend.Application.Features.Shop_Features.Shop.Handlers.Queries;

public class GetShopIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetShopByIdRequest, ShopResponseDTO>
{
    public async Task<ShopResponseDTO> Handle(GetShopByIdRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Id))
        {
            throw new BadRequestException("Id is required");
        }

        var shop = await unitOfWork.ShopRepository.GetShopByIdAsync(request.Id);
        var shopResponse = mapper.Map<ShopResponseDTO>(shop);
        shopResponse.Categories = JsonConvert.DeserializeObject<List<string>>(shop.Category) ?? new List<string>();
        shopResponse.SocialMediaLinks = JsonConvert.DeserializeObject<Dictionary<string, string>>(shop.SocialMedias) ?? new Dictionary<string, string>();
        return shopResponse;
    }
}