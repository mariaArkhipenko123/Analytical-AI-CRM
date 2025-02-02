using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.LoggerService.Application.Extensions
{
	public static class ServiceCollectionApplicationExtensions
	{
		public static IServiceCollection AddApplication(
			 this IServiceCollection services, ConfigurationManager config)
		{
			return services;
		}
	}
}
