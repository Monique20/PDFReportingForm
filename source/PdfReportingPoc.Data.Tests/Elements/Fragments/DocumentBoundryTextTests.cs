using FluentAssertions;
using NUnit.Framework;
using PdfReportingPoc.Elements.Fragments;

namespace PdfReportingPoc.Data.Tests.Elements.Fragments
{
    [TestFixture]
    public class DocumentBoundryTextTests
    {
        [Test]
        public void Ctor_ShouldInitalizeProperties()
        {
            // Arrange
            // Act
            var sut = new DocumentBoundryText();
            // Assert
            Assert.NotNull(sut.Text);
            Assert.NotNull(sut.Label);
            Assert.AreEqual(0,sut.SpacesNeeded);
        }

        [Test]
        public void ToString_WhenPaddingNeeded_ShouldReturnPaddedString()
        {
            // Arrange
            var sut = new DocumentBoundryText
            {
                SpacesNeeded = 50,
                Text = "some test",
                Label = "a label:"
            };
            // Act
            var actual = sut.ToString();
            // Assert
            var expected = "a label: some test                                ";
            actual.Should().Be(expected);
        }
    }
}
