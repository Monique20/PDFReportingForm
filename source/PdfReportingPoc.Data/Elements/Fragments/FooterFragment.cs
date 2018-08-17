using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using PdfReportingPoc.Domain.Elements.Fragements;

namespace PdfReportingPoc.Elements.Fragments
{
    public class FooterFragment : IFragement
    {
        public List<DocumentBoundryText> Fragments { get; set; }

        public double BottomMargin { get; set; }
        public double LeftMargin { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }
        public Color Color { get; set; }

        public FooterFragment()
        {
            Fragments = new List<DocumentBoundryText>();
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Center;
            Color = Color.Black;
        }

        public byte[] Render(byte[] report)
        {
            using (var stream = new MemoryStream(report))
            {
                var document = new Document(stream);
                foreach (var page in document.Pages)
                {
                    var line = CombineFragementsIntoSingleLine();
                    var textStamp = new TextStamp(line)
                    {
                        BottomMargin = BottomMargin,
                        LeftMargin = LeftMargin,
                        HorizontalAlignment = HorizontalAlignment,
                        VerticalAlignment = VerticalAlignment,
                        
                    };
                    textStamp.TextState.ForegroundColor = Color;
                    page.AddStamp(textStamp);
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

        private string CombineFragementsIntoSingleLine()
        {
            var result = string.Empty;
            foreach (var fragment in Fragments)
            {
                result += fragment.ToString();
            }

            return result;
        }
    }
}
