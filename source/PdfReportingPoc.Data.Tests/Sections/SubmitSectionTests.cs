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
    public class SubmitSectionTests
    {
        [Test]
        public void Render_GivenSubmitSectionDetails_ShouldCreateSubmitSection()
        {
            //Arrange
            var fileName = "blank.pdf";
            var tableRenderData = GetFileBytes(fileName);
            var table = GetTable();

            //Act
            var actual = table.Render(tableRenderData);

            //Assert
            var expected = tableRenderData.PdfBytes.Length;
            actual.PdfBytes.Length.Should().BeGreaterThan(expected);
        }

        private static Table GetTable()
        {
            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 100f,
                    CellBorder = new BorderInfo(BorderSide.None, 0.20f),
                    ColumnAdjustment = ColumnAdjustment.AutoFitToWindow
                },
                Rows = new List<IRow>
                {
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                            new Text
                             {
                                 DisplayText = "Submit Your Application:",
                                 TextProperties = new TextProperties
                                 {
                                      FontSize = 14,
                                      FontType = "Arial",
                                      FontStyle = FontStyles.Bold,
                                      FontColor = Color.Black,
                                      Invisible = false
                                 }
                             },
                        }
 
                    },
                     new Row
                    {
                       Cells = new List<ICell>
                       {
                             new Text {
                                        DisplayText = @"
Please note: should this application not be sent to us within 90 days, a new updated application form will be required


Email your application to: fl @sahomeloans.com


Fax your application to: 0865526888",
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
                    },
                }
            };
        }

        private TableRenderData GetFileBytes(string text)
        {
            var baseDirectory = TestContext.CurrentContext.TestDirectory + "\\TestData\\";
            var currentFilePath = Path.Combine(baseDirectory, text);
            var fileBytes = File.ReadAllBytes(currentFilePath);

            return new TableRenderData
            {
                PdfBytes = fileBytes
            };
        }
    }
}
