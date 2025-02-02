using CRM.CoreService.Application.Interfaces.Repositories;
using CRM.CoreService.Domain.Entities;
using CRM.CoreService.Infrastructure.Contexts;
using CRM.CoreService.Infrastructure.Repositories;

namespace CRM.CoreService.Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PostgresDbContext _dbContext;
        private IGenericRepository<UserEntity>? _userRepository;
        private GenericRepository<ReportEntity>? _reportRepository;

        public UnitOfWork(PostgresDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<UserEntity> UserRepository =>
            _userRepository ??= new GenericRepository<UserEntity>(_dbContext);

        public IGenericRepository<ReportEntity> ReportRepository =>
            _reportRepository ??= new GenericRepository<ReportEntity>(_dbContext);

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
