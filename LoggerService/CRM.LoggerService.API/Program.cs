using CRM.LoggerService.Application.Extensions;
using CRM.LoggerService.Infrastructure.Extensions;
using CRM.LoggerService.Infrastructure.Migrations;

namespace CRM.LoggerService.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApplication(builder.Configuration);
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();

            //For migrations. If you want to run migration in terminal write: dotnet run -- --migrate
            if (args.Contains("--migrate"))
            {
                using var scope = app.Services.CreateScope();
                var serviceProvider = scope.ServiceProvider;

                var migrationManager = serviceProvider.GetRequiredService<MigrationManager>();

                var migrations = new List<Migration>
                {
                    //you can configure this different type of migration
                    new AddDefaultValuesToGraphQLLogs(),
                };

                foreach (var migration in migrations)
                {
                    await migrationManager.ApplyMigrationAsync(migration);
                }

                Console.WriteLine("Migrations applied successfully.");
                return; 
            }

            //If you want to rollback migration in terminal write: dotnet run -- --rollback
            if (args.Contains("--rollback"))
            {
                using var scope = app.Services.CreateScope();
                var serviceProvider = scope.ServiceProvider;

                var migrationManager = serviceProvider.GetRequiredService<MigrationManager>();

                var migrations = new List<Migration>
                {
                    new AddDefaultValuesToGraphQLLogs(),
                };

                foreach (var migration in migrations)
                {
                    await migrationManager.RollbackMigrationAsync(migration);
                }

                Console.WriteLine("Migrations rolled back successfully.");
                return;
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            await app.RunAsync();
        }
    }
}
