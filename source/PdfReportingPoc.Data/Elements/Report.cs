using System.Collections.Generic;
using System.Linq;
using PdfReportingPoc.Domain.Elements;
using PdfReportingPoc.Operations;

namespace PdfReportingPoc.Elements
{
    public class Report : IReport
    {
        public Header Header { get; set; }
        public Footer Footer { get; set; }
        public List<ISection> Sections { get; set; }
        public string Password { get; set; }

        public Report()
        {
            Sections = new List<ISection>(); // todo : write test
        }

        public byte[] Render(byte[] reportBytes) // todo : write test
        {
            var passwordProtector = new Password();
            var tableRenderData = new TableRenderData
            {
                PdfBytes = reportBytes,
                TableHeight = 0
            };

            tableRenderData = Sections.Aggregate(tableRenderData, (current, section) => section.Render(current));
            // todo : call table.Dispose to clean up any resource left over
            var withFooterPdfBytes = Footer.Render(tableRenderData.PdfBytes);
            var withHeaderPdfBytes = Header.Render(withFooterPdfBytes);
            var withPasswordPdfBytes = passwordProtector.PasswordProtect(withHeaderPdfBytes, Password);

            return withPasswordPdfBytes;
        }
    }
}
