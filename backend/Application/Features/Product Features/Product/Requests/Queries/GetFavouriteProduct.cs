using backend.Application.DTO.Product.ProductDTO.DTO;
using MediatR;

namespace backend.Application.Features.Product_Features.Product.Requests.Queries;

public class GetFavouriteProduct(
    string userId,
    int skip = 0,
    int limit = 10) : IRequest<List<ProductResponseDTO>>
{
    public string UserId { get; set; } = userId;
    public int Skip { get; set; } = skip;
    public int Limit { get; set; } = limit;
}