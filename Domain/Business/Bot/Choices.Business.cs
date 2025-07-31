using Discord.WebSocket;
using Domain.Interface.Business.Bot;
using Microsoft.Extensions.Caching.Memory;

namespace Business.Domain.Bot;

public class ChoicesBusiness(IMemoryCache cache, ILolBusiness lolBusiness, IJokeBusiness jokeBusiness) : IChoicesBusiness
{
    public async Task ChoseCoice(SocketMessage message)
    {
        if (!message.Content.StartsWith("_") && message.Content.ToLower() != "filmaço"
            && message.Content.ToLower() != "$")
            return;

        var messageString = message.Content.ToLower();

        if (messageString.Equals("filmaço"))
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                await message.Channel.SendMessageAsync("SEXTOU, HOJE É DIA DE FILMAÇO!");
            else
                await message.Channel.SendMessageAsync("FILMAÇO SÓ SEXTA, MALUCÃO.");
        else if (jokeBusiness.MudaeWrongPlaceValidate(messageString, message.Channel.Name))
            await message.Channel.SendMessageAsync($"{message.Author.GlobalName} é viado!");
        else if (messageString.Equals("_lolrndteam"))
            await message.Channel.SendMessageAsync(lolBusiness.RandomLolTeam());
        else if (messageString.Equals("_sorteio"))
            await message.Channel.SendMessageAsync(jokeBusiness.SortearNoCanalDeVoz(message));
    }
}
