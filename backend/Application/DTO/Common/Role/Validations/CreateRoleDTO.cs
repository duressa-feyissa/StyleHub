using backend.Application.Contracts.Persistence.Repositories.Common;
using backend.Application.DTO.Common.Role.DTO;
using FluentValidation;

namespace backend.Application.DTO.Common.Role.Validations
{
    public class CreateRoleValidation : AbstractValidator<CreateRoleDTO>
    {
        IRoleRepository _RoleRepository;

        public CreateRoleValidation(IRoleRepository RoleRepository)
        {
            _RoleRepository = RoleRepository;

            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Name is required")
                .NotEmpty()
                .WithMessage("Name cannot be empty")
                .Custom((name, context) => context.InstanceToValidate.Name = name.ToLower());

            RuleFor(x => x.Description)
                .NotNull()
                .WithMessage("Description is required")
                .NotEmpty()
                .WithMessage("Description cannot be empty")
                .MaximumLength(200)
                .WithMessage("Description cannot be more than 200 characters")
                .MinimumLength(3)
                .WithMessage("Description cannot be less than 3 characters");

            RuleFor(x => x.Code)
                .NotNull()
                .WithMessage("Code is required")
                .NotEmpty()
                .WithMessage("Code cannot be empty")
                .Custom((code, context) => context.InstanceToValidate.Code = code.ToLower());

            RuleFor(x => x.Name)
                .MustAsync(
                    async (name, cancellation) =>
                    {
                        var Role = await _RoleRepository.GetByName(name);
                        return Role == null;
                    }
                )
                .WithMessage("Role already exists");

            RuleFor(x => x.Code)
                .MustAsync(
                    async (code, cancellation) =>
                    {
                        var Role = await _RoleRepository.GetByCode(code);
                        return Role == null;
                    }
                )
                .WithMessage("Role code already exists");
        }
    }
}
