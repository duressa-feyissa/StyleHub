using backend.Application.DTO.Shop.WorkingHourDTO.DTO;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Shop_Features.WorkingHour.Requests.Commands;

public class CreateWorkingHourRequest: IRequest<BaseResponse<WorkingHourResponseDTO>>
{
    public required CreateWorkingHourDTO WorkingHour { get; set; }
    public required string UserId  { get; set; }
}