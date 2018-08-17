using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using PdfReportingPoc.Elements.Fragments;

namespace PdfReportingPoc.Elements
{
    public class Footer
    {
        public List<FooterFragment> TextFragements { get; set; }

        public bool InsertPageNumbers { get; set; }

        public byte[] Render(byte[] report)
        {
            var reportBody = TextFragements.Aggregate(report, (current, fragement) => fragement.Render(current));

            return InsertPageNumbers ? PlacePageNumbers(reportBody) : reportBody;
        }

        private byte[] PlacePageNumbers(byte[] reportBody)
        {
            using (var incomingStream = new MemoryStream(reportBody))
            {
                using (var document = new Document(incomingStream))
                {
                    var pageCounter = 1;
                    foreach (var page in document.Pages)
                    {
                        var pageNumberStamp = GetPageNumber(pageCounter);
                        page.AddStamp(pageNumberStamp);
                        pageCounter++;
                    }

                    return ExtractBytes(document);
                }
            }
        }

        private static byte[] ExtractBytes(Document document)
        {
            byte[] documentBytes;
            using (var documentStream = new MemoryStream())
            {
                document.Save(documentStream);
                documentBytes = documentStream.ToArray();
            }
            return documentBytes;
        }

        private PageNumberStamp GetPageNumber(int pageNumber)
        {
            var pageNumberStamp = new PageNumberStamp
            {
                Format = $"Page {pageNumber}",
                BottomMargin = 10,
                RightMargin = 15,
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                StartingNumber = 1
            };
            pageNumberStamp.TextState.ForegroundColor = Color.Gray;
            return pageNumberStamp;
        }

    }
}
