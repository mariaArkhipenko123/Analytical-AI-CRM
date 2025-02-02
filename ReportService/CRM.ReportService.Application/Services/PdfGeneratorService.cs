using CRM.ReportService.Application.Interfaces.Application;
using CRM.ReportService.Application.Models.Input;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CRM.ReportService.Application.Services
{
    public class PdfGeneratorService : IFileGenerator
    {
        public string GenerateFile(AIServiceInputModel input, Guid fileId)
        {
            string filePath = Path.Combine("/files", fileId.ToString() + ".pdf");
            Directory.CreateDirectory("/files");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                Document doc = new Document();
                PdfWriter.GetInstance(doc, stream);
                doc.Open();
                doc.Add(new Paragraph(input.Content));
                doc.Close();
            }

            return filePath;
        }
    }
}
