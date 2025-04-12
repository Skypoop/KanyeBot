using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace KanyeBot.Commands.Slash;

public class KanyeSlashCommands : ApplicationCommandModule
{
    [SlashCommand("kanye", "Get a random Kanye quote")]
    public static async Task Kanye(InteractionContext itx)
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

    [SlashCommand("ask-kanye", "Talk with Kanye")]
    public static async Task AskKanye(
        InteractionContext itx,
        [Option("question", "Ask Kanye anything")] string question)
    {
        try
        {
            if (Program.OllamaApiClient == null)
            {
                throw new InvalidOperationException("OllamaApiClient is not initialized.");
            }

            await itx.CreateResponseAsync(
                InteractionResponseType.DeferredChannelMessageWithSource
            );

            var KanyeResponse = await Program.OllamaApiClient.GenerateResponseAsync("dolphin-mistral",question);

            await itx.EditResponseAsync(
                new DiscordWebhookBuilder()
                    .WithContent(KanyeResponse)
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await itx.CreateResponseAsync(
                InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder()
                    .WithContent("An error occurred while processing your question.")
                    .AsEphemeral()
            );
        }
    }


    [SlashCommand("kanye-tweet", "Get a AI generated Kanye tweet")]
    public static async Task KanyeTweet(InteractionContext itx)
    {
        try
        {
            if (Program.OllamaApiClient == null)
            {
                throw new InvalidOperationException("OllamaApiClient is not initialized.");
            }

            await itx.CreateResponseAsync(
                InteractionResponseType.DeferredChannelMessageWithSource
            );

            var kanyeRoast = await Program.OllamaApiClient.GenerateTweetAsync("dolphin-mistral");

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
