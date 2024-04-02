using Application.DTO.User.AuthenticationDTO.DTO;
using Domain.Entities.User;

namespace Application.Contracts.Infrastructure.Services
{
    public interface IAuthenticationService
    {
        public AuthenticationResponseDTO Login(
            LoginRequestDTO user,
            User userEntity,
            bool LoginAfterOtpVerification = default
        );
    }
}
