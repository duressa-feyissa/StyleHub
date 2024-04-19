using backend.Application.DTO.User.AuthenticationDTO.DTO;
using backend.Domain.Entities.User;

namespace backend.Application.Contracts.Infrastructure.Services
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
