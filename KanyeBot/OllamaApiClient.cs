using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/tags");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var models = JObject.Parse(json)["models"];
            return models?.ToObject<JArray>()?.Select(m => m["name"]?.ToString())?.ToArray() ?? Array.Empty<string>();
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