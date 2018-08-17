using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;
using FluentAssertions;
using NUnit.Framework;
using PdfReportingPoc.Domain.Elements;
using PdfReportingPoc.Domain.Elements.Cell;
using PdfReportingPoc.Domain.Elements.Cells;
using PdfReportingPoc.Elements;
using PdfReportingPoc.Elements.Cells;
using PdfReportingPoc.Elements.Fragments;
using Row = PdfReportingPoc.Elements.Row;
using Signature = PdfReportingPoc.Elements.Cells.Signature;
using Table = PdfReportingPoc.Elements.Table;

namespace PdfReportingPoc.Data.Tests.Demo
{
    [TestFixture]
    public class DemoScenarioTests
    {
        [Test]
        public void GivenOnePerson_ShouldDisplayApplicantDetails()
        {
            // Arrange
            var resultLocation = "C:\\Systems\\demo.pdf";
            var fileName = "blank.pdf";
            var fileBytes = GetFileBytes(fileName);
            var logoBytes = GetFileBytes("right-justified-logo.png");
            var applicationId = Guid.NewGuid().ToString();

            var textProperties = CreateDefaultTextProperties();
            var columnHeadingTextProperties = CreateBoldTextProperties();

            var report = new Report
            {
                Password = "1234",
                Header = CreateHeader(logoBytes),
                Sections =
                {
                    CreateApplicationDetailsSection(textProperties),
                    CreateSignitoriesSection(textProperties),
                    CreateDisbursementSection(textProperties),
                    CreateHomeOwnersCoverSection(textProperties),
                    CreatePersonalDetailsSection(textProperties, columnHeadingTextProperties),
                    CreateTermsAndConditionsSection(textProperties),
                    CreateSupportingDocumentsSection(),
                    CreateSubmitApplicationSection(textProperties),
                    CreateContactUsSection(textProperties),
                    CreateApplicationTrackingSection(textProperties, applicationId)
            },
                Footer = CreateFooter()
            };
            // Act
            var actual = report.Render(fileBytes);
            // Assert
            actual.Length.Should().BeGreaterThan(0);
        }

        private Section CreateApplicationTrackingSection(TextProperties textProperties, string applicationId)
        {
            return new Section
            {
                Name = "Qr Code",
                Tables =
                {
                    GetHeading("Application Tracking:"),
                    GetQrCode(textProperties,applicationId)
                }
            };
        }

        private TextProperties CreateDefaultTextProperties()
        {
            var textProperties = new TextProperties
            {
                FontSize = 10,
                FontType = "Arial",
                FontStyle = FontStyles.Regular,
                FontColor = Color.Black,
                Invisible = false
            };
            return textProperties;
        }

        private TextProperties CreateBoldTextProperties()
        {
            var boldTextProperties = new TextProperties
            {
                FontSize = 12,
                FontType = "Arial",
                FontStyle = FontStyles.Bold,
                FontColor = Color.DarkGray,
                Invisible = false
            };
            return boldTextProperties;
        }

        private Section CreateSupportingDocumentsSection()
        {
            var supportingDocumentsSection = new Section
            {
                Name = "Supporting Documents",
                Tables =
                {
                    GetHeading("Supporting Documents:"),
                    GetAdditionalDocumentationCheckListTable(),
                    GetPropertyDocumentationCheckListTable(),
                }
            };
            return supportingDocumentsSection;
        }

        private Section CreateTermsAndConditionsSection(TextProperties textProperties)
        {
            var file = "termsAndConditions.txt";
            var baseDirectory = TestContext.CurrentContext.TestDirectory + "\\TestData\\";
            var currentFilePath = Path.Combine(baseDirectory, file);
            var termsAndConditionsLines = File.ReadAllLines(currentFilePath);

            var termsAndConditionsSection = new Section
            {
                Name = "Terms and Conditions",
                Tables =
                {
                    GetHeading("Terms And Conditions:"),
                    GetTermsAndConditionsSubHeading(),
                }
            };

            termsAndConditionsSection.Tables.AddRange(GetTermsAndConditionsTables(termsAndConditionsLines));
            termsAndConditionsSection.Tables.Add(GetTsAndCsSignatureTable(textProperties));
            return termsAndConditionsSection;
        }

        private Section CreateContactUsSection(TextProperties textProperties)
        {
            var contactUsSection = new Section
            {
                Name = "Contact Us",
                Tables =
                {
                    GetHeading("Contact Us:"),
                    GetContactUsSection(textProperties)
                }
            };
            return contactUsSection;
        }

        private Section CreateSubmitApplicationSection(TextProperties textProperties)
        {
            var submitApplicationSection = new Section
            {
                Name = "Submit Application",
                Tables =
                {
                    GetHeading("Submit Your Application:"),
                    GetSubmitApplicationSection(textProperties)
                }
            };
            return submitApplicationSection;
        }

        private Section CreatePersonalDetailsSection(TextProperties textProperties, TextProperties boldTextProperties)
        {
            var personalDetailsSection = new Section
            {
                Name = "Personal Details",
                Tables =
                {
                    GetHeading("Personal Details:"),
                    GetPersonalDetailsSection(textProperties, boldTextProperties),
                    GetResidentialAddressSection(boldTextProperties),
                    GetDeclarationsSection(textProperties, boldTextProperties)
                }
            };
            return personalDetailsSection;
        }

        private Section CreateHomeOwnersCoverSection(TextProperties textProperties)
        {
            var homeOwnersCoverSection = new Section
            {
                Name = "HomeOwnersCover",
                Tables =
                {
                    GetHeading("Home Owners Cover:"),
                    GetHomeOwnersCoverSection(textProperties)
                }
            };
            return homeOwnersCoverSection;
        }

        private Section CreateDisbursementSection(TextProperties textProperties)
        {
            var disbursementSection = new Section
            {
                Name = "Disbursement",
                Tables =
                {
                    GetHeading("Disbursement Bank Account:"),
                    GetDisbursementSectionDescription(textProperties),
                    GetBankingDetailsSection(textProperties),
                    GetBankAccountTypeSection(textProperties)
                }
            };
            return disbursementSection;
        }

        private Section CreateSignitoriesSection(TextProperties textProperties)
        {
            var signitoriesSection = new Section
            {
                Name = "Signitories",
                Tables =
                {
                    GetHeading("Signature:"),
                    GetSignatureSection(textProperties)
                }
            };
            return signitoriesSection;
        }

        private Section CreateApplicationDetailsSection(TextProperties textProperties)
        {
            var applicationDetailsSection = new Section
            {
                Name = "Application Details",
                Tables =
                {
                    GetHeading("Application Details:"),
                    GetApplicationDetailsSection(textProperties)
                }
            };
            return applicationDetailsSection;
        }

        private Footer CreateFooter()
        {
            var footer = new Footer
            {
                InsertPageNumbers = true,
                TextFragements = new List<FooterFragment>
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
                                SpacesNeeded = 70
                            },
                            new DocumentBoundryText
                            {
                                Text = "Reload Application Form",
                                SpacesNeeded = 0
                            }
                        }
                    }
                }
            };
            return footer;
        }

        private Header CreateHeader(byte[] logoBytes)
        {
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
                            Text = "Reload Application Form"
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

            var header = new Header
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

            return header;
        }

        private Table GetApplicationDetailsSection(TextProperties textProperties)
        {
            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Position = TablePosition.Relative,
                    Top = 2,
                    Left = 0,
                    Widths = "160 250",
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
                                 DisplayText = "Requested Loan Amount:",
                                 TextProperties = textProperties
                             },
                             new TextBox
                             {
                                 Id = "txtAmount",
                                 TextBoxProperties =  new TextBoxProperties
                                 {
                                       Height = 20,
                                       Width = 248,
                                       MaxLen = 22,
                                       Required = true,
                                       ForceComb = false,
                                     Type = "number"
                                 }
                             },
                        }
                    },
                    new Row
                    {
                        Cells = new List<ICell>
                        {
                             new Text {
                                 DisplayText = "What will you use this loan for?",
                                 TextProperties = textProperties
                             },
                             new TextBox
                             {
                                 Id = "txtReasonForLoan",
                                 TextBoxProperties =  new TextBoxProperties
                                 {
                                        Height = 20,
                                        Width = 248,
                                        MaxLen = 22,
                                        Required = true,
                                        ForceComb = false,
                                     Type = "number"
                                 }
                             }
                        }
                    }
                }
            };
        }

        private Table GetHeading(string heading)
        {
            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 10,
                    Position = TablePosition.Relative,
                    Left = 0,
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
                                DisplayText = heading,
                                TextProperties = new TextProperties
                                {
                                    FontSize = 14,
                                    FontType = "Arial",
                                    FontStyle = FontStyles.Bold,
                                    FontColor = Color.Black,
                                    Invisible = false
                                }

                            }
                        }
                    }
                }
            };
        }

        private Table GetSignatureSection(TextProperties textProperties)
        {
            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 5,
                    Position = TablePosition.Relative,
                    Left = 0,
                    Widths = "160 250",
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
                                    DisplayText = "Title:",
                                    TextProperties = textProperties

                                },
                                new Text {
                                    DisplayText = "Mr",
                                    TextProperties = textProperties
                                },
                            }
                    },
                    new Row
                    {
                        Cells = new List<ICell>
                            {
                                new Text {
                                    DisplayText = "Full Name:",
                                    TextProperties = textProperties

                                },
                                new Text {
                                    DisplayText = "Bob Marley",
                                    TextProperties = textProperties
                                }
                            }
                    },
                    new Row
                    {
                        Cells = new List<ICell>
                            {
                                new Text {
                                    DisplayText = "Identity Number:",
                                    TextProperties = textProperties

                                },
                                new Text {
                                    DisplayText = "904506349993",
                                    TextProperties = textProperties
                                }
                            }
                    },
                    new Row
                    {
                        Cells = new List<ICell>
                            {
                                new Text {
                                    DisplayText = "Role:",
                                    TextProperties = textProperties

                                },
                                new Text {
                                    DisplayText = "Applicant",
                                    TextProperties = textProperties
                                }
                            }
                    },
                    new Row
                    {
                        Cells = new List<ICell>
                            {
                                new Text {
                                    DisplayText = "Signature:",
                                    TextProperties = textProperties

                                },
                                new Signature
                                {
                                    SignatureProperties =  new SignatureProperties
                                    {
                                        PartialName = "signature1",
                                        Height = 30,
                                        Width = 248,
                                    }
                                }
                            }
                    }
                }
            };
        }

        private Table GetDisbursementSectionDescription(TextProperties textProperties)
        {
            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 5,
                    Left = 0,
                    Position = TablePosition.Relative,
                    Widths = "410",
                    CellBorder = new BorderInfo(BorderSide.None, 0.20f),
                    ColumnAdjustment = ColumnAdjustment.Customized
                },
                Rows = new List<IRow>
                {
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

        private Table GetBankingDetailsSection(TextProperties textProperties)
        {
            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 10,
                    Left = 0,
                    Position = TablePosition.Relative,
                    Widths = "75 130 70 135",
                    CellBorder = new BorderInfo(BorderSide.None, 0.20f),
                    ColumnAdjustment = ColumnAdjustment.Customized
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
                                            Width = 128,
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
                                            Width = 132,
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
                                            Width = 128,
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
                                            Width = 132,
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

        private Table GetBankAccountTypeSection(TextProperties textProperties)
        {
            var optionItemProperties = new OptionItemProperties
            {
                Height = 20,
                Width = 20,
                Style = BoxStyle.Check
            };

            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 10,
                    Left = 0,
                    Position = TablePosition.Relative,
                    Widths = "110 78 22 80 22 75 22",
                    CellBorder = new BorderInfo(BorderSide.None, 0.20f),
                    ColumnAdjustment = ColumnAdjustment.Customized
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

        private Table GetHomeOwnersCoverSection(TextProperties textProperties)
        {
            var optionItemProperties = new OptionItemProperties
            {
                Height = 20,
                Width = 20,
                Style = BoxStyle.Check
            };

            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 5,
                    Position = TablePosition.Relative,
                    Left = 0,
                    Widths = "390 22",
                    CellBorder = new BorderInfo(BorderSide.None, 0.20f, Color.Black),
                    ColumnAdjustment = ColumnAdjustment.Customized
                },
                Rows = new List<IRow>
                {
                    new Row
                    {
                        Cells=new List<ICell>
                        {
                            new Text
                            {
                                DisplayText = "We require proof of building insurance (Home Owners Cover)."
                                              + Environment.NewLine +
                                              "Alternatively, SA Home Loans can provide Bond Protection.",
                                TextProperties= textProperties
                            }
                        }
                    },
                    new Row
                    {
                        Cells=new List<ICell>
                        {
                            new Text
                            {
                                DisplayText = "I already have building insurance (Home Owners Cover)",
                                TextProperties= textProperties
                            },
                            new OptionGroup
                            {
                                OptionCells = new List<ICell>{
                                    new OptionItem
                                    {
                                        OptionName = "haveHoc",
                                        Id = "haveHoc",
                                        OptionItemProperties = optionItemProperties
                                    }
                                }
                            }
                        }
                    },
                    new Row
                    {
                        Cells=new List<ICell>
                        {
                            new Text
                            {
                                DisplayText = "I want to apply for Bond Protection with SA Home Loans",
                                TextProperties= textProperties
                            },
                            new OptionGroup
                            {
                                OptionCells = new List<ICell>{
                                    new OptionItem
                                    {
                                        OptionName = "sahlHoc",
                                        Id = "sahlHoc",
                                        OptionItemProperties = optionItemProperties
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

        private Table GetPersonalDetailsSection(TextProperties textProperties, TextProperties boldTextProperties)
        {

            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 10,
                    Position = TablePosition.Relative,
                    Left = 0,
                    Widths = "125 150 150",
                    CellBorder = new BorderInfo(BorderSide.None, 0.20f),
                    ColumnAdjustment = ColumnAdjustment.Customized

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
                                 TextProperties = textProperties
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
                                  Value = "Bob Marley",
                                  ReadOnly = true
                                }
                             },
                            new Text
                             {
                                 DisplayText = "",
                                 TextProperties = textProperties
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
                                 TextProperties = textProperties
                             },
                             new TextBox
                                {
                                    Id = "txtIdNumber",
                                    TextBoxProperties =  new TextBoxProperties
                                    {
                                            Height = 20,
                                            Width = 137,
                                            MaxLen = 13,
                                            Required = false,
                                            ForceComb = false,
                                            Type ="Number",
                                            NumberFormatProperties = new NumberFormatProperties
                                           {
                                            NumberOfPlacesAfterDecimalPoint = 0,
                                            SeparateWithComma = false,
                                            NegativeStyle = NegativeNumberFormat.Red
                                           },
                                            Value ="904506349993",
                                            ReadOnly = true
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
                                 TextProperties = textProperties
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
                                 TextProperties = textProperties
                             },
                             new TextBox
                                {
                                  Id = "txtProvidedHomeNumber",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      MaxLen = 10,
                                      Required = false,
                                      ForceComb = false,
                                      Value = "0356574433",
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
                                      Required = false,
                                      ForceComb = true,
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
                                 TextProperties = textProperties
                             },
                             new TextBox
                                {
                                  Id = "txtProvidedWorkNumber",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      MaxLen = 10,
                                      Required = false,
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
                                      Required = false,
                                      ForceComb = true,
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
                                 TextProperties = textProperties
                             },
                             new TextBox
                                {
                                  Id = "txtProvidedCellNumber",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      MaxLen = 10,
                                      Required = false,
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
                                      Required = false,
                                      ForceComb = true,
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
                                 TextProperties = textProperties
                             },
                             new TextBox
                                {
                                  Id = "txtProvidedEmailAddress",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      Required = false,
                                      ForceComb = false,
                                      MaxLen =50,
                                      Value = "bob@gmail.com",
                                      ReadOnly = true
                                     },
                                 },
                            new TextBox
                                {
                                  Id = "txtUpdatedEmailAddress",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 137,
                                      Required = false,
                                      MaxLen =50,
                                      ForceComb = false,
                                     },
                                 },
                           }
                       },
                  }
            };
        }

        private Table GetResidentialAddressSection(TextProperties boldTextProperties)
        {
            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 10,
                    Position = TablePosition.Relative,
                    Left = 0,
                    Widths = "207 207",
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
                                 DisplayText = "Please update if changed:",
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
                                      Width = 203,
                                      MaxLen =50,
                                      Required = false,
                                      ForceComb = false,
                                      Value = "376 Anton Lembede",
                                      ReadOnly = true
                                     },
                                 },
                            new TextBox
                                {
                                  Id = "txtUpdatedStreetName",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 203,
                                      MaxLen =50,
                                      Required = false,
                                      ForceComb = false
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
                                      Width = 203,
                                      MaxLen =50,
                                      Required = false,
                                      ForceComb = false,
                                      Value = "Durban",
                                      ReadOnly = true
                                     },
                                 },
                            new TextBox
                                {
                                  Id = "txtUpdatedCity",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 203,
                                      MaxLen =50,
                                      Required = false,
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
                                      Width = 203,
                                      Required = false,
                                      MaxLen =50,
                                      ForceComb = false,
                                      Value ="4001",
                                      ReadOnly = true
                                     },
                                 },
                            new TextBox
                                {
                                  Id = "txtUpdatedCode",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 203,
                                      Required = false,
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
                                      Width = 203,
                                      Required = false,
                                      MaxLen =50,
                                      ForceComb = false,
                                      Value = "South Africa",
                                      ReadOnly = true
                                     },
                                 },
                            new TextBox
                                {
                                  Id = "txtUpdatedCountry",
                                  TextBoxProperties =  new TextBoxProperties
                                    {
                                      Height = 20,
                                      Width = 203,
                                      MaxLen =50,
                                      Required = false,
                                      ForceComb = false,
                                      
                                     },
                                 },
                            }

                       },
                  }
            };
        }

        private Table GetDeclarationsSection(TextProperties textProperties, TextProperties boldTextProperties)
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

            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 10,
                    Position = TablePosition.Relative,
                    Left = 0,
                    Widths = "142 27 24 26 24 90 118",
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
                                 TextProperties = textProperties
                             },
                              new Text
                             {
                                 DisplayText = "",
                                 TextProperties = textProperties
                             },
                             new Text
                             {
                                 DisplayText = "",
                                 TextProperties = textProperties
                             },
                             new Text
                             {
                                 DisplayText = "",
                                 TextProperties = textProperties
                             },
                              new Text
                             {
                                 DisplayText = "",
                                 TextProperties = textProperties
                             },
                             new Text
                             {
                                 DisplayText = "",
                                 TextProperties = textProperties
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
                                 TextProperties = textProperties
                             },
                             new OptionGroup
                            {
                               OptionCells =  new List<ICell>{
                                   new Text
                                   {
                                      DisplayText = "Yes",
                                      TextProperties = textProperties
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
                                      TextProperties = textProperties
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
                                 TextProperties = textProperties
                             },
                             new TextBox
                              {
                                 Id = "txtRehabilitatedDate",
                                 TextBoxProperties =  new TextBoxProperties
                                 {
                                     Height = 20,
                                     Width = 79,
                                     Required = false,
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
                                 TextProperties = textProperties
                             },
                             new OptionGroup
                            {
                               OptionCells =  new List<ICell>{
                                   new Text
                                   {
                                      DisplayText = "Yes",
                                      TextProperties = textProperties
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
                                      TextProperties = textProperties
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
                                 TextProperties = textProperties
                             },
                             new TextBox
                             {
                                Id = "txtRescindedDate",
                                TextBoxProperties =  new TextBoxProperties
                                 {
                                  Height = 20,
                                  Width = 79,
                                  Required = false,
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
                                 TextProperties = textProperties
                             },
                             new OptionGroup
                            {
                               OptionCells =  new List<ICell>{
                                   new Text
                                   {
                                      DisplayText = "Yes",
                                      TextProperties = textProperties
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
                                      TextProperties = textProperties
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
                                 TextProperties = textProperties
                             },
                             new Text
                             {
                                 DisplayText = "",
                                 TextProperties = textProperties
                             },
                           }
                       },
                  }
            };
        }

        private Table GetTermsAndConditionsSubHeading()
        {
            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 10,
                    Position = TablePosition.Relative,
                    Left = 0,
                    CellBorder = new BorderInfo(BorderSide.None, 0.20f),
                    ColumnAdjustment = ColumnAdjustment.AutoFitToWindow
                },
                Rows = new List<IRow>
                {
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                           new Text {
                               DisplayText = "I hereby acknowledge and warrant that:",
                               TextProperties = new TextProperties
                               {
                                   FontSize = 10,
                                   FontType = "Arial",
                                   FontStyle = FontStyles.Bold,
                                   FontColor = Color.Black,
                                   Invisible = false
                               }
                           }
                       }
                    },
                }
            };
        }

        private List<Table> GetTermsAndConditionsTables(IEnumerable<string> lines)
        {
            var tables = new List<Table>();
            foreach (var line in lines)
            {
                var table = new Table
                {
                    Layout = new TableLayout
                    {
                        CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                        Top = 2,
                        Position = TablePosition.Relative,
                        Left = 0,
                        CellBorder = new BorderInfo(BorderSide.None, 0.05f),
                        ColumnAdjustment = ColumnAdjustment.AutoFitToWindow
                    },
                    Rows = new List<IRow>
                {
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                           new Text {
                               DisplayText = line,
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
                tables.Add(table);
            }
            return tables;
        }

        private Table GetTsAndCsSignatureTable(TextProperties textProperties)
        {
            var headingTextPropeties = new TextProperties
            {
                FontSize = 10,
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
                    Top = 10,
                    Position = TablePosition.Relative,
                    Left = 0,
                    Widths = "220 80 150",
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
                            DisplayText = "FULL NAME & ID NUMBER ",
                             TextProperties = headingTextPropeties
                        },
                        new Text {
                            DisplayText = "DATE",
                             TextProperties = headingTextPropeties
                        },
                        new Text {
                            DisplayText = "SIGNATURE",
                             TextProperties = headingTextPropeties
                        },
                     }
                  },
                   new Row
                   {
                      Cells = new List<ICell>
                      {
                                new Text {
                                    DisplayText = $"Mr Bob Marley{Environment.NewLine}ID: 904506349993",
                                    TextProperties = textProperties
                                },
                                new TextBox
                                {
                                    Id = "txtSignedDate",
                                    TextBoxProperties =  new TextBoxProperties
                                    {
                                            Height = 20,
                                            Width = 77,
                                            Required = true,
                                            ForceComb = false,
                                            MaxLen = 10,
                                            Type="Date"
                                     },
                                },
                                new Signature
                                {
                                    SignatureProperties =  new SignatureProperties
                                    {
                                        PartialName = "signature2",
                                        Height = 20,
                                        Width = 110
                                    }
                                }
                            }
                   }
               }
            };
        }

        private Table GetPropertyDocumentationCheckListTable()
        {
            var headingTextProperties = new TextProperties
            {

                FontSize = 10,
                FontType = "Arial",
                FontStyle = FontStyles.Bold,
                FontColor = Color.DarkGray,
                Invisible = false
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
                    Top = 10,
                    Left = 0,
                    Position = TablePosition.Relative,
                    Widths = "360 70",
                    CellBorder = new BorderInfo(BorderSide.All, 0.20f),
                },
                Rows = new List<IRow>
                {
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                            new Text {
                                DisplayText = "Additional documentation required if your bond is held in the name of a Trust",
                                TextProperties = headingTextProperties
                            },
                            new Text {
                                DisplayText = "Checklist",
                                TextProperties = headingTextProperties
                            },
                        }
                    },
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                            new Text {
                                DisplayText ="      • A resolution giving permission for this loan, signed by all trustees",
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

        private Table GetAdditionalDocumentationCheckListTable()
        {
            var headingTextProperties = new TextProperties
            {

                FontSize = 10,
                FontType = "Arial",
                FontStyle = FontStyles.Bold,
                FontColor = Color.DarkGray,
                Invisible = false
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
                    Top = 10,
                    Left = 0,
                    Position = TablePosition.Relative,
                    Widths = "360 70",
                    CellBorder = new BorderInfo(BorderSide.All, 0.20f),
                },
                Rows = new List<IRow>
                {
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                            new Text {
                                DisplayText = "Property Documentation",
                                TextProperties = headingTextProperties
                            },
                            new Text {
                                DisplayText = "Checklist",
                                TextProperties = headingTextProperties
                            },
                       }
                    },
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                                new Text {
                                    DisplayText = @"If you do not intend to apply for Homeowners Cover from SA Home Loans, please provide:

      • Current building insurance policy document",
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

        private Table GetSubmitApplicationSection(TextProperties textProperties)
        {
            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 5,
                    Position = TablePosition.Relative,
                    Left = 0,
                    Widths = "441",
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
                                        DisplayText = @"Please note: should this application not be sent to us within 90 days, a new updated application form will be required

Email your application to: fl @sahomeloans.com

Fax your application to: 0865526888",
                                    TextProperties = textProperties
                             }
                         }
                    },
                }
            };
        }

        private Table GetContactUsSection(TextProperties textProperties)
        {
            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 5,
                    Position = TablePosition.Relative,
                    Left = 0,
                    Widths = "441",
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
                               DisplayText = @"Please quote your account number when submitting an enquiry.

Email: fl@sahomeloans.com

Telephone: 031 571 3150",
                           TextProperties = textProperties
                           },
                         }
                      }
                }
            };
        }

        private Table GetQrCode(TextProperties textProperties, string applicationId)
        {
            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 0,
                    Position = TablePosition.Relative,
                    Left = 0,
                    CellBorder = new BorderInfo(BorderSide.None, 0.20f),
                    ColumnAdjustment = ColumnAdjustment.AutoFitToWindow
                },
                Rows = new List<IRow>
                {
                    new Row
                    {
                       Cells = new List<ICell>
                       {
                           new Text {
                               DisplayText = @"The QR Code below is used to track your application electronically.",
                               TextProperties = textProperties
                           }
                       }
                    },
                    new Row
                    {
                        Cells = new List<ICell>
                        {
                            new PdfReportingPoc.Elements.Cells.BarCode
                            {
                                Text = applicationId,
                                Height = 100,
                                Width = 100,
                                Title = "QrCode"
                            }
                        }
                    }
                }
            };
        }

        private byte[] GetFileBytes(string text)
            {
                if (text.Contains("\\"))
                {
                    return File.ReadAllBytes(text);
                }
                var baseDirectory = TestContext.CurrentContext.TestDirectory + "\\TestData\\";
                var currentFilePath = Path.Combine(baseDirectory, text);
                var fileBytes = File.ReadAllBytes(currentFilePath);

                return fileBytes;
            }

        }
    }
