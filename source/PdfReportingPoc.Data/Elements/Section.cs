using System;
using System.Collections.Generic;
using PdfReportingPoc.Domain.Elements;

namespace PdfReportingPoc.Elements
{
    public class Section : ISection
    {
        public string Name { get; set; } // simply for informational purposes 

        public List<ITable> Tables { get; } // add tables to a section for display

        public Section()
        {
            Tables = new List<ITable>();
        }

        public TableRenderData Render(TableRenderData input)
        {
            var data = input;

            foreach (var table in Tables)
            {
                data = table.Render(data);
            }

            return data;
        }
    }
}
