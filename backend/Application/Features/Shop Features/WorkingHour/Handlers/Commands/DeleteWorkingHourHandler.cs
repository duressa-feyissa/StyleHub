using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Shop.WorkingHourDTO.DTO;
using backend.Application.Exceptions;
using backend.Application.Features.Shop_Features.WorkingHour.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Shop_Features.WorkingHour.Handlers.Commands;

public class DeleteWorkingHourHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<DeleteWorkingHourRequest, BaseResponse<WorkingHourResponseDTO>>
{
    public async Task<BaseResponse<WorkingHourResponseDTO>> Handle(DeleteWorkingHourRequest request, CancellationToken cancellationToken)
    {
        var workingHour = await unitOfWork.WorkingHourRepository.GetWorkingHourByIdAsync(request.Id);
        if (workingHour == null)
        {
            throw new NotFoundException("No working hour found with this id");
        }
        var shop = await unitOfWork.ShopRepository.GetShopByIdAsync(workingHour.ShopId);
        if (shop.UserId != request.UserId)
        {
            throw new BadRequestException("You are not the owner of this shop");
        }
        await unitOfWork.WorkingHourRepository.Delete(workingHour);
        return new BaseResponse<WorkingHourResponseDTO>
        {
            Data = mapper.Map<WorkingHourResponseDTO>(workingHour),
            Message = "Working hour deleted successfully",
            Success = true
        };
    }
}