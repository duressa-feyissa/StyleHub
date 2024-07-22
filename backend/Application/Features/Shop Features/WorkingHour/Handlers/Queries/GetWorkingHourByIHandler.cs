using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Shop.WorkingHourDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Shop_Features.WorkingHour.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Shop_Features.WorkingHour.Handlers.Queries;

public class GetWorkingHourByIHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetWorkingHourByIdRequest, WorkingHourResponseDTO>
{
    public async Task<WorkingHourResponseDTO> Handle(GetWorkingHourByIdRequest request, CancellationToken cancellationToken)
    {
        var workingHour = await unitOfWork.WorkingHourRepository.GetWorkingHourByIdAsync(request.Id);
        if (workingHour == null)
        {
            throw new NotFoundException("No working hour found with this id");
        }
        return mapper.Map<WorkingHourResponseDTO>(workingHour);
    }
}