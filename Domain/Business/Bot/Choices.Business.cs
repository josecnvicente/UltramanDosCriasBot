using Discord.WebSocket;
using Domain.DTO.Common;
using Domain.DTO.Genshin;
using Domain.Interface.Bot;
using Domain.Interface.Business.Genshin;
using Microsoft.Extensions.Caching.Memory;

namespace ConsoleApp.Bot;

public class ChoicesBusiness : IChoicesBusiness
{

    private List<AccountDto> _account;
    private IMemoryCache _cache;
    private IGenshinBusiness _genshinBusiness;

    public ChoicesBusiness(IMemoryCache cache, IGenshinBusiness genshinBusiness)
    {
        _cache = cache;
        _account = new List<AccountDto>();
        _genshinBusiness = genshinBusiness;
    }

    public async Task ChoseCoice(SocketMessage message)
    {
        if (!message.Content.StartsWith("_") && message.Content.ToLower() != "filmaço")
            return;

        var messageString = message.Content.ToLower();

        SaveUserId(message);

        if (messageString.Equals("filmaço"))
            if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
                await message.Channel.SendMessageAsync("SEXTOU, HOJE É DIA DE FILMAÇO!");
            else
                await message.Channel.SendMessageAsync("FILMAÇO SÓ SEXTA, MALUCÃO.");

        else if (MudaeWrongPlaceValidate(messageString, message.Channel.Name))
            await message.Channel.SendMessageAsync($"{message.Author.GlobalName} é viado!");

        else if (messageString.Equals("_genshintenpull"))
            await SaveGenshinCharactersOnAccount(message);

        else if (messageString.Equals("_genshinverifycharacters"))
            await VerifyCharactersFromAccount(message);
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

    private void SaveUserId(SocketMessage message)
    {
        bool updateCache = true;

        if (_account.Any())
        {
            if (_cache.TryGetValue("ACCOUNT", out List<AccountDto> listAccount))
            {
                if (!listAccount.Select(x => x.AccountId).Contains(message.Author.Id))
                {
                    _account.Add(new AccountDto
                    {
                        AccountId = message.Author.Id,
                        GenshinCharacters = new List<GenshinCharacterDto>()
                    });
                }
                else
                    updateCache = false;
            }
        }
        else
            _account.Add(new AccountDto
            {
                AccountId = message.Author.Id,
                GenshinCharacters = new List<GenshinCharacterDto>()
            }
            );

        if (updateCache)
            _cache.Set("ACCOUNT", _account);
    }

    private async Task SaveGenshinCharactersOnAccount(SocketMessage message)
    {
        var accountToEdit = _account.Where(x => x.AccountId == message.Author.Id).FirstOrDefault();

        var characters = _genshinBusiness.PullTenCharacters();

        string messageToSend = $"{message.Author.GlobalName} deu 10 tiros em Genshin Impact e obteve:\n";

        foreach (var character in characters)
            messageToSend += $"Nome: {character.Name} - Elemento: {Enum.GetName(character.Element)} - Estrelas: {(int)character.Star}\n";

        await message.Channel.SendMessageAsync(messageToSend);

        accountToEdit.GenshinCharacters.AddRange(characters);

        _account = _account.Where(x => x.AccountId != message.Author.Id).ToList();

        _account.Add(accountToEdit);

        _cache.Set(_account, _account);
    }

    private async Task VerifyCharactersFromAccount(SocketMessage message)
    {
        var characters = _account.Where(x => x.AccountId == message.Author.Id).Select(x => x.GenshinCharacters).FirstOrDefault();

        if (!characters.Any())
        {
            await message.Channel.SendMessageAsync("Você não tem personagens");
            return;
        }

        var messageString = $"{message.Author.GlobalName} tem os seguintes personagens na conta:\n";

        foreach (var character in characters)
            messageString += $"Nome: {character.Name} - Elemento: {Enum.GetName(character.Element)} - Estrelas: {(int)character.Star}\n";

        await message.Channel.SendMessageAsync(messageString);
    }
}
