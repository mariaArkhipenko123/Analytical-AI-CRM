using CRM.AIService.Application.Interfaces;
using CRM.AIService.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.AIService.Infrastructure.Extensions
{
	public static class ServiceCollectionApplicationExtensions
	{
		public static IServiceCollection AddInfrastructure(
			 this IServiceCollection services, ConfigurationManager config)
		{
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = config["ConnectionStrings:Redis"];
            });
			services.AddSingleton<ICacheService, CacheServiceWrapper>();
            return services;
		}
	}
}
