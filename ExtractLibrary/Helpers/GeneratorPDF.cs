using SelectPdf;

namespace ExtractLibrary.Helpers
{
    public class GeneratorPDF
    {
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
            //converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.ShrinkOnly;
            //converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.ShrinkOnly;
            // Perform the conversion

            PdfDocument document = converter.ConvertHtmlString(htmlContent);

            // Save the PDF document to the output path
            document.Save(outputPath);

            // Close the PDF document
            document.Close();
        }


        public void GeneratePDFForCompare(string? title, string? titleSecond , string? date, string? difference, string? outputPath)
        {
            if (string.IsNullOrEmpty(title))
            {
                title = "Report";
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
                    }}
                    .difference-key {{
                        font-weight: bold;
                    }}
                    .difference-file1,
                    .difference-file2 {{
                        margin-left: 20px;
                    }}
                </style>
            </head>
            <body>
                <h1>First file: {title}</h1>
                <h1>Second file: {titleSecond}</h1>
                <h2>{date}</h2>
                {difference}
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
            //converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.ShrinkOnly;
            //converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.ShrinkOnly;

            // Perform the conversion
            PdfDocument document = converter.ConvertHtmlString(htmlContent);

            // Save the PDF document to the output path
            document.Save(outputPath);

            // Close the PDF document
            document.Close();

        }
    }
}
