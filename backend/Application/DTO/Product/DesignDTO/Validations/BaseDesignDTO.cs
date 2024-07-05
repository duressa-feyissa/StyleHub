using backend.Application.Contracts.Persistence.Repositories.Product;
using backend.Application.DTO.Product.DesignDTO.DTO;
using FluentValidation;

namespace backend.Application.DTO.Product.DesignDTO.Validations
{
    public class BaseDesignValidation : AbstractValidator<BaseDesignDTO>
    {
        IDesignRepository _designRepository;

        public BaseDesignValidation(IDesignRepository designRepository)
        {
            _designRepository = designRepository;

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
                        var design = await _designRepository.GetByName(name);
                        return design == null;
                    }
                )
                .WithMessage("Design already exists");
        }
    }
}
