using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Application.DTO.Product.MaterialDTO.DTO;
using FluentValidation;

namespace backend.Application.DTO.Product.MaterialDTO.Validations
{
    public class BaseMaterialValidation : AbstractValidator<BaseMaterialDTO>
    {
        IMaterialRepository _materialRepository;

        public BaseMaterialValidation(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Name is required")
                .NotEmpty()
                .WithMessage("Name cannot be empty")
                .Custom((name, context) => context.InstanceToValidate.Name = name.ToLower());

            RuleFor(x => x.Name)
                .MustAsync(
                    async (name, cancellation) =>
                    {
                        var material = await _materialRepository.GetByName(name);
                        return material == null;
                    }
                )
                .WithMessage("Material already exists");
        }
    }
}
