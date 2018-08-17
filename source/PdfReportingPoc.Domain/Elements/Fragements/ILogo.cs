namespace PdfReportingPoc.Domain.Elements.Fragements
{
    public interface ILogo
    {
        byte[] Render(byte[] pdfReport);
    }
}
