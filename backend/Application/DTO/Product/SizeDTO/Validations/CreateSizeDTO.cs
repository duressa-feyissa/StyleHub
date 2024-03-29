using Application.Contracts.Persistence.Repositories.Product;
using Application.DTO.Product.SizeDTO.DTO;
using FluentValidation;

namespace Application.DTO.Product.SizeDTO.Validations
{
    public class CreateSizeValidation : AbstractValidator<CreateSizeDTO>
    {
        ISizeRepository _sizeRepository;

        public CreateSizeValidation(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Name is required")
                .NotEmpty()
                .WithMessage("Name cannot be empty")
                .Custom((name, context) => context.InstanceToValidate.Name = name.ToLower());

            RuleFor(x => x.Abbreviation)
                .NotNull()
                .WithMessage("Abbreviation is required")
                .NotEmpty()
                .WithMessage("Abbreviation cannot be empty")
                .Custom(
                    (abbreviation, context) =>
                        context.InstanceToValidate.Abbreviation = abbreviation.ToLower()
                );

            RuleFor(x => x.Name)
                .MustAsync(
                    async (name, cancellation) =>
                    {
                        var size = await _sizeRepository.GetByName(name);
                        return size == null;
                    }
                )
                .WithMessage("Size already exists");

            RuleFor(x => x.Abbreviation)
                .MustAsync(
                    async (abbreviation, cancellation) =>
                    {
                        var size = await _sizeRepository.GetByAbbreviation(abbreviation);
                        return size == null;
                    }
                )
                .WithMessage("Size already exists");
        }
    }
}
