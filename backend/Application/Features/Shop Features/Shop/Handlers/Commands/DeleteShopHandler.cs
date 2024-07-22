using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Shop.ShopDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Shop_Features.Shop.Requests.Commands;
using backend.Application.Response;
using MediatR;
using Newtonsoft.Json;

namespace backend.Application.Features.Shop_Features.Shop.Handlers.Commands;

public class DeleteShopHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<DeleteShopRequest, BaseResponse<ShopResponseDTO>>
{
    public async Task<BaseResponse<ShopResponseDTO>> Handle(DeleteShopRequest request, CancellationToken cancellationToken)
    {
        var shop = await unitOfWork.ShopRepository.GetShopByIdAsync(request.Id);
        if (shop == null)
            throw new NotFoundException("Shop Not Found");
        
        if (shop.UserId != request.UserId)
            throw new BadRequestException("You are not the owner of this shop");
        
        await unitOfWork.ShopRepository.Delete(shop);
        
        var shopResponse = mapper.Map<ShopResponseDTO>(shop);
        shopResponse.Categories =  JsonConvert.DeserializeObject<List<string>>(shop.Category) ?? new List<string>();
        shopResponse.SocialMediaLinks = JsonConvert.DeserializeObject<Dictionary<string, string>>(shop.SocialMedias) ?? new Dictionary<string, string>();
        
        return new BaseResponse<ShopResponseDTO>
        {
            Success = true,
            Data = shopResponse,
            Message = "Shop Deleted Successfully"
        };
    }
}