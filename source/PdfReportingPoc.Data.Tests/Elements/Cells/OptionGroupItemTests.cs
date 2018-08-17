using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Pdf;
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
        public void learning_test()
        {
            //Arrange
            var file = GetFilePath();
            var fileBytes = GetFileBytes(file);
            var fileStream = GetFileStream(fileBytes);

            var document = new Document(fileStream);


            var sut = new OptionItem
            {
                Id = "txtContact",
                OptionName = "radioButton"
            };

            //Act
            sut.Format(document);

            //Assert
            var fileResultsBytes = Save(document);
            File.WriteAllBytes(@"C:\New folder\Results.pdf", fileResultsBytes);
            fileStream.Close();
        }

        private byte[] Save(Document pdfDocument)
        {
            byte[] documentBytes;
            using (var documentStream = new MemoryStream())
            {
                pdfDocument.Save(documentStream);
                documentBytes = documentStream.ToArray();
            }
            return documentBytes;
        }
        private MemoryStream GetFileStream(byte[] fileBytes)
        {
            return new MemoryStream(fileBytes);
        }

        private static byte[] GetFileBytes(string file)
        {
            return File.ReadAllBytes(file);
        }

        private static string GetFilePath()
        {
            return @"C:\New folder\PdfForTestingTextBoxTypes.pdf";
        }
    }
}

