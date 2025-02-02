using CRM.ReportService.Application.Interfaces.Application;
using CRM.ReportService.Application.Models.Input;
using OfficeOpenXml;

namespace CRM.ReportService.Application.Services
{
    public class ExcelGeneratorService : IFileGenerator
    {
        public string GenerateFile(AIServiceInputModel input, Guid fileId)
        {
            string filePath = Path.Combine("/files", fileId.ToString() + ".xlsx");
            Directory.CreateDirectory("/files");

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Report");
                worksheet.Cells[1, 1].Value = input.Content;
                File.WriteAllBytes(filePath, package.GetAsByteArray());
            }

            return filePath;
        }
    }
}
