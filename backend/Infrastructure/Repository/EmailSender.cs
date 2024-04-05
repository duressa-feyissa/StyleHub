using System.Net;
using System.Net.Mail;
using Application.Contracts.Infrastructure.Services;
using Application.DTO.User.AuthenticationDTO.DTO;
using Infrastructure.Models;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repository
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        private static async Task SendEmailAsync(SmtpClient client, MailMessage message)
        {
            try
            {
                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task SendEmail(EmailDTO email)
        {
            var message = new MailMessage();

            if (
                _emailSettings.SenderEmail != null
                || _emailSettings.SenderPassword != null
                || _emailSettings.DisplayName != null
            )
            {
                message.From = new MailAddress(
                    _emailSettings.SenderEmail!,
                    _emailSettings.DisplayName
                );
                message.To.Add(email.To);
                message.Subject = email.Subject;
                message.Body = email.Body;
                message.IsBodyHtml = true;

                var client = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(
                        _emailSettings.SenderEmail,
                        _emailSettings.SenderPassword
                    ),
                    EnableSsl = true
                };

                await SendEmailAsync(client, message);
            }
        }
    }
}
