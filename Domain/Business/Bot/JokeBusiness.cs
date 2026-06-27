using Discord.WebSocket;
using Domain.Interface.Business.Bot;
using Microsoft.Extensions.Logging;

namespace Domain.Business.Bot;

public class JokeBusiness(ISharedBusiness sharedBusiness, ILogger<JokeBusiness> logger) : IJokeBusiness
{
    public async Task VerifyFriday(SocketMessage message)
    {
        if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            await message.Channel.SendMessageAsync("SEXTOU, HOJE É DIA DE FILMAÇO!");
        else
            await message.Channel.SendMessageAsync("FILMAÇO SÓ SEXTA, MALUCÃO.");
    }

    public async Task<string> SortearNoCanalDeVoz(SocketMessage message)
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

    public async Task MudaeWrongPlace(SocketMessage message)
        => await message.Channel.SendMessageAsync($"{message.Author.GlobalName} é viado!");

    public async Task Boiola(SocketMessage message)
    {
        string path = AppContext.BaseDirectory;
        string pastaImagens = Path.Combine(path, "Misc", "Imagens");

        // Verifica se a pasta existe antes de tentar listar arquivos
        if (!Directory.Exists(pastaImagens))
        {
            logger.LogWarning("Pasta de imagens não encontrada.");
            return;
        }

        // Procura especificamente por "boiola.jpg" (ignora maiúsculas/minúsculas)
        var imagemSelecionada = Directory.GetFiles(pastaImagens, "*.*")
            .FirstOrDefault(f => string.Equals(Path.GetFileName(f), "boiola.jpg", StringComparison.OrdinalIgnoreCase));

        if (string.IsNullOrEmpty(imagemSelecionada) || !File.Exists(imagemSelecionada))
        {
            logger.LogWarning("`boiola.jpg` não encontrada na pasta.");
            return;
        }

        using (var stream = new FileStream(imagemSelecionada, FileMode.Open, FileAccess.Read))
        {
            await message.Channel.SendFileAsync(stream, Path.GetFileName(imagemSelecionada), "");
        }
    }

    public async Task Vampetaco(SocketMessage message)
    {
        string path = AppContext.BaseDirectory;

        string pastaImagens = Path.Combine(path, "Misc", "Imagens", "Vampetaco");

        if (!Directory.Exists(pastaImagens))
        {
            logger.LogWarning("Pasta de imagens 'Vampetaco' não encontrada.");
            return;
        }

        // Obtém todos os arquivos de imagem da pasta
        var arquivos = Directory.GetFiles(pastaImagens, "*.*")
            .Where(f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
            .ToArray();

        if (arquivos.Length == 0)
        {
            logger.LogWarning("Nenhuma imagem encontrada na pasta.");
            return;
        }

        // Escolhe uma imagem aleatória
        var random = new Random();
        var imagemSelecionada = arquivos[random.Next(arquivos.Length)];

        // Envia a imagem
        using (var stream = new FileStream(imagemSelecionada, FileMode.Open, FileAccess.Read))
        {
            await message.Channel.SendFileAsync(stream, Path.GetFileName(imagemSelecionada), "");
        }
    }

    public async Task MonthJoke(SocketMessage message)
    {
        string[] validUsersPrideMounth = ["jesususouaegis4"];
        string[] validUsersCrazyDogMounth = ["dwolfwood", "yanorth0"];
        int[] validMonths = new[] { 6, 8 };
        DateTime now = DateTime.Now;
        TimeZoneInfo brazilTZ = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
        DateTime brazilTime = TimeZoneInfo.ConvertTime(now, brazilTZ);

        if (validMonths.Contains(brazilTime.Month))
        {
            if (await sharedBusiness.MounthJokeCache(message.Author.Username, brazilTime))
                return;

            if (brazilTime.Month == 6)
            {
                if (validUsersPrideMounth.Contains(message.Author.Username.ToLower()))
                    await message.Channel.SendMessageAsync($"Pode ficar tranquilo {message.Author.Mention}, é Junho, estamos no mês gay. 🌈");
            }
            else if (brazilTime.Month == 8)
            {
                if (validUsersCrazyDogMounth.Contains(message.Author.Username.ToLower()))
                    await message.Channel.SendMessageAsync($"Tome cuidado {message.Author.Mention}! É Agosto, estamos no mês do cachorro louco. 🐶");
            }

        }
    }
}