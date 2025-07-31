using Domain.Enum.Lol;

namespace Domain.DTO.Lol;

public class ChampionAndRolesDto
{
    public string Name { get; set; } = string.Empty;
    public List<ERoles> Roles { get; set; } = [];
}