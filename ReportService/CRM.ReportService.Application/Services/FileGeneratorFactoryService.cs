using CRM.ReportService.Application.Interfaces.Application;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.ReportService.Application.Services
{
    public class FileGeneratorFactoryService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, Type> _generators = new Dictionary<string, Type>
        {
            { "pdf", typeof(PdfGeneratorService) },
            { "excel", typeof(ExcelGeneratorService) }
        };

        public FileGeneratorFactoryService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IFileGenerator CreateGenerator(string fileType)
        {
            if (_generators.TryGetValue(fileType.ToLower(), out var generatorType))
            {
                return (IFileGenerator)_serviceProvider.GetRequiredService(generatorType);
            }

            throw new ArgumentException("Unsupported file type");
        }
    }
}
