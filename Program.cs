using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Win32;


string pastaAppData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PcMonitor");
string arquivoConfig = Path.Combine(pastaAppData, "config.json");

Directory.CreateDirectory(pastaAppData);

Configuracao config;


if (!File.Exists(arquivoConfig))
{
    Console.WriteLine("========================================");
    Console.WriteLine("   BEM-VINDO AO PC MONITOR (SETUP)     ");
    Console.WriteLine("========================================");
    Console.WriteLine("Parece ser a primeira vez rodando!\n");

    Console.Write("Digite o seu Bot Token do Telegram: ");
    string? token = Console.ReadLine()?.Trim();

    Console.Write("Digite o seu Chat ID do Telegram: ");
    string? chat = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(chat))
    {
        Console.WriteLine("Valores inválidos. Encerrando...");
        return;
    }

    Console.Write("\nDeseja iniciar junto com o Windows automaticamente? (s/n): ");
    bool autostart = Console.ReadLine()?.Trim().ToLower() == "s";

    config = new Configuracao { BotToken = token, ChatId = chat };
    File.WriteAllText(arquivoConfig, JsonSerializer.Serialize(config));

    if (autostart)
    {
        ConfigurarInicializacaoWindows(true);
        Console.WriteLine("✔ Configurado para iniciar com o Windows!");
    }

    Console.WriteLine("\nConfiguração salva com sucesso!\n");
}
else
{
    string json = File.ReadAllText(arquivoConfig);
    config = JsonSerializer.Deserialize<Configuracao>(json)!;
}


string computador = Environment.MachineName;
string usuario = Environment.UserName;
DateTime horario = DateTime.Now;

Console.WriteLine("==============================");
Console.WriteLine("        PC MONITOR");
Console.WriteLine("==============================");
Console.WriteLine($"Computador: {computador}");
Console.WriteLine($"Usuário:    {usuario}");
Console.WriteLine($"Horário:    {horario:dd/MM/yyyy HH:mm:ss}");
Console.WriteLine("Enviando notificação para o Telegram...");


using HttpClient client = new();
string url = $"https://api.telegram.org/bot{config.BotToken}/sendMessage";

var payload = new
{
    chat_id = config.ChatId,
    text = $"🖥️ *PC Monitor - Inicialização*\n\n" +
           $"👤 *Usuário:* `{usuario}`\n" +
           $"💻 *Máquina:* `{computador}`\n" +
           $"🕒 *Data/Hora:* `{horario:dd/MM/yyyy HH:mm:ss}`",
    parse_mode = "Markdown"
};

try
{
    HttpResponseMessage resposta = await client.PostAsJsonAsync(url, payload);
    if (resposta.IsSuccessStatusCode)
    {
        Console.WriteLine("✔ Notificação enviada com sucesso!");
    }
    else
    {
        string erro = await resposta.Content.ReadAsStringAsync();
        Console.WriteLine($"❌ Erro ao enviar: {erro}");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Falha na conexão: {ex.Message}");
}

await Task.Delay(3000);

static void ConfigurarInicializacaoWindows(bool ativar)
{
    string chaveNome = "PcMonitor";
    string executavelPath = Environment.ProcessPath ?? "";

    using RegistryKey? chave = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
    if (chave != null)
    {
        if (ativar && !string.IsNullOrEmpty(executavelPath))
        {
            chave.SetValue(chaveNome, $"\"{executavelPath}\"");
        }
        else
        {
            chave.DeleteValue(chaveNome, false);
        }
    }
}

public class Configuracao
{
    public string BotToken { get; set; } = string.Empty;
    public string ChatId { get; set; } = string.Empty;
}