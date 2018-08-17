using System;
using Aspose.Pdf;

namespace PdfReportingPoc.Domain.Elements
{
    public interface ICell
    {
        void Render(Row row);
        void Format(Document pdfDocument);
    } 
}
