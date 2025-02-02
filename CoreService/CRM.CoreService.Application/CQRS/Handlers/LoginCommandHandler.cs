using CRM.CoreService.Application.CQRS.Commands;
using CRM.CoreService.Application.Interfaces.Infrastructure;
using CRM.CoreService.Application.Models.DTOs;
using MediatR;

namespace CRM.CoreService.Application.CQRS.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, TokensResponseDTO>
    {
        private readonly IJwtAuthService _jwtAuthService;
        public LoginCommandHandler(IJwtAuthService jwtAuthService)
        {
            _jwtAuthService = jwtAuthService;
        }
        public async Task<TokensResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _jwtAuthService.LoginAsync(request.Email, request.Password);
        }
    }

}
