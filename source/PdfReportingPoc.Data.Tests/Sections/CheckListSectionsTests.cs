using Aspose.Pdf;
using Aspose.Pdf.Text;
using FluentAssertions;
using NUnit.Framework;
using PdfReportingPoc.Elements;
using System.Collections.Generic;
using System.IO;
using PdfReportingPoc.Domain.Elements;
using PdfReportingPoc.Domain.Elements.Cell;
using PdfReportingPoc.Elements.Cells;
using Row = PdfReportingPoc.Elements.Row;
using Table = PdfReportingPoc.Elements.Table;

namespace PdfReportingPoc.Data.Tests.Sections
{
    [TestFixture]
    public class CheckListSectionsTests
    {
        [Test]
        public void Render_GivenTextForSupportingDocumentsSection_ShouldCreateSupportDocumentsSection()
        {
            //Arrange
            var fileName = "blank.pdf";
            var tableRenderData = GetFileBytes(fileName);
            var table = GetTableForCheckListHeading();

            //Act
            var actual = table.Render(tableRenderData);

            //Assert
            var expected = tableRenderData.PdfBytes.Length;
            actual.PdfBytes.Length.Should().BeGreaterThan(expected);
        }

       

        [Test]
        public void Render_GivenFirstCheckListSectionDetails_ShouldCreateFirstCheckListSection()
        {
            //Arrange
            var fileName = "blank.pdf";
            var tableRenderData = GetFileBytes(fileName);
            var table = GetTableForFirstCheckListSection();

            //Act
            var actual = table.Render(tableRenderData);

            //Assert
            var expected = tableRenderData.PdfBytes.Length;
            actual.PdfBytes.Length.Should().BeGreaterThan(expected);
        }

        [Test]
        public void Render_GivenSecondCheckListSectionDetails_ShouldCreateSecondCheckListSection()
        {
            //Arrange
            var fileName = "blank.pdf";
            var tableRenderData = GetFileBytes(fileName);
            var table = GetTableForSecondCheckListSection();

            //Act
            var actual = table.Render(tableRenderData);

            //Assert
            var expected = tableRenderData.PdfBytes.Length;
            actual.PdfBytes.Length.Should().BeGreaterThan(expected);
        }

        [Test]
        public void Render_WhenLoanDetailsTypeIsPaidUpWithHocOrAPersonIntendToApplyForHomeOwnersCoverOrMainApplicantIsNotATrust_ShouldNotAddSectionToDocument()
        {
            //Arrange
            var fileName = "blank.pdf";
            var tableRenderData = GetFileBytes(fileName);
            var section = GetNullSection();

            //Act
            var actual = section.Render(tableRenderData);

            //Assert
            Assert.AreEqual(actual.PdfBytes.Length, tableRenderData.PdfBytes.Length);
        }

        private ITable GetNullSection()
        {
            return new Table
            {
                Layout = new TableLayout(),

                Rows = new List<IRow>()
            };
        }

        private static Table GetTableForCheckListHeading()
        {
            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 100f,
                    Widths = "370 57",
                    CellBorder = new BorderInfo(BorderSide.None, 0.20f),
                },
                Rows = new List<IRow>
                {
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                            new Text {
                                DisplayText = "Supporting Documents:",
                                TextProperties = new TextProperties
                                {
                                        FontSize = 10,
                                        FontType = "Arial",
                                        FontStyle = FontStyles.Regular,
                                        FontColor = Color.Black,
                                        Invisible = false
                               }
                            }
                         }
                    }
                }
            };
        }

        private Table GetTableForSecondCheckListSection()
        {
            var textProperties = new TextProperties
            {
                FontSize = 10,
                FontType = "Arial",
                FontStyle = FontStyles.Regular,
                FontColor = Color.Black,
                Invisible = false
            };
            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 100f,
                    Widths = "370 57",
                    CellBorder = new BorderInfo(BorderSide.None, 0.20f),
                },
                Rows = new List<IRow>
                {
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                            new Text {
                                DisplayText = "Additional documentation required if your bond is held in the name of a Trust",
                                TextProperties = textProperties
                            },
                            new Text {
                                DisplayText = "Checklist",
                                TextProperties = textProperties
                            },
                        }
                    },
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                            new Text {
                                DisplayText ="A resolution giving permission for this loan, signed by all trustees",
                                TextProperties = textProperties
                            },
                            new Text {
                                DisplayText = "",
                                TextProperties = textProperties
                            },
                        }
                    }
                }
            };
        }

        private Table GetTableForFirstCheckListSection()
        {
            var textProperties = new TextProperties
            {

                FontSize = 10,
                FontType = "Arial",
                FontStyle = FontStyles.Regular,
                FontColor = Color.Black,
                Invisible = false
            };
            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 100f,
                    Widths = "370 57",
                    CellBorder = new BorderInfo(BorderSide.None, 0.20f),
                },
                Rows = new List<IRow>
                {
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                            new Text {
                                DisplayText = "Property Documentation",
                                TextProperties = textProperties
                            },
                            new Text {
                                DisplayText = "Checklist",
                                TextProperties = textProperties
                            },
                       }
                    },
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                                new Text {
                                    DisplayText = @"If you do not intend to apply for Homeowners Cover from SA Home Loans, please provide:

•	  Current building insurance policy document
",
                                    TextProperties = textProperties},

                                new Text {
                                    DisplayText = string.Empty,
                                    TextProperties = textProperties
                                },
                             }
                       }
                  }
            };
        }

        private TableRenderData GetFileBytes(string text)
        {
            //if (text.Contains("\\"))
            //{
            //    return File.ReadAllBytes(text);
            //}
            var baseDirectory = TestContext.CurrentContext.TestDirectory + "\\TestData\\";
            var currentFilePath = Path.Combine(baseDirectory, text);
            var fileBytes = File.ReadAllBytes(currentFilePath);

            return new TableRenderData
            {
                PdfBytes = fileBytes,
                TableHeight = 0
            };
        }
    }
}
