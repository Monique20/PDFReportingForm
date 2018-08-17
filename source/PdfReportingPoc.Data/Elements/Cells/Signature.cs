using Aspose.Pdf;
using Aspose.Pdf.Forms;
using PdfReportingPoc.Domain.Elements;
using PdfReportingPoc.Domain.Elements.Cell;

namespace PdfReportingPoc.Elements.Cells
{
    public class Signature : ICell
    {
        public SignatureProperties SignatureProperties { get; set; }

        public Signature()
        {
            SignatureProperties = new SignatureProperties
            {
                PartialName = "signature1",
                Height = 20,
                Width = 100
            };
        }

        public void Render(Aspose.Pdf.Row row)
        {
            var cell = row.Cells.Add();
            cell.Paragraphs.Add(new TextBoxField
            {
                PartialName = SignatureProperties.PartialName,
                Height = SignatureProperties.Height,
                Width = SignatureProperties.Width
            });
        }

        public void Format(Document pdfDocument)
        {
            var textboxId = SignatureProperties.PartialName;
            var textboxField = pdfDocument.Form[textboxId] as TextBoxField;
            var rectangle = textboxField.GetRectangle(false);

            var signatureField = new SignatureField(pdfDocument.Pages[textboxField.PageIndex], rectangle)
            {
                PartialName = SignatureProperties.PartialName,
                Width = SignatureProperties.Width,
                Height = SignatureProperties.Height
            };
            signatureField.Characteristics.Border = System.Drawing.Color.Black;
            pdfDocument.Form.Delete(textboxId);
            pdfDocument.Form.Add(signatureField);
        }
    }
}
