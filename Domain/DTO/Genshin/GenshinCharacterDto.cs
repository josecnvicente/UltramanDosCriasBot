using Domain.Enum.Genshin;

namespace Domain.DTO.Genshin;

public class GenshinCharacterDto
{
    public string Name { get; set; }
    public ElementEnum Element { get; set; }
    public int Star { get; set; }
}
