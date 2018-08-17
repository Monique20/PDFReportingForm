using PdfReportingPoc.Domain.BarCode;

namespace PdfReportingPoc.UseCase.BarCode
{
   public class CreateBarCodeUseCase : ICreateBarCodeUseCase
    {
        private readonly IBarCodeGenerator _barCodeGenerator;
        public CreateBarCodeUseCase(IBarCodeGenerator barCodeGenerator)
        {
            _barCodeGenerator = barCodeGenerator;
        }
        public CreateBarCodeResponse Execute(CreateBarCodeRequest inputData)
        {
            var createBarCode = _barCodeGenerator.With_Text(inputData.Text)
                .With_Default_Dimension()
                .Of_Type_QR_Code()
                .As_Png()
                .Create();

            var result = new CreateBarCodeResponse { BarCode = createBarCode};
            return result;
        }
    }
}

