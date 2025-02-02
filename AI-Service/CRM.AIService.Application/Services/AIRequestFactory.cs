using CRM.AIService.Application.Utils.AIRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.AIService.Application.Services
{
    public class AIRequestFactory
    {
        public SqlAIRequestConstructor GetSqlAIRequest(string message, string preamble)
        {
            return new SqlAIRequestConstructor(message, preamble);
        }
        public ReportAIRequestConstructor GetReportAIRequest(string message, string data)
        {
            return new ReportAIRequestConstructor(message, data);
        }
    }
}
