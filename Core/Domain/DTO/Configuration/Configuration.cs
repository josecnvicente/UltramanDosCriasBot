namespace Domain.DTO.Config;

public static class Configuration
{
    public static DiscordDto DiscordConfig { get; set; }

    public class DiscordDto
    {
        public string Token { get; set; }
    }
}
