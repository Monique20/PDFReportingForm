using System;
using System.IO;
using FluentAssertions;
using NUnit.Framework;
using PdfReportingPoc.BarCode;

namespace PdfReportingPoc.Data.Tests.BarCode
{
    [TestFixture]
    //[Ignore("Working code")]
    public class BarCodesTests
    {
        [TestFixture]
        //[Ignore("Working code")]
        public class Create
        {
            [Test]
            public void WhenStringNotEmptyOrWhitespace_ExpectQrCode()
            {
                // arrange 
                var text = Guid.NewGuid().ToString();
                var sut = new BarCodeGenerator();
                // act
                var actual = sut.With_Text(text)
                    .With_Default_Dimension()
                    .Of_Type_QR_Code()
                    .As_Png()
                    .Create();
                // assert
                actual.Length.Should().BeGreaterThan(0);
            }

            [TestCase("")]
            [TestCase(" ")]
            [TestCase(null)]
            public void WhenNullOrWhitespaceText_ExpectNoQrCode(string text)
            {
                // arrange 
                var sut = new BarCodeGenerator();
                //act
                var actual = sut.With_Text(text)
                    .With_Default_Dimension()
                    .Of_Type_QR_Code()
                    .As_Png()
                    .Create();
                // assert
                actual.Length.Should().Be(0);
            }
        }

        [TestFixture]
        //[Ignore("Working code")]
        public class Read
        {
            [Test]
            public void WhenQrCodeWithText_ExpectEncodedStringReturned()
            {
                // arrange 
                var barcodePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "barcode-withText.png");
                var bytes = File.ReadAllBytes(barcodePath);
                var sut = new BarCodeGenerator();
                // act 
                var actual = sut.With_Image(bytes)
                    .Of_Type_QR_Code()
                    .As_Png()
                    .Extract_Text();
                // assert
                var expected = "4b744914-00cc-4d29-99b9-d6c1a92db7a8";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void WhenQrCodeWithoutText_ExpectEncodedStringReturned()
            {
                // arrange 
                var barcodePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestData", "barcode-withoutText.png");
                var bytes = File.ReadAllBytes(barcodePath);
                var sut = new BarCodeGenerator();
                // act 
                var actual = sut.With_Image(bytes)
                    .Of_Type_QR_Code()
                    .As_Png()
                    .Extract_Text();
                // assert
                var expected = "a5dd8fc3-6b4b-4f26-ba42-a6317de3f4d4";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void WhenInvalidImageBytes_ExpectEmptyString()
            {
                // arrange 
                var bytes = new byte[100];
                var sut = new BarCodeGenerator();
                // act 
                var actual = sut.With_Image(bytes)
                    .Of_Type_QR_Code()
                    .As_Png()
                    .Extract_Text();
                // assert
                var expected = string.Empty;
                Assert.AreEqual(expected, actual);
            }
        }
    }
}