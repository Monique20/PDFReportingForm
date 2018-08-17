namespace PdfReportingPoc.Domain.Elements
{
    public interface IHeader
    {
        byte[] Render(byte[] report);
    }
}
