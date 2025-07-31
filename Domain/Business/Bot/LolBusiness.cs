using Domain.DTO.Lol;
using Domain.Enum.Lol;
using Domain.Interface.Business.Bot;

namespace Business.Domain.Bot;

public class LolBusiness : ILolBusiness
{
    private ChampionAndRolesDto[] _championsAndRoles = [
        new () { Name = "Aatrox", Roles = new List<ERoles> { ERoles.Top } },
        new () { Name = "Ahri", Roles = new List<ERoles> { ERoles.Mid } },
        new () { Name = "Akali", Roles = new List<ERoles> { ERoles.Top, ERoles.Mid } },
        new () { Name = "Akshan", Roles = new List<ERoles> { ERoles.Mid } },
        new () { Name = "Alistar", Roles = new List<ERoles> { ERoles.Sup } },
        new () { Name = "Ambessa", Roles = new List<ERoles> { ERoles.Mid } },
        new () { Name = "Amumu", Roles = new List<ERoles> { ERoles.Jungle } },
        new () { Name = "Anivia", Roles = new List<ERoles> { ERoles.Mid } },
        new () { Name = "Annie", Roles = new List<ERoles> { ERoles.Mid } },
        new () { Name = "Aphelios", Roles = new List<ERoles> { ERoles.Adc } },
        new () { Name = "Ashe", Roles = new List<ERoles> { ERoles.Adc, ERoles.Sup } }
    ];

    public string RandomLolTeam()
    {
        Random rand = new Random();

        ChampionAndRolesDto[] topChampion = _championsAndRoles.Where(x => 
            x.Roles.Contains(ERoles.Top)).ToArray();
        ChampionAndRolesDto[] jgChampion = _championsAndRoles.Where(x => 
            x.Roles.Contains(ERoles.Jungle)).ToArray();
        ChampionAndRolesDto[] midChampion = _championsAndRoles.Where(x => 
            x.Roles.Contains(ERoles.Mid)).ToArray();
        ChampionAndRolesDto[] adcChampion = _championsAndRoles.Where(x => 
            x.Roles.Contains(ERoles.Adc)).ToArray();
        ChampionAndRolesDto[] supChampion = _championsAndRoles.Where(x => 
            x.Roles.Contains(ERoles.Sup)).ToArray();

        ChampionAndRolesDto top = topChampion[rand.Next(topChampion.Length)];
        ChampionAndRolesDto jungle = jgChampion[rand.Next(jgChampion.Length)];
        ChampionAndRolesDto mid = midChampion[rand.Next(midChampion.Length)];
        ChampionAndRolesDto adc = adcChampion[rand.Next(adcChampion.Length)];
        ChampionAndRolesDto sup = supChampion[rand.Next(supChampion.Length)];

        return $"Top: {top.Name}, Jungle: {jungle.Name}, Mid: {mid.Name}, ADC: {adc.Name}, Support: {sup.Name}";
    }
}