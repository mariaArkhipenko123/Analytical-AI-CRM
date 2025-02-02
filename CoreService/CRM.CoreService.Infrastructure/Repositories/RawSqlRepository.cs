using CRM.CoreService.Application.Interfaces.Repositories;
using CRM.CoreService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CRM.CoreService.Infrastructure.Repositories
{
    public class RawSqlRepository : IRawSqlRepository
    {
        private readonly PostgresDbContext context;
        public RawSqlRepository(PostgresDbContext context)
        {
            this.context = context;
        }
        public async Task<List<T>> ExecuteQueryAsync<T>(string sql, object[] parameters = null) where T : class
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("SQL query cannot be null or empty", nameof(sql));

            return await context.Set<T>().FromSqlRaw(sql, parameters ?? Array.Empty<object>()).ToListAsync();
        }

        public async Task<int> ExecuteCommandAsync(string sql, object[] parameters = null)
        {
            if (string.IsNullOrWhiteSpace(sql))
                throw new ArgumentException("SQL query cannot be null or empty", nameof(sql));

            return await context.Database.ExecuteSqlRawAsync(sql, parameters ?? Array.Empty<object>());
        }
    }
}
