namespace PdfReportingPoc.Domain.Elements.Fragements
{
    public interface IFragement
    {
        byte[] Render(byte[] pdfReport);
    }
}
