using Assert = NUnit.Framework.Assert;

namespace BotTests;

public class JoinedRequestParserTests
{
    private readonly JoinedRequestParser _service;

    public JoinedRequestParserTests()
    {
        _service = new JoinedRequestParser();
    }

    [Fact]
    public void TestRegex()
    {
        var expectedUserName = "other hazy";
        var userName = _service.GetUserName($"Player {expectedUserName} spawned into the world");
        Assert.AreEqual(expectedUserName, userName);
    }
}