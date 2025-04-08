using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using bot_test;
using System.IO;
using System;

namespace bot_test.commands
{
    public class KanyeCommands : BaseCommandModule
    {
        [Command("kanye")]
        public async Task Kanye(CommandContext ctx)
        {
            string kanyeQuote = await KanyeRestClient.GetKanyeQuote();
            await ctx.RespondAsync(kanyeQuote);
        }

        [Command("kanye-roast")]
        public async Task KanyeRoast(CommandContext ctx)
        {
            Console.WriteLine("Reading prompt");
            string prompt = File.ReadAllText("prompt.txt");
            Console.WriteLine($"Prompt length: {prompt.Length}");
            string kanyeRoast = await Program.OllamaApiClient.GenerateAsync("dolphin-mistral", prompt);
            await ctx.RespondAsync(kanyeRoast);
        }
        //[Command("kanye")]
        //public async Task Kanye(CommandContext ctx)
        //{
        //    string kanyeQuote = await KanyeRestClient.GetKanyeQuote();
        //    await ctx.RespondAsync(kanyeQuote);
        //}

        [Command("test")]
        public async Task Test(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync($"Big Booty Latinas {ctx.User.Username}");
        }
    }
}