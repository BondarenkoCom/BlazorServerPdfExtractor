using SelectPdf;

namespace ExtractLibrary.Helpers
{
    public class GeneratorPDF
    {
        public void GeneratePDFFromTitle(
            string? title,
            int? countHeader,
            int? countCheckBox,
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

            // Prepare HTML content using the title
            string htmlContent = $@"
                <html>
                    <body>
                        <h1>{title}</h1>
                        <h3>Filename: {fileName}</h3>
                        <h3>Language: </h3>
                        <h3>Size: {fileSize}</h3>
                        <h1>Result: </h1>
                        <h2>Datetime: {dateTime}</h2>
                        <h2>Standard PDF: </h2>
                        <h2>Table headers: {countHeader}</h2>
                        <h2>Check box: {countCheckBox}</h2>
                        <h2>Paragraph: {countParagraph}:<br/>{textParagraph}</h2>
                        <h2>Embedded Files: </h2>
                        <h2>Natural Language: </h2>
                        <h2>Table count: {countTable}</h2>
                        <h2>Table Parag Count: {countTableParagCount}:<br/>{textTableparag}</h2>
                        <h2>Table table row: {countTableRow}:<br/>{textTableTextRow}</h2>
                        <h2>count bullet point: {countTableBulletPoint}:<br/>{textTableBulletPoint}</h2>
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

            // Perform the conversion
            PdfDocument document = converter.ConvertHtmlString(htmlContent);

            // Save the PDF document to the output path
            document.Save(outputPath);

            // Close the PDF document
            document.Close();
        }


        public void GeneratePDFForCompare(string? title, string? date, string? difference, string? outputPath)
        {
            if (string.IsNullOrEmpty(title))
            {
                title = "Report";
                difference = "Empty";
                date = "Empty";
            }

            // Prepare HTML content using the title
            string htmlContent = $@"
                <html>
                    <body>
                        <h1>{title}</h1>
                        <h2>{date}</h2>
                        <p>{difference}</p
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

            // Perform the conversion
            PdfDocument document = converter.ConvertHtmlString(htmlContent);

            // Save the PDF document to the output path
            document.Save(outputPath);

            // Close the PDF document
            document.Close();

        }
    }
}
