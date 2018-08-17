using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace PdfReportingPoc.Domain.Elements.Cell
{
    public class TextProperties
    {
        public float FontSize { get; set; }
        public string FontType { get; set; }
        public FontStyles FontStyle { get; set; }
        public Color FontColor { get; set; }
        public bool Invisible { get; set; }
    }
}
