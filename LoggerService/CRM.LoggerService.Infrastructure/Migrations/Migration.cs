using CRM.LoggerService.Infrastructure.Data.Context;
using MongoDB.Driver;

namespace CRM.LoggerService.Infrastructure.Migrations
{
    public abstract class Migration
    {
        public abstract string Version { get; }
        public abstract Task ApplyAsync(LoggerDbContext context);
        public abstract Task RollbackAsync(LoggerDbContext context);
    }

}
