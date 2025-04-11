using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace KanyeBot.Commands
{
    public class KanyeCommands : BaseCommandModule
    {
        [Command("kanye")]
        public async Task Kanye(CommandContext ctx)
        {
            var kanyeQuote = await KanyeRestClient.GetKanyeQuote();
            if (string.IsNullOrWhiteSpace(kanyeQuote))
            {
                await ctx.RespondAsync("No quote was retrieved. Please try again later.");
                return;
            }
            await ctx.RespondAsync(kanyeQuote);
        }

        [Command("kanye-tweet")]
        public async Task KanyeTweet(CommandContext ctx)
        {
            if (Program.OllamaApiClient == null)
            {
                await ctx.RespondAsync("The Ollama API client is not initialized.");
                return;
            }

            var prompt = File.ReadAllText("prompt.txt");
            Console.WriteLine("Asking kanye...");
            var kanyeRoast = await Program.OllamaApiClient.GenerateAsync("dolphin-mistral", prompt);
            Console.WriteLine("Kanye has answered");
            await ctx.RespondAsync(kanyeRoast);
        }
    }
}
