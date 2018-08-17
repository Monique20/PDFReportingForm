using PdfReportingPoc.Domain.BarCode;

namespace PdfReportingPoc.UseCase.BarCode
{
    public class ExtractBarCodeUseCase : IExtractBarCodeUseCase
    {
        private readonly IBarCodeGenerator _barCodeGenerator;
        public ExtractBarCodeUseCase(IBarCodeGenerator barCodeGenerator)
        {
            _barCodeGenerator = barCodeGenerator;
        }
        public ExtractBarCodeResponse Execute(ExtractBarCodeRequest inputData)
        {
            var textFromQrCode = _barCodeGenerator.With_Image(inputData.Image)
                                 .Of_Type_QR_Code()
                                 .As_Png()
                                 .Extract_Text();

            var result = new ExtractBarCodeResponse { Text = textFromQrCode };
            return result;
        }
    }
}
