namespace ExtractLibrary.Helpers
{
    public class GeneratorPDF
    {
        public void GenerateHtmlFromFont(
            string? jsonFileName,
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

            if (fileName.Contains("_"))
            {
                fileName = fileName.Substring(0, fileName.IndexOf("_"));
            }

            string xmlContent = textFontType.Replace("<hr/>", "\n\n").Replace("<", "&lt;").Replace(">", "&gt;");

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
                            <h1>HTML Report</h1>
                            <h3>Filename: {fileName}</h3>
                            <h3>JSON File Name: {jsonFileName}</h3
                            <h3>Size: {fileSize}</h3>
                            <h3>Datetime: {dateTime}</h3>
                            <h2>Result:</h2>
                            <h2>Element counts:{countTextFonts}</h2>
                            <h2>Font type:{textFont}</h2>
                            <ul>
                                <hr/>
                                <li>Text fonts: <br/>{textFontType}</li>
                                <li>Xml nodes: <p><pre>{xmlContent}</pre></p></li>
                            </ul>
                        </body>
                    </html>";

            string tempFilePath = Path.GetTempPath() + Guid.NewGuid().ToString() + ".html";
            File.WriteAllText(tempFilePath, htmlContent);

            File.Copy(tempFilePath, outputPath);
            Console.WriteLine($"paths - {tempFilePath} , {outputPath}");
        }

        public async Task GenerateрHTMLFromTitle(
            string? jsonFileName,
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

            if (fileName.Contains("_"))
            {
                fileName = fileName.Substring(0, fileName.IndexOf("_"));
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
                                .highlight {{
                                    color: red;
                                }}
                                .underline {{
                                    text-decoration: underline;
                                }}
                                table {{
                                    width: 100%;
                                    border-collapse: collapse;
                                }}
                                th, td {{
                                    border: 1px solid #ddd;
                                    padding: 8px;
                                }}
                                th {{
                                    background-color: #f2f2f2;
                                }}
                            </style>
                        </head>
                        <body>
                            <h1>{title}</h1>
                            <h3 class='highlight'>Filename: {fileName}</h3>
                            <h3>JSON File Name: {jsonFileName}</h3>
                            <h3>Size: {fileSize}</h3>
                            <h3>Datetime: {dateTime}</h3>
                            <h2 class='underline'>Element counts:</h2>
                            <table>
                                <tr>
                                    <th>Element</th>
                                    <th>Count</th>
                                    <th>Text</th>
                                </tr>
                                <tr>
                                    <td>Table headers</td>
                                    <td>{countTableHeader}</td>
                                    <td>{textTableHeader}</td>
                                </tr>
                                <tr>
                                    <td>Headers</td>
                                    <td>{countHeader}</td>
                                    <td>{textHeader}</td>
                                </tr>
                                <tr>
                                    <td>Check box</td>
                                    <td>{countCheckBox}</td>
                                    <td>{textCheckBox}</td>
                                </tr>
                                <tr>
                                    <td>Paragraph</td>
                                    <td>{countParagraph}</td>
                                    <td>{textParagraph}</td>
                                </tr>
                                <tr>
                                    <td>Table count</td>
                                    <td>{countTable}</td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Table Parag Count</td>
                                    <td>{countTableParagCount}</td>
                                    <td>{textTableparag}</td>
                                </tr>
                                <tr>
                                    <td>Table table row</td>
                                    <td>{countTableRow}</td>
                                    <td>{textTableTextRow}</td>
                                </tr>
                                <tr>
                                    <td>Count bullet point</td>
                                    <td>{countTableBulletPoint}</td>
                                    <td>{textTableBulletPoint}</td>
                                </tr>
                            </table>
                        </body>
                    </html>";


            string tempFilePath = Path.GetTempPath() + Guid.NewGuid().ToString().Substring(0, 5) + ".html";
            File.WriteAllText(tempFilePath, htmlContent);

            File.Copy(tempFilePath, outputPath);
            if (!File.Exists(outputPath))
            {
                throw new Exception($"File was not created at {outputPath}");
            }

            await Task.Delay(3000);
        }


        public void GenerateHtmlForCompare(string? titleFirst, string? titleSecond, string? titleThird, string? date, string? difference, string? outputPath)
        {

            if (string.IsNullOrEmpty(titleFirst) || string.IsNullOrEmpty(titleSecond) || string.IsNullOrEmpty(titleThird))
            {
                titleFirst = "First Report";
                titleSecond = "Second Report";
                titleThird = "Third Report";
                difference = "Empty";
                date = "Empty";
            }

            if (!string.IsNullOrEmpty(difference))
            {
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
                        <h1 style='color: blue; background-color: #e3f2fd;'>Second file: {titleSecond}</h1>";

                if (titleThird != "Nothing")
                {
                    htmlContent += $"<h1 style='color: red; background-color: #ffebee;'>Third file: {titleThird}</h1>";
                }

                htmlContent += $@"
                        <h2>{date}</h2>
                        {difference}
                    </body>
                </html>";

                string tempFilePath = Path.GetTempPath() + Guid.NewGuid().ToString() + ".html";
                File.WriteAllText(tempFilePath, htmlContent);

                File.Copy(tempFilePath, outputPath);
                Console.WriteLine($"paths - {tempFilePath} , {outputPath}");
            }
        }
    }
}
