using Application.Contracts.Persistence.Repositories.Product;
using Application.DTO.Product.CategoryDTO.DTO;
using FluentValidation;

namespace Application.DTO.Product.CategoryDTO.Validations
{
    public class BaseCategoryValidation : AbstractValidator<CreateCategoryDTO>
    {
        ICategoryRepository _brandRepository;

        public BaseCategoryValidation(ICategoryRepository brandRepository)
        {
            _brandRepository = brandRepository;

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Name is required")
                .NotEmpty()
                .WithMessage("Name cannot be empty")
                .Custom((name, context) => context.InstanceToValidate.Name = name.ToLower());

            RuleFor(x => x.Image)
                .NotNull()
                .WithMessage("Image is required")
                .NotEmpty()
                .WithMessage("Image cannot be empty");

            RuleFor(x => x.Name)
                .MustAsync(
                    async (name, cancellation) =>
                    {
                        var brand = await _brandRepository.GetByName(name);
                        return brand == null;
                    }
                )
                .WithMessage("Category already exists");
        }
    }
}
