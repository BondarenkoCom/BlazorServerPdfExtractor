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

        public (int, int, List<string>, List<string>) CountElementsTable()
        {
            string jsonContent = File.ReadAllText(jsonResut);
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(jsonContent);

            int headerCount = 0;
            int tableHeaderCount = 0;

            List<string> textHeaderList = new List<string>();
            List<string> textTableHeaderList = new List<string>();


            foreach (var element in myDeserializedClass.elements.Where(e => e.Path.Contains(PdfCheckPaths.pdfHeader) && e.TextSize != 0))
            {
                try
                {
                    if (element.Text.Contains(PdfCheckPaths.pdfTableHeader))
                    {
                        tableHeaderCount++;
                        textHeaderList.Add(element.Text);
                    }
                    else
                    {
                        headerCount++;
                        textTableHeaderList.Add(element.Text);
                    }
                }
                catch (AssertionException ex)
                {
                    continue;
                }
            }
            return (tableHeaderCount, headerCount, textTableHeaderList, textHeaderList);
        }

        public (int, List<string>) CountElementsCheckBox()
        {
            string jsonContent = File.ReadAllText(jsonResut);
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(jsonContent);
            int checkBoxCount = 0;
            List<string> TextCheckBoxList = new List<string>();

            foreach (var checkBoxElement in myDeserializedClass.elements.Where(e => e.Path.Contains(PdfCheckPaths.pdfDoc) && e.TextSize != 0))
            {
                try
                {
                    if (string.IsNullOrEmpty(checkBoxElement.Text) || !checkBoxElement.Text.Contains(PdfCheckPaths.pdfCheckBox))
                    {
                        continue;
                    }
                    checkBoxCount++;
                    TextCheckBoxList.Add(checkBoxElement.Text);
                }
                catch (AssertionException ex)
                {
                    continue;
                }
            }
            return (checkBoxCount, TextCheckBoxList);
        }

        public (int, List<string>, List<string>) CountElementFont(string FontType)
        {
            string jsonContent = File.ReadAllText(jsonResut);
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(jsonContent);
            int fontTypeCount = 0;
            string fontInfo;
            List<string> TextFontList = new List<string>();
            List<string> TextFontTypeList = new List<string>();

            foreach (var textFontElement in myDeserializedClass.elements.Where(e => e.TextSize != 0))
            {
                try
                {
                    fontInfo = textFontElement.Font.font_type.ToString();

                    fontTypeCount++;
                    //"TrueType"
                    if (fontInfo == FontType)
                    {
                        TextFontList.Add(textFontElement.Text);
                        TextFontTypeList.Add(textFontElement.Font.font_type.ToString());
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }

            return (fontTypeCount, TextFontList, TextFontTypeList);
        }


        public (int, List<string>) CountElementsSectionParagraph()
        {
            string jsonContent = File.ReadAllText(jsonResut);
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(jsonContent);

            int paragCount = 0;
            List<string> TextParagList = new List<string>();


            foreach (var poElement in myDeserializedClass.elements.Where(e => e.Path.Contains(PdfCheckPaths.pdfParapgraphPath) && e.TextSize != 0))
            {
                try
                {
                    paragCount++;
                    TextParagList.Add(poElement.Text);
                }
                catch (AssertionException ex)
                {
                    continue;
                }
            }
            return (paragCount, TextParagList);
        }
        
        public (int, int, int, int, List<string>, List<string>, List<string>) GetSectionTable()
        {
            string jsonContent = File.ReadAllText(jsonResut);
            Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(jsonContent);

            int tableCount = 0;
            int tableParagCount = 0;
            int tableRow = 0;
            int tableBulletPoint = 0;
            
            List<string> tableTextParagList = new List<string>();
            List<string> tableTextRowList = new List<string>();
            List<string> tableTextBulletPointList = new List<string>();

            foreach (var tableElement in myDeserializedClass.elements.Where(e => e.Path.Contains(PdfCheckPaths.pdfTable) && e.TextSize != 0))
            {
                try
                {
                    tableCount++;

                    if (tableElement.Path.Contains(PdfCheckPaths.pdfTableParagraph))
                    {
                        tableParagCount++;
                        tableTextParagList.Add(tableElement.Text);
                    }
                    else if (tableElement.Path.Contains(PdfCheckPaths.pdfTableRows))
                    {
                        tableRow++;
                        tableTextRowList.Add(tableElement.Text);
                    }
                    else if (tableElement.Path.Contains(PdfCheckPaths.pdfTableBulletPoint))
                    {
                        tableBulletPoint++;
                        tableTextBulletPointList.Add(tableElement.Text);
                    }

                }
                catch (AssertionException ex)
                {
                    continue;
                }
            }
            return (tableCount, tableParagCount, tableRow, tableBulletPoint,
                tableTextParagList, tableTextRowList, tableTextBulletPointList);
        }
    }
}
