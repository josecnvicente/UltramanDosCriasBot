using Domain.DTO.Genshin;
using Domain.Enum.Genshin;
using Domain.Interface.Business.Genshin;

namespace Domain.Business.Genshin;

public class GenshinBusiness : IGenshinBusiness
{
    public List<GenshinCharacterDto> PullTenCharacters()
    {
        var characterList = new List<GenshinCharacterDto>();
        for (int c = 0; c < 10; c++)
        {
            if (c == 9 && !characterList.Where(x => x.Star == StarsEnum.Four || x.Star == StarsEnum.Five).Any())
                characterList.Add(SelectCharacter(StarsEnum.Four));
            else
                characterList.Add(PercentageCharacter());
        }

        return characterList.Where(x => x.Name != "flop").ToList();
    }

    private GenshinCharacterDto PercentageCharacter()
    {
        var fiveStar = 60;
        var fourstar = 561;
        GenshinCharacterDto character;

        var rng = new Random().Next(1, 10000);

        if (fiveStar >= rng)
            return character = SelectCharacter(StarsEnum.Five);
        else if (fiveStar < rng && rng <= fourstar)
            return character = SelectCharacter(StarsEnum.Four);

        return new GenshinCharacterDto()
        {
            Name = "flop"
        };
    }

    private GenshinCharacterDto SelectCharacter(StarsEnum stars)
    {
        var listOfCharacters = ListOfCharactersHandler._listOfCharacter.Where(x => x.Star == stars);

        var rand = new Random();

        return listOfCharacters.ElementAt(rand.Next(listOfCharacters.Count()));
    }
}
