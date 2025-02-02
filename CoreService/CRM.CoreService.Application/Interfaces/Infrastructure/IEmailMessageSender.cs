using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRM.CoreService.Application.Models.DTOs;

namespace CRM.CoreService.Application.Interfaces.Infrastructure
{
    public interface IEmailMessageSender
    {
        Task SendEmailAsync(string to, EmailMessageDTO message);
    }

}
