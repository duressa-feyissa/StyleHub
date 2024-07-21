using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Common.Image.DTO;
using backend.Application.Features.Shop_Features.Shop.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Shop_Features.Shop.Handlers.Queries;

public class GetShopProductImagesHandler(IUnitOfWork unitOfWork, IMapper mapper): IRequestHandler<GetShopProductImagesRequest, List<ImageResponseDTO>>
{
    public async Task<List<ImageResponseDTO>> Handle(GetShopProductImagesRequest request, CancellationToken cancellationToken)
    {
        var images = await unitOfWork.ShopRepository.GetShopImagesAsync(
            shopId: request.ShopId,
            skip: request.Skip,
            limit: request.Limit
        );
        return mapper.Map<List<ImageResponseDTO>>(images);
    }
}