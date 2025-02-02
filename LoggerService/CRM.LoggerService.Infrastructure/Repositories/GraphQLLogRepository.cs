using CRM.LoggerService.Domain.Entities;
using CRM.LoggerService.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CRM.LoggerService.Infrastructure.Repositories
{
    public class GraphQLLogRepository : IGraphQLLogRepository
    {
        private readonly LoggerDbContext _context;

        public GraphQLLogRepository(LoggerDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(GraphQLLog graphQLLog)
        {
            await _context.GraphQLLogs.AddAsync(graphQLLog);
            await _context.SaveChangesAsync();
        }

        public async Task<GraphQLLog> GetByIdAsync(ObjectId id)
        {
            return await _context.GraphQLLogs.FindAsync(id);
        }

        public async Task<IEnumerable<GraphQLLog>> GetAllAsync()
        {
            return await _context.GraphQLLogs.ToListAsync();
        }

        public async Task<IEnumerable<GraphQLLog>> GetByUserIdAsync(string userId)
        {
            return await _context.GraphQLLogs.Where(log => log.UserId == userId).ToListAsync();
        }

        public async Task UpdateAsync(GraphQLLog graphQLLog)
        {
            _context.GraphQLLogs.Update(graphQLLog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ObjectId id)
        {
            var graphQLLog = await _context.GraphQLLogs.FindAsync(id);
            if (graphQLLog != null)
            {
                _context.GraphQLLogs.Remove(graphQLLog);
                await _context.SaveChangesAsync();
            }
        }
    }
}
