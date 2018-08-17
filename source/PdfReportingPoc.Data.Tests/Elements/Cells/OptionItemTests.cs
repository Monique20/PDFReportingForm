using System;
using System.IO;
using Aspose.Pdf;
using FluentAssertions;
using NUnit.Framework;
using PdfReportingPoc.Elements.Cells;

namespace PdfReportingPoc.Data.Tests.Elements.Cells
{
    [TestFixture]
    public class OptionItemTests
    {
        [Test]
        public void Ctor_ShouldNotHaveNullTextProperties()
        {
            //Arrange

            // Act
            var sut = new OptionItem();
            // Assert
            Assert.NotNull(sut.OptionItemProperties);
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

        [Test]
        public void Format_GivenOptionItemWithPageNumber_ShouldAddRadioButtonOnSamePageNumberOfTexBbox()
        {
            //Arrange
            var file = GetFilePath();
            var originalFileBytes = GetFileBytes(file);
            using (var originalFileStream = GetFileStream(originalFileBytes))
            {
                var document = GetDocument(originalFileStream);

                var sut = new OptionItem
                {
                    Id = "txtContact",
                    OptionName = "radioButton1"
                };

                //Act
                sut.Format(document);
                var fileResultsBytes = Save(document);

                //Assert
                fileResultsBytes.Length.Should().BeGreaterThan(originalFileBytes.Length);
            }
        }

        private static Document GetDocument(Stream fileStream)
        {
            return new Document(fileStream);
        }

        private byte[] Save(Document pdfDocument)
        {
            pdfDocument.FreeMemory();

            byte[] documentBytes;
            using (var documentStream = new MemoryStream())
            {
                pdfDocument.Save(documentStream);
                documentBytes = documentStream.ToArray();
            }
            return documentBytes;
        }

        private static MemoryStream GetFileStream(byte[] fileBytes)
        {
            return new MemoryStream(fileBytes);
        }

        private static byte[] GetFileBytes(string file)
        {
            return File.ReadAllBytes(file);
        }

        private static string GetFilePath()
        {
            var filePath = AppDomain.CurrentDomain.BaseDirectory + "TestData\\check.pdf";
            return filePath;
        }
    }
}
