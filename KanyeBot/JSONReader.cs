using Newtonsoft.Json;

namespace KanyeBot
{
    internal class JSONReader
    {
        public required string Token { get; set; }
        public required string Prefix { get; set; }

        public static async Task<JSONReader> LoadAsync(string path = "config.json")
        {
            var configPath = Path.GetFullPath(path, AppContext.BaseDirectory);

            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException($"Configuration file not found: {configPath}");
            }

            using StreamReader sr = new(configPath);
            var json = await sr.ReadToEndAsync();

            var config = JsonConvert.DeserializeObject<JSONReader>(json)
                         ?? throw new Exception("Failed to deserialize JSON");

            return config;
        }
    }
}