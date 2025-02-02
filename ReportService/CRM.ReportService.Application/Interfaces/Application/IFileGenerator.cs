using CRM.ReportService.Application.Models.Input;

namespace CRM.ReportService.Application.Interfaces.Application
{
    public interface IFileGenerator
    {
        string GenerateFile(AIServiceInputModel input, Guid fileId);
    }
}
