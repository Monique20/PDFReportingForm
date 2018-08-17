using System.IO;
using FluentAssertions;
using NUnit.Framework;
using PdfReportingPoc.Operations;

namespace PdfReportingPoc.Data.Tests.Operations
{
    [TestFixture]
    public class PasswordTests
    {
        [Test]
        public void PasswordProtect_GivenValidPassword_ShouldEncryptFile()
        {
            //Arrange
            var fileName = "BootCampForm-v2.pdf";
            var fileBytes = GetFileBytes(fileName);
            var password = "user";

            var sut = CreatePdfOperations();

            //Act
            var actual = sut.PasswordProtect(fileBytes, password);

            //Assert
            actual.Length.Should().BeLessThan(fileBytes.Length);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void PasswordProtect_GivenEmptyOrNullPassword_ShouldReturnFalse(string password)
        {
            //Arrange
            var fileName = "BootCampForm-v2.pdf";
            var fileBytes = GetFileBytes(fileName);

            var sut = CreatePdfOperations();

            //Act
            var actual = sut.PasswordProtect(fileBytes, password);

            //Assert
            actual.Should().BeEquivalentTo(fileBytes);
        }
        
        private static Password CreatePdfOperations()
        {
            return new Password();
        }

        private static byte[] GetFileBytes(string text)
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
