using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.AIService.Application.CQRS.Queries
{
    public class GenerateReportQuery : IRequest<string>
    {
        public string Data { get; set; }
    }
}
