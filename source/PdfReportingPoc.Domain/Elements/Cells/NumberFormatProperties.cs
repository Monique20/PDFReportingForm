namespace PdfReportingPoc.Domain.Elements.Cell
{
    public class NumberFormatProperties
    {
        public int NumberOfPlacesAfterDecimalPoint { get; set; }
        public bool SeparateWithComma { get; set; }
        public NegativeNumberFormat NegativeStyle { get; set; }
        public string CurrencySymbol { get; set; }
        public bool CurrencyPrepend { get; set; }

    }
}
