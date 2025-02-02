using CRM.LoggerService.Infrastructure.Repositories;

namespace CRM.LoggerService.Infrastructure.UoW
{
    public interface IUnitOfWork
    {
        IFileLogRepository FileLogs { get; }
        IGraphQLLogRepository GraphQLLogs { get; }
        IReportLogRepository ReportLogs { get; }
        IUserLogRepository UserLogs { get; }
        Task<int> CommitAsync();
        void Dispose();
    }
}