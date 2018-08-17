using System.Collections.Generic;
using Aspose.Pdf;
using PdfReportingPoc.Domain.Elements;

namespace PdfReportingPoc.Elements.Cells
{
    public class OptionGroup : ICell
    {
        public List<ICell> OptionCells { get; set; }

        public OptionGroup()
        {
            OptionCells = new List<ICell>();               
        }
       
        public void Render(Aspose.Pdf.Row row)
        {
            foreach (var cell in OptionCells)
            {
                cell.Render(row);
            }
        }

        public void Format(Document pdfDocument)
        {
            foreach (var cell in OptionCells)
            {
                cell.Format(pdfDocument);
            }
        }
    }
}