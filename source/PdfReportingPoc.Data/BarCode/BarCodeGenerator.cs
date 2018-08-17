using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using PdfReportingPoc.Domain.BarCode;
using ZXing;
using ZXing.Common;

namespace PdfReportingPoc.BarCode
{
    public class BarCodeGenerator : IBarCodeGenerator
    {
        private readonly BarCodeGeneratorBuilder _builder;

        public BarCodeGenerator()
        {
            _builder = new BarCodeGeneratorBuilder();
        }

        public IBarCodeDimension With_Text(string text)
        {
            _builder.With_Text(text);
            return _builder;
        }

        public IBarCodeType With_Image(byte[] image)
        {
            _builder.With_Image(image);
            return _builder;
        }

        private class BarCodeGeneratorBuilder : IBarCodeGenerator, IBarCodeDimension, IBarCodeType, IBarCodeFormat, IBarCodeOperations
        {
            private string _text;
            private int _dimensionX;
            private int _dimensionY;
            private BarcodeFormat _type;
            private byte[] _imageBytes;
            private ImageFormat _format;

            public IBarCodeDimension With_Text(string text)
            {
                _text = text;
                return this;
            }

            public IBarCodeType With_Image(byte[] imageBytes)
            {
                _imageBytes = imageBytes;
                return this;
            }

            public IBarCodeType With_Default_Dimension()
            {
                _dimensionX = 10;
                _dimensionY = 20;
                return this;
            }

            public IBarCodeType With_Custom_Dimension(int x, int y)
            {
                _dimensionX = x;
                _dimensionY = y;
                return this;
            }

            public IBarCodeFormat Of_Type_QR_Code()
            {
                _type = BarcodeFormat.QR_CODE;
                return this;
            }

            public IBarCodeOperations As_Png()
            {
                _format = ImageFormat.Png;
                return this;
            }

            public byte[] Create()
            {
                if (string.IsNullOrWhiteSpace(_text))
                {
                    return new byte[0];
                }

                var writer = CreateBarcodeWriter();

                return RenderBarcode(writer);
            }

            public string Extract_Text()
            {
                var reader = new BarcodeReader();
                using (var stream = new MemoryStream(_imageBytes))
                {
                    try
                    {
                        return DecodeBarcode(stream, reader);
                    }
                    catch (ArgumentException e)
                    {
                        return string.Empty;
                    }   
                }
            }

            private static string DecodeBarcode(MemoryStream stream, BarcodeReader reader)
            {
                var barcodeBitmap = Image.FromStream(stream) as Bitmap;
                var result = reader.Decode(barcodeBitmap);
                return result.Text;
            }

            private byte[] RenderBarcode(BarcodeWriter writer)
            {
                var image = writer.Write(_text);

                using (var stream = new MemoryStream())
                {
                    image.Save(stream, _format);
                    return stream.ToArray();
                }
            }

            private BarcodeWriter CreateBarcodeWriter()
            {
                var writer = new BarcodeWriter
                {
                    Format = _type,
                    Options = new EncodingOptions
                    {
                        Height = _dimensionX,
                        Width = _dimensionY
                    }
                };

                return writer;
            }
        }
    }
}