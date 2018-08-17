using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using PdfReportingPoc.BarCode;
using PdfReportingPoc.Domain.Elements;

namespace PdfReportingPoc.Elements.Cells
{
    public class BarCode : IDisposableCell
    {
        public string Text { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Title { get; set; }

        private Stream _imageStream;

        public BarCode()
        {
            Text = string.Empty;
            Height = 100;
            Width = 100;
        }

        public void Render(Aspose.Pdf.Row row)
        {
            var cell = row.Cells.Add();
            var bytes = new BarCodeGenerator()
                     .With_Text(Text)
                     .With_Default_Dimension()
                     .Of_Type_QR_Code()
                     .As_Png()
                     .Create();

            _imageStream = new MemoryStream(bytes);
            {
                var image = new Image
                {
                    ImageStream = _imageStream,
                    FixHeight = Height,
                    FixWidth = Width,
                    Title = new TextFragment(Text)
                };
                cell.Paragraphs.Add(image);
            }
        }

        public void Format(Document pdfDocument)
        {
        }

        public void Dispose()
        {
            _imageStream.Dispose(); // todo : do something with this in report when done rendering
        }
    }
}
