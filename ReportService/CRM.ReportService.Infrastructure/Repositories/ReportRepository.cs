using CRM.ReportService.Application.Interfaces.Infrastructure;
using CRM.ReportService.Domain.Entities;
using CRM.ReportService.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CRM.ReportService.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly PostgresDbContext _context;

        public ReportRepository(PostgresDbContext context)
        {
            _context = context;
        }

        public async Task<Report> GetByIdAsync(Guid id)
        {
            return await _context.Set<Report>().FindAsync(id);
        }

        public async Task<List<Report>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Set<Report>().Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task AddAsync(Report report)
        {
            await _context.Set<Report>().AddAsync(report);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Report report)
        {
            _context.Set<Report>().Update(report);
            await _context.SaveChangesAsync();
        }
    }
}
