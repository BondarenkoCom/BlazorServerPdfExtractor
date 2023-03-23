using Microsoft.AspNetCore.Components.Forms;
using Adobe.PDFServicesSDK.auth;
using Adobe.PDFServicesSDK.io;
using Adobe.PDFServicesSDK.options.extractpdf;
using Adobe.PDFServicesSDK.pdfops;
using AdobeExecutionContext = Adobe.PDFServicesSDK.ExecutionContext;
using Newtonsoft.Json;
using System.IO.Compression;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorServerPdfExtractor.Helper
{
    public class PdfTextExtractorService
    {
        private string pdfFilePath;
        private string extractedText;
        private string? credentialsFilePath = JsonHelper.GetValues().AdobeCredPath;
        private string? zipResult = JsonHelper.GetValues().zipPath;
        private string? jsonResut = JsonHelper.GetValues().jsonPath;
        readonly ZipExtractor resExtract = new ZipExtractor();
        private string? tempPath;
        private string? tempPdfFilePath;

        //TODO провести дебагг кода и найти ошибку куда девается PDF файл
        public  async Task<string> ExtractTextFromPDF(string Pdfpath)
        {
            Adobe.PDFServicesSDK.auth.Credentials credentials =
                Adobe.PDFServicesSDK.auth.Credentials.ServiceAccountCredentialsBuilder()
            .FromFile(credentialsFilePath)
            .Build();
            Console.WriteLine(credentials);

            FileRef sourceFileRef = FileRef.CreateFromLocalFile(Pdfpath);

            ExtractPDFOptions extractPDFOptions = ExtractPDFOptions.ExtractPDFOptionsBuilder()
                .AddElementsToExtract(new List<ExtractElementType> { ExtractElementType.TEXT, ExtractElementType.TABLES })
                .Build();
         
            ExtractPDFOperation operation = ExtractPDFOperation.CreateNew();
            operation.SetInputFile(sourceFileRef);
            operation.SetOptions(extractPDFOptions);
         
            AdobeExecutionContext executionContext = AdobeExecutionContext.Create(credentials);
            FileRef result;
         
            try
            {
                result = operation.Execute(executionContext);
                return result.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message; 
            }
         
            try
            {
                if (File.Exists(zipResult))
                {
                    File.Delete(zipResult);
                    File.Delete(tempPath);
                }
                result.SaveAs(zipResult);
                resExtract.ExtractZip(zipResult);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
