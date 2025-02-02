using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CRM.AIService.Application.Utils.AIRequests
{
    public class ReportAIRequestConstructor : AbstractAIRequest
    {
        private readonly string _message;
        private readonly string _data;
        public ReportAIRequestConstructor(string message, string data)
        {
            _message = message;
            _data = data;
        }
        public override HttpRequestMessage GetRequest()
        {
            var request = base.GetRequest();
            Body.Message = _message;
            Body.Preamble = _data;
            request.Content = new StringContent(JsonSerializer.Serialize(Body), Encoding.UTF8, ContentType);
            return request;
        }
    }
}
