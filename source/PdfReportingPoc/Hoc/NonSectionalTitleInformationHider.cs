using Aspose.Pdf;
using Aspose.Pdf.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PdfReportingPoc.Hoc
{
    public class NonSectionalTitleInformationHider
    {
        public NonSectionalTitleInformationHider()
        {
            var license = new License();
            license.SetLicense("Aspose.Pdf.Lic");
        }
        public byte[] CheckPropertyTypeAndHide(string file, string propertyType)
        {
            Document pdfDocument = new Document(file);
            var wordsToHide = GetAllStringToHide();
            
            if(propertyType == "Sectional")
            {
                HideElements(pdfDocument, wordsToHide);
            }
            var fileName = "output.pdf";
            var filePath = GetFile(fileName);
            pdfDocument.Save(filePath);
            var fileBytes = GetFileBytes(filePath);
            return fileBytes;
        }

        private void HideElements(Document pdfDocument, List<string> listOfWordsToHide)
        {
            for(int i = 0; i < listOfWordsToHide.Count(); i++)
            {
                TextFragmentAbsorber textFragmentAbsorber1 = new TextFragmentAbsorber(listOfWordsToHide[i]);
                pdfDocument.Pages.Accept(textFragmentAbsorber1);
                TextFragmentCollection textFragmentCollection = textFragmentAbsorber1.TextFragments;

                if (textFragmentCollection.Any())
                {
                    foreach (TextFragment textFragment in textFragmentCollection)
                    {
                        textFragment.TextState.Invisible = true;
                    }
                }

            }
            var comboBox = pdfDocument.Form["ComboBox1"];
            pdfDocument.Pages[1].Annotations.Delete(comboBox);
        }

        private List<string> GetAllStringToHide()
        {
            var listOfWordsToHide = new List<string>();
            listOfWordsToHide.Add("We require proof of building insurance (Home Owners Cover) . Alternatively, SA Home Loans can");
            listOfWordsToHide.Add("provide");
            listOfWordsToHide.Add("Home owners Cover:");
            listOfWordsToHide.Add("Select:");
            return listOfWordsToHide;
        }

        private byte[] GetFileBytes(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }

        private string GetFile(string fileName)
        {
            var pdfPath = AppDomain.CurrentDomain.BaseDirectory + "TestData\\" + fileName;
            return pdfPath;
        }
    }
}
