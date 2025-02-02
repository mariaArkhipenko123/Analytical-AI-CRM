using CRM.ReportService.Application.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using CRM.ReportService.Application.Models.Input;
using CRM.ReportService.Application.CQRS.Commands;
using MediatR;

namespace CRM.CoreService.Infrastructure.Extensions
{
    /// <summary>
    /// This static class provides extension methods to configure and initialize the Stream Manager within an API layer.
    /// It is responsible for setting up listeners for specific Redis streams, configuring event handlers for various 
    /// tasks, and starting the listening process.
    /// </summary>
    public static class WebApplicationStreamManagerExtension
	{

		public static void UseStreamManager(this WebApplication app)
		{
			
            var streamManager = app.Services.GetRequiredService<IStreamManager>();
            ConfigureStreamListeners(streamManager);
            ConfigureReportHandlers(streamManager, app.Services);
            StartListeners(streamManager);
        }

		private static void ConfigureStreamListeners(IStreamManager streamManager)
		{
            streamManager.AddListener("AIServiceStream");
        }
		private static void StartListeners(IStreamManager streamManager)
		{
            streamManager.StartListener("AIServiceStream");
        }
		private static void ConfigureReportHandlers(IStreamManager streamManager, IServiceProvider serviceProvider)
        {
            var mediator = serviceProvider.GetRequiredService<IMediator>();

            streamManager.Subscribe<AIServiceInputModel>(
                "AIServiceStream",
                "GenerateReport",
                async (input) => await mediator.Send(new ProcessReportCommand(input))
            );
        }
    }
}
