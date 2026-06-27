using Discord;
using Discord.WebSocket;
using Domain.DTO.Config;
using Domain.Interface.Business.Bot;
using Microsoft.Extensions.Logging;

namespace Domain.Business.Bot;

public class BotBusiness(IChoicesBusiness choicesBusiness,
    IBirthdayBusiness birthdayBusiness, ILogger<BotBusiness> logger) : IBotBusiness
{
    private DiscordSocketClient _client = new();

    public async Task RunBotAsync()
    {
        try
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
            _client.Ready += ReadyAsync;


            string token = ConfigDto.DiscordConfig.Token;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(-1);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while running the bot. Message: {Message} StackTrace: {StackTrace}", ex.Message, ex.StackTrace);
        }
    }

    private async Task LogAsync(LogMessage log)
    {
        logger.LogInformation(log.ToString());
    }

    private async Task MessageReceivedAsync(SocketMessage message)
    {
        await choicesBusiness.ChoseCoice(message);
    }

    private async Task ReadyAsync()
    {
        while(true)
            Task.WaitAll(birthdayBusiness.CheckAndSendBirthdaysAsync(_client));
    }
}
