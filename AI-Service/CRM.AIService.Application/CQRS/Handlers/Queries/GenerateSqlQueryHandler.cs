using CRM.AIService.Application.CQRS.Queries;
using CRM.AIService.Application.Interfaces;
using CRM.AIService.Application.Models;
using CRM.AIService.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CRM.AIService.Application.CQRS.Handlers.Queries
{
    public class GenerateSqlQueryHandler : IRequestHandler<GenerateSqlQuery, string>
    {
        private readonly AIRequestFactory _requestFactory;
        private readonly AIRequestHandler _requestHandler;
        private readonly ICacheService _cacheService;
        public GenerateSqlQueryHandler(AIRequestFactory requestFactory, IHttpClientFactory httpClientFactory, AIRequestHandler requestHandler, ICacheService cacheService)
        {
            _requestFactory = requestFactory;
            _requestHandler = requestHandler;
            _cacheService = cacheService;
        }
        public async Task<string> Handle(GenerateSqlQuery query, CancellationToken cancellationToken)
        {
            var preamble = await _cacheService.GetAsync<string>("db_schema");
            var aiRequest = _requestFactory.GetSqlAIRequest(query.Message, preamble);
            var request = aiRequest.GetRequest();
            var response = await _requestHandler.Handle(request, cancellationToken);
            return response;
        }
    }
}
