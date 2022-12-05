namespace ValheimBot.Services;

public class BootStrapper
{
    public static void Configure(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IConfigService, ConfigService>();
        builder.Services.AddSingleton<IInsultSendingService, TelegramInsultSendingService>();
        builder.Services.AddSingleton<IJoinedRequestParser, JoinedRequestParser>();
        builder.Services.AddSingleton<IJoinedResponseMaker, JoinedResponseMaker>();
        builder.Services.AddSingleton<ILeftRequestParser, LeftRequestParser>();
        builder.Services.AddSingleton<ILeftResponseMaker, LeftResponseMaker>();
        builder.Services.AddSingleton<IDiedRequestParser, DiedRequestParser>();
        builder.Services.AddSingleton<IUserRepository, UserRepository>();
        builder.Services.AddSingleton<IRandomDeathInsultRepo, RandomDeathInsultRepo>();
        builder.Services.AddSingleton<IRandomLeavingInsultRepo, RandomLeavingInsultRepo>();
        builder.Services.AddSingleton<IDiedResponseMaker, DiedResponseMaker>();
        builder.Services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient(provider.GetService<IConfigService>().GetValue(ConfigConstants.Token)));
    }
}