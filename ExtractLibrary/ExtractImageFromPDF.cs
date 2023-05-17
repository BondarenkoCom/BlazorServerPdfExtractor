using Adobe.PDFServicesSDK.io;
using Adobe.PDFServicesSDK.options.extractpdf;
using Adobe.PDFServicesSDK.pdfops;
using AdobeExecutionContext = Adobe.PDFServicesSDK.ExecutionContext;
using SpecFlowPdfReader.Helpers;
using ExtractLibrary.Helpers;

namespace ExtractLibrary
{
    public class ExtractImageFromPDF
    {
        private string extractedText;
        private string? zipResult = JsonHelper.GetZipFileForExtract();
        private string? credentialsFilePath = JsonHelper.GetValues().AdobeCredPath;
        private string? jsonResut = JsonHelper.GetValues().jsonPath;
        readonly ZipExtractor resExtract = new ZipExtractor();

        public async Task<string> ExtractImagesFromPDF(string pdfFilePath)
        {
            try
            {
                Adobe.PDFServicesSDK.auth.Credentials credentials = Adobe.PDFServicesSDK.auth.Credentials.ServiceAccountCredentialsBuilder()
            .FromFile(credentialsFilePath)
            .Build();

                FileRef sourceFileRef = FileRef.CreateFromLocalFile(pdfFilePath);

                ExtractPDFOptions extractPDFOptions = ExtractPDFOptions.ExtractPDFOptionsBuilder()
                    .AddElementsToExtractRenditions(new List<ExtractRenditionsElementType> { ExtractRenditionsElementType.FIGURES })
                    .Build();

                ExtractPDFOperation operation = ExtractPDFOperation.CreateNew();
                operation.SetInputFile(sourceFileRef);
                operation.SetOptions(extractPDFOptions);

                AdobeExecutionContext executionContext = AdobeExecutionContext.Create(credentials);
                FileRef result;

                result = await Task.Run(() => operation.Execute(executionContext));

                // Create new directory for images if it doesn't exist
                string imageDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Images");
                if (!Directory.Exists(imageDirectoryPath))
                {
                    Directory.CreateDirectory(imageDirectoryPath);
                }

                // Define the path for the zip result
                string zipResultPath = Path.Combine(imageDirectoryPath, "extractedImages.zip");

                if (File.Exists(zipResultPath))
                {
                    File.Delete(zipResultPath);
                }
                result.SaveAs(zipResultPath);

                //resExtract.ExtractZip(zipResultPath);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}