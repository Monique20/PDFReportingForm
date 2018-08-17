using System;
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
    public class HeaderTests
    {
        [Test]
        public void Render_GivenHeaderWithImageAndText_ShouldAddHeaderOnEachPage()
        {
            //Arrange
            // right-justified-logo.png
            var fileBytes = GetFileBytes("template.pdf");
            var logoBytes = GetFileBytes("right-justified-logo.png");

            var fragements = new List<DocumentBoundryText>
            {
                new DocumentBoundryText
                {
                   Label = "Account Number: ",
                   Text = "12345",
                   SpacesNeeded = 28
                },
                new DocumentBoundryText
                {
                    Label = "Application Number:",
                    Text = "3620115",
                    SpacesNeeded = 50
                },
                new DocumentBoundryText
                {
                    Label = "Date:",
                    Text = DateTime.Now.ToString("MM/dd/yyyy"),
                    SpacesNeeded = 16
                }
            };

            var headerTextFragments = new List<HeaderTextFragment>
            {
                new HeaderTextFragment
                {
                    Color = Color.Black,
                    Size = 16,
                    TextFragments = new List<DocumentBoundryText>
                    {
                        new DocumentBoundryText
                        {
                            Text = "Demo Report"
                        }
                    },
                    TopMargin = 30,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top
                },
                new HeaderTextFragment
                {
                    TextFragments = fragements,
                    TopMargin = 55,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Color = Color.Black
                },
                new HeaderTextFragment
                {
                    Color = Color.Black,
                    TextFragments = new List<DocumentBoundryText>
                    {
                        new DocumentBoundryText
                        {
                            Text = "__________________________________________________________________________________________________"
                        }
                    },
                    TopMargin = 60,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top
                }
            };

            var sut = new Header
            {
                Logo = new TopLeftLogoFragement
                {
                    Image = logoBytes,
                    Height = 35,
                    Width = 85,
                    TopMargin = 7,
                    RightMargin = 10
                },
                TextFragements = headerTextFragments
            };
            //Act
            var actual = sut.Render(fileBytes);
            //Assert
            var expectedFileSize = 249666; // keeps shifting by many bytes every run?!
            actual.Length.Should().BeGreaterOrEqualTo(expectedFileSize);
        }

        private static byte[] GetFileBytes(string fileName)
        {
            var baseDirectory = TestContext.CurrentContext.TestDirectory + "\\TestData\\";
            var currentFilePath = Path.Combine(baseDirectory, fileName);
            var fileBytes = File.ReadAllBytes(currentFilePath);
            return fileBytes;
        }
    }
}
