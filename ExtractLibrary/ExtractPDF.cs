﻿using Adobe.PDFServicesSDK.io;
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

        public async Task<Tuple<string, string>> ExtractTextFromPDF(string pdfFilePath)
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
        
                Directory.CreateDirectory(Path.GetDirectoryName(zipResult));
        
                result.SaveAs(zipResult);
                string randomString = RandomStringName.GenerateRandomString(5);
                var path = resExtract.ExtractZipCopy(zipResult, randomString);
                return Tuple.Create("Success",path);
        
            }
            catch (Exception ex)
            {
                return new Tuple<string, string>(ex.Message.ToString(), null);
            }
        }

        public async Task<Tuple<string, string>> ExtractTextFromPDFForCompare(string pdfFilePath)
        {
            try
            {
                if (!File.Exists(credentialsFilePath))
                {
                    throw new Exception($"Credentials file does not exist: {credentialsFilePath}");
                }

                string credentialsContents = File.ReadAllText(credentialsFilePath);
                if (string.IsNullOrEmpty(credentialsContents))
                {
                    throw new Exception($"Credentials file is empty: {credentialsFilePath}");
                }


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
                var path = resExtract.ExtractZipCopy(zipResult, randomString);
                return Tuple.Create(path, "Success");
            }
            catch (Exception ex)
            {
                return Tuple.Create<string, string>(null, $"Error - {ex.Message}");
            }
            return null;
        }
    }
}