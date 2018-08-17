using System.Collections.Generic;
using PdfReportingPoc.Domain.Elements;
using PdfReportingPoc.Domain.Elements.Fragements;
using PdfReportingPoc.Elements.Fragments;

namespace PdfReportingPoc.Elements
{
    public class Header : IHeader
    {
        public List<HeaderTextFragment> TextFragements { get; set; }
        public ILogo Logo { get; set; }

        public Header()
        {
            TextFragements = new List<HeaderTextFragment>();
        }

        public byte[] Render(byte[] report)
        {
            var reportBody = report;
            foreach (var fragement in TextFragements)
            {
                reportBody = fragement.Render(reportBody);
            }

            reportBody = Logo.Render(reportBody);

            return reportBody;
        }
    }
}
