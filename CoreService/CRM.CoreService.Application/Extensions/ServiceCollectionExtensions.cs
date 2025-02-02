using CRM.CoreService.Application.GraphQL.Mutations;
using CRM.CoreService.Application.GraphQL.Queries;
using CRM.CoreService.Application.Interfaces.Services;
using CRM.CoreService.Application.Models.DTOs;
using CRM.CoreService.Application.Models.Mapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.CoreService.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceCollectionExtensions).Assembly));
            services
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))      
                .AddTypeExtension<UserQuery>()          
                .AddTypeExtension<ReportQuery>()
                .AddMutationType(d => d.Name("Mutation")) 
                .AddTypeExtension<UserMutation>()       
                .AddTypeExtension<ReportMutation>()     
                .AddProjections()
                .AddFiltering()
                .AddSorting();

            services.AddAutoMapper(typeof(MappingProfile));
            return services;
        }
    }
}
