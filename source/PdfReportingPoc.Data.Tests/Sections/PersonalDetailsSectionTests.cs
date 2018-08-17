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
    public class PersonalDetailsSectionTests
    {
        public PersonalDetailsSectionTests()
        {
            var license = new License();
            license.SetLicense("Aspose.Pdf.Lic");
        }

        [Test]
        public void Render_GivenPersonalDetails_ShouldCreateFirstTableForPersonalDetailsSection()
        {
            //Arrange
            var fileName = "blank.pdf";
            var fileBytes = GetFileBytes(fileName);
            var table = GetFirstTableForPersonalDetailsSection();

            //Act
            var actual = table.Render(fileBytes);

            //Assert
            var expected = fileBytes.PdfBytes.Length;
            actual.PdfBytes.Length.Should().BeGreaterThan(expected);
        }

        [Test]
        public void Render_GivenPersonalDetails_ShouldCreateSecondTableForPersonalDetailsSection()
        {
            //Arrange
            var fileName = "blank.pdf";
            var fileBytes = GetFileBytes(fileName);
            var table = GetSecondTableForPersonalDetailsSection();

            //Act
            var actual = table.Render(fileBytes);

            //Assert
            var expected = fileBytes.PdfBytes.Length;
            actual.PdfBytes.Length.Should().BeGreaterThan(expected);
        }

        [Test]
        public void Render_GivenPersonalDetails_ShouldCreateThirdTableForPersonalDetailsSection()
        {
            //Arrange
            var fileName = "blank.pdf";
            var fileBytes = GetFileBytes(fileName);
            var table = GetThirdTableForPersonalDetailsSection();

            //Act
            var actual = table.Render(fileBytes);

            //Assert
            var expected = fileBytes.PdfBytes.Length;
            actual.PdfBytes.Length.Should().BeGreaterThan(expected);
        }


        private Table GetFirstTableForPersonalDetailsSection()
        {
            var regularTextProperties = new TextProperties
            {
                FontSize = 10,
                FontType = "Arial",
                FontStyle = FontStyles.Regular,
                FontColor = Color.Black,
                Invisible = false
            };
            var boldTextProperties = new TextProperties
            {
                FontSize = 12,
                FontType = "Arial",
                FontStyle = FontStyles.Bold,
                FontColor = Color.DarkGray,
                Invisible = false
            };

            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 100f,
                    Widths = "142 142 142",
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
                                 DisplayText = "Name:",
                                 TextProperties = regularTextProperties
                             },
                            new TextBox
                             {
                               Id = "txtName",
                               TextBoxProperties =  new TextBoxProperties
                                {
                                  Height = 20,
                                  Width = 137,
                                  Required = true,
                                  MaxLen =50,
                                  ForceComb = false,
                                  Value = "Solomon"
                                },
                             },
                            new Text
                             {
                                 DisplayText = "",
                                 TextProperties = regularTextProperties
                             },
                           }
                       },
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                             new Text
                             {
                                 DisplayText = "ID Number:",
                                 TextProperties = regularTextProperties
                             },
                             new TextBox
                                {
                                    Id = "txtIdNumber",
                                    TextBoxProperties =  new TextBoxProperties
                                    {
                                            Height = 20,
                                            Width = 137,
                                            MaxLen = 13,
                                            Required = true,
                                            ForceComb = false,
                                            Type ="Number",
                                            NumberFormatProperties = new NumberFormatProperties
                                           {
                                            NumberOfPlacesAfterDecimalPoint = 0,
                                            SeparateWithComma = false,
                                            NegativeStyle = NegativeNumberFormat.Red
                                           },
                                            Value ="6501050549084"
                                        },
                                 },
                              new Text
                             {
                                 DisplayText = "",
                                 TextProperties = boldTextProperties
                             },
                           }
                       },
                     new Row
                    {
                       Cells = new List<ICell>
                       {
                             new Text
                             {
                                 DisplayText = "Contact Details",
                                 TextProperties = boldTextProperties
                             },
                             new Text
                             {
                                 DisplayText = "",
                                 TextProperties = regularTextProperties
                             },
                             new Text
                             {
                                 DisplayText = "Please update if changed:",
                                 TextProperties = boldTextProperties
                             },
                           }
                       },
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                             new Text
                             {
                                 DisplayText = "Home No:",
                                 TextProperties = regularTextProperties
                             },
                             new TextBox
                                {
                                  Id = "txtProvidedHomeNumber",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      MaxLen = 10,
                                      Required = true,
                                      ForceComb = false,
                                      Value = "0356574433"
                                     },
                                 },
                            new TextBox
                                {
                                  Id = "txtUpdatedHomeNumber",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      MaxLen = 10,
                                      Required = true,
                                      ForceComb = false,
                                    },
                                 },
                           }
                       },
                     new Row
                    {
                       Cells = new List<ICell>
                       {
                             new Text
                             {
                                 DisplayText = "Work Tel Number:",
                                 TextProperties = regularTextProperties
                             },
                             new TextBox
                                {
                                  Id = "txtProvidedWorkNumber",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      MaxLen = 10,
                                      Required = true,
                                      ForceComb = false,
                                      Value = "0317567485",
                                     },
                                 },
                            new TextBox
                                {
                                  Id = "txtUpdatedWorkNumber",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      MaxLen = 10,
                                      Required = true,
                                      ForceComb = false,
                                     },
                                 },
                           }
                       },
                     new Row
                    {
                       Cells = new List<ICell>
                       {
                             new Text
                             {
                                 DisplayText = "Cell Number:",
                                 TextProperties = regularTextProperties
                             },
                             new TextBox
                                {
                                  Id = "txtProvidedCellNumber",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      MaxLen = 10,
                                      Required = true,
                                      ForceComb = false,
                                      Value = "0834567856",
                                    },
                                 },
                            new TextBox
                                {
                                  Id = "txtUpdatedCellNumber",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      MaxLen = 10,
                                      Required = true,
                                      ForceComb = false,
                                    },
                                 },
                           }
                       },
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                             new Text
                             {
                                 DisplayText = "Email Address:",
                                 TextProperties = regularTextProperties
                             },
                             new TextBox
                                {
                                  Id = "txtProvidedEmailAddress",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      Required = true,
                                      ForceComb = false,
                                      MaxLen =50,
                                      Value = "Tebogo@gmail.com"
                                     },
                                 },
                            new TextBox
                                {
                                  Id = "txtUpdatedEmailAddress",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      Required = true,
                                      MaxLen =50,
                                      ForceComb = false,
                                     },
                                 },
                           }
                       },

                  }
            };
        }

        private Table GetSecondTableForPersonalDetailsSection()
        {
            var regularTextProperties = new TextProperties
            {
                FontSize = 10,
                FontType = "Arial",
                FontStyle = FontStyles.Regular,
                FontColor = Color.Black,
                Invisible = false
            };
            var boldTextProperties = new TextProperties
            {
                FontSize = 12,
                FontType = "Arial",
                FontStyle = FontStyles.Bold,
                FontColor = Color.DarkGray,
                Invisible = false
            };

            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 100f,
                    Widths = "284 142",
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
                                 DisplayText = "Residential Address:",
                                 TextProperties = boldTextProperties
                             },
                             new Text
                             {
                                 DisplayText = "",
                                 TextProperties = boldTextProperties
                             },
                           }
                       },
                      new Row
                    {
                       Cells = new List<ICell>
                       {
                             new TextBox
                                {
                                  Id = "txtProvidedStreetName",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      MaxLen =50,
                                      Required = true,
                                      ForceComb = false,
                                      Value = "376 Anton Lembede"
                               
                                     },
                                 },
                            new TextBox
                                {
                                  Id = "txtUpdatedStreetName",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      MaxLen =50,
                                      Required = true,
                                      ForceComb = false,
                                     },
                                 },
                           }
                       },
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                             new TextBox
                                {
                                  Id = "txtProvidedCity",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      MaxLen =50,
                                      Required = true,
                                      ForceComb = false,
                                      Value = "Durban"
                                     },
                                 },
                            new TextBox
                                {
                                  Id = "txtUpdatedCity",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      MaxLen =50,
                                      Required = true,
                                      ForceComb = false,
                                     },
                                 },
                           }
                       },
                      new Row
                    {
                       Cells = new List<ICell>
                       {
                             new TextBox
                                {
                                  Id = "txtProvidedCode",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      Required = true,
                                      MaxLen =50,
                                      ForceComb = false,
                                      Value ="4001"
                                     },
                                 },
                            new TextBox
                                {
                                  Id = "txtUpdatedCode",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      Required = true,
                                      MaxLen =50,
                                      ForceComb = false,
                                     },
                                 },
                           }
                       },
                        new Row
                    {
                       Cells = new List<ICell>
                       {
                             new TextBox
                                {
                                  Id = "txtProvidedCountry",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      Required = true,
                                      MaxLen =50,
                                      ForceComb = false,
                                      Value = "South Africa"
                                     },
                                 },
                            new TextBox
                                {
                                  Id = "txtUpdatedCountry",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      MaxLen =50,
                                      Required = true,
                                      ForceComb = false,
                                     },
                                 },
                            }

                       },

                  }
            };
        }

        private Table GetThirdTableForPersonalDetailsSection()
        {
            var firstRowOptionItemProperties = new OptionItemProperties
            {
                Height = 20,
                Width = 20,
                Style = BoxStyle.Check
            };
            var secondRowOptionItemProperties = new OptionItemProperties
            {
                Height = 20,
                Width = 20,
                Style = BoxStyle.Check
            };
            var thirdRowOptionItemProperties = new OptionItemProperties
            {
                Height = 20,
                Width = 20,
                Style = BoxStyle.Check
            };
            var regularTextProperties = new TextProperties
            {
                FontSize = 10,
                FontType = "Arial",
                FontStyle = FontStyles.Regular,
                FontColor = Color.Black,
                Invisible = false
            };

            var boldTextProperties = new TextProperties
            {
                FontSize = 12,
                FontType = "Arial",
                FontStyle = FontStyles.Bold,
                FontColor = Color.DarkGray,
                Invisible = false
            };

            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 100f,
                    Widths = "142 27 24 27 24 90 126",
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
                                 DisplayText = "Declarations:",
                                 TextProperties = boldTextProperties
                             },
                             new Text
                             {
                                 DisplayText = "",
                                 TextProperties = regularTextProperties
                             },
                              new Text
                             {
                                 DisplayText = "",
                                 TextProperties = regularTextProperties
                             },
                             new Text
                             {
                                 DisplayText = "",
                                 TextProperties = regularTextProperties
                             },
                             new Text
                             {
                                 DisplayText = "",
                                 TextProperties = regularTextProperties
                             },
                              new Text
                             {
                                 DisplayText = "",
                                 TextProperties = regularTextProperties
                             },
                             new Text
                             {
                                 DisplayText = "",
                                 TextProperties = regularTextProperties
                             },
                           }
                       },
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                             new Text
                             {
                                 DisplayText = "Have you ever been declared insolvent?",
                                 TextProperties = regularTextProperties
                             },
                             new OptionGroup
                            {
                               OptionCells =  new List<ICell>{
                                   new Text
                                   {
                                      DisplayText = "Yes",
                                      TextProperties = regularTextProperties
                                   },
                                   new OptionItem
                                   {
                                      OptionName = "yesRehabilitatedRadioBtn",
                                      Id = "yesRehabilitatedTxtBox",
                                      OptionItemProperties = firstRowOptionItemProperties
                                   },
                                    new Text
                                   {
                                      DisplayText = "No",
                                      TextProperties = regularTextProperties
                                   },
                                   new OptionItem
                                   {
                                       Id = "NoRehabilitatedTxtBox",
                                       OptionName = "NoRehabilitatedRadioBtn",
                                       OptionItemProperties = firstRowOptionItemProperties
                                   }
                               }
                            },

                             new Text
                             {
                                 DisplayText = "Date Rehabilitated:",
                                 TextProperties = regularTextProperties
                             },
                             new TextBox
                              {
                                 Id = "txtRehabilitatedDate",
                                 TextBoxProperties =  new TextBoxProperties
                                 {
                                     Height = 16,
                                     Width = 90,
                                     Required = true,
                                     ForceComb = false,
                                     Type ="Date",
                                     MaxLen = 10
                                   },
                                 },
                             }
                        },
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                             new Text
                             {
                                 DisplayText = "Have you ever been under an administrative order?",
                                 TextProperties = regularTextProperties
                             },
                             new OptionGroup
                            {
                               OptionCells =  new List<ICell>{
                                   new Text
                                   {
                                      DisplayText = "Yes",
                                      TextProperties = regularTextProperties
                                   },
                                   new OptionItem
                                   {
                                      OptionName = "yesRescindedRadioBtn",
                                      Id = "yesRescindedTxtBox",
                                      OptionItemProperties = secondRowOptionItemProperties
                                   },
                                    new Text
                                   {
                                      DisplayText = "No",
                                      TextProperties = regularTextProperties
                                   },
                                   new OptionItem
                                   {
                                       Id = "NoRescindedTxtBox",
                                       OptionName = "NoRescindedRadioBtn",
                                       OptionItemProperties = secondRowOptionItemProperties
                                   }
                               }
                            },
                             new Text
                             {
                                 DisplayText = "Date Order Rescinded:",
                                 TextProperties = regularTextProperties
                             },
                             new TextBox
                             {
                                Id = "txtRescindedDate",
                                TextBoxProperties =  new TextBoxProperties
                                 {
                                  Height = 16,
                                  Width = 90,
                                  Required = true,
                                  ForceComb = false,
                                  Type ="Date",
                                  MaxLen = 10
                                 },
                              },
                           }
                       },
                   new Row
                    {
                       Cells = new List<ICell>
                       {
                             new Text
                             {
                                 DisplayText = "Are you currently under debt counselling or debt review?",
                                 TextProperties = regularTextProperties
                             },
                             new OptionGroup
                            {
                               OptionCells =  new List<ICell>{
                                   new Text
                                   {
                                      DisplayText = "Yes",
                                      TextProperties = regularTextProperties
                                   },
                                   new OptionItem
                                   {
                                      OptionName = "yesDebtRadioBtn",
                                      Id = "yesDebtTxtBox",
                                      OptionItemProperties = thirdRowOptionItemProperties
                                   },
                                    new Text
                                   {
                                      DisplayText = "No",
                                      TextProperties = regularTextProperties
                                   },
                                   new OptionItem
                                   {
                                       Id = "NoDebtTxtBox",
                                       OptionName = "NoDebtRadioBtn",
                                       OptionItemProperties = thirdRowOptionItemProperties
                                   }
                               }
                            },
                            new Text
                             {
                                 DisplayText = "",
                                 TextProperties = regularTextProperties
                             },
                             new Text
                             {
                                 DisplayText = "",
                                 TextProperties = regularTextProperties
                             },
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
