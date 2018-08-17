using Aspose.Pdf;
using Aspose.Pdf.Forms;

namespace PdfReportingPoc.Domain.Elements.Cell
{
    public class OptionItemProperties
    {
        public BoxStyle Style { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }

        private RadioButtonField _fieldGroup;

        public RadioButtonField GetFieldGroup(Document pdfDocument)
        {
            if (_fieldGroup == null)
            {
                _fieldGroup = new RadioButtonField(pdfDocument);
            }

            return _fieldGroup;
        }
    }
}
