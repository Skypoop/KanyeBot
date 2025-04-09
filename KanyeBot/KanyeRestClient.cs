using Newtonsoft.Json.Linq;

namespace KanyeBot
{
    public static class KanyeRestClient
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string?> GetKanyeQuote()
        {
            try
            {
                var response = await client.GetStringAsync("https://api.kanye.rest/");
                var json = JObject.Parse(response);

                if (json == null)
                {
                    throw new Exception("Failed to parse JSON");
                }
                
                if (!json.TryGetValue("quote", out var quote))
                {
                    throw new Exception("Quote not found in JSON");
                }
                
                return quote.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}