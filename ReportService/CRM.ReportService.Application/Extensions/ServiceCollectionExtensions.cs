using CRM.ReportService.Application.CQRS.Commands;
using CRM.ReportService.Application.Interfaces.Application;
using CRM.ReportService.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.ReportService.Application.Extensions
{
    public static class ServiceCollectionApplicationExtensions
    {
        public static IServiceCollection AddApplication(
             this IServiceCollection services, ConfigurationManager config)
        {
            services.AddSingleton<FileGeneratorFactoryService>(); 
            services.AddScoped<PdfGeneratorService>();           
            services.AddScoped<ExcelGeneratorService>();        
            services.AddScoped<IFileGenerator, PdfGeneratorService>();
            services.AddScoped<IFileGenerator, ExcelGeneratorService>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProcessReportCommand).Assembly));

            return services;
        }
    }
}
