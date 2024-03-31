using Application.Contracts.Persistence.Repositories.User;
using Application.DTO.User.UserDTO.DTO;
using FluentValidation;

namespace Application.DTO.User.UserDTO.Validations
{
    public class CreateUserValidation : AbstractValidator<CreateUserDTO>
    {
        IUserRepository _UserRepository;

        public CreateUserValidation(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;

            RuleFor(x => x.FirstName)
                .NotNull()
                .WithMessage("First name is required")
                .NotEmpty()
                .WithMessage("First name cannot be empty")
                .Custom(
                    (firstName, context) =>
                        context.InstanceToValidate.FirstName = firstName.ToLower()
                );

            RuleFor(x => x.LastName)
                .NotNull()
                .WithMessage("Last name is required")
                .NotEmpty()
                .WithMessage("Last name cannot be empty")
                .Custom(
                    (lastName, context) => context.InstanceToValidate.LastName = lastName.ToLower()
                );

            RuleFor(x => x.PhoneNumber)
                .NotNull()
                .WithMessage("Phone number is required")
                .NotEmpty()
                .WithMessage("Phone number cannot be empty")
                .Matches(@"^(\+251|0)[789][01]\d{8}$")
                .WithMessage("Invalid phone number");

            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("Password is required")
                .NotEmpty()
                .WithMessage("Password cannot be empty")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long")
                .MaximumLength(20)
                .WithMessage("Password must be at most 20 characters long")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,20}$")
                .WithMessage(
                    "Password must contain at least one uppercase letter, one lowercase letter, one digit and one special character"
                );
        }
    }
}
