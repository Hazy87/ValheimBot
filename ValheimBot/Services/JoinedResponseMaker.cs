namespace ValheimBot.Services;

public class JoinedResponseMaker : IJoinedResponseMaker
{
    private readonly IJoinedRequestParser _joinedRequestParser;

    public JoinedResponseMaker(IJoinedRequestParser joinedRequestParser)
    {
        _joinedRequestParser = joinedRequestParser;
    }
    public string GetResponse(string content)
    {
        var userName = _joinedRequestParser.GetUserName(content);
        return $"All welcome {userName}";
    }
}