using ExtractLibrary.Const;
using Newtonsoft.Json;
using NUnit.Framework;
using SpecFlowPdfReader.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtractLibrary.ExtractFromJson
{
    public class MassExctracter
    {
        private string? jsonResut = JsonHelper.GetJsonFile();


        public string CountTitle()
        {
            string jsonContent = File.ReadAllText(jsonResut);
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(jsonContent);
            var titleElement = myDeserializedClass.elements.FirstOrDefault(e => e.Path.Contains(PdfCheckPaths.pdfTitlePath) && e.TextSize != 0);

            try
            {
                Assert.That(titleElement, Is.Not.Null, "Title element not found");
                Assert.That(titleElement?.Text, Is.Not.Null.Or.Empty, "Title element text is null or empty");
                return titleElement?.Text;
            }
            catch (AssertionException ex)
            {
                return ($"Title element text is null or empty - {ex.Message}, PDF Path - {titleElement.Path}");
            }
            return null;
        }

    }
}
