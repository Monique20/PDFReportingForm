using System.Collections.Generic;

namespace PdfReportingPoc.Domain.Elements
{
    public interface ISection
    {
        List<ITable> Tables { get; }

        TableRenderData Render(TableRenderData input);
    }
}
