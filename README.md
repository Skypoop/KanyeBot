---
# KanyeBot

KanyeBot is a Discord bot built with **DSharpPlus** and **.NET 8.0**, designed to bring unhinged Kanye West-inspired interactions to your server. The bot features slash commands as well as prefix-based alternatives for a clean and modern user experience.

## Features

- **Random Kanye Quotes**  
  Use a command to fetch random Kanye West quotes from the [Kanye REST API](https://api.kanye.rest).

- **Generate Kanye Tweets**  
  Generate new Kanye-style tweets based on his previous tweets using a locally hosted **Ollama API**. This leverages the **dolphin-mistral** uncensored model for optimal results.  

- **Slash Commands & Prefix Alternatives**  
  All commands are available as easy-to-use slash commands, but you can also use the default prefix `!` for those who prefer classic command styles.

## Getting Started

### Prerequisites

Before running KanyeBot, ensure you have the following:

1. **.NET 8.0 SDK**  
   Download and install the .NET 8.0 SDK from [Microsoft's official site](https://dotnet.microsoft.com/).

2. **DSharpPlus**  
   Make sure the required DSharpPlus library is installed. It will be handled during the build process if dependencies are properly defined.

3. **Ollama API**  
   - Install the Ollama API for local use. Visit [Ollama's website](https://www.ollama.com/) for installation instructions.
   - Run the Ollama API using the following command:  
     ```bash
     ollama serve
     ```
     The API should run on the default address `127.0.0.1:11434`.

4. **Dolphin-Mistral Model**  
   Set up the uncensored **dolphin-mistral** model for the best tweet generation results.
     ```bash
     ollama pull dolphin-mistral
     ```

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/Skypoop/KanyeBot.git
   ```
2. Navigate to the project directory:
   ```bash
   cd KanyeBot
   ```
3. Build the project:
   ```bash
   dotnet build
   ```
4. Run the bot:
   ```bash
   dotnet run
   ```

### Configuration

- Update the `config.json.example` file with your own bot token and rename it to `config.json`. Refer to the source code for specifics.
- Ensure the Ollama API is running (`ollama serve`) before executing commands that generate Kanye tweets.

## Commands

### Slash Commands

- `/kanye`  
  Fetches a random Kanye West quote.

- `/kanye-tweet`  
  Generates a new Kanye-style tweet using the dolphin-mistral model.

### Prefix Commands (Default: `!`)

- `!kanye`  
  Same as `/kanye`.

- `!kanye-tweet`  
  Same as `/kanye-tweet`.

## Contributing

Contributions are welcome! Feel free to fork the repository, make your changes, and submit a pull request.

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch-name`).
3. Commit your changes (`git commit -m "Add some feature"`).
4. Push to the branch (`git push origin feature-branch-name`).
5. Open a Pull Request.

## License

This project is licensed under the [MIT License](LICENSE).

## Acknowledgements

- [Kanye REST API](https://api.kanye.rest) for random Kanye quotes.
- [Ollama API](https://www.ollama.com/) for tweet generation.
- The **DSharpPlus** library for facilitating Discord bot development.

---
