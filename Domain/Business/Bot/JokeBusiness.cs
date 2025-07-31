using Discord.WebSocket;
using Domain.Interface.Business.Bot;

namespace Business.Domain.Bot;

public class JokeBusiness : IJokeBusiness
{
    public string SortearNoCanalDeVoz(SocketMessage message)
    {
        if (message.Author.IsBot)
            return "";
    
        var user = message.Author as SocketGuildUser;
        var voiceChannel = user?.VoiceChannel;

        if (voiceChannel == null)
            return "Você precisa estar em um canal de voz para fazer o sorteio.";

        List<SocketGuildUser> membrosNaCall = voiceChannel.Users
            .Where(u => !u.IsBot && u.VoiceChannel != null)
            .ToList();
        
        if (membrosNaCall.Count == 0)
            return "Não há membros suficientes na call.";

        Random random = new Random();
        SocketGuildUser? sorteado = membrosNaCall[random.Next(membrosNaCall.Count)];

        return $"🎉 Sorteado: **{sorteado.DisplayName}**!";
    }
    
    public bool MudaeWrongPlaceValidate(string message, string channelName)
    {
        return (!channelName.Equals("mudae") ||
                !channelName.Equals("ok1") ||
                !channelName.Equals("ok2")) &
               (message.Equals("$wa") || message.Equals("$ma") || message.Equals("$ha") ||
                message.Equals("$wx") || message.Equals("$mx") || message.Equals("$hx") ||
                message.Equals("$wg") || message.Equals("$mg") || message.Equals("$hg") ||
                message.Equals("$w") || message.Equals("$m") || message.Equals("$h"));
    }
}