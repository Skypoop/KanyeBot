using Newtonsoft.Json.Linq;

namespace KanyeBot
{
    public static class KanyeRestClient
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<string> GetKanyeQuote()
        {
            var response = await client.GetStringAsync("https://api.kanye.rest/");
            var json = JObject.Parse(response);
            return json["quote"].ToString();
        }
    }
}