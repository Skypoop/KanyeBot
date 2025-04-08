﻿using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using bot_test;

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
            string kanyeRoast = await Program.OllamaApiClient.GenerateAsync("dolphin-mistral", "-- enter prompt --");
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
