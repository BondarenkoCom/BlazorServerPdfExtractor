using Adobe.PDFServicesSDK.auth;
using Adobe.PDFServicesSDK.io;
using Adobe.PDFServicesSDK.options.extractpdf;
using Adobe.PDFServicesSDK.pdfops;
using AdobeExecutionContext = Adobe.PDFServicesSDK.ExecutionContext;
using SpecFlowPdfReader.Helpers;
using Newtonsoft.Json;
using ExtractTextTableInfoFromPDF.Models;

namespace ExtractLibrary
{
    public class ExtractPDF
    {
        //private string pdfFilePath;
        private string extractedText;
        private string? credentialsFilePath = @"D:\source\BlazorServerPdfExtractor\PDFServices.NET.SDK.Samples-master\ExtractTextTableInfoFromPDF\pdfservices-api-credentials.json";
        private string? zipResult = @"D:\source\BlazorServerPdfExtractor\BlazorServerPdfExtractor\JsonResults\resultFromPDF.zip";
        //private string? zipResult = JsonHelper.GetValues().zipPath;
        //private string? credentialsFilePath = JsonHelper.GetValues().AdobeCredPath;
        //private string? zipResult = JsonHelper.GetValues().zipPath;
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
                //todo 
                Console.WriteLine("point");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error - {ex.Message}");
            }
        }
    }
}