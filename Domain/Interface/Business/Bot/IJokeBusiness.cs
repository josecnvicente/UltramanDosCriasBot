using Discord.WebSocket;

namespace Domain.Interface.Business.Bot;

public interface IJokeBusiness
{
    Task VerifyFriday(SocketMessage message);
    Task MudaeWrongPlace(SocketMessage message);
    string SortearNoCanalDeVoz(SocketMessage message);
    Task EnviarMensagemParaCargo(SocketMessage message);
    Task Vampetaco(SocketMessage message);
    Task Boiola(SocketMessage message);
    Task MonthJoke(SocketMessage message);
}