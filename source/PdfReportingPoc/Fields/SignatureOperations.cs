using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;
using PdfReportingPoc.Domain.Pdf;

namespace PdfReportingPoc.Fields
{
    public class SignatureOperations
    {
        public SignatureOperations()
        {
            var license = new License();
            license.SetLicense("Aspose.Pdf.Lic");
        }

        public PdfFieldsOperationsResponse InsertSignatureField(byte[] template, string dataDir, string fileName)
        {
            var documentBytes = new byte[0];
            var results = new PdfFieldsOperationsResponse();

            using (var incomingStream = new MemoryStream(template))
            {
                var pdfDocument = new Document(incomingStream);              
                Page page = pdfDocument.Pages[1];
                CreateTableAndInsertValues(page);
                documentBytes = Save(pdfDocument);  // Save document with table

                //Insert signature field
                //Save document and return bytes
            }
            results.ErrorMessage = string.Empty;
            results.Output = documentBytes;
            return results;
        }

        private static void CreateTableAndInsertValues(Page page)
        {
            Table table = new Table();
            table.Margin.Top = 100;
            page.Paragraphs.Add(table);
            table.ColumnWidths = "100 100 100";
            table.DefaultCellBorder = new BorderInfo(BorderSide.All, 0.1F);
            table.Border = new BorderInfo(BorderSide.All, 1F);

            Row row1 = table.Rows.Add();
            row1.Cells.Add("Name");
            row1.Cells.Add("ID No");
            row1.Cells.Add("Signature");
            for (int RowCounter = 0; RowCounter < 1; RowCounter++)
            {
                row1 = table.Rows.Add();
                row1.Cells.Add("Mcebisi");
                row1.Cells.Add("2125487655687");
                row1.Cells.Add("");
            }
        }

        private static byte[] Save(Document pdfDocument)
        {
            byte[] documentBytes;
            using (var documentStream = new MemoryStream())
            {
                pdfDocument.Save(documentStream);
                documentBytes = documentStream.ToArray();
                Document doc = CreateSignature(documentStream);
                doc.Save(documentStream);
                documentBytes = documentStream.ToArray();
            }

            return documentBytes;
        }

        private static Document CreateSignature(MemoryStream documentStream)
        {
            var doc = new Document(documentStream);
            var absorber = new TableAbsorber();
            absorber.Visit(doc.Pages[1]);
            var fragment = absorber.TableList[1].RowList[1].CellList[2].TextFragments[1];
            var urx = absorber.TableList[1].RowList[1].CellList[2].Rectangle.URX;
            var ury = absorber.TableList[1].RowList[1].CellList[2].Rectangle.URY;
            var x = fragment.Position.XIndent;
            var y = fragment.Position.YIndent;
            var formEditor = new FormEditor();
            formEditor.BindPdf(doc);
            formEditor.AddField(FieldType.Signature, "signatureField", 1, (float)x, (float)y, (float)urx, (float)ury);
            return doc;
        }
    }
}
