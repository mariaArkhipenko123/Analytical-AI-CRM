using CRM.LoggerService.Domain.Entities;
using MongoDB.Bson;

namespace CRM.LoggerService.Infrastructure.Repositories
{
    public interface IReportLogRepository
    {
        Task CreateAsync(ReportLog reportLog);
        Task DeleteAsync(ObjectId id);
        Task<IEnumerable<ReportLog>> GetAllAsync();
        Task<ReportLog> GetByIdAsync(ObjectId id);
        Task<IEnumerable<ReportLog>> GetByRequestedByAsync(string requestedBy);
        Task UpdateAsync(ReportLog reportLog);
    }
}