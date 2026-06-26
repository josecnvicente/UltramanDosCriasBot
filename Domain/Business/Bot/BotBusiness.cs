using Discord;
using Discord.WebSocket;
using Domain.DTO.Config;
using Domain.Interface.Business.Bot;

namespace Domain.Business.Bot;

public class BotBusiness(IChoicesBusiness choicesBusiness) : IBotBusiness
{
    private DiscordSocketClient _client = new();

    public async Task RunBotAsync()
    {
        var configuracoes = new DiscordSocketConfig()
        {
            GatewayIntents = GatewayIntents.Guilds |
                             GatewayIntents.GuildMessages |
                             GatewayIntents.MessageContent
        };

        _client = new DiscordSocketClient(configuracoes);
        _client.Log += LogAsync;
        _client.MessageReceived += MessageReceivedAsync;

        string token = ConfigDto.DiscordConfig.Token;

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();

        await Task.Delay(-1);
    }

    private async Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log);
    }

    private async Task MessageReceivedAsync(SocketMessage message)
    {
        await choicesBusiness.ChoseCoice(message);
    }
}
