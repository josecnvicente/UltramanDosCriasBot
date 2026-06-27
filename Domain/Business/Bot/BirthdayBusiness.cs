using Discord;
using Discord.WebSocket;
using Domain.Interface.Business.Bot;
using Microsoft.Extensions.Logging;

namespace Domain.Business.Bot;

public class BirthdayBusiness(ILogger<BirthdayBusiness> logger,
    ISharedBusiness sharedBusiness) : IBirthdayBusiness
{
    private Dictionary<string, DateTime> _birthdays = new Dictionary<string, DateTime>()
    {
        { "dwolfwood", new DateTime(1996, 6, 4) },
        { "jesususouaegis4", new DateTime(1996, 6, 28) },
        { "manodosgato", new DateTime(1995, 12,15) },
        { "auron14", new DateTime(1996, 5, 14) },
        { "yomi_x3", new DateTime(2000, 11, 14) },
        { "g_guerra", new DateTime(1996, 10, 30) },
        { "xwnandoz", new DateTime(1990, 9, 03)  },
        { "garland_invincible", new DateTime(1990, 8, 20) },
        { "yanorth0", new DateTime(1990, 3, 5) }
    };

    public async Task CheckAndSendBirthdaysAsync(DiscordSocketClient client)
    {
        TimeZoneInfo brazilTZ = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        DateTime brazilDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brazilTZ);

        // Garante que o processo rode apenas uma vez por dia utilizando SharedBusiness.DailyCache
        if (await sharedBusiness.DailyCache(brazilDate.Date))
        {
            logger.LogInformation("Verificação de aniversários já executada para {Date}", brazilDate.Date);
            Task.Delay(3600000).Wait();
            return;
        }

        var todays = _birthdays
            .Where(kvp => kvp.Value.Day == brazilDate.Day && kvp.Value.Month == brazilDate.Month)
            .ToList();

        if (!todays.Any())
            return;

        foreach (var entry in todays)
        {

            string username = entry.Key;
            try
            {
                bool sent = false;

                foreach (var guild in client.Guilds)
                {
                    var users = new List<IGuildUser>();
                    await foreach (var userChunk in guild.GetUsersAsync())
                    {
                        users.AddRange(userChunk);
                    }

                    var user = users.FirstOrDefault(u =>
                        string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase) ||
                        (!string.IsNullOrEmpty(u.Nickname) && string.Equals(u.Nickname, username, StringComparison.OrdinalIgnoreCase)));

                    if (user == null)
                        continue;

                    var textChannel = guild.TextChannels
                        .OrderBy(c => c.Position)
                        .FirstOrDefault(c => string.Equals(c.Name, "chat", StringComparison.OrdinalIgnoreCase))
                        ?? guild.TextChannels.OrderBy(c => c.Position).FirstOrDefault();

                    if (textChannel == null)
                    {
                        logger.LogInformation("Nenhum canal de texto disponível na guilda {GuildId} para anunciar aniversário de {User}", guild.Id, username);
                        continue;
                    }

                    string message = $"🎉 Hoje é aniversário de {user.Mention}! Parabéns!";
                    await textChannel.SendMessageAsync(message);
                    sent = true;
                }

                if (!sent)
                {
                    logger.LogInformation("Usuário '{Username}' com aniversário hoje não foi encontrado em nenhuma guilda.", username);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao anunciar aniversário de {Username}: {Message}", entry.Key, ex.Message);
            }
        }
    }
}
