using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace KanyeBot.Commands
{
    public class KanyeCommands : BaseCommandModule
    {
        [Command("kanye")]
        public async Task Kanye(CommandContext ctx)
        {
            string kanyeQuote = await KanyeRestClient.GetKanyeQuote();
            await ctx.RespondAsync(kanyeQuote);
        }

        [Command("kanye-tweet")]
        public async Task KanyeTweet(CommandContext ctx)
        {
            string prompt = File.ReadAllText("prompt.txt");
            Console.WriteLine("Asking kanye...");
            string kanyeRoast = await Program.OllamaApiClient.GenerateAsync("dolphin-mistral", prompt);
            Console.WriteLine("Kanye has answered");
            await ctx.RespondAsync(kanyeRoast);
        }

        [Command("test")]
        public async Task Test(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"Big Booty Latinas {ctx.User.Username}");
        }
    }
}
