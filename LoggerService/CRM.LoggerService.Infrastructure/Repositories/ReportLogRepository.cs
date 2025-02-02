using CRM.LoggerService.Domain.Entities;
using CRM.LoggerService.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CRM.LoggerService.Infrastructure.Repositories
{
    public class ReportLogRepository : IReportLogRepository
    {
        private readonly LoggerDbContext _context;

        public ReportLogRepository(LoggerDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ReportLog reportLog)
        {
            await _context.ReportLogs.AddAsync(reportLog);
            await _context.SaveChangesAsync();
        }

        public async Task<ReportLog> GetByIdAsync(ObjectId id)
        {
            return await _context.ReportLogs.FindAsync(id);
        }

        public async Task<IEnumerable<ReportLog>> GetAllAsync()
        {
            return await _context.ReportLogs.ToListAsync();
        }

        public async Task<IEnumerable<ReportLog>> GetByRequestedByAsync(string requestedBy)
        {
            return await _context.ReportLogs.Where(log => log.RequestedBy == requestedBy).ToListAsync();
        }

        public async Task UpdateAsync(ReportLog reportLog)
        {
            _context.ReportLogs.Update(reportLog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ObjectId id)
        {
            var reportLog = await _context.ReportLogs.FindAsync(id);
            if (reportLog != null)
            {
                _context.ReportLogs.Remove(reportLog);
                await _context.SaveChangesAsync();
            }
        }
    }
}
