using Discord.WebSocket;

namespace Domain.Interface.Business.Bot;

public interface IBirthdayBusiness
{
    Task CheckAndSendBirthdaysAsync(DiscordSocketClient client);
}
