using CRM.CoreService.Application.Interfaces.Infrastructure;
using CRM.CoreService.Application.Interfaces.Repositories;
using CRM.CoreService.Application.Models.DTOs;
using CRM.CoreService.Domain.Entities;
using CRM.CoreService.Infrastructure.AuthenticationServices;
using CRM.CoreService.Infrastructure.Contexts;
using CRM.CoreService.Infrastructure.MessageBroker;
using CRM.CoreService.Infrastructure.Repositories;
using CRM.CoreService.Infrastructure.Services;
using CRM.CoreService.Infrastructure.TokenProviders;
using CRM.CoreService.Infrastructure.UoW;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace CRM.CoreService.Infrastructure.Extensions
{
	public static  class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgresConnection");
            services.AddDbContext<PostgresDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            services.AddIdentity<UserEntity, RoleEntity>()
                .AddEntityFrameworkStores<PostgresDbContext>()
                .AddDefaultTokenProviders();


            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var connectionConfiguration = configuration["ConnectionStrings:Redis"];
                return ConnectionMultiplexer.Connect(connectionConfiguration);
            });
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["ConnectionStrings:Redis"];
            });
            services.AddSingleton<IStreamManager, RedisStreamManager>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddScoped<IRedisMessageBroker, RedisMessageBroker>();
            services.AddScoped<IRawSqlRepository, RawSqlRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IJwtAuthService, JwtAuthService>();
            services.AddScoped<IGoogleAuthService, GoogleAuthService>(); //optional feature
            services.AddScoped<IEmailMessageSender, SmtpEmailSender>();
            services.Configure<SmtpSettingsDTO>(configuration.GetSection("Smtp"));
            services.AddTransient<IPasswordGenerator, PasswordGenerator>();
            services.AddTransient<EmailMessageBuilder>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddControllers()
            .AddDataAnnotationsLocalization()
            .AddViewLocalization();
            return services;
        }
    }
}
