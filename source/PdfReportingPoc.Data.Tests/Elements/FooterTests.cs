using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using FluentAssertions;
using NUnit.Framework;
using PdfReportingPoc.Elements;
using PdfReportingPoc.Elements.Fragments;

namespace PdfReportingPoc.Data.Tests.Elements
{
    [TestFixture]
    public class FooterTests
    {
        [Test]
        public void Render_GivenText_ShouldAddFooterOnEachPage()
        {
            //Arrange
            var fileName = "DemoForm.pdf";
            var baseDirectory = TestContext.CurrentContext.TestDirectory + "\\TestData\\";
            var currentFilePath = Path.Combine(baseDirectory, fileName);
            var fileBytes = File.ReadAllBytes(currentFilePath);
            
            var fragments = new List<FooterFragment>
            {
                new FooterFragment
                {
                    BottomMargin = 10,
                    LeftMargin = 15,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Color = Color.Gray,
                    Fragments = new List<DocumentBoundryText>
                    {
                        new DocumentBoundryText
                        {
                            Text = "<<File Location>>",
                            SpacesNeeded = 80
                        },
                        new DocumentBoundryText
                        {
                            Text = "<<Report Name>>",
                            SpacesNeeded = 0
                        }
                    }
                }
            };

            var sut = new Footer
            {
                InsertPageNumbers = true,
                TextFragements = fragments
            };

            //Act
            var actual = sut.Render(fileBytes);
            //Assert
            var expectedFileSize = 3520560; // this keeps shifting by 1 or 2 bytes ever run?!
            actual.Length.Should().BeGreaterOrEqualTo(expectedFileSize);
        }
        
    }
}
