using Aspose.Pdf;
using Aspose.Pdf.Text;
using PdfReportingPoc.Domain.Elements;
using PdfReportingPoc.Domain.Elements.Cell;

namespace PdfReportingPoc.Elements.Cells
{
    public class Text : ICell
    {
        public TextProperties TextProperties { get; set; }

        public string DisplayText { get; set; }

        public Text()
        {
            TextProperties = new TextProperties
            {
                FontSize = 12,
                FontType = "Arial",
                FontStyle = FontStyles.Regular,
                FontColor = Color.Black,
                Invisible = false
            };
        }

        public void Render(Aspose.Pdf.Row row)
        {
            var cell = row.Cells.Add();

            cell.Paragraphs.Add(new TextFragment
            {
                Text = DisplayText,
                TextState =
                {
                    FontSize = TextProperties.FontSize,
                    Font = FontRepository.FindFont(TextProperties.FontType),
                    FontStyle = TextProperties.FontStyle,
                    ForegroundColor = TextProperties.FontColor,
                    Invisible = TextProperties.Invisible
                },
            });
        }

        public void Format(Document pdfDocument)
        {
        }
    }
}