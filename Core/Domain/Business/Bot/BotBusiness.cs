using Discord;
using Discord.WebSocket;
using Domain.DTO.Config;
using Domain.Interface.Bot;

namespace ConsoleApp.Bot;

public class BotBusiness: IBotBusiness
{
    private DiscordSocketClient _client;
    private IChoicesBusiness _choices;

    public BotBusiness(IChoicesBusiness choicesBusiness)
        => _choices = choicesBusiness;

    public async Task RunBotAsync()
    {
        var configuracoes = new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.All
        };

        _client = new DiscordSocketClient(configuracoes);
        _client.Log += LogAsync;
        _client.MessageReceived += MessageReceivedAsync;

        string token = Configuration.DiscordConfig.Token;

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        await Task.Delay(-1);
    }

    private Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log);
        return Task.CompletedTask;
    }

    private async Task MessageReceivedAsync(SocketMessage message)
    {
        await _choices.ChoseCoice(message);
    }
}
