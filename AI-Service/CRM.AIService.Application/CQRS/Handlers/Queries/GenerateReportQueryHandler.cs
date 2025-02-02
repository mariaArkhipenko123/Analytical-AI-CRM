using CRM.AIService.Application.CQRS.Queries;
using CRM.AIService.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.AIService.Application.CQRS.Handlers.Queries
{
    public class GenerateReportQueryHandler : IRequestHandler<GenerateReportQuery, string>
    {
        private readonly AIRequestFactory _requestFactory;
        private readonly AIRequestHandler _requestHandler;
        public GenerateReportQueryHandler(AIRequestFactory requestFactory, IHttpClientFactory httpClientFactory, AIRequestHandler requestHandler)
        {
            _requestFactory = requestFactory;
            _requestHandler = requestHandler;
        }
        public async Task<string> Handle(GenerateReportQuery query, CancellationToken cancellationToken)
        {
            string message = "Generate a useful report from data you have received from preamble";
            var aiRequest = _requestFactory.GetReportAIRequest(message, query.Data);
            var request = aiRequest.GetRequest();
            var response = await _requestHandler.Handle(request, cancellationToken);
            return response;
        }
    }
}
