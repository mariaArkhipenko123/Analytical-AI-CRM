using CRM.LoggerService.Domain.Entities;
using MongoDB.Bson;

namespace CRM.LoggerService.Infrastructure.Repositories
{
    public interface IFileLogRepository
    {
        Task CreateAsync(FileLog fileLog);
        Task DeleteAsync(ObjectId id);
        Task<IEnumerable<FileLog>> GetAllAsync();
        Task<FileLog> GetByIdAsync(ObjectId id);
        Task<IEnumerable<FileLog>> GetByReportIdAsync(ObjectId reportId);
        Task UpdateAsync(FileLog fileLog);
    }
}