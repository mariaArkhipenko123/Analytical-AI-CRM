using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static CRM.AIService.Application.Utils.AIRequests.AbstractAIRequest;

namespace CRM.AIService.Application.Utils.AIRequests
{
    public class SqlAIRequestConstructor : AbstractAIRequest
    {
        private readonly string _message;
        private readonly string _preamble;
        public SqlAIRequestConstructor(string message, string preamble)
        {
            _message = message;
            _preamble = preamble;
        }
        public override HttpRequestMessage GetRequest()
        {
            var request = base.GetRequest();
            Body.Message = _message;
            Body.Preamble = _preamble;
            request.Content = new StringContent(JsonSerializer.Serialize(Body), Encoding.UTF8, ContentType);
            return request;
        }
    }
}
