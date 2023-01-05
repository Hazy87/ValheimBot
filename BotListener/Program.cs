// See https://aka.ms/new-console-template for more information

using System.Globalization;
using Common.Interfaces;
using Common.Services;
using Microsoft.Extensions.DependencyInjection;
using SteamQueryNet;
using SteamQueryNet.Interfaces;
using Telegram.Bot;
using ValheimBot;
using ValheimBot.Interfaces;

class Program
{
    public static async Task Main(string[] args)
    {
        ServiceCollection sc = new ServiceCollection();
        sc.AddSingleton<IUserRepository, UserRepository>();
        sc.AddSingleton<IConfigService, ConfigService>();

        sc.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(provider.GetService<IConfigService>().GetValue(ConfigConstants.Token)));
        var services = sc.BuildServiceProvider();
        ITelegramBotClient client = services.GetService<ITelegramBotClient>();
        await client.ReceiveAsync(async (botClient, update, arg3) =>
        {
            switch (update?.Message?.Text.Split("@")[0])
            {
                case "/server":
                    IServerQuery serverQuery = new ServerQuery();
                    serverQuery.Connect("192.168.10.4", 2457);
                    var players = (await serverQuery.GetPlayersAsync());
                    if(players.Count == 0)
                        await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"Currently 0 players online.");
                    else
                        await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"Currently online are {string.Join(", ", players.Select(x => x.Name))}");
                    break;
                case "/deaths":
                    var users = await services.GetService<IUserRepository>().GetUsers();
                    var message = string.Join(Environment.NewLine,
                        users.Select(x =>
                            $"{x.UserId} has had {x.Deaths?.Count} deaths, of which {x.Deaths?.Where(death => parseMyDate(death) > DateTime.Now.AddDays(-1)).Count()} were today."));
                    await botClient.SendTextMessageAsync(update.Message.Chat.Id, message);
                    break;
                    
            }
        }, (botClient, exception, arg3) => { throw new Exception(); });
    }

    private static DateTime parseMyDate(string death)
    {
        DateTime result;
        if (DateTime.TryParse(death, out result))
            return result;
        return DateTime.ParseExact(death, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
    }
}  