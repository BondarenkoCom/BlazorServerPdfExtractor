using ExtractLibrary.Const;
using Newtonsoft.Json;
using NUnit.Framework;
using SpecFlowPdfReader.Helpers;

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
