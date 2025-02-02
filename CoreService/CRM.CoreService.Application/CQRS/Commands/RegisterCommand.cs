using CRM.CoreService.Application.Models.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.CoreService.Application.CQRS.Commands
{
    public class RegisterCommand : IRequest<TokensResponseDTO>
    {
        public string Email { get; set; }
        public RegisterCommand(string email)
        {
            Email = email;
        }
    }

}
