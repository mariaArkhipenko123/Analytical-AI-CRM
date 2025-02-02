using CRM.CoreService.Application.CQRS.Commands;
using CRM.CoreService.Application.Interfaces.Infrastructure;
using CRM.CoreService.Application.Models.DTOs;
using MediatR;

namespace CRM.CoreService.Application.CQRS.Handlers
{
    public class RefreshTokensCommandHandler : IRequestHandler<RefreshTokensCommand, TokensResponseDTO>
    {
        private readonly IJwtAuthService _jwtAuthService;
        public RefreshTokensCommandHandler(IJwtAuthService jwtAuthService)
        {
            _jwtAuthService = jwtAuthService;
        }
        public async Task<TokensResponseDTO> Handle(RefreshTokensCommand request, CancellationToken cancellationToken)
        {
            return await _jwtAuthService.RefreshTokens(request.UserId);
        }
    }

}
