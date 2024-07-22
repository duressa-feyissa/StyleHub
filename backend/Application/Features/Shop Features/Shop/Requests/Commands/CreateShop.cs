using backend.Application.DTO.Shop.ShopDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Shop_Features.Shop.Requests.Commands;

public class CreateShopRequest : IRequest<BaseResponse<ShopResponseDTO>>
{
    public required CreateShopDTO Shop { get; set; }
    public required string UserId { get; set; }
}
