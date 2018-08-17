using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using FluentAssertions;
using NUnit.Framework;
using PdfReportingPoc.Domain.Elements;
using PdfReportingPoc.Domain.Elements.Cell;
using PdfReportingPoc.Elements;
using PdfReportingPoc.Elements.Cells;
using Row = PdfReportingPoc.Elements.Row;
using Table = PdfReportingPoc.Elements.Table;


namespace PdfReportingPoc.Data.Tests.Sections
{
    [TestFixture]
    public class TermsAndConditionSection
    {
        [Test]
        public void Render_GivenThatSignatoryHaveBondProtection_ShouldCreateTableWithTermsAndConditions()
        {
            //Arrange
            var fileName = "blank.pdf";
            var tableRenderData = GetFileBytes(fileName);
            var textProperties = new TextProperties
            {
                FontSize = 8,
                FontType = "Arial",
                FontStyle = FontStyles.Regular,
                FontColor = Color.Black,
                Invisible = false
            };
            var table = GetTermsAndConditionDetailsTable(textProperties);
            //Act
            var actual = table.Render(tableRenderData);
            //Assert
            var expected = tableRenderData.PdfBytes.Length;
            actual.PdfBytes.Length.Should().BeGreaterThan(expected);
        }

        [Test]
        public void Render_TermsAndConditionsSignatures_ShouldCreateTableForApplicantSignatures()
        {
            //Arrange
            var fileName = "blank.pdf";
            var tableRenderData = GetFileBytes(fileName);
            var textProperties = new TextProperties
            {
                FontSize = 10,
                FontType = "Arial",
                FontStyle = FontStyles.Regular,
                FontColor = Color.Black,
                Invisible = false
            };
            
            var table = GetTsAndCsSignatureTable(textProperties);

            //Act
            var actual = table.Render(tableRenderData);
            //Assert
            var expected = tableRenderData.PdfBytes.Length;
            actual.PdfBytes.Length.Should().BeGreaterThan(expected);
        }

        [Test]
        public void Render_GivenThatSignatoryDoesNotHaveBondProtection_ShouldReturnPagesWithoutTermsAndConditionSection()
        {
            //Arrange
            var fileName = "blank.pdf";
            var tableRenderData = GetFileBytes(fileName);
            var table = GetEmptyTable();
            //Act
            var actual = table.Render(tableRenderData);
            //Assert
            var expected = tableRenderData.PdfBytes.Length;
            actual.PdfBytes.Length.Should().Be(expected);
        }

        private static Table GetEmptyTable()
        {
            return new Table
            {
                Rows = new List<IRow>()
            };
        }

        private static Table GetTermsAndConditionDetailsTable(TextProperties textProperties)
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
                           new Text {
                               DisplayText = "I hereby acknowledge and warrant that:",
                               TextProperties =new TextProperties
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
                new Row
                {
                    Cells = new List<ICell>
                    {
                             new Text {
                                 DisplayText = "1. All information given by meistrue, accurate and correct. I have reviewed the information and confirm the corretness " + Environment.NewLine +
                                             "there of." + Environment.NewLine +
                                             "2. I have provided all in formation which is directly relevant and  material to my application." + Environment.NewLine +
                                             "3. I have the legal capacity to enter into anagreement unassisted as I am a major." + Environment.NewLine +
                                             "4. I consent to SA Home Loans (Proprietary) Limited (SA Home Loans) performing a credit " + Environment.NewLine +
                                             "reference check. Further,I consent" + Environment.NewLine +
                                             "to SA Home Loans providing credit reference agencies with regular updates with regard to the conduct of my account. " + Environment.NewLine +
                                             "This will include failure to maintain the obligations as agreed with SA Home Loans.  Futher more, i consent  " + Environment.NewLine +
                                             "to the credit reference agencies making this information avilable to other credit providers" + Environment.NewLine +
                                             "5. In order to process my application I agree that SA Home Loans may need to perform another checks in order to verify " + Environment.NewLine +
                                             "the information provided, and I consent to SA Home Loans performing all checks which it deems necessary." + Environment.NewLine +
                                             "6. I am not aware of any other information which could adversely affect my application." + Environment.NewLine +
                                             "7. I consent to SA Home Loans carrying out identity and fraud prevention checks and sharing information relating to this " + Environment.NewLine +
                                             "application with the South African Fraud Prevention Service." + Environment.NewLine +
                                             "8. In the event of my financial situation in terms of affordability changing from date of signature of my application with " + Environment.NewLine +
                                             "SA Home Loans, I will inform SA Home Loans in writing." + Environment.NewLine +
                                             "9. I am able to afford the repayments of the home loan finance I am seeking." + Environment.NewLine +
                                            "10. I / We are aware that this application is subject to th standard terms and conditions of the SA Home Loans and all  its" + Environment.NewLine +
                                             "credit criteria applicable to home loan finance." + Environment.NewLine +
                                             "11. A valuation of my property may be necessary. I hereby appoint SA Home Loans to act as my agent authorized to:" + Environment.NewLine +
                                             "              1.Instruct a valuer to perform a property valuation;" + Environment.NewLine +
                                             "              2.Incur the cost of the valuation for my account;" + Environment.NewLine +
                                             "              3.Recover the cost of the valuation from the disbursement of my loan, and I indemnify SA Home " + Environment.NewLine +
                                             "              Loans against all loss resulting from an instruction in terms of this clause,and if SA Home Loans fails to " + Environment.NewLine +
                                             "              recover the costs of the valuation from my loan, I undertake to pay any such cost of the valuation directly to SA" + Environment.NewLine +
                                             "              Home Loans." + Environment.NewLine +
                                             "12. Should a valuation be carried out on my property which results in the replacement value increasing, the SAHL HOC " + Environment.NewLine +
                                             "premium will automatically be increased in line with the new replacement value. In the case where the cover is " + Environment.NewLine +
                                             "provided by an external HOC Policy, you will be required to increase the cover accordingly.",
                                 TextProperties = textProperties
                             }
                    }
                },
                new Row
                {
                    Cells = new List<ICell>
                    {
                           new Text {
                                DisplayText = "Conditions applicable should you have an existing SAHL Life Bond Protection Plan Policy Should you access additional " + Environment.NewLine +
                                              "funds on your loan, the premium applicable to your SAHL Life Bond Protection Plan Policy will be increased in line " + Environment.NewLine +
                                              "with the additional funds accessed by you. Please also take note of the following sections of the terms and" + Environment.NewLine +
                                              "conditions of the policy:" + Environment.NewLine +
                                                Environment.NewLine +
                                                "13. Exclusions " + Environment.NewLine +
                                                "SAHL Life will not be obliged to make payment in respect of any claim arising from any condition or event arising directly " + Environment.NewLine +
                                                "or indirectly from or traceable to: " + Environment.NewLine +
                                                "13.4 Pre-Existing Conditions" + Environment.NewLine +
                                                "If a Life Assured" + Environment.NewLine +
                                                "                   13.4.1 dies within 24 months of commencement of the policy" + Environment.NewLine +
                                                "                   13.4.2 becomes disabled at any time during the duration of the policy due to any condition, physical defect, " + Environment.NewLine +
                                                "                   illness, bodily injury or disability which the insured was aware of and/or received medical advice or treatment " + Environment.NewLine +
                                                "                   for prior to the commencement date or date of any reinstatement, no claim will be paid and all premiums paid" + Environment.NewLine +
                                                "                   will be forfeited. In the event of any increase in the amount of any benefit this condition shall also apply " + Environment.NewLine +
                                                "                   to the increase with effect from the date of the increase." + Environment.NewLine +
                                                Environment.NewLine +
                                                "11.alterations to the policy" + Environment.NewLine +
                                                "11.3 All alterations to the policy will be subject to the following:" + Environment.NewLine +
                                                "11.3.1 All exclusions and pre-existing clauses shall apply to any increase in the sum assured and the Instalment " + Environment.NewLine +
                                                "Protector Benefit with effect from the date of such increase." + Environment.NewLine +
                                                "11.3.3 Where the sum assured is increased or a life assured added to the policy a pro-rate premium will be charged for " + Environment.NewLine +
                                                "the balance of the policy year." + Environment.NewLine +
                                                 Environment.NewLine +
                                                "Conditions applicable should you have an existing Regent Life Policy:" + Environment.NewLine +
                                                 Environment.NewLine +
                                                 "Should you require an increase in your life cover to match your full outstanding bond balance, please contact Regent Life " + Environment.NewLine +
                                                 "on telephone number:  011 8795000",
                                TextProperties = textProperties
                           }
                    }
                }
            }
        };

        }

        private static Table GetTsAndCsSignatureTable(TextProperties textProperties)
        {
            return new Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 665f,
                    Widths = "50 85 120 110",
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
                             DisplayText = "TITLE",
                             TextProperties =new TextProperties
                             {
                                FontSize = 10,
                                FontType = "Arial",
                                FontStyle = FontStyles.Bold,
                                FontColor = Color.Black,
                                Invisible = false
                             }
                        },
                        new Text {
                            DisplayText = "FULL NAME",
                             TextProperties =new TextProperties
                             {
                                 FontSize = 10,
                                 FontType = "Arial",
                                 FontStyle = FontStyles.Bold,
                                 FontColor = Color.Black,
                                 Invisible = false
                             }
                        },
                        new Text {
                            DisplayText = "ID NO:",
                             TextProperties =new TextProperties
                             {
                                 FontSize = 10,
                                 FontType = "Arial",
                                 FontStyle = FontStyles.Bold,
                                 FontColor = Color.Black,
                                 Invisible = false
                             }
                        },
                        new Text {
                            DisplayText = "ROLE",
                             TextProperties =new TextProperties
                             {
                                 FontSize = 10,
                                 FontType = "Arial",
                                 FontStyle = FontStyles.Bold,
                                 FontColor = Color.Black,
                                 Invisible = false
                             }
                        },
                        new Text {
                            DisplayText = "SIGNATURE",
                             TextProperties =new TextProperties
                             {
                                 FontSize = 10,
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
                                    DisplayText = "MS",
                                    TextProperties = textProperties
                                },
                                new Text {
                                    DisplayText = "Alicia Govender",
                                    TextProperties = textProperties
                                },
                                new Text {
                                    DisplayText = "111111111",
                                    TextProperties = textProperties
                                },
                                new Text {
                                    DisplayText = "MainApplicant",
                                    TextProperties = textProperties
                                },
                                new Signature
                                {
                                    SignatureProperties =  new SignatureProperties
                                    {
                                        PartialName = "signature1",
                                        Height = 20,
                                        Width = 105
                                    }
                                }
                            }
                   },
                   new Row
                   {
                      Cells = new List<ICell>
                      {
                            new Text {
                                DisplayText = "MR",
                                TextProperties = textProperties
                            },
                            new Text {
                                DisplayText = "Thabani Tembe",
                            TextProperties = textProperties
                            },
                            new Text {
                                DisplayText = "0005279632081",
                            TextProperties = textProperties
                            },
                            new Text {
                                DisplayText = "MainApplicant",
                            TextProperties = textProperties
                            },
                            new Signature
                            {
                               SignatureProperties =  new SignatureProperties
                               {
                                    PartialName = "signature12",
                                    Height = 20,
                                    Width = 105
                               }
                            }
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
                PdfBytes = fileBytes
            };
        }
    }
}
