using Application.Contracts.Persistence.Repositories.User;
using Application.DTO.User.AuthenticationDTO.DTO;
using FluentValidation;

namespace Application.DTO.User.UserDTO.Validations
{
    public class VerifyPhoneDTOValidation : AbstractValidator<VerifyPhoneDTO>
    {
        IUserRepository _UserRepository;

        public VerifyPhoneDTOValidation(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;

            RuleFor(x => x.PhoneNumber)
                .NotNull()
                .WithMessage("Phone number is required")
                .NotEmpty()
                .WithMessage("Phone number cannot be empty")
                .Matches(@"^(\+251|0)[789][01]\d{8}$")
                .WithMessage("Invalid phone number");

            RuleFor(x => x.Code)
                .NotNull()
                .WithMessage("Code is required")
                .NotEmpty()
                .WithMessage("Code cannot be empty")
                .Matches(@"^\d{4}$")
                .WithMessage("Invalid code");

            RuleFor(x => x.PhoneNumber)
                .MustAsync(
                    async (phoneNumber, cancellation) =>
                        await _UserRepository.IsPhoneNumberRegistered(phoneNumber) == true
                )
                .WithMessage("Phone number is not registered");
        }
    }
}
