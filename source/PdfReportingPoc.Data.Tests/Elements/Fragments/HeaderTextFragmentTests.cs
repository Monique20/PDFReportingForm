using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using PdfReportingPoc.Elements.Fragments;

namespace PdfReportingPoc.Data.Tests.Elements.Fragments
{
    [TestFixture]
    public class HeaderTextFragmentTests
    {
        [Test]
        public void Ctor_ShouldInitalizeProperties()
        {
            // Arrange
            // Act
            var sut = new HeaderTextFragment();
            // Assert
            Assert.NotNull(sut.TextFragments);
            Assert.NotNull(sut.HorizontalAlignment);
            Assert.NotNull(sut.VerticalAlignment);
            Assert.NotNull(sut.Color);
            Assert.NotNull(sut.Font);
            Assert.AreNotEqual(0.0, sut.Size);
        }

        [Test]
        public void Render_GivenTextWithLabel_ShouldRenderIntoDocumentHeader()
        {
            // Arrange
            var fileBytes = GetFileBytes("template.pdf");

            var sut = new HeaderTextFragment
            {
                TextFragments = new List<DocumentBoundryText>
                {
                    new DocumentBoundryText
                    {
                        Text = "Hello world!",
                        Label = "My First Label:",
                        SpacesNeeded = 100
                    }
                }
            };
            // Act
            var actual = sut.Render(fileBytes);
            // Assert
            var expectedFileBytes = 173500; // keeps shifting by many bytes every run?!
            actual.Length.Should().BeGreaterOrEqualTo(expectedFileBytes);

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
