using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.CoreService.Application.Models.DTOs;
using Microsoft.Extensions.Localization;

namespace CRM.CoreService.Infrastructure.Services
{
    public class EmailMessageBuilder
    {
        private readonly IStringLocalizer<EmailMessageBuilder> _localizer;
        public EmailMessageBuilder(IStringLocalizer<EmailMessageBuilder> localizer)
        {
            _localizer = localizer;
        }
        public EmailMessageDTO BuildRegistrationMessage(string email, string temporaryPassword, string culture)
        {
            CultureInfo.CurrentCulture = new CultureInfo(culture);
            CultureInfo.CurrentUICulture = new CultureInfo(culture);

            var subject = _localizer["Subject"].Value;
            var body = string.Format(_localizer["Body"].Value, email, temporaryPassword);
            var bodyHtml = ConvertToHtml(body);

            return new EmailMessageDTO
            {
                Subject = subject,
                Body = bodyHtml,
            };
        }
        private string ConvertToHtml(string text)
        {
            return $@"
        <html>
        <body>
            <p>{text.Replace("\n", "<br>")}</p>
        </body>
        </html>";
        }
    }
}
