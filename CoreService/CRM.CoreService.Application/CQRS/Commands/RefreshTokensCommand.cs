using CRM.CoreService.Application.Models.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.CoreService.Application.CQRS.Commands
{
    public class RefreshTokensCommand : IRequest<TokensResponseDTO>
    {
        public Guid UserId { get; set; }
        public RefreshTokensCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}
