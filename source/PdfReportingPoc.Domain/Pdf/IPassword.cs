namespace PdfReportingPoc.Domain.Pdf
{
    public interface IPassword
    {
        byte[] PasswordProtect(byte[] fileBytes, string password);

    }
}
