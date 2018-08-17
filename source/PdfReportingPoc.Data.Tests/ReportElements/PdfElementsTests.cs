using NUnit.Framework;
using System.IO;
using PdfReportingPoc.ReportElements;
using FluentAssertions;
using PdfReportingPoc.Domain.Pdf;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace PdfReportingPoc.Data.Tests.ReportElements
{
    class PdfElementsTests
    {
        [Test]
        public void LoadWithHocSection_GivenFileAndListOfFragments_ShouldReturnFileWithHocSection()
        {
            //Arrange
            var fileName = "Redraw-Application-Form.pdf";
            var pdfPath = TestContext.CurrentContext.TestDirectory;
            var fullPath = Path.Combine(pdfPath, "TestData", fileName);
            var bytes = File.ReadAllBytes(fullPath);

            var fragments = new List<Fragment>
            {
                new Fragment
                {
                    Type = "Text",
                    Text = "BUILDING INSURANCE",
                    Font = FontRepository.FindFont("Cambria", FontStyles.Bold),
                    ForegroundColor = Color.Parse("#E36C0A"),
                    FontSize = 30,
                    Margin = new MarginInfo(0,0,0,250),
                    Page=1
                },
                new Fragment
                {
                    Type = "Text",
                    Text = "Select:",
                    Font = FontRepository.FindFont("Arial"),
                    ForegroundColor = Color.Parse("#000000"),
                    FontSize = 12,
                    Margin = new MarginInfo(0,0,0,30),
                    Page=1
                },
                new Fragment
                {
                    Type = "Text",
                    Text = "We require proof of building insurance (Home Owners Cover). Alternatively SA Home Loans can provide Bond Protection:",
                    Font = FontRepository.FindFont("Arial"),
                    ForegroundColor = Color.Parse("#000000"),
                    FontSize = 12,
                    Margin = new MarginInfo(0,0,0,280),
                    Page=1
                },
                new Fragment
                {
                    Type="Checklist",
                    Options=new List<string>
                    {
                        "I already have building insurance (Home Owners Cover)",
                        "I already have building  (Home Owners Cover)",
                        "I already have  insurance (Home Owners Cover)",
                        "I  have building insurance (Home Owners Cover)",
                        "I want to apply for Home Owners Cover with SA Home Loans"
                    },
                    Margin=new MarginInfo(0,0,0,400),
                    Page=1
                }
            };

            var sut = new PdfElements();

            //Act
            var actual = sut.LoadWithHocSection(bytes, fragments);

            //Assert
            actual.Length.Should().BeGreaterThan(bytes.Length);
        }

        [Test]
        public void LoadWithHocSection_GivenFileAndEmptyListOfFragments_ShouldReturnFileWithoutHocSection()
        {
            //Arrange
            var fileName = "ProductionTemplateWithoutHOCSection.pdf";
            var pdfPath = TestContext.CurrentContext.TestDirectory;
            var fullPath = Path.Combine(pdfPath, "TestData", fileName);
            var bytes = File.ReadAllBytes(fullPath);

            var fragments = new List<Fragment>();

            var sut = new PdfElements();

            //Act
            var actual = sut.LoadWithHocSection(bytes, fragments);

            //Assert
            actual.Length.Should().Equals(bytes.Length);
        }

        [Test]
        public void LoadWithHocSection_GivenListOfFragmentsAndNoFile_ShouldReturnEmptyFile()
        {
            //Arrange
            var bytes = new byte[0];

            var fragments = new List<Fragment>
            {
                new Fragment
                {
                    Type = "Text",
                    Text = "BUILDING INSURANCE",
                    Font = FontRepository.FindFont("Cambria", FontStyles.Bold),
                    ForegroundColor = Color.Parse("#E36C0A"),
                    FontSize = 30,
                    Margin = new MarginInfo(0,0,0,250),
                    Page=1
                },
                new Fragment
                {
                    Type = "Text",
                    Text = "Select:",
                    Font = FontRepository.FindFont("Arial"),
                    ForegroundColor = Color.Parse("#000000"),
                    FontSize = 12,
                    Margin = new MarginInfo(0,0,0,30),
                    Page=1
                },
                new Fragment
                {
                    Type = "Text",
                    Text = "We require proof of building insurance (Home Owners Cover). Alternatively SA Home Loans can provide Bond Protection:",
                    Font = FontRepository.FindFont("Arial"),
                    ForegroundColor = Color.Parse("#000000"),
                    FontSize = 12,
                    Margin = new MarginInfo(0,0,0,280),
                    Page=1
                },
                new Fragment
                {
                    Type="Checklist",
                    Options=new List<string>
                    {
                        "I already have building insurance (Home Owners Cover)",
                        "I want to apply for Home Owners Cover with SA Home Loans"
                    },
                    Margin=new MarginInfo(0,0,0,400),
                    Page=1
                }
            };

            var sut = new PdfElements();

            //Act
            var actual = sut.LoadWithHocSection(bytes, fragments);

            //Assert
            actual.Length.Should().Equals(bytes.Length);
        }

        [Test]
        public void LoadWithHocSection_GivenNoFileAndEmptyListOfFragments_ShouldReturnEmptyFile()
        {
            //Arrange
            var bytes = new byte[0];

            var fragments = new List<Fragment>();

            var sut = new PdfElements();

            //Act
            var actual = sut.LoadWithHocSection(bytes, fragments);

            //Assert
            actual.Length.Should().Equals(0);
        }
    }
}
