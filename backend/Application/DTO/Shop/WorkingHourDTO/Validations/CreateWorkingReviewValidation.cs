using backend.Application.Contracts.Persistence;
using backend.Application.DTO.Shop.WorkingHourDTO.DTO;
using FluentValidation;

namespace backend.Application.DTO.Shop.WorkingHourDTO.Validations
{
    public class CreateWorkingReviewValidation : AbstractValidator<CreateWorkingHourDTO>
    {
        public CreateWorkingReviewValidation(IUnitOfWork unitOfWork)
        {
            List<string> days = new List<string> { "monday", "tuesday", "wednesday", "thursday", "friday", "saturday", "sunday" };
            List<string> times = new List<string> { "morning", "afternoon", "evening", "all_day" };

            RuleFor(x => x.ShopId)
                .NotNull()
                .WithMessage("ShopId is required")
                .NotEmpty()
                .WithMessage("ShopId is required")
                .MustAsync(async (shopId, cancellation) => await unitOfWork.ShopRepository.ExistsAsync(shopId))
                .WithMessage("ShopId does not exist");

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

            RuleFor(x => new { x.ShopId, x.Day })
                .MustAsync(
                    async (dto, cancellation) =>
                    {
                        var workingHour = await unitOfWork.WorkingHourRepository.IsWorkingHourDayTimeExistsAsync(dto.ShopId, dto.Day);
                        return !workingHour;
                    }
                )
                .WithMessage("WorkingHour already exists");
        }
    }
}
