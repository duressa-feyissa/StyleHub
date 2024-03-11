using FluentValidation;
using StyleHub.Application.Contracts;
using StyleHub.Application.DTO.UserDTO.DTO;

namespace StyleHub.Application.DTO.UserDTO.Validations
{
    public class UserCreateValidation : AbstractValidator<UserCreateDTO>
    {
        IUserRepository _userRepository;
        public UserCreateValidation(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required");
            RuleFor(x => x.Email).MustAsync(async (email, cancellation) =>
           {
               var allUsers = await _userRepository.GetAllUser();

               foreach (var user in allUsers)
               {
                   if (user.Email == email)
                   {
                       return false;
                   }
               }

               return true;
           }).WithMessage("A User with this email already exists");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }
    }
}