using Application.Contracts.Persistence.Repositories.User;
using Application.DTO.User.AuthenticationDTO.DTO;
using FluentValidation;

namespace Application.DTO.User.UserDTO.Validations
{
    public class LoginUserValidation : AbstractValidator<LoginRequestDTO>
    {

        public LoginUserValidation()
        {

            // RuleFor(x => x.PhoneNumber)
            //     .Cascade(CascadeMode.Stop)
            //     .NotNull()
            //     .WithMessage("Phone number is required")
            //     .NotEmpty()
            //     .WithMessage("Phone number cannot be empty");
            //     //.Matches(@"^(\+251|0)[789][01]\d{8}$")
            //     //.WithMessage("Invalid phone number");

            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
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
                //     "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character"
                // );

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("Email is required")
                .NotEmpty()
                .WithMessage("Email cannot be empty")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                .WithMessage("Invalid email");
        }
    }
}
