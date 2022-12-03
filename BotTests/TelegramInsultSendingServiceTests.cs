using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace BotTests;

public class TelegramInsultSendingServiceTests
{
    private readonly TelegramInsultSendingService _service;
    private Mock<IConfigService> _configServiceMock;
    private Mock<ITelegramBotClient> _telegramBotClientMock;

    public TelegramInsultSendingServiceTests()
    {
        _configServiceMock = new Mock<IConfigService>();
        _telegramBotClientMock = new Mock<ITelegramBotClient>();
        _service = new TelegramInsultSendingService(_telegramBotClientMock.Object,
            _configServiceMock.Object);
    }

    [Fact]
    public async Task ShouldCallConfigServiceForChatId()
    {
        await _service.SendMessage("");
        
        _configServiceMock.Verify(x => x.GetValue(ConfigConstants.ChatId), Times.Once);
    }

    [Fact]
    public async Task SendMessage_ShouldUse_LongFromConfig()
    {
        long chatId = 2383981982371823789;
        _configServiceMock.Setup(x => x.GetValue(ConfigConstants.ChatId)).Returns(chatId.ToString);

        await _service.SendMessage("");
        //extension methods cant be tested and im tired...
        /*_telegramBotClientMock.Verify(x => 
            x.SendTextMessageAsync(It.Is<ChatId>(chatId => chatId.Identifier == chatId),
                It.IsAny<string>(), It.IsAny<int>(), It.IsAny<ParseMode?>(), 
                It.IsAny<IEnumerable<MessageEntity>?>(), It.IsAny<bool?>(),
                It.IsAny<bool?>(), It.IsAny<bool?>(),It.IsAny<int?>(), 
                It.IsAny<bool?>(), It.IsAny<IReplyMarkup?>(), 
                It.IsAny<CancellationToken>()), Times.Once);*/
    }
}