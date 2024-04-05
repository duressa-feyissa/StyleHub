using FluentValidation;

namespace Application.DTO.Product.ProductDTO.DTO
{
    public class CreateProductValidation : AbstractValidator<CreateProductDTO>
    {
        string[] Condition = { "new", "used" };
        string[] Target = { "men", "women", "kid" };

        public CreateProductValidation()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .WithMessage("Title is required")
                .NotEmpty()
                .WithMessage("Title cannot be empty")
                .MinimumLength(3)
                .WithMessage("Title must be at least 3 characters long")
                .MaximumLength(50)
                .WithMessage("Title must be at most 50 characters long");

            RuleFor(x => x.Description)
                .NotNull()
                .WithMessage("Description is required")
                .NotEmpty()
                .WithMessage("Description cannot be empty")
                .MinimumLength(3)
                .WithMessage("Description must be at least 3 characters long")
                .MaximumLength(255)
                .WithMessage("Description must be at most 255 characters long");

            RuleFor(x => x.Price)
                .NotNull()
                .WithMessage("Price is required")
                .NotEmpty()
                .WithMessage("Price cannot be empty")
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0");

            RuleFor(x => x.Quantity)
                .NotNull()
                .WithMessage("Quantity is required")
                .NotEmpty()
                .WithMessage("Quantity cannot be empty")
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0");

            RuleFor(x => x.Condition)
                .NotNull()
                .WithMessage("Condition is required")
                .NotEmpty()
                .WithMessage("Condition cannot be empty")
                .Must(x => Condition.Contains(x))
                .WithMessage("Condition must be new or used");

            RuleFor(x => x.Target)
                .NotNull()
                .WithMessage("Target is required")
                .NotEmpty()
                .WithMessage("Target cannot be empty")
                .Must(x => Target.Contains(x))
                .WithMessage("Target must be men, women or kids");

            RuleFor(x => x.Latitude)
                .NotNull()
                .WithMessage("Latitude is required")
                .GreaterThanOrEqualTo(-90)
                .WithMessage("Latitude must be greater than -90")
                .LessThanOrEqualTo(90)
                .WithMessage("Latitude must be less than 90");

            RuleFor(x => x.Longitude)
                .NotNull()
                .WithMessage("Longitude is required")
                .GreaterThanOrEqualTo(-180)
                .WithMessage("Longitude must be greater than -180")
                .LessThanOrEqualTo(180)
                .WithMessage("Longitude must be less than 180");
        }
    }
}
