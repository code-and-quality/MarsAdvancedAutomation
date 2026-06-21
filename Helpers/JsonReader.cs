using Newtonsoft.Json;

namespace MarsAdvancedAutomation.Helpers
{
    public static class JsonReader
    {
        public static T ReadJson<T>(string relativePath)
        {
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var fullPath = Path.Combine(basePath, relativePath);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"JSON file not found: {fullPath}");

            var json = File.ReadAllText(fullPath);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}


/*using Newtonsoft.Json;

namespace MarsAdvancedAutomation.Helpers
{
    public static class JsonReader
    {
        public static T ReadJson<T>(string filePath)
        {
            var json = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}*/