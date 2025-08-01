using Discord.WebSocket;

namespace Domain.Interface.Business.Bot;

public interface IJokeBusiness
{
    bool MudaeWrongPlaceValidate(string message, string channelName);
    string SortearNoCanalDeVoz(SocketMessage message);
    Task EnviarMensagemParaCargo(SocketMessage message);
    Task Vampetaco(SocketMessage message);
}