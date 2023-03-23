using BlazorServerPdfExtractor.Model;
using Newtonsoft.Json;


namespace BlazorServerPdfExtractor.Helper
{
    public class JsonHelper
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
    }
}
