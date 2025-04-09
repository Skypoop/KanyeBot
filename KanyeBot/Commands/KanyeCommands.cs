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
            await ctx.RespondAsync(kanyeQuote);
        }

        [Command("kanye-tweet")]
        public async Task KanyeTweet(CommandContext ctx)
        {
            var prompt = File.ReadAllText("prompt.txt");
            Console.WriteLine("Asking kanye...");
            var kanyeRoast = await Program.OllamaApiClient.GenerateAsync("dolphin-mistral", prompt);
            Console.WriteLine("Kanye has answered");
            await ctx.RespondAsync(kanyeRoast);
        }
    }
}
