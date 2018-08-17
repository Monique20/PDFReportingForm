namespace PdfReportingPoc.Domain.BarCode
{
    public interface IBarCodeGenerator
    {
        IBarCodeDimension With_Text(string text);
        IBarCodeType With_Image(byte[] imageBytes);
    }

    public interface IBarCodeDimension
    {
        IBarCodeType With_Custom_Dimension(int x, int y);
        IBarCodeType With_Default_Dimension();
    }

    public interface IBarCodeType
    {
        IBarCodeFormat Of_Type_QR_Code();
    }

    public interface IBarCodeFormat
    {
        IBarCodeOperations As_Png();
    }

    public interface IBarCodeOperations
    {
        byte[] Create();
        string Extract_Text();
    }
}