using ExtractTextTableInfoFromPDF.Models;
using Newtonsoft.Json;
using SpecFlowPdfReader.Models;

namespace SpecFlowPdfReader.Helpers
{
    public static class JsonHelper
    {
        public static PathModel? GetValues()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string jsonFilePath = Path.Combine(currentDirectory, "Resourses", "Paths.json");

            Console.Write(jsonFilePath);
            if (!File.Exists(jsonFilePath))
                return null;

            string json = File.ReadAllText(jsonFilePath);
            return JsonConvert.DeserializeObject<PathModel>(json);
        }

        public static string GetZipFileForExtract()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string FolderPath = Path.Combine(currentDirectory, "tempZip", "resultFromPDF.zip");

            return FolderPath;
        }

        public static string GetFolderTargetForExtract()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string FolderPath = Path.Combine(currentDirectory, "tempZip");

            return FolderPath;
        }

        public static string GetFolderJsonResult()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string FolderPath = Path.Combine(currentDirectory, "JsonResults");

            return FolderPath;
        }

        public static string GetJsonFile()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string JsonPath = Path.Combine(currentDirectory, "JsonResults", "structuredData.json");

            return JsonPath;
        }

        public static string ReportPathName(string path)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string JsonPath = Path.Combine(path, "reports", "report.pdf");

            return JsonPath;
        }

        public static (string settingPath,string ClientId, string ClientSecret,
            string OrganizationId, string accountId, string privateKeyFile, string privateTextContain
            ) GetAbodeKeys()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string jsonPath = Path.Combine(currentDirectory, "Resourses", "pdfservices-api-credentials.json");

            string jsonPathPrivateKey = Path.Combine(currentDirectory, "Resourses", "private.key");
            var pathPrivateKeyText = File.ReadAllText(jsonPathPrivateKey);

            string jsonString = File.ReadAllText(jsonPath);
            Credentials credentials = JsonConvert.DeserializeObject<Credentials>(jsonString);

            return (
                currentDirectory.ToString(),
                credentials.client_credentials.client_id,
                credentials.client_credentials.client_secret,
                credentials.service_account_credentials.organization_id,
                credentials.service_account_credentials.account_id,
                credentials.service_account_credentials.private_key_file,
                pathPrivateKeyText.ToString()
                );
        }

        public static void UpdateAdobeKeys(string clientId, string clientSecret, string organizationId,
            string accountId, string privateKeyText)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string jsonPath = Path.Combine(currentDirectory, "Resourses", "pdfservices-api-credentials.json");
            string jsonPathPrivateKey = Path.Combine(currentDirectory, "Resourses", "private.key");

            string jsonString = File.ReadAllText(jsonPath);
            Credentials credentials = JsonConvert.DeserializeObject<Credentials>(jsonString);

            credentials.client_credentials.client_id = clientId;
            credentials.client_credentials.client_secret = clientSecret;
            credentials.service_account_credentials.organization_id = organizationId;
            credentials.service_account_credentials.account_id = accountId;

            string updatedJsonString = JsonConvert.SerializeObject(credentials, Formatting.Indented);
            File.WriteAllText(jsonPath, updatedJsonString);

            File.WriteAllText(jsonPathPrivateKey, privateKeyText);
        }

    }
}