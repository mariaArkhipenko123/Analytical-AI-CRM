using CRM.ReportService.Application.CQRS.Commands;
using CRM.ReportService.Application.Interfaces.Infrastructure;
using CRM.ReportService.Application.Models.Output;
using CRM.ReportService.Application.Services;
using CRM.ReportService.Domain.Entities;
using MediatR;

namespace CRM.ReportService.Application.CQRS.Handlers
{
    public class ProcessReportCommandHandler : IRequestHandler<ProcessReportCommand, Guid>
    {
        private readonly FileGeneratorFactoryService _fileGeneratorFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;
        private readonly IRedisMessageBroker _redisBroker;

        public ProcessReportCommandHandler(
            FileGeneratorFactoryService fileGeneratorFactory,
            IUnitOfWork unitOfWork,
            ICacheService cacheService,
            IRedisMessageBroker redisBroker)
        {
            _fileGeneratorFactory = fileGeneratorFactory;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
            _redisBroker = redisBroker;
        }

        public async Task<Guid> Handle(ProcessReportCommand request, CancellationToken cancellationToken)
        {
            var input = request.Input;
            var reportId = Guid.NewGuid();
            var fileId = Guid.NewGuid();
            var createdAt = DateTime.UtcNow;

            var generator = _fileGeneratorFactory.CreateGenerator(input.FileType);
            string filePath = generator.GenerateFile(input, fileId);

            var report = new Report
            {
                Id = reportId,
                UserId = input.UserId,
                Type = input.FileType,
                Status = "Generated",
                FileId = fileId,
                CreatedAt = createdAt
            };

            await _unitOfWork.ReportRepository.AddAsync(report);
            await _unitOfWork.SaveChangesAsync();

            await _cacheService.SetAsync(reportId.ToString(), report, TimeSpan.FromHours(1), TimeSpan.FromDays(1));

            var fileContent = Convert.ToBase64String(File.ReadAllBytes(filePath));

            var output = new FileServiceOutputModel
            {
                UserId = input.UserId,
                FileType = input.FileType,
                FileName = Path.GetFileName(filePath),
                FileContent = fileContent,
                FileId = fileId,
                CreatedAt = createdAt
            };

            await _redisBroker.WriteMessageToStreamAsync(
                "FileServiceStream",
                new Dictionary<string, string> { { "Task", "SaveFile" } },
                output
            );

            return reportId;
        }
    }
}
