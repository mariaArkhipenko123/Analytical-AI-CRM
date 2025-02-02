using CRM.FileService.Application.Interfaces.Infrastructure;
using CRM.FileService.Domain.Entities;
using CRM.FileService.Infrastructure.Context;
using CRM.FileService.Infrastructure.Repositories;

namespace CRM.FileService.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly PostgresDbContext _dbContext;
        private IGenericRepository<FileEntity>? _fileRepository = null;
        public IGenericRepository<FileEntity> FileRepository => _fileRepository ??= new GenericRepository<FileEntity>(_dbContext);

        public UnitOfWork(PostgresDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> SaveChangesAsync()
        {
            int result;
            try
            {
                result = await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving data to the database.");
            }
            return result;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
