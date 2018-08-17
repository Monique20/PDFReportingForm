namespace PdfReportingPoc.Domain.Elements
{
    public interface IReport
    {
        byte[] Render(byte[] reportBytes);
    }
}
