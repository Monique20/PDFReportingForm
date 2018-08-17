using System.IO;
using Aspose.Pdf;
using PdfReportingPoc.Domain.Elements.Fragements;

namespace PdfReportingPoc.Elements.Fragments
{
    public class TopLeftLogoFragement : ILogo
    {
        public byte[] Image { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int TopMargin { get; set; }
        public int RightMargin { get; set; }

        public byte[] Render(byte[] report)
        {
            if (Image == null) return report;

            using (var stream = new MemoryStream(report))
            {
                var document = new Document(stream);
                foreach (var page in document.Pages)
                {
                    using (var logoStream = new MemoryStream(Image))
                    {
                        var imageStamp = new ImageStamp(logoStream)
                        {
                            Height = Height,
                            Width = Width,
                            TopMargin = TopMargin,
                            RightMargin = RightMargin,
                            HorizontalAlignment = HorizontalAlignment.Right,
                            VerticalAlignment = VerticalAlignment.Top
                        };

                        page.AddStamp(imageStamp);
                    }
                }

                return ExtractBytes(document);
            }
        }

        private byte[] ExtractBytes(Document pdfDocument)
        {
            byte[] documentBytes;
            using (var documentStream = new MemoryStream())
            {
                pdfDocument.Save(documentStream);
                documentBytes = documentStream.ToArray();
            }
            return documentBytes;
        }
    }
}
