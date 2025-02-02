using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using CRM.ReportService.Application.Interfaces.Infrastructure;
using CRM.ReportService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using CRM.ReportService.Infrastructure.Contexts;
using CRM.ReportService.Infrastructure.MessageBroker;
using CRM.ReportService.Infrastructure.Repositories;
using CRM.ReportService.Infrastructure.UofW;

namespace CRM.ReportService.Infrastructure.Extensions
{
    public static class ServiceCollectionApplicationExtensions
    {
        public static IServiceCollection AddInfrastructure(
             this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var connectionConfiguration = configuration["ConnectionStrings:Redis"];
                return ConnectionMultiplexer.Connect(connectionConfiguration);
            });

            services.AddDbContext<PostgresDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgresConnection")));

            // Регистрация для кэша
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["ConnectionStrings:Redis"];
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IReportRepository, ReportRepository>();

            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<IRedisMessageBroker, RedisMessageBroker>();
            services.AddSingleton<IStreamManager, RedisStreamManager>();
            return services;
        }
    }
}
