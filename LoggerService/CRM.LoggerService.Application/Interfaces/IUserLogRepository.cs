using CRM.LoggerService.Domain.Entities;
using MongoDB.Bson;

namespace CRM.LoggerService.Infrastructure.Repositories
{
    public interface IUserLogRepository
    {
        Task CreateAsync(UserLog userLog);
        Task DeleteAsync(ObjectId id);
        Task<IEnumerable<UserLog>> GetAllAsync();
        Task<UserLog> GetByIdAsync(ObjectId id);
        Task UpdateAsync(UserLog userLog);
    }
}