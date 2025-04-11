using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace KanyeBot.Commands.Slash;

public class KanyeSlashCommands : ApplicationCommandModule
{
    [SlashCommand("kanye", "Get a random Kanye quote")]
    public async Task Kanye(InteractionContext itx)
    {
        try
        {
            var quote = await KanyeRestClient.GetKanyeQuote();
            await itx.CreateResponseAsync(
                InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder()
                    .WithContent(quote)
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await itx.CreateResponseAsync(
                InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder()
                    .WithContent("An error occurred while fetching the quote.")
                    .AsEphemeral()
            );
        }
    }

    [SlashCommand("kanye-tweet", "Get a AI generated Kanye tweet")]
    public async Task KanyeTweet(InteractionContext itx)
    {
        try
        {
            if (Program.OllamaApiClient == null)
            {
                throw new InvalidOperationException("OllamaApiClient is not initialized.");
            }

            var prompt = File.ReadAllText("prompt.txt");
            await itx.CreateResponseAsync(
                InteractionResponseType.DeferredChannelMessageWithSource
            );

            var kanyeRoast = await Program.OllamaApiClient.GenerateAsync("dolphin-mistral", prompt);

            await itx.EditResponseAsync(
                new DiscordWebhookBuilder()
                    .WithContent(kanyeRoast)
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await itx.CreateResponseAsync(
                InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder()
                    .WithContent("An error occurred while generating the tweet.")
                    .AsEphemeral()
            );
        }
    }
}