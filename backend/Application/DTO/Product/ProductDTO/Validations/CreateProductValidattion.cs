using backend.Application.Contracts.Persistence.Repositories.Shop;
using backend.Application.DTO.Product.ProductDTO.DTO;
using FluentValidation;

namespace backend.Application.DTO.Product.ProductDTO.Validations
{
    public class CreateProductValidation : AbstractValidator<CreateProductDTO>
    {
        string[] Condition = { "new", "used", "fairy used" };

        public CreateProductValidation(IShopRepository shopRepository)
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

            RuleFor(x => x.ShopId)
                .NotNull()
                .WithMessage("ShopId is required")
                .NotEmpty()
                .WithMessage("ShopId cannot be empty")
                .MustAsync(async (shopId, token) => await shopRepository.ExistsAsync(shopId));
            
            RuleFor(x => x.VideoUrl)
                .Cascade(CascadeMode.Stop)
                .Must(x => string.IsNullOrEmpty(x) || Uri.IsWellFormedUriString(x, UriKind.Absolute))
                .WithMessage("VideoUrl must be a valid URL");
        }
    }
}
