using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KanyeBot
{
    public class OllamaApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public OllamaApiClient(string baseUrl = "http://localhost:11434")
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl.TrimEnd('/');
        }

        public async Task<string[]> ListModelsAsync()
        {
            string[] models = Array.Empty<string>();

            HttpResponseMessage response = await _httpClient.GetAsync($"{_baseUrl}/api/tags");

            try
            {
              response.EnsureSuccessStatusCode();
            } catch (HttpRequestException e) {
              Console.WriteLine($"Ollama http request failed: {e.Message}");
              return models;
            }

            var json = await response.Content.ReadAsStringAsync();
            var jsons = JObject.Parse(json)["models"];
            models = jsons?.ToObject<JArray>()?.Select(m => m["name"]?.ToString())?.ToArray() ?? models;
            return models;
        }

        public async Task<string> GenerateAsync(string model, string prompt)
        {
            var requestBody = new
            {
                model = model,
                prompt = prompt,
                stream = false,
                //system = system,
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/api/generate", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();
            var parsed = JObject.Parse(result);
            return parsed["response"]?.ToString() ?? string.Empty;
        }

        public async Task<string> GetServerStatusAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}/");
                response.EnsureSuccessStatusCode();
                return "Ollama server is running.";
            }
            catch (Exception ex)
            {
                return $"Failed to reach Ollama server: {ex.Message}";
            }
        }
    }
}