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

            //string JsonPath = Path.Combine(currentDirectory, "Reports", "output.pdf");

            return JsonPath;
        }
    }
}