using CRM.ReportService.Application.Models.Input;
using MediatR;

namespace CRM.ReportService.Application.CQRS.Commands
{
    public class ProcessReportCommand : IRequest<Guid>
    {
        public AIServiceInputModel Input { get; }

        public ProcessReportCommand(AIServiceInputModel input)
        {
            Input = input;
        }
    }
}
