using System.Text.RegularExpressions;

namespace ValheimBot.Services;

class DiedRequestParser : IDiedRequestParser
{
    public string GetUserName(string content)
    {
        var match = Regex.Match(content, "(.*) just died!");
        return match.Groups[1].Value.Trim();
    }
}