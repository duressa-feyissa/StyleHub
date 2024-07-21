using backend.Application.DTO.Shop.ShopDTO.DTO;
using backend.Application.DTO.Shop.WorkingHourDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Shop_Features.WorkingHour.Requests.Commands;

public class UpdateWorkingHourRequest: IRequest<BaseResponse<WorkingHourResponseDTO>>
{
    public required UpdateWorkingHourDTO WorkingHour { get; set; }
    public required string UserId  { get; set; }
}