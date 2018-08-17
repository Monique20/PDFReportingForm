using System.Collections.Generic;
using Aspose.Pdf;
using PdfReportingPoc.Domain.Elements;

namespace PdfReportingPoc.Elements
{
    public class Row : IRow
    {
        public List<ICell> Cells { get; set; }

        public void Render(Aspose.Pdf.Table table)
        {
            var row = table.Rows.Add();

            foreach (var cell in Cells)
            {
                cell.Render(row);
            }
        }

        public void Format(Document pdfDocument)
        {
            foreach (var cell in Cells)
            {
                cell.Format(pdfDocument);
            }
        }
      
    }
}