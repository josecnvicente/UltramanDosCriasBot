using Domain.DTO.Genshin;

namespace Domain.Business.Genshin;

public static class ListOfCharactersHandler
{
    private static readonly List<GenshinCharacterDto> _listOfCharacter = new List<GenshinCharacterDto>()
    {
 #region Characters pyro
        new GenshinCharacterDto()
        {
            Name = "Diluc",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = 5
        },
        new GenshinCharacterDto()
        {
            Name = "Dehya",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = 5
        },
        new GenshinCharacterDto()
        {
            Name = "Klee",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = 5
        },
        new GenshinCharacterDto()
        {
            Name = "Hu Tao",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = 5
        },
        new GenshinCharacterDto()
        {
            Name = "Lyney",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = 5
        },
        new GenshinCharacterDto()
        {
            Name = "Yoimiya",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = 5
        },
        new GenshinCharacterDto()
        {
            Name = "Amber",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = 4
        },
        new GenshinCharacterDto()
        {
            Name = "Bennet",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = 4
        },
        new GenshinCharacterDto()
        {
            Name = "Chevreuse",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = 4
        },
        new GenshinCharacterDto()
        {
            Name = "Thoma",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = 4
        },
        new GenshinCharacterDto()
        {
            Name = "Gaming",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = 4
        },
        new GenshinCharacterDto()
        {
            Name = "Xiangling",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = 4
        },
        new GenshinCharacterDto()
        {
            Name = "Xinyan",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = 4
        },
        new GenshinCharacterDto()
        {
            Name = "Yanfei",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = 4
        },
#endregion
    };
}
