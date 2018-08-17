namespace PdfReportingPoc.Elements.Fragments
{
    public class DocumentBoundryText
    {
        public string Label { get; set; }
        public string Text { get; set; }
        public int SpacesNeeded { get; set; }

        public DocumentBoundryText()
        {
            Label = string.Empty;
            Text = string.Empty;
            SpacesNeeded = 0;
        }

        public override string ToString()
        {
            var result = $"{Label} {Text}";
            result = result.PadRight(SpacesNeeded, ' ');
            return result;
        }
    }
}