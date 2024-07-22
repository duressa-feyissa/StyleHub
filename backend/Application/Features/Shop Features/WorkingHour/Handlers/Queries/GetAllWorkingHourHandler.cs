using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Shop.WorkingHourDTO.DTO;
using backend.Application.Features.Shop_Features.WorkingHour.Requests.Queries;
using MediatR;

namespace backend.Application.Features.Shop_Features.WorkingHour.Handlers.Queries;

public class GetAllWorkingHourHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllWorkingHourOfShopRequest, List<WorkingHourResponseDTO>>
{
    public async Task<List<WorkingHourResponseDTO>> Handle(GetAllWorkingHourOfShopRequest request, CancellationToken cancellationToken)
    {
        var workingHours = await unitOfWork.WorkingHourRepository.GetWorkingHoursByShopIdAsync(request.ShopId);
        return mapper.Map<List<WorkingHourResponseDTO>>(workingHours);
    }
}