using CRM.FileService.Application.Interfaces.Infrastructure;
using CRM.FileService.Domain.Entities;
using CRM.FileService.Infrastructure.Context;
using CRM.FileService.Infrastructure.Repositories;
using CRM.FileService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.FileService.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("FileServiceConnection");
            services.AddDbContext<PostgresDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddScoped<IGenericRepository<FileEntity>, GenericRepository<FileEntity>>();

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var connectionConfiguration = configuration["ConnectionStrings:Redis"];
                return ConnectionMultiplexer.Connect(connectionConfiguration);
            });

            services.AddScoped<IRedisMessageBroker, RedisMessageBroker>();

            return services;
        }
    }
}
