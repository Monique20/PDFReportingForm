using Aspose.Pdf;
using NUnit.Framework;
using PdfReportingPoc.Domain.Elements.Cell;

namespace PdfReportingPoc.Domain.Tests.Elements
{
    [TestFixture]
    public class OptionItemPropertiesTests
    {
        [Test]
        public void GetFieldGroup_WhenNull_ShouldInitalizeAndReturn()
        {
            // Arrange
            var document = new Document();
            var sut = new OptionItemProperties();
            // Act
            var actual = sut.GetFieldGroup(document);
            // Assert
            Assert.NotNull(actual);
        }

        [Test]
        public void GetFieldGroup_WhenNotNull_ShouldCurrentInstance()
        {
            // Arrange
            var document = new Document();
            var sut = new OptionItemProperties();
            // Act
            var actual1 = sut.GetFieldGroup(document);
            var actual2 = sut.GetFieldGroup(document);
            // Assert
            Assert.AreEqual(actual1, actual2);
        }
    }
}
