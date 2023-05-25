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

            Console.WriteLine($"Attempting to read JSON values from: {jsonFilePath}");
            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine($"File not found: {jsonFilePath}");
                return null;
            }

            string json = File.ReadAllText(jsonFilePath);
            PathModel pathModel = JsonConvert.DeserializeObject<PathModel>(json);

            pathModel.AdobeCredPath = Path.Combine(currentDirectory, pathModel.AdobeCredPath);

            return pathModel;
        }

        public static string GetZipFileForExtract()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string FolderPath = Path.Combine(currentDirectory, "tempZip", "resultFromPDF.zip");

            Console.WriteLine($"Zip file for extraction: {FolderPath}");
            return FolderPath;
        }

        public static string GetFolderTargetForExtract()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string FolderPath = Path.Combine(currentDirectory, "tempZip");

            Console.WriteLine($"Folder target for extraction: {FolderPath}");
            return FolderPath;
        }

        public static string GetFolderJsonResult()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string FolderPath = Path.Combine(currentDirectory, "JsonResults");

            Console.WriteLine($"Folder for JSON results: {FolderPath}");
            return FolderPath;
        }

        public static string GetJsonFile()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string JsonPath = Path.Combine(currentDirectory, "JsonResults", "structuredData.json");

            Console.WriteLine($"JSON file path: {JsonPath}");
            return JsonPath;
        }

        public static (string, string) ReportPathName(string path, string uniqueKeyNamePath)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string JsonPath = Path.Combine(path, "reports", $"report_{uniqueKeyNamePath}.html");

            Console.WriteLine($"Report path name: {JsonPath}");
            return (JsonPath, uniqueKeyNamePath);
        }

        public static (string settingPath, string ClientId, string ClientSecret,
     string OrganizationId, string accountId, string privateKeyFile, string privateTextContain
     ) GetAbodeKeys()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string jsonPath = Path.Combine(currentDirectory, "Resourses", "pdfservices-api-credentials.json");

            Console.WriteLine($"Attempting to read credentials from: {jsonPath}");
            if (!File.Exists(jsonPath))
            {
                Console.WriteLine($"Credentials file not found: {jsonPath}");
            }

            string jsonPathPrivateKey = Path.Combine(currentDirectory, "Resourses", "private.key");
            Console.WriteLine($"Attempting to read private key from: {jsonPathPrivateKey}");
            if (!File.Exists(jsonPathPrivateKey))
            {
                Console.WriteLine($"Private key file not found: {jsonPathPrivateKey}");
            }

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

            Console.WriteLine($"Updating Adobe keys in: {jsonPath}");
            string jsonString = File.ReadAllText(jsonPath);
            Credentials credentials = JsonConvert.DeserializeObject<Credentials>(jsonString);

            credentials.client_credentials.client_id = clientId;
            credentials.client_credentials.client_secret = clientSecret;
            credentials.service_account_credentials.organization_id = organizationId;
            credentials.service_account_credentials.account_id = accountId;

            string updatedJsonString = JsonConvert.SerializeObject(credentials, Formatting.Indented);
            File.WriteAllText(jsonPath, updatedJsonString);

            Console.WriteLine($"Updating private key in: {jsonPathPrivateKey}");
            File.WriteAllText(jsonPathPrivateKey, privateKeyText);
        }
    }
}
