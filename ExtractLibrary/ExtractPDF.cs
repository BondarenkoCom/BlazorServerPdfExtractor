using Adobe.PDFServicesSDK.auth;
using Adobe.PDFServicesSDK.io;
using Adobe.PDFServicesSDK.options.extractpdf;
using Adobe.PDFServicesSDK.pdfops;
using AdobeExecutionContext = Adobe.PDFServicesSDK.ExecutionContext;
using SpecFlowPdfReader.Helpers;
using ExtractLibrary.Helpers;

namespace ExtractLibrary
{
    public class ExtractPDF
    {
        private string extractedText;
        private string? zipResult = JsonHelper.GetZipFileForExtract();
        private string? credentialsFilePath = JsonHelper.GetValues().AdobeCredPath;
        private string? jsonResut = JsonHelper.GetValues().jsonPath;
        readonly ZipExtractor resExtract = new ZipExtractor();

        public async Task ExtractTextFromPDF(string pdfFilePath)
        {
            try
            {

                Adobe.PDFServicesSDK.auth.Credentials credentials = Adobe.PDFServicesSDK.auth.Credentials.ServiceAccountCredentialsBuilder()
            .FromFile(credentialsFilePath)
            .Build();

                FileRef sourceFileRef = FileRef.CreateFromLocalFile(pdfFilePath);

                ExtractPDFOptions extractPDFOptions = ExtractPDFOptions.ExtractPDFOptionsBuilder()
                    .AddElementsToExtract(new List<ExtractElementType> { ExtractElementType.TEXT, ExtractElementType.TABLES })
                    .Build();

                ExtractPDFOperation operation = ExtractPDFOperation.CreateNew();
                operation.SetInputFile(sourceFileRef);
                operation.SetOptions(extractPDFOptions);

                AdobeExecutionContext executionContext = AdobeExecutionContext.Create(credentials);
                FileRef result;

                result = await Task.Run(() => operation.Execute(executionContext));
                
                if (File.Exists(zipResult))
                {
                    File.Delete(zipResult);
                }
                result.SaveAs(zipResult);

                resExtract.ExtractZip(zipResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error - {ex.Message}");
            }
        }


        public async Task<string> ExtractTextFromPDFForCompare(string pdfFilePath)
        {
            try
            {

                Adobe.PDFServicesSDK.auth.Credentials credentials = Adobe.PDFServicesSDK.auth.Credentials.ServiceAccountCredentialsBuilder()
            .FromFile(credentialsFilePath)
            .Build();

                FileRef sourceFileRef = FileRef.CreateFromLocalFile(pdfFilePath);

                ExtractPDFOptions extractPDFOptions = ExtractPDFOptions.ExtractPDFOptionsBuilder()
                    .AddElementsToExtract(new List<ExtractElementType> { ExtractElementType.TEXT, ExtractElementType.TABLES })
                    .Build();

                ExtractPDFOperation operation = ExtractPDFOperation.CreateNew();
                operation.SetInputFile(sourceFileRef);
                operation.SetOptions(extractPDFOptions);

                AdobeExecutionContext executionContext = AdobeExecutionContext.Create(credentials);
                FileRef result;

                result = await Task.Run(() => operation.Execute(executionContext));

                if (File.Exists(zipResult))
                {
                    File.Delete(zipResult);
                }
                result.SaveAs(zipResult);

                string randomString = RandomStringName.GenerateRandomString(5);
                var path =  resExtract.ExtractZipCopy(zipResult , randomString);
                return path;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error - {ex.Message}");
            }
            return null;
        }
    }
}