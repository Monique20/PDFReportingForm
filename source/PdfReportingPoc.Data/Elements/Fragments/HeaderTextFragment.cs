using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using PdfReportingPoc.Domain.Elements.Fragements;
using Color = Aspose.Pdf.Color;
using Font = Aspose.Pdf.Text.Font;

namespace PdfReportingPoc.Elements.Fragments
{
    public class HeaderTextFragment : IFragement
    {
        public List<DocumentBoundryText> TextFragments { get; set; }
        public double TopMargin { get; set; }
        public HorizontalAlignment HorizontalAlignment { get; set; }
        public VerticalAlignment VerticalAlignment { get; set; }
        public Color Color { get; set; }
        public Font Font { get; set; }
        public float Size { get; set; }

        public HeaderTextFragment()
        {
            TextFragments = new List<DocumentBoundryText>();
            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Center;
            Color = Color.Black;
            Font = FontRepository.FindFont("Arial");
            Size = 10;
        }

        public byte[] Render(byte[] report)
        {
            using (var stream = new MemoryStream(report))
            {
                var document = new Document(stream);
                foreach (var page in document.Pages)
                {
                    var line = CombineFragementsIntoSingleLine();
                    var textStamp = CreateTextStamp(line);
                    page.AddStamp(textStamp);
                }

                return ExtractBytes(document);
            }  
        }

        private TextStamp CreateTextStamp(string line)
        {
            var textStamp = new TextStamp(line)
            {
                TopMargin = TopMargin,
                HorizontalAlignment = HorizontalAlignment,
                VerticalAlignment = VerticalAlignment,
            };
            textStamp.TextState.ForegroundColor = Color;
            textStamp.TextState.Font = Font;
            textStamp.TextState.FontSize = Size;
            return textStamp;
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
            foreach (var fragment in TextFragments)
            {
                result += fragment.ToString();
            }

            return result;
        }
    }
}
