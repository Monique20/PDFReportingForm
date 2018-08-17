using Aspose.Pdf;
using NUnit.Framework;
using PdfReportingPoc.Elements.Cells;

namespace PdfReportingPoc.Data.Tests.Elements.Cells
{
    [TestFixture]
    public class TextCellTests
    {
        [Test]
        public void Ctor_ShouldNotHaveNullTextProperties()
        {
            // Arrange
            // Act
            var sut = new Text();
            // Assert
            Assert.NotNull(sut.TextProperties);
        }

        [Test]
        public void Render_ShouldAddCellToRow()
        {
            // Arrange
            var row = new Row();

            var sut = new Text
            {
                DisplayText = "Hello world!"
            };
            // Act
            sut.Render(row);
            // Assert
            Assert.AreEqual(1, row.Cells.Count);
        }
    }
}
