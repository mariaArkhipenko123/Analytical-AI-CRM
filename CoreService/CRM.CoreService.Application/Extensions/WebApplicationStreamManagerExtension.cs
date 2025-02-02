using CRM.CoreService.Application.Interfaces.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using CRM.CoreService.Domain.Enums;
using System.Diagnostics;


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
			ConfigureReportHandlers(streamManager);
			StartListeners(streamManager);
		}

		private static void ConfigureStreamListeners(IStreamManager streamManager)
		{
			// Example of adding a listener for a specific Redis stream
			streamManager.AddListener(RedisMessageStream.ReportStream);
		}
		private static void StartListeners(IStreamManager streamManager)
		{
			// Example of starting the listener for a specific Redis stream
			streamManager.StartListener(RedisMessageStream.ReportStream);
		}
		private static void ConfigureReportHandlers(IStreamManager streamManager)
		{
            // Example of subscribing to a specific task within a Redis stream
            //streamManager.SendAsync(RedisMessageStream.ReportStream, RedisTarget.aiService, RedisTask.ExecuteSQL, "Hello");
			//streamManager.Subscribe<string>(RedisMessageStream.ReportStream, RedisTask.ExecuteSQL, MessageHandler);
		}

		//private static void MessageHandler(string message)
		//{
		//	Debug.WriteLine(message + "qwe");
		//}
	}
}
