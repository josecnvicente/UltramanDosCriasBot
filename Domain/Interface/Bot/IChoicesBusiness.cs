using Discord.WebSocket;

namespace Domain.Interface.Bot;

public interface IChoicesBusiness
{
    Task ChoseCoice(SocketMessage message);
}
