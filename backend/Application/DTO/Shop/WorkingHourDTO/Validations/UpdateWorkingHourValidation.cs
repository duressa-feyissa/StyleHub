using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Shop.ShopDTO.DTO;
using backend.Application.DTO.Shop.WorkingHourDTO.DTO;

namespace backend.Application.DTO.Shop.WorkingHourDTO.Validations;
using FluentValidation;

public class UpdateWorkingHourValidation: AbstractValidator<UpdateWorkingHourDTO>
{
    public UpdateWorkingHourValidation(IUnitOfWork unitOfWork)
    {
        List<string> days = new List<string> { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" };
        List<string> times = new List<string> { "morning", "afternoon", "evening", "all_day" };

        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Id is required")
            .NotEmpty()
            .WithMessage("Id is required")
            .MustAsync(async (id, cancellation) => await unitOfWork.WorkingHourRepository.IsExistingWorkingHourAsync(id));

        RuleFor(x => x.Day)
            
            .NotNull()
            .WithMessage("Day is required")
            .NotEmpty()
            .WithMessage("Day is required")
            .Must(x => days.Contains(x))
            .WithMessage("Day is invalid");

        RuleFor(x => x.Time)
            .NotNull()
            .WithMessage("Time is required")
            .NotEmpty()
            .WithMessage("Time is required")
            .Must(x => times.Contains(x))
            .WithMessage("Time is invalid");
    }
    
}


