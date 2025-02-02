using CRM.CoreService.Application.Interfaces.Infrastructure;
using CRM.CoreService.Application.Models.DTOs;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace CRM.CoreService.Infrastructure.Services
{
    public class SmtpEmailSender : IEmailMessageSender
    {
        private readonly SmtpSettingsDTO _smtpSettings;
        public SmtpEmailSender(IOptions<SmtpSettingsDTO> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string to, EmailMessageDTO messageDTO)
        {
            using var smtpClient = new SmtpClient(_smtpSettings.Provider)
            {
                Port = _smtpSettings.Port,
                Credentials = new NetworkCredential(_smtpSettings.From, _smtpSettings.Password),
                EnableSsl = true
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_smtpSettings.From),
                Subject = messageDTO.Subject,
                Body = messageDTO.Body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(to);
            await smtpClient.SendMailAsync(mailMessage);
        }
    }

}
