using Domain.DTO.Genshin;
using Domain.Enum.Genshin;

namespace Domain.Business.Genshin;

public class GenshinBusiness
{
    public GenshinCharacterDto SelectCharacter(StarsEnum stars)
    {
        var listOfCharacters = ListOfCharactersHandler._listOfCharacter.Where(x => x.Star == stars);

        var rand = new Random();

        return listOfCharacters.ElementAt(rand.Next(listOfCharacters.Count()));
    }
}
