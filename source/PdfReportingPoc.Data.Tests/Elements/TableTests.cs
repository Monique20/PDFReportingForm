using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;
using FluentAssertions;
using NUnit.Framework;
using PdfReportingPoc.Elements;
using System.Collections.Generic;
using System.IO;
using PdfReportingPoc.Domain.Elements;
using PdfReportingPoc.Domain.Elements.Cell;
using PdfReportingPoc.Domain.Elements.Cells;
using PdfReportingPoc.Elements.Cells;
using Row = PdfReportingPoc.Elements.Row;
using Signature = PdfReportingPoc.Elements.Cells.Signature;
using Table = PdfReportingPoc.Elements.Table;

namespace PdfReportingPoc.Data.Tests.Elements
{
    [TestFixture]
    public class TableTests
    {
        [Test]
        public void Ctor_ShouldCreateDefaultLayout()
        {
            // Arrange
            // Act
            var sut = new Table();
            // Assert
            Assert.NotNull(sut.Layout);
        }

        [Test]
        public void Format_GivenTextBoxFieldWithFieldTypeAndFormular_ShouldFormatTextBoxFieldAndCreateFormular()
        {
            //Arrange
            var fileName = "Signature.pdf";
            var tableRenderData = GetFileBytes(fileName);

            var table = new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 100f,
                    Widths = "140 140 140",
                    CellBorder = new BorderInfo(BorderSide.None, 0.20f),
                    ColumnAdjustment = ColumnAdjustment.Customized
                },
                Rows = new List<IRow>
                {
                         new Row
                         {
                            Cells = new List<ICell>
                            {
                                new TextBox
                                {
                                    Id = "txtSalary",
                                    TextBoxProperties =  new TextBoxProperties
                                    {
                                        Height = 20,
                                        Width = 130,
                                        MaxLen = 10,
                                        Required = true,
                                        Calculatable = false,
                                        ForceComb = false,
                                        Type = "Number",
                                        NumberFormatProperties = new NumberFormatProperties
                                        {
                                            NumberOfPlacesAfterDecimalPoint = 2,
                                            SeparateWithComma = true,
                                            NegativeStyle = NegativeNumberFormat.Red
                                        }
                                    },
                                },
                                new TextBox
                                {
                                    Id = "txtExpense",
                                    TextBoxProperties =  new TextBoxProperties
                                    {
                                        Height = 20,
                                        Width = 130,
                                        MaxLen = 10,
                                        Required = true,
                                        Calculatable = false,
                                        ForceComb = false,
                                        Type = "number",
                                        NumberFormatProperties = new NumberFormatProperties
                                        {
                                            NumberOfPlacesAfterDecimalPoint = 2,
                                            SeparateWithComma = true,
                                            NegativeStyle = NegativeNumberFormat.Red
                                        }
                                    },
                                },
                                new TextBox
                                {
                                    Id = "txtTotal",
                                    TextBoxProperties =  new TextBoxProperties
                                    {
                                        Height = 20,
                                        Width = 130,
                                        MaxLen = 10,
                                        Required = true,
                                        ReadOnly = true,
                                        ForceComb = false,
                                        Calculatable = true,
                                        Type = "Number",
                                        NumberFormatProperties = new NumberFormatProperties
                                        {
                                            NumberOfPlacesAfterDecimalPoint = 2,
                                            SeparateWithComma = true,
                                            NegativeStyle = NegativeNumberFormat.Red,
                                            CurrencySymbol = "R",
                                            CurrencyPrepend = true
                                        },
                                        Formular = "txtSalary + txtExpense * txtSalary"
                                    },
                                }
                            }
                         }
                    }
            };

            //Act
            var actual = table.Render(tableRenderData);

            //Assert
            var expected = tableRenderData.PdfBytes.Length;
            actual.PdfBytes.Length.Should().BeGreaterThan(expected);
        }

        [Test]
        public void Render_GivenTextBoxFieldWithValueToPopulated_ShouldPopulatedTextBoxFieldAndWithGivenValue()
        {
            //Arrange
            var fileName = "Signature.pdf";
            var tableRenderData = GetFileBytes(fileName);

            var table = new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 100f,
                    Widths = "140 140 140",
                    CellBorder = new BorderInfo(BorderSide.None, 0.20f),
                    ColumnAdjustment = ColumnAdjustment.Customized
                },
                Rows = new List<IRow>
                {
                         new Row
                         {
                            Cells = new List<ICell>
                            {
                                new TextBox
                                {
                                    Id = "txtSalary",
                                    TextBoxProperties =  new TextBoxProperties
                                    {
                                        Height = 20,
                                        Width = 130,
                                        Required = true,
                                        ReadOnly=true,
                                        Calculatable = false,
                                        ForceComb = false,
                                        Type = "Number",
                                        Value = "20.00"
                                    },
                                },
                                new TextBox
                                {
                                    Id = "txtExpense",
                                    TextBoxProperties =  new TextBoxProperties
                                    {
                                        Height = 20,
                                        Width = 130,
                                        MaxLen = 10,
                                        Required = true,
                                        ReadOnly=true,
                                        ForceComb = true,
                                        Value="Mcebisi"
                                    },
                                },
                                new TextBox
                                {
                                    Id = "txtTotal",
                                    TextBoxProperties =  new TextBoxProperties
                                    {
                                        Height = 20,
                                        Width = 130,
                                        MaxLen = 10,
                                        Required = true,
                                        ReadOnly = true,
                                        ForceComb = false,
                                        Type = "Date",
                                        Value="8/15/18"
                                    },
                                }
                            }
                         }
                    }
            };

            //Act
            var actual = table.Render(tableRenderData);

            //Assert
            var expected = tableRenderData.PdfBytes.Length;
            actual.PdfBytes.Length.Should().BeGreaterThan(expected);
        }

        [Test]
        public void Render_GivenTableDetails_ShouldCreateAndPopulateTableAndReturnFileBytes()
        {
            //Arrange
            var fileName = "Signature.pdf";
            var tableRenderData = GetFileBytes(fileName);
            var table = GetTable();

            //Act
            var actual = table.Render(tableRenderData);

            //Assert
            var expected = tableRenderData.PdfBytes.Length;
            actual.PdfBytes.Length.Should().BeGreaterThan(expected);
        }

        [Test]
        public void Render_GivenTableWithRadioButtonAndCheckBoxDetails_ShouldCreateAndPopulateTableAndReturnFileBytes()
        {
            //Arrange
            var fileName = "Signature.pdf";
            var tableRenderData = GetFileBytes(fileName);
            var optionItemProperties = new OptionItemProperties
            {
                Height = 20,
                Width = 20,
                Style = BoxStyle.Check
            };
            var textProperties = new TextProperties
            {
                FontSize = 15,
                FontType = "Arial",
                FontStyle = FontStyles.Bold,
                FontColor = Color.Black,
                Invisible = false
            };
            var table = new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 100f,
                    Widths = "200 30 30 30",
                    ColumnAdjustment = ColumnAdjustment.Customized
                },
                Rows = new List<IRow>
                {
                    new Row
                    {
                        Cells = new List<ICell>
                        {
                            new Text {
                                DisplayText = "Declarations",
                                TextProperties = textProperties},
                        }
                    },
                     new Row
                     {
                        Cells = new List<ICell>
                        {
                            new Text
                            {
                                DisplayText = "Have you ever been declared insolvent?",
                                TextProperties = new TextProperties
                                {
                                    FontSize = 10,
                                    FontType = "Arial",
                                    FontStyle = FontStyles.Regular,
                                    FontColor = Color.Black,
                                    Invisible = false
                                }
                            },
                            new OptionGroup
                            {
                               OptionCells =  new List<ICell>{
                                   new Text
                                   {
                                      DisplayText = "Yes",
                                      TextProperties = new TextProperties
                                      {
                                         FontSize = 10,
                                         FontType = "Arial",
                                         FontStyle = FontStyles.Regular,
                                         FontColor = Color.Black,
                                         Invisible = false
                                      }
                                   },
                                   new OptionItem
                                   {
                                      OptionName = "yesRadioBtn",
                                      Id = "yesTxtBox",
                                      OptionItemProperties = optionItemProperties
                                   },
                                    new Text
                                   {
                                      DisplayText = "No",
                                      TextProperties = new TextProperties
                                      {
                                         FontSize = 10,
                                         FontType = "Arial",
                                         FontStyle = FontStyles.Regular,
                                         FontColor = Color.Black,
                                         Invisible = false
                                      }
                                   },
                                   new OptionItem
                                   {
                                       Id = "NoTxtBox",
                                       OptionName = "NoRadioBtn",
                                       OptionItemProperties = optionItemProperties
                                   }
                               }
                            },
                            new Checkbox
                            {
                                CheckboxProperites = new CheckboxProperites{
                                    Height = 20,
                                    Width = 20,
                                    Id = "checkBox"
                                }
                            }
                        }
                     },
                }
            };

            //Act
            var actual = table.Render(tableRenderData);

            //Assert
            var expected = tableRenderData.PdfBytes.Length;
            actual.PdfBytes.Length.Should().BeGreaterThan(expected);
        }

        [Test]
        public void Render_SignatureCell_ShouldAddSignatureCellOnTableInTheCorrectFormat()
        {
            //Arrange
            var fileName = "blank.pdf";
            var tableRenderData = GetFileBytes(fileName);
            var table = new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 200f,
                    Widths = "50 100 100 80 100",
                    CellBorder = new BorderInfo(BorderSide.None, 0.20f),
                    ColumnAdjustment = ColumnAdjustment.Customized
                },
                Rows = new List<IRow>
                {
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                            new Signature
                            {
                                SignatureProperties =  new SignatureProperties
                                {
                                    PartialName = "signature1",
                                    Height = 20,
                                    Width = 250
                                }
                            }
                       }
                    }
                }
            };

            //Act
            var actual = table.Render(tableRenderData);

            //Assert
            var expected = tableRenderData.PdfBytes.Length;
            actual.PdfBytes.Length.Should().BeLessThan(expected);
        }

        private Table GetTable()
        {
            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 100f,
                    Position = TablePosition.Absolute,
                    Widths = "85 92 130 120",
                    CellBorder = new BorderInfo(BorderSide.None, 0.20f),
                    ColumnAdjustment = ColumnAdjustment.Customized
                },
                Rows = new List<IRow>
                {
                   new Row
                   {
                      Cells = new List<ICell>
                      {
                         new Text {
                                    DisplayText = "FULL NAME",
                                    TextProperties = new TextProperties
                                    {
                                        FontSize = 15,
                                        FontType = "Arial",
                                        FontStyle = FontStyles.Bold,
                                        FontColor = Color.Black,
                                        Invisible = false
                                    }},
                         new Text {
                                    DisplayText = "ID No",
                                    TextProperties = new TextProperties
                                    {
                                        FontSize = 15,
                                        FontType = "Arial",
                                        FontStyle = FontStyles.Bold,
                                        FontColor = Color.Black,
                                        Invisible = false
                                    }},
                         new Text {
                                    DisplayText = "ROLE",
                                    TextProperties = new TextProperties
                                    {
                                        FontSize = 15,
                                        FontType = "Arial",
                                        FontStyle = FontStyles.Bold,
                                        FontColor = Color.Black,
                                        Invisible = false
                                    }},
                         new Text {
                                    DisplayText = "SIGNATURE",
                                    TextProperties = new TextProperties
                                    {
                                        FontSize = 15,
                                        FontType = "Arial",
                                        FontStyle = FontStyles.Bold,
                                        FontColor = Color.Black,
                                        Invisible = false
                                    }}
                      }
                   },
                   new Row
                   {
                      Cells = new List<ICell>
                      {
                         new Text {
                                    DisplayText = "Travis Frisinger",
                                    TextProperties = new TextProperties
                                    {
                                        FontSize = 10,
                                        FontType = "Arial",
                                        FontStyle = FontStyles.Regular,
                                        FontColor = Color.Black,
                                        Invisible = false
                                    }},
                         new Text {
                                    DisplayText = "111111111",
                                    TextProperties = new TextProperties
                                    {
                                        FontSize = 10,
                                        FontType = "Arial",
                                        FontStyle = FontStyles.Regular,
                                        FontColor = Color.Black,
                                        Invisible = false
                                    }},
                         new Text {
                                    DisplayText = "A dude",
                                    TextProperties = new TextProperties
                                    {
                                        FontSize = 10,
                                        FontType = "Arial",
                                        FontStyle = FontStyles.Regular,
                                        FontColor = Color.Black,
                                        Invisible = false
                                    }},
                         new Signature
                                {
                                    SignatureProperties =  new SignatureProperties
                                    {
                                        PartialName = "signature1",
                                        Height = 20,
                                        Width = 115
                                    }
                                }
                      }
                   },
                   new Row
                   {
                      Cells = new List<ICell>
                      {
                         new Text {
                                    DisplayText = "Thabani Tembe",
                                    TextProperties = new TextProperties
                                    {
                                        FontSize = 10,
                                        FontType = "Arial",
                                        FontStyle = FontStyles.Italic,
                                        FontColor = Color.Black,
                                        Invisible = false
                                    }},
                         new Text {
                                    DisplayText = "0005279632081",
                                    TextProperties = new TextProperties
                                    {
                                        FontSize = 10,
                                        FontType = "Arial",
                                        FontStyle = FontStyles.Italic,
                                        FontColor = Color.Black,
                                        Invisible = false
                                    }},
                         new Text {
                                    DisplayText = "Apprentice",
                                    TextProperties = new TextProperties
                                    {
                                        FontSize = 10,
                                        FontType = "Arial",
                                        FontStyle = FontStyles.Italic,
                                        FontColor = Color.Black,
                                        Invisible = false
                                    }},
                         new Signature
                                {
                                    SignatureProperties =  new SignatureProperties
                                    {
                                        PartialName = "signature12",
                                        Height = 20,
                                        Width = 115
                                    }
                                }
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
