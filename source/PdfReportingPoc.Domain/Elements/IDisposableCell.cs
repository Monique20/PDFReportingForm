using System;
using Aspose.Pdf;

namespace PdfReportingPoc.Domain.Elements
{
    public interface IDisposableCell : ICell, IDisposable
    {
        void Render(Row row);
        void Format(Document pdfDocument);
    } 
}
