using Telegram.Bot.Types;

namespace ValheimBot.Services;

public class TelegramInsultSendingService : IInsultSendingService
{
    private readonly ITelegramBotClient _client;
    private readonly IConfigService _configService;

    public TelegramInsultSendingService(ITelegramBotClient client, IConfigService configService)
    {
        _client = client;
        _configService = configService;
    }

    public async Task SendMessage(string message)
    {
        var chatId = long.Parse(_configService.GetValue(ConfigConstants.ChatId));
        await _client.SendTextMessageAsync(new ChatId(chatId), message);
    }
}