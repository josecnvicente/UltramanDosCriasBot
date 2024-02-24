using Discord.WebSocket;
using Domain.DTO.Common;
using Domain.Interface.Bot;
using Microsoft.Extensions.Caching.Memory;

namespace ConsoleApp.Bot;

public class ChoicesBusiness : IChoicesBusiness
{

    private List<AccountDto> _account;
    private IMemoryCache _cache;

    public ChoicesBusiness(IMemoryCache cache)
    {
        _cache = cache;
        _account = cache.GetOrCreate("AccountCache", x =>
        {
            x.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            return new List<AccountDto>();
        });
    }

    public async Task ChoseCoice(SocketMessage message)
    {
        var messageString = message.Content.ToLower();

        if (messageString.Equals("filmaço"))
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                await message.Channel.SendMessageAsync("SEXTOU, HOJE É DIA DE FILMAÇO!");
            else
                await message.Channel.SendMessageAsync("FILMAÇO SÓ SEXTA, MALUCÃO.");

        else if (MudaeWrongPlaceValidate(messageString, message.Channel.Name))
            await message.Channel.SendMessageAsync($"{message.Author.GlobalName} é viado!");
    }

    private bool MudaeWrongPlaceValidate(string message, string channelName)
    {
        return (!channelName.Equals("mudae") ||
            !channelName.Equals("ok1") ||
            !channelName.Equals("ok2")) &
        (message.Equals("$wa") || message.Equals("$ma") || message.Equals("$ha") ||
        message.Equals("$wx") || message.Equals("$mx") || message.Equals("$hx") ||
        message.Equals("$wg") || message.Equals("$mg") || message.Equals("$hg") ||
        message.Equals("$w") || message.Equals("$m") || message.Equals("$h"));
    }
}
