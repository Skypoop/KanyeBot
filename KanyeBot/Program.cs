using KanyeBot.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using KanyeBot.Commands.Slash;

namespace KanyeBot
{
    internal class Program
    {
        private static DiscordClient? Client { get; set; }
        private static CommandsNextExtension? Commands { get; set; }
        public static OllamaApiClient? OllamaApiClient { get; set; }

        static async Task Main(string[] args)
        {
            var config = await JSONReader.LoadAsync();
            OllamaApiClient = new OllamaApiClient();

            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = config.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            Client = new DiscordClient(discordConfig);
            
            var slash = Client.UseSlashCommands();
            
            slash.RegisterCommands<KanyeSlashCommands>();

            Client.Ready += Client_Ready;

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = [config.Prefix],
                EnableDms = true,
                EnableMentionPrefix = true,
                DmHelp = true,
                EnableDefaultHelp = true,
            };

            Commands = Client.UseCommandsNext(commandsConfig);
            Commands.RegisterCommands<KanyeCommands>();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task Client_Ready(DiscordClient sender, ReadyEventArgs args)
        {
            Console.WriteLine("Bot is ready");
            return Task.CompletedTask;
        }
    }
}