using CRM.CoreService.Application.Models.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.CoreService.Application.CQRS.Commands
{
    public class GoogleLoginCommand : IRequest<TokensResponseDTO>
    {
        public string IdToken { get; set; }
        public GoogleLoginCommand(string idToken)
        {
            IdToken = idToken;
        }
    }
}
