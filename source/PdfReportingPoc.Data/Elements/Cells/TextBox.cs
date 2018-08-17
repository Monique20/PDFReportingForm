using System.Collections.Generic;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using PdfReportingPoc.Domain.Elements;
using PdfReportingPoc.Domain.Elements.Cell;
using PdfReportingPoc.Domain.Elements.Cells;

namespace PdfReportingPoc.Elements.Cells
{
    public class TextBox : ICell
    {
        public TextBoxProperties TextBoxProperties { get; set; }
        public string Id { get; set; }

        public void Render(Aspose.Pdf.Row row)
        {
            var cell = row.Cells.Add();
            TextBoxField paragraph = new TextBoxField
            {
                PartialName = Id,
                Height = TextBoxProperties.Height,
                Width = TextBoxProperties.Width,
                MaxLen = TextBoxProperties.MaxLen,
                Required = TextBoxProperties.Required
            };
            cell.Paragraphs.Add(paragraph);
        }

        public void Format(Document pdfDocument)
        {
            var textboxId = Id;
            var textBoxField = pdfDocument.Form[textboxId] as TextBoxField;    
            if (textBoxField == null) return;
            textBoxField.Characteristics.Border = System.Drawing.Color.Black;
            if (textBoxField.MaxLen == 0)
            {
                var value = TextBoxProperties.Value;
                if (value == null)
                {
                    textBoxField.MaxLen = 0;
                }
                else
                {
                    textBoxField.MaxLen = value.Length + 4;
                }
            }
            var requestedType = TextBoxProperties.Type;
            textBoxField.ForceCombs = TextBoxProperties.ForceComb;
            textBoxField.Value = TextBoxProperties.Value;
            textBoxField.ReadOnly = TextBoxProperties.ReadOnly;
            SetFieldFormat(pdfDocument, textboxId, textBoxField, requestedType);
        }

        private void SetFieldFormat(Document pdfDocument, string textboxId, WidgetAnnotation textBoxField, string requestedType)
        {
            if (requestedType == null) return;
            if (NumericTextboxWithProperties(requestedType))
            {
                FormatTextBoxTypeToNumber(pdfDocument, textboxId, textBoxField);
            }
            else if (DateTextbox(requestedType)) 
            {
                FormatTextBoxTypeToDate(textBoxField);
            }
        }

        private static bool DateTextbox(string requestedType)
        {
            return requestedType.ToLower() == "date";
        }

        private bool NumericTextboxWithProperties(string requestedType)
        {
            return requestedType.ToLower() == "number" && TextBoxProperties.NumberFormatProperties != null;
        }

        private void FormatTextBoxTypeToNumber(Document pdfDocument, string textboxID, WidgetAnnotation textBoxField)
        {
            SetFieldFormatToNumber(TextBoxProperties, textBoxField);

            if (TextBoxProperties.Calculatable)
            {
                CreateFormularForTextBoxField(pdfDocument, textboxID);
            }
        }

        private void CreateFormularForTextBoxField(Document pdfDocument, string textboxId)
        {
            var operators = GetConstantOperators();
            var formular = TextBoxProperties.Formular;
            var operatorsInFormular = GetOperatorsInFormular(operators, formular);
            var textBoxNamesInFormular = GetTextBoxNamesInFormular(operatorsInFormular, formular);
            var javaScriptFormular = AppendFormularInJavaScriptFormat(textboxId, textBoxNamesInFormular, operatorsInFormular);
            var fieldsFound = FieldsFound(pdfDocument, textBoxNamesInFormular);
            if (fieldsFound)
            {
                AssignFormularToTextBoxField(textBoxNamesInFormular, pdfDocument, javaScriptFormular);
            }
        }

        private static List<char> GetConstantOperators()
        {
            return new List<char> { '-', '+', '/', '*', '%' };
        }

        private static IEnumerable<string> GetTextBoxNamesInFormular(IEnumerable<char> operatorsInFormular, string formular)
        {
            return formular.Split(operatorsInFormular.ToArray()).Select(textbox => textbox.Trim());
        }

        private static IEnumerable<char> GetOperatorsInFormular(ICollection<char> operators, string formular)
        {
            return formular.Where(operators.Contains);
        }

        private string AppendFormularInJavaScriptFormat(string textboxId, IEnumerable<string> listOfTextBoxNames, IEnumerable<char> listOfOperators)
        {
            var formularInJavaScriptFormat = $"this.getField('{textboxId}').value = this.getField('{listOfTextBoxNames.ElementAt(0)}').value";
            for (var i = 0; i < listOfOperators.Count(); i++)
            {
                formularInJavaScriptFormat += $" {listOfOperators.ElementAt(i)} this.getField('{listOfTextBoxNames.ElementAt(i + 1)}').value";
            }

            return formularInJavaScriptFormat;
        }

        private static void AssignFormularToTextBoxField(IEnumerable<string> listOfTextBoxNames, Document pdfDocument, string javaScriptFormular)
        {
            foreach (var textBoxName in listOfTextBoxNames)
            {
                var textBoxField = pdfDocument.Form[textBoxName] as TextBoxField;
                textBoxField.Actions.OnLostFocus = new JavascriptAction(javaScriptFormular);
            }
        }

        private bool FieldsFound(Document pdfDocument, IEnumerable<string> listOfTextBoxNames)
        {
            return listOfTextBoxNames.All(givenTextbox => pdfDocument.Form.Any(textboxInDocument => textboxInDocument.FullName.Equals(givenTextbox)));
        }

        private void SetFieldFormatToNumber(TextBoxProperties textBoxProperties, WidgetAnnotation textBoxField)
        {
            var separationFormat = GetSeparationFormat(textBoxProperties.NumberFormatProperties.SeparateWithComma);
            var negativeStyle = GetNegativeStyleFormat(textBoxProperties.NumberFormatProperties.NegativeStyle);
            var currencyPrepend = GetCurrencyPrepend(textBoxProperties.NumberFormatProperties.CurrencyPrepend);

            textBoxField.Actions.OnFormat = new JavascriptAction($"AFNumber_Format({textBoxProperties.NumberFormatProperties.NumberOfPlacesAfterDecimalPoint}, {separationFormat}, {negativeStyle}, 0, \"{textBoxProperties.NumberFormatProperties.CurrencySymbol}\", {currencyPrepend})");
            textBoxField.Actions.OnModifyCharacter = new JavascriptAction($"AFNumber_Keystroke({textBoxProperties.NumberFormatProperties.NumberOfPlacesAfterDecimalPoint}, {separationFormat}, {negativeStyle}, 0, \"{textBoxProperties.NumberFormatProperties.CurrencySymbol}\", {currencyPrepend})");
        }

        private static void FormatTextBoxTypeToDate(WidgetAnnotation textBoxField)
        {
            textBoxField.Actions.OnFormat = new JavascriptAction("AFDate_FormatEx('mm/dd/yy')");
            textBoxField.Actions.OnModifyCharacter = new JavascriptAction("AFDate_KeystrokeEx('mm/dd/yy')");
        }

        private static int GetSeparationFormat(bool formatSeparateWithComma)
        {
            return formatSeparateWithComma ? 0 : 1;
        }

        private static int GetNegativeStyleFormat(NegativeNumberFormat negativeStyle)
        {
            if (negativeStyle == NegativeNumberFormat.Minusblack)
            {
                return 0;
            }
            else if (negativeStyle == NegativeNumberFormat.Red)
            {
                return 1;
            }
            else if (negativeStyle == NegativeNumberFormat.BlackParentesis)
            {
                return 2;
            }
            return 3;
        }

        private static string GetCurrencyPrepend(bool formatCurrencyPrepend)
        {
            return formatCurrencyPrepend ? "true" : "false";
        }
    }
}