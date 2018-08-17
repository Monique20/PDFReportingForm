using System.IO;
using Aspose.Pdf;
using PdfReportingPoc.Domain.Pdf;

namespace PdfReportingPoc.Operations
{
    public class Password : IPassword
    {
        public Password()
        {
            var license = new License();
            license.SetLicense("Aspose.Pdf.Lic");
        }
        
        public byte[] PasswordProtect(byte[] fileBytes, string password)
        {
            if (NullOrWhiteSpace(password))
            {
                return fileBytes;
            }

            using (var incomingStream = new MemoryStream(fileBytes))
            {
                var pdfDocument = new Document(incomingStream);
                pdfDocument.Encrypt(password, password, 0, CryptoAlgorithm.RC4x128);
                using (var documentStream = new MemoryStream())
                {
                    pdfDocument.Save(documentStream);
                    return documentStream.ToArray();
                }
            }
        }

        private bool NullOrWhiteSpace(string password)
        {
            return string.IsNullOrWhiteSpace(password);
        }
    }
}
