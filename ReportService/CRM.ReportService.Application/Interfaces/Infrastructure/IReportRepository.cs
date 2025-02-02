using CRM.ReportService.Domain.Entities;

namespace CRM.ReportService.Application.Interfaces.Infrastructure
{
    public interface IReportRepository
    {
        Task AddAsync(Report report);
        Task<Report> GetByIdAsync(Guid id);
        Task<List<Report>> GetByUserIdAsync(Guid userId);
        Task UpdateAsync(Report report);
    }
}