using backend.Application.DTO.Shop.ShopDTO.DTO;
using MediatR;

namespace backend.Application.Features.Shop_Features.Shop.Requests.Queries;

public class GetShopByIdRequest: IRequest<ShopResponseDTO>
{
    public string Id { get; set; }
}