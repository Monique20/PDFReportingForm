using Aspose.Pdf;
using Aspose.Pdf.Text;
using FluentAssertions;
using NUnit.Framework;
using PdfReportingPoc.Elements;
using System.Collections.Generic;
using System.IO;
using PdfReportingPoc.Domain.Elements;
using PdfReportingPoc.Elements.Cells;
using Row = PdfReportingPoc.Elements.Row;
using Table = PdfReportingPoc.Elements.Table;
using Aspose.Pdf.Forms;
using PdfReportingPoc.Domain.Elements.Cell;
using PdfReportingPoc.Domain.Elements.Cells;

namespace PdfReportingPoc.Data.Tests.Sections
{

    [TestFixture]
    public class DisbursementSectionTests
    {
        public DisbursementSectionTests()
        {
            var license = new License();
            license.SetLicense("Aspose.Pdf.Lic");
        }

        [Test]
        public void Render_GivenDisbursementBankAccountHeading_ShouldCreateFirstTableForDisbursementSection()
        {
            //Arrange
            var fileName = "blank.pdf";
            var tableRenderData = GetFileBytes(fileName);
            var table = GetFirstTableForDisbursementSection();

            //Act
            var actual = table.Render(tableRenderData);

            //Assert
            var expected = tableRenderData.PdfBytes.Length;
            actual.PdfBytes.Length.Should().BeGreaterThan(expected);
        }

        [Test]
        public void Render_GivenDisbursementBankAccountDetails_ShouldCreateSecondTableForDisbursementSection()
        {
            //Arrange
            var fileName = "blank.pdf";
            var tableRenderData = GetFileBytes(fileName);
            var table = GetSecondTableForDisbursementSection();

            //Act
            var actual = table.Render(tableRenderData);

            //Assert
            var expected = tableRenderData.PdfBytes.Length;
            actual.PdfBytes.Length.Should().BeGreaterThan(expected);
        }

        [Test]
        public void Render_GivenDisbursementBankAccountDetails_ShouldCreateThirdTableForDisbursementSection()
        {
            //Arrange
            var fileName = "blank.pdf";
            var fileBytes = GetFileBytes(fileName);
            var table = GetThirdTableForDisbursementSection();

            //Act
            var actual = table.Render(fileBytes);
  
            //Assert
            var expected = fileBytes.PdfBytes.Length;
            actual.PdfBytes.Length.Should().BeGreaterThan(expected);
        }

        [Test]
        public void Render_GivenThatApplicantDoesNotHaveDebitOrderPaymentDetailsAndHOC_ShouldNotShowDisburmentBankAccountSection()
        {
            //Arrange
            var fileName = "blank.pdf";
            var fileBytes = GetFileBytes(fileName);
            var table = GetEmptyTable();
            //Act
            var actual = table.Render(fileBytes);

            //Assert
            var expected = fileBytes.PdfBytes.Length;
            actual.PdfBytes.Length.Should().Be(expected);
        }

        private static Table GetEmptyTable()
        {
            return new Table
            {
                Rows = new List<IRow>()
            };
        }

        private static Table GetThirdTableForDisbursementSection()
        {
            var optionItemProperties = new OptionItemProperties
            {
                Height = 20,
                Width = 20,
                Style = BoxStyle.Check
            };
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
                    Widths = "110 50 40 70 40 70 20",
                    CellBorder = new BorderInfo(BorderSide.None, 0.20f),
                },

                Rows = new List<IRow>
                {
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                            new Text
                            {
                                DisplayText = "Select Account Type:",
                                TextProperties = textProperties
                             },

                            new OptionGroup
                            {
                               OptionCells =  new List<ICell>{
                                   new Text
                                   {
                                      DisplayText = "Current",
                                      TextProperties = textProperties
                                   },
                                   new OptionItem
                                   {
                                      OptionName = "currentRadioBtn",
                                      Id = "currentTxtBox",
                                      OptionItemProperties = optionItemProperties
                                   },
                                    new Text
                                   {
                                      DisplayText = "Savings",
                                      TextProperties = textProperties
                                   },
                                   new OptionItem
                                   {
                                       Id = "savingsTxtBox",
                                       OptionName = "savingsRadioBtn",
                                       OptionItemProperties = optionItemProperties
                                   },
                                   new Text
                                   {
                                      DisplayText = "Transmission",
                                      TextProperties = textProperties
                                   },
                                   new OptionItem
                                   {
                                       Id = "transmissionTxtBox",
                                       OptionName = "transmissionRadioBtn",
                                       OptionItemProperties = optionItemProperties
                                   }
                                }
                             },
                          }
                      },
                  }
            };
        }

        private static Table GetSecondTableForDisbursementSection()
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
                    Widths = "90 150 90 90 ",
                    CellBorder = new BorderInfo(BorderSide.None, 0.20f),
                },

                Rows = new List<IRow>
                {
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                                new Text
                                {
                                    DisplayText = "Bank:",
                                    TextProperties = textProperties
                                },
                                 new TextBox
                                {
                                    Id = "txtBankName",
                                    TextBoxProperties =  new TextBoxProperties
                                    {
                                            Height = 20,
                                            Width = 137,
                                            Required = true,
                                            ForceComb = false,
                                            MaxLen = 10
                                     },
                                },
                                 new Text
                                 {
                                    DisplayText = "Branch Code:",
                                    TextProperties = textProperties
                                 },
                                 new TextBox
                                {
                                    Id = "txtBranchCode",
                                    TextBoxProperties =  new TextBoxProperties
                                    {
                                            Height = 20,
                                            Width = 137,
                                            Required = true,
                                            ForceComb = false,
                                            MaxLen = 10
                                     },
                                }
                             }
                       },
                      new Row
                    {
                       Cells = new List<ICell>
                       {
                                new Text
                                {
                                    DisplayText = "Account Name:",
                                    TextProperties = textProperties
                                },
                                 new TextBox
                                {
                                    Id = "txtAccountName",
                                    TextBoxProperties =  new TextBoxProperties
                                    {
                                            Height = 20,
                                            Width = 137,
                                            Required = true,
                                            ForceComb = false,
                                            MaxLen = 10
                                     },
                                },
                                 new Text
                                 {
                                    DisplayText = "Account No:",
                                    TextProperties = textProperties
                                 },
                                 new TextBox
                                {
                                    Id = "txtAccountNumber",
                                    TextBoxProperties =  new TextBoxProperties
                                    {
                                            Height = 20,
                                            Width = 137,
                                            Required = true,
                                            ForceComb = false,
                                            MaxLen = 10
                                     },
                                 }

                            }
                       },
                  }
            };
        }

        private static Table GetFirstTableForDisbursementSection()
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
                    Widths = "90 90 10 90 10 90 10 ",
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
                                    DisplayText = "Disbursement Bank Account:",
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
                                new Text
                                {
                                    DisplayText = "In the event of your application being approved, please provide the banking details of an applicant on this loan. This is where we will deposit your funds.",
                                    TextProperties = textProperties
                                },
                          }
                    }
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
