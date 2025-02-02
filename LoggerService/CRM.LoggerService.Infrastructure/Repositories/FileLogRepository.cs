using CRM.LoggerService.Domain.Entities;
using CRM.LoggerService.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CRM.LoggerService.Infrastructure.Repositories
{
    public class FileLogRepository : IFileLogRepository
    {
        private readonly LoggerDbContext _context;

        public FileLogRepository(LoggerDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(FileLog fileLog)
        {
            await _context.FileLogs.AddAsync(fileLog);
            await _context.SaveChangesAsync();
        }

        public async Task<FileLog> GetByIdAsync(ObjectId id)
        {
            return await _context.FileLogs.FindAsync(id);
        }

        public async Task<IEnumerable<FileLog>> GetAllAsync()
        {
            return await _context.FileLogs.ToListAsync();
        }

        public async Task<IEnumerable<FileLog>> GetByReportIdAsync(ObjectId reportId)
        {
            return await _context.FileLogs.Where(log => log.ReportId == reportId).ToListAsync();
        }

        public async Task UpdateAsync(FileLog fileLog)
        {
            _context.FileLogs.Update(fileLog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ObjectId id)
        {
            var fileLog = await _context.FileLogs.FindAsync(id);
            if (fileLog != null)
            {
                _context.FileLogs.Remove(fileLog);
                await _context.SaveChangesAsync();
            }
        }
    }
}
