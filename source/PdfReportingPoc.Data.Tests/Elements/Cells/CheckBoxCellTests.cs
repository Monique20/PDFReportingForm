using Aspose.Pdf;
using NUnit.Framework;
using PdfReportingPoc.Domain.Elements.Cells;
using PdfReportingPoc.Elements.Cells;

namespace PdfReportingPoc.Data.Tests.Elements.Cells
{
    [TestFixture]
    public class CheckBoxCellTests
    {
        [Test]
        public void Constructor_ShouldNotHaveNullCheckBoxProperties()
        {
            // Arrange
            // Act
            var sut = new Checkbox();
            // Assert
            Assert.NotNull(sut.CheckboxProperites);
        }

        [Test]
        public void Render_ShouldAddCellToRow()
        {
            // Arrange
            var row = new Row();

            var sut = new Checkbox
            {
                CheckboxProperites = new CheckboxProperites
                {
                    Id = "testChckbox",
                    Height = 20,
                    Width = 20,
                }
            };
            // Act
            sut.Render(row);
            // Assert
            Assert.AreEqual(1, row.Cells.Count);
        }

    }
}
