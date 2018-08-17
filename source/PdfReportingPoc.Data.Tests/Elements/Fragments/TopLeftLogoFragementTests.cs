using System.IO;
using FluentAssertions;
using NUnit.Framework;
using PdfReportingPoc.Elements.Fragments;

namespace PdfReportingPoc.Data.Tests.Elements.Fragments
{
    [TestFixture]
    public class TopLeftLogoFragementTests
    {
        [Test]
        public void Render_WhenNullBytes_ShouldNotRender()
        {
            // Arrange
            var pdfByte = new byte[1];
            var sut = new TopLeftLogoFragement();
            // Act
            var actual = sut.Render(pdfByte);
            // Assert
            actual.Should().BeEquivalentTo(pdfByte);
        }

        [Test]
        public void Render_WhenNotNullBytes_ShouldRender()
        {
            // Arrange
            var fileBytes = GetFileBytes("template.pdf");
            var logoBytes = GetFileBytes("right-justified-logo.png");

            var sut = new TopLeftLogoFragement
            {
                Image = logoBytes,
                Height = 35,
                Width = 85,
                TopMargin = 7,
                RightMargin = 10
            };
            // Act
            var actual = sut.Render(fileBytes);
            // Assert
            var expectedFileSize = 164407; // keeps shifting by many bytes every run?!
            actual.Length.Should().Be(expectedFileSize);
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
