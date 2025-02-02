using CRM.LoggerService.Domain.Entities;
using MongoDB.Bson;

namespace CRM.LoggerService.Infrastructure.Repositories
{
    public interface IGraphQLLogRepository
    {
        Task CreateAsync(GraphQLLog graphQLLog);
        Task DeleteAsync(ObjectId id);
        Task<IEnumerable<GraphQLLog>> GetAllAsync();
        Task<GraphQLLog> GetByIdAsync(ObjectId id);
        Task<IEnumerable<GraphQLLog>> GetByUserIdAsync(string userId);
        Task UpdateAsync(GraphQLLog graphQLLog);
    }
}