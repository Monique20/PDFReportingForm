using PdfReportingPoc.Domain.FileSystem;
using Aspose.Pdf;

namespace PdfReportingPoc.FileSystem
{
    public class FileSystemProvider : IFileSystemProvider
    {
        public FileSystemProvider()
        {
            var license = new License();
            license.SetLicense("Aspose.Pdf.Lic");
        }

        public byte[] LoadFile(string pdfPath)
        { 
            if (System.IO.File.Exists(pdfPath))
            {
                var fileByte = System.IO.File.ReadAllBytes(pdfPath);
                return fileByte;
            }


            return new byte[0];
        }
        public bool SaveFile(string targetPath, byte[] bytes)
        {
            if (string.IsNullOrWhiteSpace(targetPath))
            {
                return false;
            }

            System.IO.File.WriteAllBytes(targetPath, bytes);

            return true;
        }

    }
}
