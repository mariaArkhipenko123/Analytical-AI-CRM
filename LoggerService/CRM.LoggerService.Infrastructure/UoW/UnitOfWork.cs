using CRM.LoggerService.Infrastructure.Data.Context;
using CRM.LoggerService.Infrastructure.Repositories;

namespace CRM.LoggerService.Infrastructure.UoW
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private readonly LoggerDbContext _context;

        private IReportLogRepository _reportLogRepository;
        private IGraphQLLogRepository _graphQLLogRepository;
        private IFileLogRepository _fileLogRepository;
        private IUserLogRepository _userLogRepository;

        public UnitOfWork(LoggerDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IReportLogRepository ReportLogs
            => _reportLogRepository ??= new ReportLogRepository(_context);

        public IGraphQLLogRepository GraphQLLogs
            => _graphQLLogRepository ??= new GraphQLLogRepository(_context);

        public IFileLogRepository FileLogs
            => _fileLogRepository ??= new FileLogRepository(_context);

        public IUserLogRepository UserLogs
            => _userLogRepository ??= new UserLogRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();            
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
