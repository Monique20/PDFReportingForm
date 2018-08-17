using Aspose.Pdf;
using Aspose.Pdf.Forms;
using PdfReportingPoc.Domain.Elements;
using PdfReportingPoc.Domain.Elements.Cells;

namespace PdfReportingPoc.Elements.Cells
{
    public class Checkbox : ICell
    {
        public CheckboxProperites CheckboxProperites { get; set; }

        public Checkbox() {
            CheckboxProperites = new CheckboxProperites
            {
                Id = "defaultCheckBox",
                Height = 20,
                Width = 20,
                Required = true
            };
        }

        public void Render(Aspose.Pdf.Row row)
        {
            var cell = row.Cells.Add();
            cell.Paragraphs.Add(new CheckboxField
            {
                Width = CheckboxProperites.Width,
                Height = CheckboxProperites.Height,
                Name = CheckboxProperites.Id,                
            });
        }

        public void Format(Document pdfDocument)
        {
        }
    }
}