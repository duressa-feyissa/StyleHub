using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Shop.ShopDTO.DTO;
using backend.Application.DTO.Shop.ShopDTO.Validations;
using backend.Application.Exceptions;
using backend.Application.Features.Shop_Features.Shop.Requests.Commands;
using backend.Application.Response;
using MediatR;
using Newtonsoft.Json;
using IImageRepository = backend.Application.Contracts.Infrastructure.Repositories.IImageRepository;

namespace backend.Application.Features.Shop_Features.Shop.Handlers.Commands;

public class CreateShopHandler(IUnitOfWork unitOfWork, IMapper mapper,   IImageRepository imageRepository)
    : IRequestHandler<CreateShopRequest, BaseResponse<ShopResponseDTO>>
{
    public async Task<BaseResponse<ShopResponseDTO>> Handle(CreateShopRequest request, CancellationToken cancellationToken)
    {
        var shopExists = await unitOfWork.ShopRepository.IsUserShopOwnerAsync(request.UserId);
        if (shopExists)
            throw new BadRequestException("User already has a shop");
        
        var validator = new CreateShopValidation(
            unitOfWork.ShopRepository);
        var validationResult = await validator.ValidateAsync(request.Shop);
        if (!validationResult.IsValid)
            throw new BadRequestException(
                validationResult.Errors.FirstOrDefault()?.ErrorMessage!
            );

        var shop = mapper.Map<Domain.Entities.Shop.Shop>(request.Shop);
        shop.Logo =  await imageRepository.Upload(shop.Logo, shop.Id + "-logo");
        if (request.Shop.Banner != null)
            shop.Banner =await imageRepository.Upload(request.Shop.Banner, shop.Id + "-banner");
        shop.UserId = request.UserId;
        shop.Category = JsonConvert.SerializeObject(request.Shop.Categories);
        shop.SocialMedias = JsonConvert.SerializeObject(request.Shop.SocialMediaLinks);
      
        await unitOfWork.ShopRepository.Add(shop);
        var shopResponse = mapper.Map<ShopResponseDTO>(shop);
        shopResponse.Categories =  JsonConvert.DeserializeObject<List<string>>(shop.Category) ?? new List<string>();
        shopResponse.SocialMediaLinks = JsonConvert.DeserializeObject<Dictionary<string, string>>(shop.SocialMedias) ?? new Dictionary<string, string>();
        
        return new BaseResponse<ShopResponseDTO>
        {
            Success = true,
            Data = shopResponse,
            Message = "Shop Created Successfully"
        };
    }
}