using CRM.AIService.Application.CQRS.Queries;
using CRM.AIService.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CRM.AIService.Application.Services
{
    public class AIRequestHandler
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public AIRequestHandler(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
       public async Task<string> Handle(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            var jsonDoc = JsonDocument.Parse(responseContent);
            if (jsonDoc.RootElement.TryGetProperty("text", out JsonElement textElement))
            {
                return textElement.GetString() ?? "Error: No 'text' field found";
            }
            return "Error: 'text' field not found in the response";
        }
    }
}
