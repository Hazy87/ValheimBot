namespace ValheimBot.Interfaces;

public interface IDiedResponseMaker
{
    Task<string> GetResponse(string content);
}