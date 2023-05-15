using SelectPdf;

namespace ExtractLibrary.Helpers
{
    public class GeneratorPDF
    {
        public void GeneratePDFFromFont(
            string? fileName,
            string? fileSize,
            string? dateTime,
            int? countTextFonts,
            string? textFontType,
            string? textFont,
            string? outputPath)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = "empty";
                fileSize = "empty";
                countTextFonts = 0;
                textFontType = "empty";
                textFont = "emtpy";
            }

            string htmlContent = $@"
        <html>
            <head>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                    }}
                    h1, h2, h3 {{
                        margin-bottom: 10px;
                    }}
                    h1 {{
                        font-size: 24px;
                    }}
                    h2 {{
                        font-size: 20px;
                    }}
                    h3 {{
                        font-size: 18px;
                    }}
                    ul {{
                        font-size: 16px;
                    }}
                    li {{
                        margin-bottom: 10px;
                    }}
                </style>
            </head>
            <body>
                <h1>PDF Report</h1>
                <h3>Filename: {fileName}</h3>
                <h3>Size: {fileSize}</h3>
                <h3>Datetime: {dateTime}</h3>
                <h2>Result:</h2>
                <h2>Element counts:{countTextFonts}</h2>
                <h2>Font type:{textFont}</h2>
                <ul>
                    <hr/>
                    <li>Text fonts: <br/>{textFontType}</li>
                </ul>
            </body>
        </html>";

            // Configure the HTML to PDF conversion
            HtmlToPdf converter = new HtmlToPdf();

            converter.Options.MarginTop = 20;
            converter.Options.MarginRight = 20;
            converter.Options.MarginBottom = 20;
            converter.Options.MarginLeft = 20;
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

            PdfDocument document = converter.ConvertHtmlString(htmlContent);

            // Save the PDF document to the output path
            document.Save(outputPath);

            // Close the PDF document
            document.Close();
        }

        public void GeneratePDFFromTitle(
            string? title,
            int? countTableHeader,
            int? countHeader,
            string? textTableHeader,
            string? textHeader,
            int? countCheckBox,
            string? textCheckBox,
            string? fileName,
            string? fileSize,
            string? dateTime,
            int? countParagraph,
            string? textParagraph,
            string? countTable,
            string? countTableParagCount,
            string? countTableRow,
            string? countTableBulletPoint,
            string? textTableparag,
            string? textTableTextRow,
            string? textTableBulletPoint,
            string? outputPath)
        {
            if (string.IsNullOrEmpty(title))
            {
                title = "Default Title";
                countHeader = 0;
                countCheckBox = 0;
                fileName = "empty";
                fileSize = "empty";
                countParagraph = 0;
                countTable = "emtpy";
                countTableParagCount = "empty";
                countTableRow = "emtpy";
                countTableBulletPoint = "empty";
            }

            string htmlContent = $@"
                <html>
                    <head>
                        <style>
                            h1, h2, h3 {{
                                margin-bottom: 10px;
                            }}
                        </style>
                    </head>
                    <body>
                        <h1>{title}</h1>
                        <h3>Filename: {fileName}</h3>
                        <h3>Size: {fileSize}</h3>
                        <h3>Datetime: {dateTime}</h3>
                        <h2>Result:</h2>
                        <ul>
                            <li>Standard PDF:</li>
                            <li>Language:</li>
                            <li>Natural Language:</li>
                            <li>Embedded Files:</li>
                        </ul>
                        <h2>Element counts:</h2>
                        <ul>
                            <li>Table headers: {countTableHeader}<br/>{textTableHeader}</li>
                            <li>Headers: {countHeader}<br/>{textHeader}</li>
                            <li>Check box: {countCheckBox}<br/>{textCheckBox}</li>
                            <li>Paragraph: {countParagraph}<br/>{textParagraph}</li>
                            <li>Table count: {countTable}</li>
                            <li>Table Parag Count: {countTableParagCount}<br/>{textTableparag}</li>
                            <li>Table table row: {countTableRow}<br/>{textTableTextRow}</li>
                            <li>count bullet point: {countTableBulletPoint}<br/>{textTableBulletPoint}</li>
                        </ul>
                    </body>
                </html>";


            // Configure the HTML to PDF conversion
            HtmlToPdf converter = new HtmlToPdf();

            converter.Options.MarginTop = 20;
            converter.Options.MarginRight = 20;
            converter.Options.MarginBottom = 20;
            converter.Options.MarginLeft = 20;
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

            PdfDocument document = converter.ConvertHtmlString(htmlContent);

            // Save the PDF document to the output path
            document.Save(outputPath);

            // Close the PDF document
            document.Close();
        }


        public void GeneratePDFForCompare(string? titleFirst, string? titleSecond, string? titleThird, string? date, string? difference, string? outputPath)
        {
            if (string.IsNullOrEmpty(titleFirst) || string.IsNullOrEmpty(titleSecond) || string.IsNullOrEmpty(titleThird))
            {
                titleFirst = "First Report";
                titleSecond = "Second Report";
                titleThird = "Third Report";
                difference = "Empty";
                date = "Empty";
            }

            string htmlContent = $@"
                            <!DOCTYPE html>
                    <html>
                        <head>
                            <style>
                                body {{
                                    font-family: Arial, sans-serif;
                                    font-size: 14px;
                                    line-height: 1.6;
                                }}
                                h1 {{
                                    font-size: 24px;
                                }}
                                h2 {{
                                    font-size: 18px;
                                    color: #444;
                                    margin-bottom: 20px;
                                }}
                                .difference {{
                                    margin-bottom: 20px;
                                    background-color: #f8f8f8; /* Add a background color */
                                    padding: 10px; /* Add some padding */
                                    border-radius: 5px; /* Add some border radius for a softer look */
                                }}
                                .difference-key {{
                                    font-weight: bold;
                                }}
                                .difference-file1,
                                .difference-file2,
                                .difference-file3 {{
                                    margin-left: 20px;
                                }}
                            </style>
                        </head>
                        <body>
                            <h1 style='color: green; background-color: #e8f5e9;'>First file: {titleFirst}</h1>
                            <h1 style='color: blue; background-color: #e3f2fd;'>Second file: {titleSecond}</h1>
                            <h1 style='color: red; background-color: #ffebee;'>Third file: {titleThird}</h1>
                            <h2>{date}</h2>
                            {difference}
                        </body>
                    </html>";

            HtmlToPdf converter = new HtmlToPdf();

            converter.Options.MarginTop = 20;
            converter.Options.MarginRight = 20;
            converter.Options.MarginBottom = 20;
            converter.Options.MarginLeft = 20;
            converter.Options.PdfPageSize = PdfPageSize.A4;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

            PdfDocument document = converter.ConvertHtmlString(htmlContent);

            document.Save(outputPath);

            document.Close();
        }
    }
}
