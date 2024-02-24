using Domain.DTO.Genshin;

namespace Domain.DTO.Common;

public class AccountDto
{
    public int AccountId { get; set; }
    public List<GenshinCharacterDto> GenshinCharacters { get; set; }
}
