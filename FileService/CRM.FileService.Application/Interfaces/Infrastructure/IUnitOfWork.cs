using CRM.FileService.Domain.Entities;

namespace CRM.FileService.Application.Interfaces.Infrastructure
{
    public interface IUnitOfWork
    {
        IGenericRepository<FileEntity> FileRepository { get; }
        void Dispose();
        Task<int> SaveChangesAsync();
    }
}