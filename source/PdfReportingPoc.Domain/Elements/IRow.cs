using Aspose.Pdf;

namespace PdfReportingPoc.Domain.Elements
{
    public interface IRow
    {
        void Render(Table layout);
        void Format(Document pdfDocument);
    }
}
