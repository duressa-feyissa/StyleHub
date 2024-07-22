using backend.Application.Contracts.Persistence.Repositories.Shop;
using backend.Application.DTO.Shop.ReviewDTO.DTO;
using FluentValidation;

namespace backend.Application.DTO.Shop.ReviewDTO.Validations;

public class CreateReviewValidation: AbstractValidator<CreateReviewDTO>
{
    public CreateReviewValidation(IShopRepository shopRepository)
    {
        RuleFor(x => x.Rating).InclusiveBetween(1, 5);
        RuleFor(x => x.Review)
            .NotNull()
            .WithMessage("Review is required")
            .NotEmpty()
            .WithMessage("Review is required")
            .MinimumLength(10)
            .MaximumLength(500);
        RuleFor(x => x.ShopId)
            .NotNull()
            .WithMessage("ShopId is required")
            .NotEmpty()
            .MustAsync(async (shopId, cancellation) => await shopRepository.ExistsAsync(shopId));
    }
    
}