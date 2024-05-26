using Domain.DTO.Genshin;
using Domain.Enum.Genshin;

namespace Domain.Business.Genshin;

public static class ListOfCharactersHandler
{
    public static readonly List<GenshinCharacterDto> _listOfCharacter = new List<GenshinCharacterDto>()
    {
 #region Characters pyro
        new GenshinCharacterDto()
        {
            Name = "Diluc",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = StarsEnum.Five
        },
        new GenshinCharacterDto()
        {
            Name = "Dehya",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = StarsEnum.Five
        },
        new GenshinCharacterDto()
        {
            Name = "Klee",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = StarsEnum.Five
        },
        new GenshinCharacterDto()
        {
            Name = "Hu Tao",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = StarsEnum.Five
        },
        new GenshinCharacterDto()
        {
            Name = "Lyney",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = StarsEnum.Five
        },
        new GenshinCharacterDto()
        {
            Name = "Yoimiya",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = StarsEnum.Five
        },
        new GenshinCharacterDto()
        {
            Name = "Amber",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = StarsEnum.Four
        },
        new GenshinCharacterDto()
        {
            Name = "Bennet",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = StarsEnum.Four
        },
        new GenshinCharacterDto()
        {
            Name = "Chevreuse",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = StarsEnum.Four
        },
        new GenshinCharacterDto()
        {
            Name = "Thoma",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = StarsEnum.Four
        },
        new GenshinCharacterDto()
        {
            Name = "Gaming",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = StarsEnum.Four
        },
        new GenshinCharacterDto()
        {
            Name = "Xiangling",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = StarsEnum.Four
        },
        new GenshinCharacterDto()
        {
            Name = "Xinyan",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = StarsEnum.Four
        },
        new GenshinCharacterDto()
        {
            Name = "Yanfei",
            Element = Enum.Genshin.ElementEnum.Pyro,
            Star = StarsEnum.Four
        },
#endregion
    };
}
