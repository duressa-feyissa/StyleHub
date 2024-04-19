using backend.Application.DTO.User.AuthenticationDTO.DTO;

namespace backend.Application.Contracts.Infrastructure.Services
{
   public interface IEmailSender
{
    Task SendEmail(EmailDTO emailDto);
}
}
