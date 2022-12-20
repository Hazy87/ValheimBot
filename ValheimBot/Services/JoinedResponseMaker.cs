namespace ValheimBot.Services;

public class JoinedResponseMaker : IJoinedResponseMaker
{
    private readonly IJoinedRequestParser _joinedRequestParser;
    private readonly IUserRepository _userRepository;

    public JoinedResponseMaker(IJoinedRequestParser joinedRequestParser, IUserRepository userRepository)
    {
        _joinedRequestParser = joinedRequestParser;
        _userRepository = userRepository;
    }
    public async Task<string> GetResponse(string content)
    {
        var userName = _joinedRequestParser.GetUserName(content);
        var user  = await _userRepository.GetUser(userName);
        user.LastLogin = DateTime.Now;
        await _userRepository.SaveUser(user);
        return $"All welcome {userName}";
    }
}