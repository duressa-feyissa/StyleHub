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

            RuleFor(x => x.BinaryImages)
                .NotNull()
                .WithMessage("BinaryImages is required")
                .NotEmpty()
                .WithMessage("BinaryImages cannot be empty");

            RuleFor(x => x.IsNegotiable)
                .NotNull()
                .WithMessage("IsNegotiable is required")
                .NotEmpty()
                .WithMessage("IsNegotiable cannot be empty");
        }
    }
}
