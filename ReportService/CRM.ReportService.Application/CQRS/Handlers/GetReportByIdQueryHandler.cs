using CRM.ReportService.Application.CQRS.Queries;
using CRM.ReportService.Application.Interfaces.Infrastructure;
using CRM.ReportService.Domain.Entities;
using MediatR;

namespace CRM.ReportService.Application.CQRS.Handlers
{
    public class GetReportByIdQueryHandler : IRequestHandler<GetReportByIdQuery, Report>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetReportByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Report> Handle(GetReportByIdQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.ReportRepository.GetByIdAsync(request.ReportId);
        }
    }
}
