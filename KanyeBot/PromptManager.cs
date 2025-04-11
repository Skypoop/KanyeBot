using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KanyeBot;

class PromptManager
{
    private const string TweetsFilename = "tweets.json";
    private const string TweetPromptsFilename = "tweet_prompt.txt";
    private const string TweetPromptStart =
        "Every tweet stops after 3 newline characters, Here are some tweets from Kanye West:";
    private const string TweetPromptEnd =
        "\n \n \nI have provided you with tweets kanye has posted. You are kanye west. Create ONE unhinged new tweet kanye would post. Respond with tweet contents only.";

    private static PromptManager? _instance = null;
    private static object _singletonLock = new object();

    public static PromptManager Instance
    {
        get
        {
            lock (_singletonLock)
            {
                if (_instance is not null)
                    return _instance;
                else
                    _instance = new PromptManager();
                return _instance;
            }
        }
    }
    public string RandomizedTweetPrompt
    {
        get
        {
            ShuffleTweetPrompt();
            return TweetPrompt;
        }
    }

    private string TweetPrompt { get; set; }
    private JArray TweetsCluster { get; init; }
    private Random Rng { get; init; }

    private PromptManager()
    {
        Rng = new Random();
        TweetPrompt = "";
        using (StreamReader jsonFile = File.OpenText(TweetsFilename))
        {
            JToken json = JToken.ReadFrom(new JsonTextReader(jsonFile));
            TweetsCluster =
                json.ToObject<JArray>()
                ?? throw new JsonException(
                    $"Error in instantiating {this}: json of type JToken is not convertible to type JArray"
                );
        }
        ShuffleTweetPrompt();
    }

    // randomizes with O(n) complexity
    // specific algorithm used:
    // https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_modern_algorithm
    private void ShuffleTweetPrompt(bool writeToFile = true)
    {
        // shuffle cluster
        JArray randomizedCluster = TweetsCluster;
        int i = randomizedCluster.Count;
        while (i > 1)
        {
            i--;
            // roll
            int k = Rng.Next(maxValue: i + 1);

            // swap random element and iterating element
            JToken tokenK = randomizedCluster[k];
            randomizedCluster[k] = randomizedCluster[i];
            randomizedCluster[i] = tokenK;
        }

        // build prompt
        StringBuilder sb = new StringBuilder(TweetPromptStart);
        foreach (JObject jObj in TweetsCluster)
        {
            string tweetContent = "";
            string tweetDate = "";
            JToken? text = jObj["tweet_data"]?["tweet"]?["text"];
            JToken? date = jObj["tweet_data"]?["tweet"]?["created_at"];
            if (text is not null && date is not null)
            {
                tweetDate = date.ToString();
                tweetContent = text.ToString();
                sb.Append("\n\n");
                sb.Append(tweetDate);
                sb.Append('\n');
                sb.Append(tweetContent);
                sb.Append("\n\n");
            }
            else
                Console.WriteLine(
                    $"ERROR: tried to retrieve tweet content in {this}.{nameof(ShuffleTweetPrompt)}, but found null!"
                );
        }
        sb.Append(TweetPromptEnd);

        // assign prompt
        TweetPrompt = sb.ToString();

        // optionally write to file
        if (writeToFile == false)
            return;
        else
        {
            string promptsPath = Path.Combine(Environment.CurrentDirectory, TweetPromptsFilename);
            using (StreamWriter promptsFile = new StreamWriter(path: promptsPath, append: false))
                promptsFile.Write(TweetPrompt);
        }
    }
}
