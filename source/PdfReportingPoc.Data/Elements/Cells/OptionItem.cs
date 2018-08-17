using Aspose.Pdf;
using Aspose.Pdf.Forms;
using PdfReportingPoc.Domain.Elements;
using PdfReportingPoc.Domain.Elements.Cell;

namespace PdfReportingPoc.Elements.Cells
{
    public class OptionItem : ICell
    {
        public string OptionName { get; set; }
        public string Id { get; set; }

        public OptionItemProperties OptionItemProperties { get; set; }


        public OptionItem()
        {
            var license = new License();
            license.SetLicense("Aspose.Pdf.Lic");

            OptionItemProperties = new OptionItemProperties
            {
                Height = 20,
                Width = 20,
                Style = BoxStyle.Check
            };
        }

        public void Render(Aspose.Pdf.Row row)
        {
            var cell = row.Cells.Add();
            cell.Paragraphs.Add(new TextBoxField
            {
                PartialName = Id,
                Height = OptionItemProperties.Height,
                Width = OptionItemProperties.Width
            });
        }

        public void Format(Document pdfDocument)
        {
            var radioButton = OptionItemProperties.GetFieldGroup(pdfDocument);

            var textboxId = Id;
            var textboxField = pdfDocument.Form[textboxId] as TextBoxField;
            var pageNumber = textboxField.PageIndex;

            var rectangle = textboxField?.GetRectangle(false);

            var option = new RadioButtonOptionField(pdfDocument.Pages[pageNumber], rectangle)
            {
                
                OptionName = OptionName,
                Style = OptionItemProperties.Style
            };
            option.Characteristics.Border = System.Drawing.Color.Black;
            radioButton.Add(option);
            pdfDocument.Form.Delete(textboxId);
            pdfDocument.Form.Add(radioButton);
        }
    }
}