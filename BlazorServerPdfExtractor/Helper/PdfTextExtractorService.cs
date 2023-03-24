using Microsoft.AspNetCore.Components.Forms;
using Adobe.PDFServicesSDK.auth;
using Adobe.PDFServicesSDK.io;
using Adobe.PDFServicesSDK.options.extractpdf;
using Adobe.PDFServicesSDK.pdfops;
using AdobeExecutionContext = Adobe.PDFServicesSDK.ExecutionContext;
using Newtonsoft.Json;
using System.IO.Compression;
using Microsoft.AspNetCore.Components.Forms;

// Add these using statements
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlazorServerPdfExtractor.Helper
{
    public class PdfTextExtractorService
    {
        private string extractedText;
        //private string? credentialsFilePath = JsonHelper.GetValues().AdobeCredPath;
        //private string? zipResult = JsonHelper.GetValues().zipPath;
        private string? jsonResut = JsonHelper.GetValues().jsonPath;
        readonly ZipExtractor resExtract = new ZipExtractor();
        private string? tempPath;
        private string? tempPdfFilePath;
        private string stringPath = @"D:\source\BlazorServerPdfExtractor\BlazorServerPdfExtractor\pdfFiles\example.pdf";
        private string zipResult = @"D:\source\BlazorServerPdfExtractor\BlazorServerPdfExtractor\JsonResults\";
        private string credentialsFilePath = "D:\\source\\SpecFlowReadPdf\\SpecFlowPdfReader\\SpecFlowPdfReader\\Resourses\\pdfservices-api-credentials.json";

        //TODO провести дебагг кода и найти ошибку куда девается PDF файл
        //public  async Task<string> ExtractTextFromPDF(string Pdfpath)
        //public void ExtractTextFromPDF(string Pdfpath)
        public void ExtractTextFromPDF(string Pdfpath)
        {
            Adobe.PDFServicesSDK.auth.Credentials credentials =
                Adobe.PDFServicesSDK.auth.Credentials.ServiceAccountCredentialsBuilder()
            .FromFile(credentialsFilePath)
            .Build();

            FileRef sourceFileRef = FileRef.CreateFromLocalFile(stringPath);

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
                result.SaveAs(zipResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
