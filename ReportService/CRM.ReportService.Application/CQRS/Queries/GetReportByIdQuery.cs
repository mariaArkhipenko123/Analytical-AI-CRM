using CRM.ReportService.Domain.Entities;
using MediatR;

namespace CRM.ReportService.Application.CQRS.Queries
{
    public class GetReportByIdQuery : IRequest<Report>
    {
        public Guid ReportId { get; }

        public GetReportByIdQuery(Guid reportId)
        {
            ReportId = reportId;
        }
    }
}
