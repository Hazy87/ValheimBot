using System.Text.RegularExpressions;

namespace ValheimBot.Services;

class LeftRequestParser : ILeftRequestParser
{
    public string GetUserName(string content)
    {
        var match = Regex.Match(content, "Player(.*)spawned into the world");
        return match.Groups[1].Value.Trim();
    }
}