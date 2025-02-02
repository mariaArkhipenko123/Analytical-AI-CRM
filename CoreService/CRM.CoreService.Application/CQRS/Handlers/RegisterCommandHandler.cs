using CRM.CoreService.Application.CQRS.Commands;
using CRM.CoreService.Application.Interfaces.Infrastructure;
using CRM.CoreService.Application.Models.DTOs;
using MediatR;

namespace CRM.CoreService.Application.CQRS.Handlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, TokensResponseDTO>
    {
        private readonly IJwtAuthService _jwtAuthService;
        public RegisterCommandHandler(IJwtAuthService jwtAuthService)
        {
            _jwtAuthService = jwtAuthService;
        }
        public async Task<TokensResponseDTO> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _jwtAuthService.RegisterAsync(request.Email);
        }
    }
}
