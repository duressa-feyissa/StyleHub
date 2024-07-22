using backend.Application.DTO.Shop.ShopDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Shop_Features.Shop.Requests.Commands;

public class UpdateShopRequest: IRequest<BaseResponse<ShopResponseDTO>>
{
    public required UpdateShopDTO Shop { get; set; }
    public required string UserId { get; set; }
}
