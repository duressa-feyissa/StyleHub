using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Shop.WorkingHourDTO.DTO;
using backend.Application.DTO.Shop.WorkingHourDTO.Validations;
using backend.Application.Exceptions;
using backend.Application.Features.Shop_Features.WorkingHour.Requests.Commands;
using backend.Application.Response;
using FluentValidation;
using MediatR;

namespace backend.Application.Features.Shop_Features.WorkingHour.Handlers.Commands;

public class UpdateWorkingHourHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<UpdateWorkingHourRequest, BaseResponse<WorkingHourResponseDTO>>
{
    public async Task<BaseResponse<WorkingHourResponseDTO>> Handle(UpdateWorkingHourRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateWorkingHourValidation(unitOfWork);
        var validationResult = await validator.ValidateAsync(request.WorkingHour);
        if (!validationResult.IsValid)
            throw new BadRequestException(
                validationResult.Errors.FirstOrDefault()?.ErrorMessage!
            );
        var workingHour = await unitOfWork.WorkingHourRepository.GetWorkingHourByIdAsync(request.WorkingHour.Id);
        var shop = await unitOfWork.ShopRepository.GetShopByIdAsync(workingHour.ShopId);
        if (shop.UserId != request.UserId)
        {
            throw new BadRequestException("You are not the owner of this shop");
        }
        workingHour.Day = request.WorkingHour.Day;
        workingHour.Time = request.WorkingHour.Time;
        await unitOfWork.WorkingHourRepository.Update(workingHour);
        
        var response = new BaseResponse<WorkingHourResponseDTO>
        {
            Success = true,
            Message = "WorkingHour updated successfully",
            Data = mapper.Map<WorkingHourResponseDTO>(workingHour)
        };
        return response;
    }
}