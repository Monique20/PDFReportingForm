using Aspose.Pdf;
using NUnit.Framework;
using Signature = PdfReportingPoc.Elements.Cells.Signature;

namespace PdfReportingPoc.Data.Tests.Elements.Cells
{
    [TestFixture]
    public class SignatureCellTests
    {
        [Test]
        public void Constructor_ShouldNotHaveNullSignatureProperties()
        {
            // Arrange
            // Act
            var sut = new Signature();

            // Assert
            Assert.NotNull(sut.SignatureProperties);
        }

        [Test]
        public void Render_ShouldAddCellToRow()
        {
            // Arrange
            var row = new Row();

            var sut = new Signature();

            // Act
            sut.Render(row);

            // Assert
            Assert.AreEqual(1, row.Cells.Count);
        }
       
    }
}
