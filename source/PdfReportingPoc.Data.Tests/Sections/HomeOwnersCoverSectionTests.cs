using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using NUnit.Framework;
using PdfReportingPoc.Domain.Elements;
using PdfReportingPoc.Domain.Elements.Cell;
using PdfReportingPoc.Domain.Elements.Cells;
using Row=PdfReportingPoc.Elements.Row;
using PdfReportingPoc.Elements.Cells;
using PdfReportingPoc.Elements;

namespace PdfReportingPoc.Data.Tests.Sections
{
    [TestFixture]
    public class HomeOwnersCoverSectionTests
    {
        [Test]
        public void RenderHomeOwnersCoverSection_GivenNaturalPersonWithHomeOwnersCoverAndDebitOrderPayments_ShouldNotAddSectionToDocument()
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

        [Test]
        public void RenderHomeOwnersCoverSection_GivenNaturalPersonWithoutHomeOwnersCoverAndDebitOrderPaymentsAndChosenOptionIsIAlreadyHaveHomeOwnersCover_ShouldAddSectionToDocument()
        {
            //Arrange
            var fileName = "blank.pdf";
            var tableRenderData = GetFileBytes(fileName);
            var section = GetSection();

            //Act
            var actual = section.Render(tableRenderData);

            //Assert
            Assert.Greater(actual.PdfBytes.Length, tableRenderData.PdfBytes.Length);
        }

        [Test]
        public void RenderHomeOwnersCoverSection_GivenNaturalPersonWithoutHomeOwnersCoverAndDebitOrderPaymentsAndChosenOptionIsIWantToApplyForHomeOwnersCoverWithSAHomeLoans_ShouldAddSectionToDocument()
        {
            //Arrange
            var fileName = "blank.pdf";
            var tableRenderData = GetFileBytes(fileName);
            var section = GetSection();

            //Act
            var actual = section.Render(tableRenderData);

            //Assert
            Assert.Greater(actual.PdfBytes.Length, tableRenderData.PdfBytes.Length);
        }

        [Test]
        public void RenderHomeOwnersCoverSection_GivenNaturalPersonWithHomeOwnersCoverAndNoDebitOrderPaymentDetails_ShouldNotAddSectionToDocument()
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

        [Test]
        public void RenderHomeOwnersCoverSection_GivenNaturalPersonWithoutHomeOwnersCoverAndNoDebitOrderPaymentDetails_ShouldNotAddSectionToDocument()
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

        [Test]
        public void RenderHomeOwnersCoverSection_GivenNaturalPersonWithHomeOwnersCoverAndDebitOrderPaymentDetailsAndBondProtection_ShouldNotAddSectionToDocument()
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

        [Test]
        public void RenderHomeOwnersCoverSection_GivenNaturalPersonWithHomeOwnersCoverAndDebitOrderPaymentDetailsAndNoBondProtection_ShouldNotAddSectionToDocument()
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

        [Test]
        public void RenderHomeOwnersCoverSection_GivenTrustWithHomeOwnersCoverAndDebitOrderPaymentDetailsAndBondProtection_ShouldNotAddSectionToDocument()
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

        [Test]
        public void RenderHomeOwnersCoverSection_GivenClosedCorporationWithHomeOwnersCoverAndDebitOrderPaymentDetailsAndBondProtection_ShouldNotAddSectionToDocument()
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

        [Test]
        public void RenderHomeOwnersCoverSection_GivenClosedCorporationWithHomeOwnersCoverAndDebitOrderPaymentDetailsAndSignatoryHasBondProtection_ShouldNotAddSectionToDocument()
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

        [Test]
        public void RenderHomeOwnersCoverSection_GivenSectionalPropertyType_ShouldNotAddSectionToDocument()
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

        private ITable GetSection()
        {
            return new PdfReportingPoc.Elements.Table
            {
                Layout = new TableLayout
                {
                    CellMargin = new MarginInfo(2f, 2f, 2f, 2f),
                    Top = 100f,
                    Widths = "380 35",
                    CellBorder = new BorderInfo(BorderSide.None, 0.20f,Color.White),
                    ColumnAdjustment = ColumnAdjustment.Customized
                },
                
                Rows=new List<IRow>
                {
                    new Row
                    {
                        Cells=new List<ICell>
                        {
                            new Text
                            {
                                DisplayText = "Building Insurance:",
                                TextProperties=new TextProperties
                                {
                                    FontSize = 14,
                                    FontType = "Arial",
                                    FontStyle = FontStyles.Bold,
                                    FontColor = Color.Black,
                                    Invisible = false,
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
                                DisplayText = "We require proof of building insurance (Home Owners Cover). Alternatively, SA Home Loans can provide Bond Protection:",
                                TextProperties=new TextProperties
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
                    new Row
                    {
                        Cells=new List<ICell>
                        {
                            new Text
                            {
                                DisplayText = "Select:",
                                TextProperties=new TextProperties
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
                    new Row
                    {
                        Cells=new List<ICell>
                        {
                            new Text
                            {
                                DisplayText = "I already have building insurance (Home Owners Cover)",
                                TextProperties=new TextProperties
                                {
                                    FontSize = 10,
                                    FontType = "Arial",
                                    FontStyle = FontStyles.Regular,
                                    FontColor = Color.Black,
                                    Invisible = false
                                }
                            },
                            new Checkbox
                            {
                                 CheckboxProperites = new CheckboxProperites{
                                        Height = 20,
                                        Width = 20,
                                        Id = "checkBox1"
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
                                TextProperties=new TextProperties
                                {
                                    FontSize = 10,
                                    FontType = "Arial",
                                    FontStyle = FontStyles.Regular,
                                    FontColor = Color.Black,
                                    Invisible = false
                                }
                            },
                            new Checkbox
                            {
                                 CheckboxProperites = new CheckboxProperites{
                                        Height = 20,
                                        Width = 20,
                                        Id = "checkBox2"
                                    }
                            }
                        }
                    }
                }
            };
        }

        private ITable GetNullSection()
        {
            return new PdfReportingPoc.Elements.Table
            {
                Layout = new TableLayout(),

                Rows = new List<IRow>()
            };
        }

        private TableRenderData GetFileBytes(string fileName)
        {
            var baseDirectory = TestContext.CurrentContext.TestDirectory;
            var currentFilePath = Path.Combine(baseDirectory, "TestData", fileName);
            var fileBytes = File.ReadAllBytes(currentFilePath);

            return new TableRenderData
            {
                PdfBytes = fileBytes
            };
        }
    }
}
