namespace PdfReportingPoc.Domain.Elements
{
    public interface ITable
    {
        TableRenderData Render(TableRenderData input);
    }
}