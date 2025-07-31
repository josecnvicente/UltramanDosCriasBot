using Discord.WebSocket;

namespace Domain.Interface.Business.Bot;

public interface IChoicesBusiness
{
    Task ChoseCoice(SocketMessage message);
}
