using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using PdfReportingPoc.Elements.Fragments;

namespace PdfReportingPoc.Data.Tests.Elements.Fragments
{
    [TestFixture]
    public class FooterFragementTests
    {
        [Test]
        public void Ctor_ShouldInitalizeProperties()
        {
            // Arrange
            // Act
            var sut = new FooterFragment();
            // Assert
            Assert.NotNull(sut.Fragments);
            Assert.NotNull(sut.HorizontalAlignment);
            Assert.NotNull(sut.VerticalAlignment);
            Assert.NotNull(sut.Color);
        }

        [Test]
        public void Render_GivenTextWithLabel_ShouldRenderIntoDocumentHeader()
        {
            // Arrange
            var fileBytes = GetFileBytes("template.pdf");

            var sut = new FooterFragment
            {
                Fragments = new List<DocumentBoundryText>
                {
                    new DocumentBoundryText
                    {
                        Text = "Footer",
                        Label = "My Second Label:"
                    }
                }
            };
            // Act
            var actual = sut.Render(fileBytes);
            // Assert
            var expectedFileBytes = 143519; // keeps shifting by many bytes every run?!
            actual.Length.Should().Be(expectedFileBytes);

        }

        private byte[] GetFileBytes(string text)
        {
            if (text.Contains("\\"))
            {
                return File.ReadAllBytes(text);
            }
            var baseDirectory = TestContext.CurrentContext.TestDirectory + "\\TestData\\";
            var currentFilePath = Path.Combine(baseDirectory, text);
            var fileBytes = File.ReadAllBytes(currentFilePath);

            return fileBytes;
        }
    }
}
