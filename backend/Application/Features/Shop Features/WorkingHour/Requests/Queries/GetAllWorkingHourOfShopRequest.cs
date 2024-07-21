using backend.Application.DTO.Shop.WorkingHourDTO.DTO;
using MediatR;

namespace backend.Application.Features.Shop_Features.WorkingHour.Requests.Queries;

public class GetAllWorkingHourOfShopRequest: IRequest<List<WorkingHourResponseDTO>>
{
    public required string ShopId { get; set; }
}