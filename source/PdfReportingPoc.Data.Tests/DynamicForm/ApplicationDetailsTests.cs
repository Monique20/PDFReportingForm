using FluentAssertions;
using NUnit.Framework;
using PdfReportingPoc.DynamicForm;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfReportingPoc.Data.Tests.DynamicForm
{
    class ApplicationDetailsTests
    {
        [Test]
        public void InsertSignatureFields_GivenSignatoryDetails_ShouldAddSignatureFieldAndReturnFileBytes()
        {
            //Arrange
            var fileName = "ProductionTemplateTestingPdf.pdf";
            var fileName1 = "Signature.pdf";
            var originalFile = GetFileBytes(fileName);
            var newDocument = GetFileBytes(fileName1);


            var sut = new ApplicationDetails();

            //Act
            var actual = sut.AddApplicationDetails(originalFile, newDocument);
            File.WriteAllBytes(@"C:\Users\mcebisim\Desktop\New folder\file.pdf", actual.Output);

            //Assert
            var expected = newDocument.Length;
            actual.Output.Length.Should().BeGreaterThan(expected);
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
