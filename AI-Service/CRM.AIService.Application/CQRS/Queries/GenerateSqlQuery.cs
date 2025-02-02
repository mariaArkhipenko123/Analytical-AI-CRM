using CRM.AIService.Application.Services;
using CRM.AIService.Application.Utils.AIRequests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.AIService.Application.CQRS.Queries
{
    public class GenerateSqlQuery : IRequest<string>
    {
        public string Message { get; set; }
    }
}
