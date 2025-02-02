using CRM.CoreService.Domain.Entities;

namespace CRM.CoreService.Application.Interfaces.Repositories

{
    public interface IUnitOfWork
    {
        IGenericRepository<UserEntity> UserRepository { get; }
        IGenericRepository<ReportEntity> ReportRepository { get; }
        Task SaveChangesAsync();
    }
}