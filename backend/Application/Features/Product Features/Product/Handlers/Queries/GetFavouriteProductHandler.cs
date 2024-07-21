using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Product.ProductDTO.DTO;
using backend.Application.Features.Product_Features.Product.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Handlers.Queries;

public class GetFavouriteProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetFavouriteProduct, List<ProductResponseDTO>>
{
    public async Task<List<ProductResponseDTO>> Handle(GetFavouriteProduct request, CancellationToken cancellationToken)
    {
        var products = await unitOfWork.FavouriteProductRepository.GetAll(
            userId: request.UserId,
            skip: request.Skip,
            limit: request.Limit
        );
        var productResponse = mapper.Map<List<ProductResponseDTO>>(products);

        foreach (var product in productResponse)
        {
            product.IsFavorite = true;
        }
        
        return productResponse;
    }
}