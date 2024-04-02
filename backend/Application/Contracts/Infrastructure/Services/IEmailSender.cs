using Application.DTO.User.AuthenticationDTO.DTO;

namespace Application.Contracts.Infrastructure.Services
{
   public interface IEmailSender
{
    Task SendEmail(EmailDTO emailDto);
}
}
