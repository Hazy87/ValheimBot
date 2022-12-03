namespace ValheimBot.Services;

public class DiedResponseMaker : IDiedResponseMaker
{
    private readonly IDiedRequestParser _diedRequestParser;
    private readonly IRandomDeathInsultRepo _randomDeathInsultRepo;
    private readonly IUserRepository _userRepository;

    public DiedResponseMaker(IDiedRequestParser diedRequestParser, IRandomDeathInsultRepo randomDeathInsultRepo, IUserRepository userRepository)
    {
        _diedRequestParser = diedRequestParser;
        _randomDeathInsultRepo = randomDeathInsultRepo;
        _userRepository = userRepository;
    }
    public string GetResponse(string content)
    {
        var userName = _diedRequestParser.GetUserName(content);
        _userRepository.AddDeath(userName);
        var template = _randomDeathInsultRepo.GetInsult();
        return string.Format(template, userName);
    }
}