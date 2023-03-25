using SelectPdf;

namespace ExtractLibrary.Helpers
{
    public class GeneratorPDF
    {
        public void GeneratePDFFromTitle(string? title, string outputPath)
        {
            if (string.IsNullOrEmpty(title))
            {
                title = "Default Title";
            }

            // Prepare HTML content using the title
            string htmlContent = $@"
                <html>
                    <body>
                        <h1>{title}</h1>
                        <h3>Filename: </h3>
                        <h3>Language: </h3>
                        <h3>Size: </h3>
                        <h1>Result: </h1>
                        <p>Datetime: </p>
                        <p>Standard PDF: </p>
                        <p>Fonts: </p>
                        <p>Content: </p>
                        <p>Embedded Files: </p>
                        <p>Natural Language: </p>
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
