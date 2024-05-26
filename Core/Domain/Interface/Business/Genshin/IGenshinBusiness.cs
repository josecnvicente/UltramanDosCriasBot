using Domain.DTO.Genshin;

namespace Domain.Interface.Business.Genshin;

public interface IGenshinBusiness
{
    List<GenshinCharacterDto> PullTenCharacters();
}
