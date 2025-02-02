using CRM.ReportService.Application.Interfaces.Infrastructure;
using CRM.ReportService.Infrastructure.Contexts;
using CRM.ReportService.Infrastructure.Repositories;

namespace CRM.ReportService.Infrastructure.UofW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PostgresDbContext _dbContext;
        private IReportRepository _reportRepository;

        public UnitOfWork(PostgresDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IReportRepository ReportRepository =>
            _reportRepository ??= new ReportRepository(_dbContext);

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
