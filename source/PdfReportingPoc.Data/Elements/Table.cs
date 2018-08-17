using System.Collections.Generic;
using Aspose.Pdf;
using PdfReportingPoc.Domain.Elements;
using System.IO;

namespace PdfReportingPoc.Elements
{
    public class Table : ITable
    {
        public List<IRow> Rows { get; set; }
        public TableLayout Layout { get; set; }

        public Table()
        {
            var license = new License();
            license.SetLicense("Aspose.Pdf.Lic");

            Layout = new TableLayout
            {
                CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                Top = 10,
                Position = TablePosition.Absolute,
                Left = 0,
                ColumnAdjustment = ColumnAdjustment.AutoFitToWindow
            };
        }

        public TableRenderData Render(TableRenderData input)
        {
            var pageBreakTopMargin = 10;

            if (Rows.Count == 0)
            {
                return new TableRenderData
                {
                    PdfBytes = input.PdfBytes,
                    TableHeight = 0
                };
            }

            using (var incomingStream = new MemoryStream(input.PdfBytes))
            {
                using (var pdfDocument = new Document(incomingStream))
                {
                    var lastPage = pdfDocument.Pages.Count;

                    var table = CreateTable(input.TableHeight);
                    foreach (var row in Rows)
                    {
                        row.Render(table);
                    }
                    var projectedHeight = input.TableHeight + table.GetHeight();
                    var pageHeight = pdfDocument.Pages[1].PageInfo.Height;

                    if (WillTableBreakOntoNextPage(projectedHeight, pageHeight))
                    {
                        pdfDocument.Pages.Add();
                        lastPage += 1;
                        table.Margin.Top = pageBreakTopMargin;
                    }

                    pdfDocument.Pages[lastPage].Paragraphs.Add(table);

                    var documentBytes = Save(pdfDocument);
                    var newDocumentBytes = Format(documentBytes);

                    return new TableRenderData
                    {
                        PdfBytes = newDocumentBytes,
                        TableHeight = table.GetHeight()
                    };
                }
            }
        }

        private bool WillTableBreakOntoNextPage(double projectedHeight, double pageHeight)
        {
            var heightCorrectionFactor = 480;
            var adjustPageHeight = pageHeight + heightCorrectionFactor;
            return projectedHeight > adjustPageHeight;
        }

        private byte[] Save(Document pdfDocument)
        {
            byte[] documentBytes;
            using (var documentStream = new MemoryStream())
            {
                pdfDocument.Save(documentStream);
                documentBytes = documentStream.ToArray();
            }
            return documentBytes;
        }

        private byte[] Format(byte[] documentBytes)
        {
            byte[] newDocumentBytes;
            using (var newStream = new MemoryStream(documentBytes))
            {
                var newPdfDocument = new Document(newStream);

                foreach (var row in Rows)
                {
                    row.Format(newPdfDocument);
                }
                newDocumentBytes = Save(newPdfDocument);
            }
            return newDocumentBytes;
        }

        private Aspose.Pdf.Table CreateTable(double previousTableHeight)
        {
            var topMargin = Layout.Top;
            if (Layout.Position == TablePosition.Relative)
            {
                topMargin = (float)previousTableHeight + Layout.Top;
            }
            var result = new Aspose.Pdf.Table
            {
                ColumnAdjustment = Layout.ColumnAdjustment,
                ColumnWidths = Layout.Widths,
                DefaultCellPadding = Layout.CellMargin,
                DefaultCellBorder = Layout.CellBorder,
                Margin =
                {
                    Top =  topMargin,
                    Left = Layout.Left
                }
            };

            return result;
        }
    }
}
