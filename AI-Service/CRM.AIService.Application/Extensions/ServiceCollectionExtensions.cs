using CRM.AIService.Application.CQRS.Handlers.Queries;
using CRM.AIService.Application.CQRS.Queries;
using CRM.AIService.Application.Services;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CRM.AIService.Application.Extensions
{
    public static class ServiceCollectionApplicationExtensions
	{
		public static IServiceCollection AddApplication(
			 this IServiceCollection services, ConfigurationManager config)
		{
            services.AddHttpClient();
            services.AddSingleton<AIRequestFactory>();
			services.AddScoped<AIRequestHandler>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GenerateSqlQueryHandler).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GenerateReportQueryHandler).Assembly));
            return services;
		}
	}
}
