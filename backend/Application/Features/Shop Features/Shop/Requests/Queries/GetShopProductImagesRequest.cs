using backend.Application.DTO.Common.Image.DTO;
using MediatR;

namespace backend.Application.Features.Shop_Features.Shop.Requests.Queries;

public class GetShopProductImagesRequest(string shopId, int skip = 0, int limit = 10): IRequest<List<ImageResponseDTO>>
{
    public string ShopId { get; set; } = shopId;
    public int Skip { get; set; } = skip;
    public int Limit { get; set; } = limit;
}