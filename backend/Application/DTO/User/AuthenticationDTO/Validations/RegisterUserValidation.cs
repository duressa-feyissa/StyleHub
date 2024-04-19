using backend.Application.Contracts.Persistence.Repositories.User;
using backend.Application.DTO.User.AuthenticationDTO.DTO;
using FluentValidation;

namespace backend.Application.DTO.User.AuthenticationDTO.Validations
{
    public class RegisterUserValidation : AbstractValidator<RegisterationRequestDTO>
    {
        IUserRepository _UserRepository;

        public RegisterUserValidation(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;

            RuleFor(x => x.FirstName)
                .NotNull()
                .WithMessage("First name is required")
                .NotEmpty()
                .WithMessage("First name cannot be empty")
                .Custom(
                    (firstName, context) =>
                        context.InstanceToValidate.FirstName = firstName?.ToLower()
                );

            RuleFor(x => x.LastName)
                .NotNull()
                .WithMessage("Last name is required")
                .NotEmpty()
                .WithMessage("Last name cannot be empty")
                .Custom(
                    (lastName, context) => context.InstanceToValidate.LastName = lastName?.ToLower()
                );

            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("Password is required")
                .NotEmpty()
                .WithMessage("Password cannot be empty")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long")
                .MaximumLength(20)
                .WithMessage("Password must be at most 20 characters long");
            // .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).{6,20}$")
            // .WithMessage(
            //     "Password must contain at least one uppercase letter, one lowercase letter, one digit and one special character"
            // );

            // RuleFor(x => x.PhoneNumber)
            //     .NotEmpty()
            //     .When(x => x.PhoneNumber != null)
            //     .WithMessage("Phone number cannot be empty")
            //     .MustAsync(
            //         async (phoneNumber, cancellation) =>
            //         {
            //             if (phoneNumber == null)
            //                 return true;
            //             return await _UserRepository.GetByPhoneNumber(phoneNumber) == null;
            //         }
            //     )
            //     .When(x => x.PhoneNumber != null)
            //     .WithMessage("Phone number is already registered");

            RuleFor(x => x.Email)
                .NotEmpty()
                .When(x => x.Email != null)
                .WithMessage("Email cannot be empty")
                .MustAsync(
                    async (email, cancellation) =>
                    {
                        if (email == null)
                            return true;
                        return await _UserRepository.GetByEmail(email) == null;
                    }
                )
                .When(x => x.Email != null)
                .WithMessage("Email is already registered");
        }
    }
}
