namespace ValheimBot.Interfaces;

public interface IJoinedResponseMaker
{
    Task<string> GetResponse(string content);
}