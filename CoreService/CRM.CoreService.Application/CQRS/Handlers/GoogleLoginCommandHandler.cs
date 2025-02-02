using CRM.CoreService.Application.CQRS.Commands;
using CRM.CoreService.Application.Interfaces.Infrastructure;
using CRM.CoreService.Application.Models.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.CoreService.Application.CQRS.Handlers
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommand, TokensResponseDTO>
    {
        private readonly IJwtAuthService _jwtAuthService;
        public GoogleLoginCommandHandler(IJwtAuthService jwtAuthService)
        {
            _jwtAuthService = jwtAuthService;
        }

        public async Task<TokensResponseDTO> Handle(GoogleLoginCommand request, CancellationToken cancellationToken)
        {
            return await _jwtAuthService.LoginWithGoogleAsync(request.IdToken);
        }
    }
}
