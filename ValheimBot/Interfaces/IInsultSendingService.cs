namespace ValheimBot.Interfaces;

public interface IInsultSendingService
{
    Task SendMessage(string message);
}