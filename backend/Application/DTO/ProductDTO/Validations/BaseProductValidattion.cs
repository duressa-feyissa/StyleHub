using Application.DTO.ProductDTO.DTO;
using FluentValidation;
using Application.Contracts;

namespace Application.DTO.ProductDTO.Validations
{
    public class BaseProductValidation : AbstractValidator<BaseProductDTO>
    {
        string[] Condition = { "new", "used" };
        string[] Target = { "men", "women", "kid" };
        public BaseProductValidation()
        {

            RuleFor(x => x.Title)
                .NotNull().WithMessage("Title is required")
                .NotEmpty().WithMessage("Title cannot be empty")
                .MinimumLength(3).WithMessage("Title must be at least 3 characters long")
                .MaximumLength(50).WithMessage("Title must be at most 50 characters long");

            RuleFor(x => x.Description)
                .NotNull().WithMessage("Description is required")
                .NotEmpty().WithMessage("Description cannot be empty")
                .MinimumLength(3).WithMessage("Description must be at least 3 characters long")
                .MaximumLength(255).WithMessage("Description must be at most 255 characters long");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Price is required")
                .NotEmpty().WithMessage("Price cannot be empty")
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.Quantity)
                .NotNull().WithMessage("Quantity is required")
                .NotEmpty().WithMessage("Quantity cannot be empty")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0");

            RuleFor(x => x.Condition)
                .NotNull().WithMessage("Condition is required")
                .NotEmpty().WithMessage("Condition cannot be empty")
                .Must(x => Condition.Contains(x)).WithMessage("Condition must be new or used");

            RuleFor(x => x.Target)
                .NotNull().WithMessage("Target is required")
                .NotEmpty().WithMessage("Target cannot be empty")
                .Must(x => Target.Contains(x)).WithMessage("Target must be men, women or kids");

            RuleFor(x => x.BrandId)
                .NotNull().WithMessage("BrandId is required")
                .NotEmpty().WithMessage("BrandId cannot be empty");

            RuleFor(x => x.SizeIds)
                .NotNull().WithMessage("SizeIds is required")
                .Must(sizes => sizes != null && sizes.Any())
                .WithMessage("At least one size must be selected");

            RuleFor(x => x.ColorIds)
                .NotNull().WithMessage("ColorIds is required")
                .Must(colors => colors != null && colors.Any())
                .WithMessage("At least one color must be selected");

            RuleFor(x => x.MaterialIds)
                .NotNull().WithMessage("MaterialIds is required")
                .Must(materials => materials != null && materials.Any())
                .WithMessage("At least one material must be selected");


            RuleFor(x => x.IsNegotiable)
                .NotNull().WithMessage("IsNegotiable is required")
                .NotEmpty().WithMessage("IsNegotiable cannot be empty");


        }
    }
}
