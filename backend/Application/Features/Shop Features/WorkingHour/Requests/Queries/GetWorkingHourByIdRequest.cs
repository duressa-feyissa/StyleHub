using backend.Application.DTO.Shop.WorkingHourDTO.DTO;
using MediatR;

namespace backend.Application.Features.Shop_Features.WorkingHour.Requests.Queries;

public class GetWorkingHourByIdRequest: IRequest<WorkingHourResponseDTO>
{
    public required string Id { get; set; }
}