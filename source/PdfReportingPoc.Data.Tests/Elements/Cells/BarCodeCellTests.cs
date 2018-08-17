using Aspose.Pdf;
using NUnit.Framework;

namespace PdfReportingPoc.Data.Tests.Elements.Cells
{
    [TestFixture]
    public class BarCodeCellTests
    {
        [Test]
        public void Ctor_ShouldNotHaveNullTextProperties()
        {
            // Arrange
            // Act
            var sut = new PdfReportingPoc.Elements.Cells.BarCode();
            // Assert
            Assert.NotNull(sut.Text);
        }

        [Test]
        public void Render_ShouldAddCellToRow()
        {
            // Arrange
            var row = new Row();

            var sut = new PdfReportingPoc.Elements.Cells.BarCode
            {
                Text = "Hello world!"
            };
            // Act
            sut.Render(row);
            // Assert
            Assert.AreEqual(1, row.Cells.Count);
        }
    }
}
