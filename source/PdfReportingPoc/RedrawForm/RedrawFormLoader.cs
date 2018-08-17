using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using PdfReportingPoc.Domain.RedrawForm;

namespace PdfReportingPoc.RedrawForm
{
    public class RedrawFormLoader : IRedrawFormLoader
    {

        public RedrawFormLoader()
        {
            var license = new License();
            license.SetLicense("Aspose.Pdf.Lic");
        }

        public byte[] LoadWithHocSection(byte[] file)
        {
            using (var stream = new MemoryStream(file))
            {
                var document = new Document(stream);

                var text = "<h2>BUILDING INSURANCE</h2>" +
                    "<p>" +
                    "We require proof of building insurance (Home Owners Cover). Alternatively SA Home Loans can provide Bond Protection." +
                    "</p>" +
                    "<p>" +
                    "Select: <select>" +
                    "</select>" +
                    "</p>";

                var fragment = new HtmlFragment(text);

                var margin = new MarginInfo { Top = 220 };

                fragment.Margin = margin;

                document.Pages[1].Paragraphs.Add(fragment);

                using (var stream2 = new MemoryStream())
                {
                    document.Save(stream2);
                    var doc = new Document(stream2);
                    var element = (ComboBoxField)doc.Form["field_1"];
                    element.AddOption("I already have building insurance (Home Owners Cover)");
                    element.AddOption("I want to apply for Home Owners Cover with SA Home Loans");
                    element.Border.Width = 1;
                    element.Required = true;
                    element.Height = 22;
                    element.Width = 400;
                    return stream2.ToArray();
                }
            }

        }
    }
}
