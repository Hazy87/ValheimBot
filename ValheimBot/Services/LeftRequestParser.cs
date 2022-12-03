namespace ValheimBot.Services;

class LeftRequestParser : ILeftRequestParser
{
    private readonly ILeftRequestParser _leftRequestParser;
    private readonly IRandomLeavingInsultRepo _randomLeavingInsultRepo;

    public LeftRequestParser(ILeftRequestParser leftRequestParser, IRandomLeavingInsultRepo randomLeavingInsultRepo)
    {
        _leftRequestParser = leftRequestParser;
        _randomLeavingInsultRepo = randomLeavingInsultRepo;
    }
    public string GetUserName(string content)
    {
        var userName = _leftRequestParser.GetUserName(content);
        var template = _randomLeavingInsultRepo.GetInsult();
        return string.Format(template, userName);
    }
}