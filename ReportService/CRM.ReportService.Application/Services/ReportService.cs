using CRM.ReportService.Application.CQRS.Commands;
using CRM.ReportService.Application.Interfaces.Application;
using CRM.ReportService.Application.Interfaces.Infrastructure;
using CRM.ReportService.Application.Models.Input;
using MediatR;

namespace CRM.ReportService.Application.Services
{
    public class ReportService : IReportService
    {
        private readonly IStreamManager _streamManager;
        private readonly IMediator _mediator;

        public ReportService(IStreamManager streamManager, IMediator mediator)
        {
            _streamManager = streamManager;
            _mediator = mediator;
        }

        public void Start()
        {
            _streamManager.Subscribe<AIServiceInputModel>(
                "AIServiceStream",
                "GenerateReport",
                async (input) => await _mediator.Send(new ProcessReportCommand(input))
            );

            _streamManager.StartListener("AIServiceStream");
        }
    }
}
