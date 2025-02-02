using CRM.LoggerService.Domain.Migrations;
using CRM.LoggerService.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CRM.LoggerService.Infrastructure.Migrations
{
    public class MigrationManager 
    {
        private readonly IServiceProvider _serviceProvider;

        public MigrationManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ApplyMigrationAsync(Migration migration)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<LoggerDbContext>();

            var exists = await context.MigrationLogs
                .Where(m => m.Version == migration.Version && !m.RolledBack)
                .AnyAsync();

            if (exists) return;

            await migration.ApplyAsync(context);

            var log = new MigrationLog { Version = migration.Version };
            context.MigrationLogs.Add(log);
            await context.SaveChangesAsync();
        }

        public async Task RollbackMigrationAsync(Migration migration)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<LoggerDbContext>();

            var log = await context.MigrationLogs
                .Where(m => m.Version == migration.Version && !m.RolledBack)
                .FirstOrDefaultAsync();

            if (log == null) return;

            await migration.RollbackAsync(context);

            log.RolledBack = true;
            context.MigrationLogs.Update(log);
            await context.SaveChangesAsync();
        }
    }
}
