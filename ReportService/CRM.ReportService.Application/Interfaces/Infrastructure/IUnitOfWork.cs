namespace CRM.ReportService.Application.Interfaces.Infrastructure
{
    public interface IUnitOfWork
    {
        IReportRepository ReportRepository { get; }

        Task SaveChangesAsync();
    }
}