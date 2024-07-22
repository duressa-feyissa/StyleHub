using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.ProductDTO.DTO;
using backend.Application.Features.Shop_Features.Shop.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Shop_Features.Shop.Handlers.Queries;

public class GetShopProductFollowedByUserHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetShopProductFollowedByUser, List<ProductResponseDTO>>
{
    public async Task<List<ProductResponseDTO>> Handle(GetShopProductFollowedByUser request, CancellationToken cancellationToken)
    {
        var products = await unitOfWork.ShopRepository.GetShopProductsAsync(
            userId: request.UserId,
            skip: request.Skip,
            limit: request.Limit
        );
        
        var productResponse = mapper.Map<List<ProductResponseDTO>>(products);
      
        foreach (var product in productResponse)
        {
            product.IsFavorite = await unitOfWork.FavouriteProductRepository.IsFavourite(
                userId: request.UserId,
                productId: product.Id
            );
        }
        
        return productResponse;
    }
}