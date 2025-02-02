using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CRM.AIService.Application.Utils.AIRequests
{
    public abstract class AbstractAIRequest
    {
        public string URL { get; set; } = "https://api.cohere.com/v1/chat";
        public string AcceptHeader { get; set; } = "application/json";
        public string ContentType { get; set; } = "application/json";
        public string Authorization { get; set; } = "xn3g2uPAbeq8JiBpAJ0kfj7iDGFc3CP0bfRwdQut";
        public RequestBody Body { get; set; } = new RequestBody()
        {
            AIModel = "command - r - 08 - 2024",
        };
        public class RequestBody
        {
            public string Message { get; set; } = string.Empty;
            public string AIModel { get; set; }
            public string Preamble { get; set; } = string.Empty;
        }
        public virtual HttpRequestMessage GetRequest()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, URL);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Authorization);
            request.Headers.Add("Accept", AcceptHeader);
            return request;
        }
    }
}
