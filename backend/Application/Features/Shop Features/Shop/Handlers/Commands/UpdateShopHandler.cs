using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Shop.ShopDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Shop_Features.Shop.Requests.Commands;
using backend.Application.Response;
using MediatR;
using Newtonsoft.Json;

namespace backend.Application.Features.Shop_Features.Shop.Handlers.Commands;

public class UpdateShopHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateShopRequest, BaseResponse<ShopResponseDTO>>
{
    public async Task<BaseResponse<ShopResponseDTO>> Handle(UpdateShopRequest request, CancellationToken cancellationToken)
    {
        var shop = await unitOfWork.ShopRepository.GetShopByIdAsync(request.Shop.Id);
        if (shop == null)
            throw new NotFoundException("Shop Not Found");

        if (shop.UserId != request.UserId)
            throw new BadRequestException("You are not the owner of this shop");
        
        if (!string.IsNullOrEmpty(request.Shop.Name))
        {
            var shopExists = await unitOfWork.ShopRepository.IsShopNameUniqueAsync(request.Shop.Name!);
            if (!shopExists)
                throw new BadRequestException("Shop Name already exists");
            shop.Name = request.Shop.Name;
        }
        
        if (!string.IsNullOrEmpty(request.Shop.Description))
        {
            shop.Description = request.Shop.Description;
        }
        
        if (request.Shop.Category != null && request.Shop.Category.Count > 0)
        {
            shop.Category = JsonConvert.SerializeObject(request.Shop.Category);
        }
        
        if (!string.IsNullOrEmpty(request.Shop.Street))
        {
            shop.Street = request.Shop.Street;
        }
        
        if (!string.IsNullOrEmpty(request.Shop.SubLocality))
        {
            shop.SubLocality = request.Shop.SubLocality;
        }
        
        if (!string.IsNullOrEmpty(request.Shop.SubAdministrativeArea))
        {
            shop.SubAdministrativeArea = request.Shop.SubAdministrativeArea;
        }
        
        if (!string.IsNullOrEmpty(request.Shop.PostalCode))
        {
            shop.PostalCode = request.Shop.PostalCode;
        }
        
        if (request.Shop.Latitude != null)
        {
            shop.Latitude = request.Shop.Latitude ?? 0;
        }
        
        if (request.Shop.Longitude != null)
        {
            shop.Longitude = request.Shop.Longitude ?? 0;
        }
        
        if (!string.IsNullOrEmpty(request.Shop.PhoneNumber))
        {
            shop.PhoneNumber = request.Shop.PhoneNumber;
        }
        
        if (!string.IsNullOrEmpty(request.Shop.Banner))
        {
            shop.Banner = request.Shop.Banner;
        }
        
        if (!string.IsNullOrEmpty(request.Shop.Logo))
        {
            shop.Logo = request.Shop.Logo;
        }
        
        if (request.Shop.SocialMediaLinks is { Count: > 0 })
        {
            shop.SocialMedias = JsonConvert.SerializeObject(request.Shop.SocialMediaLinks);
        }
        
        if (!string.IsNullOrEmpty(request.Shop.Website))
        {
            shop.Website = request.Shop.Website;
        }
        
        var  updatedShop = await  unitOfWork.ShopRepository.Update(shop);
        var shopResponse = mapper.Map<ShopResponseDTO>(updatedShop);
        shopResponse.Categories = JsonConvert.DeserializeObject<List<string>>(shop.Category) ?? new List<string>();
        shopResponse.SocialMediaLinks = JsonConvert.DeserializeObject<Dictionary<string, string>>(shop.SocialMedias) ?? new Dictionary<string, string>();
        return new BaseResponse<ShopResponseDTO>
        {
            Success = true,
            Data = shopResponse,
            Message = "Shop Updated Successfully"
        };
    }
}
