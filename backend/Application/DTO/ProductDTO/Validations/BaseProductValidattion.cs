using Application.DTO.ProductDTO.DTO;
using FluentValidation;

namespace Application.DTO.ProductDTO.Validations
{
    public class BaseProductValidation : AbstractValidator<BaseProductDTO>
    {
        public BaseProductValidation()
        {
            RuleFor(x => x.Title)
                .NotNull().WithMessage("Title is required")
                .NotEmpty().WithMessage("Title cannot be empty");

            RuleFor(x => x.Description)
                .NotNull().WithMessage("Description is required")
                .NotEmpty().WithMessage("Description cannot be empty");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Price is required")
                .NotEmpty().WithMessage("Price cannot be empty")
                .GreaterThan(0).WithMessage("Price must be greater than 0");

        }
    }
}
