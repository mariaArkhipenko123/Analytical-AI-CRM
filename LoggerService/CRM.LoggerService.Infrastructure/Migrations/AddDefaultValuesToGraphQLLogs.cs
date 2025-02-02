using CRM.LoggerService.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace CRM.LoggerService.Infrastructure.Migrations
{
    public class AddDefaultValuesToGraphQLLogs : Migration
    {
        public override string Version => "2025_01_14_AddRequestOrigin";
        public override async Task ApplyAsync(LoggerDbContext context)
        {
                var logs = await context.GraphQLLogs
                    .Where(log => log.RequestOrigin == null)
                    .ToListAsync();

                foreach (var log in logs)
                {
                    log.RequestOrigin = "TESTUnknown";
                }

                context.GraphQLLogs.UpdateRange(logs);
                await context.SaveChangesAsync();
        }

        public override async Task RollbackAsync(LoggerDbContext context)
        {
                var logs = await context.GraphQLLogs
                    .Where(log => log.RequestOrigin == "TESTUnknown")
                    .ToListAsync();

                // Remove the RequestOrigins field from each document
                foreach (var log in logs)
                {
                    context.Entry(log).Property("RequestOriginso").CurrentValue = null;
                }

                await context.SaveChangesAsync();
        }
    }
}
