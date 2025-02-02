using CRM.LoggerService.Infrastructure.Data.Context;
using CRM.LoggerService.Infrastructure.Migrations;
using CRM.LoggerService.Infrastructure.Repositories;
using CRM.LoggerService.Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Reflection;
using System.Xml.Linq;

namespace CRM.LoggerService.Infrastructure.Extensions
{
    public static class ServiceCollectionApplicationExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager config)
        {
            string mongoConnectionString = config["MongoDb:Connectionstring"];  
            string dbName = config["MongoDb:DatabaseName"]; 

            //register context 
            services.AddDbContext<LoggerDbContext>(options =>
                options.UseMongoDB(mongoConnectionString, dbName));

            services.AddScoped<IUserLogRepository, UserLogRepository>();
            services.AddScoped<IReportLogRepository, ReportLogRepository>();
            services.AddScoped<IFileLogRepository, FileLogRepository>();
            services.AddScoped<IGraphQLLogRepository, GraphQLLogRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //register migrations
            services.AddScoped<MigrationManager>();

            return services;
        }
        
    }
}
