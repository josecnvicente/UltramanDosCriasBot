using Discord;
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

    public async Task Boiola(SocketMessage message)
    {
        string path = AppContext.BaseDirectory;
        string pastaImagens = Path.Combine(path, "Misc\\Imagens");

        // Procura especificamente por "boiola.jpg" (ignora maiúsculas/minúsculas)
        var imagemSelecionada = Directory.GetFiles(pastaImagens, "*.*")
            .FirstOrDefault(f => string.Equals(Path.GetFileName(f), "boiola.jpg", StringComparison.OrdinalIgnoreCase));

        if (string.IsNullOrEmpty(imagemSelecionada) || !File.Exists(imagemSelecionada))
        {
            await message.Channel.SendMessageAsync("⚠️ `boiola.jpg` não encontrada na pasta.");
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
        string pastaImagens = Path.Combine(path, "Misc\\Imagens\\Vampetaco");


        // Obtém todos os arquivos de imagem da pasta
        var arquivos = Directory.GetFiles(pastaImagens, "*.*")
            .Where(f => f.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
            .ToArray();

        if (arquivos.Length == 0)
        {
            await message.Channel.SendMessageAsync("⚠️ Nenhuma imagem encontrada na pasta.");
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

    public async Task EnviarMensagemParaCargo(SocketMessage message)
    {
        string nomeDoCargo = "feeders";
        string caminhoDoAudio = AppContext.BaseDirectory;
        List<string> usuariosNaoEnviado = new List<string>();
        Dictionary<string, string> nomeParaNick = new Dictionary<string, string>
        {
            { "dwolfwood", "Amanda" },
            { "auron14", "Brunao" },
            { "yomi_x3", "Yomi" },
            { "xwnandoz", "Nando" }
        };

        if (message.Author.IsBot)
            return;

        // Obtém o usuário como membro do servidor
        var usuario = message.Author as SocketGuildUser;
        var guild = usuario?.Guild;

        if (guild == null)
        {
            await message.Channel.SendMessageAsync("❌ Comando só pode ser usado em servidores.");
            return;
        }

        // Procura o cargo pelo nome
        var cargo = guild.Roles.FirstOrDefault(r =>
            r.Name.Equals(nomeDoCargo, StringComparison.OrdinalIgnoreCase));

        if (cargo == null)
        {
            await message.Channel.SendMessageAsync($"❌ Cargo '{nomeDoCargo}' não encontrado.");
            return;
        }

        var membrosComCargo = guild.Users.Where(u => u.Roles.Contains(cargo) && !u.IsBot).ToList();

        if (membrosComCargo.Count == 0)
        {
            await message.Channel.SendMessageAsync($"⚠️ Nenhum membro com o cargo '{nomeDoCargo}' foi encontrado.");
            return;
        }

        foreach (var membro in membrosComCargo)
        {
            try
            {
                if (membro.Username.ToLower() == message.Author.Username)
                    continue;

                if (nomeParaNick.ContainsKey(membro.Username.ToLower()))
                {
                    string caminhoCompleto = $"{caminhoDoAudio}//{nomeParaNick[membro.Username.ToLower()]}.mp3";
                    var dmChannel = await membro.CreateDMChannelAsync();
                    await dmChannel.SendFileAsync(caminhoCompleto,
                        $"{usuario.DisplayName} te chamou para feedar no Sindicato dos Crias.");
                }
                else
                    await membro.SendMessageAsync($"{usuario.DisplayName} te chamou para feedar no Sindicato dos Crias.");
            }
            catch
            {
                usuariosNaoEnviado.Add(membro.DisplayName);
            }
        }

        if (usuariosNaoEnviado.Count > 0 && membrosComCargo.Count < usuariosNaoEnviado.Count)
        {
            string usuariosNaoEnviadosStr = string.Join(", ", usuariosNaoEnviado);
            await message.Channel.SendMessageAsync($"⚠️ Não foi possível enviar mensagem para: {usuariosNaoEnviadosStr}");
        }
        else if (usuariosNaoEnviado.Count > 0 && membrosComCargo.Count > usuariosNaoEnviado.Count)
        {
            string usuariosNaoEnviadosStr = string.Join(", ", usuariosNaoEnviado);
            await message.Channel.SendMessageAsync($@"⚠️ Mensagem enviada para alguns membros com cargo '{nomeDoCargo}'.
Exceto: {usuariosNaoEnviadosStr}.");
        }
        else
            await message.Channel.SendMessageAsync($"✅ Mensagem enviada para todos os membros com o cargo '{nomeDoCargo}'.");
    }
}