using Aspose.Pdf;
using PdfReportingPoc.Domain.Elements;

namespace PdfReportingPoc.Elements
{
    public class TableLayout
    {
        public string Widths { get; set; }
        public float Left { get; set; }
        public float Top { get; set; }
        public BorderInfo CellBorder { get; set; }
        public MarginInfo CellMargin { get; set; }
        public ColumnAdjustment ColumnAdjustment { get; set; }
        public TablePosition Position { get; set; }
    }
}
