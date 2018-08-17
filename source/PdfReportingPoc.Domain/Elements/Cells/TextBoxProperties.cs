using PdfReportingPoc.Domain.Elements.Cell;

namespace PdfReportingPoc.Domain.Elements.Cells
{
    public class TextBoxProperties
    {
        public double Height { get; set; }
        public double Width { get; set; }
        public int MaxLen { get; set; }
        public bool Required { get; set; }
        public bool ForceComb { get; set; }
        public bool ReadOnly { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string Formular { get; set; }
        public bool Calculatable { get; set; }
        public NumberFormatProperties NumberFormatProperties { get; set; }
    }
}
