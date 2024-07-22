using AutoMapper;
using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Shop.WorkingHourDTO.DTO;
using backend.Application.DTO.Shop.WorkingHourDTO.Validations;
using backend.Application.Exceptions;
using backend.Application.Features.Shop_Features.WorkingHour.Requests.Commands;
using backend.Application.Response;
using MediatR;

namespace backend.Application.Features.Shop_Features.WorkingHour.Handlers.Commands;

public class CreateWorkingHourHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<CreateWorkingHourRequest, BaseResponse<WorkingHourResponseDTO>>
{
    public async Task<BaseResponse<WorkingHourResponseDTO>> Handle(CreateWorkingHourRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateWorkingReviewValidation(
            unitOfWork);
        var validationResult = await validator.ValidateAsync(request.WorkingHour);
        if (!validationResult.IsValid)
            throw new BadRequestException(
                validationResult.Errors.FirstOrDefault()?.ErrorMessage!
            );
        var shop = await unitOfWork.ShopRepository.GetShopByIdAsync(request.WorkingHour.ShopId);
        if (shop == null)
        {
            throw new NotFoundException("No shop found with this id");
        }
        if (shop.UserId != request.UserId)
        {
            throw new BadRequestException("You are not the owner of this shop");
        }
        var workingHour = mapper.Map<Domain.Entities.Shop.WorkingHour>(request.WorkingHour);
        workingHour = await unitOfWork.WorkingHourRepository.Add(workingHour);
        var response = mapper.Map<WorkingHourResponseDTO>(workingHour);
        return new BaseResponse<WorkingHourResponseDTO>
        {
            Data = response,
            Message = "Working hour created successfully",
            Success = true
        };
    }
}
