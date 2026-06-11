using Discord.WebSocket;
using Domain.Interface.Business.Bot;

namespace Domain.Business.Bot;

public class ChoicesBusiness(ILolBusiness lolBusiness, IJokeBusiness jokeBusiness)
    : IChoicesBusiness
{
    public async Task ChoseCoice(SocketMessage message)
    {
        var messageString = message.Content.ToLower();

        if (VerifyMounth(messageString))
            await jokeBusiness.MonthJoke(message);

        if (messageString.Contains("filmaço"))
            await jokeBusiness.VerifyFriday(message);
        else if (MudaeWrongPlaceValidate(messageString, message.Channel.Name))
            await jokeBusiness.MudaeWrongPlace(message);
        else if (messageString.Equals("_lolrndteam"))
            await message.Channel.SendMessageAsync(lolBusiness.RandomLolTeam());
        else if (messageString.Equals("_sorteio"))
            await message.Channel.SendMessageAsync(jokeBusiness.SortearNoCanalDeVoz(message));
        else if (messageString.Equals("@feeders"))
            await jokeBusiness.EnviarMensagemParaCargo(message);
        else if (messageString.Equals("_vampetarussa"))
            await jokeBusiness.Vampetaco(message);
        else if (messageString.Contains("boiola"))
            await jokeBusiness.Boiola(message);
    }

    private bool MudaeWrongPlaceValidate(string message, string channelName)
    {
        return (!channelName.Equals("mudae") ||
                !channelName.Equals("ok1") ||
                !channelName.Equals("ok2")) &
               (message.Equals("$wa") || message.Equals("$ma") || message.Equals("$ha") ||
                message.Equals("$wx") || message.Equals("$mx") || message.Equals("$hx") ||
                message.Equals("$wg") || message.Equals("$mg") || message.Equals("$hg") ||
                message.Equals("$w") || message.Equals("$m") || message.Equals("$h"));
    }

    private bool VerifyMounth(string messageString)
    {
        int[] validMonths = [ 6 ];

        DateTime now = DateTime.Now;
        TimeZoneInfo brazilTZ = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        DateTime brazilTime = TimeZoneInfo.ConvertTime(now, brazilTZ);

        if (validMonths.Contains(brazilTime.Month))
            return true;
        return false;
    }
}
